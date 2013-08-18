using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screen.Vc.DataAccess
{
    public class ConfigName
    {
        public const string RetryCount = "RetryCount";
        public const string SqlConnectionString = "SqlConnectionString";
        public const string SqlConnectMinBackOffInSeconds = "SqlConnectMinBackOffInSeconds";
        public const string SqlConnectMaxBackOffInSeconds = "SqlConnectMaxBackOffInSeconds";
        public const string SqlConnectDeltaBackOffInSeconds = "SqlConnectDeltaBackOffInSeconds";
        public const string SqlConnectTimeoutInSeconds = "SqlConnectTimeoutInSeconds";
    }

    public class StoredProcedureName
    {
        public const string EntrepreneurHomePageDataGet = "sproc_EntrepreneurHomePageData_Get";
    }

    public class ColumnName
    {
        public const string CompanyId = "CompanyId";
        public const string Name = "Name";
        public const string LogoUrl = "LogoUrl";
        public const string UnreadQuestionCount = "UnreadQuestionCount";
        public const string UnreadCommentsCount = "UnreadCommentsCount";
        public const string IndustryId = "IndustryId";
        public const string InvestorUserId = "InvestorUserId";
        //public const string 

    }

    public class ParameterName
    {
        public const string EntrepreneurId = "EntrepreneurId";
    }

}
