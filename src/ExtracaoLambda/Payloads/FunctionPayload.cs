using System;
using System.Text.Json.Serialization;
using ExtracaoLambda.Utilities;

namespace ExtracaoLambda.Data.Entities
{
    public class Payload
    {
        public string Sigla { get; set; }

        [JsonConverter(typeof(BrazilDateConverter))]
        public DateTime DataInicial { get; set; }

        [JsonConverter(typeof(BrazilDateConverter))]
        public DateTime DataFinal { get; set; }

        public string[] Tickers { get; set; }

        public bool NewsAnalysis { get; set; }
    }
}