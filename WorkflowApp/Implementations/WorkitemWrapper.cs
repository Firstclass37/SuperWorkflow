using WorkflowApp.Interfaces;

namespace WorkflowApp.Implementations
{
    internal class WorkitemWrapper
    {
        public WorkitemWrapper(IWorkItem workItem, bool waitPrev)
        {
            WaitPrev = waitPrev;
            WorkItem = workItem;
        }

        public IWorkItem WorkItem { get; }

        public bool WaitPrev { get; }

        public bool Started { get; set; }

        public bool Completed { get; set; }
    }
}