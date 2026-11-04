import { umbHttpClient } from '@umbraco-cms/backoffice/http-client';

/**
 * Make an authorized request to any Backoffice API.
 * @param host A reference to the host element that can request a context.
 * @param url The URL to request.
 * @param method The HTTP method to use.
 * @param body The body to send with the request (if any).
 * @returns The response from the request as JSON.
 */
export async function makeRequest(host, url, method = 'GET', body) {
  // The security entry makes the client send the auth the running Umbraco version expects (bearer or cookie).
  const { data, error } = await umbHttpClient.request({
    url,
    method,
    body,
    security: [{ scheme: 'bearer', type: 'http' }],
    headers: {
      'Content-Type': 'application/json',
    },
  });

  if (error) {
    throw error;
  }

  return data;
}
