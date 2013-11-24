CREATE PROCEDURE [dbo].[sproc_UnreadQuestionsSummary_Get]
    @EntrepreneurId         bigint
AS
    SELECT  c.CompanyId, count(1) as UnreadQuestionCount
    FROM    AttributeQuestion aq 
            INNER JOIN  CompanyAttribute ca         ON aq.CompanyAttributeId = ca.CompanyAttributeId
            INNER JOIN  Company c                   ON ca.CompanyId = c.CompanyId
            INNER JOIN  EntrepreneurCompany ec      ON ec.CompanyId = c.CompanyId
    WHERE   ec.EntrepreneurId = @EntrepreneurId
    GROUP BY c.CompanyId

RETURN 0
