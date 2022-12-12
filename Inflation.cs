using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Final_Project
{
    public static class Inflation{
        public static string LineChanger(string str, int raise_rate)
        {
            int price = int.Parse(str.Split(':')[1].Trim());
            double tmp = ((double)price) * ( (double) raise_rate / 100 );
            double raise = Math.Round(tmp);
            price += (int) raise;
            return $"{str.Split(':')[0]}: {price}" ;
        }

        public static void inflation(string path)
        {
            Stopwatch watch = new Stopwatch();

            int raise_degree = 0;
            Console.Write("Enter the inflation rate. It should be an int: ");

            while ( !int.TryParse(Console.ReadLine(), out raise_degree))
                Console.Write("Invalid input. Please enter int number: ");

            watch.Start();
            string[] drugFile = FileHandler.DrugFile(path);
            drugFile = drugFile.Select( x => LineChanger(x, raise_degree) ).ToArray();
            
            File.WriteAllLines(path, drugFile);
            Console.WriteLine($"The drugs have been raised {raise_degree} percent");

            watch.Stop();
            Extensions.Task_time_printer(watch.Elapsed.ToString());
       }
    }
}