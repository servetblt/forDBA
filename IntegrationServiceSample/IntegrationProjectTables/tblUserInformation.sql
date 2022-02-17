USE [DbaTools]
GO

/****** Object:  Table [dbo].[tblUserInformation]    Script Date: 4.11.2021 10:58:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblUserInformation](
	[serverName] [nvarchar](150) NULL,
	[serverIP] [nvarchar](150) NULL,
	[serverPort] [nvarchar](150) NULL,
	[dbname] [nvarchar](150) NULL,
	[dbUser] [nvarchar](150) NULL,
	[serverLogin] [nvarchar](150) NULL,
	[logintype] [nvarchar](150) NULL,
	[sysadmin] [nvarchar](5) NULL,
	[securityadmin] [nvarchar](5) NULL,
	[serveradmin] [nvarchar](5) NULL,
	[diskadmin] [nvarchar](5) NULL,
	[setupadmin] [nvarchar](5) NULL,
	[processadmin] [nvarchar](5) NULL,
	[dbcreator] [nvarchar](5) NULL,
	[bulkadmin] [nvarchar](5) NULL,
	[create_date] [datetime] NULL,
	[modify_date] [datetime] NULL,
	[Permissions_user] [nvarchar](450) NULL,
	[logTime] [datetime] NULL
) ON [PRIMARY]
GO


