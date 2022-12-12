using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Final_Project
{
    public static class CRD_Drug
    {
        public static void Create(string alg_path, string dis_path,
        string drug_path, string eff_path)
        {
            Stopwatch watch = new Stopwatch();

            //recives the name
            Console.WriteLine("\nPlease enter the name of the drug, that you want to add to data base");
            Console.WriteLine("Hint: It is more formal and standard to represent your drug name by 10 letters; but it works if not");
            Console.Write("enter the name here: ");
            string name = Console.ReadLine();
            
            watch.Start();
            string[] drugFile = FileHandler.DrugFile(drug_path);

            //checks if is in database
            if (Extensions.drug_is_in_databasse(name, drugFile))
            {
                Console.WriteLine("This drug was added to the database");
                watch.Stop();
                Extensions.Task_time_printer(watch.Elapsed.ToString());
                return;
            }

            watch.Stop();
            //recives the price
            Console.Write("\nGood. Now, please enter the price of the drug: ");
            
            int price;
            while (!int.TryParse(Console.ReadLine(), out price ))
                Console.Write("Invalid input. Please enter an int number: ");
            
            //recives diseases -
            watch.Start();
            string[] diseasesFile = FileHandler.DiseasFile(dis_path);
            string[] alergiesFile = FileHandler.AlergisFile(alg_path);

            Console.WriteLine($"\nEnter the diseases that drug {name} has a negative effect on");
            Console.WriteLine("Enter blank if you have finished all of them");
            Console.Write("Enter them here: ");

            watch.Stop();
            Extensions.Add_drug_to_alergies(name, diseasesFile, alergiesFile, alg_path, watch, false);
            
            //recives diseases +
            Console.WriteLine($"\nEnter the diseases that drug {name} has a positive effect on");
            Console.WriteLine("Help: Do it like the previous step");
            Console.Write("Enter them here: ");
         

            Extensions.Add_drug_to_alergies(name, diseasesFile, alergiesFile, alg_path, watch);

            //recives effects
            Console.WriteLine("\nPlease enter the name of another drug and Its effect on the drug that you want to add");
            Console.WriteLine("Hint: The another drug should be available in the database");
            Console.WriteLine("An example: Suppose that you eant to add \"A\" drug, and the drug \"B\" (wich is available in the database) " + 
            "will cause to \"C\" effect in combination with \"A\" drug. You should represent the inforamtion like the below: (enter blank if you want to stop the loop)");
            Console.WriteLine("B C");
            
            Console.Write("Enter the drug and its effect here: ");

            //ading to effects file
            string[] effectsFile = FileHandler.EffectFile(eff_path);
            string line;
            int new_drugs_index_in_effFile = effectsFile.Length;
            while ((line = Console.ReadLine()) != null && line != "") {
                bool found = false;
                string[] dr_eff = line.Split();
                if (!Extensions.drug_is_in_databasse(dr_eff[0], drugFile))
                    Console.Write($"The drug {dr_eff[0]}is not available in the database. Please enter another: ");
                
                else{
                    if (new_drugs_index_in_effFile == effectsFile.Length)
                        FileHandler.
                        Add_text(eff_path, $"Drug_{name} : (Drug_{dr_eff[0]},Eff_{dr_eff[1]})");

                    else
                    {
                        effectsFile[new_drugs_index_in_effFile] =
                        effectsFile[new_drugs_index_in_effFile] + 
                        " ; (Drug_" + dr_eff[0] + ",Eff_" + dr_eff[1] + ")";

                        File.WriteAllLines(eff_path, effectsFile);
                    }

                    effectsFile = FileHandler.EffectFile(eff_path);
                    for (int i = 0; i < effectsFile.Length; i++)
                        if (effectsFile[i].Substring_between_two_chars('_',' ') == dr_eff[0])
                        {
                            effectsFile[i] =
                            effectsFile[i] + " ; (Drug_" + name + ",Eff_" + dr_eff[1] + ")";
                            File.WriteAllLines(eff_path, effectsFile);
                            found = true;
                            break;
                        }
                    
                    if (!found)
                        FileHandler.Add_text(eff_path,
                        $"Drug_{dr_eff[0]} : (Drug_{name},Eff_{dr_eff[1]})");
                    
                    Console.Write("Good. now enter another if you want. else enter blank: ");
                }
                
            }

            //ading to drug file
            FileHandler.Add_text(drug_path, $"Drug_{name} : {price}");

            Console.WriteLine($"The drug {name}, added successfully!");
            watch.Stop();
            Extensions.Task_time_printer(watch.Elapsed.ToString());

        }

        public static void Read(string alg_path, string dis_path,
        string drug_path, string eff_path)
        {
            Stopwatch watch = new Stopwatch();

            Console.Write("Enter the name of the drug here: ");

            string name = Console.ReadLine();
            
            watch.Start();
            string[] drugFile = FileHandler.DrugFile(drug_path);

            var drug_check = drugFile.Where(x => 
            x.Substring_between_two_chars('_',' ') == name)
            .ToArray();

            if ( drug_check.Length == 0)
            {
                watch.Stop();
                Console.WriteLine("\nThis Drug is not available");
                Extensions.Task_time_printer(watch.Elapsed.ToString());
                return;
            }

        
            Console.WriteLine("\nThe drug is available\n");

            Console.WriteLine($"--------------{name}'s details--------------\n");

            Console.WriteLine("Price: " +int.Parse(drug_check[0].Split(":")[1].Trim()));

            string[] alergiesFile = FileHandler.AlergisFile(alg_path);
            string[] effectsFile = FileHandler.EffectFile(eff_path);

            List<string> alg_pos = new List<string>();
            List<string> alg_neg = new List<string>();

            string disease;
            foreach (var item in alergiesFile)
            {
                if (item.Contains(name))
                {
                    disease = item.Substring_between_two_chars('_',' ');

                    if ( item[item.IndexOf( ',',item.IndexOf(name)) + 1] == '+' )
                        alg_pos.Add(disease);
                    else
                        alg_neg.Add(disease);
                }
            }

            if (alg_pos.Count>0)
            {
                Console.WriteLine($"These are diseases that drug {name} has a positive effect on");
                foreach (var item in alg_pos)
                    Console.Write(item + " ");
            
                Console.WriteLine();
            }
            
            if (alg_neg.Count>0)
            {
                Console.WriteLine($"These are diseases that drug {name} has a negative effect on");
                foreach (var item in alg_neg)
                    Console.Write(item + " ");
            
                Console.WriteLine();    
            }

            Console.WriteLine();

            string[] eff_line;
            List<string> effects = new List<string>();
            foreach (var item in effectsFile)
            {
                if (item.IndexOf(name) == 5)
                {
                    eff_line = item.Substring(item.IndexOf(':')+2).Split(";").
                    Select(x => x.Trim()).ToArray();
                    foreach (var eff in eff_line)
                    {
                        string[] effs = eff.Split(',');
                        Console.WriteLine("This drug will cause to "
                        + effs[1].Substring_between_two_chars('_', ')')  + " effect " + "in combination with "
                        + effs[0].Substring(6) + " drug" );
                    }
                    break;
                }
            }

            watch.Stop();
            Extensions.Task_time_printer(watch.Elapsed.ToString());
        }

        public static void Delete(string alg_path,
        string eff_path,
        string drug_path)
        {
            Stopwatch watch = new Stopwatch();
            
            Console.Write("Ennter the name of the drug that you want to delete from database: ");
            string name = Console.ReadLine();

            watch.Start();

            string[] alergiesFile = FileHandler.AlergisFile(alg_path);
            string[] effectsFile = FileHandler.EffectFile(eff_path);
            string[] drugFile = FileHandler.DrugFile(drug_path);

            if (! Extensions.drug_is_in_databasse(name, drugFile))
            {
                Console.WriteLine("There is no drug with such a name in database");
                watch.Stop();
                Extensions.Task_time_printer(watch.Elapsed.ToString());
                return;
            }
            
            var temp = new string[2];
            if(name[0] != '(')
            {
                temp[0] = "(Drug_" + name;
                temp[0] += ",+)";
                temp[1] = "(Drug_" + name;
                temp[1] += ",-)";
            }

            for (var i = 0; i < alergiesFile.Length; i++)
            {
                var drugList = alergiesFile[i].Split(":")[1].Trim().Split(";")
                    .Select(a => a.Trim());
                
                var diseaseName = alergiesFile[i].Split(":")[0].Trim();

                string tempLine;

                if (drugList.Contains(name) || drugList.Contains(temp[0]) || drugList.Contains(temp[1]))
                {
                    drugList = drugList.Where(drug => drug != name).ToArray();
                    drugList = drugList.Where(drug => drug != temp[0]).ToArray();
                    drugList = drugList.Where(drug => drug != temp[1]).ToArray();

                    tempLine = string.Join(" ; ", drugList);

                    tempLine = string.Join(" : ", diseaseName, tempLine);

                    alergiesFile[i] = tempLine;

                    File.WriteAllLines(alg_path, alergiesFile);
                }
            }

            drugFile = drugFile.Where(x => 
            x.Substring_between_two_chars('_',' ') != name).ToArray();
            
            File.WriteAllLines(drug_path, drugFile);
            System.Console.WriteLine("Delete Successfully");
            name = $"Drug_{name}";
            effectsFile = effectsFile
            .Where(x => 
            !x.eff_File_have_the_quality_to_be_deleted(name)).ToArray();
            
            // *Delete from alergies

            
        
            for (int i = 0; i < effectsFile.Length; i++)
            {
                if (effectsFile[i].Contains(name))
                {
                    string[] line = effectsFile[i]
                    .Substring_from_first_to_second_index(effectsFile[i].IndexOf(':')+2,
                    effectsFile[i].Length-1).Split(';')
                    .Where(x => !x.Contains(name) )
                    .Select(x => x.Trim()).ToArray();
                    string result = effectsFile[i].
                    Substring_from_first_to_second_index(0, effectsFile[i].IndexOf(' ')-1) + " :";
                    for (int j = 0; j < line.Length; j++)
                    {
                        result += " " + line[j];
                        if (j != line.Length-1)
                            result += " ;"; 
                    }
                    effectsFile[i] = result;
                }
            }

            File.WriteAllLines(eff_path, effectsFile);

            Console.WriteLine($"The drug {name}, deleted successfully");
            
            watch.Stop();
            Extensions.Task_time_printer(watch.Elapsed.ToString());
        }
    }
}