USE [db_blogengine]
GO
INSERT [dbo].[Rol] ([RolID], [RolName], [Guid], [CreationUser], [CreationDate], [UpdateUser], [UpdateDate]) VALUES (1, N'Public', N'df2696d4-c38a-4e1f-8261-1f2930de59a8', N'admin', CAST(N'2023-02-12T00:00:00.000' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Rol] ([RolID], [RolName], [Guid], [CreationUser], [CreationDate], [UpdateUser], [UpdateDate]) VALUES (2, N'Writer', N'acc10529-1ae8-4bd4-bb71-57b315752ed7', N'admin', CAST(N'2023-02-12T00:00:00.000' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Rol] ([RolID], [RolName], [Guid], [CreationUser], [CreationDate], [UpdateUser], [UpdateDate]) VALUES (3, N'Editor', N'e8a1f780-25bd-4fb1-9b4c-924483d5eb87', N'admin', CAST(N'2023-02-12T00:00:00.000' AS DateTime), NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[User] ON 
GO
INSERT [dbo].[User] ([UserID], [UserLogin], [Password], [FullName], [CreationUser], [CreationDate], [UpdateUser], [UpdateDate]) VALUES (1, N'User1', N'abc123@', N'Edgar Huarcaya', N'admin', CAST(N'2023-02-12T00:00:00.000' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[User] ([UserID], [UserLogin], [Password], [FullName], [CreationUser], [CreationDate], [UpdateUser], [UpdateDate]) VALUES (2, N'User2', N'abc123@', N'George Lopez', N'admin', CAST(N'2023-02-12T00:00:00.000' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[User] ([UserID], [UserLogin], [Password], [FullName], [CreationUser], [CreationDate], [UpdateUser], [UpdateDate]) VALUES (3, N'User3', N'abc123@', N'Carolina Manrique', N'admin', CAST(N'2023-02-12T00:00:00.000' AS DateTime), NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET IDENTITY_INSERT [dbo].[RolUser] ON 
GO
INSERT [dbo].[RolUser] ([RolUserID], [RolID], [UserID], [CreationUser], [CreationDate], [UpdateUser], [UpdateDate]) VALUES (1, 1, 1, N'admin', CAST(N'2023-02-12T00:00:00.000' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[RolUser] ([RolUserID], [RolID], [UserID], [CreationUser], [CreationDate], [UpdateUser], [UpdateDate]) VALUES (2, 2, 2, N'admin', CAST(N'2023-02-12T00:00:00.000' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[RolUser] ([RolUserID], [RolID], [UserID], [CreationUser], [CreationDate], [UpdateUser], [UpdateDate]) VALUES (3, 3, 3, N'admin', CAST(N'2023-02-12T00:00:00.000' AS DateTime), NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[RolUser] OFF
GO
INSERT [dbo].[Menu] ([MenuID], [MenuName], [MenuBaseID], [Url], [Guid], [CreationUser], [CreationDate], [UpdateUser], [UpdateDate]) VALUES (1, N'Home', NULL, N'/login', N'cc9c654e-3532-4a91-a7ce-50b0cd5d1342', N'admin', CAST(N'2023-02-12T00:00:00.000' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Menu] ([MenuID], [MenuName], [MenuBaseID], [Url], [Guid], [CreationUser], [CreationDate], [UpdateUser], [UpdateDate]) VALUES (2, N'Posts', 1, N'/post', N'9bf96c5f-3a27-4ed7-9116-2ec32ec30d20', N'admin', CAST(N'2023-02-12T00:00:00.000' AS DateTime), NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Permission] ON 
GO
INSERT [dbo].[Permission] ([PermissionID], [MenuID], [RolID], [ActionCode], [IsActive], [CreationUser], [CreationDate], [UpdateUser], [UpdateDate]) VALUES (1, 1, 1, N'ACC', 1, N'admin', CAST(N'2023-02-12T00:00:00.000' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Permission] ([PermissionID], [MenuID], [RolID], [ActionCode], [IsActive], [CreationUser], [CreationDate], [UpdateUser], [UpdateDate]) VALUES (2, 1, 2, N'ACC', 1, N'admin', CAST(N'2023-02-12T00:00:00.000' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Permission] ([PermissionID], [MenuID], [RolID], [ActionCode], [IsActive], [CreationUser], [CreationDate], [UpdateUser], [UpdateDate]) VALUES (3, 1, 3, N'ACC', 1, N'admin', CAST(N'2023-02-12T00:00:00.000' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Permission] ([PermissionID], [MenuID], [RolID], [ActionCode], [IsActive], [CreationUser], [CreationDate], [UpdateUser], [UpdateDate]) VALUES (4, 2, 1, N'ACC', 1, N'admin', CAST(N'2023-02-12T00:00:00.000' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Permission] ([PermissionID], [MenuID], [RolID], [ActionCode], [IsActive], [CreationUser], [CreationDate], [UpdateUser], [UpdateDate]) VALUES (5, 2, 2, N'ACC', 1, N'admin', CAST(N'2023-02-12T00:00:00.000' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Permission] ([PermissionID], [MenuID], [RolID], [ActionCode], [IsActive], [CreationUser], [CreationDate], [UpdateUser], [UpdateDate]) VALUES (6, 2, 3, N'ACC', 1, N'admin', CAST(N'2023-02-12T00:00:00.000' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Permission] ([PermissionID], [MenuID], [RolID], [ActionCode], [IsActive], [CreationUser], [CreationDate], [UpdateUser], [UpdateDate]) VALUES (7, 2, 1, N'LST', 1, N'admin', CAST(N'2023-02-12T00:00:00.000' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Permission] ([PermissionID], [MenuID], [RolID], [ActionCode], [IsActive], [CreationUser], [CreationDate], [UpdateUser], [UpdateDate]) VALUES (8, 2, 2, N'LST', 1, N'admin', CAST(N'2023-02-12T00:00:00.000' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Permission] ([PermissionID], [MenuID], [RolID], [ActionCode], [IsActive], [CreationUser], [CreationDate], [UpdateUser], [UpdateDate]) VALUES (9, 2, 3, N'LST', 1, N'admin', CAST(N'2023-02-12T00:00:00.000' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Permission] ([PermissionID], [MenuID], [RolID], [ActionCode], [IsActive], [CreationUser], [CreationDate], [UpdateUser], [UpdateDate]) VALUES (10, 2, 1, N'ADD', 0, N'admin', CAST(N'2023-02-12T00:00:00.000' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Permission] ([PermissionID], [MenuID], [RolID], [ActionCode], [IsActive], [CreationUser], [CreationDate], [UpdateUser], [UpdateDate]) VALUES (11, 2, 2, N'ADD', 1, N'admin', CAST(N'2023-02-12T00:00:00.000' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Permission] ([PermissionID], [MenuID], [RolID], [ActionCode], [IsActive], [CreationUser], [CreationDate], [UpdateUser], [UpdateDate]) VALUES (12, 2, 3, N'ADD', 0, N'admin', CAST(N'2023-02-12T00:00:00.000' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Permission] ([PermissionID], [MenuID], [RolID], [ActionCode], [IsActive], [CreationUser], [CreationDate], [UpdateUser], [UpdateDate]) VALUES (13, 2, 1, N'UPD', 0, N'admin', CAST(N'2023-02-12T00:00:00.000' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Permission] ([PermissionID], [MenuID], [RolID], [ActionCode], [IsActive], [CreationUser], [CreationDate], [UpdateUser], [UpdateDate]) VALUES (14, 2, 2, N'UPD', 1, N'admin', CAST(N'2023-02-12T00:00:00.000' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Permission] ([PermissionID], [MenuID], [RolID], [ActionCode], [IsActive], [CreationUser], [CreationDate], [UpdateUser], [UpdateDate]) VALUES (15, 2, 3, N'UPD', 0, N'admin', CAST(N'2023-02-12T00:00:00.000' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Permission] ([PermissionID], [MenuID], [RolID], [ActionCode], [IsActive], [CreationUser], [CreationDate], [UpdateUser], [UpdateDate]) VALUES (16, 2, 1, N'APR', 0, N'admin', CAST(N'2023-02-12T00:00:00.000' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Permission] ([PermissionID], [MenuID], [RolID], [ActionCode], [IsActive], [CreationUser], [CreationDate], [UpdateUser], [UpdateDate]) VALUES (17, 2, 2, N'APR', 1, N'admin', CAST(N'2023-02-12T00:00:00.000' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Permission] ([PermissionID], [MenuID], [RolID], [ActionCode], [IsActive], [CreationUser], [CreationDate], [UpdateUser], [UpdateDate]) VALUES (18, 2, 3, N'APR', 1, N'admin', CAST(N'2023-02-12T00:00:00.000' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Permission] ([PermissionID], [MenuID], [RolID], [ActionCode], [IsActive], [CreationUser], [CreationDate], [UpdateUser], [UpdateDate]) VALUES (19, 2, 1, N'REJ', 0, N'admin', CAST(N'2023-02-12T00:00:00.000' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Permission] ([PermissionID], [MenuID], [RolID], [ActionCode], [IsActive], [CreationUser], [CreationDate], [UpdateUser], [UpdateDate]) VALUES (20, 2, 2, N'REJ', 1, N'admin', CAST(N'2023-02-12T00:00:00.000' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Permission] ([PermissionID], [MenuID], [RolID], [ActionCode], [IsActive], [CreationUser], [CreationDate], [UpdateUser], [UpdateDate]) VALUES (21, 2, 3, N'REJ', 1, N'admin', CAST(N'2023-02-12T00:00:00.000' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Permission] ([PermissionID], [MenuID], [RolID], [ActionCode], [IsActive], [CreationUser], [CreationDate], [UpdateUser], [UpdateDate]) VALUES (22, 2, 1, N'VIW', 1, N'admin', CAST(N'2023-02-12T00:00:00.000' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Permission] ([PermissionID], [MenuID], [RolID], [ActionCode], [IsActive], [CreationUser], [CreationDate], [UpdateUser], [UpdateDate]) VALUES (23, 2, 2, N'VIW', 1, N'admin', CAST(N'2023-02-12T00:00:00.000' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Permission] ([PermissionID], [MenuID], [RolID], [ActionCode], [IsActive], [CreationUser], [CreationDate], [UpdateUser], [UpdateDate]) VALUES (24, 2, 3, N'VIW', 1, N'admin', CAST(N'2023-02-12T00:00:00.000' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Permission] ([PermissionID], [MenuID], [RolID], [ActionCode], [IsActive], [CreationUser], [CreationDate], [UpdateUser], [UpdateDate]) VALUES (25, 2, 1, N'SUB', 0, N'admin', CAST(N'2023-02-12T00:00:00.000' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Permission] ([PermissionID], [MenuID], [RolID], [ActionCode], [IsActive], [CreationUser], [CreationDate], [UpdateUser], [UpdateDate]) VALUES (26, 2, 2, N'SUB', 1, N'admin', CAST(N'2023-02-12T00:00:00.000' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Permission] ([PermissionID], [MenuID], [RolID], [ActionCode], [IsActive], [CreationUser], [CreationDate], [UpdateUser], [UpdateDate]) VALUES (27, 2, 3, N'SUB', 0, N'admin', CAST(N'2023-02-12T00:00:00.000' AS DateTime), NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Permission] OFF
GO
INSERT [dbo].[Status] ([StatusID], [Description], [CreationUser], [CreationDate], [UpdateUser], [UpdateDate]) VALUES (1, N'Pending', N'admin', CAST(N'2023-02-12T00:00:00.000' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Status] ([StatusID], [Description], [CreationUser], [CreationDate], [UpdateUser], [UpdateDate]) VALUES (2, N'Pending Aproval', N'admin', CAST(N'2023-02-12T00:00:00.000' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Status] ([StatusID], [Description], [CreationUser], [CreationDate], [UpdateUser], [UpdateDate]) VALUES (3, N'Published', N'admin', CAST(N'2023-02-12T00:00:00.000' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Status] ([StatusID], [Description], [CreationUser], [CreationDate], [UpdateUser], [UpdateDate]) VALUES (4, N'Rejected', N'admin', CAST(N'2023-02-12T00:00:00.000' AS DateTime), NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[RolStatus] ON 
GO
INSERT [dbo].[RolStatus] ([RolStatusID], [RolID], [StatusID], [IsActive], [CreationUser], [CreationDate], [UpdateUser], [UpdateDate]) VALUES (1, 1, 3, 1, N'admin', CAST(N'2023-02-12T00:00:00.000' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[RolStatus] ([RolStatusID], [RolID], [StatusID], [IsActive], [CreationUser], [CreationDate], [UpdateUser], [UpdateDate]) VALUES (2, 2, 3, 1, N'admin', CAST(N'2023-02-23T00:00:00.000' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[RolStatus] ([RolStatusID], [RolID], [StatusID], [IsActive], [CreationUser], [CreationDate], [UpdateUser], [UpdateDate]) VALUES (3, 3, 3, 1, N'admin', CAST(N'2023-02-12T00:00:00.000' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[RolStatus] ([RolStatusID], [RolID], [StatusID], [IsActive], [CreationUser], [CreationDate], [UpdateUser], [UpdateDate]) VALUES (4, 2, 1, 1, N'admin', CAST(N'2023-02-12T00:00:00.000' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[RolStatus] ([RolStatusID], [RolID], [StatusID], [IsActive], [CreationUser], [CreationDate], [UpdateUser], [UpdateDate]) VALUES (5, 2, 4, 1, N'admin', CAST(N'2023-02-12T00:00:00.000' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[RolStatus] ([RolStatusID], [RolID], [StatusID], [IsActive], [CreationUser], [CreationDate], [UpdateUser], [UpdateDate]) VALUES (6, 3, 2, 1, N'admin', CAST(N'2023-02-12T00:00:00.000' AS DateTime), NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[RolStatus] OFF
GO
