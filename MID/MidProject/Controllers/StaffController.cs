using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalDemo.Controllers
{
    [SupportStaffAccess]
    public class StaffController : Controller
    {
        public ActionResult StaffDashboard()
        {
            return View();
        }
    }
}