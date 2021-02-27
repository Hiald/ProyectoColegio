using System;
using System.Collections.Generic;
using System.Web.Http;
using Newtonsoft.Json;
using ColegioED;
using ColegioTD;

namespace ColegioAPI.Controllers
{
    public class usuarioController : ApiController
    {
        tdUsuario itdUsuario;

        [HttpGet]
        public int wsInsertarCuenta(int widnivel, int widgrado, int widsede, string wnombres, string wamaterno, string wapaterno, string wgenero
                                    , string wcorreo, Int16 westado, string wfechaRegistro, string wimagen)
        {
            int iresultado = -4;
            try
            {
                itdUsuario = new tdUsuario();
                iresultado = itdUsuario.tdInsertarCuenta(widnivel, widgrado, widsede, wnombres, wamaterno, wapaterno, wgenero
                                                        , wcorreo, westado, DateTime.Now, wimagen);
                return iresultado;
            }
            catch (Exception ex)
            {
                return iresultado;
            }
        }

        //23/01/2021
        [HttpGet]
        public int wsActualizarCuenta(int widusuario, int widnivel, int widgrado, int widsede, string wnombres, string wamaterno, string wapaterno, string wgenero
                                    , string wcorreo, Int16 westado, string wfechaRegistro)
        {
            int iresultado = -4;
            try
            {
                itdUsuario = new tdUsuario();
                iresultado = itdUsuario.tdActualizarCuenta(widusuario, widnivel, widgrado, widsede, wnombres, wamaterno, wapaterno, wgenero
                                                        , wcorreo, westado, DateTime.Now);
                return iresultado;
            }
            catch (Exception ex)
            {
                return iresultado;
            }
        }
        [HttpGet]
        public int wsInsertarUsuario(int widusuario, int wtipousuario, string wusuario, string wclave, string wtoken, Int16 westado, string wfechaRegistro)
        {
            int iresultado = -2;
            try
            {
                itdUsuario = new tdUsuario();
                iresultado = itdUsuario.tdInsertarUsuario(widusuario, wtipousuario, wusuario, wclave, wtoken, westado, DateTime.Now);
                return iresultado;
            }
            catch (Exception ex)
            {
                return iresultado;
            }
        }

        [HttpGet]
        public int wsObtenerAcceso(string wusuario, string wclave)
        {
            int iresultado = -1;
            try
            {
                itdUsuario = new tdUsuario();
                iresultado = itdUsuario.tdObtenerAcceso(wusuario, wclave);
                return iresultado;
            }
            catch (Exception ex)
            {
                return iresultado;
            }
        }

        [HttpGet]
        public string wsObtenerUsuario(int wsidusuario)
        {
            edUsuario enUsuario = new edUsuario();
            try
            {

                itdUsuario = new tdUsuario();
                enUsuario = itdUsuario.tdObtenerUsuario(wsidusuario);
                return JsonConvert.SerializeObject(enUsuario);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex);
            }
        }

        [HttpGet]
        public string wsListarUsuario(string wusuario, int wtipousuario)
        {
            List<edUsuario> enUsuario = new List<edUsuario>();
            try
            {
                var valorusuario = "";
                if (wusuario != null)
                    valorusuario = wusuario;

                itdUsuario = new tdUsuario();
                enUsuario = itdUsuario.tdListarUsuario(valorusuario, wtipousuario);
                return JsonConvert.SerializeObject(enUsuario);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex);
            }
        }

        [HttpGet]
        public int wsActualizarAcceso(int wstipoproceso, int wsidusuario, string wsdusuario, string wsdclave)
        {
            int iresultado = -4;
            try
            {
                itdUsuario = new tdUsuario();
                iresultado = itdUsuario.tdActualizarAcceso(wstipoproceso, wsidusuario, wsdusuario, wsdclave);
                return iresultado;
            }
            catch (Exception ex)
            {
                return iresultado;
            }
        }

    }
}