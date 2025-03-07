import vars from 'vars';
import appSettings from 'vars/appSettings.js';

console.log('server variables', vars);

assert(vars, 'vars should be defined');
assert(vars.apiKey === '123456789', 'vars.apiKey should be defined');
assert(vars.apiUrl === 'https://api.example.com', 'vars.apiUrl should be defined');

console.log('appSettings', appSettings);

assert(appSettings.FromAppSettings === 'Hello from appsettings.json', 'appSettings.FromAppSettings should be defined');

function assert(condition, message) {
  if (!condition) {
    throw new Error(message);
  }
}
