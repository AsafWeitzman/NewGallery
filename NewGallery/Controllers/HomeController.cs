using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewGallery.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            string user = (string)HttpContext.Session["Type"];
            
            if (user == null || user !="Admin")//!=admin
            {
                //
                if (user == "Customer")
                {
                    return RedirectToAction("IndexUserMode", "Home");
                }
                //
                return RedirectToAction("Login", "Account");
            }
            //
            if (user!=null && user != "Admin")//Customer
            {
                return RedirectToAction("Login", "Account");
            }
            //

            //if user is admin
            return View();


        }
        public ActionResult IndexUserMode()
        {
            //
            string user = (string)HttpContext.Session["Type"];


            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (user == "Customer")
            {
                return View();
            }
            if (user != null && user != "Admin")
            {
                return View();

            }
            if (user != null && user == "Admin")
            {
                return RedirectToAction("Index", "Home");//swap
            }


            return View();


        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            string user = (string)HttpContext.Session["Type"];

            if (user !="Admin")
            {
               return RedirectToAction("ContactUserMode", "Home");
            }
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult ContactUserMode()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Covid()
        {
            string user = (string)HttpContext.Session["Type"];

            if (user != "Admin")
            {
                return RedirectToAction("CovidUserMode", "Home");
            }
            ViewBag.Message = "Your covid page.";

            return View();
        }

        public ActionResult CovidUserMode()
        {
            
            return View();
        }
    }
}