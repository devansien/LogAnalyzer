using System;
using System.Collections.Generic;

namespace LogAnalyzer
{
    class FineWineObjManager
    {
        public static List<IObject> GetCatInstances(List<string> lines)
        {
            List<IObject> cats = new List<IObject>();

            for (int i = 0; i < lines.Count; i++)
            {
                string[] cols = lines[i].Split(',');

                FineWineCat cat = new FineWineCat
                {
                    Type = cols[0].Trim(),
                    Value = cols[1].Trim()
                };

                cats.Add(cat);
            }

            return cats;
        }

        public static List<IObject> GetLogInstances(List<string> lines)
        {
            List<IObject> logs = new List<IObject>();

            for (int i = 0; i < lines.Count; i++)
            {
                string[] cols = lines[i].Split(',');

                FineWineLog log = new FineWineLog
                {
                    QueryId = cols[0].Trim(),
                    CustomerId = int.Parse(cols[1]),
                    NaturalLangQuery = cols[2].Trim().Split(" "),
                    Filter = cols[3].Trim(),
                    ResultCount = int.Parse(cols[4]),
                    Timestamp = DateTime.Parse(cols[5].Trim())
                };

                logs.Add(log);
            }

            return logs;
        }

        public static List<FineWineCat> SetCatSynonyms(List<FineWineCat> cats, List<FineWineLog> logs)
        {
            int score = 0;
            HashSet<string> synonyms = new HashSet<string>();

            for (int i = 0; i < logs.Count; i++)
            {
                for (int j = 0; j < cats.Count; j++)
                {
                    foreach (string query in logs[i].NaturalLangQuery)
                    {
                        if (!string.IsNullOrEmpty(query) && query.Length > 2 && !StopWord.Entries().Contains(query))
                        {
                            string[] categories = cats[j].Value.Trim().Split(" ");

                            foreach (string category in categories)
                            {
                                score = SynonymManager.Compute(category, query);

                                if (category.ToLower().Contains(query.ToLower()) || score < 2)
                                {
                                    synonyms.Add(query);
                                    cats[j].Synonyms.Add(query);
                                }
                            }
                        }
                    }
                }

                foreach (string query in logs[i].NaturalLangQuery)
                {
                    if (!synonyms.Contains(query))
                        cats[0].Synonyms.Add(query);
                }
            }

            return cats;
        }

        public static Dictionary<string, int> GetMostQueriedCat(List<FineWineCat> cats, List<FineWineLog> logs)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();

            for (int i = 0; i < logs.Count; i++)
            {
                foreach (string query in logs[i].NaturalLangQuery)
                {
                    for (int j = 1; j < cats.Count; j++)
                    {
                        if (cats[j].Value.Contains(query))
                        {
                            string key = $"{cats[j].Type},{cats[j].Value}";
                            if (dict.ContainsKey(key))
                                dict[key] += 1;
                            else
                                dict.Add(key, 1);
                        }
                        else
                        {
                            if (cats[j].Synonyms.Contains(query))
                            {
                                string key = $"{cats[j].Type},{cats[j].Value}";
                                if (dict.ContainsKey(key))
                                    dict[key] += 1;
                                else
                                    dict.Add(key, 1);
                            }
                        }
                    }
                }
            }

            return dict;
        }
    }
}
