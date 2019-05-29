using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace LogAnalyzer
{
    class FileManager
    {
        public static void WriteToJson<T>(List<T> objects, string outputFilePath)
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
