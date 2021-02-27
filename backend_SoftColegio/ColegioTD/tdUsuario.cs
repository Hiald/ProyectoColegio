using ColegioAD;
using ColegioED;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace ColegioTD
{
    public class tdUsuario : td_aglobal
    {
        adUsuario iadUsuario;

        public int tdInsertarUsuario(int tdidusuario, int tditipousuario, string tdusuario, string tdclave, string tdtoken, Int16 tdestado, DateTime tdfechaRegistro)
        {
            int iRespuesta = -1;
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadUsuario = new adUsuario(con);
                        iRespuesta = iadUsuario.adInsertarUsuario(tdidusuario, tditipousuario, tdusuario, tdclave, tdtoken, tdestado, tdfechaRegistro);
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

        public int tdInsertarCuenta(int tdidnivel, int tdidgrado, int tdidsede, string tdnombres, string tdamaterno, string tdapaterno, string tdgenero
                                    , string tdcorreo, Int16 tdestado, DateTime tdfechaRegistro, string tdimagen)
        {
            int iRespuesta = -3;
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadUsuario = new adUsuario(con);
                        iRespuesta = iadUsuario.adInsertarCuenta(tdidnivel, tdidgrado, tdidsede, tdnombres, tdamaterno, tdapaterno, tdgenero
                                                                , tdcorreo, tdestado, tdfechaRegistro, tdimagen);
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

        public int tdActualizarCuenta(int tdidusuario, int tdidnivel, int tdidgrado, int tdidsede, string tdnombres, string tdamaterno, string tdapaterno, string tdgenero
                                    , string tdcorreo, Int16 tdestado, DateTime tdfechaRegistro)
        {
            try
            {
                int iresultado = -3;
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    iadUsuario = new adUsuario(con);
                    iresultado = iadUsuario.adActualizarCuenta(tdidusuario, tdidnivel, tdidgrado, tdidsede, tdnombres, tdamaterno, tdapaterno, tdgenero
                                                                , tdcorreo, tdestado, tdfechaRegistro);
                }
                return iresultado;
            }
            catch (Exception ex)
            {
                //UtlLog.toWrite(UtlConstantes.ventaRN, UtlConstantes.LogNamespace_ventaRN, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                throw ex;
            }
        }



        public int tdObtenerAcceso(string tdusuario, string tdclave)
        {
            int iRespuesta = -1;
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadUsuario = new adUsuario(con);
                        iRespuesta = iadUsuario.adObtenerAcceso(tdusuario, tdclave);
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

        public edUsuario tdObtenerUsuario(int tdidusuario)
        {

            edUsuario renUsuario = new edUsuario();
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadUsuario = new adUsuario(con);
                        renUsuario = iadUsuario.adObtenerUsuario(tdidusuario);
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

        public List<edUsuario> tdListarUsuario(string tdusuario, int tdtipousuario)
        {

            List<edUsuario> renUsuario = new List<edUsuario>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadUsuario = new adUsuario(con);
                        renUsuario = iadUsuario.adListarUsuario(tdusuario, tdtipousuario);
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

        public int tdActualizarAcceso(int tdtipoproceso, int tdidusuario, string wusuario, string wclave)
        {
            int iRespuesta = -3;
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadUsuario = new adUsuario(con);
                        iRespuesta = iadUsuario.adActualizarAcceso(tdtipoproceso, tdidusuario, wusuario, wclave);
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
