using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Final_Project
{
    public static class Extensions
    {
        public static string Substring_between_two_chars
        (this string str, char c1, char c2)
        {
            int start = str.IndexOf(c1);
            int end =  str.IndexOf(c2);
            return str.Substring(start+1, end-start-1);
        }

        public static string Substring_from_first_to_second_index
        (this string str, int n1, int n2)
            => str.Substring(n1, n2-n1+1);

        public static bool eff_File_have_the_quality_to_be_deleted(this string str, string name)
        {
            if (!str.Contains(name))
                return false;
            
            if (str.IndexOf(name) == 0)
                return true;
            
            else if (str.IndexOf(name) > 0 &&
                str.Substring_from_first_to_second_index(
                    str.IndexOf(':')+2,
                    str.Length-1).Split(';').Length == 1)
                return true;
            
            return false;
        }

        public static bool drug_is_in_databasse(string drug, string[] File)
        {
           foreach (var item in File)
                if (item.Substring_between_two_chars('_',' ') == drug)
                    return true;
            return false;
        }

        public static bool disease_is_in_databasse(string disease, string[] File)
        {
           foreach (var item in File)
                if (item == "Dis_" + disease)
                    return true;
            return false;
        }
        
        public static void Add_drug_to_alergies(string name,
        string[] diseasesFile, 
        string[] alergiesFile,
        string alg_path,
        Stopwatch watch,
        bool is_positive = true)
        {
            watch.Start();
            char NegOrPos;
            if (is_positive) NegOrPos = '+';
            else NegOrPos = '-';
            
            bool founded = false;
            string dis;
            watch.Stop();

            while ((dis = Console.ReadLine()) != null && dis != "")
            {
                watch.Start();
                if (!diseasesFile.Contains("Dis_" + dis))
                {
                    Console.Write($"The disease {dis} is not available in the database. Please enter another: ");
                    watch.Stop();
                }

                else
                {
                    for (int i = 0; i < alergiesFile.Length; i++)
                        if (alergiesFile[i].Substring_between_two_chars('_',' ') == dis)
                        {
                            alergiesFile[i] =
                            alergiesFile[i] + " ; (Drug_" + name + $",{NegOrPos})";
                            File.WriteAllLines("./alergies.txt", alergiesFile);
                            Console.Write("Good. now enter another if you want. else enter blank: ");
                            founded = true;
                            break;
                        }
                    
                    if (!founded)
                    {   
                        Console.Write("Good. now enter another if you want. else enter blank: ");
                        FileHandler.Add_text(alg_path, $"Dis_{dis} : (Drug_{name},{NegOrPos})");
                    }
                    watch.Stop();
                }
            }
        }

        public static int Get_drug_price(string drug_path, string drug_name)
        {
            string[] drugFile = FileHandler.DrugFile(drug_path);

            for (int i = 0; i < drugFile.Length; i++)
            {
                if ( drugFile[i].Substring_between_two_chars('_',' ') == drug_name )
                    return int.Parse(drugFile[i].Split(':')[1].Trim());
            }
            return 0;
        }

        private static string Elapsed_micro_seconds(string str)
        {
            str = str.Substring(6, 10);
            double time = Math.Round(double.Parse(str) * 1000000, 3);
            return time.ToString();
        }

        public static void Task_time_printer(string time)
        {
            Console.WriteLine("Task time: " + Elapsed_micro_seconds(time) + " micro seconds");
        }
    }
}