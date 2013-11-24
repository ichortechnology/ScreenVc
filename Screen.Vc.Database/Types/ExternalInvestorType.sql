CREATE TYPE [dbo].[ExternalInvestorType] AS TABLE
(
	[ExternalId] [int] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[OnlineBioUrl] [nvarchar](1024) NULL,
	[LinkedInUrl] [nvarchar](1024) NULL,
    [ExternalInvestorSourceId] [int] NOT NULL,
	[Updated] [datetime2](7) NULL
)
