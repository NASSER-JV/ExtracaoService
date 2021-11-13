using System;
using System.Text.Json.Serialization;

namespace ExtracaoLambda.Data.Entities
{
    public class Noticia
    {
        public string Url { get; set; }
        
        public string Titulo { get; set; }
        
        public string Corpo { get; set; }
        
        public DateTime Date { get; set; }

        [JsonPropertyName("empresa_id")]
        public int EmpresaId { get; set; }

    }
}