using Microsoft.AspNetCore.Mvc;
using ServerVariables.Services;
using StackExchange.Profiling.Internal;

namespace ServerVariables.Controllers;

[Route("/App_Plugins/ServerVariables/")]
public class ServerVariablesController(IServerVariablesService serverVariablesService) : ControllerBase
{
    [HttpGet("AppSettings.js")]
    public IActionResult AppSettings()
    {
        // Check for the presence of the configuration section
        Dictionary<string, string?> serverVariables = serverVariablesService.GetAppSettings();

        Response.Headers.ContentType = "application/javascript";
        return Content($"export default {serverVariables.ToJson()}");
    }

    [HttpGet("{sectionName}.js")]
    public IActionResult Index(string sectionName)
    {
        // return random content and header for javascript
        Response.Headers.ContentType = "application/javascript";
        Dictionary<string, string?> serverVariables = serverVariablesService.GetSection(sectionName);
        return Content($"export default {serverVariables.ToJson()};");
    }
}
