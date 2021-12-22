CREATE TABLE [dbo].[Item]
(
	[Id] INT NOT NULL  IDENTITY, 
    [ItemID] INT NOT NULL, 
    [ItemWeight] INT NOT NULL, 
    [ItemVolume] INT NOT NULL, 
    [ItemName] NVARCHAR(50) NOT NULL, 
    [Stock] INT NOT NULL, 
    CONSTRAINT [PK_Item] PRIMARY KEY ([Id])
)
