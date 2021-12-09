using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.SystemTextJson;
using ExtracaoLambda.Data.Entities;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(DefaultLambdaJsonSerializer))]

namespace ExtracaoLambda
{
    public class Function
    {
        private Operational.Operational Operational { get; } = new Operational.Operational();

        /// <summary>
        /// A simple function that takes a string and does a ToUpperz
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public string FunctionHandler(Payload input, ILambdaContext context)
        {
            if (input.NewsAnalysis)
            {
                Operational.ObterNoticiasAnalise(input);
            }
            else
            {
                Operational.ObterNoticias(input);
            }


            return "Processo concluído";
        }
    }
}