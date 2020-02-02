namespace ConsoleApp12.Interfaces
{
    public interface IWorkflow
    {
        void Start();

        void Add(IWorkItem workitem);
    }
}