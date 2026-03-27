<template>
  <div class="app-bg">
    <NuxtRouteAnnouncer />
    <NuxtPage />
  </div>
</template>

<style>
@import url('https://fonts.googleapis.com/css2?family=Manrope:wght@400;600;700;800&family=Space+Grotesk:wght@500;700&display=swap');

:root {
  --bg: #eef3f8;
  --panel: #ffffff;
  --text: #1f2a37;
  --muted: #5d6b7d;
  --primary: #1f6fb2;
  --primary-strong: #165a92;
  --accent: #2463a6;
  --danger: #b42318;
  --line: #d9e2ec;
  --shadow: 0 10px 24px rgba(31, 42, 55, 0.08);
}

* {
  box-sizing: border-box;
}

html,
body,
#__nuxt {
  margin: 0;
  min-height: 100%;
}

body {
  font-family: 'Manrope', sans-serif;
  color: var(--text);
  background: var(--bg);
}

h1,
h2,
h3 {
  font-family: 'Space Grotesk', sans-serif;
}

.app-bg {
  min-height: 100vh;
  background:
    radial-gradient(circle at 0% 0%, rgba(31, 111, 178, 0.07) 0%, rgba(31, 111, 178, 0) 35%),
    radial-gradient(circle at 100% 100%, rgba(43, 108, 176, 0.08) 0%, rgba(43, 108, 176, 0) 40%),
    linear-gradient(180deg, #f3f7fc 0%, #eef3f8 100%);
}

.kh-panel {
  background: var(--panel);
  border: 1px solid var(--line);
  box-shadow: var(--shadow);
}

.kh-btn {
  border: 0;
  text-decoration: none;
  cursor: pointer;
  font-weight: 700;
  border-radius: 12px;
  padding: 0.72rem 1rem;
  transition: transform 0.2s ease, box-shadow 0.2s ease;
}

.kh-btn:hover {
  transform: translateY(-1px);
}

.kh-btn-primary {
  color: white;
  background: linear-gradient(135deg, var(--primary), var(--primary-strong));
  box-shadow: 0 5px 14px rgba(31, 111, 178, 0.25);
}

.kh-btn-ghost {
  color: var(--text);
  background: #f7faff;
  border: 1px solid var(--line);
}

.kh-btn-danger {
  color: white;
  background: linear-gradient(120deg, #cf3f34, var(--danger));
}

.kh-appear {
  animation: slideFade 0.3s ease;
}

@keyframes slideFade {
  from {
    opacity: 0;
    transform: translateY(10px);
  }

  to {
    opacity: 1;
    transform: translateY(0);
  }
}

</style>