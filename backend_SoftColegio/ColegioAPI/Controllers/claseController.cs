using System;
using System.Collections.Generic;
using System.Web.Http;
using Newtonsoft.Json;
using ColegioED;
using ColegioTD;

namespace ColegioAPI.Controllers
{
    public class claseController : ApiController
    {
        tdClase itdClase;

        [HttpGet]
        public int wsInsertarClase(int wsidcurso, int wssemana, string wsnombre, string wsdescripcion
                                , string wsrutaenlace, string wsrutavideo, int wscategoria, string wsimagen
                                , string wsimagenruta, int wsorden, Int16 wsestado, string wfecharegistro)
        {
            int iresultado = -1;
            try
            {
                itdClase = new tdClase();
                iresultado = itdClase.tdInsertarClase(wsidcurso, wssemana, wsnombre, wsdescripcion
                                                    , wsrutaenlace, wsrutavideo, wscategoria, wsimagen
                                                    , wsimagenruta, wsorden, wsestado, DateTime.Now);
                return iresultado;
            }
            catch (Exception ex)
            {
                return iresultado;
            }
        }

        [HttpGet]
        public int wsEliminarClase(int wsidclase)
        {
            int iresultado = -1;
            try
            {
                itdClase = new tdClase();
                iresultado = itdClase.tdEliminarClase(wsidclase);
                return iresultado;
            }
            catch (Exception ex)
            {
                return iresultado;
            }
        }

        [HttpGet]
        public string wsListarCurso(int wsidgrado, int wsidnivel, int wstipousuario, int widusuario)
        {
            List<edClase> wsenClase = new List<edClase>();
            try
            {
                itdClase = new tdClase();
                wsenClase = itdClase.tdListarCurso(wsidgrado, wsidnivel, wstipousuario, widusuario);
                return JsonConvert.SerializeObject(wsenClase);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex);
            }
        }

        [HttpGet]
        public string wsListarClaseCurso(int widcurso, int widusuario, int wtipousuario)
        {
            List<edClase> wsenClase = new List<edClase>();
            try
            {
                itdClase = new tdClase();
                wsenClase = itdClase.tdListarClaseCurso(widcurso, widusuario, wtipousuario);
                return JsonConvert.SerializeObject(wsenClase);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex);
            }
        }

        [HttpGet]
        public int wsActualizarClaseGestion(int wstiproceso, int wsidclase, int wsidcurso, int wsidsemana
                                    , string wsnombre, string wsdescripcion, string wsrutaenlace
                                    , string wsrutavideo, string wsimagenruta)
        {
            int iresultado = -1;
            try
            {
                itdClase = new tdClase();
                iresultado = itdClase.tdActualizarClaseGestion(wstiproceso, wsidclase, wsidcurso, wsidsemana
                                                , wsnombre, wsdescripcion, wsrutaenlace, wsrutavideo, wsimagenruta);
                return iresultado;
            }
            catch (Exception ex)
            {
                return iresultado;
            }
        }

    }
}