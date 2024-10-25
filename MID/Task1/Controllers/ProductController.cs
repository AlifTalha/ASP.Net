using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
       public ActionResult create()
        {
            return View();
        }
        public ActionResult List()
        {
            return View();
        }
    }
}