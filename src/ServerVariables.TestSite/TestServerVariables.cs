using ServerVariables.Services;
using Umbraco.Cms.Core.Composing;

namespace ServerVariables.TestSite;

public class ServerVariablesComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.Services.AddOptions<ServerVariablesOptions>()
            .Configure((options) =>
            {
                options.Namespace = "vars";
                options.Values = new Dictionary<string, dynamic> { { "hello", "from options" }, { "isEnabled", true } };
            });
    }
}

public class TestServerVariablesComposer : ComponentComposer<TestServerVariablesComponent>
{
}

public class TestServerVariablesComponent(IServerVariablesService serverVariablesService) : IAsyncComponent
{
    public Task InitializeAsync(bool isRestarting, CancellationToken cancellationToken)
    {
        serverVariablesService.SetVariable("apiKey", "123456789");
        serverVariablesService.SetVariable("apiUrl", "https://api.example.com");

        serverVariablesService.SetVariable("brand", "My Brand", "brand");

        return Task.CompletedTask;
    }

    public Task TerminateAsync(bool isRestarting, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
