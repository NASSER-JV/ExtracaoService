using Microsoft.Extensions.Configuration;

namespace ExtracaoLambda.Data.Utilities
{
    public class Common
    {
        public static IConfigurationRoot Config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

    }
}