using System.Net.Http.Json;
using ExtracaoService.Infrastructure.Adapters.FinancialModellingPrep.Dtos;
using ExtracaoService.Infrastructure.Configurations;

namespace ExtracaoService.Infrastructure.Adapters.FinancialModellingPrep;

public class FinancialModellingPrepService
{
    public FinancialModellingPrepService()
    {
        Client = new HttpClient();
        Client.BaseAddress = new Uri("https://financialmodelingprep.com");
    }

    private HttpClient Client { get; }
    private string ApiKey { get; } = Configuration.Config["Settings:FinnancialApiKey"];


    public async Task<Company> SearchCompany(string sigla)
    {
        var response = await Client.GetFromJsonAsync<List<Company>>($"api/v3/profile/{sigla}?apikey={ApiKey}");

        return response?.FirstOrDefault() ?? throw new NullReferenceException();
    }
}