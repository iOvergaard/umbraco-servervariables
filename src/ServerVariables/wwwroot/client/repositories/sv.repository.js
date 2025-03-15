import { UmbRepositoryBase } from '@umbraco-cms/backoffice/repository';
import { SV_VARIABLES_STORE } from '../stores/variables-store.token.js';
import { makeRequest } from '../functions.js';

export class SvVariablesRepository extends UmbRepositoryBase {
  /**
   * @type {SvVariablesStore}
   */
  #dataStore;

  /**
   * @type {Promise<void>}
   */
  #initialized;
  #isLoaded = false;

  constructor(host) {
    super(host);

    this.#initialized = this.consumeContext(SV_VARIABLES_STORE, (instance) => {
      this.#dataStore = instance;
    }).asPromise();
  }

  /**
   * Requests all server variables
   * @returns {Promise<Map<string, Map<string, *>>>}
   */
  async requestAll() {
    await this.#initialized;
    if (!this.#isLoaded) {
      await this.#loadData();
    }
    return this.#dataStore.getAll();
  }

  /**
   * Requests a section of server variables
   * @param section
   * @returns {Promise<Map<string, *>>}
   */
  async requestSection(section) {
    await this.#initialized;
    if (!this.#isLoaded) {
      await this.#loadData();
    }
    return this.#dataStore.getSection(section);
  }

  async #loadData() {
    try {
      const data = await makeRequest(this._host, '/umbraco/servervariables/api/v1', 'GET');
      this.#dataStore.setData(data);
    } catch (error) {
      return {
        data: {
          total: 0,
          items: []
        },
        error: new Error(`Failed to get sv-repository variables: ${error.message}`),
      }
    }
  }
}

export default SvVariablesRepository;
