using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using ExtracaoLambda.Data.Utilities;
using ExtracaoLambda.Operational.DataService.Dtos;
using RestSharp;
using RestSharp.Serializers.SystemTextJson;

namespace ExtracaoLambda.Operational.DataService
{
    public class DataService
    {
        public DataService()
        {
            Client = new RestClient(Common.Config["Settings:DataServiceHost"]);
            Client.UseSystemTextJson(new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }

        private RestClient Client { get; }

        public Empresa BuscarEmpresa(string sigla)
        {
            var request = new RestRequest($"/api/empresas/filtrar?sigla={sigla}&ativo=true");

            var response = Client.Get<Empresa>(request);

            return response.Data;
        }

        public Empresa CriarEmpresa(CriarEmpresa empresa)
        {
            var request = new RestRequest("/api/empresas");
            request.AddJsonBody(empresa);

            var response = Client.Post<Empresa>(request);

            return response.Data;
        }

        public Noticia SalvarNoticia(Noticia noticia)
        {
            var request = new RestRequest("/api/noticias");
            request.AddJsonBody(noticia);

            var response = Client.Post<Noticia>(request);

            return response.Data;
        }

        public NoticiaAnalise CriarNoticiaAnalise(NoticiaAnalise noticiaAnalise)
        {
            var request = new RestRequest("/api/noticias-analise");
            request.AddJsonBody(noticiaAnalise);

            var response = Client.Post<NoticiaAnalise>(request);

            return response.Data;
        }

        public void CriarNoticiasAnaliseEmLote(List<NoticiaAnalise> noticiasAnalise)
        {
            Parallel.ForEach(noticiasAnalise, noticiaAnalise => CriarNoticiaAnalise(noticiaAnalise));
        }

        public void CriarNoticiasEmLote(List<Noticia> noticias)
        {
            Parallel.ForEach(noticias, noticia => SalvarNoticia(noticia));
        }

        public Juncao CriarJuncao(CriarJuncao juncao)
        {
            var request = new RestRequest("/api/juncoes");
            request.AddJsonBody(juncao);

            var response = Client.Post<Juncao>(request);

            return response.Data;
        }
    }
}