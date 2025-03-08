using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
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

    private class ServerVariablesPackage : IPackageManifestReader
    {
        public Task<IEnumerable<PackageManifest>> ReadPackageManifestsAsync()
        {
            // get version from assembly
            Assembly assembly = typeof(UmbracoPackage).Assembly;
            Version? version = assembly.GetName().Version;
            PackageManifest packageManifest = new()
            {
                Id = "Umbraco.Community.ServerVariables",
                Name = "Umbraco.Community.ServerVariables",
                AllowTelemetry = true,
                Version = version?.ToString(),
                Extensions = [],
                Importmap = new PackageManifestImportmap
                {
                    Imports = new Dictionary<string, string>
                    {
                        { "vars/", "/App_Plugins/ServerVariables/" },
                        { "vars", "/App_Plugins/ServerVariables/index.js" }
                    }
                }
            };

            IEnumerable<PackageManifest> manifests = new List<PackageManifest> { packageManifest };
            return Task.FromResult(manifests);
        }
    }
}
