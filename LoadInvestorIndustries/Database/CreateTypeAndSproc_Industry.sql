

/****** Object:  UserDefinedTableType [dbo].[IndustryType]    Script Date: 8/20/2013 10:35:37 AM ******/
CREATE TYPE [dbo].[IndustryType] AS TABLE(
	[ExternalId] [int] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[DisplayName] [nvarchar](255) NULL
)
GO


CREATE PROCEDURE [dbo].[UpsertIndustryTvp]
(@industryTvp as IndustryType readonly)
AS
BEGIN

MERGE Industry AS T
USING @industryTvp AS S 
ON (T.ExternalId = S.ExternalId) 
WHEN MATCHED 
    THEN UPDATE SET T.Name = S.Name, T.DisplayName = S.DisplayName
WHEN NOT MATCHED  
    THEN INSERT (ExternalId, Name, DisplayName) 
		 VALUES (S.ExternalId, S.Name, S.DisplayName);

RETURN 0

END






GO

