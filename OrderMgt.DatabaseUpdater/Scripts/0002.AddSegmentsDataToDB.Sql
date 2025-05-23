GO
SET IDENTITY_INSERT [dbo].[Segments] ON 
GO
INSERT [dbo].[Segments] ([SegmentID], [SegmentName], [Description], [CreatedBy], [DateCreated]) VALUES (1, N'Walk-in Shoppers', N'Customers who visit the store without prior intention and may make impulse purchases', N'admin', CAST(N'2025-05-22T13:27:30.1470000' AS DateTime2))
GO
INSERT [dbo].[Segments] ([SegmentID], [SegmentName], [Description], [CreatedBy], [DateCreated]) VALUES (2, N'Loyal Customers', N'Regular shoppers who frequently buy from the store due to brand trust and past satisfaction', N'admin', CAST(N'2025-05-22T13:27:30.1470000' AS DateTime2))
GO
INSERT [dbo].[Segments] ([SegmentID], [SegmentName], [Description], [CreatedBy], [DateCreated]) VALUES (3, N'Bulk Buyers', N'Those purchasing large quantities for personal use, resale, or business needs', N'admin', CAST(N'2025-05-22T13:27:30.1470000' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[Segments] OFF
GO


