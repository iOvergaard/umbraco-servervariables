# Server Variables for Umbraco

This package reintroduces server variables in Umbraco 15+, which is a way to expose variables from C# to JavaScript.

## What is it?

Server Variables was a thing up until Umbraco 13, where you could add variables to your site through the `ServerVariablesParser.Parsing` notification and access them in the Backoffice through `Umbraco.Sys.ServerVariables`. This was removed in Umbraco 14, but this package reintroduces the concept with a twist.

In short, this package gives you a simple way to expose server variables to your Umbraco Backoffice and/or Frontend. It allows you to add server variables to your site without having to write any or only a little code.

The variables are added through **appsettings.json** or through the `IServerVariablesService` interface.

The twist is that the variables are made available through the importmap in the browser. This way, you can import the variables where you need them without having to rely on any global JavaScript objects. They will also work in the Frontend which is a big plus.

**NB!** Do not include any secrets in the server variables as they will be exposed to the public.

## Requirements

- Umbraco 15 or higher
- .NET 9 or higher

## Usage

See the documentation for more information on how to use the package:

- [Installation](https://github.com/iOvergaard/umbraco-servervariables/blob/main/docs/01-install.md)
- [Usage with appsettings.json](https://github.com/iOvergaard/umbraco-servervariables/blob/main/docs/02-appsettings.md)
- [Usage with IServerVariablesService](https://github.com/iOvergaard/umbraco-servervariables/blob/main/docs/03-csharp.md)
- [Usage in the Frontend](https://github.com/iOvergaard/umbraco-servervariables/blob/main/docs/04-frontend.md)

## Acknowledgements

[Javascript icons created by Graphix's Art - Flaticon](https://www.flaticon.com/free-icons/javascript)
