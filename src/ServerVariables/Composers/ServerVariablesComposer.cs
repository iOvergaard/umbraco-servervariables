using Microsoft.Extensions.DependencyInjection;
using ServerVariables.Services;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Extensions;

namespace ServerVariables.Composers;

public class ServerVariablesApiComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.Services.Configure<ServerVariablesOptions>(builder.Config.GetSection(Constants.ServerVariablesSection));
        builder.Services.AddUnique<IServerVariablesService, ServerVariablesService>();
    }
}
