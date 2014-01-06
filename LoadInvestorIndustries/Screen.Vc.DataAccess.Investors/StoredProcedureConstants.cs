using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screen.Vc.DataAccess.Investors
{
    public class ColumnNames
    {
        // Common column Names come up here.
        public static readonly string ExternalInvestorId = "ExternalInvestorId";
        public static readonly string ExternalIndustryId = "ExternalIndustryId";
        public static readonly string Name = "Name";
        public static readonly string Updated = "Updated";

        // ExternalInvestor specific column names.
        public static readonly string OnlineBioUrl = "OnlineBioUrl";
        public static readonly string LinkedInUrl = "LinkedInUrl";
        public static readonly string ExternalInvestorSourceId = "ExternalInvestorSourceId";

        // ExternalIndustry specific column names.
        public static readonly string DisplayName = "DisplayName";
    }

    public class DomainData
    {
        public const Int32 AngelListExternalInvestorSourceId = 1;
    }
}
