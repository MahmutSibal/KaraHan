export function useApi() {
  const config = useRuntimeConfig()

  async function apiFetch<T>(url: string, options: Record<string, any> = {}) {
    const token = useCookie<string | null>('kh_token', { sameSite: 'lax' })
    const authToken = options.authToken ?? token.value
    const { authToken: _, ...fetchOptions } = options
    const headers = new Headers(fetchOptions.headers as HeadersInit | undefined)

    if (authToken) {
      headers.set('Authorization', `Bearer ${authToken}`)
    }

    return await $fetch<T>(url, {
      baseURL: config.public.apiBase,
      ...fetchOptions,
      headers,
    })
  }

  return { apiFetch }
}
