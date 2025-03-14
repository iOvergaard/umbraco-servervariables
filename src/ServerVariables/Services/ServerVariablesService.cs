using Microsoft.Extensions.Options;
using ServerVariables.Models;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Services;

namespace ServerVariables.Services;

public class ServerVariablesService(IOptions<ServerVariablesOptions> options) : IServerVariablesService
{
    private readonly Dictionary<string, Dictionary<string, dynamic>> _serverVariables = new();

    public void SetVariable(string key, dynamic value, string sectionName)
    {
        // Check if the section exists
        if (!_serverVariables.TryGetValue(sectionName, out Dictionary<string, dynamic>? section))
        {
            section = new Dictionary<string, dynamic>();
            _serverVariables.Add(sectionName, section);
        }

        // Check if the key exists
        if (!section.TryAdd(key, value))
        {
            section[key] = value;
        }
    }

    public void SetVariable(string key, dynamic value)
    {
        SetVariable(key, value, "index");
    }

    public Dictionary<string, dynamic> GetSection(string section)
    {
        Dictionary<string, dynamic> result = new();
        _serverVariables.TryGetValue(section, out Dictionary<string, dynamic>? serverVariables);

        if (serverVariables != null)
        {
            foreach (var (key, value) in serverVariables)
            {
                result.TryAdd(key, value);
            }
        }

        if (!string.Equals(section, "index", StringComparison.OrdinalIgnoreCase))
        {
            return result;
        }

        // Append the app settings
        Dictionary<string, dynamic>? appSettings = options.Value.Values;

        if (appSettings == null)
        {
            return result;
        }

        foreach (var (key, value) in appSettings)
        {
            result.TryAdd(key, value);
        }

        return result;
    }

    public bool SetSection(string sectionName, Dictionary<string, dynamic> variables)
    {
        return _serverVariables.TryAdd(sectionName, variables);
    }

    public Dictionary<string, Dictionary<string, dynamic>> GetAll()
    {
        return _serverVariables;
    }

    public Attempt<IEnumerable<ServerVariablesCollectionResponseModel>?, CollectionOperationStatus> GetPagedItems(string orderBy, Direction orderDirection, string? filter, int skip, int take, CancellationToken cancellationToken, out int totalNumberOfItems)
    {
        totalNumberOfItems = 0;

        if (take < 0 || skip < 0 || skip % take != 0)
        {
            return Attempt.FailWithStatus<IEnumerable<ServerVariablesCollectionResponseModel>?, CollectionOperationStatus>(CollectionOperationStatus.InvalidSkipTake, null);
        }

        List<ServerVariablesCollectionResponseModel> items = [];
        foreach (var (section, variables) in GetAll())
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return Attempt.SucceedWithStatus<IEnumerable<ServerVariablesCollectionResponseModel>?, CollectionOperationStatus>(CollectionOperationStatus.Success, null);
            }
            foreach (var (key, value) in variables)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return Attempt.SucceedWithStatus<IEnumerable<ServerVariablesCollectionResponseModel>?, CollectionOperationStatus>(CollectionOperationStatus.Success, null);
                }
                items.Add(new ServerVariablesCollectionResponseModel
                {
                    Key = key,
                    Value = value,
                    Section = section,
                });
            }
        }

        totalNumberOfItems = items.Count;

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
            _ => throw new ArgumentOutOfRangeException(nameof(orderDirection), orderDirection, null)
        };

        items = orderedList.ToList();

        items = items
            .Skip(skip)
            .Take(take)
            .ToList();

        return Attempt.SucceedWithStatus<IEnumerable<ServerVariablesCollectionResponseModel>?, CollectionOperationStatus>(CollectionOperationStatus.Success, items);
    }
}
