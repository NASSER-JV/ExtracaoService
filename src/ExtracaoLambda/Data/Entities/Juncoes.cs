using System;
using System.Text.Json.Serialization;

namespace ExtracaoLambda.Data.Entities
{
    public class Juncoes
    {
        public int id { get; set; }

        public DateTime DataInicio { get; set; }
        
        public DateTime DataFim { get; set; }

        [JsonPropertyName("empresa_id")]
        public int EmpresaId { get; set; }
    }
}