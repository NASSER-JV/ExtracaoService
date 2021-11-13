using System.Text.Json.Serialization;

namespace ExtracaoLambda.Data.Entities
{
    public class Empresa
    {
        public int Id { get; set; }
        
        public string Nome { get; set; }
        
        public string Codigo { get; set; }
        
        public bool Ativo { get; set; }
    }
}