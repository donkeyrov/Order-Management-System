
SET IDENTITY_INSERT [dbo].[OrderHistory] ON 
GO
INSERT [dbo].[OrderHistory] ([OrderHistoryID], [OrderID], [TransactionDate], [OrderNo], [CustomerID], [Details], [OrderStatus], [Discount], [Total], [CreatedBy], [DateCreated], [CompletedBy], [DateCompleted]) VALUES (3, 1, CAST(N'2025-05-22T14:53:35.2840000' AS DateTime2), N'0001', 1, N'First order by customer', N'Complete', 0, 140, N'user', CAST(N'2025-05-22T14:53:35.2840000' AS DateTime2), N'user', CAST(N'2025-05-22T17:39:57.8078099' AS DateTime2))
GO
INSERT [dbo].[OrderHistory] ([OrderHistoryID], [OrderID], [TransactionDate], [OrderNo], [CustomerID], [Details], [OrderStatus], [Discount], [Total], [CreatedBy], [DateCreated], [CompletedBy], [DateCompleted]) VALUES (5, 5, CAST(N'2025-05-22T14:53:35.2840000' AS DateTime2), N'0004', 4, N'Fifth order by customer', N'Complete', 0, 1200, N'user', CAST(N'2025-05-22T14:53:35.2840000' AS DateTime2), N'user', CAST(N'2025-05-22T17:44:16.8091318' AS DateTime2))
GO
INSERT [dbo].[OrderHistory] ([OrderHistoryID], [OrderID], [TransactionDate], [OrderNo], [CustomerID], [Details], [OrderStatus], [Discount], [Total], [CreatedBy], [DateCreated], [CompletedBy], [DateCompleted]) VALUES (7, 6, CAST(N'2025-05-22T14:53:35.2840000' AS DateTime2), N'0005', 4, N'Sixth order by customer', N'Complete', 0, 2300, N'user', CAST(N'2025-05-22T14:53:35.2840000' AS DateTime2), N'user', CAST(N'2025-05-22T17:44:54.8700495' AS DateTime2))
GO
INSERT [dbo].[OrderHistory] ([OrderHistoryID], [OrderID], [TransactionDate], [OrderNo], [CustomerID], [Details], [OrderStatus], [Discount], [Total], [CreatedBy], [DateCreated], [CompletedBy], [DateCompleted]) VALUES (9, 7, CAST(N'2025-05-22T14:53:35.2840000' AS DateTime2), N'0006', 4, N'Order 7 by customer', N'Complete', 0, 670, N'user', CAST(N'2025-05-22T14:53:35.2840000' AS DateTime2), N'user', CAST(N'2025-05-22T17:47:07.9760367' AS DateTime2))
GO
INSERT [dbo].[OrderHistory] ([OrderHistoryID], [OrderID], [TransactionDate], [OrderNo], [CustomerID], [Details], [OrderStatus], [Discount], [Total], [CreatedBy], [DateCreated], [CompletedBy], [DateCompleted]) VALUES (11, 8, CAST(N'2025-05-22T14:53:35.2840000' AS DateTime2), N'0007', 4, N'Order 8 by customer', N'Complete', 56, 504, N'user', CAST(N'2025-05-22T14:53:35.2840000' AS DateTime2), N'user', CAST(N'2025-05-22T17:49:50.2008866' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[OrderHistory] OFF
GO

