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
using System.Web;
using System.Linq;

namespace frontend_SoftColegio.Controllers
{
    public class ArchivoController : Controller
    {
        // admin y profesor gestion
        [SecuritySession]
        public ActionResult archivoGestion()
        {
            int irolusuario = UtlAuditoria.ObtenerTipoUsuario();
            int idusuario = UtlAuditoria.ObtenerIdUsuario();
            ViewBag.GrolUsuario = irolusuario;
            ViewBag.GIDUsuario = idusuario;
            return View();
        }

        // admin y profesor gestion
        [SecuritySession]
        public ActionResult archivoGestionAlumno()
        {
            int irolusuario = UtlAuditoria.ObtenerTipoUsuario();
            int idusuario = UtlAuditoria.ObtenerIdUsuario();
            ViewBag.GrolUsuario = irolusuario;
            ViewBag.GIDUsuario = idusuario;
            return View();
        }

        // alumno
        [SecuritySession]
        public ActionResult examen()
        {
            int irolusuario = UtlAuditoria.ObtenerTipoUsuario();
            ViewBag.GrolUsuario = irolusuario;
            return View();
        }

        // alumno
        [SecuritySession]
        public async Task<ActionResult> Tarea(int idclase)
        {
            try
            {
                var objResultado = new object();
                int ItipoUsuario = UtlAuditoria.ObtenerTipoUsuario();
                List<edArchivo> loenArchivo = new List<edArchivo>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(MvcApplication.wsRouteSchoolBackend);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Reslistarusu = await client.GetAsync("api/archivo/wsObtenerArchivo?widclase=" + idclase);
                    if (Reslistarusu.IsSuccessStatusCode)
                    {
                        var rwsapilu = Reslistarusu.Content.ReadAsAsync<string>().Result;
                        loenArchivo = JsonConvert.DeserializeObject<List<edArchivo>>(rwsapilu);
                    }
                }
                ViewBag.GrolUsuario = ItipoUsuario;
                ViewBag.Lista = loenArchivo;
                ViewBag.Gidclase = idclase;
                return View();
            }
            catch (Exception ex)
            {
                //UtlLog.toWrite(UtlConstantes.PizarraWEB, UtlConstantes.LogNamespace_PizarraWEB, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                return Json(ex);
            }

        }

        //06/02/2021 Lista los archivos que el profesor sube a la clase.
        public async Task<JsonResult> ListarArchivos(int idclase)
        {
            try
            {
                var objResultado = new object();
                int ItipoUsuario = UtlAuditoria.ObtenerTipoUsuario();
                List<edArchivo> loenArchivo = new List<edArchivo>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(MvcApplication.wsRouteSchoolBackend);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Reslistarusu = await client.GetAsync("api/archivo/wsObtenerArchivo?widclase=" + idclase);
                    if (Reslistarusu.IsSuccessStatusCode)
                    {
                        var rwsapilu = Reslistarusu.Content.ReadAsAsync<string>().Result;
                        loenArchivo = JsonConvert.DeserializeObject<List<edArchivo>>(rwsapilu);
                    }
                }
                return Json(loenArchivo);
            }
            catch (Exception ex)
            {
                //UtlLog.toWrite(UtlConstantes.PizarraWEB, UtlConstantes.LogNamespace_PizarraWEB, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                return Json(ex);
            }

        }








        // INACTIVO: registra las TAREAS o EJERCICIOS del profesor: admin, docente
        [HttpPost]
        public async Task<JsonResult> InsertarArchivoGestion(int idgrado, string nombre, string rutaenlace
                                                        , string rutavideo, string fechaini, string fechafin
                                                        , int itipoarchivo)
        {
            try
            {
                var objResultado = new object();
                string wfechaRegistro = DateTime.Now.ToString();
                int idGenerado = -1;
                int idusuario = UtlAuditoria.ObtenerIdUsuario();
                Int16 estado = 1;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(MvcApplication.wsRouteSchoolBackend);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage ResRegistrarArchivo = await client.GetAsync("api/archivo/wsInsertarArchivo?widclase=" + idgrado +
                        "&widusuario=" + idusuario + "&wnombre=" + nombre +
                        "&wrutaenlace=" + rutaenlace + "&wtipoarchivo=" + itipoarchivo + "&wfechainicio=" + fechaini +
                        "&wfechafin=" + fechafin + "&wrutavideo=" + rutavideo);

                    if (ResRegistrarArchivo.IsSuccessStatusCode)
                    {
                        var rwsapi = ResRegistrarArchivo.Content.ReadAsAsync<string>().Result;
                        idGenerado = int.Parse(rwsapi);

                        if (idGenerado == -1 || idGenerado == 0)
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

        // ACTIVO: registra las TAREAS o EJERCICIOS del profesor: admin, docente
        [HttpPost]
        public async Task<ActionResult> AgregarArchivo(int idgrado, string nombre, string rutaenlace
                                    , IEnumerable<HttpPostedFileBase> FRutaArchivo, string fechaini
                                    , string fechafin, int itipoarchivo, string descripcion)
        {
            try
            {
                var objResultado = new object();
                string wfechaRegistro = DateTime.Now.ToString();
                int idGenerado = -1;
                int idusuario = UtlAuditoria.ObtenerIdUsuario();
                Random random = new Random();
                const string alfabeto = "abcdefghijklmnopqrstuvwxyz0123456789";
                var releaseUris = new List<string>();
                foreach (var file in FRutaArchivo)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        string sTipoImagen = System.IO.Path.GetFileName(file.ContentType);
                        if (sTipoImagen == "plain")
                        {
                            sTipoImagen = "txt";
                        }
                        if (sTipoImagen == "msword")
                        {
                            sTipoImagen = "doc";
                        }
                        if (sTipoImagen == "vnd.ms-excel")
                        {
                            sTipoImagen = "xls";
                        }
                        if (sTipoImagen == "vnd.openxmlformats-officedocument.wordprocessingml.document" || sTipoImagen == "octet-stream")
                        {
                            sTipoImagen = "docx";
                        }
                        if (sTipoImagen == "vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                        {
                            sTipoImagen = "xlsx";
                        }

                        string sRandom = new string(Enumerable.Repeat(alfabeto, 10).Select(s => s[random.Next(s.Length)]).ToArray());
                        string sRutaLocal = System.IO.Path.Combine(Server.MapPath("~/clasearchivo/"), sRandom + "." + sTipoImagen);

                        string sRutaServidor = "/clasearchivo/" + sRandom + "." + sTipoImagen;
                        // sRuta es para la bd                        
                        file.SaveAs(sRutaLocal);
                        releaseUris.Add(sRutaServidor);
                    }
                }
                string valorarchivo = "/clasearchivo/vacio.png";
                if (releaseUris.Count == 1)
                {
                    valorarchivo = releaseUris[0];
                }
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(MvcApplication.wsRouteSchoolBackend);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage ResRegistrarArchivo = await client.GetAsync("api/archivo/wsInsertarArchivo?widclase=" + idgrado +
                        "&widusuario=" + idusuario + "&wnombre=" + nombre +
                        "&wrutaenlace=" + rutaenlace + "&wtipoarchivo=" + itipoarchivo + "&wfechainicio=" + fechaini +
                        "&wfechafin=" + fechafin + "&wdescripcion=" + descripcion + "&wrutaarchivo=" + valorarchivo);

                    if (ResRegistrarArchivo.IsSuccessStatusCode)
                    {
                        var rwsapi = ResRegistrarArchivo.Content.ReadAsAsync<string>().Result;
                        idGenerado = int.Parse(rwsapi);

                        if (idGenerado == -1)
                        {
                            //error
                            objResultado = new
                            {
                                iResultado = -1,
                                iResultadoIns = "Ha ocurrido un error, intentalo nuevamente. Error: BCK"
                            };
                            return RedirectToAction("claseGestion", "clase");
                        }
                    }
                }
                objResultado = new
                {
                    iResultado = 1,
                    iResultadoIns = "correcto"
                };
                return RedirectToAction("claseGestion", "clase");
            }
            catch (Exception ex)
            {
                //UtlLog.toWrite(UtlConstantes.PizarraWEB, UtlConstantes.LogNamespace_PizarraWEB, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                return Json(ex);
            }
        }

        // ACTIVO: obtiene las TAREAS o EJERCICIOS por cada clase: docente
        [HttpPost]
        public async Task<JsonResult> ListarArchivoEspecifico(int idclase)
        {
            try
            {
                var objResultado = new object();
                int ItipoUsuario = UtlAuditoria.ObtenerTipoUsuario();
                List<edArchivo> loenArchivo = new List<edArchivo>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(MvcApplication.wsRouteSchoolBackend);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Reslistarusu = await client.GetAsync("api/archivo/wsObtenerArchivo?widclase=" + idclase);
                    if (Reslistarusu.IsSuccessStatusCode)
                    {
                        var rwsapilu = Reslistarusu.Content.ReadAsAsync<string>().Result;
                        loenArchivo = JsonConvert.DeserializeObject<List<edArchivo>>(rwsapilu);
                    }
                }

                objResultado = new
                {
                    PageStart = 1,
                    pageSize = 100,
                    SearchText = string.Empty,
                    ShowChildren = UtlConstantes.bValorTrue,
                    iTotalRecords = loenArchivo.Count,
                    iTotalDisplayRecords = 1,
                    aaData = loenArchivo
                };
                return Json(objResultado);
            }
            catch (Exception ex)
            {
                //UtlLog.toWrite(UtlConstantes.PizarraWEB, UtlConstantes.LogNamespace_PizarraWEB, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                return Json(ex);
            }

        }

        // INACTIVO NO ME SIRVE
        [HttpPost]
        public async Task<JsonResult> ActualizarArchivoAlumno(int idarchivodetalle, string nota
                                         , string observacion, int tiponota)
        {
            try
            {
                var objResultado = new object();
                string wfechaRegistro = DateTime.Now.ToString();
                int idGenerado = -1;
                int idusuario = UtlAuditoria.ObtenerIdUsuario();
                Int16 estado = 1;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(MvcApplication.wsRouteSchoolBackend);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage ResRegistrarArchivo = await client.GetAsync("api/archivo/wsActualizarArchivoDetalle?widarchivodetalle=" + idarchivodetalle
                        + "&wnota=" + nota + "&wobservacion=" + observacion + "&widusuario=" + idusuario
                        + "&wtiponota=" + tiponota + "&westado=" + estado + "&wfecharegistro=" + wfechaRegistro);

                    if (ResRegistrarArchivo.IsSuccessStatusCode)
                    {
                        var rwsapi = ResRegistrarArchivo.Content.ReadAsAsync<string>().Result;
                        idGenerado = int.Parse(rwsapi);

                        if (idGenerado == -1 || idGenerado == 0)
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

        // ACTIVO: registra las TAREAS O EJERCICIOS por los alumnos: alumno
        [HttpPost]
        public async Task<ActionResult> InsertarArchivoAlumno(int pidcurso, string snombrecurso
                            , int idarchivo, IEnumerable<HttpPostedFileBase> imagen, string nota
                            , string observacion, string descripcion, string enlace)
        {
            try
            {
                var objResultado = new object();
                string wfechaRegistro = DateTime.Now.ToString();
                int idGenerado = -1;
                int idusuario = UtlAuditoria.ObtenerIdUsuario();
                Int16 estado = 1;
                Random random = new Random();
                const string alfabeto = "abcdefghijklmnopqrstuvwxyz0123456789";
                var releaseUris = new List<string>();
                foreach (var file in imagen)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        string sTipoImagen = System.IO.Path.GetFileName(file.ContentType);
                        if (sTipoImagen == "plain")
                        {
                            sTipoImagen = "txt";
                        }
                        if (sTipoImagen == "msword")
                        {
                            sTipoImagen = "doc";
                        }
                        if (sTipoImagen == "vnd.ms-excel")
                        {
                            sTipoImagen = "xls";
                        }
                        if (sTipoImagen == "vnd.openxmlformats-officedocument.wordprocessingml.document" || sTipoImagen == "octet-stream")
                        {
                            sTipoImagen = "docx";
                        }
                        if (sTipoImagen == "vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                        {
                            sTipoImagen = "xlsx";
                        }

                        string sRandom = new string(Enumerable.Repeat(alfabeto, 10).Select(s => s[random.Next(s.Length)]).ToArray());
                        string sRutaLocal = System.IO.Path.Combine(Server.MapPath("~/clasearchivoalumno/"), sRandom + "." + sTipoImagen);

                        string sRutaServidor = "/clasearchivoalumno/" + sRandom + "." + sTipoImagen;
                        // sRuta es para la bd                        
                        file.SaveAs(sRutaLocal);
                        releaseUris.Add(sRutaServidor);
                    }
                }
                string valorarchivo = "/clasearchivoalumno/vacio.png";
                if (releaseUris.Count == 1)
                {
                    valorarchivo = releaseUris[0];
                }

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(MvcApplication.wsRouteSchoolBackend);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage ResRegistrarArchivo = await client
                        .GetAsync("api/archivo/wsInsertarArchivoDetalle?widarchivo=" + idarchivo
                        + "&widusuario=" + idusuario + "&wimagen=" + valorarchivo + "&wnota=" + nota
                        + "&wobservacion=" + observacion + "&wdescripcion=" + descripcion
                        + "&wenlace=" + enlace);

                    if (ResRegistrarArchivo.IsSuccessStatusCode)
                    {
                        var rwsapi = ResRegistrarArchivo.Content.ReadAsAsync<string>().Result;
                        idGenerado = int.Parse(rwsapi);

                        if (idGenerado == -1)
                        {
                            //error
                            return RedirectToAction("Clase", "clase", new { idcurso = pidcurso, nombreCurso = snombrecurso });
                        }
                    }
                }
                return RedirectToAction("Clase", "clase", new { idcurso = pidcurso, nombreCurso = snombrecurso });
            }
            catch (Exception ex)
            {
                //UtlLog.toWrite(UtlConstantes.PizarraWEB, UtlConstantes.LogNamespace_PizarraWEB, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                return Json(ex);
            }

        }

        // ACTIVO: lista las propias TAREAS O EJERCICIOS propios subidos: alumno
        [HttpPost]
        public async Task<JsonResult> ListarArchivoAlumno(int idarchivo)
        {
            try
            {
                var objResultado = new object();
                int IdUsuario = UtlAuditoria.ObtenerIdUsuario();
                List<edArchivo> loenArchivo = new List<edArchivo>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(MvcApplication.wsRouteSchoolBackend);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Reslistarusu = await client.GetAsync("api/archivo/wsListarArchivoDetalle?widarchivo=" + idarchivo + "&widusuario=" + IdUsuario);
                    if (Reslistarusu.IsSuccessStatusCode)
                    {
                        var rwsapilu = Reslistarusu.Content.ReadAsAsync<string>().Result;
                        loenArchivo = JsonConvert.DeserializeObject<List<edArchivo>>(rwsapilu);
                    }
                }

                objResultado = new
                {
                    PageStart = 1,
                    pageSize = 100,
                    SearchText = string.Empty,
                    ShowChildren = UtlConstantes.bValorTrue,
                    iTotalRecords = loenArchivo.Count,
                    iTotalDisplayRecords = 1,
                    aaData = loenArchivo
                };
                return Json(objResultado);
            }
            catch (Exception ex)
            {
                //UtlLog.toWrite(UtlConstantes.PizarraWEB, UtlConstantes.LogNamespace_PizarraWEB, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                return Json(ex);
            }

        }

        //lista los archivos en general : profe y admin
        [HttpPost]
        public async Task<JsonResult> ListarArchivoGeneral(int idclase, int idgrado, int idnivel, int idcurso)
        {
            try
            {
                var objResultado = new object();
                int IdUsuario = UtlAuditoria.ObtenerIdUsuario();
                List<edArchivo> loenArchivo = new List<edArchivo>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(MvcApplication.wsRouteSchoolBackend);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Reslistarusu = await client.GetAsync("api/archivo/wsListarArchivoGeneral?wsidclasea=" + 0 +
                        "&wdgradoa=" + idgrado + "&wdnivela=" + idnivel + "&wsdcursoa=" + idcurso);
                    if (Reslistarusu.IsSuccessStatusCode)
                    {
                        var rwsapilu = Reslistarusu.Content.ReadAsAsync<string>().Result;
                        loenArchivo = JsonConvert.DeserializeObject<List<edArchivo>>(rwsapilu);
                    }
                }

                objResultado = new
                {
                    PageStart = 1,
                    pageSize = 100,
                    SearchText = string.Empty,
                    ShowChildren = UtlConstantes.bValorTrue,
                    iTotalRecords = loenArchivo.Count,
                    iTotalDisplayRecords = 1,
                    aaData = loenArchivo
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