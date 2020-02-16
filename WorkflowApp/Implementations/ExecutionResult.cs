using WorkflowApp.Interfaces;

namespace WorkflowApp.Implementations
{
    public class ExecutionResult
    {
        public ExecutionResult(IWorkItem workItem, object result)
        {
            WorkItem = workItem;
            Result = result;
        }

        public IWorkItem WorkItem { get; }

        public object Result { get; }
    }
}
