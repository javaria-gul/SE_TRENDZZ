using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SE_TRENDZZ.Controllers
{
    
        public class HomeController : Controller
        {
            public ActionResult Index()
            {
                return View();
            }

            public ActionResult Gallery()
            {
                return View();
            }

            public ActionResult About()
            {
                return View();
            }
        }

    
}