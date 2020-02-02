using ConsoleApp12.Interfaces;
using ConsoleApp12.Structure;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ConsoleApp12.Implementations
{
    internal class Workflow: IWorkflow
    {
        private readonly Runner _taskRunner;
        private readonly List<Node> _currentNodes;
        private Timer _timer;

        public Workflow(Runner taskRunner, Node startNode)
        {
            _taskRunner = taskRunner;
            _currentNodes = new List<Node>();
            _currentNodes.Add(startNode);
        }

        public void Start()
        {
            if (_timer == null)
            {
                Execute(_currentNodes.First().Value.WorkItem);
                _timer = new Timer(s => Run(), null, 0, 1);
            }
        }

        public bool Completed()
        {
            return !_currentNodes.Any();
        }

        private void Run()
        {
            if (Completed())
            {
                _timer.Dispose();
                return;
            }

            foreach(var node in _currentNodes.ToArray())
            {
                if (_taskRunner.Competed(node.Value.WorkItem) || !node.Value.Required)
                {
                    _currentNodes.Remove(node);
                    _currentNodes.AddRange(node.Next);
                    Execute(node.Next.Select(n => n.Value.WorkItem).ToArray());
                }
            }
        }

        private void Execute(params IWorkItem[] workItems)
        {
            foreach (var item in workItems)
                _taskRunner.Enqueue(item);
        }
    }
}