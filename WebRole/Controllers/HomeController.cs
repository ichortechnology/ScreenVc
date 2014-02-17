using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Screen.Vc.WebRole.Models;
using WebMatrix.WebData;
using System.Web.Security;
using Screen.Vc.WebRole.Filters;

namespace Screen.Vc.WebRole.Controllers
{
    [InitializeSimpleMembership]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new HomeModel();
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

    }
}
