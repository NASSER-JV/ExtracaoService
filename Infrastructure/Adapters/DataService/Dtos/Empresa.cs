namespace ExtracaoService.Infrastructure.Adapters.DataService.Dtos;

public class Empresa
{
    public int Id { get; set; }

    public string Nome { get; set; }

    public string Codigo { get; set; }

    public bool Ativo { get; set; }
}