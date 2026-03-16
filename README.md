# Appointment Booking System - .NET 6 Minimal API

A RESTful Appointment Booking System built with .NET 6 Minimal APIs, Dapper, and MSSQL Server.

## Features

- JWT-based authentication with role-based authorization (Admin, User, Doctor)
- Doctor management (CRUD operations)
- Appointment slot management
- Appointment booking and cancellation
- Doctor schedule viewing
- Global error handling middleware
- Logging with Serilog
- Swagger documentation

## Tech Stack

- .NET 6 Minimal API
- MSSQL Server
- Dapper (Micro ORM)
- JWT Authentication
- Serilog for logging
- Swagger/OpenAPI

## Project Structure

```
AppointmentBookingSystem/
├── src/AppointmentBookingSystem/
│   ├── Models/              # Entity models
│   ├── DTOs/                # Data Transfer Objects
│   ├── Repositories/        # Data access layer
│   ├── Services/            # Business logic layer
│   ├── Middleware/          # Custom middleware
│   ├── Program.cs           # Application entry point
│   └── appsettings.json     # Configuration
├── database/
│   ├── schema.sql           # Database schema
│   └── seed-data.sql        # Sample data
└── README.md
```

## Prerequisites

- .NET 6 SDK or later
- SQL Server (2019 or later)
- Visual Studio 2022 or VS Code

## Setup Instructions

### 1. Database Setup


**Option A: Using SQL Server Management Studio (SSMS)**
1. Open SSMS and connect to your SQL Server instance
2. Open and execute `database/schema.sql` to create the database and tables
3. Open and execute `database/seed-data.sql` to populate sample data

**Option B: Using Command Line**
```bash
sqlcmd -S localhost -U sa -P YourPassword -i database/schema.sql
sqlcmd -S localhost -U sa -P YourPassword -i database/seed-data.sql
```

### 2. Update Connection String

Edit `src/AppointmentBookingSystem/appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=AppointmentBookingDB;User Id=sa;Password=YourPassword;TrustServerCertificate=True;"
}
```

### 3. Build and Run

```bash
cd src/AppointmentBookingSystem
dotnet restore
dotnet build
dotnet run
```

The API will be available at: **http://localhost:5000**

Swagger UI: **http://localhost:5000/swagger**

## Default Credentials

After running seed-data.sql:

- **Admin**: username=`admin`, password=`password123`
- **User**: username=`user1`, password=`password123`
- **Doctor**: username=`dr.smith`, password=`password123`

## API Endpoints

### Authentication
- `POST /api/auth/login` - Login and get JWT token

### Doctors (Public read, Admin write)
- `GET /api/doctors` - Get all doctors
- `GET /api/doctors/{id}` - Get doctor by ID
- `POST /api/doctors` - Create doctor (Admin only)
- `PUT /api/doctors/{id}` - Update doctor (Admin only)
- `DELETE /api/doctors/{id}` - Delete doctor (Admin only)

### Appointment Slots
- `GET /api/slots/doctor/{doctorId}` - Get available slots for a doctor
- `POST /api/slots` - Create slot (Admin only)
- `DELETE /api/slots/{id}` - Delete slot (Admin only)

### Appointments
- `POST /api/appointments` - Book appointment (User only)
- `GET /api/appointments/my` - Get user's appointments (User only)
- `GET /api/appointments/doctor/{doctorId}` - Get doctor's schedule (Doctor/Admin)
- `DELETE /api/appointments/{id}` - Cancel appointment (User only)


## Usage Examples

### 1. Login
```bash
curl -X POST http://localhost:5000/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"username":"admin","password":"password123"}'
```

Response:
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "username": "admin",
  "role": "Admin"
}
```

### 2. Create Doctor (Admin)
```bash
curl -X POST http://localhost:5000/api/doctor \
  -H "Authorization: Bearer YOUR_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Dr. Jane Doe",
    "specialization": "Pediatrics",
    "email": "jane@hospital.com",
    "phone": "+1-555-0104",
    "username": "dr.jane",
    "password": "password123"
  }'
```

### 3. Get Available Slots
```bash
curl -X GET http://localhost:5000/api/slot/doctor/1
```

### 4. Book Appointment (User)
```bash
curl -X POST http://localhost:5000/api/appointment \
  -H "Authorization: Bearer YOUR_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "slotId": 5,
    "patientName": "John Doe",
    "patientEmail": "john@example.com",
    "patientPhone": "+1-555-1234",
    "notes": "First visit"
  }'
```

## Architecture

### Repository Pattern
- Abstracts data access logic
- Uses Dapper for efficient SQL queries
- Async/await throughout

### Service Layer
- Contains business logic
- Validates operations
- Coordinates between repositories

### Middleware
- Global error handling
- Consistent error responses
- Logging of exceptions

## Security

- JWT tokens with configurable expiry
- Role-based authorization
- Password hashing (SHA256 - upgrade to BCrypt for production)
- SQL injection prevention via parameterized queries

## Docker Support (Optional)

Create `Dockerfile`:
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/AppointmentBookingSystem/AppointmentBookingSystem.csproj", "AppointmentBookingSystem/"]
RUN dotnet restore "AppointmentBookingSystem/AppointmentBookingSystem.csproj"
COPY src/AppointmentBookingSystem/. AppointmentBookingSystem/
WORKDIR "/src/AppointmentBookingSystem"
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AppointmentBookingSystem.dll"]
```

Build and run:
```bash
docker build -t appointment-booking-api .
docker run -p 8080:80 appointment-booking-api
```

## License

MIT
