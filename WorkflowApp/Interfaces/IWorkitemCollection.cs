namespace WorkflowApp.Interfaces
{
    public interface IWorkitemCollection
    {
        IWorkItem GetFirst();

        IWorkItem[] GetNext(IWorkItem item, bool waitPrev);

        void Attach(IWorkItem item, IWorkItem[] itemForAttach);
    }
}