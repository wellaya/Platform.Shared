# Platform.Shared

**Platform.Shared** is a collection of reusable building blocks for developing **Clean Architecture** APIs with **Azure Functions (.NET 10 Isolated Worker)**.

Built from real-world, multi-vertical applications, these libraries provide a consistent foundation for cross-cutting concerns such as domain abstractions, application pipelines, infrastructure, Azure Functions middleware, and testing.

Instead of duplicating common code across every service or maintaining a single monolithic solution, each vertical (Orders, Billing, Inventory, etc.) can reference the shared packages it needs.

---

## Packages

| Package | Description |
|---------|-------------|
| **Platform.Domain** | Base domain abstractions including `BaseEntity`, `BaseAuditableEntity`, `ValueObject`, domain events, and `Result<T>`. |
| **Platform.Application** | Application-layer building blocks including MediatR pipeline behaviors (validation, logging, performance), `ICurrentUser`, and `PaginatedList<T>`. |
| **Platform.Infrastructure** | Shared infrastructure components including the EF Core audit interceptor and `IVerticalConnectionStringResolver` for database resolution. |
| **Platform.Functions** | Azure Functions isolated worker middleware including global exception handling, RFC 7807 Problem Details responses, and correlation ID support. |
| **Platform.Testing** | Test infrastructure including Testcontainers-powered `BaseIntegrationTest<TContext>` for integration testing. |

---

## Installation

Install the packages required by your project.

```bash
dotnet add package Platform.Domain --source https://nuget.pkg.github.com/YourOrg/index.json
dotnet add package Platform.Application
dotnet add package Platform.Infrastructure
dotnet add package Platform.Functions
```

---

## Database Resolution

`IVerticalConnectionStringResolver` enables a gradual migration from a shared database to dedicated databases for individual verticals.

Resolution order:

1. `ConnectionStrings:{VerticalName}`
2. `ConnectionStrings:SharedPlatformDb`

### Example

**Shared database**

```json
{
  "ConnectionStrings": {
    "SharedPlatformDb": "Server=..."
  }
}
```

All verticals use the shared database.

**Dedicated database for Orders**

```json
{
  "ConnectionStrings": {
    "SharedPlatformDb": "Server=...",
    "Orders": "Server=OrdersDb..."
  }
}
```

The **Orders** vertical now uses its own database while every other vertical continues using the shared database—without requiring any application code changes.

---

## Building

Build the solution locally using the standard .NET CLI.

```bash
dotnet restore
dotnet build
dotnet test
dotnet pack -o ./artifacts
```

---

## Versioning

This repository uses **Nerdbank.GitVersioning (NBGV)** for automatic semantic versioning based on Git history.

To create a release, tag the repository using semantic version tags such as:

```text
v1.0
v1.1
v2.0
```

Package and assembly versions are generated automatically during the build.

---

## Related Repository

See the companion **Clean Architecture Azure Functions template** for a complete implementation showing how these shared packages are used within a vertical application.

> https://github.com/YourOrg/CleanArchitecture.Functions.Template

---

## License

This project is licensed under the **MIT License**.