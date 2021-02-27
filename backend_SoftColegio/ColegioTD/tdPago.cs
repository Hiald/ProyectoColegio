using ColegioAD;
using ColegioED;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace ColegioTD
{
    public class tdPago : td_aglobal
    {
        adPago iadPago;

        public int tdActualizarUsuarioClave(int tdidusuario, string tdnuevaclave)
        {
            int iRespuesta = -1;
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadPago = new adPago(con);
                        iRespuesta = iadPago.adActualizarUsuarioClave(tdidusuario, tdnuevaclave);
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

        public List<edPago> tdListarPago(string tdcuenta, int tdidusuario, int tdidnivel, int tdidgrado
                    , int tdidcurso, Int16 tdivigente, string tdnombre, string tdfechaini, string tdfechafin
                    , int tdimes, int tdianio)
        {

            List<edPago> renPago = new List<edPago>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadPago = new adPago(con);
                        renPago = iadPago.adListarPago(tdcuenta, tdidusuario, tdidnivel, tdidgrado
                            , tdidcurso, tdivigente, tdnombre, tdfechaini, tdfechafin, tdimes, tdianio);
                        scope.Commit();
                    }
                }
                return (renPago);
            }
            catch (MySqlException ex)
            {
                //UtlLog.toWrite(UtlConstantes.TProcessRN, UtlConstantes.LogNamespace_TProcessRN, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                throw ex;
            }

        }

        public List<edPago> tdListarPagoDetalle(int tdidpago, int tdbactivo, int tdbestado, string tdfechaini
                                        , string tdfechafin)
        {

            List<edPago> renPago = new List<edPago>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadPago = new adPago(con);
                        renPago = iadPago.adListarPagoDetalle(tdidpago, tdbactivo, tdbestado, tdfechaini, tdfechafin);
                        scope.Commit();
                    }
                }
                return (renPago);
            }
            catch (MySqlException ex)
            {
                //UtlLog.toWrite(UtlConstantes.TProcessRN, UtlConstantes.LogNamespace_TProcessRN, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                throw ex;
            }

        }

        public int tdRegistrarPago(int tdidpago, int tdidpagodetalle, int tdidusuario, int tdidnivel
                , int tdidgrado, int tdidcurso, string tdoperacion
                , int tdtipopago, int tdtipomoneda, string tddescripcion, int tdmes, int tdanio
                , string tdhora, decimal tdmonto, string tdimg_ruta_1, string tdimg_ruta_2
            , string tdfecha_ini_pago, Int16 tdbestado, string tdfecharegistro, string tdfechafin)
        {
            int iRespuesta = -1;
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadPago = new adPago(con);
                        iRespuesta = iadPago.adRegistrarPago(tdidpago, tdidpagodetalle, tdidusuario
                                , tdidnivel, tdidgrado, tdidcurso, tdoperacion
                                , tdtipopago, tdtipomoneda, tddescripcion, tdmes, tdanio
                                , tdhora, tdmonto, tdimg_ruta_1, tdimg_ruta_2
                                , tdfecha_ini_pago, tdbestado, tdfecharegistro, tdfechafin);
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

        public int tdNotificarPago(int tdidusuario, int tdimes, string tdfechaacceso, string tdfechavalidar)
        {
            int iRespuesta = -1;
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadPago = new adPago(con);
                        iRespuesta = iadPago.adNotificarPago(tdidusuario, tdimes, tdfechaacceso, tdfechavalidar);
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

        public List<edPago> tdRptListarSumaPagos(int tdidnivel, int tdidgrado, int tdidcurso
                , string tdfechaini, string tdfechafin, int tdmes, int tdanio)
        {

            List<edPago> renPago = new List<edPago>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadPago = new adPago(con);
                        renPago = iadPago.adRptListarSumaPagos(tdidnivel, tdidgrado, tdidcurso
                                                            , tdfechaini, tdfechafin, tdmes, tdanio);
                        scope.Commit();
                    }
                }
                return (renPago);
            }
            catch (MySqlException ex)
            {
                //UtlLog.toWrite(UtlConstantes.TProcessRN, UtlConstantes.LogNamespace_TProcessRN, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                throw ex;
            }

        }

        public int tdListarUsuarioTotal(int tdidnivel, int tdidgrado, int tdidcurso)
        {
            int iRespuesta = -1;
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadPago = new adPago(con);
                        iRespuesta = iadPago.adListarUsuarioTotal(tdidnivel, tdidgrado, tdidcurso);
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

        public List<edPago> tdListarPagoPendiente(int tdidusuario, int tdidnivel, int tdidgrado
                                                    , int tdidcurso, int tdbactivo)
        {

            List<edPago> renPago = new List<edPago>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadPago = new adPago(con);
                        renPago = iadPago.adListarPagoPendiente(tdidusuario, tdidnivel, tdidgrado
                                                                , tdidcurso, tdbactivo);
                        scope.Commit();
                    }
                }
                return (renPago);
            }
            catch (MySqlException ex)
            {
                //UtlLog.toWrite(UtlConstantes.TProcessRN, UtlConstantes.LogNamespace_TProcessRN, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                throw ex;
            }

        }

        public List<edPago> tdRptListarUsuarioPagos(string tdusuario, string tdfechaini, string tdfechafin, int tdidcurso)
        {

            List<edPago> renPago = new List<edPago>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadPago = new adPago(con);
                        renPago = iadPago.adRptListarUsuarioPagos(tdusuario, tdfechaini, tdfechafin, tdidcurso);
                        scope.Commit();
                    }
                }
                return (renPago);
            }
            catch (MySqlException ex)
            {
                //UtlLog.toWrite(UtlConstantes.TProcessRN, UtlConstantes.LogNamespace_TProcessRN, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                throw ex;
            }

        }

        public List<edPago> tdRptVentasTotales()
        {

            List<edPago> renPago = new List<edPago>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadPago = new adPago(con);
                        renPago = iadPago.adRptVentasTotales();
                        scope.Commit();
                    }
                }
                return (renPago);
            }
            catch (MySqlException ex)
            {
                //UtlLog.toWrite(UtlConstantes.TProcessRN, UtlConstantes.LogNamespace_TProcessRN, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                throw ex;
            }

        }

    }
}
