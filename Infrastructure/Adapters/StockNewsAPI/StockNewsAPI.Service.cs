using System.Net;
using System.Net.Http.Json;
using ExtracaoService.Infrastructure.Adapters.DataService.Dtos;
using ExtracaoService.Infrastructure.Adapters.StockNewsAPI.Dtos;
using ExtracaoService.Infrastructure.Configurations;

namespace ExtracaoService.Infrastructure.Adapters.StockNewsAPI;

public class StockNewsApiService
{
    public StockNewsApiService()
    {
        Client = new HttpClient();
        Client.BaseAddress = new Uri("https://stocknewsapi.com/api/v1");
    }

    private string StockNewsApiKey { get; } = Configuration.Config["StockNewsApiKey"];
    private HttpClient Client { get; }

    public async Task<List<News>> SearchNews(DateTime dataInicial, DateTime dataFinal, List<string> tickers, int pagina = 1)
    {
        var newsList = new List<News>();

        var response = await Client.GetFromJsonAsync<NewsList>(
            $"?tickers={string.Join(",", tickers)}&items=50&token={StockNewsApiKey}&page={pagina}&date={dataInicial:MMddyyyy}-{dataFinal:MMddyyyy}&sentiment=positive,negative&extra-fields=id,eventid");

        if (response is null) return new List<News>();

        newsList.AddRange(response.Data);

        if (pagina < response.TotalPages)
            newsList.AddRange(await SearchNews(dataInicial, dataFinal, tickers, pagina + 1));

        return newsList;
    }
}