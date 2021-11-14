using System;

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
            var operational = new OperationalDataService();
            var empresa = operational.GetEmpresa(input.Sigla);
            if (empresa == null)
            {
                empresa = new Empresa()
                {
                    Codigo = input.Sigla,
                    Nome = "Stock",
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
                var noticias = new OperationalNews().BuscarNoticiasStockNews(novoInput);
                foreach (var news in noticias.Data)
                {
                    var noticia = new Noticia
                    {
                        Url = news.NewsUrl,
                        EmpresaId = empresa.Id,
                        Titulo = news.Text,
                        Corpo = news.Text,
                        Date = DateTime.Now,
                    };
                    operational.CriarNoticia(noticia);
                }

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
