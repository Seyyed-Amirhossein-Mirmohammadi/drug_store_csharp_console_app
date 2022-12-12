using System;

namespace Final_Project
{
    public static class MainRunner{
        public static void Run()
        {   
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nPlease read README.txt first, in order to be able to use from default datasets\n");
            Console.ResetColor();
            Console.WriteLine("You should first enter the path of the files. Enter blank in order to use from default datasets");
    
            string[] Paths = View.Path_reciver();

            while (true)
            {
                View.Seperator();
                View.Help();
                View.Seperator();

                string Search = Validator.Multi_option_validator(new string[] {
                    "1", "2", "3", "4", "5", "6", "7", "q"
                });

                if (Search == "1")
                {
                    Console.WriteLine();
                    Console.Write("Now you are in CRD_Drug section. Enter 1 to create, 2 to read ,and 3 to delete: ");
                    string CRD_choice = Validator.Multi_option_validator(new string[]{
                        "1", "2", "3"
                    });

                    if (CRD_choice == "1")
                        CRD_Drug.Create(Paths[0], Paths[1], Paths[2], Paths[3]);
                    else if (CRD_choice == "2")
                        CRD_Drug.Read(Paths[0], Paths[1], Paths[2], Paths[3]);
                    else
                        CRD_Drug.Delete(Paths[0], Paths[3], Paths[2]);
                    
                    View.Seperator();
                }

                else if (Search == "2")
                {
                    Console.WriteLine();
                    Console.Write("Now you are in CRD_disease section. Enter 1 to create, 2 to read ,and 3 to delete: ");
                    string CRD_choice = Validator.Multi_option_validator(new string[]{
                        "1", "2", "3"
                    });

                    if (CRD_choice == "1")
                        CRD_Disease.Create(Paths[0], Paths[1], Paths[2]);
                    else if (CRD_choice == "2")
                        CRD_Disease.Read(Paths[0], Paths[1]);
                    else
                        CRD_Disease.Delete(Paths[0], Paths[1]);
                    
                    View.Seperator();
                }

                else if (Search == "3")
                {
                    Console.WriteLine();
                    Console.Write("Now you are in the search section. Enter 1 to search drug, or 2 to search disease: ");
                    
                    string choice = Validator.Multi_option_validator(new string[]{
                        "1", "2"
                    });

                    if (choice == "1")
                        CRD_Drug.Read(Paths[0], Paths[1], Paths[2], Paths[3]);
                    else
                        CRD_Disease.Read(Paths[0], Paths[1]);

                    View.Seperator();
                }

                else if (Search == "4")
                {
                    Console.WriteLine();
                    Inflation.inflation(Paths[2]);
                    View.Seperator();
                }

                else if (Search == "5")
                {
                    Console.WriteLine();
                    Prescription.Print_prescription_cost(Paths[2]);
                    View.Seperator();
                }

                else if (Search == "6")
                {
                    Console.WriteLine();
                    Prescription.Check_alergies(Paths[0]);
                    View.Seperator();
                }

                else if (Search == "7")
                {
                    Console.WriteLine();
                    Prescription.Show_helpful_drugs(Paths[0]);
                    View.Seperator();
                }

                else
                    return;

            }
            
        }
    }
}