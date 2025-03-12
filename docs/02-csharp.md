# Usage with IServerVariablesService

You can add variables to the importmap using the options pattern or the `IServerVariablesService` in your project. This is useful if you want to add variables that are dynamic, or if you want to add variables that differ between environments.

## Configuration with the options pattern

Add the following in a composer to configure the server variables:

```csharp
using Microsoft.Extensions.DependencyInjection;
using ServerVariables.Services;
    
namespace YourNamespace
{
    public class ServerVariablesComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.Services.AddOptions<ServerVariablesOptions>()
                .Configure((options) =>
                {
                    options.Namespace = "vars";
                    options.Values = new Dictionary<string, dynamic> { { "MyVariable", "MyValue" } };
                });
        }
    }
}
```

## Configuration with service injection

Add the following in a composer to inject the `IServerVariablesService` into a component:

```csharp
using ServerVariables.Services;
using Umbraco.Cms.Core.Composing;

namespace YourNamespace
{
    public class ServerVariablesComposer : ComponentComposer<ServerVariablesComponent>
    {
    }

    public class ServerVariablesComponent(IServerVariablesService serverVariablesService) : IAsyncComponent
    {
        public Task InitializeAsync(bool isRestarting, CancellationToken cancellationToken)
        {
            serverVariablesService.SetVariable("MyVariable", "MyValue");

            return Task.CompletedTask;
        }

        public Task TerminateAsync(bool isRestarting, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
```

Note that if you add variables after the application has started, you need to restart the application to see the changes.

If you add variables through the service after the pipeline has run, you can use the `IServerVariablesService` to add variables dynamically. However, be careful to re-import the variables in the frontend after adding them to the service.

## Usage in the Backoffice

In any Backoffice component, you can now import the server variables where you need them:

```javascript
import { MyVariable } from 'vars';

console.log(MyVariable); // MyValue
```

## Sections

Additionally, you can add sections to the importmap using the `IServerVariablesService`:

```csharp
serverVariablesService.SetSection("MySection", new Dictionary<string, string>
{
    { "MyVariable", "MyValue" }
});
```

The section will be available in the frontend under the namespace you set in the configuration:

```javascript
import { MyVariable } from 'vars/MySection.js';

console.log(MyVariable); // MyValue
```
