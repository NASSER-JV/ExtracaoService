using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using ExtracaoLambda.Data.Utilities;
using ExtracaoLambda.Operational.StockNews.Dtos;
using RestSharp;
using RestSharp.Serializers.SystemTextJson;

namespace ExtracaoLambda.Operational.StockNews
{
    public class StockNewsService
    {
        public StockNewsService()
        {
            Client = new RestClient("https://stocknewsapi.com/api/v1");
            Client.UseSystemTextJson(new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }

        private string StockNewsApiKey { get; } = Common.Config["Settings:StockNewsApiKey"];
        private RestClient Client { get; }

        public List<News> SearchNews(DateTime dataInicial, DateTime dataFinal, List<string> tickers, int pagina = 1)
        {
            var newsList = new List<News>();

            var request =
                new RestRequest(
                    $"?tickers={string.Join(",", tickers)}&items=50&token={StockNewsApiKey}&page={pagina}&date={dataInicial:MMddyyyy}-{dataFinal:MMddyyyy}&sentiment=positive,negative");
            var response = Client.Get<NewsList>(request);

            if (response.StatusCode != HttpStatusCode.OK) return newsList;

            newsList.AddRange(response.Data.Data);

            if (pagina < response.Data.TotalPages)
                newsList.AddRange(SearchNews(dataInicial, dataFinal, tickers, pagina + 1));

            return newsList;
        }
    }
}