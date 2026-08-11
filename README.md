# 🏠 RealEstatePlatform

> A modern multi-layered Real Estate Management System built with **ASP.NET Core MVC**, **ASP.NET Core Web API**, **Clean Architecture**, **Entity Framework Core**, **MediatR**, **ASP.NET Core Identity**, **JWT Authentication**, and **SQL Server**.

---

## 🚀 Overview

RealEstatePlatform is a full-stack real estate management solution designed using **Clean Architecture** principles.

The system provides both:

- 🌐 ASP.NET Core MVC Web Application
- 🔌 ASP.NET Core Web API

The platform enables administrators, developers, agents, and clients to manage real estate properties efficiently through a secure and scalable architecture.

---

# ✨ Features

## 👤 User Management

- User Registration
- Login & Logout
- Email Confirmation
- Password Recovery
- Role-Based Authorization
- User Profile Management

### Supported Roles

- Administrator
- Developer
- Agent
- Client

---

## 🏡 Property Management

- Create Property
- Update Property
- Delete Property
- Property Details
- Property Gallery
- Property Types
- Property Improvements
- Property Sales Types
- Property Filtering
- Property Search

---

## 🔐 Security

- ASP.NET Core Identity
- JWT Authentication
- Role-Based Authorization
- Password Hashing
- Protected API Endpoints

---

## 📧 Email Services

- Email Confirmation
- Password Reset Emails
- SMTP Integration using MailKit

---

## 📖 API

- RESTful API
- Swagger Documentation
- JSON Responses
- JWT Protected Endpoints

---

# 🏗 Architecture

The project follows **Clean Architecture**.

```text
                Client
                   │
      ┌────────────┴────────────┐
      │                         │
 ASP.NET MVC                REST API
      │                         │
      └────────────┬────────────┘
                   │
           Core.Application
           (Business Logic)
                   │
           MediatR Handlers
                   │
            Infrastructure
        (Persistence / Identity)
                   │
              SQL Server
```

---

# 📁 Solution Structure

```text
RealEstatePlatform

├── Core
│   ├── Domain
│   └── Application
│
├── Infrastructure
│   ├── Persistence
│   ├── Identity
│   └── Shared
│
├── Presentation
│   ├── WebApp
│   └── WebApi
```

---

# 🛠 Technologies

| Category | Technology |
|-----------|------------|
| Language | C# |
| Framework | .NET 10 |
| UI | ASP.NET Core MVC + Razor Views |
| API | ASP.NET Core Web API |
| ORM | Entity Framework Core |
| Database | SQL Server |
| Authentication | ASP.NET Core Identity |
| Authorization | JWT |
| Mapping | AutoMapper |
| Validation | FluentValidation |
| Pattern | Clean Architecture |
| CQRS | MediatR |
| API Docs | Swagger |
| Email | MailKit |

---

# 🔄 Application Flow

```text
Browser
↓
MVC Controller / API Controller
↓
Application Layer
↓
MediatR
↓
Business Logic
↓
Repository
↓
Entity Framework Core
↓
SQL Server
```

---

# ⚙ Getting Started

## Clone the repository

```bash
git clone https://github.com/abdelhamed-mo/RealEstatePlatform.git
```

---

## Restore packages

```bash
dotnet restore
```

---

## Build the solution

```bash
dotnet build
```

---

## Configure the application

Update the following settings inside:

```
appsettings.Development.json
```

Configure:

- SQL Server Connection String
- JWT Secret
- SMTP Settings

---

## Apply Database Migrations

```bash
dotnet ef database update
```

---

## Run the project

```bash
dotnet run
```

---

# 🔐 Authentication

The application uses:

- ASP.NET Core Identity
- JWT Authentication
- Role-Based Authorization

---

# 📦 Main Packages

- ASP.NET Core MVC
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- MediatR
- AutoMapper
- FluentValidation
- MailKit
- ASP.NET Core Identity
- Swashbuckle

---

# 🚀 Future Improvements

- Docker Support
- Azure Deployment
- Unit Testing
- Integration Testing
- Redis Caching
- SignalR Notifications
- CI/CD Pipeline
- Logging with Serilog
- File Storage using Azure Blob Storage

---

# 👨‍💻 Author

**Abdelhamed Mohamed**

Backend .NET Developer

Email:
abdelhamed.dev@gmail.com

LinkedIn:
https://www.linkedin.com/in/abdelhamed-mo

GitHub:
https://github.com/abdelhamed-mo

---

# ⭐ If you like this project

Please consider giving it a ⭐ on GitHub.
