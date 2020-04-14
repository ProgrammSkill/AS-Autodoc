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
Title_brand CHAR(30))

CREATE TABLE Brands_and_models (
ID_brd_mdl INT PRIMARY KEY,
ID_brand INT FOREIGN KEY REFERENCES Brands(ID_brand),
ID_model INT FOREIGN KEY REFERENCES Models(ID_model))

CREATE TABLE Manufacturers (
ID_manufacturer INT PRIMARY KEY,
Manufacturer CHAR(30),
ID_country INT FOREIGN KEY REFERENCES  Country(ID_country))

CREATE TABLE Autoparts (
ID_autoparts INT PRIMARY KEY,
Article CHAR(50),
Title CHAR(50),
ID_brd_mdl INT FOREIGN KEY REFERENCES Car(ID_Car),
ID_manufacturer INT FOREIGN KEY REFERENCES Manufacturers(ID_manufacturer),
Comment CHAR(150))

CREATE TABLE Supply (
ID_supply INT PRIMARY KEY,
ID_supplier INT FOREIGN KEY REFERENCES Suppliers(ID_supplier),
ID_autoparts INT FOREIGN KEY REFERENCES Autoparts(ID_autoparts),
Purchase_price NUMERIC(8,2),
Amount INT,
Delivery_date DATE)

CREATE TABLE Department_store (
ID_department INT PRIMARY KEY,
ID_city INT FOREIGN KEY REFERENCES City(ID_city),
ID_street INT FOREIGN KEY REFERENCES Street(ID_street),
House CHAR(15),
Telephone CHAR(20))

CREATE TABLE Availability_auto_parts (
ID_availability INT PRIMARY KEY,
ID_department INT FOREIGN KEY REFERENCES Department_store(ID_department),
ID_autoparts INT FOREIGN KEY REFERENCES Autoparts(ID_autoparts),
Price_holiday NUMERIC(8,2),
Amount INT)

CREATE TABLE Sale (
ID_sale INT PRIMARY KEY,
ID_department INT FOREIGN KEY REFERENCES Department_store(ID_department),
ID_autoparts INT FOREIGN KEY REFERENCES Autoparts(ID_autoparts),
Amount INT,
Date_of_sale DATE)

CREATE TABLE Role_(
ID_role INT PRIMARY KEY,
Role_ CHAR(20));

CREATE TABLE Position (
ID_position INT PRIMARY KEY,
Position CHAR(30))

CREATE TABLE Users (
Login_ CHAR(15) PRIMARY KEY,
Password_ char(15),
ID_role INT FOREIGN KEY REFERENCES Role_(ID_role))

CREATE TABLE InfoUsers (
Login_ CHAR(15) FOREIGN KEY REFERENCES Users(Login_),
Surname char(20),
First_name char(20),
Last_name char(20),
ID_position INT FOREIGN KEY REFERENCES Position (ID_position),
Address_ char(50),
Telephone CHAR(20),
Date_of_acceptance DATE,
Date_of_termination DATE)


CREATE PROCEDURE dbo.InsertUsers
@login CHAR(15),
@pass CHAR(15),
@role INT,
@surname CHAR(20),
@f_name CHAR(20),
@l_name CHAR(20),
@pos INT,
@adress CHAR(50),
@tel CHAR(20),
@da DATE,
@dt DATE
AS
BEGIN
INSERT INTO [dbo].[Users]
           ([Login_]
           ,[Password_]
           ,[ID_role])
     VALUES
           (@login,
            @pass,
            @role)
INSERT INTO [dbo].[InfoUsers]
           ([Login_]
           ,[Surname]
           ,[First_name]
           ,[Last_name]
           ,[ID_position]
           ,[Address_]
           ,[Telephone]
           ,[Date_of_acceptance]
           ,[Date_of_termination])
     VALUES
           (@login,
            @surname,
            @f_name,
            @l_name,
            @pos,
            @adress,
            @tel,
            @da,
            @dt)
END
GO
EXECUTE dbo.InsertUsers 'fvdfd', '3545fg', 2, 'xccsd', 'dss', 'dsss', 1, 'dsdsds', '343433', 31.12.99, 01.04.20






CREATE PROCEDURE dbo.InsertCountry
@id INT,
@c CHAR(30)
AS
BEGIN
INSERT INTO [dbo].[Country]
           ([ID_country]
           ,[Country])
     VALUES
           (@id, @c)
END
GO


CREATE PROCEDURE dbo.EditCountry
@id INT,
@c CHAR(30)
AS
BEGIN
UPDATE [dbo].[Country]
   SET [ID_country] = @id,
       [Country] = @c
 WHERE ID_country=@id
END
GO


CREATE PROCEDURE dbo.InsertCity
@id INT,
@c CHAR(30)
AS
BEGIN
INSERT INTO [dbo].[City]
           ([ID_city]
           ,[City])
     VALUES
           (@id, @c)
END
GO

CREATE PROCEDURE dbo.EditCity
@id INT,
@c CHAR(30)
AS
BEGIN
UPDATE [dbo].[City]
   SET [ID_city] = @id,
       [City] = @c
 WHERE ID_city=@id
END
GO



CREATE PROCEDURE dbo.InsertStreet
@id INT,
@s CHAR(30)
AS
BEGIN
INSERT INTO [dbo].[Street]
           ([ID_street]
           ,[Street])
     VALUES
           (@id, @s)
END
GO

CREATE PROCEDURE dbo.EditStreet
@id INT,
@s CHAR(30)
AS
BEGIN
UPDATE [dbo].[Street]
   SET [ID_street] =@id,
       [Street] = @s
 WHERE ID_street=@id
END
GO



CREATE PROCEDURE dbo.InsertBrand
@id INT,
@b CHAR(30)
AS
BEGIN
INSERT INTO [dbo].[Brands]
           ([ID_brand]
           ,[Title_brand])
     VALUES
           (@id,
            @b)
END
GO

CREATE PROCEDURE dbo.EditBrand
@id INT,
@b CHAR(30)
AS
BEGIN
UPDATE [dbo].[Brands]
   SET [ID_brand] =@id,
       [Title_brand] = @b
 WHERE ID_brand=@id
END
GO


CREATE PROCEDURE dbo.InsertModel
@id INT,
@m CHAR(30)
AS
BEGIN
INSERT INTO [dbo].[Models]
           ([ID_model]
           ,[Title_model])
     VALUES
           (@id, @m)
END
GO

CREATE PROCEDURE dbo.EditModel
@id INT,
@m CHAR(30)
AS
BEGIN
UPDATE [dbo].[Models]
   SET [ID_model] = @id,
       [Title_model] = @m
 WHERE ID_model=@id
END
GO



CREATE PROCEDURE dbo.SelectManufacturers
AS
BEGIN
SELECT Manufacturers.ID_manufacturer, Manufacturers.Manufacturer, Country.Country
FROM Manufacturers INNER JOIN Country
ON Manufacturers.ID_country=Country.ID_country
END
GO
EXECUTE dbo.SelectManufacturers

CREATE PROCEDURE dbo.InsertManufacturer
@id INT,
@m CHAR(30),
@c INT
AS
BEGIN
INSERT INTO [dbo].[Manufacturers]
           ([ID_manufacturer]
           ,[Manufacturer]
           ,[ID_country])
     VALUES
           (@id,
            @m,
            @c)
END
GO



CREATE PROCEDURE dbo.SelectBrands_and_models
AS
BEGIN
SELECT Brands_and_models.ID_brd_mdl, Brands.Title_brand, Models.Title_model
FROM Brands_and_models INNER JOIN Brands
ON Brands_and_models.ID_brand=Brands.ID_brand
INNER JOIN Models ON Brands_and_models.ID_model=Models.ID_model
END
GO

CREATE PROCEDURE dbo.InsertBrand_and_model
@id INT,
@b INT,
@m INT
AS
BEGIN
INSERT INTO [dbo].[Brands_and_models]
           ([ID_brd_mdl]
           ,[ID_brand]
           ,[ID_model])
     VALUES
           (@id,
            @b,
            @m)
END
GO


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


CREATE PROCEDURE dbo.EditSupplier
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
UPDATE [dbo].[Suppliers]
   SET [ID_supplier] =@id,
       [Title] =@title,
       [TIN] = @tin,
       [CIO] = @cio,
       [FIO_director] = @fio,
       [ID_country] = @country,
       [ID_city] = @city,
       [ID_street] = @street,
       [House] = @house,
       [Telephone] = @telephon,
       [Email] = @email
 WHERE ID_supplier=@id
END
GO




CREATE PROCEDURE dbo.DeleteSupplier
@id INT
AS
BEGIN
DELETE FROM Suppliers WHERE ID_supplier=@id
END
GO


CREATE PROCEDURE dbo.SelectAutoparts
AS
BEGIN
SELECT Autoparts.ID_autoparts, Autoparts.Article, Autoparts.Title, Autoparts.ID_brd_mdl, Brands.Title_brand, Models.Title_model, Manufacturers.Manufacturer, Country.Country, Autoparts.Comment
FROM Autoparts INNER JOIN  Brands_and_models 
ON Autoparts.ID_brd_mdl=Brands_and_models.ID_brd_mdl
INNER JOIN Brands ON Brands.ID_brand=Brands_and_models.ID_brand
INNER JOIN Models ON Models.ID_model=Brands_and_models.ID_model
INNER JOIN Manufacturers ON Manufacturers.ID_manufacturer=Autoparts.ID_manufacturer
INNER JOIN Country ON Manufacturers.ID_country=Country.ID_country
END
GO

CREATE PROCEDURE dbo.SelectAnotherModels
@b CHAR(30)
AS
BEGIN
SELECT Brands_and_models.ID_brd_mdl, Brands_and_models.ID_brand, Brands_and_models.ID_model, Models.Title_model
FROM Brands_and_models INNER JOIN Models
ON Brands_and_models.ID_model=Models.ID_model
INNER JOIN Brands ON Brands_and_models.ID_brand=Brands.ID_brand AND  Brands.Title_brand=@b
END
GO


CREATE PROCEDURE dbo.InsertAutopart
@id INT,
@a CHAR(50),
@t CHAR(50),
@bm INT,
@m INT,
@c CHAR(150)
AS
BEGIN
INSERT INTO [dbo].[Autoparts]
           ([ID_autoparts]
           ,[Article]
           ,[Title]
           ,[ID_brd_mdl]
           ,[ID_manufacturer]
           ,[Comment])
     VALUES
           (@id,
            @a,
            @t,
            @bm,
            @m,
            @c)
END
GO

CREATE PROCEDURE dbo.EditAutopart
@id INT,
@a CHAR(50),
@t CHAR(50),
@bm INT,
@m INT,
@c CHAR(150)
AS
BEGIN
UPDATE [dbo].[Autoparts]
   SET [ID_autoparts] = @id,
       [Article] = @a,
       [Title] = @t,
       [ID_brd_mdl] = @bm,
       [ID_manufacturer] = @m,
       [Comment] = @c
 WHERE ID_autoparts=@id
END
GO



CREATE PROCEDURE dbo.SelectSuppply
AS
BEGIN
SELECT Supply.ID_supply, Suppliers.Title, Autoparts.Title, Supply.Purchase_price, Supply.Amount , Purchase_price*Amount AS Sum_, Supply.Delivery_date
FROM Supply INNER JOIN Suppliers
ON Supply.ID_supplier=Suppliers.ID_supplier
INNER JOIN Autoparts
ON Supply.ID_autoparts=Autoparts.ID_autoparts
END
GO

CREATE PROCEDURE dbo.SelectDepartmentCity
AS
BEGIN
SELECT Department_store.ID_department, City.City
FROM Department_store INNER JOIN City
ON Department_store.ID_city=City.ID_city
END
GO


CREATE PROCEDURE dbo.SelectDepartmentStreet
@city CHAR(30)
AS
BEGIN
SELECT Department_store.ID_department, Street.Street
FROM Department_store INNER JOIN Street
ON Department_store.ID_street=Street.ID_street
INNER JOIN City ON Department_store.ID_city=City.ID_city
AND City.City=@city
END
GO


CREATE PROCEDURE dbo.SelectDepartmentHouse
@street CHAR(30)
AS
BEGIN
SELECT Department_store.ID_department, Department_store.House
FROM Department_store INNER JOIN Street
ON Department_store.ID_street=Street.ID_street
AND Street.Street=@street
END
GO


CREATE PROCEDURE dbo.InsertSupply
@id INT,
@id_s INT,
@id_a INT,
@p NUMERIC(8,2),
@a INT,
@d DATE
AS
BEGIN
INSERT INTO [dbo].[Supply]
           ([ID_supply]
           ,[ID_supplier]
           ,[ID_autoparts]
           ,[Purchase_price]
           ,[Amount]
           ,[Delivery_date])
     VALUES
           (@id,
            @id_s,
            @id_a,
            @p,
            @a,
            @d)
END
GO

CREATE PROCEDURE dbo.CheckAutoparts
@id_d INT,
@id_a INT
AS
BEGIN
SELECT COUNT(*)
FROM Availability_auto_parts
WHERE Availability_auto_parts.ID_department=@id_d
AND Availability_auto_parts.ID_autoparts=@id_a
END
GO




CREATE PROCEDURE dbo.InsertAvailability_auto_parts
@id INT,
@id_d INT,
@id_a INT,
@p NUMERIC(8,2),
@a INT
AS
BEGIN
INSERT INTO [dbo].[Availability_auto_parts]
           ([ID_availability]
           ,[ID_department]
           ,[ID_autoparts]
           ,[Price_holiday]
           ,[Amount])
     VALUES
           (@id,
            @id_d,
            @id_a,
            @p,
            @a)
END
GO


CREATE PROCEDURE dbo.EditAvailability_auto_parts
@id INT,
@id_d INT,
@id_a INT,
@p NUMERIC(8,2),
@a INT
AS
BEGIN
UPDATE [dbo].[Availability_auto_parts]
   SET [ID_availability] = @id,
       [ID_department] = @id_d,
       [ID_autoparts] = @id_a,
       [Price_holiday] = @p,
       [Amount] = @a
 WHERE ID_availability=@id
END
GO