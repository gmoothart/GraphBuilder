using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphBuilder
{
  public class Graph
  {

    /// <summary>
    /// Create a Node object, with children. This is primarily useful because
    /// this method will do type inference, allowing you to save on some typing
    /// and even use anonymous types.
    /// </summary>
    public static Node<T> Node<T>(T data, params Node<T>[] children)
    {
      Node<T> n = new Node<T>(data, children);

      return n;
    }

    /// <summary>
    /// Allows adding weights to the edges of a graph. The number of weights and
    /// children must be equal. The first weight will be assigned to the first
    /// child, etc.
    /// </summary>
    public static Node<T> Node<T>(T data, int[] weights, params Node<T>[] children)
    {
      Node<T> n = new Node<T>(data, weights, children);

      return n;
    }
  }
}
