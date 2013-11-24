CREATE PROCEDURE [dbo].[sproc_ExternalInvestorExternalIndustry_Upsert]
(
    @externalInvestorExternalIndustryTvp as ExternalInvestorExternalIndustryType readonly
)
AS
BEGIN
     --Don't update, just insert new pairs.
    MERGE [dbo].[ExternalInvestorExternalIndustry] AS T
    USING (
            select EInv.ExternalInvestorId as ExternalInvestorId,  EInd.ExternalIndustryId as ExternalIndustryId
	        from        @externalInvestorExternalIndustryTvp as EIEITvp
	        inner join  ExternalInvestor EInv on EInv.ExternalInvestorId = EIEITvp.ExternalInvestorId
	        inner join  ExternalIndustry EInd on EInd.ExternalIndustryId = EIEITvp.ExternalIndustryId
    ) AS S 
    ON (T.ExternalInvestorId = S.ExternalInvestorId and T.ExternalIndustryId = S.ExternalIndustryId)
    WHEN NOT MATCHED BY TARGET 
        THEN INSERT (ExternalInvestorId, ExternalIndustryId) 
		     VALUES (S.ExternalInvestorId, S.ExternalIndustryId);

    RETURN 0;

END
