# Shared Projects

This document outlines the shared projects used across all bounded contexts within the CSNP Platform. These are organized following Domain-Driven Design (DDD) principles, distinguishing between SeedWork, Shared Kernel, and cross-cutting concerns.

---

## 🧱 Csnp.SeedWork

These projects provide fundamental DDD abstractions and technical scaffolding:

### ✅ Csnp.SeedWork.Domain
- Core DDD building blocks: `Entity`, `ValueObject`, `AggregateRoot`, `IDomainEvent`, etc.
- Used by all domain layers across bounded contexts.

### ✅ Csnp.SeedWork.Application
- Application-layer support: `ICommand`, `IQuery`, MediatR behaviors, result types.
- Useful for CQRS-based use cases and pipelines.

### ✅ Csnp.SeedWork.Infrastructure
- Infrastructure-related abstractions and utilities.
- Includes base repositories (EF Core), serialization helpers, caching, etc.
- Used by infrastructure layers of bounded contexts.

---

## ⚙️ Csnp.Common
- General-purpose utility classes: extensions, constants, helpers.
- Purely technical (non-domain) and cross-cutting.

---

## 📬 Csnp.EventBus
- Abstractions for event-driven communication:
  - `IIntegrationEventPublisher`
  - `IIntegrationHandler<T>`
- Used for RabbitMQ/Kafka integration between services.

---

## 🌐 Csnp.Presentation.Common
- Shared components for the presentation (API) layer:
  - API versioning
  - Middleware (e.g., error formatting, logging)
  - FluentValidation response standardization

---

## 🧩 Csnp.SharedKernel
- Reusable domain logic across contexts.
- Includes value objects like `EmailAddress`, `Money`, `Address`, etc.
- Considered part of the domain, but shared due to consistency.

---

## 📁 Placement
All shared projects are located in the `/shared/` directory at the root level:

```
shared/
├── Csnp.Common/
├── Csnp.EventBus/
├── Csnp.Presentation.Common/
├── Csnp.SeedWork.Domain/
├── Csnp.SeedWork.Application/
├── Csnp.SeedWork.Infrastructure/
└── Csnp.SharedKernel/
```

---

## ✅ Guideline
- Use SeedWork for technical building blocks only.
- Use SharedKernel sparingly — only for domain concepts shared by multiple bounded contexts.
- Keep infrastructure logic isolated and abstracted.
