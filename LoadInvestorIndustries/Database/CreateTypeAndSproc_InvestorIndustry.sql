


/****** Object:  UserDefinedTableType [dbo].[InvestorIndustryType]    Script Date: 8/20/2013 10:37:14 AM ******/
CREATE TYPE [dbo].[InvestorIndustryType] AS TABLE(
	[InvestorId] [int] NOT NULL,
	[IndustryId] [int] NOT NULL
)
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

