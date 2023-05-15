# FlowerShop
 
## Setting up the Database

### Requirements

 - Docker
 - Npgsql.EntityFrameworkCore.PostgreSQL NuGet package

### Setup

In NuGet Package Manager Console run:

    docker run --name flowershop -e POSTGRES_PASSWORD=[YOUR_DB_PASSWORD]-p 5432:5432 -d postgres:latest

update `appsetings.Development.json` based on parameters used during DB set up in Docker

    "ConnectionStrings": {
        "Defaultconnection": "Server=localhost; Port=5432; Database=postgres; User Id=postgres; Password=[YOUR_DB_PASSWORD];"
      }

In NuGet Package Manager Console run:

    database ef update
