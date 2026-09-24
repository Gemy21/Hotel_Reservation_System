CREATE DATABASE HotelReservationSystem;

USE HotelReservationSystem;
GO

CREATE TABLE dbo.Hotel
(
    Id   INT IDENTITY(1,1) NOT NULL,
    Name VARCHAR(100) NOT NULL,
    CONSTRAINT PK_Hotel PRIMARY KEY (Id)
);
GO

CREATE TABLE dbo.Users
(
    Id                    INT IDENTITY(1,1) NOT NULL,
    Username              NVARCHAR(50) NOT NULL,
    Password              NVARCHAR(256) NOT NULL,
    Roles                 NVARCHAR(20) NOT NULL,
    RefreshToken          NVARCHAR(100) NULL,
    RefreshTokenExpiryTime DATETIME2 NULL,
    CONSTRAINT PK_Users PRIMARY KEY (Id),
    CONSTRAINT UQ_Users_Username UNIQUE (Username)
);
GO

CREATE TABLE dbo.Customer
(
    Id       INT IDENTITY(1,1) NOT NULL,
    Name     VARCHAR(30) NULL,
    Location VARCHAR(40) NOT NULL,
    Email    VARCHAR(40) NOT NULL,
    Phone    BIGINT NULL,
    UserId   INT NOT NULL,
    CONSTRAINT PK_Customer PRIMARY KEY (Id),
    CONSTRAINT UQ_Customer_UserId UNIQUE (UserId),
    CONSTRAINT FK_Customer_Users
        FOREIGN KEY (UserId) REFERENCES dbo.Users(Id)
        ON DELETE NO ACTION
);
GO

CREATE TABLE dbo.Building
(
    Id       INT IDENTITY(1,1) NOT NULL,
    Location VARCHAR(60) NOT NULL,
    HotelId  INT NULL,
    CONSTRAINT PK_Building PRIMARY KEY (Id),
    CONSTRAINT FK_Building_Hotel
        FOREIGN KEY (HotelId) REFERENCES dbo.Hotel(Id)
);
GO

CREATE TABLE dbo.Room
(
    Id          INT IDENTITY(1,1) NOT NULL,
    RommNumber  INT NULL,
    Price       INT NULL,
    IsAvailable BIT NOT NULL CONSTRAINT DF_Room_IsAvailable DEFAULT (1),
    ChechIn     DATE NULL,
    ChekOut     DATE NULL,
    HotelId     INT NULL,
    BuildingId  INT NULL,
    RoomType    NVARCHAR(50) NOT NULL,
    CONSTRAINT PK_Room PRIMARY KEY (Id),
    CONSTRAINT FK_Room_Building
        FOREIGN KEY (BuildingId) REFERENCES dbo.Building(Id),
    CONSTRAINT FK_Room_Hotel
        FOREIGN KEY (HotelId) REFERENCES dbo.Hotel(Id)
);
GO

CREATE TABLE dbo.Component
(
    Id   INT IDENTITY(1,1) NOT NULL,
    Name VARCHAR(100) NULL,
    Type VARCHAR(100) NULL,
    CONSTRAINT PK_Component PRIMARY KEY (Id)
);
GO

CREATE TABLE dbo.Reservation
(
    Id         INT IDENTITY(1,1) NOT NULL,
    FromDate   DATE NOT NULL,
    ToDate     DATE NOT NULL,
    Status     VARCHAR(30) NULL,
    HotelId    INT NULL,
    RoomId     INT NULL,
    CustomerId INT NULL,
    CONSTRAINT PK_Reservation PRIMARY KEY (Id),
    CONSTRAINT FK_Reservation_Customer
        FOREIGN KEY (CustomerId) REFERENCES dbo.Customer(Id),
    CONSTRAINT FK_Reservation_Hotel
        FOREIGN KEY (HotelId) REFERENCES dbo.Hotel(Id),
    CONSTRAINT FK_Reservation_Room
        FOREIGN KEY (RoomId) REFERENCES dbo.Room(Id)
);
GO

CREATE TABLE dbo.RoomComponent
(
    RoomId      INT NOT NULL,
    ComponentId INT NOT NULL,
    CONSTRAINT PK_RoomComponent PRIMARY KEY (RoomId, ComponentId),
    CONSTRAINT FK_RoomComponent_Component
        FOREIGN KEY (ComponentId) REFERENCES dbo.Component(Id)
        ON DELETE CASCADE,
    CONSTRAINT FK_RoomComponent_Room
        FOREIGN KEY (RoomId) REFERENCES dbo.Room(Id)
        ON DELETE CASCADE
);
GO

CREATE TABLE dbo.VipRoom
(
    Id                  INT NOT NULL,
    LivingArea          DECIMAL(6,2) NOT NULL,
    LateCheckOutAllowed BIT NOT NULL,
    LateCheckOutTime    TIME NULL,
    LateCheckOutFee     DECIMAL(8,2) NOT NULL,
    CONSTRAINT PK_VipRoom PRIMARY KEY (Id),
    CONSTRAINT FK_VipRoom_Room
        FOREIGN KEY (Id) REFERENCES dbo.Room(Id)
        ON DELETE CASCADE
);
GO

CREATE TABLE dbo.LateCheckOutRequest
(
    Id            INT IDENTITY(1,1) NOT NULL,
    VipRoomId     INT NOT NULL,
    Approved      BIT NOT NULL,
    ExtraCharge   DECIMAL(8,2) NOT NULL,
    RequestedAt   DATETIME2 NOT NULL,
    CONSTRAINT PK_LateCheckOutRequest PRIMARY KEY (Id),
    CONSTRAINT FK_LateCheckOutRequest_VipRoom
        FOREIGN KEY (VipRoomId) REFERENCES dbo.VipRoom(Id)
);
GO


