
create database ArtGallery

create table RoleType(
IDRoleType int PRIMARY KEY IDENTITY,
Type VARCHAR(50),
);
create table Users(
IDUser int PRIMARY KEY IDENTITY,
Username VARCHAR(50) NOT NULL,
FirstName VARCHAR(50) NOT NULL,
LastName VARCHAR(50) NOT NULL,
Email VARCHAR(100) NOT NULL,
Password VARCHAR(255) NOT NULL,
Picture varbinary(max),
RoleTypeID int,
FOREIGN KEY (RoleTypeID) REFERENCES RoleType(IDRoleType),
);
create table ArtWorkType(
IDArtWorkType int PRIMARY KEY IDENTITY,
Type VARCHAR(50),
);

create table ArtWork(
IDArtWork int PRIMARY KEY IDENTITY,
Title VARCHAR(50),
Description VARCHAR(150),
Picture varbinary(max),
Price money,
PublicationDate datetime,
UserID int,
FOREIGN KEY (UserID) REFERENCES Users(IDUser) ON DELETE CASCADE ON UPDATE CASCADE,
ArtWorkTypeID int,
FOREIGN KEY (ArtWorkTypeID) REFERENCES ArtWorkType(IDArtWorkType),
);

create table PaymentType(
IDPaymentType int PRIMARY KEY IDENTITY,
Type VARCHAR(50),
);

create table "Order"(
IDOrder int PRIMARY KEY IDENTITY,
OrderDate datetime,
UserID int,
FOREIGN KEY (UserID) REFERENCES Users(IDUser) ON DELETE CASCADE ON UPDATE CASCADE,
PaymentTypeID int,
FOREIGN KEY (PaymentTypeID) REFERENCES PaymentType(IDPaymentType),
);

create table OrderItem(
IDOrderItem int PRIMARY KEY IDENTITY,
OrderID int,
FOREIGN KEY (OrderID) REFERENCES "Order"(IDOrder),
ArtWorkID int,
FOREIGN KEY (ArtWorkID) REFERENCES ArtWork(IDArtWork),
);



