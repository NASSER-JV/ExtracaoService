using System;
using System.Text.Json.Serialization;
using ExtracaoLambda.Utilities;

namespace ExtracaoLambda.Operational.DataService.Dtos
{
    public class NoticiaAnalise
    {
        public string Url { get; set; }
        public string Titulo { get; set; }
        public string Texto { get; set; }

        [JsonConverter(typeof(BrazilDateConverter))]
        public DateTime Data { get; set; }

        public int Sentimento { get; set; }
        public string[] Tickers { get; set; }
    }
}