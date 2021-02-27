using System;
using System.Collections.Generic;
using System.Web.Http;
using Newtonsoft.Json;
using ColegioED;
using ColegioTD;

namespace ColegioAPI.Controllers
{
    public class archivoController : ApiController
    {
        tdArchivo itdArchivo;

        [HttpGet]
        public int wsInsertarArchivo(int widclase, int widusuario, string wnombre
                               , string wrutaenlace, int wtipoarchivo, string wfechainicio
                               , string wfechafin, string wdescripcion, string wrutaarchivo)
        {
            int iresultado = -1;
            try
            {

                itdArchivo = new tdArchivo();
                iresultado = itdArchivo.tdInsertarArchivo(widclase, widusuario, wnombre
                                               , wrutaenlace, wtipoarchivo, wfechainicio
                                               , wfechafin, wdescripcion, wrutaarchivo);
                return iresultado;
            }
            catch (Exception ex)
            {
                return iresultado;
            }
        }

        [HttpGet]
        public string wsObtenerArchivo(int widclase)
        {
            List<edArchivo> enUsuario = new List<edArchivo>();
            try
            {

                itdArchivo = new tdArchivo();
                enUsuario = itdArchivo.tdObtenerArchivo(widclase);
                return JsonConvert.SerializeObject(enUsuario);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(enUsuario);
            }
        }

        [HttpGet]
        public int wsActualizarArchivoDetalle(int widarchivodetalle, string wnota, string wobservacion, int widusuario
                                        , int wtiponota, Int16 westado, string wfecharegistro)
        {
            int iresultado = -1;
            try
            {

                itdArchivo = new tdArchivo();
                iresultado = itdArchivo.tdActualizarArchivoDetalle(widarchivodetalle, wnota, wobservacion, widusuario
                                                                    , wtiponota, westado, DateTime.Now);
                return iresultado;
            }
            catch (Exception ex)
            {
                return iresultado;
            }
        }

        [HttpGet]
        public int wsInsertarArchivoDetalle(int widarchivo, int widusuario, string wimagen
                    , string wnota, string wobservacion, string wdescripcion, string wenlace)
        {
            int iresultado = -1;
            try
            {
                itdArchivo = new tdArchivo();
                iresultado = itdArchivo.tdInsertarArchivoDetalle(widarchivo, widusuario, wimagen
                                                    , wnota, wobservacion, wdescripcion, wenlace);
                return iresultado;
            }
            catch (Exception ex)
            {
                return iresultado;
            }
        }

        [HttpGet]
        public string wsListarArchivoDetalle(int widarchivo, int widusuario)
        {
            List<edArchivo> wsenUsuario = new List<edArchivo>();
            try
            {
                itdArchivo = new tdArchivo();
                wsenUsuario = itdArchivo.tdListarArchivoDetalle(widarchivo, widusuario);
                return JsonConvert.SerializeObject(wsenUsuario);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex);
            }
        }

        [HttpGet]
        public string wsListarArchivoGeneral(int wsidclasea, int wdgradoa, int wdnivela, int wsdcursoa)
        {
            List<edArchivo> wsenUsuario = new List<edArchivo>();
            try
            {
                itdArchivo = new tdArchivo();
                wsenUsuario = itdArchivo.tdListarArchivo(wsidclasea, wdgradoa, wdnivela, wsdcursoa);
                return JsonConvert.SerializeObject(wsenUsuario);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex);
            }
        }

        [HttpGet]
        public int wsActualizarArchivo(int wsitipoactualizar, int wsidarchivo, string wsnombre, string wsenlace
                                       , string wsitipoarchivo, int wsipuntajeminimo, int wsipuntajemaximo
                                       , string wfechainicio, string wfechafin)
        {
            int iresultado = -1;
            try
            {
                DateTime? wsfechainicio = DateTime.Parse(wfechainicio);
                DateTime? wsfechafin = DateTime.Parse(wfechafin);
                itdArchivo = new tdArchivo();
                iresultado = itdArchivo.tdActualizarArchivo(wsitipoactualizar, wsidarchivo, wsnombre, wsenlace
                                                       , wsitipoarchivo, wsipuntajeminimo, wsipuntajemaximo
                                                       , wsfechainicio, wsfechafin);
                return iresultado;
            }
            catch (Exception ex)
            {
                return iresultado;
            }
        }

        [HttpGet]
        public int wsActualizarArchivoDetalle(int wsitipoactualizar, int wsidarchivodetalle, int wsidarchivo
                                        , int wsidusuario, string wsidnota, string wsobservacion, string wsenlace)
        {
            int iresultado = -1;
            try
            {
                itdArchivo = new tdArchivo();
                iresultado = itdArchivo.tdActualizarArchivoDetalle(wsitipoactualizar, wsidarchivodetalle, wsidarchivo
                                                                    , wsidusuario, wsidnota, wsobservacion, wsenlace);
                return iresultado;
            }
            catch (Exception ex)
            {
                return iresultado;
            }
        }

    }
}