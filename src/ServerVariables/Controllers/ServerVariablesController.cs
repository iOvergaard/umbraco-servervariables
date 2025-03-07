using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StackExchange.Profiling.Internal;

namespace ServerVariables.Controllers;

[Route("/App_Plugins/ServerVariables/")]
public class ServerVariablesController(IConfiguration configuration) : ControllerBase
{
    [HttpGet("AppSettings.js")]
    public IActionResult AppSettings()
    {
        // Check for the presence of the configuration section
        IConfigurationSection serverVariablesSection = configuration.GetSection("ServerVariables");
        Dictionary<string, string?> serverVariables = serverVariablesSection.AsEnumerable().Where(x => !string.IsNullOrEmpty(x.Value)).ToDictionary(x => x.Key.Replace("ServerVariables:", string.Empty), x => x.Value);

        Response.Headers.ContentType = "application/javascript";
        return Content($"export default {serverVariables.ToJson()}");
    }

    [HttpGet("{filename}.js")]
    public IActionResult Index(string filename)
    {
        // return random content and header for javascript
        Response.Headers.ContentType = "application/javascript";
        return Content("export default { 'apiKey': '123456789', 'apiUrl': 'https://api.example.com', 'fromServer': '" + filename + "' };");
    }
}
