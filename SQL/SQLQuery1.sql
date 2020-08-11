USE [Magnus]
GO
/****** Object:  Table [dbo].[MUser]    Script Date: 08/11/2020 22:42:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MUser](
	[Username] [nvarchar](50) NOT NULL,
	[Alias] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[IDRoleUser] [int] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_MUser] PRIMARY KEY CLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MRoleUserD]    Script Date: 08/11/2020 22:42:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MRoleUserD](
	[ID] [int] NOT NULL,
	[IDMenu] [int] NULL,
	[IsBaru] [bit] NULL,
	[IsUbah] [bit] NULL,
	[IsHapus] [bit] NULL,
	[IsCetak] [bit] NULL,
	[IDLayoutVersion] [int] NULL,
 CONSTRAINT [PK_MRoleUserD] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MRoleUser]    Script Date: 08/11/2020 22:42:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MRoleUser](
	[ID] [int] NOT NULL,
	[Kode] [varchar](50) NULL,
	[Nama] [nvarchar](50) NULL,
	[Keterangan] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_MRoleUser] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MLayoutVersion]    Script Date: 08/11/2020 22:42:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MLayoutVersion](
	[ID] [int] NOT NULL,
	[Kode] [nvarchar](50) NULL,
	[Nama] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_MLayoutVersion] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
