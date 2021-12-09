using System;
using System.Collections.Generic;
using System.Linq;
using ExtracaoLambda.Data.Entities;
using ExtracaoLambda.Operational.DataService.Dtos;
using ExtracaoLambda.Operational.FinancialModellingPrep;
using ExtracaoLambda.Operational.StockNews;

namespace ExtracaoLambda.Operational
{
    public class Operational
    {
        public Operational()
        {
            StockNewsService = new StockNewsService();
            DataService = new DataService.DataService();
            FinancialModellingPrepService =
                new FinancialModellingPrepService();
        }

        private StockNewsService StockNewsService { get; }
        private DataService.DataService DataService { get; }

        private FinancialModellingPrepService FinancialModellingPrepService { get; }

        public void ObterNoticias(Payload payload)
        {
            var empresa = DataService.BuscarEmpresa(payload.Sigla);
            if (empresa == null)
            {
                var company = FinancialModellingPrepService.SearchCompany(payload.Sigla);
                empresa = DataService.CriarEmpresa(new CriarEmpresa
                {
                    Codigo = payload.Sigla,
                    Nome = company.CompanyName,
                    Ativo = true
                });
            }

            var newsList = StockNewsService.SearchNews(payload.DataInicial, payload.DataFinal,
                new List<string> { payload.Sigla });

            var noticias = newsList.Select(news => new Noticia
            {
                Url = news.NewsUrl,
                Titulo = news.Title,
                Texto = news.Text,
                Data = DateTime.Parse(news.Date),
                EmpresaId = empresa.Id,
                Sentimento = SentimentoParaNumero(news.Sentiment)
            }).ToList();
            DataService.CriarNoticiasEmLote(noticias);

            var junção = new CriarJuncao
            {
                EmpresaId = empresa.Id,
                DataFinal = payload.DataFinal,
                DataInicio = payload.DataInicial
            };
            DataService.CriarJuncao(junção);
        }

        public void ObterNoticiasAnalise(Payload input)
        {
            var newsList = StockNewsService.SearchNews(input.DataInicial, input.DataFinal, input.Tickers.ToList());

            var noticiasAnalise = newsList.Select(news => new NoticiaAnalise
            {
                Url = news.NewsUrl,
                Titulo = news.Title,
                Texto = news.Text,
                Tickers = news.Tickers,
                Sentimento = SentimentoParaNumero(news.Sentiment)
            }).ToList();
            DataService.CriarNoticiasAnaliseEmLote(noticiasAnalise.Where(noticia => noticia.Sentimento != 0).ToList());
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
}