using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/**
 * A-star AI for 8 game puzzle
 * Peter Cappello
 * CECS 451
 * */
namespace EightGameAI
{
    class Program
    {
        static void Main(string[] args)
        {
           Board start = new Board();
           FileRead file = new FileRead();
           DateTime now = DateTime.Now;
           Console.WriteLine("Program start");
           file.createSequences();
           int count = 0;
           foreach (int[] arr in file.getArraySequence)
           {
              start.begin(arr, file.getArrayStrings.ElementAt(count));
              count++;
           }

           DateTime finished = DateTime.Now;
           TimeSpan duration = (finished - now);
           Console.WriteLine("\nProgram finished in " + duration.Milliseconds + "ms\n");
        }
    }
}
