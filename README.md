
![image](https://github.com/user-attachments/assets/af617c49-717d-4aef-92cf-7adb3d973728)


## Hello, everyone! Welcome to our Open Source project, WebAPI KRATOS. 🪓


> [!WARNING]
> **What is the Kratos project?**
>
> A powerful API developed with .NET 8 and clean architecture, following the best industry practices to ensure quality, scalability, and maintainability.
>
> The initial idea is to assist developers with simple operations, such as creating CRUDs, which can be time-consuming in day-to-day work. The focus of this project is precisely to optimize time, allowing us to dedicate more attention to more complex logic — something that Kratos cannot handle.

Regardless of the company or the industry, Kratos will always be ready to assist you in development with extremely high performance.

> [!IMPORTANT]
> For those who want to follow the development: https://github.com/users/thiagocorreanet/projects/31/views/1

### Project Structure

**API**
- **Controllers:** Project Endpoints
- **SwaggerExtension:** Class responsible for the custom configuration of Swagger.

**Application**
- **Commands:** Structure to use application commands such as create, update, or delete.
- **Mapping:** Class responsible for mapping objects.
- **Notification:** Application structure to notify the API that something went wrong and report it to our graphical interface.
- **Queries:** Structure to use application queries for database retrieval. Both commands and queries are part of the CQRS architecture pattern.
- **Validators:** Structure to perform validations using Fluent Validations.

**Core**
- **Auth:** Structure for authentication with JWT.
- **Entities:** Classes that will represent our entities.
- **Enums:** Project Enumerations
- **Repositories:** Interface contract for all repositories.
 
**Infrastructure**
- Auth: Logic to manage user access using claims.
- Migrations: EF Migration from C# structure => Database
- Persistence:
  - Configuration: Configuration structure for database fields.
  - Repositories: Repositories for data access.
 
Hope you are enjoying it! Come contribute to our project. ❤️

  
