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
