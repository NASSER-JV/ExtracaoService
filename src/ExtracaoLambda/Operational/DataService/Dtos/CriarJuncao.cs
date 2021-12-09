using System;
using System.Text.Json.Serialization;
using ExtracaoLambda.Utilities;

namespace ExtracaoLambda.Operational.DataService.Dtos
{
    public class CriarJuncao
    {
        [JsonConverter(typeof(BrazilDateConverter))]
        public DateTime DataInicio { get; set; }

        [JsonConverter(typeof(BrazilDateConverter))]
        public DateTime DataFinal { get; set; }

        public int EmpresaId { get; set; }
    }

    public class Juncao
    {
        public int Id { get; set; }

        [JsonConverter(typeof(BrazilDateConverter))]
        public DateTime DataInicio { get; set; }

        [JsonConverter(typeof(BrazilDateConverter))]
        public DateTime DataFim { get; set; }

        public int EmpresaId { get; set; }
    }
}