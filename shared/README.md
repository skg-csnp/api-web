# Shared Projects

This document outlines the shared projects used across all bounded contexts within the CSNP Platform. These follow Domain-Driven Design (DDD) principles, distinguishing between SeedWork, Shared Kernel, and cross-cutting concerns.

---

## 🧱 Csnp.SeedWork

These projects provide clean, dependency-free DDD abstractions and technical scaffolding:

### ✅ Csnp.SeedWork.Domain

* Core DDD building blocks: `Entity`, `ValueObject`, `AggregateRoot`, `IDomainEvent`, etc.
* Used by all domain layers across bounded contexts.
* No external dependencies.

### ✅ Csnp.SeedWork.Application

* Application-layer contracts and base interfaces:

  * `ICommand`, `IQuery`, `IRequestHandler<T>`, etc.
* Defines MediatR-based abstractions only (no concrete behaviors).
* Keeps the layer implementation-agnostic and testable.

### ✅ Csnp.SeedWork.Infrastructure

* Infrastructure abstractions like `IRepository<T>`, `IUnitOfWork`, etc.
* Contains no external references to Entity Framework or any database provider.
* Allows infrastructure layer to plug in actual implementations.

---

## 🧹 Csnp.SharedKernel

Reusable domain logic and implementations shared across bounded contexts:

### ✅ Csnp.SharedKernel.Domain

* Shared value objects: `EmailAddress`, `Money`, `PhoneNumber`, etc.
* Domain events reused across services: `UserSignedUpDomainEvent`, etc.
* May depend on `Csnp.SeedWork.Domain`.

### ✅ Csnp.SharedKernel.Application

* Implementation of shared MediatR pipelines: `ValidationBehavior`, `LoggingBehavior`, etc.
* FluentValidation integration, base result types, error formatting.
* Depends on `Csnp.SeedWork.Application`.

### ✅ Csnp.SharedKernel.Infrastructure

* Base EF Core repositories, audit support, integration helpers.
* Shared database-related implementation logic.

---

## ⚙️ Csnp.Common

* General-purpose technical helpers:

  * String/date extensions, constants, utilities.
* Non-domain and cross-cutting by nature.
* No dependency on other layers.

---

## 📬 Csnp.EventBus

* Abstractions for event-driven communication between services:

  * `IIntegrationEventPublisher`
  * `IIntegrationHandler<T>`
* May include shared serialization settings or messaging contracts.
* Used for RabbitMQ/Kafka integration.

---

## 🌐 Csnp.Presentation.Common

* Common presentation-layer components:

  * Middleware (logging, exception handling)
  * API versioning
  * Standardized error response for validation
* Targets Web API layer reusability.

---

## 📁 Directory Structure

All shared libraries are located in the `/shared/` directory at the root level:

```
shared/
├── Csnp.Common/
├── Csnp.EventBus/
├── Csnp.Presentation.Common/
├── Csnp.SeedWork.Application/
├── Csnp.SeedWork.Domain/
├── Csnp.SeedWork.Infrastructure/
├── Csnp.SharedKernel.Application/
├── Csnp.SharedKernel.Domain/
└── Csnp.SharedKernel.Infrastructure/
```

---

## ✅ Usage Guidelines

| Layer                   | Purpose                                                         | Dependencies |
| ----------------------- | --------------------------------------------------------------- | ------------ |
| **SeedWork**            | Pure DDD contracts and abstractions. No external packages.      | ❌ None       |
| **SharedKernel**        | Shared domain and implementation logic across bounded contexts. | ✅ Allowed    |
| **Common**              | Technical utilities not tied to DDD.                            | ✅ Allowed    |
| **EventBus**            | Integration event abstraction for message brokers.              | ✅ Allowed    |
| **Presentation.Common** | Reusable Web API components like middlewares and formatters.    | ✅ Allowed    |

> 🧼 Keep `Csnp.SeedWork` clean and stable.
> 🔁 Evolve `Csnp.SharedKernel` carefully when multiple domains need the same concept.
> 🔌 Never depend on infrastructure implementation from SeedWork.
