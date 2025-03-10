# Usage in the Frontend

In any frontend component, you can now import the server variables where you need them by accessing the virtual path directly:

```javascript
import { MyVariable } from '/App_Plugins/ServerVariables/index.js';

console.log('MyVariable', MyVariable);
```

This will log `MyValue` to the console.

## Sections

Sections can be accessed in the frontend by importing the section directly:

```javascript
import { MyVariable } from '/App_Plugins/ServerVariables/MySection.js';

console.log('MyVariable', MyVariable);
```

## Importmap

You can construct your own importmap in your frontend, if you want to mimick the Backoffice:

```html
<script type="importmap">
  {
    "imports": {
      "vars": "/App_Plugins/ServerVariables/index.js",
      "vars/MySection.js": "/App_Plugins/ServerVariables/MySection.js"
    }
  }
</script>
```

This will allow you to import the variables like this:

```javascript
import { MyVariable } from 'vars';
import { MyVariable } from 'vars/MySection.js';
```
