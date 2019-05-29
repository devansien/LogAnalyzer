using System;
using System.Collections.Generic;

namespace LogAnalyzer
{
    class FineWineObjManager
    {
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
