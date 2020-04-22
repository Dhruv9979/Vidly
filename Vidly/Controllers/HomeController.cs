using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Vidly.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        /*VaryByParam: if this action takes mmore than 1 parameter and the ouptut changes based on thsi parameter
         * we cancache each output seperately.
         * "*" is used for multiple parameters, which means for any combination of these parameters we will have a
         * different version in a cache.
         * 
         * OutputCache is used only when a page is heavy and take a time to load. But never assume and use it.
         * Only use this when there a performance issues related to a page, after testing them for performance
         * and it benefit from caching it.
         * 
         * To disable caching you can use OutputCache(Duration = 0, VaryByParam = "*", NoStore = true)
         */
        //[OutputCache(Duration = 50, Location = OutputCacheLocation.Server, VaryByParam = "*")]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}