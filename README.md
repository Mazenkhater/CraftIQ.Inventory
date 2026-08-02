# CraftIQ.Inventory

Clean Architecture Inventory System built with ASP.NET Core Web API, ASP.NET Identity, JWT Authentication, CQRS, MediatR, Redis Distributed Caching, Serilog, FluentValidation, and Entity Framework Core.

This project demonstrates a real-world inventory management backend designed using Clean Architecture principles with clear separation of concerns between Core, Application, Infrastructure, and API layers.

---

# PROJECT OVERVIEW

CraftIQ.Inventory is a scalable inventory management system that handles products, categories, inventory tracking, orders, and transactions with secure authentication and authorization.

The application follows modern .NET development practices to provide maintainability, scalability, high performance, and clean code organization.

---

# ARCHITECTURE

The solution follows Clean Architecture:

API Layer
- Controllers
- Middleware
- Swagger

Application Layer
- CQRS (Commands & Queries)
- MediatR Handlers
- DTOs
- Validation
- Caching Pipeline
- Logging Pipeline

Core Layer
- Entities
- Interfaces
- Contracts
- Domain Models

Infrastructure Layer
- Entity Framework Core
- ASP.NET Identity
- SQL Server
- Redis
- JWT Authentication
- Repository Pattern

Dependency Rule

Inner layers never depend on outer layers.

---

# FEATURES

## Authentication

- User Registration
- Secure Login
- JWT Authentication
- Refresh Token Support
- Forgot Password
- Reset Password

## Inventory Management

- Product CRUD Operations
- Category CRUD Operations
- Inventory Tracking
- Search
- Filtering
- Pagination

## Orders

- Order Creation
- Order Details Management
- Product Availability Validation

## Transactions

- Transaction Logging
- Inventory Movement Tracking

## Performance

- Redis Distributed Caching
- MediatR Caching Pipeline Behavior
- Automatic Cache Invalidation
- Faster Read Operations

## Logging

- Structured Logging using Serilog
- Console Logging
- File Logging
- Request Logging
- Exception Logging

---

# TECH STACK

- ASP.NET Core Web API
- Entity Framework Core
- ASP.NET Core Identity
- SQL Server
- MediatR (CQRS)
- JWT Authentication
- Redis Distributed Cache
- Serilog
- FluentValidation
- Swagger / OpenAPI

---

# DATABASE ENTITIES

- Product
- Category
- Inventory
- Order
- OrderDetail
- Transaction
- RefreshToken

---

# AUTHENTICATION FLOW

1. User logs in using email and password.
2. The system validates user credentials.
3. JWT Access Token is generated.
4. Refresh Token is stored in the database.
5. Access Token is used to authorize API requests.
6. Refresh Token is used to generate a new Access Token when the current one expires.

---

# CACHING FLOW

1. Client sends a request.
2. Cache Behavior checks Redis.
3. If cached data exists, it is returned immediately.
4. Otherwise, the request reaches the handler.
5. The response is stored in Redis.
6. Future requests are served directly from cache.

---

# LOGGING FLOW

1. Every incoming request is logged.
2. Execution time is tracked.
3. Exceptions are logged automatically.
4. Logs are written to both Console and Log Files using Serilog.

---

# HOW TO RUN PROJECT

```bash
git clone https://github.com/YOUR_USERNAME/CraftIQ.Inventory.git

cd CraftIQ.Inventory

dotnet restore

dotnet ef database update

dotnet run
```

---

# CONFIGURATION

Connection String

```json
"InventoryDBConnection": "your_sql_server_connection"
```

JWT Settings

```json
Key
Issuer
Audience
DurationInMinutes
```

Redis

Configure your Redis server inside:

```json
ConnectionStrings:Redis
```

---

# PROJECT STRUCTURE

```
API
│
├── Controllers
├── Middleware
├── Mapping

Application
│
├── Commands
├── Queries
├── Handlers
├── Validators
├── Behaviors

Core
│
├── Entities
├── Interfaces
├── Contracts

Infrastructure
│
├── DbContext
├── Identity
├── Repositories
├── Cache
├── Logging
```

---

# AUTHOR

**Mazen Osama**

Backend .NET Developer
