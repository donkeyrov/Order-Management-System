
SET IDENTITY_INSERT [dbo].[Orders] ON 
GO
INSERT [dbo].[Orders] ([OrderID], [TransactionDate], [OrderNo], [CustomerID], [Details], [OrderStatus], [Discount], [Total], [CreatedBy], [DateCreated], [CompletedBy], [DateCompleted]) VALUES (2, CAST(N'2025-05-22T14:53:35.2840000' AS DateTime2), N'0002', 2, N'second order by customer', N'Created', 0, 1340, N'user', CAST(N'2025-05-22T14:53:35.2840000' AS DateTime2), N'user', CAST(N'2025-05-22T14:53:35.2840000' AS DateTime2))
GO
INSERT [dbo].[Orders] ([OrderID], [TransactionDate], [OrderNo], [CustomerID], [Details], [OrderStatus], [Discount], [Total], [CreatedBy], [DateCreated], [CompletedBy], [DateCompleted]) VALUES (3, CAST(N'2025-05-22T14:53:35.2840000' AS DateTime2), N'0003', 2, N'Third order by customer', N'Created', 0, 200, N'user', CAST(N'2025-05-22T14:53:35.2840000' AS DateTime2), N'user', CAST(N'2025-05-22T14:53:35.2840000' AS DateTime2))
GO
INSERT [dbo].[Orders] ([OrderID], [TransactionDate], [OrderNo], [CustomerID], [Details], [OrderStatus], [Discount], [Total], [CreatedBy], [DateCreated], [CompletedBy], [DateCompleted]) VALUES (4, CAST(N'2025-05-22T14:53:35.2840000' AS DateTime2), N'0009', 3, N'Fourth order by customer', N'Created', 0, 12000, N'user', CAST(N'2025-05-22T14:53:35.2840000' AS DateTime2), N'user', CAST(N'2025-05-22T14:53:35.2840000' AS DateTime2))
GO
INSERT [dbo].[Orders] ([OrderID], [TransactionDate], [OrderNo], [CustomerID], [Details], [OrderStatus], [Discount], [Total], [CreatedBy], [DateCreated], [CompletedBy], [DateCompleted]) VALUES (9, CAST(N'2025-05-22T14:53:35.2840000' AS DateTime2), N'0008', 4, N'Order 9 by customer', N'Created', 0, 3000, N'user', CAST(N'2025-05-22T14:53:35.2840000' AS DateTime2), N'user', CAST(N'2025-05-22T14:53:35.2840000' AS DateTime2))
GO
INSERT [dbo].[Orders] ([OrderID], [TransactionDate], [OrderNo], [CustomerID], [Details], [OrderStatus], [Discount], [Total], [CreatedBy], [DateCreated], [CompletedBy], [DateCompleted]) VALUES (10, CAST(N'2025-05-22T14:53:35.2840000' AS DateTime2), N'00011', 4, N'Order 10 by customer', N'Created', 0, 3400, N'user', CAST(N'2025-05-22T14:53:35.2840000' AS DateTime2), N'user', CAST(N'2025-05-22T14:53:35.2840000' AS DateTime2))
GO
INSERT [dbo].[Orders] ([OrderID], [TransactionDate], [OrderNo], [CustomerID], [Details], [OrderStatus], [Discount], [Total], [CreatedBy], [DateCreated], [CompletedBy], [DateCompleted]) VALUES (11, CAST(N'2025-05-22T14:53:35.2840000' AS DateTime2), N'0010', 4, N'Order 11 by customer', N'Created', 0, 980, N'user', CAST(N'2025-05-22T14:53:35.2840000' AS DateTime2), N'user', CAST(N'2025-05-22T14:53:35.2840000' AS DateTime2))
GO
INSERT [dbo].[Orders] ([OrderID], [TransactionDate], [OrderNo], [CustomerID], [Details], [OrderStatus], [Discount], [Total], [CreatedBy], [DateCreated], [CompletedBy], [DateCompleted]) VALUES (12, CAST(N'2025-05-22T14:53:35.2840000' AS DateTime2), N'0012', 5, N'Order 12 by customer', N'Created', 0, 6000, N'user', CAST(N'2025-05-22T14:53:35.2840000' AS DateTime2), N'user', CAST(N'2025-05-22T14:53:35.2840000' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO

