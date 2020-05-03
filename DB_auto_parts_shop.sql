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
Street CHAR(50))

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
ID_brd_mdl INT FOREIGN KEY REFERENCES Brands_and_models(ID_brd_mdl),
ID_manufacturer INT FOREIGN KEY REFERENCES Manufacturers(ID_manufacturer),
Comment CHAR(150))

CREATE TABLE Supply (
ID_supply INT PRIMARY KEY,
ID_supplier INT FOREIGN KEY REFERENCES Suppliers(ID_supplier),
ID_autoparts INT FOREIGN KEY REFERENCES Autoparts(ID_autoparts),
Purchase_price NUMERIC(8,2),
Amount INT,
Delivery_date DATE,
Status_ CHAR(20))

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
Sale_price NUMERIC(8,2),
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

CREATE TABLE UserSession (
ID_session INT PRIMARY KEY,
Login_ CHAR(15) FOREIGN KEY REFERENCES Users(Login_),
Date_of_entrance DATETIME)



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
@s CHAR(50)
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
@s CHAR(50)
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

CREATE PROCEDURE dbo.EditManufacturer
@id INT,
@m CHAR(30),
@c INT
AS
BEGIN
UPDATE [dbo].[Manufacturers]
   SET [ID_manufacturer] = @id,
       [Manufacturer] = @m,
       [ID_country] = @c
 WHERE ID_manufacturer=@id
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


CREATE PROCEDURE dbo.EditBrand_and_model
@id INT,
@b INT,
@m INT
AS
BEGIN
UPDATE [dbo].[Brands_and_models]
   SET [ID_brd_mdl] = @id,
       [ID_brand] = @b,
       [ID_model] = @m
 WHERE ID_brd_mdl=@id
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
dbo.SelectAnotherModels 'BMW'


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
SELECT Supply.ID_supply, Suppliers.Title, Autoparts.ID_autoparts, Autoparts.Title, Manufacturers.Manufacturer, Autoparts.Article, Supply.Purchase_price, Supply.Amount , Purchase_price*Amount AS Sum_, Supply.Delivery_date, Supply.Status_
FROM Supply INNER JOIN Suppliers
ON Supply.ID_supplier=Suppliers.ID_supplier
INNER JOIN Autoparts
ON Supply.ID_autoparts=Autoparts.ID_autoparts
INNER JOIN Manufacturers
ON Autoparts.ID_manufacturer=Manufacturers.ID_manufacturer
END
GO
EXECUTE dbo.SelectSuppply

CREATE PROCEDURE dbo.SelectManufacturersAutoparts
@a CHAR(50)
AS
BEGIN
SELECT Autoparts.ID_autoparts, Manufacturers.Manufacturer
FROM Autoparts INNER JOIN Manufacturers
ON Autoparts.ID_manufacturer=Manufacturers.ID_manufacturer
AND Autoparts.Title=@a
END
GO

CREATE PROCEDURE dbo.SelectArticleAutoparts
@a CHAR(50),
@m CHAR(30)
AS
BEGIN
SELECT Autoparts.ID_autoparts, Autoparts.Article
FROM Autoparts INNER JOIN Manufacturers
ON Autoparts.ID_manufacturer=Manufacturers.ID_manufacturer
AND Autoparts.Title=@a AND Manufacturers.Manufacturer=@m
END
GO


--CREATE PROCEDURE dbo.SelectDepartmentCity
--AS
--BEGIN
--SELECT Department_store.ID_department, City.City
--FROM Department_store INNER JOIN City
--ON Department_store.ID_city=City.ID_city
--END
--GO


--CREATE PROCEDURE dbo.SelectDepartmentStreet
--@city CHAR(30)
--AS
--BEGIN
--SELECT Department_store.ID_department, Street.Street
--FROM Department_store INNER JOIN Street
--ON Department_store.ID_street=Street.ID_street
--INNER JOIN City ON Department_store.ID_city=City.ID_city
--AND City.City=@city
--END
--GO


--CREATE PROCEDURE dbo.SelectDepartmentHouse
--@street CHAR(30)
--AS
--BEGIN
--SELECT Department_store.ID_department, Department_store.House
--FROM Department_store INNER JOIN Street
--ON Department_store.ID_street=Street.ID_street
--AND Street.Street=@street
--END
--GO


CREATE PROCEDURE dbo.InsertSupply
@id INT,
@id_s INT,
@id_a INT,
@p NUMERIC(8,2),
@a INT,
@d DATE,
@s CHAR(20)
AS
BEGIN
INSERT INTO [dbo].[Supply]
           ([ID_supply]
           ,[ID_supplier]
           ,[ID_autoparts]
           ,[Purchase_price]
           ,[Amount]
           ,[Delivery_date]
		   ,[Status_])
     VALUES
           (@id,
            @id_s,
            @id_a,
            @p,
            @a,
            @d,
			@s)
END
GO


CREATE PROCEDURE dbo.EditSupply
@id INT,
@id_s INT,
@id_a INT,
@p NUMERIC(8,2),
@a INT,
@d DATE
AS
BEGIN
UPDATE [dbo].[Supply]
   SET [ID_supply] = @id,
       [ID_supplier] = @id_s,
       [ID_autoparts] = @id_a ,
       [Purchase_price] = @p,
       [Amount] = @a,
       [Delivery_date] = @d
WHERE ID_supply=@id
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


CREATE PROCEDURE dbo.StatusChange
@id INT,
@s CHAR(20)
AS
BEGIN
UPDATE [dbo].[Supply]
   SET [ID_supply] = @id,
       [Status_] = @s
 WHERE ID_supply=@id
END
GO


CREATE PROCEDURE dbo.SelectAvailability_auto_parts
AS
BEGIN
SELECT ID_availability, ID_department, Autoparts.Title, Manufacturers.Manufacturer, Autoparts.Article, Sale_price, Amount, Sale_price*Amount AS Sum_
FROM Availability_auto_parts INNER JOIN Autoparts
ON Availability_auto_parts.ID_autoparts=Autoparts.ID_autoparts
INNER JOIN Manufacturers
ON Autoparts.ID_manufacturer=Manufacturers.ID_manufacturer
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
           ,[Sale_price]
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
       [Sale_price] = @p,
       [Amount] = @a
 WHERE ID_availability=@id
END
GO


CREATE PROCEDURE dbo.SelectDepartment_store
AS
BEGIN
SELECT Department_store.ID_department, City.City, Street.Street, House, Telephone
FROM Department_store INNER JOIN City
ON Department_store.ID_city=City.ID_city
INNER JOIN Street
ON Department_store.ID_street=Street.ID_street
END
GO


CREATE PROCEDURE dbo.InsertDepartment_store
@id INT,
@city INT,
@street INT,
@h CHAR(15),
@t CHAR(20)
AS
BEGIN
INSERT INTO [dbo].[Department_store]
           ([ID_department]
           ,[ID_city]
           ,[ID_street]
           ,[House]
           ,[Telephone])
     VALUES
           (@id,
            @city,
            @street,
            @h,
            @t)
END
GO

CREATE PROCEDURE dbo.EditDepartment_store
@id INT,
@city INT,
@street INT,
@h CHAR(15),
@t CHAR(20)
AS
BEGIN
UPDATE [dbo].[Department_store]
   SET [ID_department] = @id,
       [ID_city] = @city,
       [ID_street] = @street,
       [House] = @h,
       [Telephone] = @t
 WHERE ID_department=@id
END
GO
 

CREATE PROCEDURE dbo.SelectSale
AS
BEGIN
SELECT ID_sale, Sale.ID_department, Sale.ID_autoparts, Autoparts.Title, Manufacturer, Autoparts.Article, Availability_auto_parts.Sale_price, Sale.Amount, Availability_auto_parts.Sale_price*Sale.Amount AS Sum_, Sale.Date_of_sale
FROM Sale INNER JOIN Autoparts
ON Sale.ID_autoparts=Autoparts.ID_autoparts
INNER JOIN Manufacturers
ON Autoparts.ID_manufacturer=Manufacturers.ID_manufacturer
INNER JOIN Availability_auto_parts
ON Availability_auto_parts.ID_autoparts=Sale.ID_autoparts AND Sale.ID_department=Availability_auto_parts.ID_department
END
GO


CREATE PROCEDURE dbo.SelectComboBoxAutoparts
@id_d INT
AS
BEGIN
SELECT DISTINCT Autoparts.Title
FROM Autoparts INNER JOIN Availability_auto_parts
ON Autoparts.ID_autoparts=Availability_auto_parts.ID_autoparts
AND Availability_auto_parts.ID_department=@id_d
END
GO

 
CREATE PROCEDURE dbo.InsertSale
@id INT,
@id_d INT,
@id_a INT,
@a INT,
@d DATE 
AS
BEGIN
INSERT INTO [dbo].[Sale]
           ([ID_sale]
           ,[ID_department]
           ,[ID_autoparts]
           ,[Amount]
           ,[Date_of_sale])
     VALUES
           (@id,
            @id_d,
            @id_a,
            @a,
            @d)
END
GO

CREATE PROCEDURE dbo.EditSale
@id INT,
@id_d INT,
@id_a INT,
@a INT,
@d DATE 
AS
BEGIN
UPDATE [dbo].[Sale]
   SET [ID_sale] = @id,
       [ID_department] = @id_d,
       [ID_autoparts] = @id_a, 
       [Amount] = @a,
       [Date_of_sale] = @d
 WHERE ID_sale=@id
END
GO


CREATE PROCEDURE dbo.SearchDateSale
@startDate DATE,
@endDate DATE
AS
BEGIN
SELECT ID_sale, Sale.ID_department, Sale.ID_autoparts, Autoparts.Title, Manufacturer, Autoparts.Article, Availability_auto_parts.Sale_price, Sale.Amount, Availability_auto_parts.Sale_price*Sale.Amount AS Sum_, Sale.Date_of_sale
FROM Sale INNER JOIN Autoparts
ON Sale.ID_autoparts=Autoparts.ID_autoparts
INNER JOIN Manufacturers
ON Autoparts.ID_manufacturer=Manufacturers.ID_manufacturer
INNER JOIN Availability_auto_parts
ON Availability_auto_parts.ID_autoparts=Sale.ID_autoparts AND Sale.ID_department=Availability_auto_parts.ID_department
WHERE Date_of_sale BETWEEN @startDate AND @endDate 
END
GO


CREATE PROCEDURE dbo.SearchByPriceRange
@startPrice NUMERIC(8,2),
@endPrice NUMERIC(8,2)
AS
BEGIN
SELECT ID_availability, ID_department, Autoparts.Title, Manufacturers.Manufacturer, Autoparts.Article, Sale_price, Amount, Sale_price*Amount AS Sum_
FROM Availability_auto_parts INNER JOIN Autoparts
ON Availability_auto_parts.ID_autoparts=Autoparts.ID_autoparts
INNER JOIN Manufacturers
ON Autoparts.ID_manufacturer=Manufacturers.ID_manufacturer
WHERE Sale_price BETWEEN @startPrice AND @endPrice
END
GO
EXECUTE dbo.SearchByPriceRange  434, 999999.00


--����� �������
CREATE PROCEDURE dbo.DeliveryReport
AS
BEGIN
SELECT Suppliers.Title AS '���������', Autoparts.Title AS '������������', Manufacturers.Manufacturer AS '�������������', Autoparts.Article AS '�������', Supply.Purchase_price AS '����', Supply.Amount AS '����������' , Purchase_price*Amount AS '�����', Supply.Delivery_date AS '����'
FROM Supply INNER JOIN Suppliers
ON Supply.ID_supplier=Suppliers.ID_supplier
INNER JOIN Autoparts
ON Supply.ID_autoparts=Autoparts.ID_autoparts
INNER JOIN Manufacturers
ON Autoparts.ID_manufacturer=Manufacturers.ID_manufacturer
END


CREATE PROCEDURE dbo.SalesReport
AS
BEGIN
SELECT Sale.ID_department AS '�������',  Autoparts.Title AS '������������', Manufacturer AS '�������������', Autoparts.Article AS '�������', Availability_auto_parts.Sale_price AS '����', Sale.Amount AS '����������', Availability_auto_parts.Sale_price*Sale.Amount AS '�����', Sale.Date_of_sale AS '����'
FROM Sale INNER JOIN Autoparts
ON Sale.ID_autoparts=Autoparts.ID_autoparts
INNER JOIN Manufacturers
ON Autoparts.ID_manufacturer=Manufacturers.ID_manufacturer
INNER JOIN Availability_auto_parts
ON Availability_auto_parts.ID_autoparts=Sale.ID_autoparts AND Sale.ID_department=Availability_auto_parts.ID_department
END


---------------------���������� ��������--------------------------------------

--����� �� ����������
SELECT Suppliers.Title AS '���������', Autoparts.Title AS '������������', Manufacturers.Manufacturer AS '�������������', Autoparts.Article AS '�������', Supply.Purchase_price AS '����', Supply.Amount AS '����������' , Purchase_price*Amount AS '�����', Supply.Delivery_date AS '����'
FROM Supply INNER JOIN Suppliers
ON Supply.ID_supplier=Suppliers.ID_supplier
INNER JOIN Autoparts
ON Supply.ID_autoparts=Autoparts.ID_autoparts
INNER JOIN Manufacturers
ON Autoparts.ID_manufacturer=Manufacturers.ID_manufacturer AND Suppliers.Title=@s

--����� �� ����
SELECT Suppliers.Title AS '���������', Autoparts.Title AS '������������', Manufacturers.Manufacturer AS '�������������', Autoparts.Article AS '�������', Supply.Purchase_price AS '����', Supply.Amount AS '����������' , Purchase_price*Amount AS '�����', Supply.Delivery_date AS '����'
FROM Supply INNER JOIN Suppliers
ON Supply.ID_supplier=Suppliers.ID_supplier
INNER JOIN Autoparts
ON Supply.ID_autoparts=Autoparts.ID_autoparts
INNER JOIN Manufacturers
ON Autoparts.ID_manufacturer=Manufacturers.ID_manufacturer
WHERE Supply.Delivery_date BETWEEN @startDate AND @endDate


---------------------���������� ������--------------------------------------

--�� ��������-----
SELECT Sale.ID_department AS '�������',  Autoparts.Title AS '������������', Manufacturer AS '�������������', Autoparts.Article AS '�������', Availability_auto_parts.Sale_price AS '����', Sale.Amount AS '����������', Availability_auto_parts.Sale_price*Sale.Amount AS '�����', Sale.Date_of_sale AS '����'
FROM Sale INNER JOIN Autoparts
ON Sale.ID_autoparts=Autoparts.ID_autoparts
INNER JOIN Manufacturers
ON Autoparts.ID_manufacturer=Manufacturers.ID_manufacturer
INNER JOIN Availability_auto_parts
ON Availability_auto_parts.ID_autoparts=Sale.ID_autoparts AND Sale.ID_department=Availability_auto_parts.ID_department
WHERE Sale.ID_department=@store

--�� ����-------
SELECT Sale.ID_department AS '�������',  Autoparts.Title AS '������������', Manufacturer AS '�������������', Autoparts.Article AS '�������', Availability_auto_parts.Sale_price AS '����', Sale.Amount AS '����������', Availability_auto_parts.Sale_price*Sale.Amount AS '�����', Sale.Date_of_sale AS '����'
FROM Sale INNER JOIN Autoparts
ON Sale.ID_autoparts=Autoparts.ID_autoparts
INNER JOIN Manufacturers
ON Autoparts.ID_manufacturer=Manufacturers.ID_manufacturer
INNER JOIN Availability_auto_parts
ON Availability_auto_parts.ID_autoparts=Sale.ID_autoparts AND Sale.ID_department=Availability_auto_parts.ID_department
WHERE Sale.Date_of_sale BETWEEN @startDate AND @endDate



--������ ��������������

CREATE PROCEDURE dbo.SelectUserAuthentication
@l CHAR(15)
AS
BEGIN
SELECT Users.Login_, InfoUsers.Surname, InfoUsers.First_name, InfoUsers.Last_name, Role_.Role_
FROM Users INNER JOIN InfoUsers ON Users.Login_=InfoUsers.Login_
AND Users.Login_=@l
INNER JOIN Role_
ON Users.ID_role=Role_.ID_role
END
GO


CREATE PROCEDURE dbo.InsertRole
@id INT,
@r CHAR(20)
AS
BEGIN
INSERT INTO [dbo].[Role_]
           ([ID_role]
           ,[Role_])
     VALUES
           (@id, @r )
END
GO

EXECUTE dbo.InsertRole 1, '�������������'
EXECUTE dbo.InsertRole 2, '��������'
EXECUTE dbo.InsertRole 3, '��������'
EXECUTE dbo.InsertRole 4, '��������'
EXECUTE dbo.InsertRole 5, '���������'


CREATE PROCEDURE dbo.InsertPosition
@id INT,
@p CHAR (30)
AS
BEGIN
INSERT INTO [dbo].[Position]
           ([ID_position]
           ,[Position])
     VALUES
           (@id, @p)
END
GO

EXECUTE dbo.InsertPosition 1, '�������������'
EXECUTE dbo.InsertPosition 2, '��������'
EXECUTE dbo.InsertPosition 3, '��������'
EXECUTE dbo.InsertPosition 4, '��������'
EXECUTE dbo.InsertPosition 5, '���������'


CREATE PROCEDURE dbo.SelectUsers
AS
BEGIN
SELECT Users.Login_, Surname, First_name, Last_name, Position
FROM Users INNER JOIN InfoUsers
ON Users.Login_=InfoUsers.Login_
INNER JOIN Position
ON InfoUsers.ID_position=Position.ID_position
END
GO


CREATE PROCEDURE dbo.SelectInfoUsers
@l CHAR(15)
AS
BEGIN
SELECT Users.Login_, Users.Password_, Role_.Role_, Surname, First_name, Last_name, Position, Address_, Telephone, Date_of_acceptance, Date_of_termination
FROM Users INNER JOIN InfoUsers
ON Users.Login_=InfoUsers.Login_
INNER JOIN Role_
ON Role_.ID_role=Users.ID_role
INNER JOIN Position
ON InfoUsers.ID_position=Position.ID_position
WHERE InfoUsers.Login_=@l
END
GO


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
@da DATE
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
           ,[Date_of_acceptance])
     VALUES
           (@login,
            @surname,
            @f_name,
            @l_name,
            @pos,
            @adress,
            @tel,
            @da)
END
GO
EXECUTE dbo.InsertUsers '111', '111', 2, 'xccsd', 'dss', 'dsss', 1, 'dsdsds', '343433', '31.12.1999', '01.04.2020'


CREATE PROCEDURE dbo.EditInfoUsers
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
UPDATE [dbo].[Users]
   SET [Login_] = @login,
       [Password_] = @pass,
       [ID_role] = @role
 WHERE Login_=@login
UPDATE [dbo].[InfoUsers]
   SET [Login_] = @login,
       [Surname] =@surname,
       [First_name] = @f_name,
       [Last_name] = @l_name,
       [ID_position] = @pos,
       [Address_] = @adress,
       [Telephone] = @tel,
       [Date_of_acceptance] = @da,
       [Date_of_termination] = @dt
 WHERE Login_=@login
END
GO
EXECUTE dbo.EditInfoUsers '111', '111', 2, 'xccsd', 'dss', 'dsss', 1, 'dsdsds', '343433', '31.12.1999', NULL


CREATE PROCEDURE dbo.SelectUserSession
AS
BEGIN
SELECT ID_session, Users.Login_, Surname, First_name, Last_name, Role_.Role_, UserSession.Date_of_entrance
FROM UserSession INNER JOIN Users
ON UserSession.Login_=Users.Login_
INNER JOIN Role_
ON Users.ID_role=Role_.ID_role
INNER JOIN InfoUsers
ON Users.Login_=InfoUsers.Login_
END
GO


CREATE PROCEDURE dbo.SelectUserSessionDate
@startDate DATE,
@endDate DATE
AS
BEGIN
SELECT ID_session, Users.Login_, Surname, First_name, Last_name, Role_.Role_, UserSession.Date_of_entrance
FROM UserSession INNER JOIN Users
ON UserSession.Login_=Users.Login_
INNER JOIN Role_
ON Users.ID_role=Role_.ID_role
INNER JOIN InfoUsers
ON Users.Login_=InfoUsers.Login_
WHERE Date_of_entrance BETWEEN @startDate AND @endDate
END
GO


CREATE PROCEDURE dbo.InsertUserSession
@id INT,
@id_user CHAR(15),
@dt DATETIME
AS
BEGIN
INSERT INTO [dbo].[UserSession]
           ([ID_session]
           ,[Login_]
           ,[Date_of_entrance])
     VALUES
           (@id, @id_user,@dt)
END
GO


--���������� ������ �������


--������
EXECUTE dbo.InsertCity 1, '������'
EXECUTE dbo.InsertCity 2, '�����-���������'
EXECUTE dbo.InsertCity 3, '������'
EXECUTE dbo.InsertCity 4, '�����'
EXECUTE dbo.InsertCity 5, '�����������'
EXECUTE dbo.InsertCity 6, '���������'
EXECUTE dbo.InsertCity 7, '�������'
EXECUTE dbo.InsertCity 9, '��������'
EXECUTE dbo.InsertCity 10, '������������'
EXECUTE dbo.InsertCity 11, '������'
EXECUTE dbo.InsertCity 12, '������� ��������'
EXECUTE dbo.InsertCity 13, '������� �����'
EXECUTE dbo.InsertCity 14, '��������'
EXECUTE dbo.InsertCity 15, '���������'
EXECUTE dbo.InsertCity 16, '�������'
EXECUTE dbo.InsertCity 17, '���������'
EXECUTE dbo.InsertCity 18, '������������'
EXECUTE dbo.InsertCity 19, '����'
EXECUTE dbo.InsertCity 20, '����������'
EXECUTE dbo.InsertCity 21, '�������'
EXECUTE dbo.InsertCity 22, '������'
EXECUTE dbo.InsertCity 23, '�������'
EXECUTE dbo.InsertCity 24, '������-���'
EXECUTE dbo.InsertCity 25, '������'
EXECUTE dbo.InsertCity 24, '������-���'
EXECUTE dbo.InsertCity 25, '�����������'
EXECUTE dbo.InsertCity 26, '������'
EXECUTE dbo.InsertCity 27, '��������'
EXECUTE dbo.InsertCity 28, '�����'
EXECUTE dbo.InsertCity 29, '���������'
EXECUTE dbo.InsertCity 30, '�����'
EXECUTE dbo.InsertCity 31, '������'
EXECUTE dbo.InsertCity 32, '���������'
EXECUTE dbo.InsertCity 33, '��������'
EXECUTE dbo.InsertCity 34, '������ ��������'
EXECUTE dbo.InsertCity 35, '����'
EXECUTE dbo.InsertCity 35, '�������'
EXECUTE dbo.InsertCity 36, '���������'


--�����
EXECUTE dbo.InsertStreet 1, '��. ���������'
EXECUTE dbo.InsertStreet 2, '��. �����������'
EXECUTE dbo.InsertStreet 3, '�������� �������� �����'
EXECUTE dbo.InsertStreet 4, '��. ��������'
EXECUTE dbo.InsertStreet 5, '��. ��������������'
EXECUTE dbo.InsertStreet 6, '��. ������� ��������'
EXECUTE dbo.InsertStreet 7, '�������� ������� ������������'
EXECUTE dbo.InsertStreet 8, '��. ������������'
EXECUTE dbo.InsertStreet 9, '��. �����������'
EXECUTE dbo.InsertStreet 10, '���������� ��������'
EXECUTE dbo.InsertStreet 11, '���. ���������'
EXECUTE dbo.InsertStreet 12, '��-� ������ ���������'
EXECUTE dbo.InsertStreet 13, '��. �������������'
EXECUTE dbo.InsertStreet 14, '��. ���������'
EXECUTE dbo.InsertStreet 15, '��. ��������'
EXECUTE dbo.InsertStreet 16, '��. ��������'
EXECUTE dbo.InsertStreet 17, '��. �������������'
EXECUTE dbo.InsertStreet 18, '��. �������'
EXECUTE dbo.InsertStreet 19, '����� ������ �����������'
EXECUTE dbo.InsertStreet 20, '��������� ��������'
EXECUTE dbo.InsertStreet 21, '��. ����������'
EXECUTE dbo.InsertStreet 22, '��. �������'
EXECUTE dbo.InsertStreet 23, '��. �����������'
EXECUTE dbo.InsertStreet 24, '��. ������������'
EXECUTE dbo.InsertStreet 25, '��. ������'
EXECUTE dbo.InsertStreet 26, '��. ��������'
EXECUTE dbo.InsertStreet 27, '��. ���������'
EXECUTE dbo.InsertStreet 28, '��. �����'
EXECUTE dbo.InsertStreet 29, '��.����������'
EXECUTE dbo.InsertStreet 30, '��.��������'
EXECUTE dbo.InsertStreet 31, '��. ���������'
EXECUTE dbo.InsertStreet 32, '��. ������'
EXECUTE dbo.InsertStreet 33, '��. �����������'
EXECUTE dbo.InsertStreet 34, '��. ������������'
EXECUTE dbo.InsertStreet 35, '�������� ������'
EXECUTE dbo.InsertStreet 36, '��. ���������'
EXECUTE dbo.InsertStreet 37, '��. ����������'
EXECUTE dbo.InsertStreet 38, '��.��������'
EXECUTE dbo.InsertStreet 39, '��. ������� ������'
EXECUTE dbo.InsertStreet 40, '��. ���������������'
EXECUTE dbo.InsertStreet 41, '��. ����������� ��������'
EXECUTE dbo.InsertStreet 42, '��. ����� ���������'
EXECUTE dbo.InsertStreet 43, '��. ��������������'
EXECUTE dbo.InsertStreet 44, '��. ��������������'
EXECUTE dbo.InsertStreet 45, '��. ��������'
EXECUTE dbo.InsertStreet 46, '�������� ����'
EXECUTE dbo.InsertStreet 47, '��. ��������'
EXECUTE dbo.InsertStreet 48, '��. ��������'
EXECUTE dbo.InsertStreet 49, '�������� ����� I'
EXECUTE dbo.InsertStreet 50, '��. ���������'
EXECUTE dbo.InsertStreet 51, '������������� �����, ����� �����'
EXECUTE dbo.InsertStreet 52, '��. ���������'
EXECUTE dbo.InsertStreet 53, '��. ��������'
EXECUTE dbo.InsertStreet 54, '��. ��������'
EXECUTE dbo.InsertStreet 55, '�������� ��������'
EXECUTE dbo.InsertStreet 56, '��.10 ��� �������'
EXECUTE dbo.InsertStreet 57, '��. ����������'
EXECUTE dbo.InsertStreet 58, '��. �������'
EXECUTE dbo.InsertStreet 59, '��. ��������'
EXECUTE dbo.InsertStreet 60, '��. ��������'
EXECUTE dbo.InsertStreet 61, '��. ��������'


--//������ ��������
EXECUTE dbo.InsertDepartment_store 1, 1, 1, '�.48', '9613453349'
EXECUTE dbo.InsertDepartment_store 2, 2, 2, '�.11', '9613663349'
EXECUTE dbo.InsertDepartment_store 3, 3, 3, '�.9, �.1, ���.4', '9653663347'
EXECUTE dbo.InsertDepartment_store 4, 3, 4, '�.2', '9653223350'
EXECUTE dbo.InsertDepartment_store 5, 4, 5, '�. 4�', '9953227755'
EXECUTE dbo.InsertDepartment_store 6, 4, 6, '�.16', '9953227755'
EXECUTE dbo.InsertDepartment_store 7, 7, 61, '�.122', '9888223755'
EXECUTE dbo.InsertDepartment_store 8, 9, 7, '�.92', '9889223743'
EXECUTE dbo.InsertDepartment_store 9, 9, 8, '�.5�', '9789233743'
EXECUTE dbo.InsertDepartment_store 10, 10, 9, '�.111', '9789111743'
EXECUTE dbo.InsertDepartment_store 11, 11, 10, '�.1', '9739121747'
EXECUTE dbo.InsertDepartment_store 11, 11, 11, '�.57�', '9229121749'
EXECUTE dbo.InsertDepartment_store 12, 11, 12, '�.42', '9239126647'
EXECUTE dbo.InsertDepartment_store 13, 11, 13, '�.64', '9889186655'
EXECUTE dbo.InsertDepartment_store 14, 12, 14, '�.25', '9889186655'
EXECUTE dbo.InsertDepartment_store 15, 13, 15, '�.35', '9855187659'
EXECUTE dbo.InsertDepartment_store 16, 14, 16, '�.27', '9855182649'
EXECUTE dbo.InsertDepartment_store 17, 15, 17, '�.141', '9855188649'
EXECUTE dbo.InsertDepartment_store 18, 15, 18, '�.29', '9755338649'
EXECUTE dbo.InsertDepartment_store 19, 16, 19, '�.4', '9335368649'
EXECUTE dbo.InsertDepartment_store 20, 16, 20, '�. 172', '9335567649'
EXECUTE dbo.InsertDepartment_store 21, 16, 21, '�. 49', '9335567649'
EXECUTE dbo.InsertDepartment_store 22, 17, 22, '�.1, ����.2', '9335567649'
EXECUTE dbo.InsertDepartment_store 23, 18, 23, '�.49', '9784567649'
EXECUTE dbo.InsertDepartment_store 24, 18, 24, '�.77', '9784555649'
EXECUTE dbo.InsertDepartment_store 25, 18, 25, '�.26', '9784555649'
EXECUTE dbo.InsertDepartment_store 26, 18, 26, '�.60', '9714552249'
EXECUTE dbo.InsertDepartment_store 27, 18, 27, '�.3�', '9714442849'
EXECUTE dbo.InsertDepartment_store 28, 18, 28, '�.58', '9711142849'
EXECUTE dbo.InsertDepartment_store 29, 19, 29, '�.111', '9767111849'
EXECUTE dbo.InsertDepartment_store 30, 20, 30, '�.10', '9762311849'