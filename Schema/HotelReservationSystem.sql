
CREATE DATABASE HotelReservationSystem;
GO

USE HotelReservationSystem;
GO
CREATE TABLE Hotel (
    Id INT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL
);

create table Building(
Id int primary key,
Location varchar(60) not null,
HotelId int,
FOREIGN KEY(HotelId) references Hotel(Id));

create table Room(
Id int primary key,
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
Id int primary key,
Name varchar(100),
Type varchar(100));


create table RoomComponent(
RoomId int,
ComponentId int,
FOREIGN KEY(RoomId) references Room(Id),
FOREIGN KEY(ComponentId) references Component(Id));

create table Customer(
Id int primary key,
Name varchar(30),
Username varchar(40) not null,
Password varchar(30) not null,
Location varchar(40) not null,
Email varchar(40) not null,
Phone bigint);

create table Reservation(
Id int primary key,
FromDate date not null,
ToDate date not null,
Status varchar(30),
HotelId int,
RoomId int,
CustomerId int,
FOREIGN KEY(HotelId) references Hotel(Id),
FOREIGN KEY(RoomId) references Room(Id),
FOREIGN KEY(CustomerId) references Customer(Id));

CREATE TABLE [Users] (
    [Id] int NOT NULL IDENTITY,
    [Username] nvarchar(max) NOT NULL,
    [Password] nvarchar(max) NOT NULL,
    [Roles] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);
