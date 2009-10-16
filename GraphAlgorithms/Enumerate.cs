using System.Collections.Generic;
using GraphBuilder;

namespace GraphAlgorithms
{
  public static class Enumerate
  {
    /// <summary>
    /// Enumerate a graph breadth-first
    /// </summary>
    /// <remarks>
    /// We use a stack rather than recursion. recursive iterators, which would be
    /// required here, have some undesirable properties. See here for more
    /// details:
    /// http://blogs.msdn.com/wesdyer/archive/2007/03/23/all-about-iterators.aspx
    /// </remarks>
    /// <typeparam name="T"></typeparam>
    /// <param name="n"></param>
    /// <returns></returns>
    public static IEnumerable<Node<T>> BreadthFirst<T>(this Node<T> n)
    {
      if (n == null) yield break;

      var q = new Queue<Node<T>>();
      var marked = new Dictionary<Node<T>, bool>();

      q.Enqueue(n);
      while (q.Count > 0) {
        var node = q.Dequeue();
        if(marked.ContainsKey(node)) continue;

        yield return node;
        marked[node] = true;

        foreach(var child in node.Adjacent) {
          q.Enqueue(child);
        }
      }
    }

    public static IEnumerable<Node<T>> DepthFirst<T>(this Node<T> n)
    {
      if (n == null) yield break;

      var marked = new Dictionary<Node<T>, bool>();
      var stack = new Stack<Node<T>>();

      stack.Push(n);
      while(stack.Count > 0) {
        var node = stack.Pop();
        if (marked.ContainsKey(node)) continue;

        yield return node;
        marked[node] = true;

        // push nodes in reverse order so that they are enumerated in the 
        //  order they were created in
        for (int i = node.Adjacent.Count-1; i >= 0; i-- ) {
          stack.Push( node.Adjacent[i] );
        }
      }
    }
  }
}
