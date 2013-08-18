using Screen.Vc.Model;
using Screen.Vc.Interfaces;
using Screen.Vc.Interfaces.DataAccess;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screen.Vc.DataAccess
{
    public class EntrepreneurHomePageData : AzureStoredProcedure, IEntrepreneurHomePageData
    {
        public EntrepreneurHomePageData(IConfigurationProvider configProvider, SqlConnection dbConnection) : base(configProvider, dbConnection)
        {
        }

        #region Public properties

        public Int64 EntrepreneurId { get; set; }

        public IList<CompanySummary> CompanySummary { get; protected set; }

        public IDictionary<Int64, UnreadQuestionsSummary> UnreadQuestionsSummary { get; protected set; }

        public IDictionary<Int64, UnreadCommentsSummary> UnreadCommentsSummary { get; protected set; }

        public IDictionary<Int64, IList<MatchingInvestor> > MatchingInvestors { get; protected set; }

        #endregion

        protected override string GetStoredProcedureName()
        {
            return StoredProcedureName.EntrepreneurHomePageDataGet;
        }

        protected override System.Data.SqlClient.SqlParameter[] GetParameters()
        {
            SqlParameter[] parameters = new SqlParameter[1];
 
            parameters[0] = new SqlParameter("@EntrepreneurId", SqlDbType.BigInt);
            parameters[0].Value =  EntrepreneurId;

            return parameters;
        }

        protected override void ParseData(SqlDataReader dataReader)
        {
            // ResultSet 0 contains CompanySummary
            CompanySummary = new List<CompanySummary>();
            while (dataReader.Read())
            {
                CompanySummary  companySummary = new CompanySummary() { 
                                                                        CompanyId = Convert.ToInt64(dataReader[ColumnName.CompanyId]),
                                                                        Name = Convert.ToString(dataReader[ColumnName.Name]),
                                                                        LogoUrl = Convert.ToString(dataReader[ColumnName.LogoUrl]) 
                                                                      };
                CompanySummary.Add(companySummary);
            }

            // ResultSet 1 contains UnreadQuestionsSummary
            dataReader.NextResult();
            UnreadQuestionsSummary = new Dictionary<Int64, UnreadQuestionsSummary>();
            while (dataReader.Read())
            {
                UnreadQuestionsSummary questionsSummary = new UnreadQuestionsSummary() { 
                                                                                            CompanyId = Convert.ToInt64(dataReader[ColumnName.CompanyId]),
                                                                                            UnreadQuestionCount = Convert.ToInt32(dataReader[ColumnName.UnreadQuestionCount]) 
                                                                                       };
                UnreadQuestionsSummary.Add(questionsSummary.CompanyId, questionsSummary);
            }

            // ResultSet 2 contains UnreadCommentsSummary
            dataReader.NextResult();
            UnreadCommentsSummary = new Dictionary<Int64, UnreadCommentsSummary>();
            while (dataReader.Read())
            {
                UnreadCommentsSummary commentsSummary = new UnreadCommentsSummary() { 
                                                                                        CompanyId = Convert.ToInt64(dataReader[ColumnName.CompanyId]),
                                                                                        UnreadCommentsCount = Convert.ToInt32(dataReader[ColumnName.UnreadCommentsCount]) 
                                                                                    };
                UnreadCommentsSummary.Add(commentsSummary.CompanyId, commentsSummary);
            }

            // ResultSet 3 contains MatchingInvestors
            dataReader.NextResult();
            MatchingInvestors = new Dictionary<Int64, IList<MatchingInvestor>>();
            while (dataReader.Read())
            {
                IList<MatchingInvestor>     investorList;
                MatchingInvestor investor = new MatchingInvestor() { 
                                                                        CompanyId = Convert.ToInt64(dataReader[ColumnName.CompanyId]),
                                                                        InvestorUserId = Convert.ToInt32(dataReader[ColumnName.InvestorUserId]),
                                                                        IndustryId = Convert.ToInt32(dataReader[ColumnName.IndustryId]),
                                                                        IndustryName = Convert.ToString(dataReader[ColumnName.Name])
                                                                   };
                if (!MatchingInvestors.TryGetValue(investor.CompanyId, out investorList))
                {
                    investorList = new List<MatchingInvestor>();
                    MatchingInvestors.Add(investor.CompanyId, investorList);
                }
                investorList.Add(investor);
            }
        }
    }
}

