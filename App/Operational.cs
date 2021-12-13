using ExtracaoService.Infrastructure.Adapters.DataService;
using ExtracaoService.Infrastructure.Adapters.DataService.Dtos;
using ExtracaoService.Infrastructure.Adapters.DataService.Dtos.Enums;
using ExtracaoService.Infrastructure.Adapters.FinancialModellingPrep;
using ExtracaoService.Infrastructure.Adapters.StockNewsAPI;
using ExtracaoService.Infrastructure.Adapters.StockNewsAPI.Dtos;

namespace ExtracaoService.App;

public class Operational
    {
        public Operational()
        {
            StockNewsService = new StockNewsApiService();
            DataService = new DataService();
            FinancialModellingPrepService =
                new FinancialModellingPrepService();
        }

        private StockNewsApiService StockNewsService { get; }
        private DataService DataService { get; }

        private FinancialModellingPrepService FinancialModellingPrepService { get; }

        public async Task ExecutarTarefas()
        {
            var tarefas = await DataService.BuscarTarefasPorFinalizado(false);

            await Task.WhenAll(tarefas.Select(ExecutarTarefa));
        }

        private async Task ExecutarTarefa(Tarefa tarefa)
        {
            var news = await ObterNoticias(tarefa.Tickers, tarefa.DataInicial, tarefa.DataFinal);

            switch (tarefa.Tipo)
            {
                case TipoTarefa.ExtrairNoticias:
                    await SalvarNoticias(tarefa, news);
                    break;
                case TipoTarefa.ExtrairNoticiasAnalise:
                    SalvarNoticiasAnalise(news);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(tarefa.Tipo));
            }

            tarefa.Finalizado = true;
            await DataService.SalvarTarefa(tarefa);
        }

        private async Task<List<News>> ObterNoticias(IEnumerable<string> tickers, DateTime dataInicial, DateTime dataFinal)
        {
            return await StockNewsService.SearchNews(dataInicial, dataFinal, tickers.ToList());
        }

        private async Task SalvarNoticias(Tarefa tarefa, List<News> newsList)
        {
            var empresa = await DataService.BuscarEmpresa(tarefa.Tickers[0]);
            if (empresa == null)
            {
                var company = await FinancialModellingPrepService.SearchCompany(tarefa.Tickers[0]);
                empresa = await DataService.CriarEmpresa(new CriarEmpresa
                {
                    Codigo = tarefa.Tickers[0],
                    Nome = company.CompanyName,
                    Ativo = true
                });
            }

            var noticias = newsList.Select(news => new Noticia
            {
                Id = news.NewsId,
                Url = news.NewsUrl,
                Titulo = news.Title,
                Texto = news.Text,
                Data = DateTime.Parse(news.Date),
                EmpresaId = empresa!.Id,
                Sentimento = SentimentoParaNumero(news.Sentiment),
                EventoId = news.EventId
            }).ToList();
            DataService.CriarNoticiasEmLote(noticias);

            var junção = new CriarJuncao
            {
                EmpresaId = empresa!.Id,
                DataFinal = tarefa.DataFinal,
                DataInicio = tarefa.DataInicial
            };
            await DataService.CriarJuncao(junção);
        }
        
        

        private void SalvarNoticiasAnalise(List<News> newsList)
        {
            var noticiasAnalise = newsList.Select(news => new NoticiaAnalise
            {
                Url = news.NewsUrl,
                Titulo = news.Title,
                Texto = news.Text,
                Tickers = news.Tickers,
                Data = DateTime.Parse(news.Date),
                Sentimento = SentimentoParaNumero(news.Sentiment)
            }).ToList();
            DataService.CriarNoticiasAnaliseEmLote(noticiasAnalise.ToList());
        }

        private static int SentimentoParaNumero(string sentimento)
        {
            switch (sentimento)
            {
                case "Positive":
                    return 1;
                case "Negative":
                    return -1;
                default: throw new ArgumentOutOfRangeException(sentimento);
            }
        }
    }