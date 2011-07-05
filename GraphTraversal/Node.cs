using System.Collections.Generic;

namespace GraphTraversal
{
    public class Node<T>
    {
        private readonly Dictionary<T, Node<T>> neighbors = new Dictionary<T, Node<T>>();

        public Node(T nodeData)
        {
            Contents = nodeData;
        }

        public T Contents { get; set; }

        public bool Visited { get; set; }

        public IEnumerable<Node<T>> Neighbors
        {
            get { return neighbors.Values; }
        }

        public void AddNeighbor(Node<T> neighbor)
        {
            AddUnidirectional(neighbor);
            neighbor.AddUnidirectional(this);
        }

        public void RemoveNeighbor(Node<T> neighbor)
        {
            RemoveUnidirectional(neighbor);
            neighbor.RemoveUnidirectional(this);
        }

        private void AddUnidirectional(Node<T> neighbor)
        {
            neighbors.Add(neighbor.Contents, neighbor);
        }

        private void RemoveUnidirectional(Node<T> neighbor)
        {
            neighbors.Remove(neighbor.Contents);
        }

    }
}