CREATE PROCEDURE [dbo].[sproc_ExternalIndustry_Upsert]
(@industryTvp as ExternalIndustryType readonly)
AS
BEGIN
    MERGE ExternalIndustry AS T
    USING @industryTvp AS S 
    ON (T.ExternalId = S.ExternalId) 
    WHEN MATCHED 
        THEN UPDATE SET T.Name = S.Name, T.DisplayName = S.DisplayName, T.Updated = GETDATE()
    WHEN NOT MATCHED  
        THEN INSERT (ExternalId, Name, DisplayName) 
		     VALUES (S.ExternalId, S.Name, S.DisplayName);

    RETURN 0
END
