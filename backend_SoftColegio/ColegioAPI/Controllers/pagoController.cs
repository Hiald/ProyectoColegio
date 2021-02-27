using System;
using System.Collections.Generic;
using System.Web.Http;
using Newtonsoft.Json;
using ColegioED;
using ColegioTD;

namespace ColegioAPI.Controllers
{
    public class pagoController : ApiController
    {
        tdPago itdPago;

        [HttpGet]
        public int wsActualizarUsuarioClave(int wsidusuario, string wsnuevaclave)
        {
            int iresultado = -4;
            try
            {
                itdPago = new tdPago();
                iresultado = itdPago.tdActualizarUsuarioClave(wsidusuario, wsnuevaclave);
                return iresultado;
            }
            catch (Exception ex)
            {
                return iresultado;
            }
        }

        [HttpGet]
        public string wsListarPago(string wcuenta, int widusuario, int widnivel, int widgrado
                    , int widcurso, Int16 wivigente, string wnombre, string wfechaini, string wfechafin
                    , int wimes, int wianio)
        {
            List<edPago> enPago = new List<edPago>();
            try
            {
                itdPago = new tdPago();
                enPago = itdPago.tdListarPago(wcuenta, widusuario, widnivel, widgrado
                    , widcurso, wivigente, wnombre, wfechaini, wfechafin, wimes, wianio);
                return JsonConvert.SerializeObject(enPago);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex);
            }
        }

        [HttpGet]
        public string wListarPagoDetalle(int idpago, int bactivo, int bestado, string fechaini, string fechafin)
        {
            List<edPago> enPago = new List<edPago>();
            try
            {
                itdPago = new tdPago();
                enPago = itdPago.tdListarPagoDetalle(idpago, bactivo, bestado, fechaini, fechafin);
                return JsonConvert.SerializeObject(enPago);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex);
            }
        }

        [HttpGet]
        public int wsRegistrarPago(int widpago, int widpagodetalle, int widusuario, int widnivel
                , int widgrado, int widcurso, string woperacion
                , int wtipopago, int wtipomoneda, string wdescripcion, int wmes, int wanio
                , string whora, decimal wmonto, string wimg_ruta_1, string wimg_ruta_2
                , string wfecha_ini_pago, Int16 wbestado, string wfr, string wff)
        {
            int iresultado = -4;
            try
            {
                itdPago = new tdPago();
                iresultado = itdPago.tdRegistrarPago(widpago, widpagodetalle, widusuario, widnivel
                , widgrado, widcurso, woperacion, wtipopago, wtipomoneda, wdescripcion, wmes, wanio
                , whora, wmonto, wimg_ruta_1, wimg_ruta_2, wfecha_ini_pago, wbestado, wfr, wff);
                return iresultado;
            }
            catch (Exception ex)
            {
                return iresultado;
            }
        }

        [HttpGet]
        public int wsNotificarPago(int widusuario, int imes, string fechaacceso, string fechavalidar)
        {
            int iresultado = -4;
            try
            {
                itdPago = new tdPago();
                iresultado = itdPago.tdNotificarPago(widusuario, imes, fechaacceso, fechavalidar);
                return iresultado;
            }
            catch (Exception ex)
            {
                return iresultado;
            }
        }

        [HttpGet]
        public string wsRptListarSumaPagos(int widnivel, int widgrado, int widcurso
                            , string wfechaini, string wfechafin, int wmes, int wanio)
        {
            List<edPago> enPago = new List<edPago>();
            try
            {
                itdPago = new tdPago();
                enPago = itdPago.tdRptListarSumaPagos(widnivel, widgrado, widcurso
                                                    , wfechaini, wfechafin, wmes, wanio);
                return JsonConvert.SerializeObject(enPago);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex);
            }
        }

        [HttpGet]
        public int wsListarUsuarioTotal(int widnivel, int widgrado, int widcurso)
        {
            int iresultado = -4;
            try
            {
                itdPago = new tdPago();
                iresultado = itdPago.tdListarUsuarioTotal(widnivel, widgrado, widcurso);
                return iresultado;
            }
            catch (Exception ex)
            {
                return iresultado;
            }
        }

        [HttpGet]
        public string wsListarPagoPendiente(int wsidusuario, int wsidnivel, int wsidgrado
                                            , int wsidcurso, int wsbactivo)
        {
            List<edPago> enPago = new List<edPago>();
            try
            {
                itdPago = new tdPago();
                enPago = itdPago.tdListarPagoPendiente(wsidusuario, wsidnivel, wsidgrado
                                                        , wsidcurso, wsbactivo);
                return JsonConvert.SerializeObject(enPago);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex);
            }
        }


        [HttpGet]
        public string wsRptListarUsuarioPagos(string wsusuario, string wsfechaini, string wsfechafin, int wsidcurso)
        {
            List<edPago> enPago = new List<edPago>();
            try
            {
                itdPago = new tdPago();
                enPago = itdPago.tdRptListarUsuarioPagos(wsusuario, wsfechaini, wsfechafin, wsidcurso);
                return JsonConvert.SerializeObject(enPago);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex);
            }
        }

        [HttpGet]
        public string wsRptVentasTotales()
        {
            List<edPago> enPago = new List<edPago>();
            try
            {
                itdPago = new tdPago();
                enPago = itdPago.tdRptVentasTotales();
                return JsonConvert.SerializeObject(enPago);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex);
            }
        }


    }
}