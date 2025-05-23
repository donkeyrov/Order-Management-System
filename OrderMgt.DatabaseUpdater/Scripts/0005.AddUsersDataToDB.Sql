
SET IDENTITY_INSERT [dbo].[Users] ON 
GO
INSERT [dbo].[Users] ([UserID], [Username], [Password], [FullName], [IsActive], [Role]) VALUES (1, N'user@demo.com', N'824AHJR6rp2uuBP3jiC/Y17ZvkZ92V7noNHADYFsMjI=', N'user user', 1, N'user')
GO
INSERT [dbo].[Users] ([UserID], [Username], [Password], [FullName], [IsActive], [Role]) VALUES (2, N'admin@demo.com', N'824AHJR6rp2uuBP3jiC/Y17ZvkZ92V7noNHADYFsMjI=', N'administrator', 1, N'admin')
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
