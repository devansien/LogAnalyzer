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
            ConvertFineWineCsvToJson("finewinelogs.csv", "finewinelogs.json");
            Console.WriteLine($"Process completed: {DateTime.UtcNow}");

            Console.ReadKey();
        }

        static void ConvertFineWineCsvToJson(string inputFilePath, string outputFilePath)
        {
            List<string> lines = CsvReader.ReadLines(true, inputFilePath);
            List<IObject> logs = FineWineObjManager.GetLogInstances(lines);
            WriteToJson(logs, outputFilePath);
        }

        static void WriteToJson(List<IObject> objects, string outputFilePath)
        {
            using (StreamWriter writer = File.AppendText(outputFilePath))
            {
                writer.WriteLine('[');
                for (int i = 0; i < objects.Count; i++)
                {
                    string json = JsonConvert.SerializeObject(objects[i], Formatting.Indented);
                    json = i.Equals(objects.Count - 1) ? json : json + ',';
                    writer.WriteLine(json);
                }
                writer.WriteLine(']');
            }
        }
    }
}
