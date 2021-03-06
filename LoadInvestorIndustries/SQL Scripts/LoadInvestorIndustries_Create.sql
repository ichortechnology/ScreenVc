USE [ScreenVc]
GO

/****** Object:  UserDefinedTableType [dbo].[IndustryType]    Script Date: 9/26/2013 12:09:13 AM ******/
CREATE TYPE [dbo].[IndustryType] AS TABLE(
	[ExternalId] [int] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[DisplayName] [nvarchar](255) NULL,
	[Updated] [datetime2](7) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[InvestorIndustryType]    Script Date: 9/26/2013 12:09:13 AM ******/
CREATE TYPE [dbo].[InvestorIndustryType] AS TABLE(
	[InvestorId] [int] NOT NULL,
	[IndustryId] [int] NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[InvestorType]    Script Date: 9/26/2013 12:09:13 AM ******/
CREATE TYPE [dbo].[InvestorType] AS TABLE(
	[ExternalId] [int] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[OnlineBioUrl] [varchar](1024) NULL,
	[LinkedInUrl] [varchar](1024) NULL,
	[Updated] [datetime2](7) NULL
)
GO
/****** Object:  StoredProcedure [dbo].[UpsertIndustryTvp]    Script Date: 9/26/2013 12:09:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[UpsertIndustryTvp]
(@industryTvp as IndustryType readonly)
AS
BEGIN

MERGE Industry AS T
USING @industryTvp AS S 
ON (T.ExternalId = S.ExternalId) 
WHEN MATCHED 
    THEN UPDATE SET T.Name = S.Name, T.DisplayName = S.DisplayName, T.Updated = GETDATE()
WHEN NOT MATCHED  
    THEN INSERT (ExternalId, Name, DisplayName) 
		 VALUES (S.ExternalId, S.Name, S.DisplayName);

RETURN 0

END










GO
/****** Object:  StoredProcedure [dbo].[UpsertInvestorIndustryTvp]    Script Date: 9/26/2013 12:09:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[UpsertInvestorIndustryTvp]
(@investorIndustryTvp as InvestorIndustryType readonly)
AS
BEGIN

-- Don't update, just insert new pairs.
MERGE InvestorIndustry AS T
USING (
select Investor.Id as InvestorId,  Industry.Id as IndustryId
	from @investorIndustryTvp as IITvp
	inner join Investor on Investor.ExternalId = IITvp.InvestorId
	inner join Industry on Industry.ExternalId = IITvp.IndustryId
)
 AS S 
ON (T.InvestorId = S.InvestorId and T.IndustryId = S.IndustryId)
WHEN NOT MATCHED BY TARGET 
    THEN INSERT (InvestorId, IndustryId) 
		 VALUES (S.InvestorId, S.IndustryId);

RETURN 0;

END







GO
/****** Object:  StoredProcedure [dbo].[UpsertInvestorTvp]    Script Date: 9/26/2013 12:09:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpsertInvestorTvp]
(@investorTvp as InvestorType readonly)
AS
BEGIN

MERGE Investor AS T
USING @investorTvp AS S 
ON (T.ExternalId = S.ExternalId) 
WHEN MATCHED 
    THEN UPDATE SET T.Name = S.Name, T.OnlineBioUrl = S.OnlineBioUrl, T.LinkedInUrl = S.LinkedInUrl, T.Updated = GETDATE() 
WHEN NOT MATCHED BY TARGET 
    THEN INSERT (ExternalId, Name, OnlineBioUrl, LinkedInUrl) 
		 VALUES (S.ExternalId, S.Name, S.OnlineBioUrl, S.LinkedInUrl);

RETURN 0;

END





GO
/****** Object:  Table [dbo].[Industry]    Script Date: 9/26/2013 12:09:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Industry](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ExternalId] [int] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[DisplayName] [nvarchar](255) NULL,
	[Updated] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Industry] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Investor]    Script Date: 9/26/2013 12:09:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Investor](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ExternalId] [int] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[OnlineBioUrl] [varchar](1024) NULL,
	[LinkedInUrl] [varchar](1024) NULL,
	[Updated] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Investor] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[InvestorIndustry]    Script Date: 9/26/2013 12:09:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvestorIndustry](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InvestorId] [int] NOT NULL,
	[IndustryId] [int] NOT NULL,
 CONSTRAINT [PK_InvestorIndustry] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Industry] ADD  CONSTRAINT [DF_Industry_Updated]  DEFAULT (getdate()) FOR [Updated]
GO
ALTER TABLE [dbo].[Investor] ADD  CONSTRAINT [DF_Investor_Updated]  DEFAULT (getdate()) FOR [Updated]
GO
ALTER TABLE [dbo].[InvestorIndustry]  WITH CHECK ADD  CONSTRAINT [FK_InvestorIndustry_Industry] FOREIGN KEY([IndustryId])
REFERENCES [dbo].[Industry] ([Id])
GO
ALTER TABLE [dbo].[InvestorIndustry] CHECK CONSTRAINT [FK_InvestorIndustry_Industry]
GO
ALTER TABLE [dbo].[InvestorIndustry]  WITH CHECK ADD  CONSTRAINT [FK_InvestorIndustry_Investor] FOREIGN KEY([InvestorId])
REFERENCES [dbo].[Investor] ([Id])
GO
ALTER TABLE [dbo].[InvestorIndustry] CHECK CONSTRAINT [FK_InvestorIndustry_Investor]
GO
USE [master]
GO
ALTER DATABASE [ScreenVc] SET  READ_WRITE 
GO
