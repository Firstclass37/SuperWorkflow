using System.Threading.Tasks;

namespace WorkflowApp.Interfaces
{
    public interface IRunner
    {
        int QueueLength { get; }

        Task<IWorkItem> Enqueue(IWorkItem workItem);
    }
}