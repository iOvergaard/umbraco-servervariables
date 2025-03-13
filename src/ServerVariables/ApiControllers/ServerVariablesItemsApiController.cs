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
public class ServerVariablesItemsApiController(IServerVariablesService serverVariablesService) : ServerVariablesApiControllerBase
{
    [HttpGet("items")]
    [ProducesResponseType(typeof(PagedViewModel<ServerVariablesCollectionResponseModel>), StatusCodes.Status200OK, "application/json")]
    public IActionResult Items(
        CancellationToken cancellationToken,
        string orderBy = "section",
        string? orderCulture = null,
        Direction orderDirection = Direction.Ascending,
        string? filter = null,
        int skip = 0,
        int take = 100)
    {
        List<ServerVariablesCollectionResponseModel> items = [];
        Dictionary<string, Dictionary<string, dynamic>> serverVariables = serverVariablesService.GetAll();
        foreach (var (section, variables) in serverVariables)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                break;
            }
            foreach (var (key, value) in variables)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }
                items.Add(new ServerVariablesCollectionResponseModel
                {
                    Key = key,
                    Value = value,
                    Section = section,
                });
            }
        }

        items = items
            .Skip(skip)
            .Take(take)
            .ToList();

        return CollectionResult(items, items.Count);
    }

    private OkObjectResult CollectionResult(List<ServerVariablesCollectionResponseModel> collectionResponseModels, long totalNumberOfItems)
    {
        PagedViewModel<ServerVariablesCollectionResponseModel> pageViewModel = new()
        {
            Items = collectionResponseModels,
            Total = totalNumberOfItems,
        };

        return Ok(pageViewModel);
    }
}
