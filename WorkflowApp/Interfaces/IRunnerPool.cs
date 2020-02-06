using System.Threading.Tasks;

namespace WorkflowApp.Interfaces
{
    public interface IRunnerPool
    {
        Task<IWorkItem> Enqueue(IWorkItem workItem);
    }
}