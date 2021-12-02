USE [DbaTools]
GO

/****** Object:  Table [dbo].[tblBackupInfoError]    Script Date: 4.11.2021 10:53:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblBackupInfoError](
	[serverName] [nvarchar](150) NULL,
	[errorText] [nvarchar](max) NULL,
	[erorDate] [datetime2](7) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


