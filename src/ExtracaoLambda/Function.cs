using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using ExtracaoLambda.Data.Entities;

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
            return $"{input.sigla}, {input.dataFinal}, {input.dataInicial}";
        }

    }
}
