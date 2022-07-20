# <p align="center"> QuickRent</p>

Car rental project written in accordance with SOLID principles. Used technologies and techniques in this project: </br>
DOTNET framework, Resful API, SQL Server, Entity Framework, Dependency Injection and AOP by Autofac, FluentValidation, JWT.

## Project Installation
> :warning: Please do the installation in order!
- Clone the repository
- Open the project in Visual Studio
- Install database

### Database Installation

- Find your SQL Server name in the "SQL Server Object Explorer" then change server name in "DataAccess/Concrete/EntityFramework/CarRentDbContext.cs
- Open cmd or any command shell in DataAccess file</br>
- Run ```dotnet tool install --global dotnet-ef``` command to install .Net Entity Framework CLI</br>
- Run ```dotnet ef database update``` to create the database</br>

## How to use
- Set WebAPI project as Startup Project and start the project
- Navigate ```https://localhost:44357/swagger/index.html``` to view or use swagger documentation.

## Frontend of this project: https://github.com/lyyvig/QuickRent


