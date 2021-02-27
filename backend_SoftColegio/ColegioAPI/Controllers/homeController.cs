using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ColegioAPI.Controllers
{
    public class homeController : Controller
    {
        public ActionResult index()
        {
            return View();
        }
    }
}