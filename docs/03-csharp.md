# Usage with IServerVariablesService

You can add variables to the importmap using the `IServerVariablesService` in your project. This is useful if you want to add variables that are dynamic, or if you want to add variables that differ between environments.

## Configuration

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
