using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestTask.OnlineContract.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Калькулятор";

            return View();
        }

        [Authorize]
        public ActionResult Auction()
        {
            ViewBag.Title = "Аукцион";

            return View();
        }
    }
}
