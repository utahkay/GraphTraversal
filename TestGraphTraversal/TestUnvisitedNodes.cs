using FluentAssertions;
using GraphTraversal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestGraphTraversal
{
    [TestClass]
    public class TestUnvisitedNodes
    {
        private readonly IUnvisitedNodes<string> breadthFirst = new UnvisitedNodesBreadthFirst<string>();
        private readonly IUnvisitedNodes<string> depthFirst = new UnvisitedNodesDepthFirst<string>();
        private readonly Node<string> node1 = new Node<string>("One");
        private readonly Node<string> node2 = new Node<string>("Two");
        private readonly Node<string> node3 = new Node<string>("Three");

        [TestInitialize]
        public void Setup()
        {
            breadthFirst.Add(node1);
            breadthFirst.Add(node2);
            depthFirst.Add(node1);            
            depthFirst.Add(node2);            
        }

        [TestMethod]
        public void BreadthFirst()
        {
            breadthFirst.Get().Should().Be(node1);
            breadthFirst.Get().Should().Be(node2);
            breadthFirst.HasElements().Should().BeFalse();
        }

        [TestMethod]
        public void BreadthFirstDoesntCountVisited()
        {
            node1.Visited = true;
            node2.Visited = true;
            breadthFirst.HasElements().Should().BeFalse();
        }

        [TestMethod]
        public void BreadthFirstRemovesVisited()
        {
            node1.Visited = true;
            breadthFirst.Get().Should().Be(node2);
            breadthFirst.HasElements().Should().BeFalse();
        }

        [TestMethod]
        public void DepthFirst()
        {
            depthFirst.Get().Should().Be(node2);
            depthFirst.Get().Should().Be(node1);
            depthFirst.HasElements().Should().BeFalse();
        }

        [TestMethod]
        public void DepthFirstRemovesDuplicates()
        {
            depthFirst.Add(node3);
            depthFirst.Add(node1);
            depthFirst.Get().Should().Be(node1);
            depthFirst.Get().Should().Be(node3);
            depthFirst.Get().Should().Be(node2);
            depthFirst.HasElements().Should().BeFalse();
        }
    }
}
