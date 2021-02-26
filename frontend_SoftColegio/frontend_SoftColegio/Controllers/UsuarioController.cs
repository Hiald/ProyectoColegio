using System.Web.Mvc;
using frontend_SoftColegio.Filters;
using frontendUtil;

namespace frontend_SoftColegio.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        [SecuritySession]
        public ActionResult Index()
        {
            int irolusuario = UtlAuditoria.ObtenerTipoUsuario();
            ViewBag.GrolUsuario = irolusuario;
            return View();
        }

    }
}