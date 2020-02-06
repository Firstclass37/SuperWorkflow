using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using WorkflowApp.Interfaces;

namespace WorkflowApp.Implementations
{
    public class RunnerManager : IRunnerManager
    {
        private readonly ConcurrentDictionary<Guid, IHandler> _sources = new ConcurrentDictionary<Guid, IHandler>();
        private readonly Runner[] _runners = new Runner[] { new Runner(4) };

        public void RegisterSource(Guid sourceId, IHandler handler)
        {
            if (!_sources.TryAdd(sourceId, handler))
                throw new Exception($"source {sourceId} was already registered");
        }

        public void Enqueue(Guid sourceId, IWorkItem item)
        {
            SelectRunner().Enqueue(item).ContinueWith(t => EndExecution(t, sourceId));
        }

        private Runner SelectRunner()
        {
            return _runners.OrderBy(r => r.QueueLength).First();
        }
       
        private void EndExecution(Task<IWorkItem> task, Guid sourceId)
        {
            var handler = _sources[sourceId];
            if (task.IsCompleted)
                handler.OnWorkCompleted(task.Result);
            else if (task.IsFaulted)
                handler.OnException(task.Result, task.Exception);
        }
    }
}