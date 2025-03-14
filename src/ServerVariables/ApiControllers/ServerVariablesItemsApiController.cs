using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerVariables.Models;
using ServerVariables.Services;
using Umbraco.Cms.Api.Common.ViewModels.Pagination;
using Umbraco.Cms.Core;

namespace ServerVariables.ApiControllers;

[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Variables")]
public class ServerVariablesItemsApiController(IServerVariablesService serverVariablesService)
    : ServerVariablesApiControllerBase
{
    [HttpGet("items")]
    [ProducesResponseType(typeof(PagedViewModel<ServerVariablesCollectionResponseModel>), StatusCodes.Status200OK,
        "application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest,
        "application/json")]
    public IActionResult Items(
        CancellationToken cancellationToken,
        string orderBy = "section",
        Direction orderDirection = Direction.Ascending,
        string? filter = null,
        int skip = 0,
        int take = 100)
    {
        Attempt<IEnumerable<ServerVariablesCollectionResponseModel>?, CollectionOperationStatus> result =
            serverVariablesService.GetPagedItems(orderBy, orderDirection, filter, skip, take, cancellationToken,
                out var totalNumberOfItems);

        if (result.Success is false)
        {
            return result.Status switch
            {
                CollectionOperationStatus.Error => OperationStatusResult(result.Status, builder =>
                    new BadRequestObjectResult(builder.WithOperationStatus(result.Status)
                        .WithTitle("Failed to get server variables")
                        .Build())),
                CollectionOperationStatus.InvalidSkipTake => SkipTakeToPagingProblem(),
                _ => new ObjectResult("An error occurred")
            };
        }

        return CollectionResult(result.Result!, totalNumberOfItems);
    }

    private OkObjectResult CollectionResult(
        IEnumerable<ServerVariablesCollectionResponseModel> collectionResponseModels, long totalNumberOfItems)
    {
        PagedViewModel<ServerVariablesCollectionResponseModel> pageViewModel = new()
        {
            Items = collectionResponseModels, Total = totalNumberOfItems,
        };

        return Ok(pageViewModel);
    }
}
