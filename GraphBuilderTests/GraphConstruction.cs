using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GraphBuilder;
using MbUnit;
using MbUnit.Framework;

namespace GraphBuilderTests
{
  [TestFixture]
  public class GraphConstruction
  {
    [Test]
    public void Builder_CanCreateCycles()
    {
      var g1 = Graph.Node("a",
                 Graph.Node("b",
                   Graph.Node("c")),
                 Graph.Node("d", 
                   Graph.Node("e"),
                   Graph.Node("f")));

      var g2 = Graph.Node("x",
                 Graph.Node("y",
                   g1),
                 Graph.Node("w",
                   g1));


      Assert.AreEqual(g1.Data, g2[0][0].Data);
      Assert.AreEqual(g2[1][0].Data, g2[0][0].Data);
    }

    [Test]
    public void Builder_WeightedGraph()
    {
      var g1 = Graph.Node("a", new[] {5, 1},
                 Graph.Node("b",
                   Graph.Node("c")),
                 Graph.Node("d", new[] {1, 15},
                   Graph.Node("e"),
                   Graph.Node("f")));
      
      Assert.AreEqual(5.0, g1.Weight(0));
      Assert.AreEqual(5.0, g1.Weight(g1[0]));

      Assert.AreEqual(1, g1.Weight(1));

      Assert.AreEqual(0.5, g1[1].Weight(1));
      Assert.AreEqual(0.0, g1[0].Weight(0));

    }
  }
}
