
USE HotelReservationSystem;
GO

SET IDENTITY_INSERT dbo.Users ON;
INSERT INTO dbo.Users
    (Id, Username, Password, Roles, RefreshToken, RefreshTokenExpiryTime)
VALUES
(1,  N'ahmed.salah',    N'AQAAAAIAAYagAAAAEAARIjNEVWZ3iJmqu8zd7v+3nLsN0AZ3XbhXKRX7OTxX8GQ4u383I5+BQihcjgG68A==', N'Customer', NULL, NULL),
(2,  N'mona.fathy',     N'AQAAAAIAAYagAAAAEAARIjNEVWZ3iJmqu8zd7v+3nLsN0AZ3XbhXKRX7OTxX8GQ4u383I5+BQihcjgG68A==', N'Customer', NULL, NULL),
(3,  N'omar.khaled',    N'AQAAAAIAAYagAAAAEAARIjNEVWZ3iJmqu8zd7v+3nLsN0AZ3XbhXKRX7OTxX8GQ4u383I5+BQihcjgG68A==', N'Customer', NULL, NULL),
(4,  N'sara.hassan',    N'AQAAAAIAAYagAAAAEAARIjNEVWZ3iJmqu8zd7v+3nLsN0AZ3XbhXKRX7OTxX8GQ4u383I5+BQihcjgG68A==', N'Customer', NULL, NULL),
(5,  N'youssef.adel',   N'AQAAAAIAAYagAAAAEAARIjNEVWZ3iJmqu8zd7v+3nLsN0AZ3XbhXKRX7OTxX8GQ4u383I5+BQihcjgG68A==', N'Customer', NULL, NULL),
(6,  N'nour.ibrahim',   N'AQAAAAIAAYagAAAAEAARIjNEVWZ3iJmqu8zd7v+3nLsN0AZ3XbhXKRX7OTxX8GQ4u383I5+BQihcjgG68A==', N'Customer', NULL, NULL),
(7,  N'karim.mostafa',  N'AQAAAAIAAYagAAAAEAARIjNEVWZ3iJmqu8zd7v+3nLsN0AZ3XbhXKRX7OTxX8GQ4u383I5+BQihcjgG68A==', N'Customer', NULL, NULL),
(8,  N'laila.tarek',    N'AQAAAAIAAYagAAAAEAARIjNEVWZ3iJmqu8zd7v+3nLsN0AZ3XbhXKRX7OTxX8GQ4u383I5+BQihcjgG68A==', N'Customer', NULL, NULL),
(9,  N'hossam.nabil',   N'AQAAAAIAAYagAAAAEAARIjNEVWZ3iJmqu8zd7v+3nLsN0AZ3XbhXKRX7OTxX8GQ4u383I5+BQihcjgG68A==', N'Customer', NULL, NULL),
(10, N'dina.samir',     N'AQAAAAIAAYagAAAAEAARIjNEVWZ3iJmqu8zd7v+3nLsN0AZ3XbhXKRX7OTxX8GQ4u383I5+BQihcjgG68A==', N'Customer', NULL, NULL),
(11, N'tamer.fouad',    N'AQAAAAIAAYagAAAAEAARIjNEVWZ3iJmqu8zd7v+3nLsN0AZ3XbhXKRX7OTxX8GQ4u383I5+BQihcjgG68A==', N'Customer', NULL, NULL),
(12, N'rania.emad',     N'AQAAAAIAAYagAAAAEAARIjNEVWZ3iJmqu8zd7v+3nLsN0AZ3XbhXKRX7OTxX8GQ4u383I5+BQihcjgG68A==', N'Customer', NULL, NULL),
(13, N'amr.hesham',     N'AQAAAAIAAYagAAAAEAARIjNEVWZ3iJmqu8zd7v+3nLsN0AZ3XbhXKRX7OTxX8GQ4u383I5+BQihcjgG68A==', N'Customer', NULL, NULL),
(14, N'heba.waleed',    N'AQAAAAIAAYagAAAAEAARIjNEVWZ3iJmqu8zd7v+3nLsN0AZ3XbhXKRX7OTxX8GQ4u383I5+BQihcjgG68A==', N'Customer', NULL, NULL),
(15, N'sherif.adham',   N'AQAAAAIAAYagAAAAEAARIjNEVWZ3iJmqu8zd7v+3nLsN0AZ3XbhXKRX7OTxX8GQ4u383I5+BQihcjgG68A==', N'Customer', NULL, NULL),
(16, N'yasmin.ashraf',  N'AQAAAAIAAYagAAAAEAARIjNEVWZ3iJmqu8zd7v+3nLsN0AZ3XbhXKRX7OTxX8GQ4u383I5+BQihcjgG68A==', N'Customer', NULL, NULL),
(17, N'mahmoud.reda',   N'AQAAAAIAAYagAAAAEAARIjNEVWZ3iJmqu8zd7v+3nLsN0AZ3XbhXKRX7OTxX8GQ4u383I5+BQihcjgG68A==', N'Customer', NULL, NULL),
(18, N'aya.gamal',      N'AQAAAAIAAYagAAAAEAARIjNEVWZ3iJmqu8zd7v+3nLsN0AZ3XbhXKRX7OTxX8GQ4u383I5+BQihcjgG68A==', N'Customer', NULL, NULL);
SET IDENTITY_INSERT dbo.Users OFF;
GO

SET IDENTITY_INSERT dbo.Hotel ON;
INSERT INTO dbo.Hotel (Id, Name) VALUES
(1, N'Grand Nile Hotel'),
(2, N'Sea View Resort'),
(3, N'Al Mansoura Palace'),
(4, N'Cairo Tower Hotel'),
(5, N'Red Sea Paradise'),
(6, N'Luxor Heritage Hotel'),
(7, N'Alexandria Corniche Hotel'),
(8, N'Aswan Nile Resort'),
(9, N'Sharm Sunset Bay'),
(10, N'Hurghada Lagoon Hotel'),
(11, N'Delta Business Hotel'),
(12, N'Marsa Matrouh Beach Hotel'),
(13, N'Giza Pyramids View Hotel'),
(14, N'Dahab Blue Lagoon'),
(15, N'Fayoum Oasis Hotel');
SET IDENTITY_INSERT dbo.Hotel OFF;
GO

SET IDENTITY_INSERT dbo.Building ON;
INSERT INTO dbo.Building (Id, Location, HotelId) VALUES
(1, N'Main Building - Ground Floor Block', 1),
(2, N'Sea Wing', 2),
(3, N'Central Block', 3),
(4, N'Tower A', 4),
(5, N'Beach Wing', 5),
(6, N'Heritage Block', 6),
(7, N'Corniche Wing', 7),
(8, N'Nile Block', 8),
(9, N'Sunset Wing', 9),
(10, N'Lagoon Block', 10),
(11, N'Business Tower', 11),
(12, N'Beachfront Block', 12),
(13, N'Pyramids Wing', 13),
(14, N'Lagoon Bungalows', 14),
(15, N'Oasis Block', 15);
SET IDENTITY_INSERT dbo.Building OFF;
GO

SET IDENTITY_INSERT dbo.Customer ON;
INSERT INTO dbo.Customer (Id, Name, Location, Email, Phone, UserId) VALUES
(1,  N'Ahmed Salah',    N'Mansoura',   N'ahmed.salah@example.com',   1011112221, 1),
(2,  N'Mona Fathy',     N'Cairo',      N'mona.fathy@example.com',    1011112222, 2),
(3,  N'Omar Khaled',    N'Alexandria', N'omar.khaled@example.com',   1011112223, 3),
(4,  N'Sara Hassan',    N'Giza',       N'sara.hassan@example.com',   1011112224, 4),
(5,  N'Youssef Adel',   N'Tanta',      N'youssef.adel@example.com',  1011112225, 5),
(6,  N'Nour Ibrahim',   N'Zagazig',    N'nour.ibrahim@example.com',  1011112226, 6),
(7,  N'Karim Mostafa',  N'Aswan',      N'karim.mostafa@example.com', 1011112227, 7),
(8,  N'Laila Tarek',    N'Luxor',      N'laila.tarek@example.com',   1011112228, 8),
(9,  N'Hossam Nabil',   N'Suez',       N'hossam.nabil@example.com',  1011112229, 9),
(10, N'Dina Samir',     N'Ismailia',   N'dina.samir@example.com',    1011112230, 10),
(11, N'Tamer Fouad',    N'Damietta',   N'tamer.fouad@example.com',   1011112231, 11),
(12, N'Rania Emad',     N'Minya',      N'rania.emad@example.com',    1011112232, 12),
(13, N'Amr Hesham',     N'Beni Suef',  N'amr.hesham@example.com',    1011112233, 13),
(14, N'Heba Waleed',    N'Assiut',     N'heba.waleed@example.com',   1011112234, 14),
(15, N'Sherif Adham',   N'Sohag',      N'sherif.adham@example.com',  1011112235, 15),
(16, N'Yasmin Ashraf',  N'Qena',       N'yasmin.ashraf@example.com', 1011112236, 16),
(17, N'Mahmoud Reda',   N'Fayoum',     N'mahmoud.reda@example.com',  1011112237, 17),
(18, N'Aya Gamal',      N'Beheira',    N'aya.gamal@example.com',     1011112238, 18);
SET IDENTITY_INSERT dbo.Customer OFF;
GO

SET IDENTITY_INSERT dbo.Room ON;
INSERT INTO dbo.Room
    (Id, RommNumber, Price, IsAvailable, ChechIn, ChekOut, HotelId, BuildingId, RoomType)
VALUES
(101,  101, 1200, 1, '2026-09-01', '2026-09-05', 1, 1,  N'Standard'),
(102,  102, 1500, 0, '2026-09-02', '2026-09-06', 1, 1,  N'VIP'),
(201,  201, 1100, 1, '2026-09-03', '2026-09-07', 2, 2,  N'Standard'),
(202,  202, 1800, 1, '2026-09-04', '2026-09-08', 2, 2,  N'VIP'),
(301,  301,  900,  1, '2026-09-05', '2026-09-09', 3, 3,  N'Standard'),
(302,  302, 1600, 0, '2026-09-06', '2026-09-10', 3, 3,  N'VIP'),
(401,  401, 1300, 1, '2026-09-07', '2026-09-11', 4, 4,  N'Standard'),
(402,  402, 2000, 1, '2026-09-08', '2026-09-12', 4, 4,  N'VIP'),
(501,  501, 1000, 1, '2026-09-09', '2026-09-13', 5, 5,  N'Standard'),
(502,  502, 1700, 0, '2026-09-10', '2026-09-14', 5, 5,  N'VIP'),
(601,  601, 950,  1, '2026-09-11', '2026-09-15', 6, 6,  N'Standard'),
(602,  602, 1550, 1, '2026-09-12', '2026-09-16', 6, 6,  N'VIP'),
(701,  701, 1050, 1, '2026-09-13', '2026-09-17', 7, 7,  N'Standard'),
(702,  702, 1650, 0, '2026-09-14', '2026-09-18', 7, 7,  N'VIP'),
(801,  801, 1150, 1, '2026-09-15', '2026-09-19', 8, 8,  N'Standard'),
(802,  802, 1750, 1, '2026-09-16', '2026-09-20', 8, 8,  N'VIP'),
(901,  901, 1250, 1, '2026-09-17', '2026-09-21', 9, 9,  N'Standard'),
(902,  902, 1900, 0, '2026-09-18', '2026-09-22', 9, 9,  N'VIP'),
(1001, 1001, 1350, 1, '2026-09-19', '2026-09-23', 10, 10, N'Standard'),
(1002, 1002, 2100, 1, '2026-09-20', '2026-09-24', 10, 10, N'VIP');
SET IDENTITY_INSERT dbo.Room OFF;
GO

SET IDENTITY_INSERT dbo.Reservation ON;
INSERT INTO dbo.Reservation (Id, FromDate, ToDate, Status, HotelId, RoomId, CustomerId) VALUES
(1,  '2026-10-01', '2026-10-04', N'Confirmed', 1, 101, 1),
(2,  '2026-10-02', '2026-10-05', N'Pending',   1, 102, 2),
(3,  '2026-10-03', '2026-10-06', N'Confirmed', 2, 201, 3),
(4,  '2026-10-04', '2026-10-07', N'Cancelled', 2, 202, 4),
(5,  '2026-10-05', '2026-10-08', N'Confirmed', 3, 301, 5),
(6,  '2026-10-06', '2026-10-09', N'Confirmed', 3, 302, 6),
(7,  '2026-10-07', '2026-10-10', N'Pending',   4, 401, 7),
(8,  '2026-10-08', '2026-10-11', N'Confirmed', 4, 402, 8),
(9,  '2026-10-09', '2026-10-12', N'Confirmed', 5, 501, 9),
(10, '2026-10-10', '2026-10-13', N'Cancelled', 5, 502, 10),
(11, '2026-10-11', '2026-10-14', N'Confirmed', 6, 601, 11),
(12, '2026-10-12', '2026-10-15', N'Pending',   6, 602, 12),
(13, '2026-10-13', '2026-10-16', N'Confirmed', 7, 701, 13),
(14, '2026-10-14', '2026-10-17', N'Confirmed', 7, 702, 14),
(15, '2026-10-15', '2026-10-18', N'Confirmed', 8, 801, 15),
(16, '2026-10-16', '2026-10-19', N'Pending',   8, 802, 16),
(17, '2026-10-17', '2026-10-20', N'Confirmed', 9, 901, 17),
(18, '2026-10-18', '2026-10-21', N'Cancelled', 9, 902, 18),
(19, '2026-10-19', '2026-10-22', N'Confirmed', 10, 1001, 1),
(20, '2026-10-20', '2026-10-23', N'Confirmed', 10, 1002, 2);
SET IDENTITY_INSERT dbo.Reservation OFF;
GO

SET IDENTITY_INSERT dbo.Component ON;
INSERT INTO dbo.Component (Id, Name, Type) VALUES
(1,  N'Air Conditioner', N'Cooling'),
(2,  N'TV', N'Entertainment'),
(3,  N'Mini Bar', N'Amenity'),
(4,  N'Wi-Fi Router', N'Connectivity'),
(5,  N'Safe Box', N'Security'),
(6,  N'Hair Dryer', N'Amenity'),
(7,  N'Balcony', N'Structure'),
(8,  N'Jacuzzi', N'Luxury'),
(9,  N'Coffee Machine', N'Amenity'),
(10, N'Sea View Window', N'Structure'),
(11, N'Smart Lock', N'Security'),
(12, N'Room Heater', N'Heating'),
(13, N'Work Desk', N'Furniture'),
(14, N'Sound System', N'Entertainment'),
(15, N'Private Bathtub', N'Luxury');
SET IDENTITY_INSERT dbo.Component OFF;
GO

INSERT INTO dbo.RoomComponent (RoomId, ComponentId) VALUES
(101,1),(101,2),(101,4),
(102,1),(102,3),(102,8),
(201,1),(201,2),
(202,1),(202,5),(202,8),
(301,2),(301,4),
(302,1),(302,3),(302,15),
(401,1),(401,6),
(402,1),(402,13);
GO

INSERT INTO dbo.VipRoom
    (Id, LivingArea, LateCheckOutAllowed, LateCheckOutTime, LateCheckOutFee)
VALUES
(102, 45.50, 1, '14:00:00', 250.00),
(202, 50.00, 1, '15:00:00', 300.00),
(302, 42.75, 0, NULL, 0.00),
(402, 55.00, 1, '14:30:00', 275.00),
(502, 48.00, 1, '13:30:00', 225.00),
(602, 60.00, 1, '15:00:00', 350.00),
(702, 47.25, 0, NULL, 0.00),
(802, 52.00, 1, '14:00:00', 260.00),
(902, 58.50, 1, '16:00:00', 400.00),
(1002, 65.00, 1, '15:30:00', 450.00);
GO

SET IDENTITY_INSERT dbo.LateCheckOutRequest ON;
INSERT INTO dbo.LateCheckOutRequest
    (Id, VipRoomId, Approved, ExtraCharge, RequestedAt)
VALUES
(1, 102, 1, 250.00, '2026-10-04T09:15:00'),
(2, 202, 0, 0.00, '2026-10-06T10:00:00'),
(3, 402, 1, 275.00, '2026-10-10T08:30:00'),
(4, 502, 1, 225.00, '2026-10-12T11:45:00'),
(5, 602, 1, 350.00, '2026-10-14T09:00:00'),
(6, 802, 0, 0.00, '2026-10-18T12:20:00'),
(7, 902, 1, 400.00, '2026-10-20T10:10:00'),
(8, 1002, 1, 450.00, '2026-10-22T13:00:00');
SET IDENTITY_INSERT dbo.LateCheckOutRequest OFF;
GO