CREATE PROCEDURE [dbo].[sproc_MatchingInvestors_Get]
    @EntrepreneurId         bigint
AS
    SELECT  ci.CompanyId, ii.InvestorUserId, i.IndustryId, i.Name
    FROM                Company c 
            INNER JOIN  EntrepreneurCompany ec on c.CompanyId = ec.CompanyId
            INNER JOIN  CompanyIndustry ci ON c.CompanyId = ci.CompanyId
            INNER JOIN  Industry i ON ci.IndustryId = i.IndustryId
            INNER JOIN  InvestorIndustry ii on i.IndustryId = ii.IndustryId
    WHERE ec.EntrepreneurId = @EntrepreneurId
    ORDER BY ci.CompanyId

RETURN 0
