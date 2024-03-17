
# EShop-API
The Source Code of the ASP.NET Core E-Commerce API

## Features
* Clean Architecture
* DDD Tactical Patterns
* CQRS using MediatR
* Multiple Database (SQL Server + Redis)
* Multiple ORM (EFCore + Dapper)
* FluentValidation
* Web API
* XSS Protected
* Caching
* Docker Enabled

## Packages
* AspNetCoreRateLimit `v5.0.0`
* AutoMapper.Extensions.Microsoft.DependencyInjection `v12.0.1`
* Microsoft.AspNetCore.Authentication.JwtBearer `v6.0.25`
* Microsoft.EntityFrameworkCore.Design `v6.0.25`
* Microsoft.Extensions.Caching.Redis `v2.1.2`
* Microsoft.Extensions.Caching.StackExchangeRedis `v6.0.26`
* Microsoft.VisualStudio.Azure.Containers.Tools.Targets `v1.18.1`
* Swashbuckle.AspNetCore `v6.2.3`
* UAParser `v3.1.47`
* FluentValidation `v10.4.0`
* FluentValidation.DependencyInjectionExtensions `v11.8.1`
* MediatR.Extensions.Microsoft.DependencyInjection `v11.1.0`
* Microsoft.Extensions.DependencyInjection.Abstractions `v8.0.0`
* Dapper `v2.1.24`
* Microsoft.EntityFrameworkCore.SqlServer `v6.0.25`
* Microsoft.EntityFrameworkCore.Tools `v6.0.25`
* MediatR `v12.2.0`

## Getting Started
To run the application:

1. Clone the Project
2. Open Visual Studio and load the Solution
3. Resolve any missing/required nuget package
4. Set `Shop.API` as startup project
5. Build Database. Open `Package Manager Console`, set `Shop.Infrastructure` as defualt project and run the following commands: `update-database`
6. That's all... Run the Project and enjoy it!
