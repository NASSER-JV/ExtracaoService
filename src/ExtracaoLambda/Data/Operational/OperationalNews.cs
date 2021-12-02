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
        private RestClient _clientFinancial;

        public OperationalNews()
        {
            _clientFinancial = new RestClient("https://financialmodelingprep.com");
            _clientFinancial.UseSystemTextJson(new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });
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

        public DTO.Data[] BuscarNomeEmpresaFinancialApi(string sigla)
        {
            var request =
                new RestRequest(
                    $"api/v3/profile/{sigla}?apikey={Common.Config["Settings:FinnancialApiKey"]}");
            var response = _clientFinancial.Get(request);
            return _clientFinancial.Deserialize<DTO.Data[]>(response).Data;
        }        
    }
}