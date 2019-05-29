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
    }
}
