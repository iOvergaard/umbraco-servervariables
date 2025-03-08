# Server Variables 

[![Downloads](https://img.shields.io/nuget/dt/Umbraco.Community.ServerVariables?color=cc9900)](https://www.nuget.org/packages/Umbraco.Community.ServerVariables/)
[![NuGet](https://img.shields.io/nuget/vpre/Umbraco.Community.ServerVariables?color=0273B3)](https://www.nuget.org/packages/Umbraco.Community.ServerVariables)
[![GitHub license](https://img.shields.io/github/license/iOvergaard/umbraco-servervariables?color=8AB803)](../LICENSE)

This package reintroduces server variables from C# to JavaScript in Umbraco 15+ with a twist. 

In short, it is a simple way to add server variables to your Umbraco site. It allows you to add server variables to your site without having to write any code.

The variables are added through appsettings.json or through the IServerVariablesService interface.

The twist is that the variables are made available through the importmap in the frontend. This way, you can import the variables where you need them without having to rely on any global JavaScript objects.

## Installation

Add the package to an existing Umbraco website (v15+) from nuget:

`dotnet add package Umbraco.Community.ServerVariables`

### Configuration through appsettings.json

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

* **Namespace**: This is the namespace that the variables will be available under in the frontend through the importmap. For example, if you set this to `myVars`, you will import the variables like this: `import { MyVariable } from 'myVars';`.
* **CacheHeader**: This is the cache header value that will be set on the importmap. This is useful if you want to cache the importmap. By default, it is set to `no-cache, no-store, must-revalidate`.
* **Values**: This is an object with the variables you want to add. The key is the name of the variable, and the value is the value of the variable.

### Configuration through IServerVariablesService

Add the following to your `Startup.cs` or ideally in a composer:

```csharp
using ServerVariables.Services;
using Umbraco.Cms.Core.Composing;

namespace ServerVariables.TestSite;

public class TestServerVariablesComposer : ComponentComposer<TestServerVariablesComponent>
{
}

public class TestServerVariablesComponent(IServerVariablesService serverVariablesService) : IAsyncComponent
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

### Usage in the Backoffice

In any Backoffice component, you can now import the server variables where you need them:

```javascript
import { MyVariable } from '@server-variables';

console.log('MyVariable', MyVariable);
```

This will log `MyValue` to the console.

### Usage in the Frontend

In any frontend component, you can now import the server variables where you need them:

```javascript
import { MyVariable } from '/App_Plugins/ServerVariables/index.js';

console.log('MyVariable', MyVariable);
```

This will log `MyValue` to the console.

> ![NOTE]
> Do not include any secrets in the server variables as they will be exposed to the public.

## Contributing

Contributions to this package are most welcome! Please read the [Contributing Guidelines](CONTRIBUTING.md).
