using System;
using System.Linq;

namespace GraphTraversal
{
    public class GraphTraversal : IGraphTraversal
    {
        public string Traverse()
        {
            var graph = CreateGraph();
            var results = graph.Traverse(Graph<string>.Method.BreadthFirst);
            return string.Join(",", results.ToArray());
        }

        private static Graph<string> CreateGraph()
        {
            var provo = CreateNode("Provo");
            var springville = CreateNode("Springville");
            var mapleton = CreateNode("Mapleton");
            var lehi = CreateNode("Lehi");
            var highland = CreateNode("Highland");
            provo.AddNeighbor(springville);
            provo.AddNeighbor(mapleton);
            provo.AddNeighbor(lehi);
            provo.AddNeighbor(highland);
            springville.AddNeighbor(mapleton);
            highland.AddNeighbor(lehi);
            return new Graph<string>(provo);
        }

        private static Node<string> CreateNode(string contents)
        {
            return new Node<string>(contents);
        }
    }
}
