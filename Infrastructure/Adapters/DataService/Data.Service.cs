using System.Net.Http.Json;
using System.Security.Cryptography;
using ExtracaoService.Infrastructure.Adapters.DataService.Dtos;
using ExtracaoService.Infrastructure.Configurations;

namespace ExtracaoService.Infrastructure.Adapters.DataService;

public class DataService
    {
        public DataService()
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri(Configuration.Config["DataServiceHost"]);
        }

        private HttpClient Client { get; }

        public async Task<Empresa?> BuscarEmpresa(string sigla)
        {
            return await Client.GetFromJsonAsync<Empresa>($"/api/empresas/filtrar?sigla={sigla}&ativo=true");
        }

        public async Task<Empresa?> CriarEmpresa(CriarEmpresa empresa)
        {
            var response = await Client.PostAsJsonAsync("/api/empresas", empresa);

            return await response.Content.ReadFromJsonAsync<Empresa>();
        }

        public async Task SalvarNoticia(Noticia noticia)
        { 
            await Client.PostAsJsonAsync("api/noticias", noticia);
        }

        public async Task CriarNoticiaAnalise(NoticiaAnalise noticiaAnalise)
        {
            await Client.PostAsJsonAsync("api/noticias-analise", noticiaAnalise);
        }

        public async void CriarNoticiasAnaliseEmLote(List<NoticiaAnalise> noticiasAnalise)
        {
            Task.WaitAll(noticiasAnalise.Select(CriarNoticiaAnalise).ToArray());
        }

        public void CriarNoticiasEmLote(List<Noticia> noticias)
        {
            Task.WaitAll(noticias.Select(SalvarNoticia).ToArray());
        }

        public async Task CriarJuncao(CriarJuncao juncao)
        {
            await Client.PostAsJsonAsync("api/juncoes", juncao);
        }

        public async Task<List<Tarefa>> BuscarTarefasPorFinalizado(bool finalizado)
        {
            var data = await Client.GetFromJsonAsync<List<Tarefa>>($"api/tarefas?finalizado={finalizado}");

            return data ?? throw new HttpRequestException("Erro ao realizar requisicao");
        }

        public async Task SalvarTarefa(Tarefa tarefa)
        {
            await Client.PostAsJsonAsync("api/tarefas", tarefa);
        }
    }