using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace DAKI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            if (!WebSecurity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            ViewBag.Message = "";

            return View();
        }

        public ActionResult About()
        {
            if (!WebSecurity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            if (!WebSecurity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            ViewBag.Message = "Your contact page.";

            return View();
        }

        
    }
}
