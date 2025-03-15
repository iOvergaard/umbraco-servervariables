import { UMB_AUTH_CONTEXT } from '@umbraco-cms/backoffice/auth';

/**
 * Make an authorized request to any Backoffice API.
 * @param host A reference to the host element that can request a context.
 * @param url The URL to request.
 * @param method The HTTP method to use.
 * @param body The body to send with the request (if any).
 * @returns The response from the request as JSON.
 */
export async function makeRequest(host, url, method = 'GET', body) {
  const authContext = await host.getContext(UMB_AUTH_CONTEXT);
  const token = await authContext.getLatestToken();
  const response = await fetch(url, {
    method,
    body: body ? JSON.stringify(body) : undefined,
    headers: {
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`,
    },
  });
  return response.json();
}
