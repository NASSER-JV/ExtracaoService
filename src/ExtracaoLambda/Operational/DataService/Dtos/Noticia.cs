using System;
using System.Text.Json.Serialization;
using ExtracaoLambda.Utilities;

namespace ExtracaoLambda.Operational.DataService.Dtos
{
    public class Noticia
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Titulo { get; set; }
        public string Texto { get; set; }
        public int Sentimento { get; set; }

        [JsonConverter(typeof(BrazilDateConverter))]
        public DateTime Data { get; set; }

        public string EventoId { get; set; }
        public int EmpresaId { get; set; }
    }
}