CREATE PROCEDURE [dbo].[sproc_ExternalInvestor_Upsert]
(
    @investorTvp as ExternalInvestorType readonly
)
AS
BEGIN

    MERGE [dbo].[ExternalInvestor]  AS T
    USING @investorTvp              AS S 
    ON (T.ExternalId = S.ExternalId) 
    WHEN MATCHED 
        THEN UPDATE SET T.Name = S.Name, T.OnlineBioUrl = S.OnlineBioUrl, T.LinkedInUrl = S.LinkedInUrl, T.Updated = GETDATE() 
    WHEN NOT MATCHED BY TARGET 
        THEN INSERT (ExternalId, Name, OnlineBioUrl, LinkedInUrl) 
		     VALUES (S.ExternalId, S.Name, S.OnlineBioUrl, S.LinkedInUrl);

    RETURN 0;
END