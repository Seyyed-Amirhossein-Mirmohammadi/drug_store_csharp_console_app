using System;
using System.IO;

namespace Final_Project
{
    class Validator
    {
        public static string Path_validator(string str)
        {
            string path;
            while (!File.Exists(path = Console.ReadLine()))
            {   
                if (path == "")
                {
                    if (str == "drug")
                        path = "./datasets/drugs.txt";
                    
                    else if (str == "alg")
                        path = "./datasets/alergies.txt";
                    
                    else if (str == "dis")
                        path = "./datasets/diseases.txt";
                    
                    else if (str == "eff")
                        path = "./datasets/effects.txt";
                    
                    break;
                }

                else
                    Console.Write("These file does not exists. Please enter another path: ");
            }

            if ( !File.Exists(path) )
            
                throw new Exception("You can not use from default path. Please read README.txt first");
            
            return path;
        }

        public static string Multi_option_validator(string[] valids)
        {
            string result = Console.ReadLine();
            while ( !Is_valid(result, valids) )
            {
                
                Console.Write("Invalid input. Try another: ");
                result = Console.ReadLine();
            }
            return result;
        }

        private static bool Is_valid(string str, string[] valids)
        {
            for (int i = 0; i < valids.Length; i++)
                if ( valids[i] == str )
                    return true;
            return false;
        }

        public static bool Is_valid_prescription_form(string line)
        {
            int x;
            if (line.Split().Length != 2)
                return false;
            
            if (!int.TryParse(line.Split()[1].Trim(), out x ))
                return false;
            return true;
        }

    }
}