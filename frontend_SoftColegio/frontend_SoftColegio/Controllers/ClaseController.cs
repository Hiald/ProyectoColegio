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
using System.Linq;
using System.Web;

namespace frontend_SoftColegio.Controllers
{
    public class ClaseController : Controller
    {
        //CRUD para administrar
        [SecuritySession]
        public ActionResult claseGestion()
        {
            int irolusuario = UtlAuditoria.ObtenerTipoUsuario();
            int idusuario = UtlAuditoria.ObtenerIdUsuario();
            ViewBag.GrolUsuario = irolusuario;
            ViewBag.GIDUsuario = idusuario;
            return View();
        }

        [SecuritySession]
        public async Task<ActionResult> Clase(int idcurso, string nombreCurso)
        {
            //int irolusuario = UtlAuditoria.ObtenerTipoUsuario();            
            try
            {
                var objResultado = new object();
                int ItipoUsuario = UtlAuditoria.ObtenerTipoUsuario();
                int Iidusuario = UtlAuditoria.ObtenerIdUsuario();
                List<edClase> loenClase = new List<edClase>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(MvcApplication.wsRouteSchoolBackend);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Reslistarusu = await client.GetAsync("api/clase/wsListarClaseCurso?widcurso=" + idcurso + "&widusuario=" + Iidusuario + "&wtipousuario=" + ItipoUsuario);
                    if (Reslistarusu.IsSuccessStatusCode)
                    {
                        var rwsapilu = Reslistarusu.Content.ReadAsAsync<string>().Result;
                        loenClase = JsonConvert.DeserializeObject<List<edClase>>(rwsapilu);
                    }
                }
                ViewBag.GrolUsuario = ItipoUsuario;
                ViewBag.Lista = loenClase;
                ViewBag.Curso = nombreCurso;
                ViewBag.IdCurso = idcurso;
                return View();
            }
            catch (Exception ex)
            {
                //UtlLog.toWrite(UtlConstantes.PizarraWEB, UtlConstantes.LogNamespace_PizarraWEB, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                return Json(ex);
            }
        }

        //Lista las clases del alumno
        public async Task<JsonResult> ListarClase(int idcurso)
        {
            //int irolusuario = UtlAuditoria.ObtenerTipoUsuario();            
            try
            {
                var objResultado = new object();
                int ItipoUsuario = UtlAuditoria.ObtenerTipoUsuario();
                int Iidusuario = UtlAuditoria.ObtenerIdUsuario();
                List<edClase> loenClase = new List<edClase>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(MvcApplication.wsRouteSchoolBackend);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Reslistarusu = await client.GetAsync("api/clase/wsListarClaseCurso?widcurso=" + idcurso + "&widusuario=" + Iidusuario + "&wtipousuario=" + ItipoUsuario);
                    if (Reslistarusu.IsSuccessStatusCode)
                    {
                        var rwsapilu = Reslistarusu.Content.ReadAsAsync<string>().Result;
                        loenClase = JsonConvert.DeserializeObject<List<edClase>>(rwsapilu);
                    }
                }
                return Json(loenClase);
            }
            catch (Exception ex)
            {
                //UtlLog.toWrite(UtlConstantes.PizarraWEB, UtlConstantes.LogNamespace_PizarraWEB, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                return Json(ex);
            }
        }

        //ACTIVO : muestra la vista y obtiene los cursos por cada grado y nivel : alumno
        [SecuritySession]
        public async Task<ActionResult> Curso()
        {
            try
            {
                var objResultado = new object();
                int ItipoUsuario = UtlAuditoria.ObtenerTipoUsuario();
                int Iidusuario = UtlAuditoria.ObtenerIdUsuario();
                int idgrado = UtlAuditoria.ObtenerIdGrado();
                int idnivel = UtlAuditoria.ObtenerIdNivel();
                List<edClase> loenClase = new List<edClase>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(MvcApplication.wsRouteSchoolBackend);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Reslistarusu = await client.
                        GetAsync("api/clase/wsListarCurso?wsidgrado=" + idgrado 
                        + "&wsidnivel=" + idnivel + "&wstipousuario=" + ItipoUsuario + "&widusuario=" + Iidusuario);
                    if (Reslistarusu.IsSuccessStatusCode)
                    {
                        var rwsapilu = Reslistarusu.Content.ReadAsAsync<string>().Result;
                        loenClase = JsonConvert.DeserializeObject<List<edClase>>(rwsapilu);

                    }
                }
                ViewBag.GrolUsuario = ItipoUsuario;
                ViewBag.Lista = loenClase;
                return View();
            }
            catch (Exception ex)
            {
                //UtlLog.toWrite(UtlConstantes.PizarraWEB, UtlConstantes.LogNamespace_PizarraWEB, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                return Json(ex);
            }

        }

        //ACTIVO : inserta la clase : admin
        [HttpPost]
        public async Task<JsonResult> InsertarClaseGestion(int idgrado, int isemana, string nombre, string descripcion
            , string rutaenlace, string rutavideo, int categoria, string imagenruta
            , int orden, string imagen)
        {
            try
            {
                var objResultado = new object();
                string wfechaRegistro = DateTime.Now.ToString();
                int idGenerado = -1;
                Int16 estado = 1;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(MvcApplication.wsRouteSchoolBackend);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage ResRegistrarCuenta = await client.GetAsync("api/clase/wsInsertarClase?wsidcurso=" + idgrado
                        + "&wssemana=" + isemana + "&wsnombre=" + nombre + "&wsdescripcion=" + descripcion
                        + "&wsrutaenlace=" + rutaenlace + "&wsrutavideo=" + rutavideo + "&wscategoria=" + categoria
                        + "&wsimagen=" + imagen + "&wsimagenruta=" + imagenruta + "&wsorden=" + orden
                        + "&wsestado=" + estado + "&wfecharegistro=" + wfechaRegistro);

                    if (ResRegistrarCuenta.IsSuccessStatusCode)
                    {
                        var rwsapi = ResRegistrarCuenta.Content.ReadAsAsync<string>().Result;
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

        [HttpPost]
        public async Task<ActionResult> RegistrarClaseAdminDocente(int widgrado, int wisemana
            , string wnombre, string wdescripcion, string wrutaenlace, string wrutavideo
            , int wcategoria, string wimagenruta, int worden
            , IEnumerable<HttpPostedFileBase> wimagen)
        {
            try
            {
                var objResultado = new object();
                int idGenerado = -1;
                Random random = new Random();
                const string alfabeto = "abcdefghijklmnopqrstuvwxyz0123456789";
                var releaseUris = new List<string>();
                string wfechaRegistro = DateTime.Now.ToString();
                Int16 estado = 1;
                foreach (var file in wimagen)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        string sTipoImagen = System.IO.Path.GetFileName(file.ContentType);
                        string sRandom = new string(Enumerable.Repeat(alfabeto, 10).Select(s => s[random.Next(s.Length)]).ToArray());
                        string sRutaLocal = System.IO.Path.Combine(Server.MapPath("~/clasevideo/"), sRandom + "." + sTipoImagen);

                        string sRutaServidor = "/clasevideo/" + sRandom + "." + sTipoImagen;
                        // sRuta es para la bd                        
                        file.SaveAs(sRutaLocal);
                        releaseUris.Add(sRutaServidor);
                    }
                }
                string valorimg1 = "/Content/imgpago/vacio.png";
                string valorimg2 = "/Content/imgpago/vacio.png";
                if (releaseUris.Count == 1)
                {
                    valorimg1 = releaseUris[0];
                }
                if (releaseUris.Count == 2)
                {
                    valorimg1 = releaseUris[0];
                    valorimg2 = releaseUris[1];
                }
                
                    //si el pago es individual
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(MvcApplication.wsRouteSchoolBackend);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            HttpResponseMessage ResRegistrarCuenta = await 
                            client.GetAsync("api/clase/wsInsertarClase?wsidcurso=" + widgrado
                         + "&wssemana=" + wisemana + "&wsnombre=" + wnombre + "&wsdescripcion=" 
                         + wdescripcion + "&wsrutaenlace=" + wrutaenlace + "&wsrutavideo=" 
                         + wrutavideo + "&wscategoria=" + wcategoria + "&wsimagen=" + wimagen 
                         + "&wsimagenruta=" + wimagenruta + "&wsorden=" + worden
                         + "&wsestado=" + estado + "&wfecharegistro=" + wfechaRegistro);

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
                return RedirectToAction("pagoGestion", "pago");
            }
            catch (Exception ex)
            {
                //UtlLog.toWrite(UtlConstantes.PizarraWEB, UtlConstantes.LogNamespace_PizarraWEB, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                return RedirectToAction("index", "Login", new { ivalorsesion = 1, valorlogin = ex.Message });
            }

        }

        [HttpPost]
        public async Task<JsonResult> ActualizarClaseGestion(int widclase, int wsidgrado, string wsnombre, string wsdescripcion
                , string wsrutaenlace, string wsrutavideo, int wscategoria, string wsimagen, string wsimagenruta
                , int wsorden)
        {
            try
            {
                var objResultado = new object();
                string wfechaRegistro = DateTime.Now.ToString();
                int idGenerado = -1;
                Int16 wsestado = 1;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(MvcApplication.wsRouteSchoolBackend);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage ResRegistrarCuenta = await client.GetAsync("api/clase/wsActualizarClase?widclase=" + widclase
                        + "&wsidgrado=" + wsidgrado + "&wsnombre=" + wsnombre + "&wsdescripcion=" + wsdescripcion
                        + "&wsrutaenlace=" + wsrutaenlace + "&wsrutavideo=" + wsrutavideo + "&wscategoria=" + wscategoria
                        + "&wsimagen=" + wsimagen + "&wsimagenruta=" + wsimagenruta
                        + "&wsorden=" + wsorden + "&wsestado=" + wsestado + "&fecharegistro=" + wfechaRegistro);

                    if (ResRegistrarCuenta.IsSuccessStatusCode)
                    {
                        var rwsapi = ResRegistrarCuenta.Content.ReadAsAsync<string>().Result;
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

        [HttpPost]
        public async Task<JsonResult> EliminarClaseGestion(int widclase)
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
                    HttpResponseMessage ResRegistrarCuenta = await client.GetAsync("api/clase/wsEliminarClase?wsidclase=" + widclase);

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

        //ACTIVO : lista las clases por cada curso : alumno, admin
        [HttpPost]
        public async Task<JsonResult> ListarClaseGeneral(int idcurso)
        {
            try
            {
                var objResultado = new object();
                int ItipoUsuario = UtlAuditoria.ObtenerTipoUsuario();
                int IidUsuario = UtlAuditoria.ObtenerIdUsuario();
                List<edClase> loenClase = new List<edClase>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(MvcApplication.wsRouteSchoolBackend);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Reslistarusu = await client.GetAsync("api/clase/wsListarClaseCurso?widcurso=" + idcurso + "&widusuario=" + IidUsuario + "&wtipousuario=" + ItipoUsuario);
                    if (Reslistarusu.IsSuccessStatusCode)
                    {
                        var rwsapilu = Reslistarusu.Content.ReadAsAsync<string>().Result;
                        loenClase = JsonConvert.DeserializeObject<List<edClase>>(rwsapilu);
                    }
                }

                objResultado = new
                {
                    PageStart = 1,
                    pageSize = 100,
                    SearchText = string.Empty,
                    ShowChildren = UtlConstantes.bValorTrue,
                    iTotalRecords = loenClase.Count,
                    iTotalDisplayRecords = 1,
                    aaData = loenClase
                };
                return Json(objResultado);
            }
            catch (Exception ex)
            {
                //UtlLog.toWrite(UtlConstantes.PizarraWEB, UtlConstantes.LogNamespace_PizarraWEB, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                return Json(ex);
            }

        }

        [HttpPost]
        public async Task<JsonResult> ListarClaseGestion(int idgrado, int idnivel)
        {
            try
            {
                var objResultado = new object();
                int ItipoUsuario = UtlAuditoria.ObtenerTipoUsuario();
                int Iidusuario = UtlAuditoria.ObtenerIdUsuario();
                List<edClase> loenClase = new List<edClase>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(MvcApplication.wsRouteSchoolBackend);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Reslistarusu = await client.
                        GetAsync("api/clase/wsListarCurso?wsidgrado=" + idgrado + "&wsidnivel=" + idnivel 
                        + "&wstipousuario=" + ItipoUsuario  + "&widusuario=" + Iidusuario);
                    if (Reslistarusu.IsSuccessStatusCode)
                    {
                        var rwsapilu = Reslistarusu.Content.ReadAsAsync<string>().Result;
                        loenClase = JsonConvert.DeserializeObject<List<edClase>>(rwsapilu);
                    }
                }

                return Json(loenClase);
            }
            catch (Exception ex)
            {
                //UtlLog.toWrite(UtlConstantes.PizarraWEB, UtlConstantes.LogNamespace_PizarraWEB, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                return Json(ex);
            }

        }

    }
}