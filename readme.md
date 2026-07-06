Equipment Management API

A production‑ready .NET 8 Web API for managing industrial equipment, their inputs (sensors/actuators), and time‑series value logs. Built with clean architecture, Dapper (raw SQL), and SQL Server. Designed for scalability and maintainability in industrial IoT (IIoT) scenarios.
📋 Features

    Equipment – CRUD operations for equipment (name, location).

    Inputs – CRUD for inputs belonging to an equipment, with type (Analog / Modbus) and unit of measure.

    Value Logs – Store historical readings (value, date, time) for each input.

    SQL‑first – Uses Dapper with raw SQL for maximum performance and control.

    Async everywhere – Fully asynchronous from repository to controller.

    DTOs & Auto‑mapping – Clean separation between domain models and API contracts.

    Dependency Injection – Proper DI for all services and repositories.

    Error Handling – Meaningful HTTP status codes and exception handling.

🛠️ Tech Stack

Runtime	.NET 8 (LTS)
Database	SQL Server (2019 or higher)
ORM / Data	Dapper (raw SQL)
API Framework	ASP.NET Core Web API
Documentation	Swagger / OpenAPI (via Swashbuckle)
Language	C# 12
Testing	(can be added – xUnit/NUnit)
🏗️ Architecture Overview

The solution follows a layered architecture with clear separation of concerns:
text

Controllers  →  Services  →  Repositories  →  Database (SQL)
   ↑               ↑              ↑
 DTOs            Models        Raw SQL (Dapper)

    Models – Domain entities (Equipment, Input, ValueLog).

    Repositories – Data access layer with raw SQL queries. Each repository interface is implemented using Dapper.

    Services – Business logic, validation, and orchestration.

    Controllers – REST endpoints, mapping between DTOs and models.

    DTOs – Data transfer objects for request/response.

All dependencies are injected via constructor injection.
🗄️ Database Schema
sql
CREAATE DATABSE IOT;
USE IOT:

CREATE TABLE Equipment (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Location NVARCHAR(200) NOT NULL
);

CREATE TABLE Input (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    EquipmentId INT NOT NULL FOREIGN KEY REFERENCES Equipment(Id) ON DELETE CASCADE,
    Name NVARCHAR(100) NOT NULL,
    UnitOfMeasure NVARCHAR(50) NOT NULL,
    Type NVARCHAR(20) NOT NULL CHECK (Type IN ('Analog', 'Modbus'))
);

CREATE TABLE ValueLog (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    InputId INT NOT NULL FOREIGN KEY REFERENCES Input(Id) ON DELETE CASCADE,
    LogDate DATE NOT NULL,
    LogTime TIME(0) NOT NULL,
    Value DECIMAL(18,6) NOT NULL
);

📦 Prerequisites

    .NET 8 SDK

    SQL Server (local or cloud)

    Visual Studio Code (or any IDE) with C# extension

    (Optional) SQL Server Management Studio (SSMS) or Azure Data Studio

🚀 Setup Instructions
1. Clone the repository
bash

git clone https://github.com/MAHDICVBN41/IOT-api.git
cd equipment-management-api

2. Create the database

    Open SQL Server Management Studio or run a SQL script to create a new database (e.g., EquipmentDB).

    Execute the schema script (provided in the Database folder or above) to create tables.

3. Configure connection string

Open appsettings.json and update the DefaultConnection:
json

"ConnectionStrings": {
  "DefaultConnection": "SERVER=.\\SQLEXPRESS;Database=IoT;User Id=your_user;Password=your_password;Integrated Security=True;TrustServerCertificate=True;MultipleActiveResultSets=true;encrypt=true;"
}

    Adjust Server, Database, and authentication as needed.

4. Restore dependencies
bash

dotnet restore

5. Run the application
bash

dotnet run

The API will start on https://localhost:5001 (HTTPS) and http://localhost:5000 (HTTP).
6. Swagger documentation

Navigate to https://localhost:5001/swagger to explore and test the endpoints interactively.
📡 API Endpoints

All endpoints are under the /api/ base path.

GET/api/equipment--> Get all equipment
GET/api/equipment/{id}-->Get equipment by ID
POST/api/equipment-->Create new equipment
PUT/api/equipment/{id}-->Update existing equipment
DELETE/api/equipment/{id}-->Delete equipment
GET/api/input/by-equipment/{equipmentId}-->Get all inputs for an equipment
GET/api/input/{id}-->Get input by ID
POST/api/input-->Create new input (for an equipment)
PUT/api/input/{id}-->Update input
DELETE/api/input/{id}-->Delete input
GET/api/valuelog/by-input/{inputId}-->Get all value logs for an input
GET/api/valuelog/{id}-->Get value log by ID
POST/api/valuelog-->Create new value log
PUT/api/valuelog/{id}-->Update value log
DELETE/api/valuelog/{id}-->Delete value log
📁 Project Structure
text

EquipmentManagement.API/
├── Controllers/               # API endpoints
│   ├── EquipmentController.cs
│   ├── InputController.cs
│   └── ValueLogController.cs
├── Data/                      # Database connection factory
│   └── DbConnectionFactory.cs
├── DTOs/                      # Data transfer objects
│   ├── Equipment/
│   ├── Input/
│   └── ValueLog/
├── Models/                    # Domain entities
│   ├── Equipment.cs
│   ├── Input.cs
│   ├── ValueLog.cs
│   └── Enums/
│       └── InputType.cs
├── Repositories/              # Data access (Dapper)
│   ├── IEquipmentRepository.cs
│   ├── EquipmentRepository.cs
│   ├── IInputRepository.cs
│   ├── InputRepository.cs
│   ├── IValueLogRepository.cs
│   └── ValueLogRepository.cs
├── Services/                  # Business logic
│   ├── IEquipmentService.cs
│   ├── EquipmentService.cs
│   ├── IInputService.cs
│   ├── InputService.cs
│   ├── IValueLogService.cs
│   └── ValueLogService.cs
├── appsettings.json           # Configuration
├── Program.cs                 # Application entry point
└── EquipmentManagement.API.csproj

🧪 Example API Requests
Create equipment
http

POST /api/equipment
Content-Type: application/json

{
  "name": "Pump Station 3",
  "location": "Building B - Floor 2"
}

Add an input
http

POST /api/input
Content-Type: application/json

{
  "equipmentId": 1,
  "name": "Pressure Sensor",
  "unitOfMeasure": "psi",
  "type": "Analog"
}

Log a value
http

POST /api/valuelog
Content-Type: application/json

{
  "inputId": 1,
  "logDate": "2026-07-04",
  "logTime": "14:30:00",
  "value": 42.5
}

🔧 Customisation & Extensibility

    Add new endpoints – Simply extend the existing controllers and services.

    Add more complex queries – Use Dapper’s rich mapping capabilities (multi‑mapping for joins).

    Unit tests – Create test projects for repositories and services with a mock database.

    Authentication – Integrate JWT or Identity easily by adding middleware.

    Logging – Serilog or built‑in ILogger can be added for production monitoring.
