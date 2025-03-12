import vars from 'vars';
import brand from 'vars/brand.js';

console.log('server variables', vars);

assert(vars, 'vars should be defined');
assert(vars.apiKey === '123456789', 'vars.apiKey should be defined');
assert(vars.apiUrl === 'https://api.example.com', 'vars.apiUrl should be defined');
assert(vars.hello === 'from options', 'vars.hello should be defined');
assert(typeof vars.isEnabled === 'boolean' && vars.isEnabled === true, 'vars.isEnabled should be defined as a boolean and true');

console.log('brand', brand);

assert(brand.brand === 'My Brand', 'brand.brand should be defined');

function assert(condition, message) {
  if (!condition) {
    throw new Error(message);
  }
}
