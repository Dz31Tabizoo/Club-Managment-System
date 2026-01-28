# 🏅 Elite Sports Club Management System (CMS)

A robust, high-performance Enterprise Resource Planning (ERP) solution for sports clubs. This system is designed to manage everything from player registrations and technical coaching to financial subscriptions and team scheduling.

Built with **ASP.NET Core 8 Web API** and powered by **Dapper (Micro-ORM)** for maximum efficiency.

---

## 🏗️ Architectural Overview
The project follows the **N-Tier Architecture** (Layered Architecture) to ensure a separation of concerns and maintainability:

1. **Presentation Layer (Web API):** RESTful endpoints using ASP.NET Core 8.
2. **Business Logic Layer (BLL):** Contains the core "Club Rules," validations, and service coordination.
3. **Data Access Layer (DAL):** High-speed database operations using **Dapper**, **T-SQL**, and the **Repository Pattern**.
4. **DTOs Layer (Common):** Shared Data Transfer Objects and Models, utilizing **C# Inheritance** for entity relations.

## 🛠️ Tech Stack & Key Concepts
- **Framework:** .NET 8.0
- **ORM:** Dapper (Chosen for lightning-fast execution and full control over T-SQL).
- **Design Patterns:** Repository Pattern with **Interfaces** and **Dependency Injection (DI)**.
- **Database:** SQL Server (Complex schema handling inheritance and relational integrity).
- **Logic:** Data-Driven "Smart Save" (ID-based persistence logic).

## 📊 Database Schema Entities
The system handles a wide range of club-specific entities:
- **Core Entities (Inheritance-based):** - `Persons`: The base for all human entities.
  - `Users`: System administrators with role-based access.
  - `Players`: Detailed athlete profiles and stats.
  - `Coaches`: Specialized staff management.
- **Management Entities:**
  - `Teams`: Grouping players under specific coaches.
  - `Categories`: Sport types (Football, Swimming, etc.).
  - `Subscriptions`: Membership tracking and duration management.
  - `Payments`: Financial transaction logging and revenue tracking.
  - `Attendance`: Session-based tracking for players and staff.

## 🚀 Professional Implementation Progress
- [x] **Project Structure:** Fully decoupled N-Tier solution setup.
- [x] **Advanced Mapping:** Implementation of Class Inheritance in C# matching SQL Schema.
- [x] **Dapper Integration:** Direct T-SQL execution for high-performance data retrieval.
- [x] **Clean Code:** Use of Interfaces to decouple Data Access from Business Logic.
- [ ] **Smart Persistence:** Automatic Add/Update detection without explicit Mode flags.
- [ ] **Complex Joins:** Reporting services for unpaid subscriptions and team rosters.

## ⚙️ Setup and Installation
1. Clone the repository.
2. Configure your SQL Server connection string in `appsettings.json`.
3. Run the provided T-SQL scripts to generate the schema and stored procedures.
4. Run the API project and use the **Swagger UI** to test endpoints.

---
*This project is part of a professional challenge to master Micro-ORMs and Clean Architecture in modern .NET environments.*
