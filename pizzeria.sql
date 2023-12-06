-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Dec 06, 2023 at 11:44 PM
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
-- Database: `pizzeria`
--

-- --------------------------------------------------------

--
-- Table structure for table `dishes`
--

CREATE TABLE `dishes` (
  `DishID` int(11) NOT NULL,
  `DishName` varchar(50) NOT NULL,
  `Price` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `dishes`
--

INSERT INTO `dishes` (`DishID`, `DishName`, `Price`) VALUES
(10, 'Пицца', 50),
(12, 'Шаурма', 150),
(13, 'Биг-Мак', 200);

-- --------------------------------------------------------

--
-- Table structure for table `dishingredients`
--

CREATE TABLE `dishingredients` (
  `DishIngredientID` int(11) NOT NULL,
  `DishID` int(11) NOT NULL,
  `ProductID` int(11) NOT NULL,
  `QuantityRequired` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `dishingredients`
--

INSERT INTO `dishingredients` (`DishIngredientID`, `DishID`, `ProductID`, `QuantityRequired`) VALUES
(70, 12, 3, 45),
(71, 12, 4, 45),
(72, 12, 5, 45);

-- --------------------------------------------------------

--
-- Table structure for table `employees`
--

CREATE TABLE `employees` (
  `EmployeeID` int(11) NOT NULL,
  `EmployeeName` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `employees`
--

INSERT INTO `employees` (`EmployeeID`, `EmployeeName`) VALUES
(1, 'Alexei Cozlov'),
(2, 'Vladimir Ivanov');

-- --------------------------------------------------------

--
-- Table structure for table `employeesalaries`
--

CREATE TABLE `employeesalaries` (
  `SalaryID` int(11) NOT NULL,
  `EmployeeID` int(11) NOT NULL,
  `CommissionAmount` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `employeesalaries`
--

INSERT INTO `employeesalaries` (`SalaryID`, `EmployeeID`, `CommissionAmount`) VALUES
(7, 1, 1140);

-- --------------------------------------------------------

--
-- Table structure for table `orders`
--

CREATE TABLE `orders` (
  `OrderID` int(11) NOT NULL,
  `OrderDate` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `CustomerName` varchar(50) NOT NULL,
  `DishID` int(11) NOT NULL,
  `Quantity` int(11) NOT NULL,
  `TotalPrice` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `orders`
--

INSERT INTO `orders` (`OrderID`, `OrderDate`, `CustomerName`, `DishID`, `Quantity`, `TotalPrice`) VALUES
(25, '2023-12-06 20:08:38', '', 12, 4, 600),
(26, '2023-12-06 21:25:17', 'Алексей', 12, 44, 6600),
(27, '2023-12-06 21:26:09', 'Иван', 12, 15, 2250),
(28, '2023-12-06 21:26:33', 'пп', 12, 444, 66600),
(29, '2023-12-06 21:27:00', 'Иван', 12, 45, 6750),
(30, '2023-12-06 21:28:22', 'Иван', 12, 15, 2250),
(31, '2023-12-06 21:30:01', 'ааа', 12, 4, 600),
(32, '2023-12-06 21:31:28', 'Иван', 12, 4, 600),
(33, '2023-12-06 22:19:30', 'аааа', 12, 15, 2250),
(34, '2023-12-06 22:19:32', 'аааа', 12, 5, 750),
(35, '2023-12-06 22:19:37', 'аааа', 12, 5, 750),
(36, '2023-12-06 22:19:39', 'аааа', 12, 4, 600),
(37, '2023-12-06 22:22:37', 'аааа', 12, 15, 2250),
(38, '2023-12-06 22:22:38', 'аааа', 12, 15, 2250),
(39, '2023-12-06 22:22:39', 'аааа', 12, 15, 2250),
(40, '2023-12-06 22:22:49', 'аааа', 12, 15, 2250),
(41, '2023-12-06 22:22:51', 'аааа', 12, 15, 2250),
(42, '2023-12-06 22:22:56', 'аааа', 12, 1, 150);

-- --------------------------------------------------------

--
-- Table structure for table `products`
--

CREATE TABLE `products` (
  `ProductID` int(11) NOT NULL,
  `Name` varchar(30) NOT NULL,
  `Quantity` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `products`
--

INSERT INTO `products` (`ProductID`, `Name`, `Quantity`) VALUES
(3, 'Вода', 74979),
(4, 'Помидоры', 419376),
(5, 'Мука', 4630535),
(6, 'Пармезан', 454),
(8, 'Яйца', 4700);

-- --------------------------------------------------------

--
-- Table structure for table `sales`
--

CREATE TABLE `sales` (
  `SalesID` int(11) NOT NULL,
  `OrderID` int(11) NOT NULL,
  `SaleDate` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `Amount` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `dishes`
--
ALTER TABLE `dishes`
  ADD PRIMARY KEY (`DishID`);

--
-- Indexes for table `dishingredients`
--
ALTER TABLE `dishingredients`
  ADD PRIMARY KEY (`DishIngredientID`),
  ADD UNIQUE KEY `DishID` (`DishID`,`ProductID`),
  ADD KEY `ProductID` (`ProductID`);

--
-- Indexes for table `employees`
--
ALTER TABLE `employees`
  ADD PRIMARY KEY (`EmployeeID`);

--
-- Indexes for table `employeesalaries`
--
ALTER TABLE `employeesalaries`
  ADD PRIMARY KEY (`SalaryID`),
  ADD UNIQUE KEY `EmployeeID` (`EmployeeID`);

--
-- Indexes for table `orders`
--
ALTER TABLE `orders`
  ADD PRIMARY KEY (`OrderID`),
  ADD KEY `DishID` (`DishID`);

--
-- Indexes for table `products`
--
ALTER TABLE `products`
  ADD PRIMARY KEY (`ProductID`);

--
-- Indexes for table `sales`
--
ALTER TABLE `sales`
  ADD PRIMARY KEY (`SalesID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `dishes`
--
ALTER TABLE `dishes`
  MODIFY `DishID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;

--
-- AUTO_INCREMENT for table `dishingredients`
--
ALTER TABLE `dishingredients`
  MODIFY `DishIngredientID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=73;

--
-- AUTO_INCREMENT for table `employees`
--
ALTER TABLE `employees`
  MODIFY `EmployeeID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `employeesalaries`
--
ALTER TABLE `employeesalaries`
  MODIFY `SalaryID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT for table `orders`
--
ALTER TABLE `orders`
  MODIFY `OrderID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=43;

--
-- AUTO_INCREMENT for table `products`
--
ALTER TABLE `products`
  MODIFY `ProductID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT for table `sales`
--
ALTER TABLE `sales`
  MODIFY `SalesID` int(11) NOT NULL AUTO_INCREMENT;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `dishingredients`
--
ALTER TABLE `dishingredients`
  ADD CONSTRAINT `dishingredients_ibfk_1` FOREIGN KEY (`DishID`) REFERENCES `dishes` (`DishID`),
  ADD CONSTRAINT `dishingredients_ibfk_2` FOREIGN KEY (`ProductID`) REFERENCES `products` (`ProductID`);

--
-- Constraints for table `employeesalaries`
--
ALTER TABLE `employeesalaries`
  ADD CONSTRAINT `employeesalaries_ibfk_1` FOREIGN KEY (`EmployeeID`) REFERENCES `employees` (`EmployeeID`);

--
-- Constraints for table `orders`
--
ALTER TABLE `orders`
  ADD CONSTRAINT `orders_ibfk_1` FOREIGN KEY (`DishID`) REFERENCES `dishes` (`DishID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
