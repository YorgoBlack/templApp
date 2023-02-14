use master

IF DB_ID('templ') IS NOT NULL
  drop database templ
GO

create database templ
GO

use templ

CREATE TABLE Customer
(
	CustomerId UNIQUEIDENTIFIER PRIMARY KEY default NEWID(),
	[Name] VARCHAR (50) NOT NULL,
	CompanyName VARCHAR (100) NOT NULL,
	CompanyAddress VARCHAR (100) NOT NULL,
	Phone VARCHAR (20) NOT NULL,
	Email VARCHAR (50) NOT NULL,
)
GO

create unique index customerName on dbo.Customer ([Name])
create index customerCompanyName on dbo.Customer (CompanyName)
create index customerPhone on dbo.Customer (Phone)
create index customerEmail on dbo.Customer (Email)
GO

Create Procedure GetCustomers
(
 @top int = 1,  
 @limit int = 10,
 @orderByColumn int = 1,
 @orderByDesc int = 0, 
 @filterByName varchar(50) = '_',
 @filterByCompanyName varchar(50) = '_',
 @filterByPhone varchar(50) = '_',
 @filterByEmail varchar(50) = '_'
)  
As  
Begin  
SET NOCOUNT ON;

declare @Sort int
set @Sort = @orderByColumn*10 + @orderByDesc
set @top = @top - 1
if @top < 0 
	set @top = 0
if @limit <= 0 
	set @limit = 10


select * from Customer

where 

	[Name] like case when @filterByName = '_' then  [Name]  else '%'+@filterByName+'%' end And

	CompanyName like case when @filterByCompanyName = '_' then  CompanyName  else '%'+@filterByCompanyName+'%' end And

	Phone like case when @filterByPhone = '_' then Phone else '%'+@filterByPhone+'%' end And

	Email like case when @filterByEmail = '_' then Email else '%'+@filterByEmail+'%' end

Order by 
	case @Sort when 10 then [Name] end asc, 
	case @Sort when 11 then [Name] end desc,
	case @Sort when 20 then [CompanyName] end asc, 
	case @Sort when 21 then [CompanyName] end desc,
	case @Sort when 30 then [Phone] end asc, 
	case @Sort when 31 then [Phone] end desc,
	case @Sort when 40 then [Email] end asc, 
	case @Sort when 41 then [Email] end desc

OFFSET @top Rows  
Fetch NEXT @limit ROWS ONLY  
End  