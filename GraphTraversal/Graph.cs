using System.Collections.Generic;
using System.Linq;

namespace GraphTraversal
{
    public class Graph<T>
    {
        public enum Method { BreadthFirst, DepthFirst }

        private readonly Node<T> root;
        private IUnvisitedNodes<T> unvisited;
        private List<T> results;

        public Graph(Node<T> root)
        {
            this.root = root;
        }

        public IEnumerable<T> Traverse(Method method)
        {
            BeginTraversal(method);
            AddUnvisitedNode(root);
            while (AnyUnvisitedNodes())
            {
                var n = GetUnvisitedNode();
                Visit(n);
                AddUnvisitedNeighbors(n);
            }
            return EndTraversal();
        }

        private void BeginTraversal(Method method)
        {
            results = new List<T>();
            unvisited = CreateUnvisitedNodes(method);
        }

        private static IUnvisitedNodes<T> CreateUnvisitedNodes(Method method)
        {
            if (method == Method.DepthFirst)
            {
                return new UnvisitedNodesDepthFirst<T>();
            }
            return new UnvisitedNodesBreadthFirst<T>();
        }

        private void AddUnvisitedNode(Node<T> startingNode)
        {
            unvisited.Add(startingNode);
        }

        private bool AnyUnvisitedNodes()
        {
            return unvisited.HasElements();
        }

        private Node<T> GetUnvisitedNode()
        {
            return unvisited.Get();
        }

        private void Visit(Node<T> n)
        {
            results.Add(n.Contents);
            n.Visited = true;
        }

        private void AddUnvisitedNeighbors(Node<T> n)
        {
            foreach (var node in n.Neighbors.Where(x => x.Visited == false))
            {
                AddUnvisitedNode(node);
            }
        }

        private IEnumerable<T> EndTraversal()
        {
            return results;
        }

    }
}