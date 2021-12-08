using System.Collections.Generic;
using System.Net;
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
        
        
        public List<DTO.Data> BuscarNoticiasStockNews(Payload payload, int pagina)
        {
            
            var newsDto = new List<DTO.Data>();
            var request = new RestRequest();
            if (payload.Sigla == null)
            {
                var tickers = string.Join(",", payload.Tickers);
                request.Resource =
                    $"?tickers={tickers}&items=50&token={stockNewsApiKey}&page={pagina}&date={payload.DataInicial}-{payload.DataFinal}";
            }
            else
            {
                request.Resource =
                    $"?tickers={payload.Sigla}&items=50&token={stockNewsApiKey}&page={pagina}&date={payload.DataInicial}-{payload.DataFinal}";
            }

            var response = _client.Get(request);
            if (response.StatusCode != HttpStatusCode.OK) return newsDto;
            var newsData = _client.Deserialize<NewsDto>(response).Data;
            newsDto.AddRange(newsData.Data);
            pagina++;
            if (pagina <= newsData.TotalPages)
            {
                newsDto.AddRange(BuscarNoticiasStockNews(payload, pagina));
            }

            return newsDto;
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