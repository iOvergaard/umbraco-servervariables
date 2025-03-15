import { UmbLitElement } from '@umbraco-cms/backoffice/lit-element';
import { html } from '@umbraco-cms/backoffice/external/lit';
import { UMB_COLLECTION_CONTEXT } from '@umbraco-cms/backoffice/collection';
import { UmbTextStyles } from '@umbraco-cms/backoffice/style';

export class SvCollectionView extends UmbLitElement {
  static get properties() {
    return {
      _tableItems: { type: Array, state: true },
    };
  }

  _tableConfig = {
    allowSelection: false,
  };

  _tableColumns = [
    {
      name: 'Key',
      alias: 'key',
    },
    {
      name: 'Value',
      alias: 'value',
    },
    {
      name: 'Section',
      alias: 'section',
    },
    {
      name: '',
      alias: 'actions',
      align: 'right'
    },
  ];

  constructor() {
    super();

    this.consumeContext(UMB_COLLECTION_CONTEXT, (instance) => {
      this.#observeCollectionItems(instance);
    })
  }

  render() {
    return html`
      <umb-table .config=${this._tableConfig} .columns=${this._tableColumns} .items=${this._tableItems}></umb-table>
    `;
  }

  #observeCollectionItems(context) {
    this.observe(context.items, (items) => {
      this.#createTable(items);
    }, '_itemsObserver')
  }

  #createTable(items) {
    this._tableItems = items.map(item => {
      return {
        id: item.key,
        icon: 'icon-globe',
        data: [
          {
            columnAlias: 'key',
            value: item.key
          },
          {
            columnAlias: 'value',
            value: item.value
          },
          {
            columnAlias: 'section',
            value: item.section
          },
          // TODO: Umbraco 15.4.0
          /*{
            columnAlias: 'actions',
            value: html`<uui-copy-text-button .text="${item.key}" label="Copy key"></uui-copy-text-button>`
          }*/
        ]
      }
    })
  }

  static styles = [
    UmbTextStyles,
  ];
}

customElements.define('sv-collection-view', SvCollectionView);

export default SvCollectionView;
