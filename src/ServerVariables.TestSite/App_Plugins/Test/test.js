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

export const onInit = (host) => {
  console.log('onInit', host);
  host.consumeContext('ServerVariablesContext', async (context) => {
    assert(context, 'consumeContext should be defined');
    assert(context.getAll, 'context.getAll should be defined');
    assert(context.getSection, 'context.getSection should be defined');

    const all = await context.getAll();
    assert(all.index.apiKey === '123456789', 'all.index.apiKey should be defined');
    console.log('getAll', all);
  });
}

function assert(condition, message) {
  if (!condition) {
    throw new Error(message);
  }
}
