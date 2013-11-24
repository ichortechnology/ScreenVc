CREATE TYPE [dbo].[ExternalIndustryType] AS TABLE
(
	[ExternalId] [int] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[DisplayName] [nvarchar](255) NULL,
	[Updated] [datetime2](7) NULL
)
