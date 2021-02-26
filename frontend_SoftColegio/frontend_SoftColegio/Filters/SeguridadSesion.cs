using System.Web;
using System.Web.Routing;
using System.Web.Mvc;
using frontendUtil;


namespace frontend_SoftColegio.Filters
{
    public class SecuritySession : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool bValidar = UtlAuditoria.ValidarSession();

            if (bValidar)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary {
                            { "controller", "Login" },
                            { "action", "Index" },
                            { "ivalorsesion", 1 },
                            { "valorlogin", "vacio" }
                        });

            }
            base.OnActionExecuting(filterContext);
        }
    }

    public class SeguridadSessionAjax : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool bValidar = UtlAuditoria.ValidarSession();
            if (bValidar)
            {

                filterContext.Result = new JsonResult
                {
                    Data = new { iTipoResultado = -5, message = UtlConstantes.msgErrorSesion },
                    JsonRequestBehavior = JsonRequestBehavior.DenyGet
                };

            }
            base.OnActionExecuting(filterContext);
        }
    }
}