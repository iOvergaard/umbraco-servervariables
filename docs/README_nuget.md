# Server Variables for Umbraco

This package reintroduces server variables from C# to JavaScript in Umbraco 15+ with a twist.

**NB!**: Do not include any secrets in the server variables as they will be exposed to the public.

In short, it is a simple way to expose server variables to your Umbraco Backoffice and/or Frontend. It allows you to add server variables to your site without having to write any or only a little code.

The variables are added through appsettings.json or through the IServerVariablesService interface.

The twist is that the variables are made available through the importmap in the browser. This way, you can import the variables where you need them without having to rely on any global JavaScript objects.

## Configuration through appsettings.json

Add the following to your `appsettings.json`:

```json
{
  "ServerVariables": {
    "Namespace": "vars",
    "CacheHeader": "no-cache, no-store, must-revalidate",
    "Values": {
      "MyVariable": "MyValue"
    }
  }
}
```

| Config      | Description                                                                                                                                                                                                                        |
| ----------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Namespace   | This is the namespace that the variables will be available under in the frontend through the importmap. For example, if you set this to `myVars`, you will import the variables like this: `import { MyVariable } from 'myVars';`. |
| CacheHeader | This is the cache header value that will be set on the importmap. This is useful if you want to cache the importmap. By default, it is set to `no-cache, no-store, must-revalidate`.                                               |
| Values      | This is an object with the variables you want to add. The key is the name of the variable, and the value is the value of the variable.                                                                                             |

## Configuration through IServerVariablesService

Add the following in a composer:

```csharp
using ServerVariables.Services;
using Umbraco.Cms.Core.Composing;

namespace YourNamespace;

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
```

## Usage in the Backoffice

In any Backoffice component, you can now import the server variables where you need them:

```javascript
import { MyVariable } from '@server-variables';

console.log('MyVariable', MyVariable);
```

This will log `MyValue` to the console.

## Usage in the Frontend

In any frontend component, you can now import the server variables where you need them by accessing the virtual path directly:

```javascript
import { MyVariable } from '/App_Plugins/ServerVariables/index.js';

console.log('MyVariable', MyVariable);
```

This will log `MyValue` to the console.

## Acknowledgements

<a href="https://www.flaticon.com/free-icons/javascript" title="javascript icons">Javascript icons created by Graphix's Art - Flaticon</a>
