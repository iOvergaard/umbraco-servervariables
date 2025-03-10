# Usage with appsettings.json

You can add variables to the importmap using the `appsettings.json` file in your project. This is useful if you want to add variables that are the same across all pages, or if you want to add variables that are not dynamic, or if the variables differ between environments.

This could be static API URLs, the current environment, or the current user.

## Configuration

Add the following to your `appsettings.json`:

```json
{
  "ServerVariables": {
    "Values": {
      "MyVariable": "MyValue"
    }
  }
}
```

## Usage

The variables will be available in any Backoffice component under the namespace you set in the configuration:

```javascript
import { MyVariable } from 'vars';

console.log(MyVariable); // MyValue
```
