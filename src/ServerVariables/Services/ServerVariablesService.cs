using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace ServerVariables.Services;

public class ServerVariablesService(IConfiguration configuration, IOptionsMonitor<ServerVariablesOptions> options) : IServerVariablesService
{
    private readonly Dictionary<string, Dictionary<string, dynamic>> _serverVariables = new();

    public void SetVariable(string key, string value, string sectionName)
    {
        // Check if the section exists
        if (!_serverVariables.TryGetValue(sectionName, out Dictionary<string, dynamic>? section))
        {
            section = new Dictionary<string, dynamic>();
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

    public Dictionary<string, dynamic> GetSection(string section)
    {
        Dictionary<string, dynamic> result = new();
        _serverVariables.TryGetValue(section, out Dictionary<string, dynamic>? serverVariables);

        if (serverVariables != null)
        {
            foreach (var (key, value) in serverVariables)
            {
                result.TryAdd(key, value);
            }
        }

        if (!string.Equals(section, "index", StringComparison.OrdinalIgnoreCase))
        {
            return result;
        }

        // Append the app settings
        Dictionary<string, dynamic>? appSettings = options.CurrentValue.Variables;

        if (appSettings == null)
        {
            return result;
        }

        foreach (var (key, value) in appSettings)
        {
            result.TryAdd(key, value);
        }

        return result;
    }

    public bool SetSection(string sectionName, Dictionary<string, dynamic> values)
    {
        return _serverVariables.TryAdd(sectionName, values);
    }
}
