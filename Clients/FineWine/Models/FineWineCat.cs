using Newtonsoft.Json;

namespace LogAnalyzer
{
    public class FineWineCat : IObject
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
