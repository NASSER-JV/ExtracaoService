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

        public NewsDto BuscarNoticiasStockNews(Payload payload)
        {
            var buscaClient = new RestClient("https://stocknewsapi.com/api/v1");
            buscaClient.UseSystemTextJson(new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });
            var request =
                new RestRequest($"?tickers={payload.Sigla}&items=50&token={stockNewsApiKey}&date={payload.DataInicial}-{payload.DataFinal}");
            var response = buscaClient.Get(request);
            return buscaClient.Deserialize<NewsDto>(response).Data;
        }
    }
}