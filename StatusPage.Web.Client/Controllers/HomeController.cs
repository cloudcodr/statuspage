using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StatusPage.Data.Models;
using StatusPage.Data.Views;

namespace StatusPage.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View(new StatusPageView());
        }
    }
}