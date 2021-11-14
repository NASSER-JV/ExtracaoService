using System;
using System.Collections.Generic;
using Amazon.Lambda.Core;
using ExtracaoLambda.Data.Entities;
using ExtracaoLambda.Data.Operational;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace ExtracaoLambda
{
    public class Function
    {
        
        /// <summary>
        /// A simple function that takes a string and does a ToUpperz
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public string FunctionHandler(Payload input, ILambdaContext context)
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
                var noticiasStock = operationalNews.BuscarNoticiasStockNews(novoInput);
                foreach (var news in noticiasStock.Data)
                {
                    var noticia = new Noticia
                    {
                        Url = news.NewsUrl,
                        EmpresaId = empresa.Id,
                        Titulo = news.Text,
                        Corpo = news.Text,
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
            return "Processo concluído";
        }

    }
}
