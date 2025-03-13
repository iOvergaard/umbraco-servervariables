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

        var totalNumberOfItems = items.Count;

        // Use filter to filter by a custom field
        if (!string.IsNullOrWhiteSpace(filter))
        {
            items = items
                .Where(x => x.Key.Contains(filter, StringComparison.OrdinalIgnoreCase) ||
                            x.Value.ToString().Contains(filter, StringComparison.OrdinalIgnoreCase) ||
                            x.Section.Contains(filter, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        // Use orderBy to order by a custom field
        IOrderedEnumerable<ServerVariablesCollectionResponseModel> orderedList = orderBy switch
        {
            "key" when orderDirection == Direction.Ascending => items.OrderBy(x => x.Key),
            "key" when orderDirection == Direction.Descending => items.OrderByDescending(x => x.Key),
            "value" when orderDirection == Direction.Ascending => items.OrderBy(x => x.Value),
            "value" when orderDirection == Direction.Descending => items.OrderByDescending(x => x.Value),
            "section" when orderDirection == Direction.Ascending => items.OrderBy(x => x.Section),
            "section" when orderDirection == Direction.Descending => items.OrderByDescending(x => x.Section),
            _ => throw new ArgumentOutOfRangeException(nameof(orderBy), orderBy, "Invalid order by field")
        };

        items = orderedList.ToList();

        items = items
            .Skip(skip)
            .Take(take)
            .ToList();

        return CollectionResult(items, totalNumberOfItems);
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
