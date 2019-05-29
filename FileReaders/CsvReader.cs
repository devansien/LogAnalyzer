using System;
using System.Collections.Generic;
using System.IO;

namespace LogAnalyzer
{
    class CsvReader
    {
        public static List<string> ReadLines(string filePath)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                int counter = 0;
                List<string> lines = new List<string>();

                while (!reader.EndOfStream)
                {
                    if (counter.Equals(0))
                        reader.ReadLine();
                    else
                        lines.Add(reader.ReadLine());

                    counter++;
                }

                return lines;
            }
        }

        public static List<IObject> GetLogInstances(List<string> lines)
        {
            List<IObject> logs = new List<IObject>();

            for (int i = 0; i < lines.Count; i++)
            {
                string[] cols = lines[i].Split(',');

                FineWineLog log = new FineWineLog
                {
                    QueryId = cols[0],
                    CustomerId = int.Parse(cols[1]),
                    NaturalLangQuery = cols[2],
                    Filter = cols[3],
                    ResultCount = int.Parse(cols[4]),
                    Timestamp = DateTime.Parse(cols[5])
                };

                logs.Add(log);
            }

            return logs;
        }
    }
}
