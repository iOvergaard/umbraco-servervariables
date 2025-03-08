using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Manifest;
using Umbraco.Cms.Infrastructure.Manifest;

namespace ServerVariables.Package;

internal sealed class UmbracoPackage : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.Services.AddSingleton<IPackageManifestReader, ServerVariablesPackage>();
    }

    private class ServerVariablesPackage(IOptions<ServerVariablesOptions> options) : IPackageManifestReader
    {
        public Task<IEnumerable<PackageManifest>> ReadPackageManifestsAsync()
        {
            // get info from assembly
            Assembly assembly = typeof(UmbracoPackage).Assembly;

            PackageManifest packageManifest = new()
            {
                Id = "Umbraco.Community.ServerVariables",
                Name = assembly.GetName().FullName,
                AllowTelemetry = true,
                Version = assembly.GetName().Version?.ToString(),
                Extensions = [],
                AllowPublicAccess = true,
                Importmap = new PackageManifestImportmap
                {
                    Imports = new Dictionary<string, string>
                    {
                        { $"{options.Value.Namespace}/", "/App_Plugins/ServerVariables/" },
                        { options.Value.Namespace, "/App_Plugins/ServerVariables/index.js" }
                    }
                }
            };

            IEnumerable<PackageManifest> manifests = new List<PackageManifest> { packageManifest };
            return Task.FromResult(manifests);
        }
    }
}
