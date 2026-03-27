<script setup lang="ts">
definePageMeta({ middleware: 'guest' })

const email = ref('')
const password = ref('')
const error = ref('')
const pending = ref(false)

const { login } = useAuth()

async function submit() {
  error.value = ''
  pending.value = true

  try {
    await login({ email: email.value, password: password.value })
    await navigateTo('/dashboard')
  }
  catch {
    error.value = 'Giris basarisiz. Bilgilerinizi kontrol edin.'
  }
  finally {
    pending.value = false
  }
}
</script>

<template>
  <main class="auth-wrap kh-appear">
    <form class="auth-card kh-panel" @submit.prevent="submit">
      <p class="eyebrow">Hos geldin</p>
      <h1>Giris Yap</h1>
      <p v-if="error" class="error">{{ error }}</p>

      <label>E-posta</label>
      <input v-model="email" type="email" required />

      <label>Sifre</label>
      <input v-model="password" type="password" required />

      <button :disabled="pending" type="submit" class="kh-btn kh-btn-primary">
        {{ pending ? 'Bekleyin...' : 'Giris' }}
      </button>

      <NuxtLink to="/register" class="switch-link">Hesabiniz yok mu? Kayit olun</NuxtLink>
    </form>
  </main>
</template>

<style scoped>
.auth-wrap {
  min-height: 100vh;
  display: grid;
  place-items: center;
  padding: 1rem;
}

.auth-card {
  width: min(430px, 100%);
  display: grid;
  gap: 0.65rem;
  border-radius: 14px;
  padding: 1.5rem;
}

.eyebrow {
  margin: 0;
  letter-spacing: 0.08em;
  text-transform: uppercase;
  color: #2f6ea8;
  font-weight: 700;
  font-size: 0.78rem;
}

input {
  border: 1px solid var(--line);
  border-radius: 10px;
  padding: 0.65rem 0.7rem;
  background: rgba(255, 255, 255, 0.78);
}

.switch-link {
  margin-top: 0.2rem;
  color: #234f70;
  font-weight: 600;
}

.error {
  color: var(--danger);
}
</style>
