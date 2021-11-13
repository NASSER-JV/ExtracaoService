using System;
using System.Text.Json.Serialization;
using ExtracaoLambda.Data.Enums;

namespace ExtracaoLambda.Data.Entities
{
    public class Noticia
    {
        [JsonPropertyName("url")]
        public string Url { get; set; }
        
        [JsonPropertyName("titulo")]
        public string Titulo { get; set; }
        
        [JsonPropertyName("corpo")]
        public string Corpo { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }
        
        [JsonPropertyName("analise")]
        public SentimentalEnum Analise { get; set; }
        
        [JsonPropertyName("empresa_id")]
        public int EmpresaId { get; set; }

    }
}