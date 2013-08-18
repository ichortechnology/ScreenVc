using Screen.Vc.WebRole.Models;
using Screen.Vc.WebRole.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Screen.Vc.WebRole.Controllers
{
    public class CompanyController : Controller
    {
        //
        // GET: /Company/
        // Returns a list of companies.
        public ActionResult Index()
        {
            ActionResult        actionResult;
            var companyList = new List<Company>();
/*
            companyList.Add( new Company { Name = "Test Company 1", LogoUrl="/images/ScreenVcLogo.png" });
            companyList.Add( new Company { Name = "Test Company 2", LogoUrl="/images/ScreenVcLogo.png" });
            companyList.Add( new Company { Name = "Test Company 3", LogoUrl="/images/ScreenVcLogo.png" });
*/
            if (companyList.Count < 1)
            {
                actionResult = View(CompanyViews.Register);
            }
            else if (companyList.Count == 1)
            {
                int companyId = 1001;
                actionResult = Details(companyId);
            }
            else
            {
                actionResult = View(companyList);
            }

            return actionResult;
        }

        //
        // GET: /Company/Details/5
        // Returns information about a specific company.
        public ActionResult Details(int id)
        {
            var company =  new Company 
                            { 
                                Name = "Test Company 1", 
                                LogoUrl="/images/ScreenVcLogo.png",
                                TagLine = "Testing the heck out of company page",
                                Pitch30SecondsUrl = "http://www.youtube.com/watch?v=drGMuf69iW8",
                                ExecutiveSummary = "This is where executive summary is shown.",
                                Faq = "Frequently asked questions.. what else"
                            };
            return View(company);
        }

        //
        // GET: /Company/Register
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Company/Register
        [HttpPost]
        public ActionResult Register(FormCollection collection)
        {
            try
            {
                
                // TODO: Add insert logic here

                return RedirectToAction(CompanyActions.Company);
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Company/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Company/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // POST: /Company/Disable/5
        [HttpPost]
        public ActionResult Disable(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
