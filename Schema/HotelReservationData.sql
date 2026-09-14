
USE HotelReservationSystem;
GO



INSERT INTO dbo.Hotel (Id, Name) VALUES
(1,  N'Grand Nile Hotel'),
(2,  N'Sea View Resort'),
(3,  N'Al Mansoura Palace'),
(4,  N'Cairo Tower Hotel'),
(5,  N'Red Sea Paradise'),
(6,  N'Luxor Heritage Hotel'),
(7,  N'Alexandria Corniche Hotel'),
(8,  N'Aswan Nile Resort'),
(9,  N'Sharm Sunset Bay'),
(10, N'Hurghada Lagoon Hotel'),
(11, N'Delta Business Hotel'),
(12, N'Marsa Matrouh Beach Hotel'),
(13, N'Giza Pyramids View Hotel'),
(14, N'Dahab Blue Lagoon'),
(15, N'Fayoum Oasis Hotel');

GO



INSERT INTO dbo.Building (Id, Location, HotelId) VALUES
(1,  N'Main Building - Ground Floor Block', 1),
(2,  N'Sea Wing', 2),
(3,  N'Central Block', 3),
(4,  N'Tower A', 4),
(5,  N'Beach Wing', 5),
(6,  N'Heritage Block', 6),
(7,  N'Corniche Wing', 7),
(8,  N'Nile Block', 8),
(9,  N'Sunset Wing', 9),
(10, N'Lagoon Block', 10),
(11, N'Business Tower', 11),
(12, N'Beachfront Block', 12),
(13, N'Pyramids Wing', 13),
(14, N'Lagoon Bungalows', 14),
(15, N'Oasis Block', 15);

GO


INSERT INTO dbo.Room (Id, RommNumber, Price, IsAvailable, ChechIn, ChekOut, HotelId, BuildingId, RoomType) VALUES
(1,  101, 1200, 1, '2026-09-01', '2026-09-05', 1,  1,  N'Standard'),
(2,  102, 1500, 0, '2026-09-02', '2026-09-06', 1,  1,  N'VIP'),
(3,  201, 1100, 1, '2026-09-03', '2026-09-07', 2,  2,  N'Standard'),
(4,  202, 1800, 1, '2026-09-04', '2026-09-08', 2,  2,  N'VIP'),
(5,  301, 900,  1, '2026-09-05', '2026-09-09', 3,  3,  N'Standard'),
(6,  302, 1600, 0, '2026-09-06', '2026-09-10', 3,  3,  N'VIP'),
(7,  401, 1300, 1, '2026-09-07', '2026-09-11', 4,  4,  N'Standard'),
(8,  402, 2000, 1, '2026-09-08', '2026-09-12', 4,  4,  N'VIP'),
(9,  501, 1000, 1, '2026-09-09', '2026-09-13', 5,  5,  N'Standard'),
(10, 502, 1700, 0, '2026-09-10', '2026-09-14', 5,  5,  N'VIP'),
(11, 601, 950,  1, '2026-09-11', '2026-09-15', 6,  6,  N'Standard'),
(12, 602, 1550, 1, '2026-09-12', '2026-09-16', 6,  6,  N'VIP'),
(13, 701, 1050, 1, '2026-09-13', '2026-09-17', 7,  7,  N'Standard'),
(14, 702, 1650, 0, '2026-09-14', '2026-09-18', 7,  7,  N'VIP'),
(15, 801, 1150, 1, '2026-09-15', '2026-09-19', 8,  8,  N'Standard'),
(16, 802, 1750, 1, '2026-09-16', '2026-09-20', 8,  8,  N'VIP'),
(17, 901, 1250, 1, '2026-09-17', '2026-09-21', 9,  9,  N'Standard'),
(18, 902, 1900, 0, '2026-09-18', '2026-09-22', 9,  9,  N'VIP'),
(19, 1001, 1350, 1, '2026-09-19', '2026-09-23', 10, 10, N'Standard'),
(20, 1002, 2100, 1, '2026-09-20', '2026-09-24', 10, 10, N'VIP');

GO



INSERT INTO dbo.Customer (Id, Name, Username, Password, Location, Email, Phone) VALUES
(1,  N'Ahmed Salah',    N'ahmed.salah',    N'Pass@123', N'Mansoura',   N'ahmed.salah@example.com',    N'01011112221'),
(2,  N'Mona Fathy',     N'mona.fathy',     N'Pass@123', N'Cairo',      N'mona.fathy@example.com',     N'01011112222'),
(3,  N'Omar Khaled',    N'omar.khaled',    N'Pass@123', N'Alexandria', N'omar.khaled@example.com',    N'01011112223'),
(4,  N'Sara Hassan',    N'sara.hassan',    N'Pass@123', N'Giza',       N'sara.hassan@example.com',    N'01011112224'),
(5,  N'Youssef Adel',   N'youssef.adel',   N'Pass@123', N'Tanta',      N'youssef.adel@example.com',   N'01011112225'),
(6,  N'Nour Ibrahim',   N'nour.ibrahim',   N'Pass@123', N'Zagazig',    N'nour.ibrahim@example.com',   N'01011112226'),
(7,  N'Karim Mostafa',  N'karim.mostafa',  N'Pass@123', N'Aswan',      N'karim.mostafa@example.com',  N'01011112227'),
(8,  N'Laila Tarek',    N'laila.tarek',    N'Pass@123', N'Luxor',      N'laila.tarek@example.com',    N'01011112228'),
(9,  N'Hossam Nabil',   N'hossam.nabil',   N'Pass@123', N'Suez',       N'hossam.nabil@example.com',   N'01011112229'),
(10, N'Dina Samir',     N'dina.samir',     N'Pass@123', N'Ismailia',   N'dina.samir@example.com',     N'01011112230'),
(11, N'Tamer Fouad',    N'tamer.fouad',    N'Pass@123', N'Damietta',   N'tamer.fouad@example.com',    N'01011112231'),
(12, N'Rania Emad',     N'rania.emad',     N'Pass@123', N'Minya',      N'rania.emad@example.com',     N'01011112232'),
(13, N'Amr Hesham',     N'amr.hesham',     N'Pass@123', N'Beni Suef',  N'amr.hesham@example.com',     N'01011112233'),
(14, N'Heba Waleed',    N'heba.waleed',    N'Pass@123', N'Assiut',     N'heba.waleed@example.com',    N'01011112234'),
(15, N'Sherif Adham',   N'sherif.adham',   N'Pass@123', N'Sohag',      N'sherif.adham@example.com',   N'01011112235'),
(16, N'Yasmin Ashraf',  N'yasmin.ashraf',  N'Pass@123', N'Qena',       N'yasmin.ashraf@example.com',  N'01011112236'),
(17, N'Mahmoud Reda',   N'mahmoud.reda',   N'Pass@123', N'Fayoum',     N'mahmoud.reda@example.com',   N'01011112237'),
(18, N'Aya Gamal',      N'aya.gamal',      N'Pass@123', N'Beheira',    N'aya.gamal@example.com',      N'01011112238');

GO


INSERT INTO dbo.Reservation (Id, FromDate, ToDate, Status, HotelId, RoomId, CustomerId) VALUES
(1,  '2026-10-01', '2026-10-04', N'Confirmed', 1,  1,  1),
(2,  '2026-10-02', '2026-10-05', N'Pending',   1,  2,  2),
(3,  '2026-10-03', '2026-10-06', N'Confirmed', 2,  3,  3),
(4,  '2026-10-04', '2026-10-07', N'Cancelled', 2,  4,  4),
(5,  '2026-10-05', '2026-10-08', N'Confirmed', 3,  5,  5),
(6,  '2026-10-06', '2026-10-09', N'Confirmed', 3,  6,  6),
(7,  '2026-10-07', '2026-10-10', N'Pending',   4,  7,  7),
(8,  '2026-10-08', '2026-10-11', N'Confirmed', 4,  8,  8),
(9,  '2026-10-09', '2026-10-12', N'Confirmed', 5,  9,  9),
(10, '2026-10-10', '2026-10-13', N'Cancelled', 5,  10, 10),
(11, '2026-10-11', '2026-10-14', N'Confirmed', 6,  11, 11),
(12, '2026-10-12', '2026-10-15', N'Pending',   6,  12, 12),
(13, '2026-10-13', '2026-10-16', N'Confirmed', 7,  13, 13),
(14, '2026-10-14', '2026-10-17', N'Confirmed', 7,  14, 14),
(15, '2026-10-15', '2026-10-18', N'Confirmed', 8,  15, 15),
(16, '2026-10-16', '2026-10-19', N'Pending',   8,  16, 16),
(17, '2026-10-17', '2026-10-20', N'Confirmed', 9,  17, 17),
(18, '2026-10-18', '2026-10-21', N'Cancelled', 9,  18, 18),
(19, '2026-10-19', '2026-10-22', N'Confirmed', 10, 19, 1),
(20, '2026-10-20', '2026-10-23', N'Confirmed', 10, 20, 2);

GO



INSERT INTO dbo.Component (Id, Name, Type) VALUES
(1,  N'Air Conditioner',   N'Cooling'),
(2,  N'TV',                N'Entertainment'),
(3,  N'Mini Bar',          N'Amenity'),
(4,  N'Wi-Fi Router',      N'Connectivity'),
(5,  N'Safe Box',          N'Security'),
(6,  N'Hair Dryer',        N'Amenity'),
(7,  N'Balcony',           N'Structure'),
(8,  N'Jacuzzi',           N'Luxury'),
(9,  N'Coffee Machine',    N'Amenity'),
(10, N'Sea View Window',   N'Structure'),
(11, N'Smart Lock',        N'Security'),
(12, N'Room Heater',       N'Heating'),
(13, N'Work Desk',         N'Furniture'),
(14, N'Sound System',      N'Entertainment'),
(15, N'Private Bathtub',   N'Luxury');

GO

INSERT INTO dbo.RoomComponent (RoomId, ComponentId) VALUES
(1,  1), (1,  2), (1,  4),
(2,  1), (2,  3), (2,  8),
(3,  1), (3,  2),
(4,  1), (4,  5), (4,  8),
(5,  2), (5,  4),
(6,  1), (6,  3), (6,  15),
(7,  1), (7,  6),
(8,  1), (8,  13);

GO