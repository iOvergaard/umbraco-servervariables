export const manifests = [
  {
    type: 'store',
    alias: 'ServerVariables.Store.Variables',
    name: 'Server Variables Store',
    js: () => import('./variables.store.js'),
  }
]
