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
            ConvertFineWineCsvToJson(typeof(FineWineCat), "finewinecat.csv", "finewinecat.json");
            //ConvertFineWineCsvToJson(typeof(FineWineLog), "finewinelogs.csv", "finewinelogs.json");
            Console.WriteLine($"Process completed: {DateTime.UtcNow}");

            Console.ReadKey();
        }

        static void ConvertFineWineCsvToJson(Type type, string inputFilePath, string outputFilePath)
        {
            List<IObject> objects = null;
            List<string> lines = CsvReader.ReadLines(true, inputFilePath);

            switch (type.GetType().Name)
            {
                case "FineWineCat":
                    FineWineObjManager.GetCatInstances(lines);
                    break;
                case "FineWineLog":
                    FineWineObjManager.GetLogInstances(lines);
                    break;
                default:
                    FineWineObjManager.GetCatInstances(lines);
                    break;
            }

            WriteToJson(objects, outputFilePath);
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
