
# Görev Yöneticisi (TaskManager)

Bu proje, Piton Technology’nin Backend Yazılım Geliştirme biriminde stajyer olarak değerlendirilmek üzere hazırlanmıştır. Proje, kullanıcıların günlük, haftalık ve aylık görevlerini yönetmelerine olanak sağlayan bir görev yönetim sistemidir.

## Projenin Amacı

Kullanıcının yapacağı işleri periyodik olarak raporlayabildiği, modern yazılım geliştirme ilkeleriyle hazırlanmış küçük ama anlamlı bir uygulama ortaya koymak. Bu proje ile hem teknik becerilerimi göstermek hem de gerçek dünya projelerinde kullanılan araçları tanımak istedim.

## Kullanılan Teknolojiler

- **ASP.NET Core Web API**: Projenin ana çatısını oluşturan API yapısı.
- **JWT Authentication**: Kimlik doğrulama için güvenli token bazlı çözüm.
- **Swagger**: API’yi test etmek ve belgelemek için entegre edildi.
- **Entity Framework Core**: ORM çözümü olarak kullanıldı.
- **MSSQL**: Veritabanı çözümü olarak tercih edildi. (İstenirse MongoDB uyarlanabilir.)
- **Docker (opsiyonel)**: Uygulamanın container ortamında çalışması için yapılandırıldı.

## Katmanlı Mimari

Proje, sürdürülebilirliği ve test edilebilirliği artırmak adına **Onion Architecture** prensipleriyle tasarlanmıştır:

- `Domain`: Temel iş kuralları ve modeller
- `Application`: Uygulama iş mantığı
- `Infrastructure`: Dış servislerle iletişim (örneğin veri erişimi)
- `API`: Sunum ve istemcilerle etkileşim katmanı

## Kimlik Doğrulama

JWT ile kullanıcı kimliği doğrulanır. Kullanıcılar sadece token ile erişim sağlayabilir. Bu sistem güvenliği artırır ve modern web uygulamalarıyla uyumludur.

## API Belgeleri

Swagger arayüzü sayesinde API uç noktaları kolayca test edilebilir:
- `GET /tasks`
- `POST /tasks`
- `PUT /tasks/{id}`
- `DELETE /tasks/{id}`
- `POST /auth/login`

## Docker Desteği

Docker kullanılarak uygulama daha taşınabilir hale getirildi. Geliştirme ve dağıtım süreçleri için `docker-compose.yml` yapılandırması hazırlandı.

## Öğrendiklerim ve Notlar

- Projenin performansını artırmak için araştırmalarım sırasında **cache mekanizmalarını** (örneğin: `IMemoryCache`, `DistributedCache`) inceledim. Ancak, projenin kapsamı gereği bu özelliği entegre etmedim. İleride bu özelliği ekleyerek performans iyileştirmesi planlıyorum.
- `OpenTelemetry` ile dağıtık izleme (distributed tracing) üzerine çalıştım ancak sadece yapılandırma seviyesinde bıraktım.
- Projede `Rate Limiter` gibi production seviyesinde önlemlerin nasıl uygulanabileceğini araştırıp belgeledim.

## Kurulum ve Başlatma

1. Gerekli paketleri yükleyin:
   ```bash
   dotnet restore
   ```

2. Veritabanı yapılandırmasını yapın (`appsettings.json` içinde bağlantı stringi).

3. Uygulamayı başlatın:
   ```bash
   dotnet run --project TaskManager.API
   ```

4. Swagger arayüzüne şu adresten ulaşabilirsiniz:
   ```
   https://localhost:{port}/swagger
   ```

## Sonuç

Bu projeyi geliştirirken sadece kod yazmakla kalmadım; aynı zamanda gerçek bir yazılım geliştirme sürecinde karşılaşılabilecek senaryoları da deneyimledim. Yeni teknolojiler öğrenerek hem kendimi geliştirdim hem de iş akışlarını daha iyi anladım.

---

Projeyi GitHub üzerinden paylaşarak değerlendirmeye aldığınız için teşekkür ederim. Geri bildiriminizi sabırsızlıkla bekliyorum!
