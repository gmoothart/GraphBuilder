using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gallio.Framework;
using GraphAlgorithms;
using GraphBuilder;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;

namespace GraphBuilderTests
{
  [TestFixture]
  public class EnumerationFixture
  {
    private Node<string> simple;
    private Node<int> cycles;

    [SetUp]
    public void SetUp()
    {
      simple = Graph.Node("a",
                 Graph.Node("b",
                   Graph.Node("d"),
                   Graph.Node("e",
                     Graph.Node("g")),
                   Graph.Node("f")),
                 Graph.Node("c"));

      var gpart1 = Graph.Node(4);
      var gpart2 = Graph.Node(2,
                     Graph.Node(5,
                       gpart1));
      gpart1.Add(gpart2);
      cycles = Graph.Node(1,
                 gpart1,
                 gpart2,
                 Graph.Node(6));
      
    }

    [Test]
    public void BFS_Simple()
    {
      Assert.AreElementsEqual(
        new[] {"a", "b", "c", "d", "e", "f", "g"}, 
        simple.BreadthFirst().Select(n => n.Data));
    }

    [Test]
    public void BFS_Cycles()
    {
      Assert.AreElementsEqual(
        new[] {1, 4, 2, 6, 5, }, 
        cycles.BreadthFirst().Select(n => n.Data));
      
    }

    [Test]
    public void DFS_Simple()
    {
      Assert.AreElementsEqual(
        new[] {"a", "b", "d", "e", "g", "f", "c"}, 
        simple.DepthFirst().Select(n => n.Data));
    }

    [Test]
    public void DFS_Cycles()
    {
      Assert.AreElementsEqual(
        new[] {1, 4, 2, 5, 6, }, 
        cycles.DepthFirst().Select(n => n.Data));
    }
  }
}
