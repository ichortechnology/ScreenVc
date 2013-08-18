CREATE PROCEDURE [dbo].[sproc_CompanySummary_Get]
    @EntrepreneurId         bigint
AS

    SELECT  c.CompanyId, Name, LogoUrl
    FROM    [dbo].Company c INNER JOIN
            [dbo].EntrepreneurCompany ec ON (c.CompanyId= ec.CompanyId)
    WHERE   ec.EntrepreneurId = @EntrepreneurId

RETURN 0
