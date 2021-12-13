using System.Text.Json.Serialization;
using ExtracaoService.Infrastructure.Adapters.DataService.Dtos.Enums;
using ExtracaoService.Infrastructure.Converters;

namespace ExtracaoService.Infrastructure.Adapters.DataService.Dtos;

public class Tarefa
{
    public int Id { get; set; }
    public TipoTarefa Tipo { get; set; }
    public string[] Tickers { get; set; }
    [JsonConverter(typeof(BrazilDateConverter))]
    public DateTime DataInicial { get; set; }
    [JsonConverter(typeof(BrazilDateConverter))]
    public DateTime DataFinal { get; set; }
    public bool Finalizado { get; set; }
 }