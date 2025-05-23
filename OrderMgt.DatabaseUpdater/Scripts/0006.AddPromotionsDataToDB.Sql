
SET IDENTITY_INSERT [dbo].[Promotions] ON 
GO
INSERT [dbo].[Promotions] ([PromotionID], [SegmentID], [PromotionName], [MinOrders], [MaxOrders], [DiscountPercentage]) VALUES (1, 2, N'Localty Appreciation 1', 3, 10, 10)
GO
INSERT [dbo].[Promotions] ([PromotionID], [SegmentID], [PromotionName], [MinOrders], [MaxOrders], [DiscountPercentage]) VALUES (2, 2, N'Localty Appreciation 2', 11, 50, 15)
GO
INSERT [dbo].[Promotions] ([PromotionID], [SegmentID], [PromotionName], [MinOrders], [MaxOrders], [DiscountPercentage]) VALUES (3, 3, N'Bulk Appreciation 1', 3, 6, 7)
GO
INSERT [dbo].[Promotions] ([PromotionID], [SegmentID], [PromotionName], [MinOrders], [MaxOrders], [DiscountPercentage]) VALUES (4, 3, N'Bulk Appreciation 2', 7, 15, 14)
GO
INSERT [dbo].[Promotions] ([PromotionID], [SegmentID], [PromotionName], [MinOrders], [MaxOrders], [DiscountPercentage]) VALUES (5, 3, N'Bulk Appreciation 2', 16, 50, 20)
GO
SET IDENTITY_INSERT [dbo].[Promotions] OFF
GO

