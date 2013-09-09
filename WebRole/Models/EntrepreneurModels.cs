using Screen.Vc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Screen.Vc.WebRole.Models
{
    /// <summary>
    /// Enum representing each company attribute from Attribute table. The enum values must 
    /// match the values specified in the table.
    /// </summary>
    public enum Attribute : int
    {
        ExecutiveSummary = 1,
        
    }
    
    #region Helpful types used inside interface models

    public class Question
    {
        public string               Content { get; set; }
        public string               Response { get; set; }

        // TODO: Name this better?
        public Int64                AskedById { get; set; }
        public Int64                ResponderId { get; set; }
    }

    public class CompanySummary
    {
        public Int64                            Id { get; set; }
        public String                           Name { get; set; }
        public String                           LogoUrl { get; set; }
        public Int32                            PossibleInvestorCount { get; set; }
        public Int32                            InterestedInvestorCount { get; set; }
        public Int32                            ViewCount { get; set; }
        public Int32                            QuestionCount { get; set; }
        public Int32                            RequestCount { get; set; }
    }

    public class Company
    {
        public Int64                            Id { get; set; }
        public string                           Name { get; set; }
        public string                           LogoUrl { get; set; }
        public string                           TagLine { get; set; }
        public string                           Pitch30SecondsUrl { get; set; }
        public string                           ExecutiveSummary { get; set; }
        public string                           Faq { get; set; }
        public Dictionary<Attribute, Question>  AttributeQuestions;
    }

    #endregion Helpful types used inside interface models

    #region ViewModels

    public class EntrepreneurProfile
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public Address PrimaryAddress { get; set; }

    }

    public class EntrepreneurHomePage
    {
        public CompanySummary CompanySummary { get; set; }
        public UnreadQuestionsSummary UnreadQuestionsSummary { get; set; }
        public UnreadCommentsSummary UnreadCommentsSummary { get; set; }
        public IList<MatchingInvestor> MatchingInvestors { get; set; }
    }

    public class HomePage
    {
        public List<CompanySummary>    Companies { get; set; }
    }

    public class RegisterCompanyPage
    {
        //public 
    }

    #endregion ViewModels
}
