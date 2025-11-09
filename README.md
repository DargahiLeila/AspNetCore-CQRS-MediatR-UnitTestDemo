# 🧠 AspNetCore-CQRS-MediatR-UnitTestDemo

This project demonstrates a clean implementation of the CQRS (Command Query Responsibility Segregation) pattern using MediatR in an ASP.NET Core MVC application. It also includes a separate unit test project (DLL) to validate command and query behaviors.

## 🎯 Project Purpose

- Showcase CQRS with MediatR in ASP.NET Core

- Apply layered architecture (N-Tier) for clean separation of concerns

- Demonstrate unit testing of CQRS handlers using a dedicated test project

## ⚙️ Technologies Used
- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- MediatR
- xUnit (or your test framework)
- N-Tier Architecture (UI, BLL, DAL, Domain)
- Separate Unit Test DLL

📁 Project Structure

├── AspNetCore-CQRS-MediatR-UnitTestDemo/

│   ├── Domain/           → Core entities and interfaces

│   ├── DAL/              → Data access layer

│   ├── BLL/              → Business logic + MediatR handlers

│   ├── UI/               → ASP.NET Core MVC frontend

│   └── Tests/            → Separate DLL for unit tests

🚀 How to Run
1.Clone the repository:
  ```bash
git clone https://github.com/DargahiLeila/AspNetCore-CQRS-MediatR-UnitTestDemo.git
2.Open the solution file (.sln) in Visual Studio 2022 or later.

3.Make sure your SQL Server instance is running.

4.Create two SQL Server databases manually:

MyApp_WriteDB → for write operations

MyApp_ReadDB → for read operations

5.Create the required table in both databases:

CREATE TABLE [dbo].[TBL_Users] (
    [Id] INT PRIMARY KEY IDENTITY(1,1),
    [Name] NVARCHAR(50),
    [IsDeleted] BIT NOT NULL
);

6.Update your appsettings.json with the correct connection strings:

"ReadConnectionString": "Data Source=YourServer;Initial Catalog=MyApp_ReadDB;User Id=your_user;Password=your_password;",

"WriteConnectionString": "Data Source=YourServer;Initial Catalog=MyApp_WriteDB;User Id=your_user;Password=your_password;"


7.Run the project:

Press Ctrl + F5 or click Start Without Debugging

🔄 Background Job: Sync Write → Read

A background job runs every 1 minute to sync data from MyApp_WriteDB to MyApp_ReadDB:

SET NOCOUNT ON;

BEGIN TRY
    BEGIN TRAN;

    TRUNCATE TABLE MyApp_ReadDB.dbo.TBL_Users;

    INSERT INTO MyApp_ReadDB.dbo.TBL_Users (Id, Name, IsDeleted)
    SELECT Id, Name, IsDeleted
    FROM MyApp_WriteDB.dbo.TBL_Users;

    COMMIT TRAN;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRAN;

    DECLARE @Err NVARCHAR(4000) = ERROR_MESSAGE();
    RAISERROR(N'Sync Users failed: %s', 16, 1, @Err);
END CATCH;


You can schedule this job using SQL Server Agent or any external scheduler.

🧪 Unit Testing

- All CQRS handlers (commands and queries) are tested using a separate test project (DLL).

- Tests are written using xUnit and cover:

- Command validation

- Query results

- Business logic edge cases

To run tests:

- Right-click the Tests project in Visual Studio

- Select Run Tests
