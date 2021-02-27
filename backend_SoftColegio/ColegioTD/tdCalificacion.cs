using ColegioAD;
using ColegioED;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace ColegioTD
{
    public class tdCalificacion : td_aglobal
    {
        adCalificacion iadCalificacion;

        public List<edCalificacion> tdListarCalificacion(int tdidusuario, int tdtiponota, int tdnota, int tdisemana)
        {
            List<edCalificacion> renUsuario = new List<edCalificacion>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadCalificacion = new adCalificacion(con);
                        renUsuario = iadCalificacion.adListarCalificacion(tdidusuario, tdtiponota, tdnota, tdisemana);
                        scope.Commit();
                    }
                }

                return (renUsuario);
            }
            catch (MySqlException ex)
            {
                //UtlLog.toWrite(UtlConstantes.TProcessRN, UtlConstantes.LogNamespace_TProcessRN, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                throw ex;
            }

        }

        public int tdActualizarNota(int tdidarchivodetalle, string tdinota, string tdobservacion, int tdidusuario,
                                    int tditiponota, Int16 tdiestado)
        {
            int iRespuesta = -1;
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadCalificacion = new adCalificacion(con);
                        iRespuesta = iadCalificacion.adActualizarNota(tdidarchivodetalle, tdinota, tdobservacion, tdidusuario, tditiponota, tdiestado);
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
