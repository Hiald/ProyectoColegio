using MySql.Data.MySqlClient;
using ColegioED;
using System;
using System.Collections.Generic;
using System.Data;

namespace ColegioAD
{
    public class adCalificacion : ad_aglobal
    {
        public adCalificacion(MySqlConnection cn)
        {
            cnMysql = cn;
        }

        public List<edCalificacion> adListarCalificacion(int adidusuario, int adtiponota, int adnota, int adisemana)
        {
            try
            {
                List<edCalificacion> loenusuario = new List<edCalificacion>();
                using (MySqlCommand cmd = new MySqlCommand("sp_listar_calificacion", cnMysql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("_idusuario", MySqlDbType.Int32).Value = adidusuario;
                    cmd.Parameters.Add("_tiponota", MySqlDbType.Int32).Value = adtiponota;
                    cmd.Parameters.Add("_idnota", MySqlDbType.Int32).Value = adnota;
                    cmd.Parameters.Add("_isemana", MySqlDbType.Int32).Value = adisemana;

                    using (MySqlDataReader mdrd = cmd.ExecuteReader())
                    {
                        if (mdrd != null)
                        {
                            edCalificacion senUsuario = null;
                            int pos_idcalificacion = mdrd.GetOrdinal("idcalificacion");
                            int pos_idusuario = mdrd.GetOrdinal("idusuario");
                            int pos_vnombres = mdrd.GetOrdinal("v_nombres");
                            int pos_vapellidomaterno = mdrd.GetOrdinal("v_apellido_materno");
                            int pos_vapellidopaterno = mdrd.GetOrdinal("v_apellido_paterno");
                            int pos_idgrado = mdrd.GetOrdinal("idgrado");
                            int pos_idnivel = mdrd.GetOrdinal("idnivel");
                            int pos_nombrenivel = mdrd.GetOrdinal("nombreNivel");
                            int pos_nombregrado = mdrd.GetOrdinal("nombreGrado");
                            int pos_itiponota = mdrd.GetOrdinal("i_tipo_nota");
                            int pos_idnota = mdrd.GetOrdinal("idnota");
                            int pos_inota = mdrd.GetOrdinal("i_nota");
                            int pos_vobservacion = mdrd.GetOrdinal("v_observacion");
                            int pos_snombrecurso = mdrd.GetOrdinal("snombrecurso");
                            int pos_idclase = mdrd.GetOrdinal("idclase");
                            int pos_isemana = mdrd.GetOrdinal("i_semana");

                            while (mdrd.Read())
                            {
                                senUsuario = new edCalificacion();
                                senUsuario.idcalificacion = (mdrd.IsDBNull(pos_idcalificacion) ? 0 : mdrd.GetInt32(pos_idcalificacion));
                                senUsuario.idusuario = (mdrd.IsDBNull(pos_idusuario) ? 0 : mdrd.GetInt32(pos_idusuario));
                                senUsuario.Snombres = (mdrd.IsDBNull(pos_vnombres) ? "-" : mdrd.GetString(pos_vnombres));
                                senUsuario.SApellidoMaterno = (mdrd.IsDBNull(pos_vapellidomaterno) ? "-" : mdrd.GetString(pos_vapellidomaterno));
                                senUsuario.SApellidoPaterno = (mdrd.IsDBNull(pos_vapellidopaterno) ? "-" : mdrd.GetString(pos_vapellidopaterno));
                                senUsuario.idgrado = (mdrd.IsDBNull(pos_idgrado) ? 0 : mdrd.GetInt32(pos_idgrado));
                                senUsuario.idnivel = (mdrd.IsDBNull(pos_idnivel) ? 0 : mdrd.GetInt32(pos_idnivel));
                                senUsuario.nombreNivel = (mdrd.IsDBNull(pos_nombrenivel) ? "-" : mdrd.GetString(pos_nombrenivel));
                                senUsuario.nombreGrado = (mdrd.IsDBNull(pos_nombregrado) ? "-" : mdrd.GetString(pos_nombregrado));
                                senUsuario.Itiponota = (mdrd.IsDBNull(pos_itiponota) ? 0 : mdrd.GetInt32(pos_itiponota));
                                senUsuario.idnota = (mdrd.IsDBNull(pos_idnota) ? 0 : mdrd.GetInt32(pos_idnota));
                                senUsuario.Inota = (mdrd.IsDBNull(pos_inota) ? 0 : mdrd.GetInt32(pos_inota));
                                senUsuario.Sobservacion = (mdrd.IsDBNull(pos_vobservacion) ? "-" : mdrd.GetString(pos_vobservacion));
                                senUsuario.Snombrecurso = (mdrd.IsDBNull(pos_snombrecurso) ? "-" : mdrd.GetString(pos_snombrecurso));
                                senUsuario.idclase = (mdrd.IsDBNull(pos_idclase) ? 0 : mdrd.GetInt32(pos_idclase));
                                senUsuario.isemana = (mdrd.IsDBNull(pos_isemana) ? 0 : mdrd.GetInt32(pos_isemana));

                                loenusuario.Add(senUsuario);
                            }
                        }
                    }
                    return loenusuario;
                }
            }
            catch (Exception ex)
            {
                //UtlLog.toWrite(UtlConstantes.TProcessAD, UtlConstantes.LogNamespace_TProcessAD, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                throw ex;
            }
        }

        public int adActualizarNota(int adidarchivodetalle, string adinota, string adobservacion, int adidusuario,
                                    int aditiponota, Int16 adiestado)
        {
            try
            {
                int result = -1;
                MySqlCommand cmd = new MySqlCommand("sp_actualizar_nota", cnMysql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("_idarchivodetalle", MySqlDbType.Int32).Value = adidarchivodetalle;
                cmd.Parameters.Add("_nota", MySqlDbType.VarChar, 10).Value = adinota;
                cmd.Parameters.Add("_observacion", MySqlDbType.VarChar, 500).Value = adobservacion;
                cmd.Parameters.Add("_idusuario", MySqlDbType.Int32).Value = adidusuario;
                cmd.Parameters.Add("_tiponota", MySqlDbType.Int32).Value = aditiponota;
                cmd.Parameters.Add("_estado", MySqlDbType.Bit).Value = adiestado;
                cmd.Parameters.Add("_fecharegistro", MySqlDbType.DateTime).Value = DateTime.Now;

                result = Convert.ToInt32(cmd.ExecuteScalar());

                return result;
            }
            catch (Exception ex)
            {
                //utllog.towrite(utlconstantes.tprocessad, utlconstantes.lognamespace_tprocessad, this.gettype().name.tostring(), methodbase.getcurrentmethod().name, utlconstantes.logtipoerror, "", ex.stacktrace.tostring(), ex.message.tostring());
                throw ex;
            }
        }

    }
}
