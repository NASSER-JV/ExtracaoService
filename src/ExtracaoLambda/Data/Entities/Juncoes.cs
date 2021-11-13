using System;
using System.Text.Json.Serialization;

namespace ExtracaoLambda.Data.Entities
{
    public class Juncoes
    {
        public int Id { get; set; }

        public DateTime DataInicio { get; set; }
        
        public DateTime DataFim { get; set; }

        [JsonPropertyName("empresa_id")]
        public int EmpresaId { get; set; }
    }
    
    public class JuncoesDto
    {
        public int Id { get; set; }

        public DateTime DataInicio { get; set; }
        
        public DateTime DataFim { get; set; }

        [JsonPropertyName("empresa")]
        public int EmpresaId { get; set; }
    }
}