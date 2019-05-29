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
            //Serialize();
            Deserialize();
            Console.WriteLine($"Process completed: {DateTime.UtcNow}");

            Console.ReadKey();
        }

        static void Serialize()
        {
            ConvertFineWineCsvToJson(typeof(FineWineCat), "finewinecats.csv", "finewinecats.json");
            ConvertFineWineCsvToJson(typeof(FineWineLog), "finewinelogs.csv", "finewinelogs.json");
        }

        static void Deserialize()
        {
            List<FineWineCat> cats = ConvertFineWineJsonToObj<FineWineCat>("finewinecats.json");
            List<FineWineLog> logs = ConvertFineWineJsonToObj<FineWineLog>("finewinelogs.json");
        }

        static void ConvertFineWineCsvToJson(Type type, string inputFilePath, string outputFilePath)
        {
            List<IObject> objects;
            List<string> lines = CsvReader.ReadLines(true, inputFilePath);

            switch (type.Name)
            {
                case "FineWineCat":
                    objects = FineWineObjManager.GetCatInstances(lines);
                    break;
                case "FineWineLog":
                    objects = FineWineObjManager.GetLogInstances(lines);
                    break;
                default:
                    objects = FineWineObjManager.GetCatInstances(lines);
                    break;
            }

            WriteToJson(objects, outputFilePath);
        }

        static List<T> ConvertFineWineJsonToObj<T>(string jsonFilePath)
        {
            List<T> objects;

            using (StreamReader reader = new StreamReader(jsonFilePath))
            {
                string json = reader.ReadToEnd();
                objects = JsonConvert.DeserializeObject<List<T>>(json);
            }

            return objects;
        }

        static void WriteToJson(List<IObject> objects, string outputFilePath)
        {
            using (StreamWriter writer = File.AppendText(outputFilePath))
            {
                string json;
                writer.WriteLine('[');

                for (int i = 0; i < objects.Count; i++)
                {
                    json = JsonConvert.SerializeObject(objects[i], Formatting.Indented);
                    json = i.Equals(objects.Count - 1) ? json : json + ',';
                    writer.WriteLine(json);
                }

                writer.WriteLine(']');
            }
        }
    }
}
