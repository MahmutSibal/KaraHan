export type AuthResponse = {
  token: string
  expiresAtUtc: string
}

export type UserProfile = {
  id: number
  fullName: string
  email: string
  createdAtUtc: string
}

export type TaskPriority = 1 | 2 | 3 | 4

export type TaskItem = {
  id: number
  title: string
  description: string | null
  priority: TaskPriority
  dueDateUtc: string
  isCompleted: boolean
  createdAtUtc: string
  completedAtUtc: string | null
  isOverdue: boolean
}

export type DailyPerformance = {
  date: string
  completedCount: number
}

export type TimeWindowPerformance = {
  windowLabel: string
  completedCount: number
}

export type DashboardSummary = {
  totalTasks: number
  completedTasks: number
  overdueTasks: number
  delayedTaskRate: number
  dailyPerformance: DailyPerformance[]
  productiveWindows: TimeWindowPerformance[]
  suggestions: string[]
}
