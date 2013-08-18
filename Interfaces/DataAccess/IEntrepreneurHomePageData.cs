using Screen.Vc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screen.Vc.Interfaces.DataAccess
{
    public interface IEntrepreneurHomePageData : IAzureStoredProcedure
    {
        Int64 EntrepreneurId { get; set; }
        IList<CompanySummary> CompanySummary { get; }
        IDictionary<Int64, UnreadQuestionsSummary> UnreadQuestionsSummary { get; }
        IDictionary<Int64, UnreadCommentsSummary> UnreadCommentsSummary { get; }
        IDictionary<Int64, IList<MatchingInvestor> > MatchingInvestors { get; }
    }
}
