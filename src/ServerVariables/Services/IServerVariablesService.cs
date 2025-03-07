using Microsoft.Extensions.Configuration;

namespace ServerVariables.Services;

public interface IServerVariablesService
{
    /// <summary>
    ///     Gets the server variables for the appSettings.js file.
    /// </summary>
    /// <returns></returns>
    public Dictionary<string, string?> GetAppSettings();

    /// <summary>
    ///     Sets a server variable
    /// </summary>
    /// <param name="key">The key</param>
    /// <param name="value">The value</param>
    /// <param name="sectionName">The section, for example a section "brand" would be imported from "vars/brand.js".</param>
    public void SetVariable(string key, string value, string sectionName);

    /// <summary>
    ///     Sets a server variable
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void SetVariable(string key, string value);

    /// <summary>
    ///     Gets a section of server variables
    /// </summary>
    /// <param name="section">The name of the section</param>
    /// <returns></returns>
    public Dictionary<string, string?> GetSection(string section);
}
