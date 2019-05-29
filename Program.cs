using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LogAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Process started: {DateTime.UtcNow}");
            ConvertFineWineCsvToJson("finewinelogs.csv");
            Console.WriteLine($"Process completed: {DateTime.UtcNow}");

            Console.ReadKey();
        }

        static void ConvertFineWineCsvToJson(string filePath)
        {
            List<string> lines = CsvReader.ReadLines(filePath);
            List<IObject> logs = CsvReader.GetLogInstances(lines);
            WriteToJson(logs);
        }

        static void WriteToJson(List<IObject> objects)
        {
            foreach (IObject obj in objects)
            {
                string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
                Console.WriteLine(json);
            }
        }
    }
}
