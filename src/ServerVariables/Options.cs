using System.ComponentModel;
using Umbraco.Cms.Core.Configuration.Models;

namespace ServerVariables;

[UmbracoOptions(Constants.ServerVariablesSection)]
public class ServerVariablesOptions
{
    /// <summary>
    ///     Gets or sets a value indicating whether to allow public access to the server variables.
    /// </summary>
    /// <remarks>This will expose the variables on the login screen.</remarks>
    [DefaultValue(false)]
    public bool AllowPublicAccess { get; set; } = false;

    /// <summary>
    ///     Gets or sets the namespace for the server variables.
    /// </summary>
    /// <remarks>This defaults to "vars" and is the path where you import the variables from.</remarks>
    [DefaultValue("vars")]
    public string Namespace { get; set; } = "vars";

    /// <summary>
    ///     Gets or sets the cache header value for the server variables.
    /// </summary>
    /// <example></example>
    [DefaultValue("no-cache, no-store, must-revalidate")]
    public string? CacheHeader { get; set; } = "no-cache, no-store, must-revalidate";
}
