using System.IO;

namespace Final_Project 
{
    public static class FileHandler
    {
        public static string[] DrugFile(string path)
        {
            if (path == "")
                return System.IO.File.ReadAllLines("./drugs.txt");
            else
                return System.IO.File.ReadAllLines(path);
        }
        public static string[] AlergisFile(string path)
        {
            if (path == "")
                return System.IO.File.ReadAllLines("./alergies.txt");
            else
                return System.IO.File.ReadAllLines(path);
        }
        
        public static string[] EffectFile(string path)
        {
            if (path == "")
                return System.IO.File.ReadAllLines("./effects.txt");
            else
                return System.IO.File.ReadAllLines(path);
        }

        public static string[] DiseasFile(string path)
        {
            if (path == "")
                return System.IO.File.ReadAllLines("./diseases.txt");
            else
                return System.IO.File.ReadAllLines(path);
        }

        public static void Add_text(string path, string text)
        {
            using (StreamWriter sw = File.AppendText(path))
                sw.WriteLine(text);
        }
    }    
}