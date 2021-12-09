using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using ExtracaoLambda.Data.Utilities;
using ExtracaoLambda.Operational.FinancialModellingPrep.Dtos;
using RestSharp;
using RestSharp.Serializers.SystemTextJson;

namespace ExtracaoLambda.Operational.FinancialModellingPrep
{
    public class FinancialModellingPrepService
    {
        public FinancialModellingPrepService()
        {
            Client = new RestClient("https://financialmodelingprep.com");
            Client.UseSystemTextJson(new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }

        private RestClient Client { get; }
        private string ApiKey { get; } = Common.Config["Settings:FinnancialApiKey"];


        public Company SearchCompany(string sigla)
        {
            var request = new RestRequest($"api/v3/profile/{sigla}?apikey={ApiKey}");

            var response = Client.Get<List<Company>>(request);

            return response.Data.First();
        }
    }
}