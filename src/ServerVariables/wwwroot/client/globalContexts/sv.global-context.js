import { UmbContextBase } from '@umbraco-cms/backoffice/class-api';
import SvRepository from '../repositories/sv.repository.js';

export class SvGlobalContext extends UmbContextBase {
  #repository = new SvRepository(this._host);

  constructor(host) {
    super(host, 'ServerVariablesContext');
  }

  /**
   * Gets all server variables
   * @returns {Promise<Map<string, Map<string, *>>>}
   */
  getAll() {
    return this.#repository.requestAll();
  }

  /**
   * Gets a section of server variables
   * @param section
   * @returns {Promise<Map<string, *>>}
   */
  getSection(section) {
    return this.#repository.requestSection(section);
  }
}

export default SvGlobalContext;
