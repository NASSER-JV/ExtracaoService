using System;
using System.Text.Json;
using ExtracaoLambda.Data.Entities;
using ExtracaoLambda.Data.Utilities;
using RestSharp;
using RestSharp.Serializers.SystemTextJson;

namespace ExtracaoLambda.Data.Operational
{
    public class OperationalDataService
    {
        private string dataServiceHost => Common.Config["Settings:DataServiceHost"];
        private string dataServiceApiKey => Common.Config["Settings:DataServiceApiKey"];

        private RestClient _client;

        public OperationalDataService()
        {
            _client = new RestClient(dataServiceHost);
            _client.UseSystemTextJson(new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });
            _client.AddDefaultHeader("apiKey", dataServiceApiKey);
        }
        
        public Empresa GetEmpresa(string sigla)
        {
            var request = new RestRequest($"/empresas/filtrar?sigla={sigla}&ativo=true");
            var response = _client.Get(request);
            var responseJson = _client.Deserialize<Empresa>(response).Data;
            return responseJson;
        }
        
        public Empresa CriarEmpresa(Empresa empresa)
        {
            var request = new RestRequest($"/empresas/criar");
            request.AddJsonBody(empresa);
            var response = _client.Post(request);
            var responseJson = _client.Deserialize<Empresa>(response).Data;
            return responseJson;
        }

        public Noticia CriarNoticia(Noticia noticia)
        {
            var request = new RestRequest($"/noticias/criar");
            request.AddJsonBody(noticia);
            var response = _client.Post(request);
            var responseJson = _client.Deserialize<Noticia>(response).Data;
            return responseJson;
        }
        
        public JuncoesDto CriarJuncao(Juncoes juncao)
        {
            var request = new RestRequest($"/juncoes/criar");
            request.AddJsonBody(juncao);
            var response = _client.Post(request);
            var responseJson = _client.Deserialize<JuncoesDto>(response).Data;
            return responseJson;
        }

    }
}