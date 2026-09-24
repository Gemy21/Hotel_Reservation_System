
CREATE DATABASE HotelReservationSystem;
GO

USE HotelReservationSystem;
GO
CREATE TABLE Hotel (
    Id INT PRIMARY KEY identity(1,1),
    Name VARCHAR(100) NOT NULL
);

create table Building(
Id int primary key identity(1,1),
Location varchar(60) not null,
HotelId int,
FOREIGN KEY(HotelId) references Hotel(Id));

create table Room(
Id int primary key identity(1,1),
RommNumber int,
Price int,
IsAvailable bit not null default 1,
ChechIn date,
ChekOut date,
HotelId int,
BuildingId int,
RoomType nvarchar(50),
FOREIGN KEY(HotelId) references Hotel(Id),
FOREIGN KEY(BuildingId) references Building(Id));


create table Component(
Id int primary key identity(1,1),
Name varchar(100),
Type varchar(100));


create table RoomComponent(
RoomId int,
ComponentId int,
FOREIGN KEY(RoomId) references Room(Id),
FOREIGN KEY(ComponentId) references Component(Id));

create table Customer(
Id int primary key identity(1,1),
Name varchar(30),
Username varchar(40) not null,
Password varchar(30) not null,
Location varchar(40) not null,
Email varchar(40) not null,
Phone bigint);

create table Reservation(
Id int primary key identity(1,1),
FromDate date not null,
ToDate date not null,
Status varchar(30),
HotelId int,
RoomId int,
CustomerId int,
FOREIGN KEY(HotelId) references Hotel(Id),
FOREIGN KEY(RoomId) references Room(Id),
FOREIGN KEY(CustomerId) references Customer(Id));

CREATE TABLE VipRoom (
    Id INT PRIMARY KEY,
    LivingArea DECIMAL(6,2) NOT NULL,
    LateCheckOutAllowed BIT NOT NULL,
    LateCheckOutTime TIME NULL,
    LateCheckOutFee DECIMAL(8,2) NOT NULL,
    FOREIGN KEY (Id) REFERENCES Room(Id) ON DELETE CASCADE
);
GO

CREATE TABLE LateCheckOutRequest (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ReservationId INT NOT NULL,
    VipRoomId INT NOT NULL,
    Approved BIT NOT NULL,
    ExtraCharge DECIMAL(8,2) NOT NULL,
    RequestedAt DATETIME2 NOT NULL,
    FOREIGN KEY (ReservationId) REFERENCES Reservation(Id),
    FOREIGN KEY (VipRoomId) REFERENCES VipRoom(Id)
);
GO

CREATE TABLE [Users] (
    [Id] int NOT NULL IDENTITY,
    [Username] nvarchar(max) NOT NULL,
    [Password] nvarchar(max) NOT NULL,
    [Roles] nvarchar(max) NOT NULL,
    [RefreshToken] nvarchar(max) NULL,
    [RefreshTokenExpiryTime] datetime2 NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);