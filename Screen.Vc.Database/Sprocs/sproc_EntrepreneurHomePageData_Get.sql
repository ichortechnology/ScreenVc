CREATE PROCEDURE [dbo].[sproc_EntrepreneurHomePageData_Get]
    @EntrepreneurId         bigint
AS
    -- Result set 1 contains information about Companies

    exec sproc_CompanySummary_Get @EntrepreneurId

    -- Result set 2 contains count of Unread questions for each company

    exec [sproc_UnreadQuestionsSummary_Get] @EntrepreneurId

    -- Result set 3 contains count of Unread comments for each company.

    exec [sproc_UnreadCommentsSummary_Get] @EntrepreneurId

    -- Result set 4 contains list of investors matching company industry.

    exec sproc_MatchingInvestors_Get @EntrepreneurId

RETURN 0
