using Microsoft.Extensions.Configuration;

namespace ExtracaoService.Infrastructure.Configurations;

public static class Configuration
{
    public static readonly IConfigurationRoot Config = new ConfigurationBuilder().AddEnvironmentVariables()
        .AddJsonFile("appsettings.json", true)
        .Build();
}