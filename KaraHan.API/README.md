# KaraHan.API

KaraHan platformunun backend uygulamasidir.

## Teknoloji Yigini

- .NET 8 Web API
- Clean Architecture (Domain, Application, Infrastructure, API)
- EF Core + SQL Server
- JWT Bearer Authentication
- Swagger / OpenAPI

## Proje Yapisi

- `KaraHan.Domain` -> Entity ve core domain kurallari
- `KaraHan.Application` -> DTO, abstraction, business service contractlari
- `KaraHan.Infrastructure` -> EF DbContext, repository, security implementasyonlari
- `KaraHan.API` -> Controller, middleware, startup konfigurasyonu

## Calistirma

```bash
dotnet restore
dotnet build KaraHan.API.sln
dotnet run --project KaraHan.API.csproj
```

Varsayilan adresler:

- API: `http://localhost:5191`
- Swagger: `http://localhost:5191/swagger`

## Konfigurasyon

`appsettings.Development.json`:

- `ConnectionStrings:SqlServer`
- `Jwt:Key`
- `Jwt:Issuer`
- `Jwt:Audience`
- `Jwt:ExpiryMinutes`

## Auth Akisi

- `POST /api/auth/register`
- `POST /api/auth/login`
- `GET /api/auth/profile`

Korumali endpoint cagrilarinda header:

`Authorization: Bearer <token>`

## Gorev ve Dashboard Endpointleri

- `GET /api/tasks`
- `POST /api/tasks`
- `PUT /api/tasks/{taskId}`
- `DELETE /api/tasks/{taskId}`
- `GET /api/dashboard/summary`

Tumu `[Authorize]` ile korunur.

## Hata Yonetimi

- Global exception middleware ile standart JSON hata cevabi donulur.
- SQL baglanti hatalari startup asamasinda loglanir.
