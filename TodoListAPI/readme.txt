  _______        _       _      _     _   
 |__   __|      | |     | |    (_)   | |  
    | | ___   __| | ___ | |     _ ___| |_ 
    | |/ _ \ / _` |/ _ \| |    | / __| __|
    | | (_) | (_| | (_) | |____| \__ \ |_ 
    |_|\___/ \__,_|\___/|______|_|___/\__|                                                                     


1. NuGet EF Packages:
Install-Package Microsoft.EntityFrameworkCore
Install-Package Microsoft.EntityFrameworkCore.Design
Install-Package Microsoft.EntityFrameworkCore.SqlServer
Install-Package Microsoft.EntityFrameworkCore.Tools

Microsoft.EntityFrameworkCore.Design:
Purpose: This package contains shared design-time components used both by Entity Framework Core commands and by design-time tools in various IDEs. These components enable operations such as migrations and scaffolding.
Common Scenarios:
Scaffolding a DbContext and entity classes from an existing database (Database First approach).
Generating migration files from changes in the DbContext and entity classes.
Other design-time operations that involve interaction between your entity model and database.

Microsoft.EntityFrameworkCore.Tools:
Purpose: This package provides additional tools specifically for use with the Package Manager Console (PMC) inside Visual Studio and the .NET CLI. It contains commands for EF Core that help in managing the database, migrations, and other related tasks.
Common Scenarios:
Running commands like Add-Migration, Update-Database, and Remove-Migration within the Package Manager Console in Visual Studio.
Using dotnet ef commands from the .NET CLI, such as dotnet ef migrations add, dotnet ef database update, and more.
Scaffolding a DbContext from an existing database via the CLI.


2. Scaffolding
Scaffold-DbContext "Server=.;Database=TodoList;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models


3. Update appsettings.json
  "ConnectionStrings": {
    "TodoListConnectionString": "Server=.;Database=TodoList;Trusted_Connection=True;TrustServerCertificate=True;"
  }


...

4. dotnet run --urls "https://localhost:5099"

...


X. Validation https://docs.fluentvalidation.net/en/latest/
• Use model validation in minimal APIs in ASP.NET Core 6 https://www.infoworld.com/article/3676077/use-model-validation-in-minimal-apis-in-aspnet-core-6.html
• Working with model validation in Minimal APIs https://dotnetthoughts.net/working-model-validation-in-minimal-api/
Tip! 
 - OnModelCreating in Entity Framework (both in EF Core and EF6) is primarily used for configuring how your POCO classes map to database tables. It allows you to specify details like table names, column names, relationships, indices, constraints, and other database-specific configurations.
 - Validation: To validate data at the application level (before it gets saved to the database or even before it reaches a controller action), you'd typically use data annotations on your POCO properties or use libraries like FluentValidation. This kind of validation can ensure business rules, data consistency, and prevent unwanted data from ever reaching deeper layers of your application.


Z. JWT
Authentication and authorization in minimal APIs
https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis/security?view=aspnetcore-7.0

Authorization and Authentication in Minimal APIs
https://www.telerik.com/blogs/authorization-authentication-minimal-apis

ASP.NET Core - Custom Token Authentication
https://dev.to/kazinix/aspnet-core-custom-token-authentication-2j9a

ASP.NET Core - Write a Simple JWT Authentication
https://dev.to/kazinix/aspnet-core-write-a-simple-jwt-authentication-and-authorization-7nk

Windows (using PowerShell):
Use the following command to generate a 256-bit key encoded in base64:
[Convert]::ToBase64String((1..32 | ForEach-Object { Get-Random -Minimum 0 -Maximum 256 })).Substring(0, 44)
