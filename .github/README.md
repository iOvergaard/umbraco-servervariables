# Server Variables for Umbraco

[![Downloads](https://img.shields.io/nuget/dt/Umbraco.Community.ServerVariables?color=cc9900)](https://www.nuget.org/packages/Umbraco.Community.ServerVariables/)
[![NuGet](https://img.shields.io/nuget/vpre/Umbraco.Community.ServerVariables?color=0273B3)](https://www.nuget.org/packages/Umbraco.Community.ServerVariables)
[![GitHub license](https://img.shields.io/github/license/iOvergaard/umbraco-servervariables?color=8AB803)](../LICENSE)

Server variables exposed to the browser in Umbraco 15+ using the importmap.

## What is it?

Server Variables was a thing up until Umbraco 13, where you could add variables to your site through the `ServerVariablesParser.Parsing` notification and access them in the Backoffice through `Umbraco.Sys.ServerVariables`. This was removed in Umbraco 14, but this package reintroduces the concept with a twist.

In short, this package gives you a simple way to expose server variables to your Umbraco Backoffice and/or Frontend. It allows you to add server variables to your site without having to write any or only a little code.

## Installation

Add the package to an existing Umbraco website (v15+) from nuget:

`dotnet add package Umbraco.Community.ServerVariables`

## Usage

See the [documentation](../docs/01-install.md) for more information.

## Contributing

Contributions to this package are most welcome! Please read the [Contributing Guidelines](CONTRIBUTING.md).
