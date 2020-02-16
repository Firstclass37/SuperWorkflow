using System;
using WorkflowApp.Implementations;

namespace WorkflowApp.Interfaces
{
    public interface IHandler
    {
        void OnWorkCompleted(ExecutionResult executionResult);

        void OnException(IWorkItem item, Exception e);
    }
}