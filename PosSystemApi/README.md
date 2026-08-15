# POS System API

A Point of Sale REST API built using ASP.NET Core Web API, Entity Framework Core, and SQL Server.

## Features

- Product Management
- Customer Management
- Shopping Cart
- Checkout and Order Management
- SQL Server Persistence with Entity Framework Core
- Async/Await Database Operations
- JWT Authentication
- Role-Based Authorization
  - Admin
  - Salesman
- Request Logging Middleware
- Product Filtering
- Order Filtering
- Swagger/OpenAPI Documentation
- JWT Authentication through Swagger

## Technologies Used

- C#
- ASP.NET Core Web API
- .NET 10
- Entity Framework Core
- SQL Server
- JWT Authentication
- Swagger / OpenAPI
- BCrypt
- Git & GitHub

## Architecture

Controllers
↓
Services / Interfaces
↓
Entity Framework Core
↓
SQL Server

Additional components:

- DTOs
- Authentication
- Custom Middleware
- Role-Based Authorization
- EF Core Migrations

## Roles

### Admin
- View products
- Add products
- Delete products
- Manage customers
- Use cart and checkout
- View orders

### Salesman
- View products
- Manage customers
- Use cart and checkout
- View orders
- Cannot perform Admin-only product management operations

## API Modules

- `/api/Auth`
- `/api/Products`
- `/api/Customers`
- `/api/Cart`
- `/api/Checkout`
- `/api/Orders`

## Database

The application uses SQL Server with Entity Framework Core.

Current database tables include:

- Products
- Customers
- Carts
- CartItems
- Orders
- Users
- Logs

Database schema changes are managed using EF Core migrations.

## Current Status

Core API implementation and database integration are complete.

Currently completing:
- End-to-end API testing
- Postman testing
- Final cleanup