# Csnp.Migrations.Credential

This project handles database migrations and schema seeding using **Entity Framework Core** and **ASP.NET Core Identity** with **SQL Server**.

---

## ✅ Requirements

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- SQL Server (Local or Docker)
- Visual Studio with Package Manager Console

---

## 📦 NuGet Packages

Run the following commands in the **Package Manager Console** with `Csnp.Migrations.Credential` selected as **Default Project**:

```powershell
Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 9.0.7
Install-Package Microsoft.EntityFrameworkCore.Design -Version 9.0.7
Install-Package Microsoft.EntityFrameworkCore.Tools -Version 9.0.7
Install-Package Microsoft.AspNetCore.Identity.EntityFrameworkCore -Version 9.0.7
```

---

## 👨‍💻 Structure

```
Csnp.Migrations.Credential/
├── CredentialDbContext.cs
├── User.cs
├── Role.cs
├── Migrations/                # Auto-generated EF migration files
├── Configurations/            # Fluent API configurations per entity
├── Seeds/                     # Optional seeding logic
└── appsettings.json           # Connection string for SQL Server
```

---

## 🔧 Configuration

### `appsettings.json`

```json
{
  "ConnectionStrings": {
    "Default": "Server=localhost;Database=local_csnp_credential;User Id=local;Password=Local+54321z@;TrustServerCertificate=True;"
  }
}
```

---

## 🧱 Create Migration

In **Package Manager Console**:

```powershell
Add-Migration InitialCreate
Add-Migration FixDisplayNameColumn
```

> Make sure `Csnp.Migrations.Credential` is selected as the **Default Project** in the console.

> You must implement `IDesignTimeDbContextFactory<CredentialDbContext>` to support `-Args` for connection string input.

---

## 🏗️ Design-Time DbContext Factory

Add the following class to support migration-time creation using custom connection string:

```csharp
public class CredentialDbContextFactory : IDesignTimeDbContextFactory<CredentialDbContext>
{
    public CredentialDbContext CreateDbContext(string[] args)
    {
        var connectionString = args.Length > 0
            ? args[0]
            : "Server=localhost;Database=local_csnp_credential;User Id=local;Password=Local+54321z@;TrustServerCertificate=True;";

        var optionsBuilder = new DbContextOptionsBuilder<CredentialDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new CredentialDbContext(optionsBuilder.Options);
    }
}
```

---

## 🚀 Apply Migration

```powershell
Update-Database InitialCreate -Args '"Server=localhost;Database=local_csnp_credential;User Id=local;Password=Local+54321z@;TrustServerCertificate=True;"'
Update-Database
```

> This will apply the schema to the database defined in your `appsettings.json`.

---

## ▶️ Optional: Seed Default Data

If you added a `SeedData.cs` file in `Seeds/`, you can run the seeding logic by calling it inside `Program.cs` after `MigrateAsync()`.

```csharp
await db.Database.MigrateAsync();
await SeedData.SeedAsync(userManager, roleManager);
```

---

## 🐳 (Optional) Docker-based Execution

You can create a `Dockerfile` to build and run the migration as a standalone container for CI/CD or Kubernetes.

---

## ✅ Tips

- Do not run `Add-Migration` or `Update-Database` from the API project.
- Keep migrations isolated in this project for better deployment separation.
- Use Git tags or commit hashes to version migration releases.

---

## 📌 Notes

- To switch to PostgreSQL, replace `UseSqlServer` with `UseNpgsql` and install the appropriate package.

