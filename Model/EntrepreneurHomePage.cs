using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screen.Vc.Model
{
    public class CompanySummary
    {
        public Int64 CompanyId { get; set; }
        public string Name { get; set; }
        public string LogoUrl { get; set; }
    }

    public class UnreadQuestionsSummary
    {
        public Int64 CompanyId { get; set; }
        public Int32 UnreadQuestionCount { get; set; }
    }

    public class UnreadCommentsSummary
    {
        public Int64 CompanyId { get; set; }
        public Int32 UnreadCommentsCount { get; set; }
    }

    public class MatchingInvestor
    {
        public Int64 CompanyId { get; set; }
        public Int64 InvestorUserId { get; set; }
        public Int32 IndustryId { get; set; }
        public string IndustryName { get; set; }
    }
}
