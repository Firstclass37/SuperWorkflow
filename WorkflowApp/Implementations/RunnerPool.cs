using System.Linq;
using System.Threading.Tasks;
using WorkflowApp.Interfaces;

namespace WorkflowApp.Implementations
{
    public class RunnerPool : IRunnerPool
    {
        private readonly IRunner[] _runners;

        public RunnerPool(IRunner[] runner)
        {
            _runners = runner;
        }

        public Task<IWorkItem> Enqueue(IWorkItem workItem)
        {
            return _runners.OrderBy(r => r.QueueLength).First().Enqueue(workItem);
        }
    }
}
