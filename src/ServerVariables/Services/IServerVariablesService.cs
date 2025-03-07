using Microsoft.Extensions.Configuration;

namespace ServerVariables.Services;

public interface IServerVariablesService
{
    /// <summary>
    ///     Gets the server variables for the appSettings.js file.
    /// </summary>
    /// <returns></returns>
    public Dictionary<string, string?> GetAppSettings();
}
