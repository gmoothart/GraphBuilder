using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphBuilder
{
  // TODO: Do we need this class?
  public class Edge<T>
  {
    public Node<T> In { get; set; } 
    public Node<T> Out { get; set; }

    public decimal Weight { get; set; }

    public Edge(Node<T> aOut, Node<T> aIn)
    {
      In = aIn;
      Out = aOut;
    }
  }
}
