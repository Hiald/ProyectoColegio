using MySql.Data.MySqlClient;
using ColegioED;
using System;
using System.Collections.Generic;
using System.Data;

namespace ColegioAD
{
    public class adArchivo : ad_aglobal
    {
        public adArchivo(MySqlConnection cn)
        {
            cnMysql = cn;
        }

        public int adInsertarArchivo(int adidclase, int adidusuario, string adnombre
                               , string adrutaenlace, int adtipoarchivo, string adfechainicio
                               , string adfechafin, string addescripcion, string adrutaarchivo)
        {
            try
            {
                int result = -1;
                MySqlCommand cmd = new MySqlCommand("sp_insertar_archivo", cnMysql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("_idclase", MySqlDbType.Int32).Value = adidclase;
                cmd.Parameters.Add("_idusuario", MySqlDbType.Int32).Value = adidusuario;
                cmd.Parameters.Add("_nombre", MySqlDbType.VarChar, 50).Value = adnombre;
                cmd.Parameters.Add("_enlace", MySqlDbType.VarChar, 500).Value = adrutaenlace;
                cmd.Parameters.Add("_tipoarchivo", MySqlDbType.Int32).Value = adtipoarchivo;
                cmd.Parameters.Add("_puntajeminimo", MySqlDbType.Int32).Value = 0;
                cmd.Parameters.Add("_puntajemaximo", MySqlDbType.Int32).Value = 0;
                cmd.Parameters.Add("_fechainicio", MySqlDbType.DateTime).Value = adfechainicio;
                cmd.Parameters.Add("_fechafin", MySqlDbType.DateTime).Value = adfechafin;
                cmd.Parameters.Add("_v_descripcion", MySqlDbType.VarChar, 500).Value = addescripcion;
                cmd.Parameters.Add("_v_ruta_archivo", MySqlDbType.VarChar, 150).Value = adrutaarchivo;
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

        public List<edArchivo> adObtenerArchivo(int adidclase)
        {
            try
            {
                List<edArchivo> slenUsuario = new List<edArchivo>();
                using (MySqlCommand cmd = new MySqlCommand("sp_obtener_archivo", cnMysql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("_idclase", MySqlDbType.Int32).Value = adidclase;

                    using (MySqlDataReader mdrd = cmd.ExecuteReader())
                    {
                        if (mdrd != null)
                        {
                            edArchivo senArchivo = null;
                            int pos_idarchivo = mdrd.GetOrdinal("idarchivo");
                            int pos_idclase = mdrd.GetOrdinal("idclase");
                            int pos_venlace = mdrd.GetOrdinal("v_enlace");
                            int pos_vnombreclase = mdrd.GetOrdinal("nombreClase");
                            int pos_itipoarchivo = mdrd.GetOrdinal("i_tipo_archivo");
                            int pos_ipuntajeminimo = mdrd.GetOrdinal("i_puntaje_minimo");
                            int pos_ipuntajemaximo = mdrd.GetOrdinal("i_puntaje_maximo");
                            int pos_vdescripcion = mdrd.GetOrdinal("v_descripcion");
                            int pos_vrutaarchivo = mdrd.GetOrdinal("v_ruta_archivo");
                            int pos_dfechainicio = mdrd.GetOrdinal("d_fecha_inicio");
                            int pos_dfechafin = mdrd.GetOrdinal("d_fecha_fin");
                            int pos_vimagen = mdrd.GetOrdinal("v_imagen");
                            int pos_bestado = mdrd.GetOrdinal("b_estado");
                            int pos_dtfecharegistro = mdrd.GetOrdinal("dt_fecharegistro");
                            int pos_isemana = mdrd.GetOrdinal("i_semana");

                            while (mdrd.Read())
                            {
                                senArchivo = new edArchivo();
                                senArchivo.idarchivo = (mdrd.IsDBNull(pos_idarchivo) ? 0 : mdrd.GetInt32(pos_idarchivo));
                                senArchivo.idclase = (mdrd.IsDBNull(pos_idclase) ? 0 : mdrd.GetInt32(pos_idclase));
                                senArchivo.Senlace = (mdrd.IsDBNull(pos_venlace) ? "-" : mdrd.GetString(pos_venlace));
                                senArchivo.SclaseNombre = (mdrd.IsDBNull(pos_vnombreclase) ? "-" : mdrd.GetString(pos_vnombreclase));
                                senArchivo.Itipoarchivo = (mdrd.IsDBNull(pos_itipoarchivo) ? 0 : mdrd.GetInt32(pos_itipoarchivo));
                                senArchivo.IPuntajeMinimo = (mdrd.IsDBNull(pos_ipuntajeminimo) ? 0 : mdrd.GetInt32(pos_ipuntajeminimo));
                                senArchivo.IpuntajeMaximo = (mdrd.IsDBNull(pos_ipuntajemaximo) ? 0 : mdrd.GetInt32(pos_ipuntajemaximo));
                                senArchivo.Sobservacion = (mdrd.IsDBNull(pos_vdescripcion) ? "-" : mdrd.GetString(pos_vdescripcion));
                                senArchivo.SenlaceAlumno = (mdrd.IsDBNull(pos_vrutaarchivo) ? "-" : mdrd.GetString(pos_vrutaarchivo));
                                senArchivo.SfechaInicio = (mdrd.IsDBNull(pos_dfechainicio) ? "-" : mdrd.GetString(pos_dfechainicio));
                                senArchivo.SfechaFin = (mdrd.IsDBNull(pos_dfechafin) ? "-" : mdrd.GetString(pos_dfechafin));
                                senArchivo.Simagen = (mdrd.IsDBNull(pos_vimagen) ? "-" : mdrd.GetString(pos_vimagen));
                                senArchivo.Bestado = (mdrd.IsDBNull(pos_bestado) ? 0 : mdrd.GetInt16(pos_bestado));
                                senArchivo.SfechaRegistro = (mdrd.IsDBNull(pos_dtfecharegistro) ? "-" : mdrd.GetString(pos_dtfecharegistro));
                                senArchivo.isemana = (mdrd.IsDBNull(pos_isemana) ? 0 : mdrd.GetInt32(pos_isemana));
                                slenUsuario.Add(senArchivo);
                            }
                        }
                    }
                    return slenUsuario;
                }
            }
            catch (Exception ex)
            {
                //UtlLog.toWrite(UtlConstantes.TProcessAD, UtlConstantes.LogNamespace_TProcessAD, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                throw ex;
            }
        }

        public int adInsertarArchivoDetalle(int adidarchivo, int adidusuario, string adimagen, string adnota
                    , string adobservacion, string addescripcion, string adenlace)
        {
            try
            {
                int result = -1;
                MySqlCommand cmd = new MySqlCommand("sp_insertar_archivo_detalle", cnMysql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("_idarchivo", MySqlDbType.Int32).Value = adidarchivo;
                cmd.Parameters.Add("_idusuario", MySqlDbType.Int32).Value = adidusuario;
                cmd.Parameters.Add("_imagen", MySqlDbType.VarChar, 150).Value = adimagen;
                cmd.Parameters.Add("_nota", MySqlDbType.VarChar, 10).Value = adnota;
                cmd.Parameters.Add("_observacion", MySqlDbType.VarChar, 500).Value = adobservacion;
                cmd.Parameters.Add("_v_descripcion", MySqlDbType.VarChar, 500).Value = addescripcion;
                cmd.Parameters.Add("_enlace", MySqlDbType.VarChar, 500).Value = adenlace;
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

        public int adActualizarArchivoDetalle(int adidarchivodetalle, string adnota, string adobservacion, int adidusuario
                                        , int adtiponota, Int16 adestado, DateTime adfecharegistro)
        {
            try
            {
                int result = -1;
                MySqlCommand cmd = new MySqlCommand("sp_insertar_archivo_detalle", cnMysql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("_idarchivodetalle", MySqlDbType.Int32).Value = adidarchivodetalle;
                cmd.Parameters.Add("_nota", MySqlDbType.VarChar, 10).Value = adnota;
                cmd.Parameters.Add("_observacion", MySqlDbType.VarChar, 500).Value = adobservacion;
                cmd.Parameters.Add("_idusuario", MySqlDbType.Int32).Value = adidusuario;
                cmd.Parameters.Add("_tiponota", MySqlDbType.Int32).Value = adtiponota;
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

        public List<edArchivo> adListarArchivoDetalle(int adidarchivo, int adidusuario)
        {
            try
            {
                List<edArchivo> loenusuario = new List<edArchivo>();
                using (MySqlCommand cmd = new MySqlCommand("sp_listar_archivo_detalle", cnMysql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("_idarchivo", MySqlDbType.Int32).Value = adidarchivo;
                    cmd.Parameters.Add("_idusuario", MySqlDbType.Int32).Value = adidusuario;

                    using (MySqlDataReader mdrd = cmd.ExecuteReader())
                    {
                        if (mdrd != null)
                        {
                            edArchivo senUsuario = null;
                            int pos_idarchivodetalle = mdrd.GetOrdinal("idarchivodetalle");
                            int pos_idarchivo = mdrd.GetOrdinal("idarchivo");
                            int pos_idusuario = mdrd.GetOrdinal("idusuario");
                            int pos_vimagen = mdrd.GetOrdinal("v_imagen");
                            int pos_vdescripcion = mdrd.GetOrdinal("v_descripcion");
                            int pos_inota = mdrd.GetOrdinal("i_nota");
                            int pos_vobservacion = mdrd.GetOrdinal("v_observacion");
                            int pos_venlace = mdrd.GetOrdinal("v_enlace");

                            while (mdrd.Read())
                            {
                                senUsuario = new edArchivo();
                                senUsuario.idarchivodetalle = (mdrd.IsDBNull(pos_idarchivodetalle) ? 0 : mdrd.GetInt32(pos_idarchivodetalle));
                                senUsuario.idarchivo = (mdrd.IsDBNull(pos_idarchivo) ? 0 : mdrd.GetInt32(pos_idarchivo));
                                senUsuario.idusuario = (mdrd.IsDBNull(pos_idusuario) ? 0 : mdrd.GetInt32(pos_idusuario));
                                senUsuario.Simagen = (mdrd.IsDBNull(pos_vimagen) ? "-" : mdrd.GetString(pos_vimagen));
                                senUsuario.SenlaceAlumno = (mdrd.IsDBNull(pos_vdescripcion) ? "-" : mdrd.GetString(pos_vdescripcion));
                                senUsuario.Inota = (mdrd.IsDBNull(pos_inota) ? 0 : mdrd.GetInt32(pos_inota));
                                senUsuario.Sobservacion = (mdrd.IsDBNull(pos_vobservacion) ? "-" : mdrd.GetString(pos_vobservacion));
                                senUsuario.Senlace = (mdrd.IsDBNull(pos_venlace) ? "-" : mdrd.GetString(pos_venlace));
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

        public List<edArchivo> adListarArchivo(int adidclase, int idgrado, int idnivel, int idcurso)
        {
            try
            {
                List<edArchivo> loenusuario = new List<edArchivo>();
                using (MySqlCommand cmd = new MySqlCommand("sp_listar_archivo", cnMysql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("_idclase", MySqlDbType.Int32).Value = adidclase;
                    cmd.Parameters.Add("_idgrado", MySqlDbType.Int32).Value = idgrado;
                    cmd.Parameters.Add("_idnivel", MySqlDbType.Int32).Value = idnivel;
                    cmd.Parameters.Add("_idcurso", MySqlDbType.Int32).Value = idcurso;

                    using (MySqlDataReader mdrd = cmd.ExecuteReader())
                    {
                        if (mdrd != null)
                        {
                            edArchivo senUsuario = null;
                            int pos_idarchivo = mdrd.GetOrdinal("idarchivo");
                            int pos_idclase = mdrd.GetOrdinal("idclase");
                            int pos_idusuario = mdrd.GetOrdinal("idusuario");
                            int pos_vnombres = mdrd.GetOrdinal("v_nombres");
                            int pos_vapellidomaterno = mdrd.GetOrdinal("v_apellido_materno");
                            int pos_vapellidopaterno = mdrd.GetOrdinal("v_apellido_paterno");
                            int pos_nombregrado = mdrd.GetOrdinal("nombreGrado");
                            int pos_nombrenivel = mdrd.GetOrdinal("nombreNivel");
                            int pos_vnombre = mdrd.GetOrdinal("v_nombre");
                            int pos_venlace = mdrd.GetOrdinal("v_enlace");
                            int pos_itipoarchivo = mdrd.GetOrdinal("i_tipo_archivo");
                            int pos_ipuntajeminimo = mdrd.GetOrdinal("i_puntaje_minimo");
                            int pos_ipuntajemaximo = mdrd.GetOrdinal("i_puntaje_maximo");
                            int pos_dfechainicio = mdrd.GetOrdinal("d_fecha_inicio");
                            int pos_dfechafin = mdrd.GetOrdinal("d_fecha_fin");
                            int pos_vimagen = mdrd.GetOrdinal("v_imagen");
                            int pos_bestado = mdrd.GetOrdinal("b_estado");
                            int pos_dtfecharegistro = mdrd.GetOrdinal("dt_fecharegistro");
                            int pos_idarchivodetalle = mdrd.GetOrdinal("idarchivodetalle");
                            int pos_imagenalumno = mdrd.GetOrdinal("imagenAlumno");
                            int pos_inota = mdrd.GetOrdinal("i_nota");
                            int pos_vobservacion = mdrd.GetOrdinal("v_observacion");
                            int pos_enlacealumno = mdrd.GetOrdinal("enlaceAlumno");
                            int pos_idcurso = mdrd.GetOrdinal("idcurso");
                            int pos_nombrecurso = mdrd.GetOrdinal("nombreCurso");
                            int pos_descripcion = mdrd.GetOrdinal("v_descripcion");

                            while (mdrd.Read())
                            {
                                senUsuario = new edArchivo();
                                senUsuario.idarchivo = (mdrd.IsDBNull(pos_idarchivo) ? 0 : mdrd.GetInt32(pos_idarchivo));
                                senUsuario.idclase = (mdrd.IsDBNull(pos_idclase) ? 0 : mdrd.GetInt32(pos_idclase));
                                senUsuario.idusuario = (mdrd.IsDBNull(pos_idusuario) ? 0 : mdrd.GetInt32(pos_idusuario));
                                senUsuario.SNombre = (mdrd.IsDBNull(pos_vnombres) ? "-" : mdrd.GetString(pos_vnombres));
                                senUsuario.SApellidoMaterno = (mdrd.IsDBNull(pos_vapellidomaterno) ? "-" : mdrd.GetString(pos_vapellidomaterno));
                                senUsuario.SApellidoPaterno = (mdrd.IsDBNull(pos_vapellidopaterno) ? "-" : mdrd.GetString(pos_vapellidopaterno));
                                senUsuario.SNombreGrado = (mdrd.IsDBNull(pos_nombregrado) ? "-" : mdrd.GetString(pos_nombregrado));
                                senUsuario.SNombreNivel = (mdrd.IsDBNull(pos_nombrenivel) ? "-" : mdrd.GetString(pos_nombrenivel));
                                senUsuario.SNombreArchivo = (mdrd.IsDBNull(pos_vnombre) ? "-" : mdrd.GetString(pos_vnombre));
                                senUsuario.Senlace = (mdrd.IsDBNull(pos_venlace) ? "-" : mdrd.GetString(pos_venlace));
                                senUsuario.Itipoarchivo = (mdrd.IsDBNull(pos_itipoarchivo) ? 0 : mdrd.GetInt32(pos_itipoarchivo));
                                senUsuario.IPuntajeMinimo = (mdrd.IsDBNull(pos_ipuntajeminimo) ? 0 : mdrd.GetInt32(pos_ipuntajeminimo));
                                senUsuario.IpuntajeMaximo = (mdrd.IsDBNull(pos_ipuntajemaximo) ? 0 : mdrd.GetInt32(pos_ipuntajemaximo));
                                senUsuario.SfechaInicio = (mdrd.IsDBNull(pos_dfechainicio) ? "-" : mdrd.GetString(pos_dfechainicio));
                                senUsuario.SfechaFin = (mdrd.IsDBNull(pos_dfechafin) ? "-" : mdrd.GetString(pos_dfechafin));
                                senUsuario.Simagen = (mdrd.IsDBNull(pos_vimagen) ? "-" : mdrd.GetString(pos_vimagen));
                                senUsuario.Bestado = (mdrd.IsDBNull(pos_bestado) ? 0 : mdrd.GetInt16(pos_bestado));
                                senUsuario.SfechaRegistro = (mdrd.IsDBNull(pos_dtfecharegistro) ? "-" : mdrd.GetString(pos_dtfecharegistro));
                                senUsuario.idarchivodetalle = (mdrd.IsDBNull(pos_idarchivodetalle) ? 0 : mdrd.GetInt32(pos_idarchivodetalle));
                                senUsuario.SimagenAlumno = (mdrd.IsDBNull(pos_imagenalumno) ? "-" : mdrd.GetString(pos_imagenalumno));
                                senUsuario.Inota = (mdrd.IsDBNull(pos_inota) ? 0 : mdrd.GetInt32(pos_inota));
                                senUsuario.Sobservacion = (mdrd.IsDBNull(pos_vobservacion) ? "-" : mdrd.GetString(pos_vobservacion));
                                senUsuario.SenlaceAlumno = (mdrd.IsDBNull(pos_enlacealumno) ? "-" : mdrd.GetString(pos_enlacealumno));
                                senUsuario.idcurso = (mdrd.IsDBNull(pos_idcurso) ? 0 : mdrd.GetInt32(pos_idcurso));
                                senUsuario.snombreCurso = (mdrd.IsDBNull(pos_nombrecurso) ? "-" : mdrd.GetString(pos_nombrecurso));
                                senUsuario.sdescripcion = (mdrd.IsDBNull(pos_descripcion) ? "-" : mdrd.GetString(pos_descripcion));

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

        public int adActualizarArchivo(int aditipoactualizar, int adidarchivo, string adnombre, string adenlace
                                       , string aditipoarchivo, int adipuntajeminimo, int adipuntajemaximo
                                       , DateTime? adfechainicio, DateTime? adfechafin)
        {
            try
            {
                int result = -1;
                MySqlCommand cmd = new MySqlCommand("sp_actualizar_archivo", cnMysql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("_tipoactualizar", MySqlDbType.Int32).Value = aditipoactualizar;
                cmd.Parameters.Add("_idarchivo", MySqlDbType.Int32).Value = adidarchivo;
                cmd.Parameters.Add("_v_nombre", MySqlDbType.VarChar, 50).Value = adnombre;
                cmd.Parameters.Add("_v_enlace", MySqlDbType.VarChar, 500).Value = adenlace;
                cmd.Parameters.Add("_i_tipo_archivo", MySqlDbType.Int32).Value = aditipoarchivo;
                cmd.Parameters.Add("_i_puntaje_minimo", MySqlDbType.Int32).Value = adipuntajeminimo;
                cmd.Parameters.Add("_i_puntaje_maximo", MySqlDbType.Int32).Value = adipuntajemaximo;
                cmd.Parameters.Add("_d_fecha_inicio", MySqlDbType.DateTime).Value = adfechainicio;
                cmd.Parameters.Add("_d_fecha_fin", MySqlDbType.DateTime).Value = adfechafin;

                result = Convert.ToInt32(cmd.ExecuteScalar());

                return result;
            }
            catch (Exception ex)
            {
                //utllog.towrite(utlconstantes.tprocessad, utlconstantes.lognamespace_tprocessad, this.gettype().name.tostring(), methodbase.getcurrentmethod().name, utlconstantes.logtipoerror, "", ex.stacktrace.tostring(), ex.message.tostring());
                throw ex;
            }
        }

        public int adActualizarArchivoDetalle(int aditipoactualizar, int adidarchivodetalle, int adidarchivo
                                        , int adidusuario, string adidnota, string adobservacion, string adenlace)
        {
            try
            {
                int result = -1;
                MySqlCommand cmd = new MySqlCommand("sp_actualizar_archivo_detalle", cnMysql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("_tipoactualizar", MySqlDbType.Int32).Value = aditipoactualizar;
                cmd.Parameters.Add("_idarchivodetalle", MySqlDbType.Int32).Value = adidarchivodetalle;
                cmd.Parameters.Add("_idarchivo", MySqlDbType.Int32).Value = adidarchivo;
                cmd.Parameters.Add("_idusuario", MySqlDbType.Int32).Value = adidusuario;
                cmd.Parameters.Add("_i_nota", MySqlDbType.VarChar, 10).Value = adidnota;
                cmd.Parameters.Add("_v_observacion", MySqlDbType.VarChar, 500).Value = adobservacion;
                cmd.Parameters.Add("_v_enlace", MySqlDbType.VarChar, 500).Value = adenlace;

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
