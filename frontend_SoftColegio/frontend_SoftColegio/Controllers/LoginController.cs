using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using frontendED;
using frontendUtil;

namespace frontend_SoftColegio.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index(int? ivalorsesion, string valorlogin)
        {
            if (ivalorsesion != 1 || ivalorsesion == null)
            {
                ivalorsesion = 0;
            }

            ViewBag.GIvalorError = valorlogin;
            ViewBag.GIvalorSesion = ivalorsesion;
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Loginusuario(string wusuario, string wclave)
        {
            try
            {
                var objResultado = new object();

                int idusuarioGenerado = -1;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(MvcApplication.wsRouteSchoolBackend);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Reslogueo = await client.GetAsync("api/usuario/wsObtenerAcceso?wusuario=" + wusuario + "&wclave=" + wclave);

                    if (Reslogueo.IsSuccessStatusCode)
                    {
                        var rwsapi = Reslogueo.Content.ReadAsAsync<string>().Result;
                        idusuarioGenerado = int.Parse(rwsapi);

                        if (idusuarioGenerado == -1 || idusuarioGenerado == 0)
                        {
                            //si la clave no es igual al correo
                            objResultado = new
                            {
                                iResultado = -3,
                                iResultadoIns = "El usuario o clave son incorrectos"
                            };
                            return Json(objResultado);
                        }
                    }
                }

                edUsuario oEnUsuario = new edUsuario();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(MvcApplication.wsRouteSchoolBackend);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Reslistarusu = await client.GetAsync("api/usuario/wsObtenerUsuario?wsidusuario=" + idusuarioGenerado);
                    if (Reslistarusu.IsSuccessStatusCode)
                    {
                        var rwsapilu = Reslistarusu.Content.ReadAsAsync<string>().Result;
                        oEnUsuario = JsonConvert.DeserializeObject<edUsuario>(rwsapilu);
                    }
                }

                Dictionary<string, string> DVariables = new Dictionary<string, string>();
                DVariables["IDUSUARIO"] = idusuarioGenerado.ToString();
                DVariables["IDNIVEL"] = oEnUsuario.idnivel.ToString();
                DVariables["IDGRADO"] = oEnUsuario.idgrado.ToString();
                DVariables["IDSEDE"] = oEnUsuario.idsede.ToString();
                DVariables["IDSECCION"] = oEnUsuario.idseccion.ToString();
                DVariables["NOMBRE"] = oEnUsuario.Snombres.ToString();
                DVariables["APELLIDOPARTERNO"] = oEnUsuario.SApellidoPaterno.ToString();
                DVariables["APELLIDOMATERNO"] = oEnUsuario.SApellidoMaterno.ToString();
                DVariables["CORREO"] = oEnUsuario.Scorreo.ToString();
                DVariables["TIPOUSUARIO"] = oEnUsuario.tipousuario.ToString();
                UtlAuditoria.SetSessionValues(DVariables);

                /* string pdip = UtlAuditoria.ObtenerDireccionIP();
                 string pdmac = UtlAuditoria.ObtenerDireccionMAC();
                 using (var client = new HttpClient())
                 {
                     client.BaseAddress = new Uri(MvcApplication.wsRoutepizarra);
                     client.DefaultRequestHeaders.Clear();
                     client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                     HttpResponseMessage Resregsesion = await client.GetAsync("api/usuario/APIRegistrarSesionUsuario?wsusuarioid="
                         + oEnUsuario.idnivel + "&wsdireccionip=" + pdip + "&wsdireccionmac=" + pdmac + "&wstipoconexion=" + 2);
                     if (Resregsesion.IsSuccessStatusCode)
                     {
                         var rwsrs = Resregsesion.Content.ReadAsAsync<string>().Result;
                     }
                 }*/

                objResultado = new
                {
                    iResultado = 1,
                    iResultadoIns = "Home/Index"
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
        public async Task<JsonResult> Crearusuario(int widnivel, int widgrado, int widsede, string wnombres, string wamaterno
                                                    , string wapaterno, int wtipousuario, string wusuario, string wclave)
        {
            try
            {
                var objResultado = new object();
                Int16 westado = 1;
                string wfechaRegistro = DateTime.Now.ToString();
                int idusuarioGenerado = -1;
                string wtoken = "vacio";
                string wgenero = "0";
                string wcorreo = "vacio";
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(MvcApplication.wsRouteSchoolBackend);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage ResRegistrarCuenta = await client.GetAsync("api/usuario/wsInsertarCuenta?widnivel=" + widnivel + "&widgrado=" + widgrado
                        + "&widsede=" + widsede + "&wnombres=" + wnombres + "&wamaterno=" + wamaterno + "&wapaterno=" + wapaterno
                        + "&wgenero=" + wgenero + "&wcorreo=" + wcorreo + "&westado=" + westado + "&wfechaRegistro=" + wfechaRegistro);


                    if (ResRegistrarCuenta.IsSuccessStatusCode)
                    {
                        var rwsapi = ResRegistrarCuenta.Content.ReadAsAsync<string>().Result;
                        idusuarioGenerado = int.Parse(rwsapi);

                        if (idusuarioGenerado == -1 || idusuarioGenerado == 0)
                        {
                            //error
                            objResultado = new
                            {
                                iResultado = -1,
                                iResultadoIns = "El usuario o clave son incorrectos"
                            };
                            return Json(objResultado);
                        }
                    }
                }

                int idcuentagenerada = 0;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(MvcApplication.wsRouteSchoolBackend);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage ResCrearCuenta = await client.GetAsync("api/usuario/wsInsertarUsuario?widusuario=" + idusuarioGenerado
                        + "&wtipousuario=" + wtipousuario + "&wusuario=" + wusuario + "&wclave=" + wclave + "&wtoken=" + wtoken
                        + "&westado=" + westado + "&wfechaRegistro=" + wfechaRegistro);
                    if (ResCrearCuenta.IsSuccessStatusCode)
                    {
                        var rwsapi = ResCrearCuenta.Content.ReadAsAsync<string>().Result;
                        idcuentagenerada = int.Parse(rwsapi);
                    }
                }

                objResultado = new
                {
                    iResultado = 1,
                    iResultadoIns = "Home/Index"
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
        public async Task<JsonResult> ActualizarUsuario(int widusuario, string wsusuario, string wsclave, int wstipousuario, int widnivel, int widgrado, int widsede, string wnombres, string wamaterno
                                                    , string wapaterno)
        {
            try
            {
                var objResultado = new object();
                Int16 westado = 1;
                string wfechaRegistro = DateTime.Now.ToString();
                string wgenero = "0";
                string wcorreo = "vacio";
                int idusuarioGenerado = -1;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(MvcApplication.wsRouteSchoolBackend);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Reswsru = await client.GetAsync("api/usuario/wsActualizarCuenta?widusuario=" + widusuario + "&widnivel=" + widnivel + "&widgrado=" + widgrado
                        + "&widsede=" + widsede + "&wnombres=" + wnombres + "&wamaterno=" + wamaterno + "&wapaterno=" + wapaterno
                        + "&wgenero=" + wgenero + "&wcorreo=" + wcorreo + "&westado=" + westado + "&wfechaRegistro=" + wfechaRegistro);

                    if (Reswsru.IsSuccessStatusCode)
                    {
                        var rwsapi = Reswsru.Content.ReadAsAsync<string>().Result;
                        idusuarioGenerado = int.Parse(rwsapi);

                        if (idusuarioGenerado == -1 || idusuarioGenerado == 0)
                        {
                            //error
                            objResultado = new
                            {
                                iResultado = -1,
                                iResultadoIns = "El usuario o clave son incorrectos"
                            };
                            return Json(objResultado);
                        }
                    }
                }

                int idcuentagenerada = 0;
                using (var client = new HttpClient())
                {
                    int wstipoproceso = 1;
                    client.BaseAddress = new Uri(MvcApplication.wsRouteSchoolBackend);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage ResCrearCuenta = await client.GetAsync("api/usuario/wsActualizarAcceso?wstipoproceso=" + wstipoproceso
                        + "&wsidusuario=" + idusuarioGenerado + "&wsdusuario=" + wsusuario + "&wsdclave=" + wsclave);
                    if (ResCrearCuenta.IsSuccessStatusCode)
                    {
                        var rwsapi = ResCrearCuenta.Content.ReadAsAsync<string>().Result;
                        idcuentagenerada = int.Parse(rwsapi);
                    }
                }

                objResultado = new
                {
                    iResultado = 1,
                    iResultadoIns = "registrado"
                };

                return Json(objResultado);
            }
            catch (Exception ex)
            {
                //UtlLog.toWrite(UtlConstantes.PizarraWEB, UtlConstantes.LogNamespace_PizarraWEB, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                throw ex;
            }
        }

        [HttpPost]
        public async Task<JsonResult> DesactivarUsuario(int widusuario)
        {
            try
            {
                var objResultado = new object();
                string wfechaRegistro = DateTime.Now.ToString();

                int idcuentagenerada = 0;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(MvcApplication.wsRouteSchoolBackend);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage ResCrearCuenta = await client.GetAsync("api/usuario/wsActualizarAcceso?wstipoproceso=" + 2
                        + "&wsidusuario=" + widusuario + "&wsdusuario=" + "vacio" + "&wsdclave=" + "vacio");
                    if (ResCrearCuenta.IsSuccessStatusCode)
                    {
                        var rwsapi = ResCrearCuenta.Content.ReadAsAsync<string>().Result;
                        idcuentagenerada = int.Parse(rwsapi);
                    }
                }

                objResultado = new
                {
                    iResultado = 1,
                    iResultadoIns = "registrado"
                };

                return Json(objResultado);
            }
            catch (Exception ex)
            {
                //UtlLog.toWrite(UtlConstantes.PizarraWEB, UtlConstantes.LogNamespace_PizarraWEB, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                throw ex;
            }
        }

        [HttpPost]
        public async Task<JsonResult> ListarUsuarioGestion(string usuario, int tipousuario)
        {
            try
            {
                var objResultado = new object();
                List<edUsuario> lenUsuario = new List<edUsuario>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(MvcApplication.wsRouteSchoolBackend);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Reslistarusu = await client.GetAsync("api/usuario/wsListarUsuario?wusuario=" + usuario + "&wtipousuario=" + tipousuario);
                    if (Reslistarusu.IsSuccessStatusCode)
                    {
                        var rwsapilu = Reslistarusu.Content.ReadAsAsync<string>().Result;
                        lenUsuario = JsonConvert.DeserializeObject<List<edUsuario>>(rwsapilu);
                    }
                }

                objResultado = new
                {
                    PageStart = 1,
                    pageSize = 100,
                    SearchText = string.Empty,
                    ShowChildren = UtlConstantes.bValorTrue,
                    iTotalRecords = lenUsuario.Count,
                    iTotalDisplayRecords = 1,
                    aaData = lenUsuario
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
        public JsonResult cerrarSesion()
        {
            var objResultado = new object();
            try
            {
                bool bResultado = UtlAuditoria.CerrarSession();
                if (bResultado)
                {
                    objResultado = new
                    {
                        iResultado = 1
                    };
                }
                else
                {
                    objResultado = new
                    {
                        iResultado = 2
                    };
                }
            }
            catch (Exception ex)
            {
                //UtlLog.toWrite(UtlConstantes.PizarraWEB, UtlConstantes.LogNamespace_PizarraWEB, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                throw ex;
            }
            return Json(objResultado);
        }

    }
}