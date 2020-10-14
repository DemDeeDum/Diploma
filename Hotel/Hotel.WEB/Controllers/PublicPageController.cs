using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hotel.WEB.Controllers
{
    public class PublicController : Controller
    {
        // GET: Public
        //For welcome
        public ActionResult MainPage()
        {
            object obj = "WELCOME";
            return View(obj);
        }
        //For Not found errors
        public ActionResult ErrorPage()
        {
            object obj = "NOT FOUND";
            return View("MainPage",obj);
        }
        
        [Authorize (Roles="deleted")]
        public ActionResult Info()
        {
            return View();
        }

    }
}