import { UmbRepositoryBase } from '@umbraco-cms/backoffice/repository';
import { makeRequest } from "../functions.js";

export class SvRepositoryVariables extends UmbRepositoryBase {
  async requestCollection(filter) {
    debugger;

    try {
      const urlParams = new URLSearchParams(filter);
      const data = await makeRequest(this._host, `/umbraco/servervariables/api/v1/items?${urlParams}`, 'GET');
      return {
        data,
      }
    } catch(error) {
      return {
        data: {
          total: 0,
          items: []
        },
        error: new Error(`Failed to get sv-repository variables for ${filter}: ${error.message}`),
      }
    }
  }
}

export default SvRepositoryVariables;
