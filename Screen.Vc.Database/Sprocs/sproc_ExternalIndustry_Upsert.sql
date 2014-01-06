CREATE PROCEDURE [dbo].[sproc_ExternalIndustry_Upsert]
(@industryTvp as ExternalIndustryType readonly)
AS
BEGIN
    MERGE ExternalIndustry AS T
    USING @industryTvp AS S 
    ON (T.ExternalId = S.ExternalId) 
    WHEN MATCHED 
        THEN UPDATE SET T.Name = S.Name, T.DisplayName = S.DisplayName, T.Updated = GETUTCDATE()
    WHEN NOT MATCHED  
        THEN INSERT (ExternalId, Name, DisplayName, Updated) 
		     VALUES (S.ExternalId, S.Name, S.DisplayName, GETUTCDATE());

    RETURN 0
END
