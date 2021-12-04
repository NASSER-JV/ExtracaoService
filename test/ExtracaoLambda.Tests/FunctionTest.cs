using System;
using Amazon.Lambda.TestUtilities;
using Xunit;
using ExtracaoLambda.Data.Entities;
using ExtracaoLambda.Data.Operational;

namespace ExtracaoLambda.Tests
{
    public class FunctionTest
    {

        [Fact]
        public void CreateCompanyDataService()
        {
            var empresa = new Empresa()
            {
                Nome = "Teste - Extracao",
                Ativo = true,
                Codigo = "TTEX"
            };
            var empresaCriada = new OperationalDataService().CriarEmpresa(empresa);

            Assert.Equal("Teste - Extracao", empresaCriada.Nome);
        }
        
        [Fact]
        public void GetCompanyDataService()
        {
            var operational = new OperationalDataService();
            var empresa = operational.GetEmpresa("TTEX");

            Assert.Equal("Teste - Extracao", empresa.Nome);
        }

        [Fact]
        public void CreateNewsDataService()
        {
            var operatinal = new OperationalDataService();
            var empresa = operatinal.GetEmpresa("TTEX");
            if (empresa == null)
            {
                var novaEmpresa = new Empresa()
                {
                    Nome = "Teste - Extracao",
                    Ativo = true,
                    Codigo = "TTEX"
                };
                empresa = operatinal.CriarEmpresa(novaEmpresa);
            }
            var noticia = new Noticia()
            {
                Url = "teste.com",
                Titulo = "TESTE - EX",
                Corpo = "TESTE - CORPO",
                Date = DateTime.Now,
                EmpresaId = empresa.Id
            };
            var noticiaCriada = operatinal.CriarNoticia(noticia);

            Assert.Equal("teste.com", noticiaCriada.Url);
        }
        
        [Fact]
        public void CreateNewsAnalysisDataService()
        {
            var operatinal = new OperationalDataService();
            var empresa = operatinal.GetEmpresa("TTEX");
            if (empresa == null)
            {
                var novaEmpresa = new Empresa()
                {
                    Nome = "Teste - Extracao",
                    Ativo = true,
                    Codigo = "TTEX"
                };
                empresa = operatinal.CriarEmpresa(novaEmpresa);
            }

            var noticiaAnalise = new NoticiaAnalise()
            {
                Url = "teste.com",
                Titulo = "TESTE - EX",
                Texto = "TESTE - CORPO",
                Sentimento = 0,
                Tickers = new[] {"TT", "TESTE"},
            };
            
            var noticiaCriada = operatinal.CriarNoticiaAnalise(noticiaAnalise);

            Assert.Equal("teste.com", noticiaCriada.Url);
        }
        
        [Fact]
        public void CreateJuncaoDataService()
        {
            var operatinal = new OperationalDataService();
            var empresa = operatinal.GetEmpresa("TTEX");
            if (empresa == null)
            {
                var novaEmpresa = new Empresa()
                {
                    Nome = "Teste - Extracao",
                    Ativo = true,
                    Codigo = "TTEX"
                };
                empresa = operatinal.CriarEmpresa(novaEmpresa);
            }
            var juncao = new Juncoes()
            {
                DataFim = DateTime.Now,
                DataInicio = DateTime.Now.AddDays(-3),
                EmpresaId = empresa.Id
            };
            var juncaoCriada = operatinal.CriarJuncao(juncao);

            Assert.Equal(empresa.Id, juncaoCriada.EmpresaId);
        }
        
                
        [Fact]
        public void BuscarNoticiasStockNew()
        {
            var payLoad = new Payload()
            {
                Sigla = "AAPL",
                DataFinal = "11122021",
                DataInicial = "10112021",
            };
            var noticias = new OperationalNews().BuscarNoticiasStockNews(payLoad);

            Assert.NotNull(noticias.Data);
        }
        
        [Fact]
        public void DeleteCompanyDataService()
        {
            var operational = new OperationalDataService();
            var delete = operational.DeletarEmpresa("TTEX");

            Assert.Equal("Empresa: Teste - Extracao removida com sucesso!", delete);
        }

        [Fact]
        public void TestandoHandler()
        {
            var function = new Function();
            var context = new TestLambdaContext();
            var payLoad = new Payload()
            {
                Sigla = "AAPL",
                DataFinal = "11/12/2021",
                DataInicial = "10/11/2021",
            };
            var retorno = function.FunctionHandler(payLoad, context);
            Assert.Equal("Processo concluído", retorno);
        }
    }
}