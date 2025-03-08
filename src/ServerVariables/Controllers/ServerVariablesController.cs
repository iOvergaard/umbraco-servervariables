using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServerVariables.Services;
using StackExchange.Profiling.Internal;

namespace ServerVariables.Controllers;

[Route("/App_Plugins/ServerVariables/")]
public class ServerVariablesController(
    IServerVariablesService serverVariablesService,
    IOptionsMonitor<ServerVariablesOptions> options) : ControllerBase
{
    [HttpGet("AppSettings.js")]
    public IActionResult AppSettings()
    {
        // Check for the presence of the configuration section
        Dictionary<string, string?> serverVariables = serverVariablesService.GetAppSettings();

        Response.Headers.ContentType = "application/javascript";
        Response.Headers.CacheControl = options.CurrentValue.CacheHeader;
        return Content($"export default {serverVariables.ToJson()}");
    }

    [HttpGet("{sectionName}.js")]
    public IActionResult Index(string sectionName)
    {
        Response.Headers.ContentType = "application/javascript";
        Response.Headers.CacheControl = options.CurrentValue.CacheHeader;

        Dictionary<string, string?> serverVariables = serverVariablesService.GetSection(sectionName);
        return Content($"export default {serverVariables.ToJson()};");
    }
}
