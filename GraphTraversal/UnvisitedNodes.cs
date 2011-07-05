using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphTraversal
{
    public interface IUnvisitedNodes<T>
    {
        void Add(Node<T> node);
        Node<T> Get();
        bool HasElements();
    }

    public class UnvisitedNodesBreadthFirst<T> : IUnvisitedNodes<T>
    {
        private readonly Queue<Node<T>> unvisited = new Queue<Node<T>>();
        public void Add(Node<T> node)
        {
            unvisited.Enqueue(node);
        }

        public Node<T> Get()
        {
            var node = unvisited.Dequeue();
            while (node != null && node.Visited)
            {
                node = unvisited.Dequeue();
            }
            return node;
        }

        public bool HasElements()
        {
            return unvisited.Where(x => !x.Visited).Count() > 0;
        }
    }

    public class UnvisitedNodesDepthFirst<T> : IUnvisitedNodes<T>
    {
        private readonly Stack<Node<T>> unvisited = new Stack<Node<T>>();
        public void Add(Node<T> node)
        {
            if (unvisited.Contains(node))
            {
                RemoveFromStack(node);
            }
            unvisited.Push(node);
        }

        public Node<T> Get()
        {
            return unvisited.Pop();
        }

        public bool HasElements()
        {
            return unvisited.Count > 0;
        }

        private void RemoveFromStack(Node<T> node)
        {
            var array = unvisited.ToArray().Reverse();
            unvisited.Clear();
            foreach (var n in array)
            {
                if (!n.Equals(node))
                    unvisited.Push(n);
            }
        }
    }
}