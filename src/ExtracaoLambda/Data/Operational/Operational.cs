using System;
using System.Collections.Generic;
using System.Linq;
using ExtracaoLambda.Data.Entities;

namespace ExtracaoLambda.Data.Operational
{
    public class Operational
    {
        public static void FunctionGetNews(Payload input)
        {
            var operationalNews = new OperationalNews();
            var operational = new OperationalDataService();
            var empresa = operational.GetEmpresa(input.Sigla);
            if (empresa == null)
            {
                var nomeEmpresa = operationalNews.BuscarNomeEmpresaFinancialApi(input.Sigla);
                empresa = new Empresa()
                {
                    Codigo = input.Sigla,
                    Nome = nomeEmpresa[0].CompanyName,
                    Ativo = true
                };
                empresa = operational.CriarEmpresa(empresa);
            }

            if (empresa != null)
            {
                var novoInput = new Payload()
                {
                    DataFinal = input.DataFinal.Replace("/", ""),
                    DataInicial = input.DataInicial.Replace("/", ""),
                    Sigla = input.Sigla
                };
                var noticias = new List<Noticia>();
                var noticiasStock = operationalNews.BuscarNoticiasStockNews(novoInput, 1);
                foreach (var news in noticiasStock.Data)
                {
                    var sentimento = tractiveSentiment(news.Sentiment);
                    var noticia = new Noticia
                    {
                        Url = news.NewsUrl,
                        EmpresaId = empresa.Id,
                        Sentimento = sentimento,
                        Titulo = news.Text,
                        Texto = news.Text,
                        Date = Convert.ToDateTime(news.Date),
                    };
                    noticias.Add(noticia);
                }
                operational.ImportarNoticias(noticias);

                var junção = new Juncoes()
                {
                    EmpresaId = empresa.Id,
                    DataFim = DateTime.Parse(input.DataFinal),
                    DataInicio = DateTime.Parse(input.DataFinal),
                };
                operational.CriarJuncao(junção);

            }
        }
        
        public static void FunctionGetNewsAnalysis(Payload input)
        {
            var operationalNews = new OperationalNews();
            var operational = new OperationalDataService();

            var novoInput = new Payload()
            {
                DataFinal = input.DataFinal.Replace("/", ""),
                DataInicial = input.DataInicial.Replace("/", ""),
                Tickers = input.Tickers
            };
            var noticias = new List<NoticiaAnalise>();
            var noticiasStock = operationalNews.BuscarNoticiasStockNews(novoInput, 1);
            foreach (var news in noticiasStock.Data)
            {
                var sentimento = tractiveSentiment(news.Sentiment);
                var noticiaAnalise = new NoticiaAnalise
                {
                    Url = news.NewsUrl,
                    Sentimento = sentimento,
                    Texto = news.Text,
                    Titulo = news.Title,
                    Tickers = news.Tickers,
                };
                noticias.Add(noticiaAnalise);
            }

            var noticiasComSentimento = noticias.Where(x => x.Sentimento != null).ToList();
            operational.ImportarNoticiasAnalise(noticiasComSentimento);
                
        }

        private static int tractiveSentiment(string Sentiment)
        {
            if (Sentiment.Equals("Positive"))
                return 1;
            if (Sentiment.Equals("Negative"))
                return -1;
            
            return 0;
        }
        
    }
}