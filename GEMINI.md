# PokemonCardFinal Project Review

## Overview
**PokemonCardFinal** is an enterprise-level, N-Tier architecture application built using C# (.NET 8). It allows users to browse and interact with a Pokémon Trading Card Game (TCG) database. The system supports two presentation layers (a desktop WPF client and a web client) backed by a robust foundational structure consisting of data domain models, logical business rules, and a data-access layer connected directly to a SQL Server database using ADO.NET.

## Architecture & Structure
The solution uses a traditional multi-tiered structure separating concerns for maintainability and scalability:

- **Presentation Layer:**
  - `PokemonCardFinal` (WPF Application): A Windows desktop application with rich UI for accessing and filtering the Pokémon card database.
  - `Website` (ASP.NET Core Application): A web frontend, utilizing Identity for user credentials and an Entity Framework context purely for identity, while consuming the core logic layers for business data.

- **Business Logic Layer:**
  - `LogicLayer`: Contains the business rules, management classes (e.g., `CardManager`), and data processing pipelines.
  - `LogicLayerInterfaces`: Abstractions for logic implementation, facilitating dependency injection and testing.
  - `LogicLayerTest`: A suite of unit tests validating the business rules against fake data layers.

- **Data Access Layer:**
  - `DataAccess`: Implements database operations using direct ADO.NET (`Microsoft.Data.SqlClient`) combined with stored procedures to optimize interaction with the database.
  - `DataAccessInterfaces`: Defines standard contracts for data operations.
  - `DataAccessFakes`: Provides dummy data implementations to allow for isolated unit testing in the `LogicLayer`.

- **Domain Models:**
  - `DataDomain`: Houses entity classes like `Card`, `Ability`, `Move`, `PokemonRule`, and `User`. This layer serves as the data transfer layer across all tiers of the application without tying them to external technologies.

## Database
Located in the `tcg_db` directory, the application employs a set of SQL scripts to manage the database setup:
- `create_db.sql`: Schema definitions.
- `create_stored_procedures.sql`: Operations and complex join queries, ensuring that large-scale data manipulation is handled at the database level.
- `add_values.sql`: Seed scripts to populate initial Pokémon card sets, illustrators, and types.
- `db_script.bat`: A lightweight batch utility to auto-deploy the schema and procedures to a local SQL server context.

## Summary
The codebase exemplifies solid Object-Oriented Programming principles by isolating layers through dependency inversion and adhering to clean modular design, making it straightforward to add new clients, update data access technologies, or implement comprehensive mock testing.
