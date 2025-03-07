using Microsoft.Extensions.Configuration;

namespace ServerVariables.Services;

public class ServerVariablesService(IConfiguration configuration) : IServerVariablesService
{
    public Dictionary<string, string?> GetAppSettings()
    {
        IConfigurationSection serverVariablesSection = configuration.GetSection("ServerVariables");
        return serverVariablesSection
            .AsEnumerable()
            .Where(x => !string.IsNullOrEmpty(x.Value))
            .ToDictionary(x =>
                    x.Key.Replace("ServerVariables:", string.Empty),
                x => x.Value);
    }
}
