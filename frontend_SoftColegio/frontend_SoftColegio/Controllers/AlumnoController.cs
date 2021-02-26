using frontend_SoftColegio.Filters;
using frontendUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace frontend_SoftColegio.Controllers
{
    public class AlumnoController : Controller
    {
        // GET: Alumno
        [SecuritySession]
        public ActionResult Asistencia()
        {
            int irolusuario = UtlAuditoria.ObtenerTipoUsuario();
            ViewBag.GrolUsuario = irolusuario;
            return View();
        }
    }
}