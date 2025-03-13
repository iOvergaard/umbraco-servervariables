using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerVariables.Services;

namespace ServerVariables.ApiControllers;

[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Variables")]
public class ServerVariablesAllApiController(IServerVariablesService serverVariablesService) : ServerVariablesApiControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(Dictionary<string, dynamic>), StatusCodes.Status200OK, "application/json")]
    public IActionResult All(CancellationToken cancellationToken)
    {
        Dictionary<string, Dictionary<string, dynamic>> serverVariables = serverVariablesService.GetAll();
        return Ok(serverVariables);
    }
}
