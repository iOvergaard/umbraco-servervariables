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
            var version = assembly.GetName().Version?.ToString() ?? "0.0.0";

            PackageManifest publicPackageManifest = new()
            {
                Id = "Umbraco.Community.ServerVariables",
                Name = "Server Variables",
                AllowTelemetry = true,
                Version = version,
                Extensions = [],
                AllowPublicAccess = true,
                Importmap = new PackageManifestImportmap
                {
                    Imports = new Dictionary<string, string>
                    {
                        { $"{options.Value.Namespace}/", "/App_Plugins/ServerVariables/" },
                        { options.Value.Namespace, $"/App_Plugins/ServerVariables/index.js?v={version}" }
                    }
                }
            };

            PackageManifest privatePackageManifest = new()
            {
                Id = "Umbraco.Community.ServerVariables.Client",
                Name = "Server Variables Client",
                AllowTelemetry = false,
                Version = version,
                Extensions = [
                    BundleManifest()
                ],
                AllowPublicAccess = false
            };

            IEnumerable<PackageManifest> manifests = new List<PackageManifest> { publicPackageManifest, privatePackageManifest };
            return Task.FromResult(manifests);
        }

        private static Dictionary<string, dynamic> BundleManifest() => new()
        {
            ["type"] = "bundle",
            ["name"] = "Server Variables Bundle",
            ["alias"] = "ServerVariables.Bundle",
            ["js"] = "/App_Plugins/ServerVariables/client/manifests.js"
        };
    }
}
