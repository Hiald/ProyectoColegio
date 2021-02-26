using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using frontend_SoftColegio.Filters;
using frontendUtil;

namespace frontend_SoftColegio.Controllers
{
    public class HomeController : Controller
    {
        [SecuritySession]
        public ActionResult Index()
        {
            ViewBag.MenuPrincipal = "active";
            int irolusuario = UtlAuditoria.ObtenerTipoUsuario();
            string susuario = UtlAuditoria.ObtenerNombre()
                    + " " + UtlAuditoria.ObtenerApellidoPaterno() + " " + UtlAuditoria.ObtenerApellidoMaterno();
            ViewBag.GrolUsuario = irolusuario;
            ViewBag.GNombreUsuario = susuario;
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