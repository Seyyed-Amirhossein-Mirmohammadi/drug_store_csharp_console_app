using System;

namespace Final_Project
{
    public static class View
    {
        public static string[] Path_reciver()
        {
            Console.Write("Enter the path of the alergies file: ");
            string alg_path = Validator.Path_validator("alg");
            
            Console.Write("Enter the path of the diseases file: ");
            string dis_path = Validator.Path_validator("dis");

            Console.Write("Enter the path of the drugs file: ");
            string drug_path = Validator.Path_validator("drug");

            Console.Write("Enter the path of the effects file: ");
            string eff_path = Validator.Path_validator("eff");

            return new string[] {alg_path, dis_path, drug_path, eff_path};
        }

        public static void Help()
        {
            Console.WriteLine("Enter 1 in order to do CRD operations for drug");
            Console.WriteLine("Enter 2 in order to do CRD operations for disease");
            Console.WriteLine("Enter 3 in order to search for a drug or disease");
            Console.WriteLine("Enter 4 in order to change all the drug's price according to inflation rate");
            Console.WriteLine("Enter 5 in order to calculate the cost of a prescription");
            Console.WriteLine("Enter 6 in order to check if drugs cause alergies in patient");
            Console.WriteLine("Enter 7 in order to show you which drugs can help you according to your disease");
            Console.WriteLine("Enter q in order to exit");
        }

        public static void Seperator()
        {
            Console.WriteLine("-------------------------------------------");
        }
    }
}