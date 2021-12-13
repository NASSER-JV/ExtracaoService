using System.Text.Json.Serialization;

namespace ExtracaoService.Infrastructure.Adapters.StockNewsAPI.Dtos;

public class NewsList
{
    public NewsList()
    {
        Data = new List<News>();
    }

    public List<News> Data { get; set; }

    [JsonPropertyName("total_pages")] public int TotalPages { get; set; }
}

public class News
{
    [JsonPropertyName("news_id")] public int NewsId { get; set; }
    [JsonPropertyName("news_url")] public string NewsUrl { get; set; }

    public string Title { get; set; }

    public string[] Tickers { get; set; }

    public string Date { get; set; }
    public string Text { get; set; }

    public string Sentiment { get; set; }
    [JsonPropertyName("eventid")] public string EventId { get; set; }
}