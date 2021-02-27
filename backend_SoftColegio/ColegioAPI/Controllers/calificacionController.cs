using System;
using System.Collections.Generic;
using System.Web.Http;
using Newtonsoft.Json;
using ColegioED;
using ColegioTD;

namespace ColegioAPI.Controllers
{
    public class calificacionController : ApiController
    {
        tdCalificacion itdCalificacion;

        [HttpGet]
        public string wsListarCalificacion(int widusuario, int wtiponota, int wnota, int wisemana)
        {
            List<edCalificacion> wsenCalificacion = new List<edCalificacion>();
            try
            {
                itdCalificacion = new tdCalificacion();
                wsenCalificacion = itdCalificacion.tdListarCalificacion(widusuario, wtiponota, wnota, wisemana);
                return JsonConvert.SerializeObject(wsenCalificacion);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex);
            }
        }

        [HttpGet]
        public int wsActualizarNota(int wsidarchivodetalle, string wsinota, string wsobservacion, int wsidusuario,
                                    int wsitiponota, Int16 wsiestado)
        {
            int iresultado = -1;
            try
            {
                itdCalificacion = new tdCalificacion();
                iresultado = itdCalificacion.tdActualizarNota(wsidarchivodetalle, wsinota, wsobservacion
                                                    , wsidusuario, wsitiponota, wsiestado);
                return iresultado;
            }
            catch (Exception ex)
            {
                return iresultado;
            }
        }

    }
}