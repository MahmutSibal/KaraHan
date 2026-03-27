<script setup lang="ts">
import type { DashboardSummary } from '~/types/api'

definePageMeta({ middleware: 'auth' })

const { user, fetchProfile, logout } = useAuth()
const { apiFetch } = useApi()

const summary = ref<DashboardSummary | null>(null)
const pending = ref(true)
const error = ref('')

async function load() {
  error.value = ''
  pending.value = true

  try {
    if (!user.value) {
      await fetchProfile()
    }
    summary.value = await apiFetch<DashboardSummary>('/api/dashboard/summary')
  }
  catch {
    error.value = 'Dashboard verileri yuklenemedi.'
  }
  finally {
    pending.value = false
  }
}

await load()
</script>

<template>
  <main class="shell kh-appear">
    <header class="topbar">
      <div>
        <p class="eyebrow">Performans Merkezi</p>
        <h1>Dashboard</h1>
        <p class="subline">{{ user?.fullName }} - {{ user?.email }}</p>
      </div>
      <div class="top-actions">
        <NuxtLink to="/tasks" class="kh-btn kh-btn-ghost">Gorevler</NuxtLink>
        <button class="kh-btn kh-btn-primary" @click="logout(); navigateTo('/login')">Cikis</button>
      </div>
    </header>

    <p v-if="error" class="error">{{ error }}</p>
    <p v-if="pending">Yukleniyor...</p>

    <section v-if="summary" class="grid">
      <article class="card kh-panel">
        <h3>Toplam Gorev</h3>
        <strong>{{ summary.totalTasks }}</strong>
      </article>
      <article class="card kh-panel">
        <h3>Tamamlanan</h3>
        <strong>{{ summary.completedTasks }}</strong>
      </article>
      <article class="card kh-panel">
        <h3>Geciken</h3>
        <strong>{{ summary.overdueTasks }}</strong>
      </article>
      <article class="card kh-panel">
        <h3>Gecikme Orani</h3>
        <strong>%{{ Math.round(summary.delayedTaskRate * 100) }}</strong>
      </article>
    </section>

    <section v-if="summary" class="split">
      <article class="card kh-panel">
        <h2>Gunluk Performans</h2>
        <ul>
          <li v-for="day in summary.dailyPerformance" :key="day.date">
            <span>{{ day.date }}</span>
            <b>{{ day.completedCount }}</b>
          </li>
        </ul>
      </article>

      <article class="card kh-panel">
        <h2>Verimli Saatler</h2>
        <ul>
          <li v-for="window in summary.productiveWindows" :key="window.windowLabel">
            <span>{{ window.windowLabel }}</span>
            <b>{{ window.completedCount }}</b>
          </li>
        </ul>
      </article>
    </section>

    <section v-if="summary" class="card kh-panel">
      <h2>Oneriler</h2>
      <ul>
        <li v-for="item in summary.suggestions" :key="item">{{ item }}</li>
      </ul>
    </section>
  </main>
</template>

<style scoped>
.shell {
  min-height: 100vh;
  padding: 1rem;
  max-width: 1000px;
  margin: 0 auto;
}

.eyebrow {
  margin: 0;
  letter-spacing: 0.08em;
  text-transform: uppercase;
  color: #2f6ea8;
  font-size: 0.78rem;
  font-weight: 700;
}

.subline {
  color: var(--muted);
  margin: 0.3rem 0 0;
}

.topbar {
  display: flex;
  justify-content: space-between;
  gap: 1rem;
  align-items: center;
  flex-wrap: wrap;
}

.top-actions {
  display: flex;
  gap: 0.5rem;
}

.grid {
  margin-top: 1rem;
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(170px, 1fr));
  gap: 0.8rem;
}

.split {
  margin-top: 1rem;
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(280px, 1fr));
  gap: 0.8rem;
}

.card {
  border-radius: 12px;
  padding: 1rem;
}

.card strong {
  font-size: 1.8rem;
}

ul {
  margin: 0;
  padding: 0;
  list-style: none;
}

li {
  display: flex;
  justify-content: space-between;
  padding: 0.4rem 0;
  border-bottom: 1px solid var(--line);
}

.error {
  color: var(--danger);
}
</style>
