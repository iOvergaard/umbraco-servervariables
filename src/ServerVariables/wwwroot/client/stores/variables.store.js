import { UmbContextBase } from '@umbraco-cms/backoffice/class-api';
import { SV_VARIABLES_STORE } from './variables-store.token.js';

export class SvVariablesStore extends UmbContextBase {
  /**
   * The data store for server variables
   * @type {Map<string, Map<string, unknown>>}
   */
  #data = new Map();

  constructor(host) {
    super(host, SV_VARIABLES_STORE)
  }

  /**
   * Gets a section of server variables
   * @param {string} section The section to get
   * @returns {Map<string, unknown>}
   */
  getSection(section) {
    return this.#data.get(section);
  }

  /**
   * Gets all server variables
   * @returns {Map<string, Map<string, unknown>>}
   */
  getAll() {
    return this.#data;
  }
}

export default SvVariablesStore;
