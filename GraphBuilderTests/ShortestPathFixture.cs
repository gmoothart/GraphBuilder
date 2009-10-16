using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gallio.Framework;
using GraphBuilder;
using MbUnit.Framework;
using GraphAlgorithms;
using MbUnit.Framework.ContractVerifiers;

namespace GraphBuilderTests
{
  [TestFixture]
  public class ShortestPathFixture
  {
    private Node<String> graph;

    [SetUp]
    public void Setup()
    {
      graph = Graph.Node("s", new[] {10, 5},
            Graph.Node("t", new[] {1},
              Graph.Node("x")),
            Graph.Node("y", new[] {2},
              Graph.Node("z")));

      var s = graph;
      var t = s["t"];
      var y = s["y"];
      var x = t["x"];
      var z = y["z"];
      
      //add internal edges
      z.Add(s, 7);
      t.Add( y, 2 );
      y.Add( t, 3 );

      y.Add(x, 9);

      z.Add(x, 6);
      x.Add(z, 4);
    }

    [Test]
    [ExpectedArgumentException]
    public void Dijkstra_ThrowsOnNegativeEdge()
    {
      var s = Graph.Node("s", new[] {2},
                Graph.Node("x", new[] {5, 10},
                  Graph.Node("u", new[] {2},
                    Graph.Node("w")),
                  Graph.Node("y")));
      var y = s["x"]["y"];
      var u = s["x"]["u"];
      var w = u["w"];

      y.Add(u, -7);

      s.DijkstraSP(w);
    }

    [Test]
    public void Dijkstra_SingleNode()
    {
      var g = new Node<string>("a");

      var res = g.DijkstraSP();
      Assert.AreEqual(0, res[g].Distance);
      Assert.AreEqual(null, res[g].Predecessor);
    }

    [Test]
    public void Dijkstra_AllPaths_HasCorrectDistance()
    {
      var res = graph.DijkstraSP();
      
      Assert.AreEqual(9, res[ graph["t"]["x"] ].Distance);
      Assert.AreEqual(7, res[ graph["y"]["z"] ].Distance);
    }

    [Test]
    public void Dijkstra_SingleDest()
    {
      var s = graph;
      var x = graph["t"]["x"];
      var z = graph["y"]["z"];

      Assert.AreElementsEqual(
        new[] {s, s["y"], s["t"], s["t"]["x"]},
        s.DijkstraSP(x));

      Assert.AreElementsEqual(
        new[] {s, s["y"], s["y"]["z"]},
        s.DijkstraSP(z));
    }
  }
}
