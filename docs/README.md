# Installation

## Requirements

- Umbraco 15 or higher
- .NET 9 or higher

## Installation

The package is available from NuGet and can be installed using Visual Studio or the .NET CLI.

### Visual Studio

1. Open your solution in Visual Studio
2. Right-click on your project and select `Manage NuGet Packages...`
3. Search for `Umbraco.Community.ServerVariables` and click `Install`

### .NET CLI

```bash
dotnet add package Umbraco.Community.ServerVariables
```

## Configuration

The package is configured using the `appsettings.json` file in your project. The following settings are available:

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

- **Namespace** - This is the namespace that the variables will be available under in the frontend through the importmap. For example, if you set this to `myVars`, you will import the variables like this: `import { MyVariable } from 'myVars';`. |
- **CacheHeader** - This is the cache header value that will be set on the importmap. This is useful if you want to cache the importmap. By default, it is set to `no-cache, no-store, must-revalidate`, which tells the browser to not cache anything. |
- **Values** - This is a dictionary of variables that you want to expose to the frontend. The key is the variable name, and the value is the variable value. |

## Usage

Use the `IServerVariablesService`, the `appsettings.json` file, or the options pattern to add variables to the importmap. The variables will be available in the frontend under the namespace you set in the configuration:

- [Configuration through appsettings.json](01-appsettings.md)
- [Configuration through .NET](02-csharp.md)
- [Usage in the Frontend](03-frontend.md)
- [Usage with a context](04-context.md)
- [Backoffice views](05-backoffice.md)
