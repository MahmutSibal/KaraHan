export default defineNuxtRouteMiddleware(async () => {
  const { token, user, fetchProfile } = useAuth()

  if (token.value && !user.value) {
    await fetchProfile()
  }

  if (token.value) {
    return navigateTo('/dashboard')
  }
})
