using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Final_Project
{
    public static class CRD_Disease
    {
        public static void Create(string alg_path, string dis_path,
        string drug_path)
        {

            Stopwatch watch = new Stopwatch();

            string[] diseaseFile = FileHandler.DiseasFile(dis_path);

            Console
                .WriteLine("Please enter the name of the disease, that you want to add to data base");
            Console
                .WriteLine("Hint: It is more formal and standard to represent your disease name by 10 letters; but it works if not");

            Console.Write("enter the name of disease here: ");
            string name = Console.ReadLine();
            
            
            watch.Start();
            
            if (diseaseFile.Contains($"Dis_{name}"))
            {
                watch.Stop();
                Console.WriteLine("This disease is available in the database");
                Extensions.Task_time_printer(watch.Elapsed.ToString());
                return;
            }

            
            string[] alergiesFile = FileHandler.AlergisFile(alg_path);
            string[] drugFile = FileHandler.DrugFile(drug_path);

            System.Console.WriteLine();
            System.Console.Write("Enter Drug with positive effect (you can enter blank if you want to skip): ");
            
            List<string> line = new List<string>(); 
            
            watch.Stop();

            string drug;
            while ((drug = Console.ReadLine()) != null && drug != "")
            {
                line.Add($"(Drug_{drug},+)");
                Console.Write("Good. now enter another if you want. else enter blank: ");
            }
            
            System.Console.WriteLine();
            System.Console.Write("Enter Drug with negative effect (you can enter blank if you want to skip): ");

            string drug0;
            while ((drug0 = Console.ReadLine()) != null && drug0 != "")
            {
                line.Add($"(Drug_{drug0},-)");
                Console.Write("Good. now enter another if you want. else enter blank: ");
            }

            watch.Start();

            List<string> should_deleted = new List<string>();

            foreach (var item in line)
                if (! Extensions.drug_is_in_databasse(item.Substring_between_two_chars('_', ','), drugFile) )
                {
                    should_deleted.Add(item);
                    Console.Write(item.Substring_between_two_chars('_', ',') + " is not in the database");
                }
            
            foreach (var item in should_deleted)
                line.Remove(item);

            
            FileHandler.Add_text(alg_path,
            "Dis_" + name + " : " + String.Join(" ; ", line));

            FileHandler.Add_text(dis_path, $"Dis_{name}");

            Console.WriteLine();
            Console.WriteLine($"The disease {name}, added successfully to the datasets!");

            watch.Stop();
            Extensions.Task_time_printer(watch.Elapsed.ToString());

        }

        public static void Delete(string alg_path, string dis_path)
        {
            Stopwatch watch = new Stopwatch();
        
            System.Console.Write("Ennter the name of the disease that you want to delete from database: ");
            string name = Console.ReadLine();
            string[] diseaseFile = FileHandler.DiseasFile(dis_path);
            
            watch.Start();
            
            if (! Extensions.disease_is_in_databasse(name, diseaseFile))
            {
                Console.WriteLine("There is no disease with such a name in database");
                watch.Stop();
                Extensions.Task_time_printer(watch.Elapsed.ToString());
                return;
            }

            diseaseFile = diseaseFile.Where( x => x != $"Dis_{name}").ToArray();
            File.WriteAllLines(dis_path, diseaseFile);

            string[] alergiesFile = FileHandler.AlergisFile(alg_path);

            alergiesFile = alergiesFile.
            Where(x => x.Substring_between_two_chars('_',' ') != name ).ToArray();
            File.WriteAllLines(alg_path, alergiesFile);

            System.Console.WriteLine($"The disease {name}, Delted successfully!");

            watch.Stop();
            Extensions.Task_time_printer(watch.Elapsed.ToString());
        }

        public static void Read(string alg_path, string dis_path)
        {
            Stopwatch watch = new Stopwatch();
            
            string[] diseaseFile = FileHandler.DiseasFile(dis_path);
            string[] alergiesFile = FileHandler.AlergisFile(alg_path);

            System.Console.Write("Enter disease for read: ");
            string name = Console.ReadLine();

            watch.Start();
            if (!diseaseFile.Contains($"Dis_{name}"))
            {
                Console.WriteLine("This drug is not available in the database");
                watch.Stop();
                Extensions.Task_time_printer(watch.Elapsed.ToString());
                return;
            }

            System.Console.WriteLine("This disease is availabe in the database");

            for (int i = 0; i < alergiesFile.Length; i++)
            {
                if ( alergiesFile[i].Substring_between_two_chars('_', ' ') == name )
                {
                    System.Console.WriteLine(alergiesFile[i].
                    Substring_from_first_to_second_index(alergiesFile[i].IndexOf(':')+2,
                    alergiesFile[i].Length-1 ));
                    break;
                }
            }

            watch.Stop();
            Extensions.Task_time_printer(watch.Elapsed.ToString());
        }
    }
}
