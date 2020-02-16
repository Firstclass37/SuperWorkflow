using System.Threading.Tasks;
using WorkflowApp.Implementations;

namespace WorkflowApp.Interfaces
{
    public interface IRunnerPool
    {
        Task<ExecutionResult> Enqueue(ExecutionTask workItem);
    }
}