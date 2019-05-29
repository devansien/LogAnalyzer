using System.Collections.Generic;

namespace LogAnalyzer
{
    class StopWord
    {
        public static HashSet<string> Entries()
        {
            return new HashSet<string>
            {
                "and",
                "the",
            };
        }
    }
}
