using MySql.Data.MySqlClient;
using ColegioED;
using System;
using System.Collections.Generic;
using System.Data;

namespace ColegioAD
{
    public class adClase : ad_aglobal
    {
        public adClase(MySqlConnection cn)
        {
            cnMysql = cn;
        }

        public int adInsertarClase(int adidcurso, int adsemana, string adnombre, string addescripcion
                                , string adrutaenlace, string adrutavideo, int adcategoria, string adimagen
                                , string adimagenruta, int adorden, Int16 adestado, DateTime adfecharegistro)
        {
            try
            {
                int result = -1;
                MySqlCommand cmd = new MySqlCommand("sp_insertar_clase", cnMysql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("_nombre", MySqlDbType.VarChar, 100).Value = adnombre;
                cmd.Parameters.Add("_descripcion", MySqlDbType.VarChar, 500).Value = addescripcion;
                cmd.Parameters.Add("_rutaenlace", MySqlDbType.VarChar, 500).Value = adrutaenlace;
                cmd.Parameters.Add("_rutavideo", MySqlDbType.VarChar, 500).Value = adrutavideo;
                cmd.Parameters.Add("_categoria", MySqlDbType.Int32).Value = adcategoria;
                cmd.Parameters.Add("_idcurso", MySqlDbType.Int32).Value = adidcurso;
                cmd.Parameters.Add("_semana", MySqlDbType.Int32).Value = adsemana;
                cmd.Parameters.Add("_imagen", MySqlDbType.Blob).Value = adimagen;
                cmd.Parameters.Add("_imagenruta", MySqlDbType.VarChar, 500).Value = adimagenruta;
                cmd.Parameters.Add("_orden", MySqlDbType.Int32).Value = adorden;
                cmd.Parameters.Add("_estado", MySqlDbType.Bit).Value = adestado;
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
       
        public int adEliminarClase(int adidclase)
        {
            try
            {
                int result = -1;
                MySqlCommand cmd = new MySqlCommand("sp_eliminar_clase", cnMysql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("_idclase", MySqlDbType.Int32).Value = adidclase;

                result = Convert.ToInt32(cmd.ExecuteScalar());

                return result;
            }
            catch (Exception ex)
            {
                //utllog.towrite(utlconstantes.tprocessad, utlconstantes.lognamespace_tprocessad, this.gettype().name.tostring(), methodbase.getcurrentmethod().name, utlconstantes.logtipoerror, "", ex.stacktrace.tostring(), ex.message.tostring());
                throw ex;
            }
        }

        public List<edClase> adListarCurso(int adidgrado, int adidnivel, int adtipousuario, int adidusuario)
        {
            try
            {
                List<edClase> slClase = new List<edClase>();
                using (MySqlCommand cmd = new MySqlCommand("sp_listar_curso", cnMysql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("_idgrado", MySqlDbType.Int32).Value = adidgrado;
                    cmd.Parameters.Add("_idnivel", MySqlDbType.Int32).Value = adidnivel;
                    cmd.Parameters.Add("_tipo_usuario", MySqlDbType.Int32).Value = adtipousuario;
                    cmd.Parameters.Add("_idusuario", MySqlDbType.Int32).Value = adidusuario;

                    using (MySqlDataReader mdrd = cmd.ExecuteReader())
                    {
                        if (mdrd != null)
                        {
                            edClase sClase = null;
                            int pos_idcurso = mdrd.GetOrdinal("idcurso");
                            int pos_idgrado = mdrd.GetOrdinal("idgrado");
                            int pos_idnivel = mdrd.GetOrdinal("idnivel");
                            int pos_nombrenivel = mdrd.GetOrdinal("nombreNivel");
                            int pos_nombregrado = mdrd.GetOrdinal("nombreGrado");
                            int pos_vnombre = mdrd.GetOrdinal("v_nombre");
                            int pos_vdescripcion = mdrd.GetOrdinal("v_descripcion");
                            int pos_vimagen = mdrd.GetOrdinal("v_imagen");
                            int pos_vimagenruta = mdrd.GetOrdinal("v_imagen_ruta");
                            int pos_bestado = mdrd.GetOrdinal("b_estado");
                            int pos_dtfecharegistro = mdrd.GetOrdinal("dt_fecharegistro");

                            while (mdrd.Read())
                            {
                                sClase = new edClase();
                                sClase.idcurso = (mdrd.IsDBNull(pos_idcurso) ? 0 : mdrd.GetInt32(pos_idcurso));
                                sClase.idgrado = (mdrd.IsDBNull(pos_idgrado) ? 0 : mdrd.GetInt32(pos_idgrado));
                                sClase.idnivel = (mdrd.IsDBNull(pos_idnivel) ? 0 : mdrd.GetInt32(pos_idnivel));
                                sClase.nombrenivel = (mdrd.IsDBNull(pos_nombrenivel) ? "-" : mdrd.GetString(pos_nombrenivel));
                                sClase.nombregrado = (mdrd.IsDBNull(pos_nombregrado) ? "-" : mdrd.GetString(pos_nombregrado));
                                sClase.Snombre = (mdrd.IsDBNull(pos_vnombre) ? "-" : mdrd.GetString(pos_vnombre));
                                sClase.Sdescripcion = (mdrd.IsDBNull(pos_vdescripcion) ? "-" : mdrd.GetString(pos_vdescripcion));
                                sClase.Simagen = (mdrd.IsDBNull(pos_vimagen) ? "-" : mdrd.GetString(pos_vimagen));
                                sClase.SimagenRuta = (mdrd.IsDBNull(pos_vimagenruta) ? "-" : mdrd.GetString(pos_vimagenruta));
                                sClase.Iestado = (mdrd.IsDBNull(pos_bestado) ? 0 : mdrd.GetInt16(pos_bestado));
                                sClase.SfechaRegistro = (mdrd.IsDBNull(pos_dtfecharegistro) ? "-" : mdrd.GetString(pos_dtfecharegistro));
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

        public List<edClase> adListarClaseCurso(int adidcurso, int adidusuario, int adtipousuario)
        {
            try
            {
                List<edClase> slClase = new List<edClase>();
                using (MySqlCommand cmd = new MySqlCommand("sp_listar_clase_curso", cnMysql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("_idcurso", MySqlDbType.Int32).Value = adidcurso;
                    cmd.Parameters.Add("_idusuario", MySqlDbType.Int32).Value = adidusuario;
                    cmd.Parameters.Add("_tipo_usuario", MySqlDbType.Int32).Value = adtipousuario;

                    using (MySqlDataReader mdrd = cmd.ExecuteReader())
                    {
                        if (mdrd != null)
                        {
                            edClase sClase = null;
                            int pos_idclase = mdrd.GetOrdinal("idclase");
                            int pos_idcurso = mdrd.GetOrdinal("idcurso");
                            int pos_isemana = mdrd.GetOrdinal("i_semana");
                            int pos_vnombre = mdrd.GetOrdinal("v_nombre");
                            int pos_vnombrecurso = mdrd.GetOrdinal("nombrecurso");
                            int pos_vdescripcion = mdrd.GetOrdinal("v_descripcion");
                            int pos_vrutaenlace = mdrd.GetOrdinal("v_ruta_enlace");
                            int pos_vrutavideo = mdrd.GetOrdinal("v_ruta_video");
                            int pos_icategoria = mdrd.GetOrdinal("i_categoria");
                            int pos_vimagen = mdrd.GetOrdinal("v_imagen");
                            int pos_vimagenruta = mdrd.GetOrdinal("v_imagen_ruta");

                            while (mdrd.Read())
                            {
                                sClase = new edClase();
                                sClase.idclase = (mdrd.IsDBNull(pos_idclase) ? 0 : mdrd.GetInt32(pos_idclase));
                                sClase.idcurso = (mdrd.IsDBNull(pos_idcurso) ? 0 : mdrd.GetInt32(pos_idcurso));
                                sClase.Isemana = (mdrd.IsDBNull(pos_isemana) ? 0 : mdrd.GetInt32(pos_isemana));
                                sClase.Snombre = (mdrd.IsDBNull(pos_vnombrecurso) ? "-" : mdrd.GetString(pos_vnombrecurso));
                                sClase.Snombrecurso = (mdrd.IsDBNull(pos_vnombre) ? "-" : mdrd.GetString(pos_vnombre));
                                sClase.Sdescripcion = (mdrd.IsDBNull(pos_vdescripcion) ? "-" : mdrd.GetString(pos_vdescripcion));
                                sClase.Srutaenlace = (mdrd.IsDBNull(pos_vrutaenlace) ? "-" : mdrd.GetString(pos_vrutaenlace));
                                sClase.SrutaVideo = (mdrd.IsDBNull(pos_vrutavideo) ? "-" : mdrd.GetString(pos_vrutavideo));
                                sClase.Icategoria = (mdrd.IsDBNull(pos_icategoria) ? 0 : mdrd.GetInt32(pos_icategoria));
                                sClase.Simagen = (mdrd.IsDBNull(pos_vimagen) ? "-" : mdrd.GetString(pos_vimagen));
                                sClase.SimagenRuta = (mdrd.IsDBNull(pos_vimagenruta) ? "-" : mdrd.GetString(pos_vimagenruta));
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

        public int adActualizarClaseGestion(int adtiproceso, int adidclase, int adidcurso, int adidsemana
                                    , string adnombre, string addescripcion, string adrutaenlace
                                    , string adrutavideo, string adimagenruta)
        {
            try
            {
                int result = -1;
                MySqlCommand cmd = new MySqlCommand("sp_actualizar_clase", cnMysql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("_tipoactualizar", MySqlDbType.Int32).Value = adtiproceso;
                cmd.Parameters.Add("_idclase", MySqlDbType.Int32).Value = adidclase;
                cmd.Parameters.Add("_idcurso", MySqlDbType.Int32).Value = adidcurso;
                cmd.Parameters.Add("_i_semana", MySqlDbType.Int32).Value = adidsemana;
                cmd.Parameters.Add("_v_nombre", MySqlDbType.VarChar, 100).Value = adnombre;
                cmd.Parameters.Add("_v_descripcion", MySqlDbType.VarChar, 100).Value = addescripcion;
                cmd.Parameters.Add("_v_ruta_enlace", MySqlDbType.VarChar, 500).Value = adrutaenlace;
                cmd.Parameters.Add("_v_ruta_video", MySqlDbType.VarChar, 500).Value = adrutavideo;
                cmd.Parameters.Add("_v_imagen_ruta", MySqlDbType.VarChar, 500).Value = adimagenruta;

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
