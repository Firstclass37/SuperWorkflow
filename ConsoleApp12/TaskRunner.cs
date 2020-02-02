using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp12
{
    internal class TaskRunner
    {
        private readonly ConcurrentQueue<Task> _queue;
        private readonly ConcurrentDictionary<Task, bool> _running;
        private readonly Timer _timer;
        private readonly int _concurrent;

        public TaskRunner(int concurrent)
        {
            _concurrent = concurrent;
            _running = new ConcurrentDictionary<Task, bool>();
            _queue = new ConcurrentQueue<Task>();
            _timer = new Timer(s => Run(), null, 0, 1);
        }

        public void Add(Task task)
        {
            _queue.Enqueue(task);
        }

        private bool TryRun(Task task)
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