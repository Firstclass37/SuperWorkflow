using ConsoleApp12.Interfaces;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp12.Implementations
{
    internal class Runner
    {
        private readonly ConcurrentQueue<IWorkItem> _queue;
        private readonly ConcurrentDictionary<Task, bool> _running;
        private readonly Timer _timer;
        private readonly int _concurrent;

        public Runner(int concurrent)
        {
            _concurrent = concurrent;
            _running = new ConcurrentDictionary<Task, bool>();
            _queue = new ConcurrentQueue<IWorkItem>();
            _timer = new Timer(s => Run(), null, 0, 1);
        }

        public void Enqueue(IWorkItem workItem)
        {
            _queue.Enqueue(workItem);
        }

        public bool Competed(IWorkItem workItem)
        {
            return !InQueue(workItem);
        }

        public bool InQueue(IWorkItem workItem)
        {
            return _queue.Any(i => i == workItem) || _running.Any(e => e.Key == workItem);
        }

        private bool TryRun(IWorkItem workItem)
        {
            if (_running.Count == _concurrent)
                return false;

            var task = Task.Factory.StartNew(() => workItem.Work()).ContinueWith(t => _running.TryRemove(t, out var _));
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