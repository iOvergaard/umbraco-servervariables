import { manifests as repositoryManifests } from './repositories/manifests.js';
import { manifests as storeManifests } from './stores/manifests.js';

export const manifests = [
  ...repositoryManifests,
  ...storeManifests,
  {
    type: 'globalContext',
    alias: 'ServerVariables.GlobalContext',
    name: 'Server Variables Global Context',
    js: () => import('./globalContexts/sv.global-context.js'),
  },
  {
    type: 'menuItem',
    name: 'Server Variables Menu Root Item',
    alias: 'ServerVariables.Menu.RootItem',
    meta: {
      label: 'Server Variables',
      icon: "icon-script",
      entityType: 'servervariables-root',
      menus: ['Umb.Menu.AdvancedSettings']
    },
  },
  {
    type: 'workspace',
    kind: 'default',
    name: 'Server Variables Workspace',
    alias: 'ServerVariables.Workspace',
    meta: {
      entityType: 'servervariables-root',
      headline: 'Server Variables',
    }
  },
  {
    type: 'workspaceView',
    kind: 'collection',
    name: 'Server Variables Workspace View',
    alias: 'ServerVariables.Workspace.View',
    meta: {
      label: 'Collection',
      pathname: 'collection',
      icon: 'icon-layers',
      collectionAlias: 'ServerVariables.Collection.Variables',
    },
    conditions: [
      {
        alias: 'Umb.Condition.WorkspaceAlias',
        match: 'ServerVariables.Workspace'
      }
    ]
  },
  {
    type: 'collection',
    kind: 'default',
    alias: 'ServerVariables.Collection.Variables',
    name: 'Server Variables Collection',
    meta: {
      repositoryAlias: 'ServerVariables.Items.Repository.Variables',
    },
  },
  {
    type: 'collectionView',
    alias: 'ServerVariables.Collection.View',
    name: 'Server Variables Collection View',
    js: () => import('./collectionViews/sv-collection-view.element.js'),
    meta: {
      label: 'Table',
      icon: 'icon-list',
      pathName: 'table',
    },
    conditions: [
      {
        alias: 'Umb.Condition.CollectionAlias',
        match: 'ServerVariables.Collection.Variables',
      },
    ],
  },
];
