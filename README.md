# KaraHan Case Study

Bu repository iki uygulamadan olusur:

- `KaraHan/` -> Nuxt 4 frontend
- `KaraHan.API/` -> .NET 8 Web API (Clean Architecture, JWT, SQL Server)

## 1. Mimari Ozet

- Frontend: Nuxt 4, Composition API, cookie tabanli JWT yonetimi
- Backend: 4 katmanli Clean Architecture
  - `KaraHan.Domain`
  - `KaraHan.Application`
  - `KaraHan.Infrastructure`
  - `KaraHan.API` (Presentation)
- Veri tabani: SQL Server (Development ayari localhost)
- Kimlik dogrulama: JWT Bearer

## 2. Gereksinimler

- Node.js 20+
- .NET SDK 8.0+
- SQL Server / SQL Server Express / LocalDB (ortama gore)

## 3. Backend Calistirma

```bash
cd KaraHan.API
dotnet restore
dotnet build KaraHan.API.sln
dotnet run --project KaraHan.API.csproj
```

API varsayilan olarak:

- `http://localhost:5191`
- Swagger: `http://localhost:5191/swagger`

Not: Veritabani baglantisi `KaraHan.API/appsettings.Development.json` icindedir.

## 4. Frontend Calistirma

```bash
cd KaraHan
npm install
npm run dev
```

Frontend varsayilan olarak:

- `http://localhost:3000`

`nuxt.config.ts` icindeki `runtimeConfig.public.apiBase` degeri backend adresini gostermelidir.

## 5. Kullanici Akisi

1. `Register` ile hesap olusturulur.
2. `Login` ile JWT token alinir.
3. Frontend tokeni cookie'de saklar ve `Authorization: Bearer <token>` olarak API'ye gonderir.
4. `Dashboard` ve `Tasks` ekranlari yetkili kullaniciya ozel veri gosterir.

## 6. Ana Endpointler

- `POST /api/auth/register`
- `POST /api/auth/login`
- `GET /api/auth/profile` (Authorize)
- `GET /api/tasks` (Authorize)
- `POST /api/tasks` (Authorize)
- `PUT /api/tasks/{taskId}` (Authorize)
- `DELETE /api/tasks/{taskId}` (Authorize)
- `GET /api/dashboard/summary` (Authorize)

## 7. Teslim Dosyalari

- Proje kurulum ve calistirma: bu README
- Teknik rapor: `TEKNIK_RAPOR.md`
- Frontend detaylari: `KaraHan/README.md`
- Backend detaylari: `KaraHan.API/README.md`
