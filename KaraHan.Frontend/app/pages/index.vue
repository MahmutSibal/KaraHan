<script setup lang="ts">
const { isAuthenticated, user, fetchProfile } = useAuth()

onMounted(async () => {
  if (isAuthenticated.value && !user.value) {
    await fetchProfile()
  }
})
</script>

<template>
  <main class="hero kh-appear">
    <section class="panel kh-panel">
      <p class="eyebrow">KaraHan Platform</p>
      <h1>Gorev Yonetimini Sade ve Guvenli Hale Getirin</h1>
      <p class="lead">
        Tum gorevlerini tek merkezden yonet, ilerlemeni gor ve ekibinle ayni standartta calis.
      </p>

      <div class="metrics">
        <div>
          <span>Takip</span>
          <strong>Gunluk ve Haftalik ozet</strong>
        </div>
        <div>
          <span>Plan</span>
          <strong>Geciken gorev kontrolu</strong>
        </div>
        <div>
          <span>Analiz</span>
          <strong>Verimli zaman araliklari</strong>
        </div>
      </div>

      <div class="actions">
        <NuxtLink v-if="!isAuthenticated" to="/login" class="kh-btn kh-btn-primary">Giris Yap</NuxtLink>
        <NuxtLink v-if="!isAuthenticated" to="/register" class="kh-btn kh-btn-ghost">Kayit Ol</NuxtLink>
        <NuxtLink v-if="isAuthenticated" to="/dashboard" class="kh-btn kh-btn-primary">Dashboard</NuxtLink>
        <NuxtLink v-if="isAuthenticated" to="/tasks" class="kh-btn kh-btn-ghost">Gorevler</NuxtLink>
      </div>
    </section>
  </main>
</template>

<style scoped>
.hero {
  min-height: 100vh;
  display: grid;
  place-items: center;
  padding: 1rem;
}

.panel {
  width: min(760px, 100%);
  border-radius: 16px;
  padding: 1.8rem;
}

.eyebrow {
  letter-spacing: 0.08em;
  text-transform: uppercase;
  color: #2f6ea8;
  font-weight: 700;
  font-size: 0.8rem;
}

h1 {
  margin: 0.5rem 0 0;
  font-size: clamp(1.8rem, 3.6vw, 3rem);
  max-width: 19ch;
}

.lead {
  margin-top: 1rem;
  color: #3e4f63;
  line-height: 1.5;
  max-width: 58ch;
}

.metrics {
  margin-top: 1.4rem;
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(160px, 1fr));
  gap: 0.7rem;
}

.metrics div {
  background: #f8fbff;
  border: 1px solid var(--line);
  border-radius: 10px;
  padding: 0.8rem;
}

.metrics span {
  display: block;
  color: #607185;
  font-size: 0.84rem;
}

.metrics strong {
  font-size: 1rem;
}

.actions {
  display: flex;
  gap: 0.8rem;
  margin-top: 1.5rem;
  flex-wrap: wrap;
}
</style>
