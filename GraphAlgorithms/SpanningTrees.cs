using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GraphBuilder;

namespace GraphAlgorithms
{
  public static class SpanningTrees
  {
    public class SpanningInfo<T>
    {
      public int MinWeight { get; set; }
      public Node<T> Parent { get; set; }
      public bool IsInQueue { get; set; }
    }

    public static Dictionary<Node<T>, SpanningInfo<T>> PrimSpanningTree<T>(this Node<T> graph)
    {
      //
      // initialize priority queue
      //
      var spInfo = new Dictionary<Node<T>, SpanningInfo<T>>();
      var Q = new PriorityQueue<Node<T>, int>(PriorityType.Min);
      foreach(var node in graph.DepthFirst()) {
        var ni = new SpanningInfo<T> { Parent = null, 
                                       MinWeight = int.MaxValue,
                                       IsInQueue = true};

        spInfo[node] = ni;
        Q.Enqueue(node, ni.MinWeight);
      }
      spInfo[graph].MinWeight = 0;

      //
      // Do it to it, Rockapella
      //
      while(Q.Count > 0) {
        Node<T> n = Q.Dequeue();
        SpanningInfo<T> nInfo = spInfo[n];

        //mark as processed
        nInfo.IsInQueue = false;

        foreach(var child in n.Adjacent) {
          SpanningInfo<T> cInfo = spInfo[ child ];
          if (cInfo.IsInQueue && n.Weight(child) < cInfo.MinWeight) {
            cInfo.Parent = n;
            cInfo.MinWeight = n.Weight(child);
            Q.Update(child, cInfo.MinWeight);
          }
        }
      } //while

      return spInfo;
    }
  }
}
