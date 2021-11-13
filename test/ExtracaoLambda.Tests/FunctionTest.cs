using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;

using ExtracaoLambda;
using ExtracaoLambda.Data.Entities;

namespace ExtracaoLambda.Tests
{
    public class FunctionTest
    {
        [Fact]
        public void TestToUpperFunction()
        {

            // Invoke the lambda function and confirm the string was upper cased.
            var function = new Function();
            var context = new TestLambdaContext();
            var payload = new Payload()
            {
                DataFinal = "11122021",
                DataInicial = "10122021", 
                Sigla = "AAPL",
            };
            var upperCase = function.FunctionHandler(payload, context);

            Assert.Equal("AAPL, 11122021, 10122021", upperCase);
        }
    }
}
