# Get the variables with a context

You can consume a context to access the server variables in the backoffice. This is useful if you want to access the server variables in a custom section or dashboard.

## Usage

In any backoffice component, you can now import the server variables where you need them by accessing global context directly:

```javascript
this.consumeContext('ServerVariablesContext', (context) => {
    console.log('MyVariable', context.index.MyVariable); // MyValue
});
```

This will log `MyValue` to the console.

This can be used in any Backoffice component that extends `UmbLitElement` or `UmbElementMixin` and therefore is a host element.

## Static access

If you want static access to the variables, you should refer to the default setup: [Usage with appsettings.json#Usage](01-appsettings.md).