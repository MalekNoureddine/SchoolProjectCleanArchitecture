# School Management System API (Clean Architecture)
## 📚 Overview
This project is a **School Management System API** built using **Clean Architecture** principles. It focuses on implementing best practices for a scalable and maintainable back-end solution. The system uses **CQRS** (Command Query Responsibility Segregation) to separate read and write operations and employs MediatR for request handling.

## Key Features
- **Clean Architecture** with modular structure:
  - **Core**:
    - **Features** : Command and query Modles(requests) entities and responses entities, validation and Handlers for each entity.
    - **Mapper** : for mapping responses and requests to enitities.
    - **Behavior** : handles validation behaviors.
    - **Bases** : Contains geniric classes for Response handling.
    - **MiddleWares** : For handling Exeptions.
    - **SharedResources** : For translating.
    - **Wrapper** : conatains pagination logic.
  - **Service**: Application-level logic for handling commands and queries, contains business logic, and interfaces.
  - **Infrastructure**: Database context and entity configurations.
  - **Data**: Entities, Healpers, Enums , Requests and Responses.
  - **API**: Exposes endpoints for client interaction.
- **Authentication & Authorization:**
  - **JWT**-based authentication.
  - **Role**-based access control.
- **Entity Relationships:**
  - Configured relationships (one-to-one, many-to-one, many-to-many) between entities such as Students, Instructors, Departments, and Subjects.
- **Best Practices:**
  - **CQRS**: Separation of commands and queries.
  - **MediatR** for endpoint request handling.
  - **Fluent Validation** for input validation.
  - **Mapping**: Object-to-object mapping for better abstraction.
- Focus on Scalability and maintainability.
- Implementing SOLID prencipals.
- **🛠️ Technology Stack**
  - **Backend:** C# with .NET 6
  - **ORM:** Entity Framework Core
  - **Authentication:** JWT with role-based access control
  - **Validation:** Fluent Validation
  - **MediatR:** For CQRS and request handling
  - **Database:** SQL Server
## 🚀 Getting Started
- **Prerequisites**
  - .NET 7 SDK or above
  - SQL Server
  - Entity Framework Core CLI tools
- **Steps to Run**
1. Clone the repository:
```bash
git clone https://github.com/MalekNoureddine/SchoolProjectCleanArchitecture.git
```
2. Navigate to the project folder:
```bash 
cd SchoolProjectCleanArchitecture
```
3. Restore dependencies:
```bash 
dotnet restore
```
4. Set up the database:
 - Update the connection string in appsettings.json.
 - Run migrations:
3. Restore dependencies:
```bash 
dotnet ef database update
```
- **Start the application:**
```bash 
dotnet run
```
## ✨ Features
- **Entities:**
  - Students
  - Instructors
  - Departments
  - Subjects
  - Users
  - ResetPassword
  - DepartmentSubject
  - TeacherDepartment
  - TeacherSubject
  - StudentSubject
  - StudentDepartment
- **Relationships configured using Fluent API:**
  - One-to-One
  - One-to-Many
  - Many-to-Many
- **Authentication & Security:**
 - JSON Web Tokens (JWT) for secure authentication.
 - Role-based authorization for Admins and Teachers.
- **Validation:**
- Input validation using Fluent Validation.
- **CQRS with MediatR:**
 - **Commands:** Handle data mutations (e.g., create, update, delete).
 - **Queries:** Handle read operations.
- **Best Practices:**
- Separation of concerns for better maintainability.
- Mapping layers for data transformations.
- Follwing design prencipals 
## 🔧 Tools and Packages
- **MediatR:** Request handling.
- **Entity Framework Core:** Database management and ORM.
- **Fluent Validation:** Ensures clean and valid input.
- **JWT Authentication:** Secure login system.
- **Automapper:** Simplifies object-to-object mapping.
## 🎯 Future Enhancements
1. Extend the database design to include more entities and relationships.
2. Add endpoints for advanced reporting and analytics.
3. Enhance logging and monitoring mechanisms.
4. Develop unit and integration tests for robust testing.
## 📧 Contact
- For any questions or suggestions, reach out via:

- **Email:** noureddinemalek50@gmail.com
- **GitHub:** Malek Noureddine

Let me know if you’d like to refine or expand this README further!
