using System;
using Xunit;
using ExtracaoLambda.Data.Entities;
using ExtracaoLambda.Data.Operational;

namespace ExtracaoLambda.Tests
{
    public class FunctionTest
    {
        [Fact]
        public void GetCompanyDataService()
        {
            var operational = new Operational();
            var empresa = operational.GetEmpresa("AAPL");

            Assert.Equal("Apple", empresa.Nome);
        }

        [Fact]
        public void CreateCompanyDataService()
        {
            var empresa = new Empresa()
            {
                Nome = "Teste - Extracao",
                Ativo = true,
                Codigo = "TTEX"
            };
            var empresaCriada = new Operational().CriarEmpresa(empresa);

            Assert.Equal("Teste - Extracao", empresaCriada.Nome);
        }

        [Fact]
        public void CreateNewsDataService()
        {
            var operatinal = new Operational();
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
        public void CreateJuncaoDataService()
        {
            var operatinal = new Operational();
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
            var operatinal = new Operational();
            var payLoad = new Payload()
            {
                Sigla = "AAPL",
                DataFinal = "11122021",
                DataInicial = "10112021",
            };
            var empresa = operatinal.GetEmpresa(payLoad.Sigla);
            var noticias = operatinal.BuscarNoticiasStockNews(payLoad);
            foreach (var news in noticias.data)
            {
                var noticia = new Noticia()
                {
                    Url = news.NewsUrl,
                    EmpresaId = empresa.Id,
                    Titulo = news.title,
                    Corpo = news.text,
                    Date = DateTime.Now,
                };
                operatinal.CriarNoticia(noticia);
            }

            Assert.NotNull(noticias.data);
        }
    }
}