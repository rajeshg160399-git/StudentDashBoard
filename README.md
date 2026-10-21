# Student Dashboard

An ASP.NET Core MVC (.NET 8) student-management application using Entity Framework Core and SQL Server.

## Open in Visual Studio

Open `StudentDashboard.sln` in Visual Studio 2022, restore packages, then run the project. The included LocalDB connection string creates `StudentDashboardDb` automatically through the included migration.

If LocalDB is not installed, update `DefaultConnection` in `appsettings.json` to point to your SQL Server instance.

## Sign-in & Registration

- **Register New User**: Visit `/Account/Register` to register a new user account with secure PBKDF2 password hashing.
- **Default Administrator**:
  - Username: `admin`  
  - Password: `Admin@123`  
  *(Seeded automatically into the database with a cryptographically secure hash on first launch)*

## Features

- User registration with unique username/email validation and ASP.NET Core Identity password hashing
- Database-backed authentication (supports username or email login)
- Cookie-protected dashboard and navigation
- Student CRUD with anti-forgery protection, validation, and database constraint handling
- Entity Framework Core data-access/repository layer and SQL Server storage
- Bootstrap responsive layout
