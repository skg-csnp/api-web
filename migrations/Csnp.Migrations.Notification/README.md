# Csnp.Migrations.Notification

This project handles database migrations and schema seeding using **Entity Framework Core** and **ASP.NET Core Identity** with **PostgreSQL**.

---

## ✅ Requirements

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- PostgreSQL (Local or Docker)
- Visual Studio with Package Manager Console

---

## 📦 NuGet Packages

Run the following commands in the **Package Manager Console** with `Csnp.Migrations.Notification` selected as **Default Project**:

```powershell
Install-Package Npgsql.EntityFrameworkCore.PostgreSQL -Version 9.0.4
Install-Package Microsoft.EntityFrameworkCore.Design -Version 9.0.8
Install-Package Microsoft.EntityFrameworkCore.Tools -Version 9.0.8
Install-Package Microsoft.AspNetCore.Identity.EntityFrameworkCore -Version 9.0.8
```

---

## 🧱 Create Migration

In **Package Manager Console**:

```powershell
Add-Migration InitialCreate
```

> Make sure `Csnp.Migrations.Notification` is selected as the **Default Project** in the console.

---

## 🚀 Apply Migration

```powershell
Update-Database
```
