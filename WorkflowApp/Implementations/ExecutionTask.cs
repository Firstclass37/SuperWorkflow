using WorkflowApp.Interfaces;

namespace WorkflowApp.Implementations
{
    public class ExecutionTask
    {

        public ExecutionTask(IWorkItem item): this (item, null)
        {
        }

        public ExecutionTask(IWorkItem item, ExecutionResult previousItemResult)
        {
            WorkItem = item;
            PreviousItemResult = previousItemResult;
        }

        public IWorkItem WorkItem { get; set; }

        public ExecutionResult PreviousItemResult { get; set; }
    }
}