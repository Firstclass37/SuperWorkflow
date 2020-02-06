using System;

namespace WorkflowApp.Interfaces
{
    internal interface IRunnerManager
    {
        void RegisterSource(Guid sourceId, IHandler handler);

        void Enqueue(Guid sourceId, IWorkItem item);
    }
}