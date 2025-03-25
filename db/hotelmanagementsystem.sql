-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Mar 25, 2025 at 05:13 AM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `hotelmanagementsystem`
--

-- --------------------------------------------------------

--
-- Table structure for table `aspnetroleclaims`
--

CREATE TABLE `aspnetroleclaims` (
  `Id` int(11) NOT NULL,
  `RoleId` varchar(191) NOT NULL,
  `ClaimType` longtext DEFAULT NULL,
  `ClaimValue` longtext DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `aspnetroles`
--

CREATE TABLE `aspnetroles` (
  `Id` varchar(191) NOT NULL,
  `Name` varchar(256) DEFAULT NULL,
  `NormalizedName` varchar(191) DEFAULT NULL,
  `ConcurrencyStamp` longtext DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `aspnetroles`
--

INSERT INTO `aspnetroles` (`Id`, `Name`, `NormalizedName`, `ConcurrencyStamp`) VALUES
('22441498-c945-48f5-85a4-7c0d63ea3c4d', 'Admin', 'ADMIN', NULL),
('524c65f1-2bde-4141-b5fb-01cf4fa309df', 'FrontDesk', 'FRONTDESK', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `aspnetuserclaims`
--

CREATE TABLE `aspnetuserclaims` (
  `Id` int(11) NOT NULL,
  `UserId` varchar(191) NOT NULL,
  `ClaimType` longtext DEFAULT NULL,
  `ClaimValue` longtext DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `aspnetuserlogins`
--

CREATE TABLE `aspnetuserlogins` (
  `LoginProvider` varchar(191) NOT NULL,
  `ProviderKey` varchar(191) NOT NULL,
  `ProviderDisplayName` longtext DEFAULT NULL,
  `UserId` varchar(191) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `aspnetuserroles`
--

CREATE TABLE `aspnetuserroles` (
  `UserId` varchar(191) NOT NULL,
  `RoleId` varchar(191) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `aspnetuserroles`
--

INSERT INTO `aspnetuserroles` (`UserId`, `RoleId`) VALUES
('b679634f-00ae-45cb-9312-6fc6a85a15ee', '22441498-c945-48f5-85a4-7c0d63ea3c4d'),
('fd540351-74d7-4aca-86fa-dee643155a99', '524c65f1-2bde-4141-b5fb-01cf4fa309df');

-- --------------------------------------------------------

--
-- Table structure for table `aspnetusers`
--

CREATE TABLE `aspnetusers` (
  `Id` varchar(191) NOT NULL,
  `UserName` varchar(256) DEFAULT NULL,
  `NormalizedUserName` varchar(191) DEFAULT NULL,
  `Email` varchar(256) DEFAULT NULL,
  `NormalizedEmail` varchar(191) DEFAULT NULL,
  `EmailConfirmed` tinyint(1) NOT NULL,
  `PasswordHash` longtext DEFAULT NULL,
  `SecurityStamp` longtext DEFAULT NULL,
  `ConcurrencyStamp` longtext DEFAULT NULL,
  `PhoneNumber` longtext DEFAULT NULL,
  `PhoneNumberConfirmed` tinyint(1) NOT NULL,
  `TwoFactorEnabled` tinyint(1) NOT NULL,
  `LockoutEnd` datetime(6) DEFAULT NULL,
  `LockoutEnabled` tinyint(1) NOT NULL,
  `AccessFailedCount` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `aspnetusers`
--

INSERT INTO `aspnetusers` (`Id`, `UserName`, `NormalizedUserName`, `Email`, `NormalizedEmail`, `EmailConfirmed`, `PasswordHash`, `SecurityStamp`, `ConcurrencyStamp`, `PhoneNumber`, `PhoneNumberConfirmed`, `TwoFactorEnabled`, `LockoutEnd`, `LockoutEnabled`, `AccessFailedCount`) VALUES
('b2187c11-a547-4482-ad36-f5a49cfca12f', 'shifeng@gmail.com', 'SHIFENG@GMAIL.COM', 'shifeng@gmail.com', 'SHIFENG@GMAIL.COM', 0, 'AQAAAAIAAYagAAAAEFCjhodWHkJHd/A3j/5yFuHJgb1pXhrr5fT+PYZTete+A/rmyH2ThzRLQcQuB26TsA==', 'T7XDACMODR7HJBHDIB3IFHOK76UN547M', 'cd573dc1-b68e-4ea5-9d64-f4b82ca7dbf6', NULL, 0, 0, NULL, 1, 0),
('b679634f-00ae-45cb-9312-6fc6a85a15ee', 'Admin123@gmail.com', 'ADMIN123@GMAIL.COM', 'Admin123@gmail.com', 'ADMIN123@GMAIL.COM', 1, 'AQAAAAIAAYagAAAAEEPfaEZvbiU2rXFVBwt+XDDV3UGCtxvIqjUwwRe4Z+9b2WP6yOYpxmOPinLgwq5M4Q==', 'BORGDN535HMTCO2ZPVPWJAIQZ4VVB74Q', 'a2cf571e-5a96-4793-9142-e7bb8d996c9e', NULL, 0, 0, NULL, 1, 0),
('fd540351-74d7-4aca-86fa-dee643155a99', 'Frontdesk123@gmail.com', 'FRONTDESK123@GMAIL.COM', 'Frontdesk123@gmail.com', 'FRONTDESK123@GMAIL.COM', 1, 'AQAAAAIAAYagAAAAEEf98DFEz5LwDy4WdZIrQ2+BKBGbUi3J9k4rm9mWEckflWHgvbWypIaujz71vwbX5Q==', '2VEMJ4OWB5EON2MLUS3U6NERKHOAEDE4', 'a9aede05-6345-4f1d-abc9-57fd9a96f0d2', NULL, 0, 0, NULL, 1, 0);

-- --------------------------------------------------------

--
-- Table structure for table `aspnetusertokens`
--

CREATE TABLE `aspnetusertokens` (
  `UserId` varchar(191) NOT NULL,
  `LoginProvider` varchar(191) NOT NULL,
  `Name` varchar(191) NOT NULL,
  `Value` longtext DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `bookings`
--

CREATE TABLE `bookings` (
  `Id` int(11) NOT NULL,
  `RoomId` int(11) NOT NULL,
  `GuestName` longtext NOT NULL,
  `GuestEmail` longtext NOT NULL,
  `CheckinDate` datetime(6) NOT NULL,
  `CheckoutDate` datetime(6) NOT NULL,
  `Status` longtext NOT NULL,
  `InvoiceNumber` longtext NOT NULL,
  `PaymentStatus` longtext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `bookings`
--

INSERT INTO `bookings` (`Id`, `RoomId`, `GuestName`, `GuestEmail`, `CheckinDate`, `CheckoutDate`, `Status`, `InvoiceNumber`, `PaymentStatus`) VALUES
(13, 160, 'ShiFeng', 'ShiFeng@gmail.com', '2025-03-26 00:00:00.000000', '2025-03-27 00:00:00.000000', 'Confirmed', 'INV-20250325093926-160', 'Pending'),
(14, 163, 'ShiFeng', 'isuganreynan.ucmain@gmail.com', '2025-03-26 00:00:00.000000', '2025-03-31 00:00:00.000000', 'Confirmed', 'INV-20250325115503-163', 'Pending'),
(15, 164, 'Rambotan', 'rambotansinabaw@gmail.com', '2025-03-26 00:00:00.000000', '2025-03-31 00:00:00.000000', 'Confirmed', 'INV-20250325115555-164', 'Pending'),
(16, 161, 'Jun2', 'jun2gwapo@gmail.com', '2025-03-26 00:00:00.000000', '2025-03-31 00:00:00.000000', 'Rejected', 'INV-20250325115903-161', 'Pending'),
(17, 162, 'Reynante', 'reynante@gmail.com', '2025-03-26 00:00:00.000000', '2025-03-30 00:00:00.000000', 'Rejected', 'INV-20250325120737-162', 'Paid');

-- --------------------------------------------------------

--
-- Table structure for table `feedbacks`
--

CREATE TABLE `feedbacks` (
  `Id` int(11) NOT NULL,
  `UserName` longtext NOT NULL,
  `UserEmail` longtext NOT NULL,
  `Comment` varchar(500) NOT NULL,
  `Rating` int(11) NOT NULL,
  `SubmittedAt` datetime(6) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `hotels`
--

CREATE TABLE `hotels` (
  `Id` int(11) NOT NULL,
  `Name` longtext NOT NULL,
  `Description` longtext NOT NULL,
  `Location` longtext NOT NULL,
  `PricePerNight` decimal(65,30) NOT NULL,
  `ImageUrl` longtext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `hotels`
--

INSERT INTO `hotels` (`Id`, `Name`, `Description`, `Location`, `PricePerNight`, `ImageUrl`) VALUES
(1, 'Luxury Hotel', 'Experience luxury and comfort.', 'Manila', 0.000000000000000000000000000000, 'P1.jpg'),
(2, 'Beachfront Resort', 'Enjoy stunning ocean views.', 'Bohol', 0.000000000000000000000000000000, 'P2.jpg'),
(3, 'City Stay', 'Perfect for business travelers.', 'Cebu City', 0.000000000000000000000000000000, 'P3.jpg');

-- --------------------------------------------------------

--
-- Table structure for table `notifications`
--

CREATE TABLE `notifications` (
  `Id` int(11) NOT NULL,
  `UserId` varchar(191) NOT NULL,
  `Message` longtext NOT NULL,
  `IsRead` tinyint(1) NOT NULL,
  `CreatedAt` datetime(6) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `rooms`
--

CREATE TABLE `rooms` (
  `Id` int(11) NOT NULL,
  `RoomNumber` longtext NOT NULL,
  `RoomType` longtext NOT NULL,
  `PricePerNight` decimal(65,30) NOT NULL,
  `Status` longtext NOT NULL,
  `HotelId` int(11) NOT NULL,
  `ImageUrl` longtext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `rooms`
--

INSERT INTO `rooms` (`Id`, `RoomNumber`, `RoomType`, `PricePerNight`, `Status`, `HotelId`, `ImageUrl`) VALUES
(160, '101', 'Deluxe', 5000.000000000000000000000000000000, 'Occupied', 1, 'deluxe-room.jpg'),
(161, '102', 'Standard', 3000.000000000000000000000000000000, 'Occupied', 1, 'standard-room.jpg'),
(162, '103', 'Suite', 8000.000000000000000000000000000000, 'Occupied', 1, 'suite-room.jpg'),
(163, '101', 'Deluxe', 5000.000000000000000000000000000000, 'Occupied', 2, 'deluxe-room.jpg'),
(164, '102', 'Standard', 3000.000000000000000000000000000000, 'Occupied', 2, 'standard-room.jpg'),
(165, '103', 'Suite', 8000.000000000000000000000000000000, 'Vacant', 2, 'suite-room.jpg'),
(166, '101', 'Deluxe', 5000.000000000000000000000000000000, 'Vacant', 3, 'deluxe-room.jpg'),
(167, '102', 'Standard', 3000.000000000000000000000000000000, 'Vacant', 3, 'standard-room.jpg'),
(168, '103', 'Suite', 8000.000000000000000000000000000000, 'Vacant', 3, 'suite-room.jpg');

-- --------------------------------------------------------

--
-- Table structure for table `__efmigrationshistory`
--

CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `__efmigrationshistory`
--

INSERT INTO `__efmigrationshistory` (`MigrationId`, `ProductVersion`) VALUES
('20250323132626_InitialCreate', '9.0.3'),
('20250324132321_AddFeedbackTable', '9.0.3'),
('20250325024405_AddNotificationTable', '9.0.3');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `aspnetroleclaims`
--
ALTER TABLE `aspnetroleclaims`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_AspNetRoleClaims_RoleId` (`RoleId`);

--
-- Indexes for table `aspnetroles`
--
ALTER TABLE `aspnetroles`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `RoleNameIndex` (`NormalizedName`);

--
-- Indexes for table `aspnetuserclaims`
--
ALTER TABLE `aspnetuserclaims`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_AspNetUserClaims_UserId` (`UserId`);

--
-- Indexes for table `aspnetuserlogins`
--
ALTER TABLE `aspnetuserlogins`
  ADD PRIMARY KEY (`LoginProvider`,`ProviderKey`),
  ADD KEY `IX_AspNetUserLogins_UserId` (`UserId`);

--
-- Indexes for table `aspnetuserroles`
--
ALTER TABLE `aspnetuserroles`
  ADD PRIMARY KEY (`UserId`,`RoleId`),
  ADD KEY `IX_AspNetUserRoles_RoleId` (`RoleId`);

--
-- Indexes for table `aspnetusers`
--
ALTER TABLE `aspnetusers`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `UserNameIndex` (`NormalizedUserName`),
  ADD KEY `EmailIndex` (`NormalizedEmail`);

--
-- Indexes for table `aspnetusertokens`
--
ALTER TABLE `aspnetusertokens`
  ADD PRIMARY KEY (`UserId`,`LoginProvider`,`Name`);

--
-- Indexes for table `bookings`
--
ALTER TABLE `bookings`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Bookings_RoomId` (`RoomId`);

--
-- Indexes for table `feedbacks`
--
ALTER TABLE `feedbacks`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `hotels`
--
ALTER TABLE `hotels`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `notifications`
--
ALTER TABLE `notifications`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Notifications_UserId` (`UserId`);

--
-- Indexes for table `rooms`
--
ALTER TABLE `rooms`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Rooms_HotelId` (`HotelId`);

--
-- Indexes for table `__efmigrationshistory`
--
ALTER TABLE `__efmigrationshistory`
  ADD PRIMARY KEY (`MigrationId`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `aspnetroleclaims`
--
ALTER TABLE `aspnetroleclaims`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `aspnetuserclaims`
--
ALTER TABLE `aspnetuserclaims`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `bookings`
--
ALTER TABLE `bookings`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=18;

--
-- AUTO_INCREMENT for table `feedbacks`
--
ALTER TABLE `feedbacks`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `hotels`
--
ALTER TABLE `hotels`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `notifications`
--
ALTER TABLE `notifications`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `rooms`
--
ALTER TABLE `rooms`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=169;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `aspnetroleclaims`
--
ALTER TABLE `aspnetroleclaims`
  ADD CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE;

--
-- Constraints for table `aspnetuserclaims`
--
ALTER TABLE `aspnetuserclaims`
  ADD CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE;

--
-- Constraints for table `aspnetuserlogins`
--
ALTER TABLE `aspnetuserlogins`
  ADD CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE;

--
-- Constraints for table `aspnetuserroles`
--
ALTER TABLE `aspnetuserroles`
  ADD CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE;

--
-- Constraints for table `aspnetusertokens`
--
ALTER TABLE `aspnetusertokens`
  ADD CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE;

--
-- Constraints for table `bookings`
--
ALTER TABLE `bookings`
  ADD CONSTRAINT `FK_Bookings_Rooms_RoomId` FOREIGN KEY (`RoomId`) REFERENCES `rooms` (`Id`) ON DELETE CASCADE;

--
-- Constraints for table `notifications`
--
ALTER TABLE `notifications`
  ADD CONSTRAINT `FK_Notifications_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE;

--
-- Constraints for table `rooms`
--
ALTER TABLE `rooms`
  ADD CONSTRAINT `FK_Rooms_Hotels_HotelId` FOREIGN KEY (`HotelId`) REFERENCES `hotels` (`Id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
