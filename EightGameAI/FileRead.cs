using System;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using System.Resources;
using System.IO;
using System.Linq;
using System.Text;

namespace EightGameAI
{
   public class FileRead
   {
      public void createSequences()
      {
         Assembly myAssembly = Assembly.GetExecutingAssembly();

         using (StreamReader sr = new StreamReader((myAssembly.GetManifestResourceStream("EightGameAI.npuzzles.txt"))))
         {
            String input;
            while ((input = sr.ReadLine()) != null)
            {
               input = input.Trim();
               if (!input.StartsWith("#") && input != "")
               {
                  arrayStrings.Add(input);
               }
            }

            foreach (String currentString in arrayStrings)
            {
               String character = "";
               int currentVal;
               int[] newArray = new int[currentString.Length];

               for (int i = 0; i < currentString.Length; i++)
               {
                  character = currentString.Substring(i, 1);
                  currentVal = Convert.ToInt32(character);
                  newArray[i] = currentVal;
               }
               arraySequences.Add(newArray);
            }
         }
      }

      public List<int[]> getArraySequence
      {
         get { return arraySequences; }
      }

      public List<String> getArrayStrings
      {
         get { return arrayStrings; }
      }

      private List<String> arrayStrings = new List<String>();
      private List<int[]> arraySequences = new List<int[]>();
   }
}
