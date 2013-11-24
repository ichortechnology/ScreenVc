CREATE PROCEDURE [dbo].[sproc_ExternalInvestorSource_GetByName]
    @sourceName nvarchar(50)
AS
    SELECT [ExternalInvestorSourceId], [SourceName], [Description], [Uri]
    FROM    dbo.ExternalInvestorSource
    WHERE SourceName = @sourceName

RETURN 0
