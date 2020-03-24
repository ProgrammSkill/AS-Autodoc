CREATE DATABASE Auto_parts_shop

use Auto_parts_shop

CREATE TABLE Country (
ID_country INT PRIMARY KEY,
Country CHAR(30))

CREATE TABLE City (
ID_city INT PRIMARY KEY,
City CHAR(30))

CREATE TABLE Street (
ID_street INT PRIMARY KEY,
Street CHAR(30))

CREATE TABLE Suppliers (
ID_supplier INT PRIMARY KEY,
Title CHAR(150),
TIN BIGINT,
CIO BIGINT,
FIO_director CHAR(50),
ID_country INT FOREIGN KEY REFERENCES  Country(ID_country),
ID_city INT FOREIGN KEY REFERENCES City(ID_city),
ID_street INT FOREIGN KEY REFERENCES Street(ID_street),
House CHAR(15),
Telephone CHAR(20),
Email char(25))

CREATE TABLE Models (
ID_model INT PRIMARY KEY,
Title_model CHAR(50))

CREATE TABLE Brands (
ID_brand INT PRIMARY KEY,
Brand CHAR(30))

CREATE TABLE Car (
ID_car INT PRIMARY KEY,
ID_brand INT FOREIGN KEY REFERENCES Brands(ID_brand),
ID_model INT FOREIGN KEY REFERENCES Models(ID_models))

CREATE TABLE Autoparts (
ID_autoparts INT PRIMARY KEY,
Article CHAR(50),
Title CHAR(50),
ID_Car INT FOREIGN KEY REFERENCES Car(ID_Car),
Price FLOAT,
Comment CHAR(150))

CREATE TABLE Role_(
ID_role INT PRIMARY KEY,
Name_role char(20));

CREATE TABLE Position (
ID_position INT PRIMARY KEY,
Title char(30))

CREATE TABLE Users(
ID_user INT PRIMARY KEY,
Login_ char(15),
Password_ char(15),
Surname char(20),
First_name char(20),
Last_name char(20),
ID_position INT FOREIGN KEY REFERENCES Position (ID_position),
ID_role INT FOREIGN KEY REFERENCES Role_(ID_role));


SELECT Autoparts.ID_autoparts, Autoparts.Article, Autoparts.Title, Autoparts.ID_Car, Car.ID_brand, Brands.Brand, Car.ID_model, Models.Title_model, Autoparts.Price, Autoparts.Comment
FROM Autoparts INNER JOIN  Car 
ON Autoparts.ID_car=Car.ID_car
INNER JOIN Brands ON Brands.ID_brand=Car.ID_brand
INNER JOIN Models ON Models.ID_model=Car.ID_model


CREATE PROCEDURE dbo.SelectSuppliers
AS
BEGIN
SELECT Suppliers.ID_supplier, Suppliers.Title, Suppliers.TIN, Suppliers.CIO, Suppliers.FIO_director, Country.Country, City.City, Street.Street , Suppliers.House, Suppliers.Telephone, Suppliers.Email
FROM Suppliers INNER JOIN Country 
ON Suppliers.ID_country=Country.ID_country
INNER JOIN City ON Suppliers.ID_city=City.ID_city
INNER JOIN Street ON Suppliers.ID_street=Street.ID_street
END
GO
EXECUTE dbo.SelectSuppliers


CREATE PROCEDURE dbo.InsertSupplier
@id INT,
@title CHAR(15),
@tin BIGINT,
@cio BIGINT,
@fio CHAR(50),
@country INT,
@city INT,
@street INT,
@house CHAR(15),
@telephon CHAR(20),
@email CHAR(25)
AS
BEGIN
INSERT INTO [dbo].[Suppliers]
           ([ID_supplier]
           ,[Title]
           ,[TIN]
           ,[CIO]
           ,[FIO_director]
           ,[ID_country]
           ,[ID_city]
           ,[ID_street]
           ,[House]
           ,[Telephone]
           ,[Email])
     VALUES
           (@id, @title, @tin, @cio, @fio, @country, @city, @street, @house, @telephon, @email)
END
GO




CREATE PROCEDURE dbo.SelectAutoparts
AS
BEGIN
SELECT Autoparts.ID_autoparts, Autoparts.Article, Autoparts.Title, Autoparts.ID_Car, Car.ID_brand, Brands.Brand, Car.ID_model, Models.Title_model, Autoparts.Price, Autoparts.Comment
FROM Autoparts INNER JOIN  Car 
ON Autoparts.ID_car=Car.ID_car
INNER JOIN Brands ON Brands.ID_brand=Car.ID_brand
INNER JOIN Models ON Models.ID_model=Car.ID_model
END
GO
EXECUTE dbo.SelectAutoparts


CREATE PROCEDURE dbo.InsertAutoparts
@id int,
@a char(50),
@t char(50),
@idc int,
@p float,
@comm char(150)
AS
BEGIN
INSERT INTO [dbo].[Autoparts]
           ([ID_autoparts]
           ,[Article]
           ,[Title]
           ,[ID_Car]
           ,[Price]
           ,[Comment])
     VALUES
           (<ID_autoparts, int,>
           ,<Article, char(50),>
           ,<Title, char(50),>
           ,<ID_Car, int,>
           ,<Price, float,>
           ,<Comment, char(150),>)
GO

