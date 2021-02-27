using ColegioAD;
using ColegioED;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace ColegioTD
{
    public class tdArchivo : td_aglobal
    {
        adArchivo iadArchivo;

        public int tdInsertarArchivo(int tdidclase, int tdidusuario, string tdnombre
                               , string tdrutaenlace, int tdtipoarchivo, string tdfechainicio
                               , string tdfechafin, string tddescripcion, string tdrutaarchivo)
        {
            int iRespuesta = -1;
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadArchivo = new adArchivo(con);
                        iRespuesta = iadArchivo.adInsertarArchivo(tdidclase, tdidusuario, tdnombre
                                               , tdrutaenlace, tdtipoarchivo, tdfechainicio
                                               , tdfechafin, tddescripcion, tdrutaarchivo);
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

        public List<edArchivo> tdObtenerArchivo(int tdidclase)
        {

            List<edArchivo> renUsuario = new List<edArchivo>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadArchivo = new adArchivo(con);
                        renUsuario = iadArchivo.adObtenerArchivo(tdidclase);
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

        public int tdActualizarArchivoDetalle(int tdidarchivodetalle, string tdnota, string tdobservacion, int tdidusuario
                                        , int tdtiponota, Int16 tdestado, DateTime tdfecharegistro)
        {
            int iRespuesta = -1;
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadArchivo = new adArchivo(con);
                        iRespuesta = iadArchivo.adActualizarArchivoDetalle(tdidarchivodetalle, tdnota, tdobservacion, tdidusuario
                                                                            , tdtiponota, tdestado, tdfecharegistro);
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

        public int tdInsertarArchivoDetalle(int tdidarchivo, int tdidusuario, string tdimagen
                    , string tdnota, string adobservacion, string tddescripcion, string tdenlace)
        {
            int iRespuesta = -1;
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadArchivo = new adArchivo(con);
                        iRespuesta = iadArchivo.adInsertarArchivoDetalle(tdidarchivo,
                            tdidusuario, tdimagen, tdnota, adobservacion, tddescripcion, tdenlace);
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

        public List<edArchivo> tdListarArchivoDetalle(int tdidarchivo, int tdidusuario)
        {
            List<edArchivo> renUsuario = new List<edArchivo>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadArchivo = new adArchivo(con);
                        renUsuario = iadArchivo.adListarArchivoDetalle(tdidarchivo, tdidusuario);
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

        public List<edArchivo> tdListarArchivo(int tdidclase, int tdgrado, int tdnivel, int tdcurso)
        {
            List<edArchivo> renUsuario = new List<edArchivo>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadArchivo = new adArchivo(con);
                        renUsuario = iadArchivo.adListarArchivo(tdidclase, tdgrado, tdnivel, tdcurso);
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

        public int tdActualizarArchivo(int tditipoactualizar, int tdidarchivo, string tdnombre, string tdenlace
                                       , string tditipoarchivo, int tdipuntajeminimo, int tdipuntajemaximo
                                       , DateTime? tdfechainicio, DateTime? tdfechafin)
        {
            int iRespuesta = -1;
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadArchivo = new adArchivo(con);
                        iRespuesta = iadArchivo.adActualizarArchivo(tditipoactualizar, tdidarchivo, tdnombre, tdenlace
                                                           , tditipoarchivo, tdipuntajeminimo, tdipuntajemaximo
                                                           , tdfechainicio, tdfechafin);
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

        public int tdActualizarArchivoDetalle(int tditipoactualizar, int tdidarchivodetalle, int tdidarchivo
                                        , int tdidusuario, string tdidnota, string tdobservacion, string tdenlace)
        {
            int iRespuesta = -1;
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadArchivo = new adArchivo(con);
                        iRespuesta = iadArchivo.adActualizarArchivoDetalle(tditipoactualizar, tdidarchivodetalle
                                                , tdidarchivo, tdidusuario, tdidnota, tdobservacion, tdenlace);
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
