using WorkflowApp.Implementations;
using System.Collections.Generic;

namespace WorkflowApp.Structure
{
    internal class Node
    {
        private readonly List<Node> _next = new List<Node>();

        public Node(WorkitemWrapper value)
        {
            Value = value;
        }

        public WorkitemWrapper Value { get; }

        public IReadOnlyCollection<Node> Next => _next;

        public void AddNext(WorkitemWrapper workitemWrapper)
        {
            _next.Add(new Node(workitemWrapper));
        }
    }
}