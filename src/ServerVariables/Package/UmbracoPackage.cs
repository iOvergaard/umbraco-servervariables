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

            PackageManifest packageManifest = new()
            {
                Id = "Umbraco.Community.ServerVariables",
                Name = assembly.GetName().FullName,
                AllowTelemetry = true,
                Version = version,
                Extensions = [
                    SettingsMenuItem(),
                    //Workspace(),
                    //WorkspaceView()
                ],
                AllowPublicAccess = false,
                Importmap = new PackageManifestImportmap
                {
                    Imports = new Dictionary<string, string>
                    {
                        { $"{options.Value.Namespace}/", "/App_Plugins/ServerVariables/" },
                        { options.Value.Namespace, $"/App_Plugins/ServerVariables/index.js?v={version}" }
                    }
                }
            };

            IEnumerable<PackageManifest> manifests = new List<PackageManifest> { packageManifest };
            return Task.FromResult(manifests);
        }

        private static Dictionary<string, dynamic> SettingsMenuItem() => new()
        {
            ["type"] = "menuItem",
            ["name"] = "Server Variables Menu Root Item",
            ["alias"] = "ServerVariables.Menu.RootItem",
            ["meta"] = new Dictionary<string, dynamic>
            {
                ["label"] = "Server Variables",
                ["icon"] = "icon-wand",
                ["entityType"] = "servervariables-root",
                ["menus"] = new List<string>{"Umb.Menu.AdvancedSettings"}
            }
        };

        private static Dictionary<string, dynamic> Workspace() => throw new NotImplementedException();

        private static Dictionary<string, dynamic> WorkspaceView() => throw new NotImplementedException();
    }
}
