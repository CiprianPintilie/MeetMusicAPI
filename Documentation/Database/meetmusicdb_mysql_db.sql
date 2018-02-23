-- phpMyAdmin SQL Dump
-- version 4.7.3
-- https://www.phpmyadmin.net/
--
-- Hôte : sylrusfrnrmeetmu.mysql.db
-- Généré le :  mer. 21 fév. 2018 à 09:29
-- Version du serveur :  5.5.55-0+deb7u1-log
-- Version de PHP :  5.6.33-0+deb8u1

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
  `user_id` int(255) NOT NULL,
  `message` longtext NOT NULL,
  `message_datetime` datetime NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table music_user
--

CREATE TABLE `music_user` (
  `id_user` int(255) NOT NULL,
  `family_id` int(2) NOT NULL
  `coefficient` float(2,2) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table music_family
--

CREATE TABLE `music_family` (
  `family_id` int(2) NOT NULL,
  `kind` varchar(255) NOT NULL,
  `family` varchar(255) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table user
--

CREATE TABLE `user` (
  `id` varchar(36) NOT NULL,
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
-- Index pour la table `m_messagerie`
--
ALTER TABLE `message`
  ADD PRIMARY KEY (`message_id`);

--
-- Index pour la table `m_musique`
--
ALTER TABLE `music_family`
  ADD PRIMARY KEY (`family_id`);

--
-- Index pour la table `m_utilisateur`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`id`) KEY_BLOCK_SIZE=1024;

ALTER TABLE `user`
  ADD UNIQUE (`username`) KEY_BLOCK_SIZE=1024;
--
-- AUTO_INCREMENT pour les tables déchargées
--

--
-- AUTO_INCREMENT pour la table `m_messagerie`
--
ALTER TABLE `message`
  MODIFY `message_id` int(255) NOT NULL AUTO_INCREMENT;

ALTER TABLE `music_user`
  FOREIGN KEY (`id_user`) REFERENCES user(`id`);

ALTER TABLE `music_user`
  FOREIGN KEY (`family_id`) REFERENCES music_family(`family_id`);

ALTER TABLE `music_user`
  ADD CONSTRAINT PK_UserMusic PRIMARY KEY (`id_user,family_id`);