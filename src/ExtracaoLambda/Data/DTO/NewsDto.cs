using System.Text.Json.Serialization;

namespace ExtracaoLambda.Data.DTO
{
    public class NewsDto
    {
        public Data[] data { get; set; }
    }

    public class Data
    {
        [JsonPropertyName("news_url")]
        public string NewsUrl { get; set; }
        public string title { get; set; }
        public string text { get; set; }
        public string sentiment { get; set; }
    }
}