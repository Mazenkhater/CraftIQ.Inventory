CraftIQ.Inventory

Clean Architecture Inventory System built with ASP.NET Core, Identity, JWT, CQRS, MediatR, and Entity Framework Core.

This project demonstrates a real-world backend system designed using Clean Architecture principles with separation of concerns between Core, Application, Infrastructure, and API layers.

------------------------------------------------------------

PROJECT OVERVIEW

CraftIQ.Inventory is an inventory management system that handles products, categories, orders, inventory tracking, and transactions with secure authentication and authorization.

The system is built to be scalable, maintainable, and testable using modern .NET practices.

------------------------------------------------------------

ARCHITECTURE

The solution follows Clean Architecture:

- API Layer → Controllers, Middleware, Swagger
- Application Layer → CQRS (Commands & Queries), Handlers, DTOs
- Core Layer → Entities, Interfaces, Contracts
- Infrastructure Layer → EF Core, Identity, Repositories, JWT

Dependency rule: Inner layers do not depend on outer layers.

------------------------------------------------------------

FEATURES

Authentication:
- User registration and login
- JWT authentication
- Refresh token support
- Forgot and reset password

Inventory Management:
- CRUD operations for products and categories
- Inventory tracking
- Search, filtering, and pagination

Orders:
- Order creation and details management
- Relationship mapping between products and orders

Transactions:
- Transaction logging and tracking

Performance :
- Redis Distributed Caching
- MediatR Caching Pipeline Behavior

Logging :
- Structured Logging using Serilog
- Request & Exception Logging
- File & Console Logging

------------------------------------------------------------

TECH STACK

- ASP.NET Core Web API
- Entity Framework Core
- ASP.NET Core Identity
- MediatR (CQRS)
- SQL Server
- JWT Authentication
- Redis Distributed Cache
- Serilog
- FluentValidation
- Swagger / OpenAPI

------------------------------------------------------------

DATABASE ENTITIES

- Product
- Category
- Inventory
- Order
- OrderDetail
- Transaction
- RefreshToken

------------------------------------------------------------

AUTHENTICATION FLOW

1. User logs in using email and password
2. System generates JWT access token
3. Refresh token is stored in database
4. Access token is used for API authorization
5. Refresh token is used to regenerate new access token

------------------------------------------------------------

HOW TO RUN PROJECT

git clone https://github.com/YOUR_USERNAME/CraftIQ.Inventory.git

cd CraftIQ.Inventory

dotnet restore

dotnet ef database update

dotnet run

------------------------------------------------------------

CONFIGURATION

Connection String:
"InventoryDBConnection": "your_sql_server_connection"

JWT Settings:
Key, Issuer, Audience, DurationInMinutes

------------------------------------------------------------

AUTHOR

Mazen Osama

Backend Developer | ASP.NET Core | Clean Architecture Enthusiast
