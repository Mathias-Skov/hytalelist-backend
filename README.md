# Backend API - ASP.NET Core & Entity Framework Core
This project is a RESTful backend API built with ASP.NET Core and Entity Framework Core for database access.

![.NET](https://img.shields.io/badge/.NET-9.0-purple)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-15-blue)
## Features
- RESTful API endpoints following standard HTTP conventions
- Full CRUD functionality (Create, Read, Update, Delete)
- Entity Framework Core for database access and migrations
- Dependency Injection for loose coupling and testability
- Layered architecture (Controller → Service → Repository)
- Data Transfer Objects (DTO) for clean API contracts
- JWT-based authentication and authorization

## Architecture Overview

Client → Cloudflare → Nginx → ASP.NET API → EF Core → PostgreSQL
- ASP.NET Core Web API
- Clean layered architecture (Controller → Service → Repository → Data)
- EF Core Code-First approach with migrations
- JWT-based authentication
- PostgreSQL relational database
- Deployed on a Linux dedicated server behind Nginx

## Technologies
- ASP.NET Core
- C#
- Entity Framework Core
- PostgreSQL (via Npgsql EF Core provider)

## Getting started

Clone the repository
```git clone https://github.com/Mathias-Skov/hytalelist-backend.git```

Edit appsettings.json for your configuration
```{
  "Jwt": {
    "Key": "<your-secret-key>",
    "Issuer": "HytaleList.Api",
    "Audience": "HytaleList.Frontend"
  },
  "ConnectionStrings": {
    "DefaultConnection": "Host=127.0.0.1;Port=5432;Database=hytalelist;Username=hytalelist;Password=password"
  },
  "Discord": {
    "WebhookUrl": "<your-webhook-url>"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

Run migrations

```dotnet ef database update```
Build and run the project

Example Endpoints
```
### Server
GET    /api/server
GET    /api/server/{id}
POST   /api/server

### User
POST   /api/user/login
GET    /api/user/details (JWT required)

### Vote
GET    /api/vote/count/{serverId}
POST   /api/vote/{serverId}
```

## Live demo

https://hytalelist.io/

Currently runs on my Debian dedicated server with PM2, Nginx and Cloudflare as CDN.

## Link for GitHub repository for the frontend

https://github.com/Mathias-Skov/hytalelist-frontend

## Roadmap / TODO
- Add Redis caching layer to improve performance and reduce database load
- Implement unit and integration tests
- Add rate limiting and request throttling
- Introduce CI/CD pipeline with GitHub Actions
- Containerize services using Docker and Docker Compose
- Implement role-based authorization (Admin/User)
