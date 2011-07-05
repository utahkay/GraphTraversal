using System;
using System.Linq;
using FluentAssertions;
using GraphTraversal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestGraphTraversal
{
    //   Nestle-------------Hershey
    //      |
    //      +-----Ghirardelli
    //                |
    //                +---Cadbury

    [TestClass]
    public class TestNode
    {
        private readonly Node<string> nestle = new Node<string>("Nestle");
        private readonly Node<string> hershey = new Node<string>("Hershey");
        private readonly Node<string> ghirardelli = new Node<string>("Ghirardelli");
        private readonly Node<string> cadbury = new Node<string>("Cadbury");

        [TestInitialize]
        public void SetupNodes()
        {
            nestle.AddNeighbor(ghirardelli);
            nestle.AddNeighbor(hershey);
            ghirardelli.AddNeighbor(cadbury);
        }

        [TestMethod]
        public void NodeContents()
        {
            nestle.Contents.Should().Be("Nestle");
        }

        [TestMethod]
        public void Visited()
        {
            nestle.Visited.Should().BeFalse();
            nestle.Visited = true;
            nestle.Visited.Should().BeTrue();
        }

        [TestMethod]
        public void Neighbors()
        {
            nestle.Neighbors.Should().BeEquivalentTo(hershey, ghirardelli);
        }

        [TestMethod]
        public void SymmetricInOtherWordsIamMyNeighborsNeighbor()
        {
            nestle.Neighbors.Should().Contain(hershey);
            hershey.Neighbors.Should().Contain(nestle);
        }

        [TestMethod]
        public void RemoveNeighbor()
        {
            nestle.RemoveNeighbor(ghirardelli);
            nestle.Neighbors.Should().NotContain(ghirardelli);
            ghirardelli.Neighbors.Should().NotContain(nestle);
        }

        [TestMethod]
        public void NeighborsMustBeUnique()
        {
            Action act = () => nestle.AddNeighbor(hershey); 
            act.ShouldThrow<ArgumentException>().Where(e => e.Message.StartsWith("An item with the same key"));
        }

        [TestMethod]
        public void NeighborsKeepTheirNeighbors()
        {
            var g = nestle.Neighbors.First(x => x.Contents.Equals("Ghirardelli"));
            g.Neighbors.Should().BeEquivalentTo(nestle, cadbury);
        }

    }
}
