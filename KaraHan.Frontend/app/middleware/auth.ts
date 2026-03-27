export default defineNuxtRouteMiddleware(async (to) => {
  const { token, user, fetchProfile } = useAuth()

  if (!token.value) {
    return navigateTo('/login')
  }

  if (!user.value) {
    await fetchProfile()
  }

  if (!token.value) {
    return navigateTo('/login')
  }

  if (to.path === '/login' || to.path === '/register') {
    return navigateTo('/dashboard')
  }
})
