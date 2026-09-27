# Conference Room Rental API

REST API for managing conference rooms, services, and room bookings.

The application allows users to create and manage conference rooms, configure available services, search for available rooms, create bookings, and calculate rental costs based on time-dependent pricing rules.

## Features

* Create, update, and soft-delete conference rooms.
* Create and manage additional services.
* Assign services to conference rooms.
* Search for available rooms by:

  * required capacity;
  * booking start time;
  * booking end time.
* Create room bookings.
* Validate booking time overlaps.
* Calculate rental costs based on time periods.
* Store service price snapshots for historical bookings.
* Centralized business exception handling.
* Request validation with FluentValidation.
* Automatic DTO ↔ entity mapping with AutoMapper.
* Swagger/OpenAPI documentation.

## Business Requirements

### Conference Rooms

A room contains:

* Name
* Capacity
* Hourly rental rate
* Available services

Example:

```text
Room: Conference Hall A
Capacity: 50
Hourly rate: 2000 UAH

Services:
- Projector — 500 UAH
- Wi-Fi — 300 UAH
```

### Bookings

A booking contains:

* Room
* Start time
* End time
* Hourly rate at the time of booking
* Selected services
* Total price

A room cannot have overlapping bookings.

The overlap condition is:

```text
existing.StartTime < requested.EndTime
AND
existing.EndTime > requested.StartTime
```

### Dynamic Pricing

The rental price depends on the booking time:

| Period      | Pricing      |
| ----------- | ------------ |
| 06:00–09:00 | 10% discount |
| 09:00–12:00 | Base price   |
| 12:00–14:00 | 15% markup   |
| 14:00–18:00 | Base price   |
| 18:00–23:00 | 20% discount |

Bookings that span multiple pricing periods are calculated hour by hour.

For example:

```text
10:00–12:00 → base price
12:00–14:00 → +15%
14:00–18:00 → base price
18:00–20:00 → -20%
```

### Historical Service Prices

When a booking is created, the current service price is stored in `BookingService.Price`.

This prevents historical bookings from being affected when the original service price is changed later.

```text
Service
Price = 500 UAH
       ↓
Booking created
       ↓
BookingService
Price = 500 UAH
       ↓
Service price changes to 700 UAH
       ↓
Existing booking still uses 500 UAH
```

## Architecture

The project follows a layered architecture:

```text
┌─────────────────────────┐
│          API            │
│ Controllers / Middleware│
└────────────┬────────────┘
             │
             ▼
┌─────────────────────────┐
│          BLL            │
│ Business Logic / DTOs   │
│ Validation / Mapping    │
└────────────┬────────────┘
             │
             ▼
┌─────────────────────────┐
│          DAL            │
│ Repositories / UoW      │
│ EF Core / DbContext     │
└────────────┬────────────┘
             │
             ▼
        SQL Server
```

### API

Responsible for:

* HTTP endpoints;
* request/response handling;
* controllers;
* middleware;
* Swagger/OpenAPI.

### BLL

Responsible for:

* business rules;
* DTOs;
* FluentValidation;
* AutoMapper;
* pricing calculation;
* booking availability;
* business exceptions.

### DAL

Responsible for:

* EF Core;
* database access;
* repositories;
* Unit of Work;
* entity configurations;
* migrations;
* database seeding.

## Project Structure

```text
TestTask
│
├── TestTask.API
│   ├── Controllers
│   ├── Middleware
│   └── Program.cs
│
├── TestTask.BLL
│   ├── DTOs
│   ├── Exceptions
│   ├── Interfaces
│   ├── Mapping
│   ├── Services
│   └── Validators
│
├── TestTask.DAL
│   ├── Data
│   ├── Entities
│   ├── Interfaces
│   ├── Repositories
│   ├── Specifications
│   ├── Configurations
│   └── Migrations
│
└── TestTask.sln
```

## Database
Database Diagram : https://dbdiagram.io/d/TestTask-6ab18fd29ba2420e18d4f42f
The application uses SQL Server with Entity Framework Core.

Main entities:

```text
Room
Service
Booking
RoomService
BookingService
```

Relationships:

```text
Room 1 ─────── * Booking

Room 1 ─────── * RoomService * ─────── 1 Service

Booking 1 ──── * BookingService * ───── 1 Service
```

`RoomService` and `BookingService` represent many-to-many relationships.

`BookingService` additionally stores the service price used when the booking was created.

## Data Access

The DAL uses:

* Entity Framework Core
* Generic Repository
* Specific repositories
* Unit of Work
* Specification Pattern
* Global Query Filters
* Soft Delete

### Soft Delete

Entities inherit from `BaseEntity`:

```csharp
public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
}
```

Deleting an entity does not physically remove it from the database.

Instead:

```text
Delete()
   ↓
IsDeleted = true
   ↓
SaveChangesAsync()
```

Global query filters automatically exclude soft-deleted entities from normal queries.

## Validation

FluentValidation is used to validate incoming DTOs.

Examples of validation rules:

* Room name cannot be empty.
* Capacity must be greater than zero.
* Hourly rate must be greater than zero.
* Service price cannot be negative.
* Booking end time must be after start time.

Common validation rules are implemented as reusable `IRuleBuilder` extension methods.

## Exception Handling

Business exceptions are handled centrally by `ExceptionHandlingMiddleware`.

| Exception             | HTTP Status |
| --------------------- | ----------: |
| `BadRequestException` |         400 |
| `NotFoundException`   |         404 |
| `ConflictException`   |         409 |

The API returns consistent JSON error responses instead of exposing internal exception details.

## Technologies

* **C#**
* **.NET**
* **ASP.NET Core Web API**
* **Entity Framework Core**
* **SQL Server**
* **AutoMapper**
* **FluentValidation**
* **Bogus**
* **Swagger / OpenAPI**
* **Git / GitHub**

## Getting Started

### Requirements

Install:

* .NET SDK
* SQL Server
* Git

### Clone the repository

```bash
git clone <repository-url>
cd TestTask
```

### Configure Database Connection

The database connection string is stored using **.NET User Secrets** and is not committed to the repository.

Initialize User Secrets for the API project:

```bash
dotnet user-secrets init --project TestTask.API
```

Set the connection string:

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "<your-connection-string>" --project TestTask.API
```

### Apply Migrations

Run:

```bash
dotnet ef database update --project TestTask.DAL --startup-project TestTask.API
```

### Run the Application

```bash
dotnet run --project TestTask.API
```

After starting the application, Swagger can be used to explore and test the API.

## Database Seeding

The application includes a database seeder based on **Bogus**.

The seeder can generate:

* rooms;
* services;
* room-service relationships;
* bookings;
* booking-service relationships.

Seed data is generated automatically when the API starts if the database does not already contain rooms.

The number of generated records can be configured through the seeder parameters.

## API Endpoints

### Rooms

```text
POST   /api/rooms
GET    /api/rooms/{id}
PUT    /api/rooms/{id}
DELETE /api/rooms/{id}
GET    /api/rooms/available
```

### Services

```text
POST   /api/services
GET    /api/services/{id}
PUT    /api/services/{id}
DELETE /api/services/{id}
```

### Bookings

```text
POST   /api/bookings
```

For detailed request and response schemas, use Swagger/OpenAPI.

## Design Decisions

### Unit of Work

The Unit of Work coordinates changes across multiple repositories and commits them through a single `SaveChangesAsync()` operation.

This is particularly useful for operations involving multiple related entities, such as creating a room together with its `RoomService` relationships.

### Specification Pattern

Specifications encapsulate reusable query conditions.

For example, room availability can combine:

```text
Room capacity requirement
        +
Room is not deleted
        +
No overlapping bookings
```

This keeps complex filtering logic out of repositories and business services.

### DTOs

DTOs prevent DAL entities from being directly exposed through the API.

They also allow API contracts to evolve independently from database entities.

### AutoMapper

AutoMapper handles straightforward DTO/entity transformations while business-specific relationship logic remains inside the BLL.

## Development Workflow

The project uses feature branches and Pull Requests.

Example:

```text
main
 │
 ├── feature/domain-entities
 │
 ├── feature/repositories
 │
 ├── feature/bll
 │
 └── feature/api
```

Each feature is developed separately, reviewed through a Pull Request, and merged into `main`.

## Status

The core functionality of the Conference Room Rental API is implemented, including:

* database layer;
* repositories and Unit of Work;
* specifications;
* DTOs;
* validation;
* mapping;
* business services;
* dynamic pricing;
* exception handling;
* REST API controllers;
* database seeding.

The project is ready for further improvements such as authentication, authorization, automated tests, logging, and production deployment.
