SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";

--
-- Database :  `meetmusicdb`
--
CREATE DATABASE IF NOT EXISTS `meetmusicdb` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `meetmusicdb`;

-- --------------------------------------------------------

--
-- Table message
--

CREATE TABLE `message` (
  `message_id` int(255) NOT NULL,
  `user_id` varchar(36) NOT NULL,
  `message` longtext NOT NULL,
  `message_datetime` datetime NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table music_user
--

CREATE TABLE `music_user` (
  `user_id` varchar(36) NOT NULL,
  `family_id` int(255) NOT NULL,
  `coefficient` float(2,2) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table music_family
--

CREATE TABLE `music_family` (
  `family_id` int(255) NOT NULL,
  `name` varchar(255) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table music_genre
--

CREATE TABLE `music_genre` (
  `genre_id` int(255) NOT NULL,
  `family_id` int(255) NOT NULL,
  `name` varchar(255) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table user
--

CREATE TABLE `user` (
  `user_id` varchar(36) NOT NULL,
  `username` varchar(20) NOT NULL,
  `password` varchar(255) NOT NULL,
  `firstname` varchar(40) NOT NULL,
  `lastname` varchar(40) NOT NULL,
  `mail` varchar(255) NOT NULL,
  `gender` int(1) NOT NULL,
  `avatar_url` varchar(255) DEFAULT NULL,
  `phone` varchar(16) DEFAULT NULL,
  `birth_date` date NOT NULL,
  `description` mediumtext,
  `longitude` varchar(255) NOT NULL,
  `latitude` varchar(255) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

--
-- TABLE CONSTRAINTS
--

-- message

ALTER TABLE `message`
  ADD PRIMARY KEY (`message_id`);

ALTER TABLE `message`
  MODIFY `message_id` int(255) NOT NULL AUTO_INCREMENT;

-- --------------------------------------------------------

-- music_family

ALTER TABLE `music_family`
  ADD PRIMARY KEY (`family_id`);

ALTER TABLE `music_family`
  ADD UNIQUE (`name`) KEY_BLOCK_SIZE=1024;

ALTER TABLE `music_family`
  MODIFY `family_id` int(255) NOT NULL AUTO_INCREMENT;

-- --------------------------------------------------------

-- music_genre

ALTER TABLE `music_genre`
  ADD PRIMARY KEY (`genre_id`);

ALTER TABLE `music_genre`
  ADD UNIQUE (`name`) KEY_BLOCK_SIZE=1024;

ALTER TABLE `music_genre`
  MODIFY `genre_id` int(255) NOT NULL AUTO_INCREMENT;

ALTER TABLE `music_genre`
  ADD FOREIGN KEY (`family_id`) REFERENCES music_family(`family_id`);

-- --------------------------------------------------------

-- user

ALTER TABLE `user`
  ADD PRIMARY KEY (`user_id`) KEY_BLOCK_SIZE=1024;

ALTER TABLE `user`
  ADD UNIQUE (`username`) KEY_BLOCK_SIZE=1024;

-- --------------------------------------------------------

-- music_user

ALTER TABLE `music_user`
  ADD FOREIGN KEY (`user_id`) REFERENCES user(`user_id`);

ALTER TABLE `music_user`
  ADD FOREIGN KEY (`family_id`) REFERENCES music_family(`family_id`);

ALTER TABLE `music_user`
  ADD CONSTRAINT PK_UserMusic PRIMARY KEY (`user_id`,`family_id`);

ALTER TABLE `music_user`
  CHANGE `coefficient` `rank` integer(255);

-- --------------------------------------------------------