using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace EightGameAI
{
   class Board
   {
      public bool isSolved()
      {
         return solved;
      }

      private void checkSolved()
      {
         if (solved && !stackUnderflow)
         {
            Console.WriteLine("\nOriginal node: {0}\nNumber of generated Nodes: {1} , Solution Sequence Length: {2}"
               , originalNode, numNodes, nodeLevel);
         }
         if (stackUnderflow)
         {
            Console.WriteLine("\nOriginal node: {0}\nUnsolvable", originalNode);
         }
      }

      public void exchange(Node current)
      {
         int zeroPosition = locateZero(current.getSequence);

         bool pushed = false;

         if ((zeroPosition > 2) && (checkExchange(zeroPosition, zeroPosition - 3, current.getSequence) == false))
         {
            pushed = true;
            priorityQ.Push((Node)exchangeValues(zeroPosition, zeroPosition - 3, "U", current));
         }
         if (((zeroPosition + 1) % 3 != 0) && (checkExchange(zeroPosition, zeroPosition + 1, current.getSequence) == false))
         {
            pushed = true;
            priorityQ.Push((Node)exchangeValues(zeroPosition, zeroPosition + 1, "R", current));
         }
         if ((zeroPosition % 3 != 0) && (checkExchange(zeroPosition, zeroPosition - 1, current.getSequence) == false))
         {
            pushed = true;
            priorityQ.Push((Node)exchangeValues(zeroPosition, zeroPosition - 1, "L", current));
         }
         if ((zeroPosition < 6) && (checkExchange(zeroPosition, zeroPosition + 3, current.getSequence) == false))
         {
            pushed = true;
            priorityQ.Push((Node)exchangeValues(zeroPosition, zeroPosition + 3, "D", current));
         }

         if (pushed == false) //No new nodes pushed onto stack
            priorityQ.Pop();
      }

      public Node exchangeValues(int zeroLocation, int newLocation, String direction, Node current)
      {
         Node newNode = new Node();
         newNode.getSequence = (int[])current.getSequence.Clone();  //Clone sequence
         newNode.getName = (String)current.getName.Clone();         //Clone actions

         int tempInt = (int)newNode.getSequence.GetValue(newLocation); //Exchange values
         newNode.getSequence.SetValue(0, newLocation);
         newNode.getSequence.SetValue(tempInt, zeroLocation);

         numNodes++;
         newNode.getName += direction; 
         addBoardSequence(newNode);
         manhattanDistance(newNode); //assign node its manhattan distance
         if (makeKey(newNode) == solutionKey)  //check if solved
         {
            solved = true;
            nodeLevel = newNode.getName.Length;
            solveSequence = newNode.getName;
         }
         return newNode;
      }

      public bool checkExchange(int zeroLocation, int newLocation, int[] arrayValues)
      {
         int[] tempArray = (int[])arrayValues.Clone();
         Node newNode = new Node();
         newNode.getSequence = (int[])tempArray;

         int tempInt = (int)newNode.getSequence.GetValue(newLocation);
         newNode.getSequence.SetValue(0, newLocation);
         newNode.getSequence.SetValue(tempInt, zeroLocation);

         return alreadyVisited(newNode); 
      }

      private int makeKey(Node newNode)
      {
         int key = 0;
         for (int i = 0; i < 9; i++)
         {
            key += ((int)newNode.getSequence.GetValue(i) * (int)(100000000 / (Math.Pow(10, i))));
         }
         return key;
      }

      public void manhattanDistance(Node current)  //set current nodes manhattan distance
      {
         int count = 0;
         for (int i = 0; i < 3; i++) //create 2d array of current nodes sequence
         {
            for (int z = 0; z < 3; z++)  
            {
               matrix[i, z] = current.getSequence.ElementAt(count);
               count++;
            }
         }

         for (int i = 0; i < 3; i++)
         {
            for (int z = 0; z < 3; z++)
            {
               int val = (int)matrix.GetValue(i, z);   //loop through 2d matrix, determine x and y position
                                                       //calculate their taxicab distance from their desired position
               if (val == 0)                           
                  current.getManhattanDistance += (Math.Abs(z - 0) + Math.Abs(i - 0));
               else if (val == 1)
                  current.getManhattanDistance += (Math.Abs(z - 1) + Math.Abs(i - 0));
               else if (val == 2)
                  current.getManhattanDistance += (Math.Abs(z - 2) + Math.Abs(i - 0));
               else if (val == 3)
                  current.getManhattanDistance += (Math.Abs(z - 0) + Math.Abs(i - 1));
               else if (val == 4)
                  current.getManhattanDistance += (Math.Abs(z - 1) + Math.Abs(i - 1));
               else if (val == 5)
                  current.getManhattanDistance += (Math.Abs(z - 2) + Math.Abs(i - 1));
               else if (val == 6)
                  current.getManhattanDistance += (Math.Abs(z - 0) + Math.Abs(i - 2));
               else if (val == 7)
                  current.getManhattanDistance += (Math.Abs(z - 1) + Math.Abs(i - 2));
               else
                  current.getManhattanDistance += (Math.Abs(z - 2) + Math.Abs(i - 2));
            }
         }
      }

      public bool alreadyVisited(Node newNode)
      {
         int key = makeKey(newNode);
         if (knownNodes.Contains(key)) return true;
         else
            return false;
      }

      public void addBoardSequence(Node newNode)
      {
         int key = makeKey(newNode);
         knownNodes.Add(key);
      }

      public int locateZero(int[] x)
      {
         int z = 0;
         for (int i = 0; i < x.Length; i++)
         {
            if (x.ElementAt(i) == 0)
               z = i;
         }
         return z;
      }

      public void begin(int[] x, String currentString)
      {
         Node root = new Node();
         root.getSequence = x;
         priorityQ.Push(root);
         addBoardSequence(root);

         while (!solved)
         {
            exchange((Node)priorityQ.Peek());
            if (priorityQ.Count == 0)
            { stackUnderflow = true; solved = true; }
         }
         checkSolved();
         reset();
      }

      private void reset()
      {
         solved = false;
         stackUnderflow = false;
         knownNodes.Clear();
         priorityQ.Clear();
         numNodes = 0;
      }

      #region fields
      private String solveSequence, originalNode;
      private static IPriorityQueue priorityQ = new BinaryPriorityQueue();
      private static int solutionKey = 012345678;
      private int[] solution = { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
      private int[,] matrix = new int[3, 3];
      private static bool solved;
      private static int numNodes, nodeLevel;
      private static HashSet<int> knownNodes = new HashSet<int>();
      private bool stackUnderflow;
      #endregion
   }
}
