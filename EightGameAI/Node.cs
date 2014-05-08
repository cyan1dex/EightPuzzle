using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EightGameAI
{
   public class Node : IComparable
   {
      public int CompareTo(object obj) //for pushing nodes onto priority queue
      {
         Node temp = (Node)obj;  //Manhattan Distance, h(n)  getName.Length returns the depth of the node, g(n)
         if ((this.manhattanDistance + this.getName.Length) > (temp.manhattanDistance + temp.getName.Length))
             return 1;
         else if ((this.manhattanDistance + this.getName.Length) < (temp.manhattanDistance + temp.getName.Length))
             return -1;
         //if ((this.manhattanDistance ) > (temp.manhattanDistance ))
         //    return 1;
         //if ((this.manhattanDistance ) < (temp.manhattanDistance ))
         //    return -1;
         else
         return 0;
      }

      public String getName
      {
         get { return name; }
         set { name = value; }
      }

      public int[] getSequence
      {
         get { return sequence; }
         set { sequence = value; }
      }

      public int getManhattanDistance
      {
          get { return manhattanDistance; }
          set { manhattanDistance = value; }
      }

      public Node()
      {
         sequence = new int[9];   //Current state in state space
         manhattanDistance = 0;   //Path Cost
         name = "";               //Action performed by parent to create node
      }

      public Node(Node x)
      {
         sequence = x.sequence;
         name = x.name;
         manhattanDistance = 0;
      }

      public Node(int[] x)
      {
         sequence = x;
         manhattanDistance = 0;
      }

      #region fields
      private String name; 
      private int[] sequence;
      private int manhattanDistance;
      #endregion
   }
}
