using System;
using System.Collections.Generic;
using System.Diagnostics;
using GraphBuilder;
using System.Linq;

namespace GraphAlgorithms
{
  public static class ShortestPaths
  {
    /// <summary>
    /// For Dijkstra's algorithm, we need to be able to associate meta-data
    /// with a node, retrievable in O(1) time.
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SPInfo<T>
    {
      /// <summary>
      /// Predecessor of this node in the shortest path
      /// </summary>
      public Node<T> Predecessor { get; set; }

      /// <summary>
      /// Shortest-path distance to this node from the source
      /// </summary>
      public int Distance { get; set; }
    }


    /// <remarks>
    /// In the future I should switch to a queue with better Update()
    /// performance. This gets the job done, but Update() is O(n log n)
    /// </remarks>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <returns>
    /// A dictionary mapping each node to its Shortest-Path info, including its
    /// predecessor in the shortest path and the distance to it.
    /// </returns>
    public static Dictionary<Node<T>, SPInfo<T>> DijkstraSP<T>(this Node<T> source)
    {
      //
      // initialize the priority queue and Nodes hash. Each node has an initial
      // weight of infinity, except the source
      //
      var spInfo = new Dictionary<Node<T>, SPInfo<T>>();
      var Q = new PriorityQueue<Node<T>, int>(PriorityType.Min);
      foreach(var node in source.DepthFirst()) {
        var ni = new SPInfo<T> {Predecessor = null, Distance = int.MaxValue};

        spInfo[node] = ni;
        Q.Enqueue(node, ni.Distance);
      }
      spInfo[source].Distance = 0;


      //Ready, steady, go
      while(Q.Count > 0) {
        Node<T> n = Q.Dequeue();
        SPInfo<T> nInfo = spInfo[n];

        for (int i = 0; i < n.Adjacent.Count; i++ ) {
          Node<T> child = n.Adjacent[i];
          SPInfo<T> cInfo = spInfo[ child ];
          int childWeight = n.Weight(i);
          if (childWeight < 0)
            throw new ArgumentException("You can't use negative weights with Dijkstra's algorithm!");
          int altDistance = nInfo.Distance + childWeight;

          //relaxation step
          if (altDistance < cInfo.Distance) {
            cInfo.Distance = altDistance;
            cInfo.Predecessor = n;
            Q.Update( child, cInfo.Distance );
          }
        }
      }

      return spInfo;
    }

    /// <summary>
    /// Returns an enumeration of the shortest path from source to dest
    /// </summary>
    public static IEnumerable<Node<T>> DijkstraSP<T>(this Node<T> source, Node<T> dest)
    {
      var spInfo = DijkstraSP(source);
      var destInfo = spInfo[dest];

      // no path
      if (destInfo.Distance == int.MaxValue) {
        yield break;
      }

      // "rewind" the path, so we can report it in forward-order
      var stack = new Stack<Node<T>>();
      stack.Push(dest);

      while(destInfo.Predecessor != null) {
        stack.Push(destInfo.Predecessor);
        destInfo = spInfo[destInfo.Predecessor];
      }

      // sanity check. The first node in our path should be the source
      Debug.Assert(source == stack.Peek());

      // yield it back out to the client
      while(stack.Count > 0) {
        yield return stack.Pop();
      }
    }
  }
}
