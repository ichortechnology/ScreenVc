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

        public ActionResult RegisterUser(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {
                    var userInfo = new { UserName = model.UserName, EmailAddress = model.EmailAddress, FirstName = model.UserName };
                    WebSecurity.CreateUserAndAccount(model.UserName, model.Password, userInfo);
                    WebSecurity.Login(model.UserName, model.Password);
                    return RedirectToAction("Index", "Entrpreneur");
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("error", "Sorry Some error occured.");
                }
            }

            // If we got this far, something failed, redisplay form
            return Json("true", JsonRequestBehavior.AllowGet);
        }
    }
}
