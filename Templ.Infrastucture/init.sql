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
	Phone VARCHAR (20),
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
 @start int = 0,  
 @limit int = 10,
 @orderByColumn int = 1,
 @filterByColumn int = 0,
 @filterByText varchar(50) = ''
)  
As  
Begin  
SET NOCOUNT ON;
select *  
from Customer
Order by 
case @orderByColumn
	when 1 then [Name]
	when 2 then [CompanyName]
end
OFFSET @start Rows  
Fetch NEXT @limit ROWS ONLY  
End  