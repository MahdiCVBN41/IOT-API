Equipment Management API

A production‑ready .NET 8 Web API for managing industrial equipment, inputs (sensors/actuators), and their value logs. Built with Dapper for raw SQL performance, SQL Server as the database, and following a clean Repository‑Service‑Controller architecture.
Table of Contents

    Overview

    Features

    Technologies

    Database Schema

    Project Structure

    Setup & Installation

    Configuration

    API Endpoints

    Usage Examples

    Error Handling

    Contributing

    License

Overview

This API is designed for industrial environments where equipment has multiple inputs (e.g., temperature sensors, pressure transmitters) and each input records time‑series values. The system provides full CRUD operations for:

    Equipment (e.g., machine, pump, conveyor)

    Inputs (analog or Modbus signals) belonging to an equipment

    Value Logs (timestamped readings) for each input

All data access is performed via raw SQL using Dapper, ensuring maximum control and performance without an ORM overhead.
Features

    Fully async/await for scalable I/O operations.

    Clean Architecture with clear separation of concerns.

    Repository pattern for data access (raw SQL with Dapper).

    Service layer for business logic and validation.

    DTOs for API contracts (decoupled from domain models).

    Dependency Injection built into .NET Core.

    Swagger/OpenAPI documentation for easy testing.

    Comprehensive error handling (404, 400, 500).

    SQL Server with foreign key constraints and cascading deletes.

Technologies
Component	Technology
Runtime	.NET 8
Language	C# 12
Database	SQL Server (2016+)
Data Access	Dapper 2.1.35
Database Driver	Microsoft.Data.SqlClient 5.2.0
API Documentation	Swagger / Swashbuckle 6.5.0
IDE	Visual Studio Code (recommended)
Version Control	Git / GitHub
Database Schema

The database consists of three tables with the following relationships:
sql

Equipment (1) ──────< (∞) Input ──────< (∞) ValueLog

Tables
Equipment
Column	Type	Description
Id	INT (PK)	Auto‑increment primary key
Name	NVARCHAR(100)	Equipment name
Location	NVARCHAR(200)	Physical location
Input
Column	Type	Description
Id	INT (PK)	Auto‑increment
EquipmentId	INT (FK)	References Equipment.Id
Name	NVARCHAR(100)	Input name (e.g., "Temp Sensor")
UnitOfMeasure	NVARCHAR(50)	e.g., "°C", "PSI", "mA"
Type	NVARCHAR(20)	'Analog' or 'Modbus' (CHECK constraint)
ValueLog
Column	Type	Description
Id	INT (PK)	Auto‑increment
InputId	INT (FK)	References Input.Id
LogDate	DATE	Date of the reading
LogTime	TIME(0)	Time of the reading (no seconds fraction)
Value	DECIMAL(18,6)	Numeric reading value

Cascading Deletes: Deleting an Equipment deletes all its Input records, which in turn deletes all related ValueLog records.
Project Structure
text

EquipmentManagement.API/
├── Controllers/
│   ├── EquipmentController.cs
│   ├── InputController.cs
│   └── ValueLogController.cs
├── Data/
│   └── DbConnectionFactory.cs
├── DTOs/
│   ├── Equipment/
│   │   ├── EquipmentDto.cs
│   │   ├── CreateEquipmentDto.cs
│   │   └── UpdateEquipmentDto.cs
│   ├── Input/
│   │   ├── InputDto.cs
│   │   ├── CreateInputDto.cs
│   │   └── UpdateInputDto.cs
│   └── ValueLog/
│       ├── ValueLogDto.cs
│       └── CreateValueLogDto.cs
├── Models/
│   ├── Equipment.cs
│   ├── Input.cs
│   ├── ValueLog.cs
│   └── Enums/
│       └── InputType.cs
├── Repositories/
│   ├── IEquipmentRepository.cs
│   ├── EquipmentRepository.cs
│   ├── IInputRepository.cs
│   ├── InputRepository.cs
│   ├── IValueLogRepository.cs
│   └── ValueLogRepository.cs
├── Services/
│   ├── IEquipmentService.cs
│   ├── EquipmentService.cs
│   ├── IInputService.cs
│   ├── InputService.cs
│   ├── IValueLogService.cs
│   └── ValueLogService.cs
├── appsettings.json
├── Program.cs
└── EquipmentManagement.API.csproj

Setup & Installation
Prerequisites

    .NET 8 SDK

    SQL Server (LocalDB, Express, or full)

    Visual Studio Code (optional) with C# extension