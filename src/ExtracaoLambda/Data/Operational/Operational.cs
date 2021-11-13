using System.Text.Json;
using ExtracaoLambda.Data.Entities;
using ExtracaoLambda.Data.Utilities;
using RestSharp;

namespace ExtracaoLambda.Data.Operational
{
    public class Operational
    {
        private string dataServiceHost => Common.Config["Settings:DataServiceHost"];
        private string dataServiceApiKey => Common.Config["Settings:DataServiceApiKey"];
        public Empresa GetEmpresa(string sigla)
        {
            var client = new RestClient(dataServiceHost);
            var request = new RestRequest($"/empresas/filtrar?sigla={sigla}&ativo=true");
            request.AddHeader("apiKey", dataServiceApiKey);
            var response = client.Get(request);
            var responseJson = JsonSerializer.Deserialize<Empresa>(response.Content);
            return responseJson;
        }
        
        public Empresa CriarEmpresa(Empresa empresa)
        {
            var client = new RestClient(dataServiceHost);
            var request = new RestRequest($"/empresas/criar");
            request.AddHeader("apiKey", dataServiceApiKey);
            request.AddJsonBody(JsonSerializer.Serialize(empresa));
            var response = client.Post(request);
            var responseJson = JsonSerializer.Deserialize<Empresa>(response.Content);
            return responseJson;
        }

        public Noticia CriarNoticia(Noticia noticia)
        {
            var client = new RestClient(dataServiceHost);
            var request = new RestRequest($"/noticias/criar");
            request.AddHeader("apiKey", dataServiceApiKey);
            request.AddJsonBody(JsonSerializer.Serialize(noticia));
            var response = client.Post(request);
            var responseJson = JsonSerializer.Deserialize<Noticia>(response.Content);
            return responseJson;
        }
    }
}