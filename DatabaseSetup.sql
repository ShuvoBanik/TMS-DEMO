-- RSS Travel Management System Database Script
-- Execute this script in SQL Server Management Studio to create the required tables

USE master;
GO

-- Create database if it doesn't exist
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'RSS-TMS')
BEGIN
    CREATE DATABASE [RSS-TMS];
END
GO

USE [RSS-TMS];
GO

-- Drop and recreate Login table to ensure proper structure
IF EXISTS (SELECT * FROM sysobjects WHERE name='Login' AND xtype='U')
BEGIN
    DROP TABLE Login;
END

CREATE TABLE Login (
    LoginID int IDENTITY(1,1) PRIMARY KEY,
    UserID nvarchar(50),
    UserName nvarchar(50) NOT NULL,
    Password nvarchar(50) NOT NULL,
    Role nvarchar(20) NOT NULL CHECK (Role IN ('Admin', 'Agent', 'Customer', 'Owner'))
);

-- Insert default users
INSERT INTO Login (UserID, UserName, Password, Role) VALUES 
('ADM001', 'admin', 'admin123', 'Admin'),
('AGT001', 'agent', 'agent123', 'Agent'),
('CST001', 'customer', 'customer123', 'Customer'),
('OWN001', 'owner', 'owner123', 'Owner');
GO

-- Create Register table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Register' AND xtype='U')
BEGIN
    CREATE TABLE Register (
        RegisterID int IDENTITY(1,1) PRIMARY KEY,
        Name nvarchar(100) NOT NULL,
        Email nvarchar(100) NOT NULL,
        Username nvarchar(50) NOT NULL UNIQUE,
        Password nvarchar(50) NOT NULL,
        RegistrationDate datetime DEFAULT GETDATE()
    );
END
GO

-- Handle Admin portal table creation/update
IF EXISTS (SELECT * FROM sysobjects WHERE name='Admin portal' AND xtype='U')
BEGIN
    -- Check if the problematic columns exist, if not add them
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('[Admin portal]') AND name = 'Salary')
    BEGIN
        ALTER TABLE [Admin portal] ADD Salary decimal(10,2) DEFAULT 0;
        PRINT 'Added Salary column to Admin portal table';
    END
    
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('[Admin portal]') AND name = 'Commission')
    BEGIN
        ALTER TABLE [Admin portal] ADD Commission decimal(10,2) DEFAULT 0;
        PRINT 'Added Commission column to Admin portal table';
    END
    
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('[Admin portal]') AND name = 'Billing ID')
    BEGIN
        ALTER TABLE [Admin portal] ADD [Billing ID] nvarchar(50);
        PRINT 'Added Billing ID column to Admin portal table';
    END
    
    -- Clear existing data and insert fresh sample data
    DELETE FROM [Admin portal];
    DBCC CHECKIDENT('[Admin portal]', RESEED, 0);
END
ELSE
BEGIN
    -- Create new table
    CREATE TABLE [Admin portal] (
        [Agent ID] int IDENTITY(1,1) PRIMARY KEY,
        [Agent Name] nvarchar(100),
        Branch nvarchar(100),
        Password nvarchar(50),
        [Number of Customer] int DEFAULT 0,
        Salary decimal(10,2) DEFAULT 0,
        Commission decimal(10,2) DEFAULT 0,
        [Billing ID] nvarchar(50)
    );
    PRINT 'Created Admin portal table';
END

-- Now insert sample data (this will work whether table was created or updated)
INSERT INTO [Admin portal] ([Agent Name], Branch, Password, [Number of Customer], Salary, Commission, [Billing ID]) VALUES
('John Smith', 'Main Branch', 'pass123', 5, 50000.00, 5000.00, 'BILL001'),
('Sarah Johnson', 'North Branch', 'pass456', 3, 45000.00, 3000.00, 'BILL002');
PRINT 'Inserted sample data into Admin portal table';
GO

-- Create CustomerBookings table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='CustomerBookings' AND xtype='U')
BEGIN
    CREATE TABLE CustomerBookings (
        BookingId int IDENTITY(1,1) PRIMARY KEY,
        Name nvarchar(100),
        Surname nvarchar(100),
        DateOfBirth date,
        Gender nvarchar(10),
        Address nvarchar(500),
        Email nvarchar(100),
        DeparturePoint nvarchar(100),
        Destination nvarchar(100),
        Accommodation nvarchar(100),
        Package nvarchar(100),
        FlightType nvarchar(50),
        TicketType nvarchar(50),
        SubTotal decimal(10,2),
        Tax decimal(10,2),
        Total decimal(10,2),
        BookingDate datetime DEFAULT GETDATE()
    );
END
GO

-- Create AgentBookings table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='AgentBookings' AND xtype='U')
BEGIN
    CREATE TABLE AgentBookings (
        BookingId int IDENTITY(1,1) PRIMARY KEY,
        DeparturePoint nvarchar(100),
        Destination nvarchar(100),
        Accommodation nvarchar(100),
        ExtraServices nvarchar(500),
        SubTotal decimal(10,2),
        Tax decimal(10,2),
        Total decimal(10,2),
        BookingDate datetime DEFAULT GETDATE(),
        AgentId int
    );
END
GO

-- Create Flights table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Flights' AND xtype='U')
BEGIN
    CREATE TABLE Flights (
        FlightId int IDENTITY(1,1) PRIMARY KEY,
        FlightNumber nvarchar(20),
        DepartureCity nvarchar(100),
        ArrivalCity nvarchar(100),
        DepartureDate datetime,
        ArrivalDate datetime,
        Price decimal(10,2),
        AvailableSeats int
    );
    
    -- Insert sample flights
    INSERT INTO Flights (FlightNumber, DepartureCity, ArrivalCity, DepartureDate, ArrivalDate, Price, AvailableSeats) VALUES
    ('FL001', 'New York', 'London', '2025-11-01 08:00', '2025-11-01 20:00', 899.99, 150),
    ('FL002', 'London', 'Paris', '2025-11-02 10:00', '2025-11-02 12:00', 299.99, 100),
    ('FL003', 'Paris', 'Tokyo', '2025-11-03 14:00', '2025-11-04 08:00', 1299.99, 200);
END
GO

-- Create Hotels table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Hotels' AND xtype='U')
BEGIN
    CREATE TABLE Hotels (
        HotelId int IDENTITY(1,1) PRIMARY KEY,
        HotelName nvarchar(200),
        City nvarchar(100),
        Address nvarchar(500),
        StarRating int,
        PricePerNight decimal(10,2),
        AvailableFrom date,
        AvailableTo date
    );
    
    -- Insert sample hotels
    INSERT INTO Hotels (HotelName, City, Address, StarRating, PricePerNight, AvailableFrom, AvailableTo) VALUES
    ('Grand Hotel London', 'London', '123 Piccadilly, London', 5, 350.00, '2025-01-01', '2025-12-31'),
    ('Paris Central Hotel', 'Paris', '456 Champs-Élysées, Paris', 4, 280.00, '2025-01-01', '2025-12-31'),
    ('Tokyo Bay Hotel', 'Tokyo', '789 Shibuya, Tokyo', 4, 220.00, '2025-01-01', '2025-12-31');
END
GO

-- Create OwnerReviews table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='OwnerReviews' AND xtype='U')
BEGIN
    CREATE TABLE OwnerReviews (
        ReviewId int IDENTITY(1,1) PRIMARY KEY,
        ReviewText nvarchar(1000),
        ReviewDate datetime DEFAULT GETDATE()
    );
END
GO

PRINT 'Database setup completed successfully!';
GO