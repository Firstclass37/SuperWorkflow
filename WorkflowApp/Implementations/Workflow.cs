using System;
using System.Linq;
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
            _runnerManager.Enqueue(Id, new ExecutionTask(_workitemCollection.GetFirst()));
        }
      
        public void OnWorkCompleted(ExecutionResult executionResult)
        {
            Execute(_workitemCollection.GetNext(executionResult.WorkItem, true).Select(i => new ExecutionTask(i, executionResult)).ToArray());
            Execute(_workitemCollection.GetNext(executionResult.WorkItem, false).Select(i => new ExecutionTask(i, executionResult)).ToArray());
        }

        public void OnException(IWorkItem item, Exception e)
        {
            throw new NotImplementedException();
        }

        private void Execute(params ExecutionTask[] executetionTasks)
        {
            foreach (var task in executetionTasks)
                _runnerManager.Enqueue(Id, task);
        }
    }
}