# Teknik Rapor - KaraHan Gorev ve Analiz Platformu

## 1. Neden Bu Teknolojiler Secildi?

### Backend: .NET 8 Web API

- Performansli ve olgun bir web API ekosistemi saglar.
- Kurumsal projelerde guclu tooling ve test destegi vardir.
- JWT, middleware, DI, OpenAPI gibi ihtiyaclar dogal olarak desteklenir.

### Mimari: 4 Katmanli Clean Architecture

- Domain bilgisini altyapi detaylarindan ayirir.
- Test edilebilirligi ve degisime dayanikliligi artirir.
- Proje buyudukce kodun moduler kalmasini saglar.

### ORM: EF Core + SQL Server

- SQL Server ile guclu entegrasyon.
- Hızlı gelistirme icin DbContext ve LINQ avantajlari.
- Migration/indeks/yapisal degisiklik yonetimi kolaydir.

### Frontend: Nuxt 4

- Vue tabanli gelistirme hizini SSR/SPA esnekligi ile birlestirir.
- Composition API ile net state yonetimi sunar.
- Route middleware ve runtime config ile API entegrasyonu rahattir.

## 2. Alternatifler Nelerdi?

- Backend: Node.js (NestJS), Java (Spring Boot), Go
- Frontend: React + Next.js
- ORM: Dapper (daha manuel ama daha ince kontrol)
- Veri tabani: PostgreSQL

Neden mevcut secim korundu?

- Case kapsaminda hizli ama duzenli bir kurgu hedeflendi.
- .NET + EF Core + SQL Server kombinasyonu authentication, CRUD ve analiz senaryosu icin dengeli bir cozum sundu.

## 3. Karsilasilan Teknik Zorluklar

### Zorluk 1: SQL baglanti hatalari

- Gelistirme ortaminda SQL Server instance/port farkliliklari nedeniyle baglanti hatasi olustu.
- Cozum: baglanti stringi netlestirildi, startup seviyesinde SQL exception loglandi.

### Zorluk 2: Frontend tarafinda 401

- Token olmasina ragmen profile cagrilarinda zaman zaman 401 alindi.
- Cozum: token aktarim akisinda tutarlilik saglandi, API istemcisi header yonetimi merkezilestirildi.

### Zorluk 3: Proje yapisinin katmanlara ayrilmasi

- Tek projeden cok projeli clean architecture yapisina geciste derleme kapsami cakisablirdi.
- Cozum: proje referanslari ve compile kapsami temizlendi.

## 4. Olceklemek Istesek Neyi Degistirirdik?

### 4.1 Uygulama Seviyesi

- CQRS + MediatR ile command/query ayrimi
- Validation pipeline (FluentValidation)
- Refresh token + revoke mekanizmasi
- Role/permission tabanli authorization

### 4.2 Veri ve Performans

- EF migrations + versioned deployment
- Kritik sorgular icin read model / materialized view
- Redis cache (dashboard ozetleri ve sık sorgulanan veriler)
- Arka plan isleri icin queue (Hangfire/RabbitMQ/Azure Service Bus)

### 4.3 Dagitim ve Operasyon

- Docker + orchestrasyon (Kubernetes veya App Service)
- API Gateway ve rate limiting
- Merkezi loglama (Serilog + Elasticsearch/Grafana Loki)
- Metrics ve tracing (OpenTelemetry)

## 5. Performans ve Olceklenebilirlik Yaklasimi

- Veritabani tarafinda user/task odakli indeksleme
- HTTP tarafinda stateless JWT ile yatay olceklenebilirlik
- Dashboard hesaplari icin onbellek ve asenkron hesaplama potansiyeli

## 6. Guvenlik Yaklasimi

- JWT Bearer auth
- Authorize policy ile korumali endpointler
- Password hashing (salt + guvenli hash)
- CORS sinirlamasi (yalniz izinli frontend origin)

## 7. Sonuc

Cozum, case gereksinimlerini calisir bir urun olarak karsilayacak sekilde hazirlandi:

- Kullanici yonetimi
- Gorev CRUD
- Dashboard ve analiz metrikleri
- Moduler, temiz ve olceklendirilebilir backend kurgusu

Bir sonraki iterasyonda oncelik: test kapsamı, cache katmani ve production hardening.
