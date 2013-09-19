using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Screen.Vc.WebRole.Source
{
    public class AccountConstants
    {
        public const string AccountType = "AccountType";
    }

    public class ControllerNames
    {
        public const string Entrepreneur = "Entrepreneur";
        public const string Home = "Home";
        public const string Investor = "Investor";
        public const string Worker = "Worker";
        public const string Company = "Company";
    }

    public class Actions
    {
        public const string Index = "Index";
        public const string About = "About";
        public const string Contact = "Contact";
    }

    public class EntrepreneurActions
    {
        public const string NoCompanies = "NoCompanies";
        public const string RegisterCompany = "RegisterCompany";
    }

    public class CompanyActions
    {
        public const string Company = "Company";
    }

    public class UserProfileActions
    {
        public const string UserProfileAjax = "UserProfileAjax";
    }

    public class CompanyViews
    {
        public const string Register = "Register";
    }

}