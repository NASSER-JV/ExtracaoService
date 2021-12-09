using Microsoft.Extensions.Configuration;

namespace ExtracaoLambda.Data.Utilities
{
    public class Common
    {
        public static readonly IConfigurationRoot Config = new ConfigurationBuilder().AddJsonFile("appsettings.json")
            .Build();
    }
}