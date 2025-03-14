using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Api.Common.Attributes;
using Umbraco.Cms.Api.Common.Builders;
using Umbraco.Cms.Web.Common.Authorization;
using Umbraco.Cms.Web.Common.Routing;

namespace ServerVariables.ApiControllers;

[ApiController]
[BackOfficeRoute("servervariables/api/v{version:apiVersion}")]
[Authorize(Policy = AuthorizationPolicies.BackOfficeAccess)]
[MapToApi(Constants.ApiName)]
public class ServerVariablesApiControllerBase : ControllerBase
{
    protected static IActionResult OperationStatusResult<TEnum>(TEnum status, Func<ProblemDetailsBuilder, IActionResult> result)
        where TEnum : Enum
        => result(new ProblemDetailsBuilder().WithOperationStatus(status));

    protected BadRequestObjectResult SkipTakeToPagingProblem() =>
        BadRequest(new ProblemDetails
        {
            Title = "Invalid skip/take",
            Detail = "Skip must be a multiple of take - i.e. skip = 10, take = 5",
            Status = StatusCodes.Status400BadRequest,
            Type = "Error",
        });
}
