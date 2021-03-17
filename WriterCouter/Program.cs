using System;
using System.IO;

namespace WriterCouter
{
    class Program
    {
        static string path =  @"C:\Users\Mario David\Desktop\WriterCounter\WriterCouter\";
        static void Main(string[] args)
        {
            string name = "";
            string type = "";
            int goal = 0;
            int days = 0;
            int wordCount = 0;
            string endDate = "";
            var startDate = DateTime.Now;
            var date = startDate.Date;
            if (!File.Exists(path + "Projects.txt")) 
            {  
                Console.WriteLine("Hola! gracias por usar WritterCounter V1!");
                Console.WriteLine("Por favor introduce los datos de tu proyecto.");
                Console.Write("Nombre del proyecto: ");
                name = Console.ReadLine();
                Console.Write("Tipo deproyecto: ");
                type = Console.ReadLine();
                Console.Write("Meta del proyecto: ");
                goal = Convert.ToInt32(Console.ReadLine());
                Console.Write("Fecha de finalizacion del proyecto: ");
                endDate = Console.ReadLine();
                using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter(path + "Projects.txt", true))
                {
                    
                    string[] dates = date.ToString().Split(" ");
                    file.WriteLine(name + "," + type + "," + goal  + "," + dates[0] + "," + endDate);
                }
            }else
            {
                string[] lines = File.ReadAllLines(path + "Projects.txt");  
                foreach(string line in lines)
                {
                    string[] data = line.Split(",");
                    name = data[0];
                    type = data[1];
                    goal = Convert.ToInt32(data[2]);
                    endDate = data[4];
                    startDate = Convert.ToDateTime(data[3]);
                }
            }
            days = Convert.ToInt16(Math.Round((Convert.ToDateTime(endDate) - startDate).TotalDays, 0));
            int wordsOfTheDay = goal/days;
            bool pass = true;
            do{
                Console.Clear();
                wordCount = 0;
                Console.WriteLine(name);
                if (File.Exists(path + "Updates.txt")) 
                {  
                    string[] lines = File.ReadAllLines(path + "Updates.txt"); 
                    foreach(var line in lines)
                    {
                        if (line.Contains(name))
                        {
                            string[] ls = line.Split(",");
                            wordCount += Convert.ToInt32(ls[1]);
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                wordsOfTheDay = (goal - wordCount)/days;
                Console.WriteLine(wordCount + "/" + goal);
                Console.WriteLine();
                Console.WriteLine(wordsOfTheDay + " palabras por hacer en " + days + " dias");
                Console.WriteLine("Desea agregar palabras al total? Y/N");
                switch(Console.ReadLine().ToUpper())
                {
                    case "Y":
                        wordCount = Convert.ToInt32(Console.ReadLine());
                        using (System.IO.StreamWriter file =
                        new System.IO.StreamWriter(path + "Updates.txt", true))
                        {   
                            string[] dates = date.ToString().Split(" ");
                            file.WriteLine(name + "," + wordCount + "," + dates[0]);
                        }
                        break;
                    case "N":
                        pass = false;
                        break;
                }
                
            }while(pass);

        }
    }
}
