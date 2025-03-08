using System.ComponentModel;
using Umbraco.Cms.Core.Configuration.Models;

namespace ServerVariables;

[UmbracoOptions(Constants.ServerVariablesSection)]
public class ServerVariablesOptions
{
    /// <summary>
    ///     Gets the namespace for the server variables.
    /// </summary>
    /// <remarks>This defaults to "vars" and is the path where you import the variables from.</remarks>
    [DefaultValue("vars")]
    public string Namespace { get; set; } = "vars";

    /// <summary>
    ///     Gets the cache header value for the server variables.
    /// </summary>
    /// <example></example>
    [DefaultValue("no-cache, no-store, must-revalidate")]
    public string? CacheHeader { get; set; } = "no-cache, no-store, must-revalidate";
}
