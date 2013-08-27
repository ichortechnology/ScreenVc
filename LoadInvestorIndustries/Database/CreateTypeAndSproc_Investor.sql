

/****** Object:  UserDefinedTableType [dbo].[InvestorType]    Script Date: 8/20/2013 10:37:53 AM ******/
CREATE TYPE [dbo].[InvestorType] AS TABLE(
	[ExternalId] [int] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[OnlineBioUrl] [varchar](1024) NULL,
	[LinkedInUrl] [varchar](1024) NULL
)
GO

CREATE PROCEDURE [dbo].[UpsertInvestorTvp]
(@investorTvp as InvestorType readonly)
AS
BEGIN

MERGE Investor AS T
USING @investorTvp AS S 
ON (T.ExternalId = S.ExternalId) 
WHEN MATCHED 
    THEN UPDATE SET T.Name = S.Name, T.OnlineBioUrl = S.OnlineBioUrl, T.LinkedInUrl = S.LinkedInUrl 
WHEN NOT MATCHED BY TARGET 
    THEN INSERT (ExternalId, Name, OnlineBioUrl, LinkedInUrl) 
		 VALUES (S.ExternalId, S.Name, S.OnlineBioUrl, S.LinkedInUrl);

RETURN 0;

END

GO

