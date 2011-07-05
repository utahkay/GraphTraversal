using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestAcceptanceGraphTraversal.GraphTraversal;

namespace TestAcceptanceGraphTraversal
{
    [TestClass]
    public class TestTraversal
    {
        [TestMethod]
        public void Traverse()
        {
            var graph = new GraphTraversalClient();
            Assert.AreEqual("Provo,Springville,Mapleton,Lehi,Highland", graph.Traverse());
        }
    }
}
