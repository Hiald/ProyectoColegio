using System;
using System.Collections.Generic;
using System.Web.Http;
using Newtonsoft.Json;
using ColegioED;
using ColegioTD;

namespace ColegioAPI.Controllers
{
    public class asistenciaController : ApiController
    {
        tdAsistencia itdAsistencia;

        [HttpGet]
        public int wsInsertarAsistencia(int widclase, int widtipoasistencia,
                                        string wfechaingreso, string wobservacion)
        {
            int iresultado = -4;
            try
            {
                itdAsistencia = new tdAsistencia();
                iresultado = itdAsistencia.tdInsertarAsistencia(widclase, widtipoasistencia, wfechaingreso, wobservacion);
                return iresultado;
            }
            catch (Exception ex)
            {
                return iresultado;
            }
        }


    }
}