using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
            //List<FineWineCat> cats = ConvertFineWineJsonToObj<FineWineCat>("finewinecats.json");

            List<FineWineCat> cats = ConvertFineWineJsonToObj<FineWineCat>("finewinecatsS.json");
            List<FineWineLog> logs = ConvertFineWineJsonToObj<FineWineLog>("finewinelogs.json");

            var sorted = from entry in FineWineObjManager.GetMostQueriedCat(cats, logs) orderby entry.Value descending select entry;

            int counter = 0;
            foreach (KeyValuePair<string, int> pair in sorted)
            {
                if (counter < 10)
                    Console.WriteLine("{0}: {1}", pair.Key, pair.Value);

                counter++;
            }

            //cats = FineWineObjManager.SetCatSynonyms(cats, logs);
            //FileManager.WriteToJson(cats, "finewinecatsS.json");
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

            FileManager.WriteToJson(objects, outputFilePath);
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
    }
}
