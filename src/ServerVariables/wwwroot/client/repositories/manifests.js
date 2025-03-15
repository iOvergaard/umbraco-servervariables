export const manifests = [
  {
    type: 'repository',
    alias: 'ServerVariables.Items.Repository.Variables',
    name: 'Server Variables Items Repository',
    api: () => import('./sv-items.repository.js'),
  }
]
