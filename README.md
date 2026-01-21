# CarRental-MajorProject

## Project Description

**CarRental** is a dynamic and user-friendly web application built with ASP.NET Core, designed to simplify the car rental experience. Users can easily browse, select, and rent vehicles according to their requirements with a seamless booking and management system.

This project has been **built completely from scratch** with a clean architecture, following industry best practices and design patterns.

## Key Features

- **Vehicle Management**: Browse cars by Brand and Vehicle Type
- **User Authentication**: Secure login and registration system with role-based access
- **Booking System**: Book vehicles with intuitive date and time selection
- **Booking Management**: Modify, view, and manage existing bookings
- **Contact System**: Users can send messages and queries to the admin
- **Invoice Generation**: Automated invoice creation for bookings
- **Admin Panel**: Administrative dashboard for managing vehicles, brands, types, and user inquiries
- **Payment Options**: Cash on Delivery (COD) payment method supported
- **User-Friendly Interface**: Responsive and intuitive UI for enhanced user experience

## Project Architecture

- **CarRental**: Main ASP.NET Core MVC web application
- **CarRental_Model**: Domain models and DTOs (Data Transfer Objects)
- **CarRental_DataAccess**: Data access layer with Entity Framework Core and Repository pattern
- **CarRental_Utility**: Utility functions and helper methods

## Technology Stack

- **Backend**: ASP.NET Core MVC (.NET Framework)
- **Database**: SQL Server with Entity Framework Core
- **Frontend**: Razor Views with HTML/CSS/JavaScript
- **Pattern**: Repository Pattern for data access
- **Authentication**: ASP.NET Core Identity

## Database

The application includes comprehensive database migrations with the following main entities:
- **Users**: User profiles with authentication
- **Vehicles**: Car inventory management
- **Brands**: Car brand information
- **Vehicle Types**: Car category classification
- **Bookings**: Rental booking records
- **Contact Messages**: User inquiries and feedback

## Getting Started

1. Clone the repository
2. Update the database connection string in `appsettings.json`
3. **Delete the Migration Folder** in `CarRental_DataAccess\Migrations` (Important: This step is required to avoid migration conflicts)
4. Run database migrations: `add-migration "Field Added"`
5. Update the Database: `update-database`
6. Build the solution: `dotnet build`
7. Run the application: `dotnet run`

## Database Setup - Important Notes

### Creating Roles
After the database is updated, you need to create the required roles manually. Run the following commands in **Package Manager Console** (Tools > NuGet Package Manager > Package Manager Console):

```
add-migration "RoleInitialization"
update-database
```

Then execute the following SQL query in SQL Server to create the User and Admin roles:

```sql
INSERT INTO AspNetRoles (Id, Name, NormalizedName, ConcurrencyStamp) VALUES 
(NEWID(), 'User', 'USER', NEWID()),
(NEWID(), 'Admin', 'ADMIN', NEWID());
```

### Migration Conflict Warning
If you encounter migration conflicts:
- The application uses **Entity Framework Core** with **ASP.NET Core Identity**
- Make sure **not to run migrations simultaneously** in multiple instances
- If conflicts occur, delete the last migration file and recreate it:
  ```
  remove-migration
  add-migration "YourMigrationName"
  update-database
  ```
- Ensure the `CarRental_DataAccess.csproj` is set as the default project in Package Manager Console

## License

This is a major project for .NET Lecture series.
