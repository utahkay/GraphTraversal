using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using GraphTraversal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestGraphTraversal
{
    [TestClass]
    public class TestGraph
    {
        private const Graph<string>.Method Breadth = Graph<string>.Method.BreadthFirst;
        private const Graph<string>.Method Depth = Graph<string>.Method.DepthFirst;

        [TestMethod]
        public void BreadthFirstNoCycles()
        {
            var graph = new Graph<string>(CreateConnectedGraphNoCycles());
            var expected = new List<string> {"Nestle", "Ghirardelli", "Hershey", "Cadbury"};
            CollectionAssert.AreEqual(expected.ToArray(), graph.Traverse(Breadth).ToArray());
        }

        [TestMethod]
        public void BreadthFirstWithCycles()
        {
            var graph = new Graph<string>(CreateConnectedGraphWithCycles());
            var expected = new List<string> {"Provo", "Springville", "Mapleton", "Lehi", "Highland"};
            CollectionAssert.AreEqual(expected.ToArray(), graph.Traverse(Breadth).ToArray());            
        }

        [TestMethod]
        public void DepthFirstNoCycles()
        {
            var graph = new Graph<string>(CreateConnectedGraphNoCycles());
            var expected = new List<string> {"Nestle", "Hershey", "Ghirardelli", "Cadbury"};
            CollectionAssert.AreEqual(expected.ToArray(), graph.Traverse(Depth).ToArray());
        }

        [TestMethod]
        public void DepthFirstWithCycles()
        {
            var graph = new Graph<string>(CreateConnectedGraphWithCycles());
            var expected = new List<string> {"Provo", "Highland", "Lehi", "Mapleton", "Springville"};
            CollectionAssert.AreEqual(expected.ToArray(), graph.Traverse(Depth).ToArray());
        }

        //   Nestle-------------Hershey
        //      |
        //      +-----Ghirardelli
        //                |
        //                +---Cadbury
        private static Node<string> CreateConnectedGraphNoCycles()
        {
            var nestle = CreateNode("Nestle");
            var ghirardelli = CreateNode("Ghirardelli");
            var hershey = CreateNode("Hershey");
            var cadbury = CreateNode("Cadbury");
            nestle.AddNeighbor(ghirardelli);
            nestle.AddNeighbor(hershey);
            ghirardelli.AddNeighbor(cadbury);
            return nestle;
        }

        //    Lehi-----+    +---------Springville         
        //      |       \  /             |
        //      |       Provo            | 
        //      |       /  \             |
        //  Highland---+    +---------Mapleton
        private static Node<string> CreateConnectedGraphWithCycles()
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
            return provo;
        }

        private static Node<string> CreateNode(string contents)
        {
            return new Node<string>(contents);
        }
    }
}
