using ConsoleApp12.Interfaces;
using System.Linq;

namespace ConsoleApp12.Structure
{
    internal static class NodeExtentions
    {
        public static Node SearchNode(this Node node, IWorkItem item)
        {
            if (!node.Next.Any())
                return null;

            if (node.Value == item)
                return node;

            foreach (var n in node.Next)
            {
                var found = SearchNode(n, item);
                if (found != null)
                    return found;
            }
            return null;
        }
    }
}