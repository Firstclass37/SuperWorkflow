using System.Threading.Tasks;
using WorkflowApp.Implementations;

namespace WorkflowApp.Interfaces
{
    public interface IRunner
    {
        int QueueLength { get; }

        Task<ExecutionResult> Enqueue(ExecutionTask executetionTask);
    }
}