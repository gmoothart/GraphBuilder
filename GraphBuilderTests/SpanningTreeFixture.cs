using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gallio.Framework;
using GraphBuilder;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;
using GraphAlgorithms;

namespace GraphBuilderTests
{
  [TestFixture]
  public class SpanningTreeFixture
  {
    [Test]
    public void Prim_OnSingleVertex()
    {
      var s = Graph.Node("s");

      var results = s.PrimSpanningTree();
      Assert.AreEqual(0, results[s].MinWeight);
      Assert.AreEqual(null, results[s].Parent);
    }

    [Test]
    public void Prim_TestGraph()
    {
      var graph = Graph.Node("a",
                    Graph.Node("b"));
      
    }
  }
}
