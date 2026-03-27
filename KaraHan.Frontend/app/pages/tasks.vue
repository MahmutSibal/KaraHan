<script setup lang="ts">
import type { TaskItem, TaskPriority } from '~/types/api'

definePageMeta({ middleware: 'auth' })

const { apiFetch } = useApi()
const { logout } = useAuth()

const tasks = ref<TaskItem[]>([])
const pending = ref(false)
const error = ref('')

const form = reactive({
  title: '',
  description: '',
  priority: 2 as TaskPriority,
  dueDateUtc: '',
})

const priorityOptions = [
  { value: 1, text: 'Low' },
  { value: 2, text: 'Medium' },
  { value: 3, text: 'High' },
  { value: 4, text: 'Critical' },
]

async function loadTasks() {
  pending.value = true
  error.value = ''

  try {
    tasks.value = await apiFetch<TaskItem[]>('/api/tasks')
  }
  catch {
    error.value = 'Gorevler yuklenemedi.'
  }
  finally {
    pending.value = false
  }
}

async function createTask() {
  if (!form.title || !form.dueDateUtc) {
    return
  }

  try {
    await apiFetch('/api/tasks', {
      method: 'POST',
      body: {
        title: form.title,
        description: form.description || null,
        priority: Number(form.priority),
        dueDateUtc: new Date(form.dueDateUtc).toISOString(),
      },
    })

    form.title = ''
    form.description = ''
    form.priority = 2
    form.dueDateUtc = ''

    await loadTasks()
  }
  catch {
    error.value = 'Gorev olusturulamadi.'
  }
}

async function toggleComplete(task: TaskItem) {
  try {
    await apiFetch(`/api/tasks/${task.id}`, {
      method: 'PUT',
      body: {
        title: task.title,
        description: task.description,
        priority: task.priority,
        dueDateUtc: task.dueDateUtc,
        isCompleted: !task.isCompleted,
      },
    })

    await loadTasks()
  }
  catch {
    error.value = 'Gorev guncellenemedi.'
  }
}

async function deleteTask(taskId: number) {
  try {
    await apiFetch(`/api/tasks/${taskId}`, { method: 'DELETE' })
    await loadTasks()
  }
  catch {
    error.value = 'Gorev silinemedi.'
  }
}

await loadTasks()
</script>

<template>
  <main class="shell kh-appear">
    <header class="topbar">
      <div>
        <p class="eyebrow">Planlama Alani</p>
        <h1>Gorevler</h1>
      </div>
      <div class="actions">
        <NuxtLink to="/dashboard" class="kh-btn kh-btn-ghost">Dashboard</NuxtLink>
        <button class="kh-btn kh-btn-primary" @click="logout(); navigateTo('/login')">Cikis</button>
      </div>
    </header>

    <p v-if="error" class="error">{{ error }}</p>

    <section class="card form-grid kh-panel">
      <input v-model="form.title" type="text" placeholder="Gorev basligi" />
      <input v-model="form.description" type="text" placeholder="Aciklama" />
      <select v-model.number="form.priority">
        <option v-for="option in priorityOptions" :key="option.value" :value="option.value">
          {{ option.text }}
        </option>
      </select>
      <input v-model="form.dueDateUtc" type="datetime-local" />
      <button class="kh-btn kh-btn-primary" @click="createTask">Ekle</button>
    </section>

    <section class="card kh-panel">
      <p v-if="pending">Yukleniyor...</p>

      <table v-else class="table">
        <thead>
          <tr>
            <th>Baslik</th>
            <th>Oncelik</th>
            <th>Teslim</th>
            <th>Durum</th>
            <th>Islem</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="task in tasks" :key="task.id">
            <td>{{ task.title }}</td>
            <td>{{ task.priority }}</td>
            <td>{{ new Date(task.dueDateUtc).toLocaleString() }}</td>
            <td>
              <span :class="task.isCompleted ? 'ok' : task.isOverdue ? 'late' : 'open'">
                {{ task.isCompleted ? 'Tamamlandi' : task.isOverdue ? 'Gecikti' : 'Acik' }}
              </span>
            </td>
            <td class="row-actions">
              <button class="kh-btn kh-btn-ghost" @click="toggleComplete(task)">
                {{ task.isCompleted ? 'Geri Al' : 'Tamamla' }}
              </button>
              <button class="kh-btn kh-btn-danger" @click="deleteTask(task.id)">Sil</button>
            </td>
          </tr>
        </tbody>
      </table>
    </section>
  </main>
</template>

<style scoped>
.shell {
  min-height: 100vh;
  padding: 1rem;
  max-width: 1100px;
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

.topbar {
  display: flex;
  justify-content: space-between;
  align-items: center;
  flex-wrap: wrap;
  gap: 0.7rem;
}

.actions {
  display: flex;
  gap: 0.5rem;
}

.card {
  margin-top: 1rem;
  border-radius: 12px;
  padding: 1rem;
}

.form-grid {
  display: grid;
  gap: 0.6rem;
  grid-template-columns: repeat(auto-fit, minmax(170px, 1fr));
}

input,
select {
  border: 1px solid var(--line);
  border-radius: 10px;
  padding: 0.6rem;
  background: rgba(255, 255, 255, 0.78);
}

.table {
  width: 100%;
  border-collapse: collapse;
}

th,
td {
  border-bottom: 1px solid var(--line);
  text-align: left;
  padding: 0.55rem;
}

.row-actions {
  display: flex;
  gap: 0.4rem;
}

.ok {
  color: #067647;
}

.late {
  color: #b42318;
}

.open {
  color: #175cd3;
}

.error {
  color: var(--danger);
}

@media (max-width: 700px) {
  .table,
  thead,
  tbody,
  tr,
  th,
  td {
    display: block;
  }

  th {
    display: none;
  }

  td {
    padding: 0.4rem 0;
  }

  .row-actions {
    margin-bottom: 0.8rem;
  }
}
</style>
