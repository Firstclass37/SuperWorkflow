using WorkflowApp.Interfaces;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace WorkflowApp.Implementations
{
    internal class Runner
    {
        private readonly ConcurrentQueue<Task<IWorkItem>> _queue;
        private readonly ConcurrentDictionary<Task, bool> _running;
        private readonly Timer _timer;
        private readonly int _concurrent;

        public Runner(int concurrent)
        {
            _concurrent = concurrent;
            _running = new ConcurrentDictionary<Task, bool>();
            _queue = new ConcurrentQueue<Task<IWorkItem>>();
            _timer = new Timer(s => Run(), null, 0, 1);
        }

        public int QueueLength => _queue.Count;

        public Task<IWorkItem> Enqueue(IWorkItem workItem)
        {
            var task = new Task<IWorkItem>(() => { workItem.Work(); return workItem; });
            _queue.Enqueue(task);
            return task;
        }

        private bool TryRun(Task<IWorkItem> task)
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
    }
}