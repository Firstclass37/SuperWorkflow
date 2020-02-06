using System;

namespace WorkflowApp.Interfaces
{
    public interface IHandler
    {
        void OnWorkCompleted(IWorkItem item);

        void OnException(IWorkItem item, Exception e);
    }
}