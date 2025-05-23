
SET IDENTITY_INSERT [dbo].[ProductCategories] ON 
GO
INSERT [dbo].[ProductCategories] ([ProductCategoryID], [CategoryName], [Description], [CreatedBy], [DateCreated]) VALUES (1, N'Electronics', N'Smartphones, Laptops, Accessories', N'admin', CAST(N'2025-05-22T14:29:33.9730000' AS DateTime2))
GO
INSERT [dbo].[ProductCategories] ([ProductCategoryID], [CategoryName], [Description], [CreatedBy], [DateCreated]) VALUES (2, N'Clothing & Apparel', N'Men, Women, Kids', N'admin', CAST(N'2025-05-22T14:29:33.9730000' AS DateTime2))
GO
INSERT [dbo].[ProductCategories] ([ProductCategoryID], [CategoryName], [Description], [CreatedBy], [DateCreated]) VALUES (3, N'Home & Kitchen', N'Furniture, Appliances, Decor', N'admin', CAST(N'2025-05-22T14:29:33.9730000' AS DateTime2))
GO
INSERT [dbo].[ProductCategories] ([ProductCategoryID], [CategoryName], [Description], [CreatedBy], [DateCreated]) VALUES (4, N'Beauty & Personal Care', N'Skincare, Haircare, Makeup', N'admin', CAST(N'2025-05-22T14:29:33.9730000' AS DateTime2))
GO
INSERT [dbo].[ProductCategories] ([ProductCategoryID], [CategoryName], [Description], [CreatedBy], [DateCreated]) VALUES (5, N'Sports & Outdoors', N'Fitness Equipment, Camping Gear', N'admin', CAST(N'2025-05-22T14:29:33.9730000' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[ProductCategories] OFF
GO
