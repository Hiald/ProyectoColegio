using ColegioAD;
using ColegioED;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace ColegioTD
{
    public class tdClase : td_aglobal
    {
        adClase iadClase;

        public int tdInsertarClase(int tdidcurso, int tdsemana, string tdnombre, string tddescripcion
                                , string tdrutaenlace, string tdrutavideo, int tdcategoria, string tdimagen
                                , string tdimagenruta, int tdorden, Int16 tdestado, DateTime tdfecharegistro)
        {
            int iRespuesta = -1;
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadClase = new adClase(con);
                        iRespuesta = iadClase.adInsertarClase(tdidcurso, tdsemana, tdnombre, tddescripcion
                                                            , tdrutaenlace, tdrutavideo, tdcategoria, tdimagen
                                                            , tdimagenruta, tdorden, tdestado, tdfecharegistro);
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

        public int tdEliminarClase(int tdidclase)
        {
            int iRespuesta = -1;
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadClase = new adClase(con);
                        iRespuesta = iadClase.adEliminarClase(tdidclase);
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

        public List<edClase> tdListarCurso(int tdidgrado, int tdidnivel, int tdtipousuario, int tdidusuario)
        {
            List<edClase> renClase = new List<edClase>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadClase = new adClase(con);
                        renClase = iadClase.adListarCurso(tdidgrado, tdidnivel, tdtipousuario, tdidusuario);
                        scope.Commit();
                    }
                }
                return (renClase);
            }
            catch (MySqlException ex)
            {
                //UtlLog.toWrite(UtlConstantes.TProcessRN, UtlConstantes.LogNamespace_TProcessRN, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                throw ex;
            }

        }

        public List<edClase> tdListarClaseCurso(int tdidcurso, int tdidusuario, int tdtipousuario)
        {
            List<edClase> renClase = new List<edClase>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadClase = new adClase(con);
                        renClase = iadClase.adListarClaseCurso(tdidcurso, tdidusuario, tdtipousuario);
                        scope.Commit();
                    }
                }
                return (renClase);
            }
            catch (MySqlException ex)
            {
                //UtlLog.toWrite(UtlConstantes.TProcessRN, UtlConstantes.LogNamespace_TProcessRN, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                throw ex;
            }

        }

        public int tdActualizarClaseGestion(int tdtiproceso, int tdidclase, int tdidcurso, int tdidsemana
                                    , string tdnombre, string tddescripcion, string tdrutaenlace
                                    , string tdrutavideo, string tdimagenruta)
        {
            int iRespuesta = -1;
            try
            {
                using (MySqlConnection con = new MySqlConnection(mysqlConexion))
                {
                    con.Open();
                    using (MySqlTransaction scope = con.BeginTransaction())
                    {
                        iadClase = new adClase(con);
                        iRespuesta = iadClase.adActualizarClaseGestion(tdtiproceso, tdidclase, tdidcurso, tdidsemana
                                                , tdnombre, tddescripcion, tdrutaenlace, tdrutavideo, tdimagenruta);
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
