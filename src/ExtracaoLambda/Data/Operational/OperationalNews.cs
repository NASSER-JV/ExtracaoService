using System.Text.Json;
using ExtracaoLambda.Data.DTO;
using ExtracaoLambda.Data.Entities;
using ExtracaoLambda.Data.Utilities;
using RestSharp;
using RestSharp.Serializers.SystemTextJson;

namespace ExtracaoLambda.Data.Operational
{
    public class OperationalNews
    {
        private string stockNewsApiKey => Common.Config["Settings:StockNewsApiKey"];
        private RestClient _client;

        public OperationalNews()
        {
            _client = new RestClient("https://stocknewsapi.com/api/v1");
            _client.UseSystemTextJson(new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });
        }
        
        public NewsDto BuscarNoticiasStockNews(Payload payload)
        {
            var request =
                new RestRequest($"?tickers={payload.Sigla}&items=50&token={stockNewsApiKey}&date={payload.DataInicial}-{payload.DataFinal}");
            var response = _client.Get(request);
            return _client.Deserialize<NewsDto>(response).Data;
        }
    }
}