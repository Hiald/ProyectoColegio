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
using System.Text;
using System.Web;
using System.Linq;

namespace frontend_SoftColegio.Controllers
{
    public class PagoController : Controller
    {
        // alumno
        [SecuritySession]
        public ActionResult pago()
        {
            int irolusuario = UtlAuditoria.ObtenerTipoUsuario();
            int idusuario = UtlAuditoria.ObtenerIdUsuario();
           
            ViewBag.GrolUsuario = irolusuario;
            ViewBag.Gidusuario = idusuario;
            return View();
        }



        [SecuritySession]
        public ActionResult Index()
        {
            int irolusuario = UtlAuditoria.ObtenerTipoUsuario();
            int idGrado = UtlAuditoria.ObtenerIdGrado();
            int idNivel = UtlAuditoria.ObtenerIdNivel();
            ViewBag.GrolUsuario = irolusuario;
            ViewBag.GidNivel = idNivel;
            ViewBag.GidGrado = idGrado;
            return View();
        }

        [SecuritySession]
        public ActionResult Reporte()
        {
            int idusuario = UtlAuditoria.ObtenerIdUsuario();
            int irolusuario = UtlAuditoria.ObtenerTipoUsuario();
            int idGrado = UtlAuditoria.ObtenerIdGrado();
            int idNivel = UtlAuditoria.ObtenerIdNivel();
            ViewBag.GrolUsuario = irolusuario;
            ViewBag.GidNivel = idNivel;
            ViewBag.GidGrado = idGrado;
            ViewBag.Gidusuario = idusuario;
            return View();
        }

        // admin
        [SecuritySession]
        public ActionResult pagoGestion()
        {
            int idusuario = UtlAuditoria.ObtenerIdUsuario();
            int irolusuario = UtlAuditoria.ObtenerTipoUsuario();
            ViewBag.Gidusuario = idusuario;
            ViewBag.GrolUsuario = irolusuario;
            return View();
        }

        public string CrearPassword(int longitud)
        {
            //string caracteres = "abcdefghjkmnopqrstuvwxyzABCDEFGHJKMNOPQRSTUVWXYZ1234567890";
            string caracteres = "abcdefghjkmnopqrstuvwxyz1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < longitud--)
            {
                res.Append(caracteres[rnd.Next(caracteres.Length)]);
            }
            return res.ToString();
        }

        //ACTIVO : actualiza la clave del usuario seleccionado
        [HttpPost]
        public async Task<JsonResult> ActualizarUsuarioClave(int widusuario, string wnuevaclave)
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
                    HttpResponseMessage ResRegistrarCuenta = await client.
                            GetAsync("api/pago/wsActualizarUsuarioClave?wsidusuario=" + widusuario
                        + "&wsnuevaclave=" + wnuevaclave);

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

        //ACTIVO : lista los pagos de los usuarios y admin
        [HttpPost]
        public async Task<JsonResult> ListarPago(string wcuenta, int widusuario, int widnivel, int widgrado
                    , int widcurso, Int16 wivigente, string wnombre, string wfechaini, string wfechafin
                    , int wimes, int wianio)
        {
            try
            {
                var objResultado = new object();
                List<edPago> loenClase = new List<edPago>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(MvcApplication.wsRouteSchoolBackend);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Reslistarusu = await client.GetAsync("api/pago/wsListarPago?wcuenta=" + wcuenta
                    + "&widusuario=" + widusuario + "&widnivel=" + widnivel + "&widgrado=" + widgrado
                    + "&widcurso=" + widcurso + "&wivigente=" + wivigente + "&wnombre=" + wnombre
                    + "&wfechaini=" + wfechaini + "&wfechafin=" + wfechafin + "&wimes=" + wimes
                    + "&wianio=" + wianio);
                    if (Reslistarusu.IsSuccessStatusCode)
                    {
                        var rwsapilu = Reslistarusu.Content.ReadAsAsync<string>().Result;
                        loenClase = JsonConvert.DeserializeObject<List<edPago>>(rwsapilu);
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

        //ACTIVO : lista los pagos de los usuarios y admin
        [HttpPost]
        public async Task<JsonResult> ListarPagoDetalle(int widpago, int wbactivo, int wbestado
                                                        , string wfechaini, string wfechafin)
        {
            try
            {
                var objResultado = new object();
                List<edPago> loenClase = new List<edPago>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(MvcApplication.wsRouteSchoolBackend);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Reslistarusu = await client.GetAsync("api/pago/wListarPagoDetalle?idpago=" + widpago
                        + "&bactivo=" + wbactivo + "&bestado=" + wbestado
                        + "&fechaini=" + wfechaini + "&fechafin=" + wfechafin);
                    if (Reslistarusu.IsSuccessStatusCode)
                    {
                        var rwsapilu = Reslistarusu.Content.ReadAsAsync<string>().Result;
                        loenClase = JsonConvert.DeserializeObject<List<edPago>>(rwsapilu);
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

        //ACTIVO : registra los pagos tanto como admin y como usuario
        [HttpPost]
        public async Task<ActionResult> RegistrarPago(int widpago, int widpagodetalle, int widusuario
                /*, int widnivel, int widgrado*/
                , int widcurso, string woperacion, int slcPagoIndiMult
                , int wtipopago, int wtipomoneda, string wdescripcion, int wmes, int wanio
                , string whora, decimal wmonto, IEnumerable<HttpPostedFileBase> FRutaImagenes
                , string wfecha_ini_pago, Int16 wbestado, string wfr, string wff
                , string chk1, string chk2, string chk3, string chk4, string chk5
                , string chk6, string chk7, string chk8)
        {
            try
            {
                var objResultado = new object();
                int idGenerado = -1;
                Random random = new Random();
                const string alfabeto = "abcdefghijklmnopqrstuvwxyz0123456789";
                var releaseUris = new List<string>();
                foreach (var file in FRutaImagenes)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        string sTipoImagen = System.IO.Path.GetFileName(file.ContentType);
                        string sRandom = new string(Enumerable.Repeat(alfabeto, 10).Select(s => s[random.Next(s.Length)]).ToArray());
                        string sRutaLocal = System.IO.Path.Combine(Server.MapPath("~/Content/imgpago/"), sRandom + "." + sTipoImagen);

                        string sRutaServidor = "/Content/imgpago/" + sRandom + "." + sTipoImagen;
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

                if (slcPagoIndiMult == 1)
                {
                    //si el pago es individual
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(MvcApplication.wsRouteSchoolBackend);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        HttpResponseMessage ResRegistrarCuenta = await client.
                        GetAsync("api/pago/wsRegistrarPago?widpago=" + widpago + "&widpagodetalle=" + widpagodetalle
                        + "&widusuario=" + widusuario + "&widnivel=" + /*widnivel*/ 1 + "&widgrado=" + 1 /*widgrado*/
                        + "&widcurso=" + widcurso + "&woperacion=" + woperacion + "&wtipopago=" + wtipopago
                        + "&wtipomoneda=" + wtipomoneda + "&wdescripcion=" + wdescripcion + "&wmes=" + wmes
                        + "&wanio=" + wanio + "&whora=" + whora + "&wmonto=" + wmonto + "&wimg_ruta_1=" + valorimg1
                        + "&wimg_ruta_2=" + valorimg2 + "&wfecha_ini_pago=" + wfecha_ini_pago
                        + "&wbestado=" + wbestado + "&wfr=" + wfr + "&wff=" + wff);

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
                }
                else
                {
                    //si el pago es multiple
                    if (chk1 == "on")
                    {
                        await PagoMultiple(widpago, widpagodetalle, widusuario
                        /*int widnivel, int widgrado, */
                        , widcurso, woperacion, slcPagoIndiMult
                        , wtipopago, wtipomoneda, wdescripcion, 1, wanio
                        , whora, wmonto, valorimg1, valorimg2, wfecha_ini_pago
                        , wbestado, wfr, wff);
                    }

                    if (chk2 == "on")
                    {
                        await PagoMultiple(widpago, widpagodetalle, widusuario
                        /*int widnivel, int widgrado, */
                        , widcurso, woperacion, slcPagoIndiMult
                        , wtipopago, wtipomoneda, wdescripcion, 2, wanio
                        , whora, wmonto, valorimg1, valorimg2, wfecha_ini_pago
                        , wbestado, wfr, wff);
                    }
                    if (chk3 == "on")
                    {
                        await PagoMultiple(widpago, widpagodetalle, widusuario
                        /*int widnivel, int widgrado, */
                        , widcurso, woperacion, slcPagoIndiMult
                        , wtipopago, wtipomoneda, wdescripcion, 3, wanio
                        , whora, wmonto, valorimg1, valorimg2, wfecha_ini_pago
                        , wbestado, wfr, wff);
                    }
                    if (chk4 == "on")
                    {
                        await PagoMultiple(widpago, widpagodetalle, widusuario
                        /*int widnivel, int widgrado, */
                        , widcurso, woperacion, slcPagoIndiMult
                        , wtipopago, wtipomoneda, wdescripcion, 4, wanio
                        , whora, wmonto, valorimg1, valorimg2, wfecha_ini_pago
                        , wbestado, wfr, wff);
                    }
                    if (chk5 == "on")
                    {
                        await PagoMultiple(widpago, widpagodetalle, widusuario
                        /*int widnivel, int widgrado, */
                        , widcurso, woperacion, slcPagoIndiMult
                        , wtipopago, wtipomoneda, wdescripcion, 5, wanio
                        , whora, wmonto, valorimg1, valorimg2, wfecha_ini_pago
                        , wbestado, wfr, wff);
                    }
                    if (chk6 == "on")
                    {
                        await PagoMultiple(widpago, widpagodetalle, widusuario
                        /*int widnivel, int widgrado, */
                        , widcurso, woperacion, slcPagoIndiMult
                        , wtipopago, wtipomoneda, wdescripcion, 6, wanio
                        , whora, wmonto, valorimg1, valorimg2, wfecha_ini_pago
                        , wbestado, wfr, wff);
                    }
                    if (chk7 == "on")
                    {
                        await PagoMultiple(widpago, widpagodetalle, widusuario
                        /*int widnivel, int widgrado, */
                        , widcurso, woperacion, slcPagoIndiMult
                        , wtipopago, wtipomoneda, wdescripcion, 7, wanio
                        , whora, wmonto, valorimg1, valorimg2, wfecha_ini_pago
                        , wbestado, wfr, wff);
                    }
                    if (chk8 == "on")
                    {
                        await PagoMultiple(widpago, widpagodetalle, widusuario
                        /*int widnivel, int widgrado, */
                        , widcurso, woperacion, slcPagoIndiMult
                        , wtipopago, wtipomoneda, wdescripcion, 8, wanio
                        , whora, wmonto, valorimg1, valorimg2, wfecha_ini_pago
                        , wbestado, wfr, wff);
                    }
                }
                objResultado = new
                {
                    iResultado = 1,
                    iResultadoIns = "Registrado correctamente"
                };
                return RedirectToAction("pago", "pago");
            }
            catch (Exception ex)
            {
                //UtlLog.toWrite(UtlConstantes.PizarraWEB, UtlConstantes.LogNamespace_PizarraWEB, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                return RedirectToAction("index", "Login", new { ivalorsesion = 1, valorlogin = ex.Message });
            }

        }

        //ACTIVO : registra los pagos tanto como admin y como usuario
        [HttpPost]
        public async Task<ActionResult> RegistrarPagoAdministrador(int widpago, int widpagodetalle, int widusuario
                /*, int widnivel, int widgrado*/
                , int widcurso, string woperacion, int slcPagoIndiMult
                , int wtipopago, int wtipomoneda, string wdescripcion, int wmes, int wanio
                , string whora, decimal wmonto, IEnumerable<HttpPostedFileBase> FRutaImagenes
                , string wfecha_ini_pago, Int16 wbestado, string wfr, string wff
                , string chk1, string chk2, string chk3, string chk4, string chk5
                , string chk6, string chk7, string chk8)
        {
            try
            {
                var objResultado = new object();
                int idGenerado = -1;
                Random random = new Random();
                const string alfabeto = "abcdefghijklmnopqrstuvwxyz0123456789";
                var releaseUris = new List<string>();
                foreach (var file in FRutaImagenes)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        string sTipoImagen = System.IO.Path.GetFileName(file.ContentType);
                        string sRandom = new string(Enumerable.Repeat(alfabeto, 10).Select(s => s[random.Next(s.Length)]).ToArray());
                        string sRutaLocal = System.IO.Path.Combine(Server.MapPath("~/Content/imgpago/"), sRandom + "." + sTipoImagen);

                        string sRutaServidor = "/Content/imgpago/" + sRandom + "." + sTipoImagen;
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

                if (slcPagoIndiMult == 1)
                {
                    //si el pago es individual
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(MvcApplication.wsRouteSchoolBackend);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        HttpResponseMessage ResRegistrarCuenta = await client.
                        GetAsync("api/pago/wsRegistrarPago?widpago=" + widpago + "&widpagodetalle=" + widpagodetalle
                        + "&widusuario=" + widusuario + "&widnivel=" + /*widnivel*/ 1 + "&widgrado=" + 1 /*widgrado*/
                        + "&widcurso=" + widcurso + "&woperacion=" + woperacion + "&wtipopago=" + wtipopago
                        + "&wtipomoneda=" + wtipomoneda + "&wdescripcion=" + wdescripcion + "&wmes=" + wmes
                        + "&wanio=" + wanio + "&whora=" + whora + "&wmonto=" + wmonto + "&wimg_ruta_1=" + valorimg1
                        + "&wimg_ruta_2=" + valorimg2 + "&wfecha_ini_pago=" + wfecha_ini_pago
                        + "&wbestado=" + wbestado + "&wfr=" + wfr + "&wff=" + wff);

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
                }
                else
                {
                    //si el pago es multiple
                    if (chk1 == "on")
                    {
                        await PagoMultiple(widpago, widpagodetalle, widusuario
                        /*int widnivel, int widgrado, */
                        , widcurso, woperacion, slcPagoIndiMult
                        , wtipopago, wtipomoneda, wdescripcion, 1, wanio
                        , whora, wmonto, valorimg1, valorimg2, wfecha_ini_pago
                        , wbestado, wfr, wff);
                    }

                    if (chk2 == "on")
                    {
                        await PagoMultiple(widpago, widpagodetalle, widusuario
                        /*int widnivel, int widgrado, */
                        , widcurso, woperacion, slcPagoIndiMult
                        , wtipopago, wtipomoneda, wdescripcion, 2, wanio
                        , whora, wmonto, valorimg1, valorimg2, wfecha_ini_pago
                        , wbestado, wfr, wff);
                    }
                    if (chk3 == "on")
                    {
                        await PagoMultiple(widpago, widpagodetalle, widusuario
                        /*int widnivel, int widgrado, */
                        , widcurso, woperacion, slcPagoIndiMult
                        , wtipopago, wtipomoneda, wdescripcion, 3, wanio
                        , whora, wmonto, valorimg1, valorimg2, wfecha_ini_pago
                        , wbestado, wfr, wff);
                    }
                    if (chk4 == "on")
                    {
                        await PagoMultiple(widpago, widpagodetalle, widusuario
                        /*int widnivel, int widgrado, */
                        , widcurso, woperacion, slcPagoIndiMult
                        , wtipopago, wtipomoneda, wdescripcion, 4, wanio
                        , whora, wmonto, valorimg1, valorimg2, wfecha_ini_pago
                        , wbestado, wfr, wff);
                    }
                    if (chk5 == "on")
                    {
                        await PagoMultiple(widpago, widpagodetalle, widusuario
                        /*int widnivel, int widgrado, */
                        , widcurso, woperacion, slcPagoIndiMult
                        , wtipopago, wtipomoneda, wdescripcion, 5, wanio
                        , whora, wmonto, valorimg1, valorimg2, wfecha_ini_pago
                        , wbestado, wfr, wff);
                    }
                    if (chk6 == "on")
                    {
                        await PagoMultiple(widpago, widpagodetalle, widusuario
                        /*int widnivel, int widgrado, */
                        , widcurso, woperacion, slcPagoIndiMult
                        , wtipopago, wtipomoneda, wdescripcion, 6, wanio
                        , whora, wmonto, valorimg1, valorimg2, wfecha_ini_pago
                        , wbestado, wfr, wff);
                    }
                    if (chk7 == "on")
                    {
                        await PagoMultiple(widpago, widpagodetalle, widusuario
                        /*int widnivel, int widgrado, */
                        , widcurso, woperacion, slcPagoIndiMult
                        , wtipopago, wtipomoneda, wdescripcion, 7, wanio
                        , whora, wmonto, valorimg1, valorimg2, wfecha_ini_pago
                        , wbestado, wfr, wff);
                    }
                    if (chk8 == "on")
                    {
                        await PagoMultiple(widpago, widpagodetalle, widusuario
                        /*int widnivel, int widgrado, */
                        , widcurso, woperacion, slcPagoIndiMult
                        , wtipopago, wtipomoneda, wdescripcion, 8, wanio
                        , whora, wmonto, valorimg1, valorimg2, wfecha_ini_pago
                        , wbestado, wfr, wff);
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

        public async Task<int> PagoMultiple(int widpago, int widpagodetalle, int widusuario
                , /*int widnivel, int widgrado, */ int widcurso, string woperacion, int slcPagoIndiMult
                , int wtipopago, int wtipomoneda, string wdescripcion, int wmes, int wanio
                , string whora, decimal wmonto, string img1, string img2
                , string wfecha_ini_pago, Int16 wbestado, string wfr, string wff)
        {
            var objResultado = new object();
            int idGenerado = -1;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(MvcApplication.wsRouteSchoolBackend);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage ResRegistrarCuenta = await client.
                GetAsync("api/pago/wsRegistrarPago?widpago=" + widpago + "&widpagodetalle=" + widpagodetalle
                + "&widusuario=" + widusuario + "&widnivel=" + 1 /*widnivel*/ + "&widgrado=" + 1 /*widgrado*/
                + "&widcurso=" + widcurso + "&woperacion=" + woperacion + "&wtipopago=" + wtipopago
                + "&wtipomoneda=" + wtipomoneda + "&wdescripcion=" + wdescripcion + "&wmes=" + wmes
                + "&wanio=" + wanio + "&whora=" + whora + "&wmonto=" + wmonto + "&wimg_ruta_1=" + img1
                + "&wimg_ruta_2=" + img2 + "&wfecha_ini_pago=" + wfecha_ini_pago
                + "&wbestado=" + wbestado + "&wfr=" + wfr + "&wff=" + wff);

                if (ResRegistrarCuenta.IsSuccessStatusCode)
                {
                    var rwsapi = ResRegistrarCuenta.Content.ReadAsAsync<string>().Result;
                    idGenerado = int.Parse(rwsapi);
                }
                return idGenerado;
            }
        }

        //ACTIVO : lista un valor para el modo usuario si faltan pocos tiempo al dia de pago
        [HttpPost]
        public async Task<JsonResult> NotificarPago(int widusuario, int wimes, string wfechaacceso
                                                , string wfechavalidar)
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
                    HttpResponseMessage ResRegistrarCuenta = await client.
                            GetAsync("api/pago/wsNotificarPago?widusuario=" + widusuario
                        + "&imes=" + wimes + "&wfechaacceso=" + wfechaacceso + "&wfechavalidar=" + wfechavalidar);

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

        //ACTIVO : lista los pagos de los usuarios y admin
        [HttpPost]
        public async Task<JsonResult> RptListarSumPago(int widnivel, int widgrado, int widcurso
                            , string wfechaini, string wfechafin, int wmes, int wanio)
        {
            try
            {
                var objResultado = new object();
                List<edPago> loenClase = new List<edPago>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(MvcApplication.wsRouteSchoolBackend);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Reslistarusu = await client.
                        GetAsync("api/pago/wsRptListarSumaPagos?widnivel=" + widnivel
                        + "&widgrado=" + widgrado + "&widcurso=" + widcurso
                        + "&wfechaini=" + wfechaini + "&wfechafin=" + wfechafin
                        + "&wmes=" + wmes + "&wanio=" + wanio);
                    if (Reslistarusu.IsSuccessStatusCode)
                    {
                        var rwsapilu = Reslistarusu.Content.ReadAsAsync<string>().Result;
                        loenClase = JsonConvert.DeserializeObject<List<edPago>>(rwsapilu);
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

        //ACTIVO : lista los usuarios totales para el modo admin
        [HttpPost]
        public async Task<JsonResult> ListarUsuarioTotal(int widnivel, int widgrado, int widcurso)
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
                    HttpResponseMessage ResRegistrarCuenta = await client.
                            GetAsync("api/pago/wsListarUsuarioTotal?widnivel=" + widnivel
                        + "&widgrado=" + widgrado + "&widcurso=" + widcurso);

                    if (ResRegistrarCuenta.IsSuccessStatusCode)
                    {
                        var rwsapi = ResRegistrarCuenta.Content.ReadAsAsync<string>().Result;
                        idGenerado = int.Parse(rwsapi);
                        if (idGenerado == -1 || idGenerado == 0)
                        {
                            //error
                            objResultado = new
                            {
                                iResultado = idGenerado,
                                iResultadoIns = "Ha ocurrido un error, intentalo nuevamente. Error: BCK"
                            };
                            return Json(objResultado);
                        }
                    }
                }

                objResultado = new
                {
                    iResultado = idGenerado,
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

        //ACTIVO : lista los pagos de los usuarios y admin
        [HttpPost]
        public async Task<JsonResult> ListarPagoPendiente(int wsidusuario, int wsidnivel, int wsidgrado
                                                             , int wsidcurso, int wsbactivo)
        {
            try
            {
                var objResultado = new object();
                List<edPago> loenClase = new List<edPago>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(MvcApplication.wsRouteSchoolBackend);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Reslistarusu = await client.
                        GetAsync("api/pago/wsListarPagoPendiente?wsidusuario=" + wsidusuario
                        + "&wsidnivel=" + wsidnivel + "&wsidgrado=" + wsidgrado
                        + "&wsidcurso=" + wsidcurso + "&wsbactivo=" + wsbactivo);
                    if (Reslistarusu.IsSuccessStatusCode)
                    {
                        var rwsapilu = Reslistarusu.Content.ReadAsAsync<string>().Result;
                        loenClase = JsonConvert.DeserializeObject<List<edPago>>(rwsapilu);
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
        public async Task<JsonResult> rptListarUsuarioPagos(string wusuario, string wfechaini, string wfechafin, int widcurso)
        {
            try
            {                
                if (UtlAuditoria.ObtenerTipoUsuario() == 3)
                {
                    wusuario = UtlAuditoria.ObtenerApellidoPaterno() + ' ' + UtlAuditoria.ObtenerApellidoMaterno() + ' ' + UtlAuditoria.ObtenerNombre();
                }

                var objResultado = new object();
                List<edPago> lenPago = new List<edPago>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(MvcApplication.wsRouteSchoolBackend);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Reslistarusu = await client.GetAsync("api/pago/wsRptListarUsuarioPagos?wsusuario=" +
                        wusuario + "&wsfechaini=" + wfechaini + "&wsfechafin=" + wfechafin + "&wsidcurso=" + widcurso);
                    if (Reslistarusu.IsSuccessStatusCode)
                    {
                        var rwsapilu = Reslistarusu.Content.ReadAsAsync<string>().Result;
                        lenPago = JsonConvert.DeserializeObject<List<edPago>>(rwsapilu);
                    }
                }

                objResultado = new
                {
                    PageStart = 1,
                    pageSize = 100,
                    SearchText = string.Empty,
                    ShowChildren = UtlConstantes.bValorTrue,
                    iTotalRecords = lenPago.Count,
                    iTotalDisplayRecords = 1,
                    aaData = lenPago
                };
                return Json(objResultado);
            }
            catch (Exception ex)
            {
                //UtlLog.toWrite(UtlConstantes.PizarraWEB, UtlConstantes.LogNamespace_PizarraWEB, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                return Json(ex);
            }

        }


        //VISTA PARA QUE EL USUARIO (ALUMNO) PUEDA VER SOLO SUS REPORTES
        [SecuritySession]
        public ActionResult Usuario()
        {
            int irolusuario = UtlAuditoria.ObtenerTipoUsuario();
            ViewBag.GrolUsuario = irolusuario;
            return View();
        }

        //VISTA PARA QUE EL ADMINISTRADOR PUEDA VER LOS PAGOS DE TODOS LOS USUARIOS (ALUMNOS)
        [SecuritySession]
        public ActionResult ReporteUsuarioPago()
        {
            int irolusuario = UtlAuditoria.ObtenerTipoUsuario();
            ViewBag.GrolUsuario = irolusuario;
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> rptVentasTotales()
        {
            try
            {
                var objResultado = new object();
                List<edPago> lenPago = new List<edPago>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(MvcApplication.wsRouteSchoolBackend);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Reslistarusu = await client.GetAsync("api/pago/wsRptVentasTotales");
                    if (Reslistarusu.IsSuccessStatusCode)
                    {
                        var rwsapilu = Reslistarusu.Content.ReadAsAsync<string>().Result;
                        lenPago = JsonConvert.DeserializeObject<List<edPago>>(rwsapilu);
                    }
                }

                objResultado = new
                {
                    PageStart = 1,
                    pageSize = 100,
                    SearchText = string.Empty,
                    ShowChildren = UtlConstantes.bValorTrue,
                    iTotalRecords = lenPago.Count,
                    iTotalDisplayRecords = 1,
                    aaData = lenPago
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
