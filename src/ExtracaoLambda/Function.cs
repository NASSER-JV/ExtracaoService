using System;
using System.Collections.Generic;
using System.Globalization;
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
            input.DataFinal = DateTime.ParseExact(input.DataFinal, "dd'/'MM'/'yyyy", CultureInfo.InvariantCulture)
                .ToString("MM'/'dd'/'yyyy");
            input.DataInicial = DateTime.ParseExact(input.DataInicial, "dd'/'MM'/'yyyy", CultureInfo.InvariantCulture)
                .ToString("MM'/'dd'/'yyyy");
            if (input.NewsAnalysis)
            {
                Operational.FunctionGetNewsAnalysis(input);
            }
            else
            {
                Operational.FunctionGetNews(input);
            }
                

            return "Processo concluído";
        }

    }
}
