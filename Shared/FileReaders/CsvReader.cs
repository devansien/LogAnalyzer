using System.Collections.Generic;
using System.IO;

namespace LogAnalyzer
{
    class CsvReader
    {
        public static List<string> ReadLines(bool skipFirstLine, string inputFilePath)
        {
            using (StreamReader reader = new StreamReader(inputFilePath))
            {
                int counter = 0;
                List<string> lines = new List<string>();

                while (!reader.EndOfStream)
                {
                    if (skipFirstLine && counter.Equals(0))
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
