using System.Text.Json;
using ExtracaoLambda.Data.Entities;
using ExtracaoLambda.Data.Utilities;
using RestSharp;
using RestSharp.Serializers.SystemTextJson;

namespace ExtracaoLambda.Data.Operational
{
    public class Operational
    {
        private string dataServiceHost => Common.Config["Settings:DataServiceHost"];
        private string dataServiceApiKey => Common.Config["Settings:DataServiceApiKey"];
        public Empresa GetEmpresa(string sigla)
        {
            var client = new RestClient(dataServiceHost);
            client.UseSystemTextJson(new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });
            var request = new RestRequest($"/empresas/filtrar?sigla={sigla}&ativo=true");
            request.AddHeader("apiKey", dataServiceApiKey);
            var response = client.Get(request);
            var responseJson = client.Deserialize<Empresa>(response).Data;
            return responseJson;
        }
        
        public Empresa CriarEmpresa(Empresa empresa)
        {
            var client = new RestClient(dataServiceHost);
            var request = new RestRequest($"/empresas/criar");
            client.UseSystemTextJson(new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });
            request.AddHeader("apiKey", dataServiceApiKey);
            request.AddJsonBody(empresa);
            var response = client.Post(request);
            var responseJson = client.Deserialize<Empresa>(response).Data;
            return responseJson;
        }

        public Noticia CriarNoticia(Noticia noticia)
        {
            var client = new RestClient(dataServiceHost);
            var request = new RestRequest($"/noticias/criar");
            client.UseSystemTextJson(new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });
            request.AddHeader("apiKey", dataServiceApiKey);
            request.AddJsonBody(noticia);
            var response = client.Post(request);
            var responseJson = client.Deserialize<Noticia>(response).Data;
            return responseJson;
        }
        
        public Juncoes CriarJuncao(Juncoes juncao)
        {
            var client = new RestClient(dataServiceHost);
            var request = new RestRequest($"/juncoes/criar");
            client.UseSystemTextJson(new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });
            request.AddHeader("apiKey", dataServiceApiKey);
            request.AddJsonBody(juncao);
            var response = client.Post(request);
            var responseJson = client.Deserialize<Juncoes>(response).Data;
            return responseJson;
        }
        
    }
}