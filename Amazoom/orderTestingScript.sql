SET IDENTITY_INSERT [dbo].[Bundle] ON
INSERT INTO [dbo].[Bundle] ([Id], [ItemID], [ItemName], [Quantity], [OrderID], [AssignedWarehouse]) VALUES (1, 5, 'Lucy', 1, 1, 'warehouse1')
SET IDENTITY_INSERT [dbo].[Bundle] OFF
