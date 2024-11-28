using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalDemo.Controllers
{
    [AdminAccess]
    public class AdminController : Controller
    {
        public ActionResult AdminDashboard()
        {

            return View();
        }
    }
}