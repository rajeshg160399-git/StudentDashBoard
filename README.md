# Student Dashboard

An ASP.NET Core MVC (.NET 8) student-management application using Entity Framework Core and SQL Server.

## Open in Visual Studio

Open `StudentDashboard.sln` in Visual Studio 2022, restore packages, then run the project. The included LocalDB connection string creates `StudentDashboardDb` automatically through the included migration.

If LocalDB is not installed, update `DefaultConnection` in `appsettings.json` to point to your SQL Server instance.

## Demo sign-in

Username: `admin`  
Password: `Admin@123`

For production, replace the demo credential check in `Controllers/AccountController.cs` with ASP.NET Core Identity or your organisation's identity provider.

## Features

- Cookie-protected dashboard and navigation
- Student CRUD with anti-forgery protection, validation, and database constraint handling
- Entity Framework Core data-access/repository layer and SQL Server storage
- Bootstrap responsive layout
