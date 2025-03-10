using Microsoft.Extensions.Configuration;

namespace ServerVariables.Services;

public class ServerVariablesService(IConfiguration configuration) : IServerVariablesService
{
    private readonly Dictionary<string, Dictionary<string, string?>> _serverVariables = new();

    public Dictionary<string, string?> GetAppSettings()
    {
        IConfigurationSection serverVariablesSection = configuration.GetSection(Constants.ServerVariablesValues);
        return serverVariablesSection
            .AsEnumerable()
            .Where(x => !string.IsNullOrEmpty(x.Value))
            .ToDictionary(x =>
                    x.Key.Replace($"{Constants.ServerVariablesValues}:", string.Empty),
                x => x.Value);
    }

    public void SetVariable(string key, string value, string sectionName)
    {
        // Check if the section exists
        if (!_serverVariables.TryGetValue(sectionName, out Dictionary<string, string?>? section))
        {
            section = new Dictionary<string, string?>();
            _serverVariables.Add(sectionName, section);
        }

        // Check if the key exists
        if (!section.TryAdd(key, value))
        {
            section[key] = value;
        }
    }

    public void SetVariable(string key, string value)
    {
        SetVariable(key, value, "index");
    }

    public Dictionary<string, string?> GetSection(string section)
    {
        return !_serverVariables.TryGetValue(section, out Dictionary<string, string?>? sectionVariables)
            ? new Dictionary<string, string?>()
            : sectionVariables;
    }

    public bool SetSection(string sectionName, Dictionary<string, string?> values)
    {
        return _serverVariables.TryAdd(sectionName, values);
    }
}
