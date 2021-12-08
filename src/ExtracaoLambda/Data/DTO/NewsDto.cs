using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ExtracaoLambda.Data.DTO
{
    public class NewsDto
    {
        public List<Data> Data { get; set; }
        
        [JsonPropertyName("total_pages")]
        public int TotalPages { get; set; }

        public NewsDto()
        {
            Data = new List<Data>();
        }
    }

    public class Data
    {
        [JsonPropertyName("news_url")]
        public string NewsUrl { get; set; }
        public string Title { get; set; }
        
        public string[] Tickers { get; set; }
        
        public string Date { get; set; }
        public string Text { get; set; }
        
        public string Sentiment { get; set; }
        
        public string CompanyName { get; set; }
    }
}