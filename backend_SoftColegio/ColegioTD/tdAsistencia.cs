using ColegioAD;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColegioTD
{
    public class tdAsistencia : td_aglobal
    {
        adAsistencia iadAsistencia;

        public int tdInsertarAsistencia(int tdidclase, int tdidtipoasistencia,
                                        string tdfechaingreso, string tdobservacion)
        {
            int iRespuesta = -1;
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadAsistencia = new adAsistencia(con);
                        iRespuesta = iadAsistencia.adInsertarAsistencia(tdidclase, tdidtipoasistencia,
                                        tdfechaingreso, tdobservacion);
                        scope.Commit();
                    }
                }
                return (iRespuesta);
            }
            catch (MySqlException ex)
            {
                //UtlLog.toWrite(UtlConstantes.TProcessRN, UtlConstantes.LogNamespace_TProcessRN, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                throw ex;
            }

        }
    }
}
