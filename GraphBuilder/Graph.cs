using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphBuilder
{

  /// <summary>
  /// A (possibly disconnected) graph object
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class Graph<T>
  {
    public bool Directed { get; set; }

    private HashSet<Node<T>> _nodes;

    public Graph(bool directed, params Node<T>[] nodes)
    {
      Directed = directed;
       
      foreach (var node in nodes) {
        Add(node);
      }
    }

    public Graph(params Node<T>[] nodes) : this(true, nodes) { }

    public void Add(Node<T> n)
    {
      _nodes.Add(n);

      foreach(var child in n.BreadthFirst()) {
        _nodes.Add(child);
      }
    }

    public Node<T> this[int index]
    {
      get { _nodes. }
    }
  }
}
