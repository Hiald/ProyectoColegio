using MySql.Data.MySqlClient;
using ColegioED;
using System;
using System.Collections.Generic;
using System.Data;

namespace ColegioAD
{
    public class adCurso : ad_aglobal
    {
        public adCurso(MySqlConnection cn)
        {
            cnMysql = cn;
        }

        public int adInsertarCursoDocente(int adidusuario, int adidnivel, int adidgrado, int adidcurso, Int16 adiestado)
        {
            try
            {
                int result = -1;
                MySqlCommand cmd = new MySqlCommand("sp_insertar_curso_docente", cnMysql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("_idusuario", MySqlDbType.Int32).Value = adidusuario;
                cmd.Parameters.Add("_idnivel", MySqlDbType.Int32).Value = adidnivel;
                cmd.Parameters.Add("_idgrado", MySqlDbType.Int32).Value = adidgrado;
                cmd.Parameters.Add("_idcurso", MySqlDbType.Int32).Value = adidcurso;
                cmd.Parameters.Add("_b_estado", MySqlDbType.Bit).Value = adiestado;

                result = Convert.ToInt32(cmd.ExecuteScalar());

                return result;
            }
            catch (Exception ex)
            {
                //utllog.towrite(utlconstantes.tprocessad, utlconstantes.lognamespace_tprocessad, this.gettype().name.tostring(), methodbase.getcurrentmethod().name, utlconstantes.logtipoerror, "", ex.stacktrace.tostring(), ex.message.tostring());
                throw ex;
            }
        }

        public List<edCurso> adListarCurso(int aditipousuario, int adidusuario, int adidnivel, int adidgrado, int adidcurso)
        {
            try
            {
                List<edCurso> slClase = new List<edCurso>();
                using (MySqlCommand cmd = new MySqlCommand("sp_listar_curso_docente", cnMysql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("_tipo_usuario", MySqlDbType.Int32).Value = aditipousuario;
                    cmd.Parameters.Add("_idusuario", MySqlDbType.Int32).Value = adidusuario;
                    cmd.Parameters.Add("_idnivel", MySqlDbType.Int32).Value = adidnivel;
                    cmd.Parameters.Add("_idgrado", MySqlDbType.Int32).Value = adidgrado;
                    cmd.Parameters.Add("_idcurso", MySqlDbType.Int32).Value = adidcurso;

                    using (MySqlDataReader mdrd = cmd.ExecuteReader())
                    {
                        if (mdrd != null)
                        {
                            edCurso sClase = null;
                            int pos_idcursodocente = mdrd.GetOrdinal("idcursodocente");
                            int pos_vnombres = mdrd.GetOrdinal("v_nombres");
                            int pos_apellidos = mdrd.GetOrdinal("apellidos");
                            int pos_nombrenivel = mdrd.GetOrdinal("nombreNivel");
                            int pos_nombregrado = mdrd.GetOrdinal("nombreGrado");
                            int pos_nombrecurso = mdrd.GetOrdinal("nombreCurso");
                            int pos_idnivel = mdrd.GetOrdinal("idnivel");
                            int pos_idgrado = mdrd.GetOrdinal("idgrado");
                            int pos_idcurso = mdrd.GetOrdinal("idcurso");

                            while (mdrd.Read())
                            {
                                sClase = new edCurso();
                                sClase.idcursodocente = (mdrd.IsDBNull(pos_idcursodocente) ? 0 : mdrd.GetInt32(pos_idcursodocente));
                                sClase.Snombres = (mdrd.IsDBNull(pos_vnombres) ? "-" : mdrd.GetString(pos_vnombres));
                                sClase.Sapellidos = (mdrd.IsDBNull(pos_apellidos) ? "-" : mdrd.GetString(pos_apellidos));
                                sClase.SnombreNivel = (mdrd.IsDBNull(pos_nombrenivel) ? "-" : mdrd.GetString(pos_nombrenivel));
                                sClase.SnombreGrado = (mdrd.IsDBNull(pos_nombregrado) ? "-" : mdrd.GetString(pos_nombregrado));
                                sClase.SnombreCurso = (mdrd.IsDBNull(pos_nombrecurso) ? "-" : mdrd.GetString(pos_nombrecurso));
                                sClase.idnivel = (mdrd.IsDBNull(pos_idnivel) ? 0 : mdrd.GetInt32(pos_idnivel));
                                sClase.idgrado = (mdrd.IsDBNull(pos_idgrado) ? 0 : mdrd.GetInt32(pos_idgrado));
                                sClase.idcurso = (mdrd.IsDBNull(pos_idcurso) ? 0 : mdrd.GetInt32(pos_idcurso));
                                slClase.Add(sClase);
                            }
                        }
                    }
                    return slClase;
                }
            }
            catch (Exception ex)
            {
                //UtlLog.toWrite(UtlConstantes.TProcessAD, UtlConstantes.LogNamespace_TProcessAD, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                throw ex;
            }
        }

        public List<edCurso> adListarGrado(int adidnivel)
        {
            try
            {
                List<edCurso> slClase = new List<edCurso>();
                using (MySqlCommand cmd = new MySqlCommand("sp_listar_grado", cnMysql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("_idnivel", MySqlDbType.Int32).Value = adidnivel;

                    using (MySqlDataReader mdrd = cmd.ExecuteReader())
                    {
                        if (mdrd != null)
                        {
                            edCurso sClase = null;
                            int pos_idgrado = mdrd.GetOrdinal("idgrado");
                            int pos_vnombre = mdrd.GetOrdinal("v_nombre");

                            while (mdrd.Read())
                            {
                                sClase = new edCurso();
                                sClase.idgrado = (mdrd.IsDBNull(pos_idgrado) ? 0 : mdrd.GetInt32(pos_idgrado));
                                sClase.SnombreGrado = (mdrd.IsDBNull(pos_vnombre) ? "-" : mdrd.GetString(pos_vnombre));
                                slClase.Add(sClase);
                            }
                        }
                    }
                    return slClase;
                }
            }
            catch (Exception ex)
            {
                //UtlLog.toWrite(UtlConstantes.TProcessAD, UtlConstantes.LogNamespace_TProcessAD, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                throw ex;
            }
        }

        public List<edCurso> adListarNivel()
        {
            try
            {
                List<edCurso> slClase = new List<edCurso>();
                using (MySqlCommand cmd = new MySqlCommand("sp_listar_nivel", cnMysql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("_idnivel", MySqlDbType.Int32).Value = 1;

                    using (MySqlDataReader mdrd = cmd.ExecuteReader())
                    {
                        if (mdrd != null)
                        {
                            edCurso sClase = null;
                            int pos_idnivel = mdrd.GetOrdinal("idnivel");
                            int pos_vnombre = mdrd.GetOrdinal("v_nombre");

                            while (mdrd.Read())
                            {
                                sClase = new edCurso();
                                sClase.idnivel = (mdrd.IsDBNull(pos_idnivel) ? 0 : mdrd.GetInt32(pos_idnivel));
                                sClase.SnombreNivel = (mdrd.IsDBNull(pos_vnombre) ? "-" : mdrd.GetString(pos_vnombre));
                                slClase.Add(sClase);
                            }
                        }
                    }
                    return slClase;
                }
            }
            catch (Exception ex)
            {
                //UtlLog.toWrite(UtlConstantes.TProcessAD, UtlConstantes.LogNamespace_TProcessAD, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                throw ex;
            }
        }

    }
}
