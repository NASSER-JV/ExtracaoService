using System.Text.Json.Serialization;

namespace ExtracaoLambda.Data.Entities
{
    public class Empresa
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nome")] 
        public string Nome { get; set; }

        [JsonPropertyName("codigo")]
        public string Codigo { get; set; }
        
        [JsonPropertyName("ativo")]
        public bool Ativo { get; set; }
    }
}