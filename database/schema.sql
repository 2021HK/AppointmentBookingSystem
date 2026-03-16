
CREATE DATABASE AppointmentBookingDB;


USE AppointmentBookingDB;



CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(100) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(500) NOT NULL,
    Email NVARCHAR(200) NOT NULL,
    Role NVARCHAR(50) NOT NULL CHECK (Role IN ('Admin', 'User', 'Doctor')),
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE()
);


CREATE TABLE Doctors (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(200) NOT NULL,
    Specialization NVARCHAR(200) NOT NULL,
    Email NVARCHAR(200) NOT NULL,
    Phone NVARCHAR(50) NOT NULL,
    UserId INT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE SET NULL
);


CREATE TABLE AppointmentSlots (
    Id INT PRIMARY KEY IDENTITY(1,1),
    DoctorId INT NOT NULL,
    StartTime DATETIME2 NOT NULL,
    EndTime DATETIME2 NOT NULL,
    IsAvailable BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    FOREIGN KEY (DoctorId) REFERENCES Doctors(Id) ON DELETE CASCADE
);


CREATE TABLE Appointments (
    Id INT PRIMARY KEY IDENTITY(1,1),
    SlotId INT NOT NULL,
    UserId INT NOT NULL,
    DoctorId INT NOT NULL,
    PatientName NVARCHAR(200) NOT NULL,
    PatientEmail NVARCHAR(200) NOT NULL,
    PatientPhone NVARCHAR(50) NOT NULL,
    Status NVARCHAR(50) NOT NULL DEFAULT 'Booked' CHECK (Status IN ('Booked', 'Cancelled', 'Completed')),
    Notes NVARCHAR(MAX) NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    FOREIGN KEY (SlotId) REFERENCES AppointmentSlots(Id),
    FOREIGN KEY (UserId) REFERENCES Users(Id),
    FOREIGN KEY (DoctorId) REFERENCES Doctors(Id)
);


CREATE INDEX IX_AppointmentSlots_DoctorId ON AppointmentSlots(DoctorId);
CREATE INDEX IX_AppointmentSlots_StartTime ON AppointmentSlots(StartTime);
CREATE INDEX IX_Appointments_UserId ON Appointments(UserId);
CREATE INDEX IX_Appointments_DoctorId ON Appointments(DoctorId);
CREATE INDEX IX_Appointments_SlotId ON Appointments(SlotId);

