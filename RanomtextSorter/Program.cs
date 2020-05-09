using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace RanomtextSorter
{
    class Program
    {
        static void Main(string[] args)
        {
            bool running = true;

            while (running)
            {
                //input.
                Console.WriteLine("Geef letter waarop gefilterd moet worden: ('stop' om te stoppen.)");
                string c = Console.ReadLine();

                //Stop de applicatie.
                if(c.Equals("stop") || c.Equals("Stop"))
                {
                    break;
                }

                //Haal bestand op.   ( D:\3.NET projecten\RanomtextSorter\RanomtextSorter\randomtext.txt ) 
                String path = "";
                Console.WriteLine("Selecteer Directory:\n 1) Juiste Path\n 2) Verkeerde Directory\n 3) Verkeerde file");
                string p = Console.ReadLine();

                switch (p)
                {
                    case "1":
                        path = @"D:\3.NET projecten\RanomtextSorter\RanomtextSorter\randomtext.txt";
                        break;
                    case "2":
                        path = @"D:\3.NET projecten\RanomtextSorter\WrongDirectory\randomtext.txt";
                        break;
                    case "3":
                        path = @"D:\3.NET projecten\RanomtextSorter\RanomtextSorter\WrongFile.txt";
                        break;
                }

                Console.WriteLine("\n");

                //filter text.
                String[] words = GetWords(path, c).ToArray();

                String[] wordsFiltered = words.OrderBy(aux => aux.Length).ToArray();

                foreach (String word in wordsFiltered)
                    Console.Write("{0}; ", word);

                Console.WriteLine("\n\n---------------------------------------------------------------------------");
            }
        }

        static IEnumerable<String> GetWords(String path, String c)
        {
            string text = "";

            try
            {
            text = System.IO.File.ReadAllText(path);
            }
            catch(Exception ex)
            {
                if(ex is FileNotFoundException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Er is geen file gevonden op pad: {0}", path);
                    text = "NO_FILE_FOUND";
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (ex is DirectoryNotFoundException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Het aangegeven directory bestaat niet: {0}", path);
                    text = "NO_DIRECTORY_FOUND";
                    Console.ForegroundColor = ConsoleColor.White;
                }

            }

            text = text.Replace(",", "");
            text = text.Replace(".", "");
            text = text.Replace(";", "");
            text = text.ToLower();
            String[] words = text.Split(' ');

            //String[] result = Array.FindAll<String>(words, s => s.StartsWith(c));


            foreach(String word in words)
            {
                if (word.StartsWith(c))
                {                
                    yield return word;
                }
            }

        }
    }
}

// String path = @"D:\3.NET projecten\RanomtextSorter\RanomtextSorter\randomtext.txt";
