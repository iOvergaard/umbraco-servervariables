using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerVariables.Services;

namespace ServerVariables.ApiControllers;

[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Variables")]
public class ServerVariablesSectionApiController(IServerVariablesService serverVariablesService) : ServerVariablesApiControllerBase
{
    [HttpGet("section/{section}")]
    [ProducesResponseType(typeof(Dictionary<string, dynamic>), StatusCodes.Status200OK, "application/json")]
    public IActionResult Section(CancellationToken cancellationToken, string section = "index")
    {
        Dictionary<string, dynamic> serverVariables = serverVariablesService.GetSection(section);
        return Ok(serverVariables);
    }
}
