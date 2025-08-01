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
├── appsettings.json           # Connection string for SQL Server
└── CredentialDbContextFactory.cs  # For design-time context creation

Csnp.Credential.Domain/
├── Aggregates/
├── Entities/
│   ├── User.cs
│   └── Role.cs
├── Interfaces/
│   ├── IUserRepository.cs
│   └── IRoleRepository.cs
├── Events/
├── Specifications/
└── DomainExceptions/

Csnp.Credential.Infrastructure/
├── Persistence/
│   ├── CredentialDbContext.cs
│   └── Configurations/
├── Repositories/
│   ├── UserRepository.cs
│   └── RoleRepository.cs
├── External/                  # SMTP, REST/gRPC clients, etc.
├── Services/                  # Domain service implementations
├── Events/                    # Domain event dispatchers
└── DependencyInjection.cs
```

---

## 🔧 Configuration

### `appsettings.json`

```json
{
  "ConnectionStrings": {
    "Default": "Server=localhost;Database=local_csnp;User Id=local;Password=Local+54321z@;TrustServerCertificate=True;"
  }
}
```

---

## 🧱 Create Migration

In **Package Manager Console**:

```powershell
Add-Migration InitialCreate
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
            : "Server=localhost;Database=local_csnp;User Id=local;Password=Local+54321z@;TrustServerCertificate=True;";

        var optionsBuilder = new DbContextOptionsBuilder<CredentialDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new CredentialDbContext(optionsBuilder.Options);
    }
}
```

---

## 🚀 Apply Migration

```powershell
Update-Database
```

> This will apply the schema to the database defined in your `appsettings.json`.

---

## ▶️ Optional: Seed Default Data

If you added a `SeedData.cs` file in `Seeds/`, you can run the seeding logic by calling it inside `Program.cs` after `MigrateAsync()`:

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

- This setup uses **SQL Server**. Make sure connection strings use `Server=` instead of `Host=`, and use `UseSqlServer` instead of `UseNpgsql`.
- Ensure `ApplyConfigurationsFromAssembly(...)` is called in `OnModelCreating()` to activate Fluent API settings.
- Use `ToTable("Roles", "Credential")` to specify schema-qualified table names.

---

## 🧱 Setup Domain & Infrastructure Projects (DDD)

### Domain Layer (`Csnp.Credential.Domain`)

- Contains only **pure business logic** — aggregates, entities, value objects, interfaces, domain services, domain events.
- No EF Core or database concerns.

Example: `Entities/User.cs`

```csharp
public class User
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = default!;
    public string DisplayName { get; set; } = default!;
}
```

Example: `Interfaces/IUserRepository.cs`

```csharp
public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);
    Task AddAsync(User user);
    Task<IEnumerable<User>> GetAllAsync();
}
```

### Infrastructure Layer (`Csnp.Credential.Infrastructure`)

Implements `CredentialDbContext`, repositories, and configuration.

Example: `Persistence/CredentialDbContext.cs`

```csharp
public class CredentialDbContext : IdentityDbContext
{
    public CredentialDbContext(DbContextOptions<CredentialDbContext> options) : base(options) {}

    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(CredentialDbContext).Assembly);
    }
}
```

Example: `Repositories/UserRepository.cs`

```csharp
public class UserRepository : IUserRepository
{
    private readonly CredentialDbContext _db;

    public UserRepository(CredentialDbContext db)
    {
        _db = db;
    }

    public Task<User?> GetByIdAsync(Guid id) =>
        _db.Users.FindAsync(id).AsTask();

    public async Task AddAsync(User user)
    {
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
    }

    public Task<IEnumerable<User>> GetAllAsync() =>
        Task.FromResult<IEnumerable<User>>(_db.Users.ToList());
}
```

Example: `DependencyInjection.cs`

```csharp
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("Default");

        services.AddDbContext<CredentialDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
```

---

Let me know if you'd like to generate full boilerplate for Role, seeding, or tests.
