//using Screen.Vc.DataAccess;
using Screen.Vc.Interfaces.DataAccess;
using Screen.Vc.WebRole.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Screen.Vc.WebRole.Adapters
{
    public static class EntrepreneurAdapters
    {
        public static EntrepreneurHomePage Convert(IEntrepreneurHomePageData src)
        {
            var                             target = new EntrepreneurHomePage();

            // We support only 1 company today. Hence we use 0th element directly.
            // FUTURE: Add support for multiple companies.
            if (src.CompanySummary == null || src.CompanySummary.Count <= 0)
            {
                target.CompanySummary = null;
                target.MatchingInvestors = null;
                target.UnreadCommentsSummary = null;
                target.UnreadQuestionsSummary = null;
            }
            else
            {
                var companyId = src.CompanySummary[0].CompanyId;
                target.CompanySummary = Convert(src.CompanySummary[0]);
                target.CompanySummary.Id = companyId;
                target.MatchingInvestors = src.MatchingInvestors[companyId];
                target.UnreadCommentsSummary = src.UnreadCommentsSummary[companyId];
                target.UnreadQuestionsSummary = src.UnreadQuestionsSummary[companyId];
            }
            return target;
        }

        public static Screen.Vc.WebRole.Models.CompanySummary Convert(Model.CompanySummary src)
        {
            CompanySummary      target = new CompanySummary()   {
                                                                    Id = src.CompanyId, 
                                                                    LogoUrl = src.LogoUrl,
                                                                    Name = src.Name
                                                                };


            return target;
        }
    }
}