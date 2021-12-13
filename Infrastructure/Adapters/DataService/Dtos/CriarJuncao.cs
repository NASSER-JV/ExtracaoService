using System.Text.Json.Serialization;
using ExtracaoService.Infrastructure.Converters;

namespace ExtracaoService.Infrastructure.Adapters.DataService.Dtos;

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