using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace GraphBuilder
{
  public class Node<T>
  {
    private List<Node<T>> _adj = new List<Node<T>>();
    public List<Node<T>> Adjacent { get { return _adj; } }

    /// <summary>
    /// Weights for the edges leaving this node
    /// </summary>
    private List<int> _weights = new List<int>();

    public Node(T data)
    {
      Data = data;
    }

    public Node(T data, params Node<T>[] children): this(data)
    {
      foreach(var c in children) {
        Add(c);

        //if graph is undirected, add to c as well
        //c.Add(this);
      }
    }

    public Node(T data, int[] weights, params Node<T>[] children)
    {
      Debug.Assert(weights.Length == children.Length);
      Data = data;

      for(int i=0; i<children.Length; i++) {
        Add(children[i], weights[i]);

        //if graph is undirected, add to c as well
        //c.Add(this, weights[i]);
      }
    }

    public void Add(Node<T> node)
    {
      _adj.Add(node);
    }

    public void Add(Node<T> node, int weight)
    {
      int pos = _adj.Count;

      _adj.Insert(pos, node);
      _weights.Insert(pos, weight);
    }

    public int Weight(int index)
    {
      if (index >= 0 && index < _weights.Count) {
        return _weights[index];
      }
      return 0;
    }

    public int Weight(Node<T> child)
    {
      return Weight(_adj.IndexOf(child));
    }

    public T Data { get; set; }

    public Node<T> this[int index]
    {
      get { return _adj[index]; }
    }

    public Node<T> this[T data]
    {
      // this is slow, but convenient. I expect the adjacency list to be small
      get { return _adj.Find(n => n.Data.Equals(data)); }
    }
  }
}
