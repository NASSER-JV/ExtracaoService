using System.Text.Json.Serialization;

namespace ExtracaoLambda.Data.DTO
{
    public class NewsDto
    {
        public Data[] Data { get; set; }
    }

    public class Data
    {
        [JsonPropertyName("news_url")]
        public string NewsUrl { get; set; }
        public string Title { get; set; }
        
        public string Date { get; set; }
        public string Text { get; set; }
        public string Sentiment { get; set; }
    }
}