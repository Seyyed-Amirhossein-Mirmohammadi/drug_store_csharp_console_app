using System;
using System.Diagnostics;
using System.Linq;

namespace Final_Project
{
    class Prescription
    {
        public static void Print_prescription_cost(string drug_path)
        {
            Stopwatch watch = new Stopwatch();
            int price = 0;

            Console.WriteLine("Now please enter the name of the dugs and its quantity one at each line");
            Console.WriteLine("Enter blank if you have entered all the drugs");
            Console.WriteLine("An example: \"ucxnqwcpsf 2\". Means there are two ucxnqwcpsf drug in your prescription");
            
            string line;
            while ((line = Console.ReadLine()) != null && line != "") {
                
                while (!Validator.Is_valid_prescription_form(line) && line != "")
                {
                    Console.Write("Invalid input. Try another: ");
                    line = Console.ReadLine();
                }
                
                if (line == "") break;
                
                watch.Start();
                
                string[] drug_and_quantity = line.Split(' ');
                
                price += int.Parse(drug_and_quantity[1].Trim())
                        * Extensions.Get_drug_price(drug_path, drug_and_quantity[0].Trim());
                
                watch.Stop();
                Console.Write("Good. now enter another if you want. else enter blank: ");
            }
            Console.WriteLine();
            Console.WriteLine($"The cost of the prescription is {price}");
            
            watch.Stop();
            Extensions.Task_time_printer(watch.Elapsed.ToString());
        }

        public static void Check_alergies(string alg_path)
        {
            Stopwatch watch = new Stopwatch();

            bool founded = false;

            Console.Write("First enter the diseases of the patient: ");
            string[] diseases = Console.ReadLine().Split();
            Console.Write("Good. now enter the drugs: ");
            string[] drugs = Console.ReadLine().Split();
            Console.WriteLine();

            watch.Start();

            string[] algFile = FileHandler.AlergisFile(alg_path);

            for (int j = 0; j < algFile.Length; j++)
                for (int i = 0; i < diseases.Length; i++)
                    if ( algFile[j].Contains(diseases[i]) )
                    {
                        foreach (var drug in drugs)
                        {
                            if (algFile[j].Contains(drug))
                                if ( algFile[j][algFile[j].IndexOf( ',',algFile[j].IndexOf(drug)) + 1] == '-'  )
                                {
                                    Console.WriteLine($"The drug {drug} have a bad effect on disease {diseases[i]}");
                                    founded = true;
                                }    
                        }
                        break;
                    }
            
            if (!founded)
                Console.WriteLine("Nothing dangerous detected in this prescription accoding to our datasets");

            watch.Stop();
            Extensions.Task_time_printer(watch.Elapsed.ToString());
        }

        public static void Show_helpful_drugs(string alg_path)
        {
            Stopwatch watch = new Stopwatch();

            Console.Write("Enter the disease name, in order to show you which drugs can help you: ");
            string name = Console.ReadLine();

            watch.Start();

            string[] drugs;
            string[] algFile = FileHandler.AlergisFile(alg_path);

            foreach (var item in algFile)
                if ( item.Substring_between_two_chars('_',' ') == name )
                {
                    drugs = item.Substring( item.IndexOf(':') + 2 ).Split(';').
                    Select( x => x.Trim()).ToArray();
                    
                    drugs = drugs.Where( x => x[ x.IndexOf(',') + 1] == '+' ).ToArray();

                    Console.WriteLine( String.Join(" ; ", drugs) );
                    break;
                }


            watch.Stop();
            Extensions.Task_time_printer(watch.Elapsed.ToString());
        }
    }
}