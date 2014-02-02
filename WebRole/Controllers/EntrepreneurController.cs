using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Screen.Vc.WebRole.Models;
using WebMatrix.WebData;
using Screen.Vc.DataAccess;
using Screen.Vc.WebRole.Filters;
using Screen.Vc.Interfaces.DataAccess;
using Screen.Vc.Model;
using Screen.Vc.WebRole.Adapters;

namespace Screen.Vc.WebRole.Controllers
{
    [InitializeSimpleMembership]
    public class EntrepreneurController : Controller
    {
        public EntrepreneurController(IEntrepreneurHomePageData fetchHomePageData) 
        {
            m_fetchHomePageData = fetchHomePageData;
        }

        //
        // GET: /Entrepreneur/
        //
        public ActionResult Index()
        {
            EntrepreneurHomePage    model = null;
            ActionResult            view;

            if (WebSecurity.IsAuthenticated)
            {
                m_fetchHomePageData.EntrepreneurId = WebSecurity.CurrentUserId;
                m_fetchHomePageData.Execute();
                
                model = new EntrepreneurHomePage();
                model = EntrepreneurAdapters.Convert(m_fetchHomePageData);
                view = View(model);
            }
            else
            {
                view = View("IndexWithNoCompanies");
            }
            return View(model);
        }

        //
        // Get: /Entrepreneut/RegisterCompany
        public ActionResult RegisterCompany()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterCompany(RegisterCompanyPage model)
        {
            // TODO: Decide on what the model looks like.
            // TODO: Handle registration success and error differently.
            return View();
        }

        //
        // Get: /Entrepreneur/BrowseProjects
        public ActionResult BrowseProjects()
        {
            return View();
        }

        //
        // Get: /Entrepreneur/UserProfile
        public ActionResult UserProfile()
        {
            return View();
        }

        //
        // POST: /Entrepreneur/UserProfile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserProfile(EntrepreneurProfile model, string returnUrl )
        {
            return View();
        }

        //
        // Get: /Entrepreneur/UserProfile
        public ActionResult UserProfileAjax()
        {
            return View();
        }

        //
        // POST: /Entrepreneur/UserProfileAjax
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserProfileAjax(EntrepreneurProfile model, string returnUrl )
        {
            return View();
        }

        //
        // POST: /Entrepreneur/_UserProfileUpdate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _UserProfileUpdate(EntrepreneurProfile model, string returnUrl )
        {
            return Json(model);
        }

        #region Private member variables

        private IEntrepreneurHomePageData           m_fetchHomePageData;

        #endregion Private member variables
    }
}
