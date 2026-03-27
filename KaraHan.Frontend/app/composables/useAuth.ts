import { computed } from 'vue'
import { useCookie, useState } from 'nuxt/app'
import type { AuthResponse, UserProfile } from '../types/api'
import { useApi } from './useApi'

export function useAuth() {
  const token = useCookie<string | null>('kh_token', { sameSite: 'lax' })
  const user = useState<UserProfile | null>('auth_user', () => null)
  const loading = useState<boolean>('auth_loading', () => false)
  const { apiFetch } = useApi()

  async function register(payload: { fullName: string; email: string; password: string }) {
    const response = await apiFetch<AuthResponse>('/api/auth/register', {
      method: 'POST',
      body: payload,
    })

    token.value = response.token
    const profile = await fetchProfile(response.token)
    if (!profile) {
      throw new Error('Profil getirilemedi.')
    }
  }

  async function login(payload: { email: string; password: string }) {
    const response = await apiFetch<AuthResponse>('/api/auth/login', {
      method: 'POST',
      body: payload,
    })

    token.value = response.token
    const profile = await fetchProfile(response.token)
    if (!profile) {
      throw new Error('Profil getirilemedi.')
    }
  }

  async function fetchProfile(tokenOverride?: string) {
    const effectiveToken = tokenOverride ?? token.value

    if (!effectiveToken) {
      user.value = null
      return null
    }

    try {
      loading.value = true
      user.value = await apiFetch<UserProfile>('/api/auth/profile', {
        authToken: effectiveToken,
      })
      return user.value
    }
    catch {
      token.value = null
      user.value = null
      return null
    }
    finally {
      loading.value = false
    }
  }

  function logout() {
    token.value = null
    user.value = null
  }

  return {
    token,
    user,
    loading,
    register,
    login,
    fetchProfile,
    logout,
    isAuthenticated: computed(() => Boolean(token.value)),
  }
}
