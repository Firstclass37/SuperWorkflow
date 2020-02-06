using System;
using WorkflowApp.Interfaces;

namespace WorkflowApp.Implementations
{
    internal class Workflow: IWorkflow, IHandler
    {
        private readonly IRunnerManager _runnerManager;
        private readonly IWorkitemCollection _workitemCollection;

        public Workflow(IRunnerManager runnerManager, IWorkitemCollection workitemCollection)
        {
            runnerManager.RegisterSource(Id, this);
            _runnerManager = runnerManager;
            _workitemCollection = workitemCollection;
        }

        public Guid Id => Guid.Parse("1AC7FC01-7869-489C-973B-1DE6C3FE9DF1");

        public void Start()
        {
            _runnerManager.Enqueue(Id, _workitemCollection.GetFirst());
        }
      
        public void OnWorkCompleted(IWorkItem item)
        {
            Execute(_workitemCollection.GetNext(item, true));
            Execute(_workitemCollection.GetNext(item, false));
        }

        public void OnException(IWorkItem item, Exception e)
        {
            throw new NotImplementedException();
        }

        private void Execute(params IWorkItem[] workItems)
        {
            foreach (var item in workItems)
                _runnerManager.Enqueue(Id, item);
        }
    }
}