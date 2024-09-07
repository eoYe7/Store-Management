-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Oct 16, 2023 at 03:16 PM
-- Server version: 10.4.27-MariaDB
-- PHP Version: 8.1.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `users`
--

-- --------------------------------------------------------

--
-- Table structure for table `items`
--

CREATE TABLE `items` (
  `id` int(11) NOT NULL,
  `name` varchar(200) NOT NULL,
  `Item_price` decimal(11,0) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- Dumping data for table `items`
--

INSERT INTO `items` (`id`, `name`, `Item_price`) VALUES
(1, 'purgure', '1200'),
(2, '\'ss\'', '0'),
(3, '\'juise\'', '0');

-- --------------------------------------------------------

--
-- Table structure for table `stocks`
--

CREATE TABLE `stocks` (
  `id` int(11) NOT NULL,
  `store_id` varchar(11) NOT NULL,
  `item_id` varchar(11) NOT NULL,
  `qantity` int(11) NOT NULL,
  `stock_type` varchar(12) NOT NULL DEFAULT '',
  `transaction_date` timestamp NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- Dumping data for table `stocks`
--

INSERT INTO `stocks` (`id`, `store_id`, `item_id`, `qantity`, `stock_type`, `transaction_date`) VALUES
(1, 'W_1', '1', 1, 'صرف مخزني', '2023-10-12 21:26:39'),
(2, 'W_1', '2', 2, 'إيداع مخزني', '2023-10-14 14:00:56'),
(3, 'W_1', '2', 2, 'إيداع مخزني', '2023-10-14 14:00:57'),
(4, 'W_1', '1', 0, 'إيداع مخزني', '2023-10-14 14:15:43'),
(5, 'W_1', '1', 0, 'صرف مخزني', '2023-10-14 14:16:52'),
(6, 'W_1', '1', 0, 'صرف مخزني', '2023-10-14 14:26:21'),
(7, 'W_1', '1', -2, 'صرف مخزني', '2023-10-14 14:33:07'),
(8, 'W_1', '1', 3, 'إيداع مخزني', '2023-10-14 14:33:20'),
(9, 'W_1', '1', -7, 'صرف مخزني', '2023-10-14 14:48:10'),
(10, 'STM_2', '1', 3, 'إيداع مخزني', '2023-10-15 18:37:26'),
(11, 'STM_2', '3', 2, 'إيداع مخزني', '2023-10-15 20:45:01');

-- --------------------------------------------------------

--
-- Table structure for table `stockstransaction`
--

CREATE TABLE `stockstransaction` (
  `id` int(11) NOT NULL,
  `stock_id` int(11) NOT NULL,
  `user_name` varchar(50) NOT NULL,
  `transactionType` varchar(50) NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `Updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `stockstransaction`
--

INSERT INTO `stockstransaction` (`id`, `stock_id`, `user_name`, `transactionType`, `created_at`, `Updated_at`) VALUES
(1, 2, 'salim', 'insert', '2023-10-14 14:00:56', NULL),
(2, 3, 'salim', 'insert', '2023-10-14 14:00:57', NULL),
(3, 4, 'salim', 'insert', '2023-10-14 14:15:43', NULL),
(4, 5, 'salim', 'insert', '2023-10-14 14:16:52', NULL),
(5, 6, 'salim', 'insert', '2023-10-14 14:26:21', NULL),
(6, 7, 'salim', 'insert', '2023-10-14 14:33:07', NULL),
(7, 8, 'salim', 'insert', '2023-10-14 14:33:20', NULL),
(8, 9, 'salim', 'insert', '2023-10-14 14:48:10', NULL),
(9, 10, 'salim', 'insert', '2023-10-15 18:37:26', NULL),
(10, 11, 'a', 'insert', '2023-10-15 20:45:01', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `stores`
--

CREATE TABLE `stores` (
  `id` int(11) NOT NULL,
  `store_code` varchar(25) NOT NULL,
  `store_name` varchar(50) NOT NULL,
  `location` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `stores`
--

INSERT INTO `stores` (`id`, `store_code`, `store_name`, `location`) VALUES
(1, 'W_1', 'التوفير', 'عصر'),
(2, 'STM_2', 'الافترضي', 'الحصبه');

-- --------------------------------------------------------

--
-- Table structure for table `user`
--

CREATE TABLE `user` (
  `id` int(11) NOT NULL,
  `name` varchar(200) NOT NULL,
  `pass` varchar(200) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- Dumping data for table `user`
--

INSERT INTO `user` (`id`, `name`, `pass`) VALUES
(1, 'a', '123'),
(2, 'h', '1'),
(3, 'user1', '1');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `items`
--
ALTER TABLE `items`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `stocks`
--
ALTER TABLE `stocks`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `stockstransaction`
--
ALTER TABLE `stockstransaction`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `stores`
--
ALTER TABLE `stores`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `store_code` (`store_code`(10));

--
-- Indexes for table `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `items`
--
ALTER TABLE `items`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `stocks`
--
ALTER TABLE `stocks`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT for table `stockstransaction`
--
ALTER TABLE `stockstransaction`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT for table `stores`
--
ALTER TABLE `stores`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `user`
--
ALTER TABLE `user`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
