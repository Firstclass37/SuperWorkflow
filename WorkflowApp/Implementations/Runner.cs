using WorkflowApp.Interfaces;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace WorkflowApp.Implementations
{
    internal class Runner: IRunner
    {
        private readonly ConcurrentQueue<Task<ExecutionResult>> _queue;
        private readonly ConcurrentDictionary<Task, bool> _running;
        private readonly Timer _timer;
        private readonly int _concurrent;

        public Runner(int concurrent)
        {
            _concurrent = concurrent;
            _running = new ConcurrentDictionary<Task, bool>();
            _queue = new ConcurrentQueue<Task<ExecutionResult>>();
            _timer = new Timer(s => Run(), null, 0, 1);
        }

        public int QueueLength => _queue.Count;

        public Task<ExecutionResult> Enqueue(ExecutionTask executetionTask)
        {
            var task = new Task<ExecutionResult>(() => Execute(executetionTask));
            _queue.Enqueue(task);
            return task;
        }

        private bool TryRun(Task<ExecutionResult> task)
        {
            if (_running.Count == _concurrent)
                return false;

            task.ContinueWith(t => _running.TryRemove(t, out var _)).Start();
            _running.TryAdd(task, true);
            return true;
        }

        private void Run()
        {
            while (_queue.TryPeek(out var t) && TryRun(t))
                _queue.TryDequeue(out var _);
        }

        private ExecutionResult Execute(ExecutionTask executetionTask)
        {
            var result = executetionTask.WorkItem.Work(executetionTask.PreviousItemResult?.Result);
            return new ExecutionResult(executetionTask.WorkItem, result);
        }
    }
}