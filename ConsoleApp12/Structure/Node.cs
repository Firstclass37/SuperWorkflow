using ConsoleApp12.Implementations;

namespace ConsoleApp12.Structure
{
    internal class Node
    {
        public Node(WorkitemWrapper value, Node[] next)
        {
            Value = value;
            Next = next;
        }

        public WorkitemWrapper Value { get; }

        public Node[] Next { get; }
    }
}