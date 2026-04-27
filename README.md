<<<<<<< HEAD
# MaintainIQ — Machine Maintenance Management

Razor Pages + EF Core + SQLite (SQL Server'a geçiş hazır)

---

## Kurulum (5 dakika)

### Gereksinimler
- [.NET 8 SDK](https://dotnet.microsoft.com/download)

### Çalıştırma

```bash
cd MaintainIQ
dotnet run
```

Tarayıcıda aç: **http://localhost:5000**

İlk çalıştırmada:
- `maintainiq.db` dosyası otomatik oluşur
- Migration otomatik uygulanır
- Seed data (2 örnek makine) otomatik eklenir

---

## SQL Server'a Geçiş

`appsettings.json` dosyasını düzenle:

```json
{
  "UseSqlServer": true,
  "ConnectionStrings": {
    "SqlServer": "Server=YOUR_SERVER;Database=MaintainIQ;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

Ardından migration ekle:

```bash
dotnet ef migrations add InitialCreate --context AppDbContext
dotnet ef database update
```

---

## Proje Yapısı

```
MaintainIQ/
├── Models/
│   ├── Machine.cs
│   ├── MaintenanceRecord.cs
│   └── FailureRecord.cs
├── Data/
│   └── AppDbContext.cs          ← EF Core context + seed data
├── Pages/
│   ├── Shared/
│   │   ├── _Layout.cshtml
│   │   └── _Sidebar.cshtml
│   ├── Machines/
│   │   ├── Details.cshtml(.cs)  ← Makine detayı + tab'lar
│   │   ├── Create.cshtml(.cs)
│   │   ├── Edit.cshtml(.cs)
│   │   ├── Maintenance/
│   │   │   ├── Create.cshtml(.cs)
│   │   │   └── Edit.cshtml(.cs)
│   │   └── Failures/
│   │       ├── Create.cshtml(.cs)
│   │       └── Edit.cshtml(.cs)
│   └── Index.cshtml
├── wwwroot/
│   ├── css/site.css             ← Tüm stiller (Poppins, light mode)
│   └── js/site.js
├── Migrations/
├── appsettings.json
└── Program.cs
```

---

## Özellikler

- ✅ Makine listesi (sol sidebar, arama)
- ✅ Makine ekle / düzenle / sil
- ✅ Bakım kayıtları (ekle / düzenle / sil)
- ✅ Arıza kayıtları (ekle / düzenle / sil)
- ✅ Yazdır (print-friendly)
- ✅ SQLite (local) — SQL Server'a tek satır config ile geçiş
- ✅ Poppins font, light mode, temiz UI
=======
# TorqIQ
A full-stack web application built with ASP.NET Razor Pages and SQLite for managing machine maintenance processes.

## 🚀 Features
- Machine inventory management
- Maintenance and failure tracking
- Scheduled maintenance planning
- CRUD operations with SQLite database
- Structured and responsive UI
- Real-time data updates

## 🛠️ Tech Stack
- ASP.NET Core Razor Pages
- C#
- SQLite
- HTML5, CSS3, Bootstrap
- JavaScript
>>>>>>> 9f76e447e524d2e8d7c704962ebb03af14a0414b
