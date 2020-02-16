using System;
using WorkflowApp.Implementations;

namespace WorkflowApp.Interfaces
{
    public interface IRunnerManager
    {
        void RegisterSource(Guid sourceId, IHandler handler);

        void Enqueue(Guid sourceId, ExecutionTask executetionTask);
    }
}