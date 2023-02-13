USE [db_blogengine]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RolUser]') AND type in (N'U'))
ALTER TABLE [dbo].[RolUser] DROP CONSTRAINT IF EXISTS [FK_RolUser_User]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RolUser]') AND type in (N'U'))
ALTER TABLE [dbo].[RolUser] DROP CONSTRAINT IF EXISTS [FK_RolUser_Rol]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RolStatus]') AND type in (N'U'))
ALTER TABLE [dbo].[RolStatus] DROP CONSTRAINT IF EXISTS [FK_RolStatus_Status]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RolStatus]') AND type in (N'U'))
ALTER TABLE [dbo].[RolStatus] DROP CONSTRAINT IF EXISTS [FK_RolStatus_Rol]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Permission]') AND type in (N'U'))
ALTER TABLE [dbo].[Permission] DROP CONSTRAINT IF EXISTS [FK_Permission_Rol]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Permission]') AND type in (N'U'))
ALTER TABLE [dbo].[Permission] DROP CONSTRAINT IF EXISTS [FK_Permission_Menu]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Menu]') AND type in (N'U'))
ALTER TABLE [dbo].[Menu] DROP CONSTRAINT IF EXISTS [FK_Menu_Menu]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Comment]') AND type in (N'U'))
ALTER TABLE [dbo].[Comment] DROP CONSTRAINT IF EXISTS [FK_Comment_Post]
GO
/****** Object:  Table [dbo].[User]    Script Date: 2/13/2023 9:34:17 AM ******/
DROP TABLE IF EXISTS [dbo].[User]
GO
/****** Object:  Table [dbo].[Status]    Script Date: 2/13/2023 9:34:17 AM ******/
DROP TABLE IF EXISTS [dbo].[Status]
GO
/****** Object:  Table [dbo].[RolUser]    Script Date: 2/13/2023 9:34:17 AM ******/
DROP TABLE IF EXISTS [dbo].[RolUser]
GO
/****** Object:  Table [dbo].[RolStatus]    Script Date: 2/13/2023 9:34:17 AM ******/
DROP TABLE IF EXISTS [dbo].[RolStatus]
GO
/****** Object:  Table [dbo].[Rol]    Script Date: 2/13/2023 9:34:17 AM ******/
DROP TABLE IF EXISTS [dbo].[Rol]
GO
/****** Object:  Table [dbo].[Post]    Script Date: 2/13/2023 9:34:17 AM ******/
DROP TABLE IF EXISTS [dbo].[Post]
GO
/****** Object:  Table [dbo].[Permission]    Script Date: 2/13/2023 9:34:17 AM ******/
DROP TABLE IF EXISTS [dbo].[Permission]
GO
/****** Object:  Table [dbo].[Menu]    Script Date: 2/13/2023 9:34:17 AM ******/
DROP TABLE IF EXISTS [dbo].[Menu]
GO
/****** Object:  Table [dbo].[Comment]    Script Date: 2/13/2023 9:34:17 AM ******/
DROP TABLE IF EXISTS [dbo].[Comment]
GO
/****** Object:  Table [dbo].[Comment]    Script Date: 2/13/2023 9:34:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comment](
	[CommentID] [int] IDENTITY(1,1) NOT NULL,
	[PostID] [int] NOT NULL,
	[Comments] [varchar](max) NOT NULL,
	[CommentType] [int] NOT NULL,
	[CreationUser] [varchar](20) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[UpdateUser] [varchar](20) NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED 
(
	[CommentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Menu]    Script Date: 2/13/2023 9:34:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menu](
	[MenuID] [int] NOT NULL,
	[MenuName] [varchar](50) NULL,
	[MenuBaseID] [int] NULL,
	[Url] [varchar](50) NULL,
	[Guid] [uniqueidentifier] NOT NULL,
	[CreationUser] [varchar](20) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[UpdateUser] [varchar](20) NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED 
(
	[MenuID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Permission]    Script Date: 2/13/2023 9:34:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permission](
	[PermissionID] [int] IDENTITY(1,1) NOT NULL,
	[MenuID] [int] NOT NULL,
	[RolID] [int] NOT NULL,
	[ActionCode] [char](3) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreationUser] [varchar](20) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[UpdateUser] [varchar](20) NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_Permission] PRIMARY KEY CLUSTERED 
(
	[PermissionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Post]    Script Date: 2/13/2023 9:34:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Post](
	[PostID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](50) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[Status] [int] NOT NULL,
	[PublishedDate] [datetime] NULL,
	[RejectedDate] [datetime] NULL,
	[CreationUser] [varchar](20) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[UpdateUser] [varchar](20) NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_Post] PRIMARY KEY CLUSTERED 
(
	[PostID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rol]    Script Date: 2/13/2023 9:34:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rol](
	[RolID] [int] NOT NULL,
	[RolName] [varchar](30) NOT NULL,
	[Guid] [uniqueidentifier] NOT NULL,
	[CreationUser] [varchar](20) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[UpdateUser] [varchar](20) NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_Rol] PRIMARY KEY CLUSTERED 
(
	[RolID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RolStatus]    Script Date: 2/13/2023 9:34:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RolStatus](
	[RolStatusID] [int] IDENTITY(1,1) NOT NULL,
	[RolID] [int] NOT NULL,
	[StatusID] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreationUser] [varchar](20) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[UpdateUser] [varchar](20) NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_RolStatus] PRIMARY KEY CLUSTERED 
(
	[RolStatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RolUser]    Script Date: 2/13/2023 9:34:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RolUser](
	[RolUserID] [int] IDENTITY(1,1) NOT NULL,
	[RolID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[CreationUser] [varchar](20) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[UpdateUser] [varchar](20) NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_RolUser] PRIMARY KEY CLUSTERED 
(
	[RolUserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Status]    Script Date: 2/13/2023 9:34:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Status](
	[StatusID] [int] NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[CreationUser] [varchar](20) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[UpdateUser] [varchar](20) NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
(
	[StatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 2/13/2023 9:34:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserLogin] [varchar](30) NOT NULL,
	[Password] [varchar](max) NULL,
	[FullName] [varchar](50) NOT NULL,
	[CreationUser] [varchar](20) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[UpdateUser] [varchar](20) NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_Post] FOREIGN KEY([PostID])
REFERENCES [dbo].[Post] ([PostID])
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_Post]
GO
ALTER TABLE [dbo].[Menu]  WITH CHECK ADD  CONSTRAINT [FK_Menu_Menu] FOREIGN KEY([MenuBaseID])
REFERENCES [dbo].[Menu] ([MenuID])
GO
ALTER TABLE [dbo].[Menu] CHECK CONSTRAINT [FK_Menu_Menu]
GO
ALTER TABLE [dbo].[Permission]  WITH CHECK ADD  CONSTRAINT [FK_Permission_Menu] FOREIGN KEY([MenuID])
REFERENCES [dbo].[Menu] ([MenuID])
GO
ALTER TABLE [dbo].[Permission] CHECK CONSTRAINT [FK_Permission_Menu]
GO
ALTER TABLE [dbo].[Permission]  WITH CHECK ADD  CONSTRAINT [FK_Permission_Rol] FOREIGN KEY([RolID])
REFERENCES [dbo].[Rol] ([RolID])
GO
ALTER TABLE [dbo].[Permission] CHECK CONSTRAINT [FK_Permission_Rol]
GO
ALTER TABLE [dbo].[RolStatus]  WITH CHECK ADD  CONSTRAINT [FK_RolStatus_Rol] FOREIGN KEY([RolID])
REFERENCES [dbo].[Rol] ([RolID])
GO
ALTER TABLE [dbo].[RolStatus] CHECK CONSTRAINT [FK_RolStatus_Rol]
GO
ALTER TABLE [dbo].[RolStatus]  WITH CHECK ADD  CONSTRAINT [FK_RolStatus_Status] FOREIGN KEY([StatusID])
REFERENCES [dbo].[Status] ([StatusID])
GO
ALTER TABLE [dbo].[RolStatus] CHECK CONSTRAINT [FK_RolStatus_Status]
GO
ALTER TABLE [dbo].[RolUser]  WITH CHECK ADD  CONSTRAINT [FK_RolUser_Rol] FOREIGN KEY([RolID])
REFERENCES [dbo].[Rol] ([RolID])
GO
ALTER TABLE [dbo].[RolUser] CHECK CONSTRAINT [FK_RolUser_Rol]
GO
ALTER TABLE [dbo].[RolUser]  WITH CHECK ADD  CONSTRAINT [FK_RolUser_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[RolUser] CHECK CONSTRAINT [FK_RolUser_User]
GO
