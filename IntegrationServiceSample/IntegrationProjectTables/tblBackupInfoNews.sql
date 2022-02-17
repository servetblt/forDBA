USE [DbaTools]
GO

/****** Object:  Table [dbo].[tblBackupInfoNews]    Script Date: 4.11.2021 10:55:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblBackupInfoNews](
	[SQLInstanceName] [nvarchar](150) NULL,
	[ServerIP] [nvarchar](60) NULL,
	[SQLInstancePort] [nvarchar](60) NULL,
	[DatabaseName] [nvarchar](150) NULL,
	[LastFullBackup] [datetime] NULL,
	[LastDiffBackup] [datetime] NULL,
	[LastBackup_Hrs] [int] NULL,
	[dbstatus] [nvarchar](50) NULL,
	[dbsize] [nvarchar](50) NULL,
	[dbbackupsize] [nvarchar](50) NULL,
	[fullBackupTime] [nvarchar](50) NULL,
	[diffBackupTime] [nvarchar](50) NULL,
	[logBackupTime] [nvarchar](50) NULL,
	[dbcompretionRate] [nvarchar](50) NULL,
	[dbInPerSec] [nvarchar](50) NULL,
	[dbOutPerSec] [nvarchar](50) NULL,
	[BackupStatus] [nvarchar](50) NULL,
	[lastTlogBackup] [datetime] NULL,
	[RecoveryModel] [nvarchar](50) NULL,
	[NoTLogSince] [int] NULL,
	[TlogBkpStatus] [nvarchar](150) NULL,
	[Logtime] [datetime] NULL
) ON [PRIMARY]
GO


