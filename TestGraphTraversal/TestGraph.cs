using System.Collections.Generic;
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
            graph.Traverse(Breadth).Should().Equal(new List<string> {"Nestle", "Ghirardelli", "Hershey", "Cadbury"});
        }

        [TestMethod]
        public void BreadthFirstWithCycles()
        {
            var graph = new Graph<string>(CreateConnectedGraphWithCycles());
            graph.Traverse(Breadth).Should().Equal(new List<string> { "Provo", "Springville", "Mapleton", "Lehi", "Highland" });            
        }

        [TestMethod]
        public void DepthFirstNoCycles()
        {
            var graph = new Graph<string>(CreateConnectedGraphNoCycles());
            graph.Traverse(Depth).Should().Equal(new List<string> { "Nestle", "Hershey", "Ghirardelli", "Cadbury" });
        }

        [TestMethod]
        public void DepthFirstWithCycles()
        {
            var graph = new Graph<string>(CreateConnectedGraphWithCycles());
            graph.Traverse(Depth).Should().Equal(new List<string> { "Provo", "Highland", "Lehi", "Mapleton", "Springville"});
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
