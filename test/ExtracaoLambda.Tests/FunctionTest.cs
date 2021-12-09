using System;
using Amazon.Lambda.TestUtilities;
using ExtracaoLambda.Data.Entities;
using Xunit;

namespace ExtracaoLambda.Tests
{
    public class FunctionTest
    {
        [Fact]
        public void TestandoHandlerCriarNoticiasAnalysis()
        {
            var function = new Function();
            var context = new TestLambdaContext();
            var payLoad = new Payload()
            {
                Tickers = new[]
                    { "AAPL", "MSFT", "AMZN", "TSLA", "FB", "GOOG", "NVDA", "AMD", "CSCO", "INTC", "SONY", "IBM" },
                DataFinal = new DateTime(2021, 11, 30),
                DataInicial = new DateTime(2021, 05, 01),
                NewsAnalysis = true
            };
            var retorno = function.FunctionHandler(payLoad, context);
            Assert.Equal("Processo concluído", retorno);
        }

        [Fact]
        public void TestandoHandlerCriarNoticias()
        {
            var function = new Function();
            var context = new TestLambdaContext();
            var payLoad = new Payload()
            {
                Sigla = "FB",
                DataFinal = new DateTime(2021, 11, 30),
                DataInicial = new DateTime(2021, 11, 25),
                NewsAnalysis = false
            };
            var retorno = function.FunctionHandler(payLoad, context);
            Assert.Equal("Processo concluído", retorno);
        }
    }
}