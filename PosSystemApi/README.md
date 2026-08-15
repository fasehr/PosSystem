# POS System API — Assignment 2

A Point of Sale (POS) REST API built using **ASP.NET Core Web API and .NET 10**. This assignment converts the original console-based POS system into a RESTful API using controllers, services, interfaces, dependency injection, DTOs, and Swagger documentation.

## Features

* Product management

  * View all products
  * Find product by SKU
  * Add products
  * Delete products
* Shopping cart management

  * Add products to cart
  * Update quantities
  * Remove items
  * Clear cart
  * Undo the last add operation
* Checkout processing
* Stock quantity updates after checkout
* Order history
* Product loading from CSV at application startup
* DTOs for cart requests
* Dependency Injection using service interfaces
* Swagger/OpenAPI documentation and API testing
* Validation and exception handling

## Technologies Used

* C#
* .NET 10
* ASP.NET Core Web API
* Swagger / OpenAPI
* Swashbuckle
* Git & GitHub

## Architecture

```text
HTTP Request
    ↓
Controller
    ↓
Service Interface
    ↓
Service Implementation
    ↓
In-Memory Collections
```

The application follows a service-based structure where controllers handle HTTP requests and delegate business logic to services through interfaces.

## Data Storage

Assignment 2 uses **in-memory data structures** rather than a database.

* `List<Product>` stores products
* `Dictionary<string, Product>` provides SKU-based product lookup
* `Cart` stores the current shopping cart
* `Stack<CartItem>` supports undo functionality
* `Queue<Order>` stores completed orders

Products are initially loaded from `Data/Products.csv` when the application starts.

> Data stored in memory is cleared when the application stops. Database persistence is introduced in the next assignment.

## API Modules

### Products

```text
GET     /api/products
GET     /api/products/{sku}
POST    /api/products
DELETE  /api/products/{sku}
```

### Cart

```text
GET     /api/cart
POST    /api/cart
PUT     /api/cart/{sku}
DELETE  /api/cart/{sku}
DELETE  /api/cart
POST    /api/cart/undo
```

### Checkout

```text
POST    /api/checkout
```

### Orders

```text
GET     /api/orders
```

## Dependency Injection

The application registers its services through ASP.NET Core's dependency injection container:

```text
IProductService  → ProductService
ICartService     → CartService
ICheckoutService → CheckoutService
```

This allows controllers to depend on service contracts instead of directly creating concrete service objects.

## Product Loading

`ProductLoader` reads product records from:

```text
Data/Products.csv
```

when the application starts and loads them into the product service.

## Swagger

Swagger UI is enabled in the Development environment and can be used to view and test all API endpoints.

## Project Structure

```text
PosSystemApi
├── Controllers
├── DTOs
├── Data
├── Helpers
├── Models
├── Services
├── Program.cs
└── appsettings.json
```

## Assignment Progression

This branch represents **Assignment 2**, where the original POS console application was converted into an ASP.NET Core REST API.

The next assignment extends this architecture by introducing persistent database storage and Entity Framework Core.
