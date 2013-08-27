

/****** Object:  Table [dbo].[Industry]    Script Date: 8/20/2013 10:34:36 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Industry](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ExternalId] [int] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[DisplayName] [nvarchar](255) NULL,
 CONSTRAINT [PK_Industry] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

