
SET IDENTITY_INSERT [dbo].[Customers] ON 
GO
INSERT [dbo].[Customers] ([CustomerID], [FirstName], [LastName], [Email], [PhoneNumber], [Address], [SegmentID], [CreatedBy], [DateCreated]) VALUES (1, N'custome1', N'customer1', N'customer1@demo.com', N'555555555', N'Africa', 1, N'admin', CAST(N'2025-05-22T14:05:46.6240000' AS DateTime2))
GO
INSERT [dbo].[Customers] ([CustomerID], [FirstName], [LastName], [Email], [PhoneNumber], [Address], [SegmentID], [CreatedBy], [DateCreated]) VALUES (2, N'custome2', N'customer2', N'customer2@demo.com', N'555555556', N'Africa 2', 1, N'admin', CAST(N'2025-05-22T14:05:46.6240000' AS DateTime2))
GO
INSERT [dbo].[Customers] ([CustomerID], [FirstName], [LastName], [Email], [PhoneNumber], [Address], [SegmentID], [CreatedBy], [DateCreated]) VALUES (3, N'customer3', N'customer3', N'customer3@demo.com', N'555555557', N'Africa 3', 3, N'admin', CAST(N'2025-05-22T14:05:46.6240000' AS DateTime2))
GO
INSERT [dbo].[Customers] ([CustomerID], [FirstName], [LastName], [Email], [PhoneNumber], [Address], [SegmentID], [CreatedBy], [DateCreated]) VALUES (4, N'customer4', N'customer4', N'customer4@demo.com', N'555555558', N'Africa 4', 2, N'admin', CAST(N'2025-05-22T14:05:46.6240000' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[Customers] OFF
GO

