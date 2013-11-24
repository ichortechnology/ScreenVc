CREATE TABLE [dbo].[ExternalInvestor]
(
    [ExternalInvestorId] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[ExternalId] [int] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[OnlineBioUrl] [nvarchar](1024) NULL,
	[LinkedInUrl] [nvarchar](1024) NULL,
	[Updated] [datetime2](7) NULL, 
    [ExternalInvestorSourceId] INT NOT NULL, 
    CONSTRAINT [FK_ExternalInvestor_ExternalInvestorSource] FOREIGN KEY ([ExternalInvestorSourceId]) REFERENCES [ExternalInvestorSource]([ExternalInvestorSourceId])
)
