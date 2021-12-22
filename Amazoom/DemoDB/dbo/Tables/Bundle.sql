CREATE TABLE [dbo].[Bundle]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ItemID] INT NOT NULL, 
    [ItemName] NVARCHAR(50) NOT NULL, 
    [Quantity] INT NOT NULL, 
    [OrderID] INT NOT NULL, 
    [AssignedWarehouse] NCHAR(10) NULL
)
