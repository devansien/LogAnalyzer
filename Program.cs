using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

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
            using (StreamWriter writer = File.AppendText(""))
            {
                JsonSerializer serializer = new JsonSerializer();

                foreach (IObject obj in objects)
                {
                    serializer.Serialize(writer, obj);
                    string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
                    Console.WriteLine(json);
                }
            }
        }
    }
}
