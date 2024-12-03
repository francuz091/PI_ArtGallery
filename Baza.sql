
create database ArtGallery

create table "User"(
IDUser int PRIMARY KEY IDENTITY,
Username VARCHAR(50) NOT NULL,
FirstName VARCHAR(50) NOT NULL,
LastName VARCHAR(50) NOT NULL,
Email VARCHAR(100) NOT NULL,
Password VARCHAR(255) NOT NULL,
Picture varbinary(max),
Role ENUM('USER', 'ADMIN') DEFAULT 'USER'
);