# KaraHan Frontend

Nuxt 4 tabanli frontend uygulamasidir.

## Ozellikler

- Register / Login ekranlari
- JWT token ile yetkili API cagrilari
- Dashboard ozet ve analiz gorunumu
- Gorev yonetimi (listeleme, olusturma, guncelleme, silme)
- Route middleware ile auth/guest sayfa korumasi

## Kurulum

```bash
npm install
```

## Gelistirme Ortami

```bash
npm run dev
```

Varsayilan adres: `http://localhost:3000`

## Build

```bash
npm run build
npm run preview
```

## API Baglantisi

`nuxt.config.ts` icindeki runtime config:

- `public.apiBase`: backend API adresi

Varsayilan deger: `http://localhost:5191`

## Dosya Yapisi (Ozet)

- `app/composables/useApi.ts` -> merkezi API istemcisi
- `app/composables/useAuth.ts` -> auth state ve profile akisi
- `app/middleware/auth.ts` -> korumali sayfalar
- `app/middleware/guest.ts` -> login/register gibi guest sayfalar
- `app/pages/dashboard.vue` -> analiz ve metrik ekrani
- `app/pages/tasks.vue` -> gorev yonetim ekrani
