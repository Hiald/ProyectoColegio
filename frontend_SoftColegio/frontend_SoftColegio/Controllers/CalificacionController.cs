using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using frontendED;
using frontendUtil;
using frontend_SoftColegio.Filters;

namespace frontend_SoftColegio.Controllers
{
    public class CalificacionController : Controller
    {
        // vista admin y profesor
        [SecuritySession]
        public ActionResult calificacionGestion()
        {
            int irolusuario = UtlAuditoria.ObtenerTipoUsuario();
            ViewBag.GrolUsuario = irolusuario;
            return View();
        }

        // vista usuario
        [SecuritySession]
        public ActionResult Calificacion()
        {
            int irolusuario = UtlAuditoria.ObtenerTipoUsuario();
            int idGrado = UtlAuditoria.ObtenerIdGrado();
            int idNivel = UtlAuditoria.ObtenerIdNivel();
            ViewBag.GrolUsuario = irolusuario;
            ViewBag.GidNivel = idNivel;
            ViewBag.GidGrado = idGrado;
            return View();
        }

        // vista usuario
        [SecuritySession]
        public ActionResult Libreta()
        {
            int irolusuario = UtlAuditoria.ObtenerTipoUsuario();
            int idGrado = UtlAuditoria.ObtenerIdGrado();
            int idNivel = UtlAuditoria.ObtenerIdNivel();
            ViewBag.GrolUsuario = irolusuario;
            ViewBag.GidNivel = idNivel;
            ViewBag.GidGrado = idGrado;
            return View();
        }

        // ACTIVO: lista las calificaciones por cada usuario o en general: alumno
        [HttpPost]
        public async Task<JsonResult> ListarCalificacionGestion(int tiponota, int idnota, int isemana)
        {
            try
            {
                var objResultado = new object();
                int ItipoUsuario = UtlAuditoria.ObtenerTipoUsuario();
                int IDUsuario = UtlAuditoria.ObtenerIdUsuario();
                int IvalorTipoUsuario = IDUsuario;
                if (ItipoUsuario == 1 || ItipoUsuario == 2)
                {
                    IvalorTipoUsuario = 0;
                }
                List<edCalificacion> loenCalificacion = new List<edCalificacion>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(MvcApplication.wsRouteSchoolBackend);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage ResWSApi = await client.
                        GetAsync("api/Calificacion/wsListarCalificacion?widusuario="
                            + IvalorTipoUsuario + "&wtiponota=" + tiponota + "&wnota=" + idnota + "&wisemana=" + isemana);
                    if (ResWSApi.IsSuccessStatusCode)
                    {
                        var rwsapilu = ResWSApi.Content.ReadAsAsync<string>().Result;
                        loenCalificacion = JsonConvert.DeserializeObject<List<edCalificacion>>(rwsapilu);
                    }
                }

                objResultado = new
                {
                    PageStart = 1,
                    pageSize = 100,
                    SearchText = string.Empty,
                    ShowChildren = UtlConstantes.bValorTrue,
                    iTotalRecords = loenCalificacion.Count,
                    iTotalDisplayRecords = 1,
                    aaData = loenCalificacion
                };
                return Json(objResultado);
            }
            catch (Exception ex)
            {
                //UtlLog.toWrite(UtlConstantes.PizarraWEB, UtlConstantes.LogNamespace_PizarraWEB, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                return Json(ex);
            }

        }

        // ACTIVO: lista las calificaciones por cada usuario o en general: admin
        [HttpPost]
        public async Task<JsonResult> ListarCalificacion(int idUsuario, int tiponota, int idnota)
        {
            try
            {
                var objResultado = new object();
                List<edCalificacion> loenCalificacion = new List<edCalificacion>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(MvcApplication.wsRouteSchoolBackend);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage ResWSApi = await client.
                        GetAsync("api/Calificacion/wsListarCalificacion?widusuario="
                            + idUsuario + "&wtiponota=" + tiponota + "&wnota=" + idnota);
                    if (ResWSApi.IsSuccessStatusCode)
                    {
                        var rwsapilu = ResWSApi.Content.ReadAsAsync<string>().Result;
                        loenCalificacion = JsonConvert.DeserializeObject<List<edCalificacion>>(rwsapilu);
                    }
                }

                objResultado = new
                {
                    PageStart = 1,
                    pageSize = 100,
                    SearchText = string.Empty,
                    ShowChildren = UtlConstantes.bValorTrue,
                    iTotalRecords = loenCalificacion.Count,
                    iTotalDisplayRecords = 1,
                    aaData = loenCalificacion
                };
                return Json(objResultado);
            }
            catch (Exception ex)
            {
                //UtlLog.toWrite(UtlConstantes.PizarraWEB, UtlConstantes.LogNamespace_PizarraWEB, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                return Json(ex);
            }

        }

        // ACTIVO: actualiza la nota de las TAREAS o EJERCICIOS: docente
        [HttpPost]
        public async Task<JsonResult> ActualizarNota(int idarchivodetalle, string inota, string observacion,
                                    int itiponota, Int16 iestado, int idusuario)
        {
            try
            {
                var objResultado = new object();
                int idGenerado = -1;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(MvcApplication.wsRouteSchoolBackend);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage ResRegistrarCuenta = await client.GetAsync("api/calificacion/wsActualizarNota?wsidarchivodetalle=" + idarchivodetalle
                        + "&wsinota=" + inota + "&wsobservacion=" + observacion + "&wsidusuario=" + idusuario
                        + "&wsitiponota=" + itiponota + "&wsiestado=" + iestado);

                    if (ResRegistrarCuenta.IsSuccessStatusCode)
                    {
                        var rwsapi = ResRegistrarCuenta.Content.ReadAsAsync<string>().Result;
                        idGenerado = int.Parse(rwsapi);

                        if (idGenerado == -1)
                        {
                            //error
                            objResultado = new
                            {
                                iResultado = -1,
                                iResultadoIns = "Ha ocurrido un error, intentalo nuevamente. Error: BCK"
                            };
                            return Json(objResultado);
                        }
                    }
                }

                objResultado = new
                {
                    iResultado = 1,
                    iResultadoIns = "Registrado correctamente"
                };
                return Json(objResultado);
            }
            catch (Exception ex)
            {
                //UtlLog.toWrite(UtlConstantes.PizarraWEB, UtlConstantes.LogNamespace_PizarraWEB, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                return Json(ex);
            }

        }


    }
}