using MySql.Data.MySqlClient;
using ColegioED;
using System;
using System.Collections.Generic;
using System.Data;

namespace ColegioAD
{
    public class adPago : ad_aglobal
    {
        public adPago(MySqlConnection cn)
        {
            cnMysql = cn;
        }

        public int adActualizarUsuarioClave(int adidusuario, string adnuevaclave)
        {
            try
            {
                int result = -2;
                MySqlCommand cmd = new MySqlCommand("sp_actualizar_usuario_clave", cnMysql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("_idusuario", MySqlDbType.Int32).Value = adidusuario;
                cmd.Parameters.Add("_nuevaclave", MySqlDbType.VarChar, 50).Value = adnuevaclave;

                result = Convert.ToInt32(cmd.ExecuteScalar());

                return result;
            }
            catch (Exception ex)
            {
                //utllog.towrite(utlconstantes.tprocessad, utlconstantes.lognamespace_tprocessad, this.gettype().name.tostring(), methodbase.getcurrentmethod().name, utlconstantes.logtipoerror, "", ex.stacktrace.tostring(), ex.message.tostring());
                throw ex;
            }
        }

        public List<edPago> adListarPago(string adcuenta, int adidusuario, int adidnivel, int adidgrado
                    , int adidcurso, Int16 adivigente, string adnombre, string adfechaini, string adfechafin
                    , int adimes, int adianio)
        {
            try
            {
                if (adcuenta == null)
                {
                    adcuenta = "";
                }
                if (adnombre == null)
                {
                    adnombre = "";
                }
                List<edPago> slenUsuario = new List<edPago>();
                using (MySqlCommand cmd = new MySqlCommand("sp_listar_pago", cnMysql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("_cuenta", MySqlDbType.VarChar, 25).Value = adcuenta;
                    cmd.Parameters.Add("_idusuario", MySqlDbType.Int32).Value = adidusuario;
                    cmd.Parameters.Add("_idnivel", MySqlDbType.Int32).Value = adidnivel;
                    cmd.Parameters.Add("_idgrado", MySqlDbType.Int32).Value = adidgrado;
                    cmd.Parameters.Add("_idcurso", MySqlDbType.Int32).Value = adidcurso;
                    cmd.Parameters.Add("_b_vigente", MySqlDbType.Bit).Value = adivigente;
                    cmd.Parameters.Add("_nombre", MySqlDbType.VarChar, 50).Value = adnombre;
                    cmd.Parameters.Add("_fechaini", MySqlDbType.VarChar, 25).Value = adfechaini;
                    cmd.Parameters.Add("_fechafin", MySqlDbType.VarChar, 25).Value = adfechafin;
                    cmd.Parameters.Add("_i_mes", MySqlDbType.Int32).Value = adimes;
                    cmd.Parameters.Add("_i_anio", MySqlDbType.Int32).Value = adianio;

                    using (MySqlDataReader mdrd = cmd.ExecuteReader())
                    {
                        if (mdrd != null)
                        {
                            edPago enUsuario = null;
                            int pos_idpago = mdrd.GetOrdinal("idpago");
                            int pos_idusuario = mdrd.GetOrdinal("idusuario");
                            int pos_vnombres = mdrd.GetOrdinal("v_nombres");
                            int pos_vapellidopaterno = mdrd.GetOrdinal("v_apellido_paterno");
                            int pos_vapellidomaterno = mdrd.GetOrdinal("v_apellido_materno");
                            int pos_usuario = mdrd.GetOrdinal("usuario");
                            int pos_idnivel = mdrd.GetOrdinal("idnivel");
                            int pos_nivelNombre = mdrd.GetOrdinal("nivelNombre");
                            int pos_idcurso = mdrd.GetOrdinal("idcurso");
                            int pos_nombrecurso = mdrd.GetOrdinal("nombreCurso");
                            int pos_vdescripcion = mdrd.GetOrdinal("v_descripcion");
                            int pos_vusuario = mdrd.GetOrdinal("v_usuario");
                            int pos_idia = mdrd.GetOrdinal("i_dia");
                            int pos_imes = mdrd.GetOrdinal("i_mes");
                            int pos_ianio = mdrd.GetOrdinal("i_anio");
                            int pos_vhora = mdrd.GetOrdinal("v_hora");
                            int pos_vfechainipago = mdrd.GetOrdinal("v_fecha_ini_pago");
                            int pos_vfechafinpago = mdrd.GetOrdinal("v_fecha_fin_pago");
                            int pos_bvigente = mdrd.GetOrdinal("b_vigente");
                            int pos_dtfecharegistro = mdrd.GetOrdinal("v_fecharegistro");

                            while (mdrd.Read())
                            {
                                enUsuario = new edPago();
                                enUsuario.idpago = (mdrd.IsDBNull(pos_idpago) ? 0 : mdrd.GetInt32(pos_idpago));
                                enUsuario.idusuario = (mdrd.IsDBNull(pos_idusuario) ? 0 : mdrd.GetInt32(pos_idusuario));
                                enUsuario.Snombres = (mdrd.IsDBNull(pos_vnombres) ? "-" : mdrd.GetString(pos_vnombres));
                                enUsuario.Sapellido_paterno = (mdrd.IsDBNull(pos_vapellidopaterno) ? "-" : mdrd.GetString(pos_vapellidopaterno));
                                enUsuario.Sapellido_materno = (mdrd.IsDBNull(pos_vapellidomaterno) ? "-" : mdrd.GetString(pos_vapellidomaterno));
                                enUsuario.Susuario = (mdrd.IsDBNull(pos_usuario) ? "-" : mdrd.GetString(pos_usuario));
                                enUsuario.idnivel = (mdrd.IsDBNull(pos_idnivel) ? 0 : mdrd.GetInt32(pos_idnivel));
                                enUsuario.SnivelNombre = (mdrd.IsDBNull(pos_nivelNombre) ? "-" : mdrd.GetString(pos_nivelNombre));
                                enUsuario.idcurso = (mdrd.IsDBNull(pos_idcurso) ? 0 : mdrd.GetInt32(pos_idcurso));
                                enUsuario.SnombreCurso = (mdrd.IsDBNull(pos_nombrecurso) ? "-" : mdrd.GetString(pos_nombrecurso));
                                enUsuario.Sdescripcion = (mdrd.IsDBNull(pos_vdescripcion) ? "-" : mdrd.GetString(pos_vdescripcion));
                                enUsuario.SusuarioAcceso = (mdrd.IsDBNull(pos_vusuario) ? "-" : mdrd.GetString(pos_vusuario));
                                enUsuario.Idia = (mdrd.IsDBNull(pos_idia) ? 0 : mdrd.GetInt32(pos_idia));
                                enUsuario.Imes = (mdrd.IsDBNull(pos_imes) ? 0 : mdrd.GetInt32(pos_imes));
                                enUsuario.Ianio = (mdrd.IsDBNull(pos_ianio) ? 0 : mdrd.GetInt32(pos_ianio));
                                enUsuario.Shora = (mdrd.IsDBNull(pos_vhora) ? "-" : mdrd.GetString(pos_vhora));
                                enUsuario.Sfecha_ini_pago = (mdrd.IsDBNull(pos_vfechainipago) ? "-" : mdrd.GetString(pos_vfechainipago));
                                enUsuario.Sfecha_fin_pago = (mdrd.IsDBNull(pos_vfechafinpago) ? "-" : mdrd.GetString(pos_vfechafinpago));
                                enUsuario.Ivigente = (mdrd.IsDBNull(pos_bvigente) ? 0 : mdrd.GetInt16(pos_bvigente));
                                enUsuario.Sfecharegistro = (mdrd.IsDBNull(pos_dtfecharegistro) ? "-" : mdrd.GetString(pos_dtfecharegistro));
                                slenUsuario.Add(enUsuario);
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

        public List<edPago> adListarPagoDetalle(int adidpago, int adbactivo, int adbestado, string adfechaini, string adfechafin)
        {
            try
            {
                List<edPago> slenUsuario = new List<edPago>();
                using (MySqlCommand cmd = new MySqlCommand("sp_listar_pago_detalle", cnMysql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("_idpago", MySqlDbType.Int32).Value = adidpago;
                    cmd.Parameters.Add("_b_activo", MySqlDbType.Int32).Value = adbactivo;
                    cmd.Parameters.Add("_b_estado", MySqlDbType.Int32).Value = adbestado;
                    cmd.Parameters.Add("_fechaini", MySqlDbType.VarChar, 25).Value = adfechaini;
                    cmd.Parameters.Add("_fechafin", MySqlDbType.VarChar, 25).Value = adfechafin;

                    using (MySqlDataReader mdrd = cmd.ExecuteReader())
                    {
                        if (mdrd != null)
                        {
                            edPago enUsuario = null;
                            int pos_idpagodetalle = mdrd.GetOrdinal("idpagodetalle");
                            int pos_idpago = mdrd.GetOrdinal("idpago");
                            int pos_operaciondata = mdrd.GetOrdinal("v_operacion_data");
                            int pos_itipopago = mdrd.GetOrdinal("i_tipopago");
                            int pos_itipomoneda = mdrd.GetOrdinal("i_tipomoneda");
                            int pos_vdescripcion = mdrd.GetOrdinal("v_descripcion");
                            int pos_idia = mdrd.GetOrdinal("i_dia");
                            int pos_imes = mdrd.GetOrdinal("i_mes");
                            int pos_ianio = mdrd.GetOrdinal("i_anio");
                            int pos_dmonto = mdrd.GetOrdinal("d_monto");
                            int pos_vimgruta1 = mdrd.GetOrdinal("v_img_ruta_1");
                            int pos_vimgruta2 = mdrd.GetOrdinal("v_img_ruta_2");
                            int pos_pasarelapagonombre = mdrd.GetOrdinal("v_pasarela_pago_nombre");
                            int pos_bestado = mdrd.GetOrdinal("b_estado");
                            int pos_vfecharegistro = mdrd.GetOrdinal("v_fecharegistro");
                            int pos_dtfechapago = mdrd.GetOrdinal("v_dt_fechapago");

                            while (mdrd.Read())
                            {
                                enUsuario = new edPago();
                                enUsuario.idpagodetalle = (mdrd.IsDBNull(pos_idpagodetalle) ? 0 : mdrd.GetInt32(pos_idpagodetalle));
                                enUsuario.idpago = (mdrd.IsDBNull(pos_idpago) ? 0 : mdrd.GetInt32(pos_idpago));
                                enUsuario.Soperacion = (mdrd.IsDBNull(pos_operaciondata) ? "-" : mdrd.GetString(pos_operaciondata));
                                enUsuario.Itipopago = (mdrd.IsDBNull(pos_itipopago) ? 0 : mdrd.GetInt32(pos_itipopago));
                                enUsuario.Itipomoneda = (mdrd.IsDBNull(pos_itipomoneda) ? 0 : mdrd.GetInt32(pos_itipomoneda));
                                enUsuario.Sdescripcion = (mdrd.IsDBNull(pos_vdescripcion) ? "-" : mdrd.GetString(pos_vdescripcion));
                                enUsuario.Idia = (mdrd.IsDBNull(pos_idia) ? 0 : mdrd.GetInt32(pos_idia));
                                enUsuario.Imes = (mdrd.IsDBNull(pos_imes) ? 0 : mdrd.GetInt32(pos_imes));
                                enUsuario.Ianio = (mdrd.IsDBNull(pos_ianio) ? 0 : mdrd.GetInt32(pos_ianio));
                                enUsuario.Dmonto = (mdrd.IsDBNull(pos_dmonto) ? 0 : mdrd.GetDecimal(pos_dmonto));
                                enUsuario.Simg_ruta_1 = (mdrd.IsDBNull(pos_vimgruta1) ? "-" : mdrd.GetString(pos_vimgruta1));
                                enUsuario.Simg_ruta_2 = (mdrd.IsDBNull(pos_vimgruta2) ? "-" : mdrd.GetString(pos_vimgruta2));
                                enUsuario.Spasarela_pago_nombre = (mdrd.IsDBNull(pos_pasarelapagonombre) ? "-" : mdrd.GetString(pos_pasarelapagonombre));
                                enUsuario.Iestado = (mdrd.IsDBNull(pos_bestado) ? 0 : mdrd.GetInt16(pos_bestado));
                                enUsuario.Sfecharegistro = (mdrd.IsDBNull(pos_vfecharegistro) ? "-" : mdrd.GetString(pos_vfecharegistro));
                                enUsuario.Sfecha_ini_pago = (mdrd.IsDBNull(pos_dtfechapago) ? "-" : mdrd.GetString(pos_dtfechapago));
                                slenUsuario.Add(enUsuario);
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

        public int adRegistrarPago(int adidpago, int adidpagodetalle, int adidusuario, int adidnivel, int adidgrado, int adidcurso, string adoperacion
                , int adtipopago, int adtipomoneda, string addescripcion, int admes, int adanio
                , string adhora, decimal admonto, string adimg_ruta_1, string adimg_ruta_2
            , string adfecha_ini_pago, Int16 adbestado, string adfecharegistro, string adfechafin)
        {
            try
            {
                int result = -2;
                MySqlCommand cmd = new MySqlCommand("sp_registrar_pago", cnMysql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("_idpago", MySqlDbType.Int32).Value = adidpago;
                cmd.Parameters.Add("_idpagodetalle", MySqlDbType.Int32).Value = adidpagodetalle;
                cmd.Parameters.Add("_idusuario", MySqlDbType.Int32).Value = adidusuario;
                cmd.Parameters.Add("_idnivel", MySqlDbType.Int32).Value = adidnivel;
                cmd.Parameters.Add("_idgrado", MySqlDbType.Int32).Value = adidgrado;
                cmd.Parameters.Add("_idcurso", MySqlDbType.Int32).Value = adidcurso;
                cmd.Parameters.Add("_v_operacion_data", MySqlDbType.VarChar, 12).Value = adoperacion;
                cmd.Parameters.Add("_i_tipopago", MySqlDbType.Int32).Value = adtipopago;
                cmd.Parameters.Add("_i_tipomoneda", MySqlDbType.Int32).Value = adtipomoneda;
                cmd.Parameters.Add("_v_descripcion", MySqlDbType.VarChar, 250).Value = addescripcion;
                cmd.Parameters.Add("_i_mes", MySqlDbType.Int32).Value = admes;
                cmd.Parameters.Add("_i_anio", MySqlDbType.Int32).Value = adanio;
                cmd.Parameters.Add("_v_hora", MySqlDbType.VarChar, 12).Value = adhora;
                cmd.Parameters.Add("_d_monto", MySqlDbType.Decimal).Value = admonto;
                cmd.Parameters.Add("_v_img_ruta_1", MySqlDbType.VarChar, 100).Value = adimg_ruta_1;
                cmd.Parameters.Add("_v_img_ruta_2", MySqlDbType.VarChar, 100).Value = adimg_ruta_2;
                cmd.Parameters.Add("_v_fecha_ini_pago", MySqlDbType.VarChar, 25).Value = adfecha_ini_pago;
                cmd.Parameters.Add("_b_estado", MySqlDbType.Bit).Value = adbestado;
                cmd.Parameters.Add("_dt_fecharegistro", MySqlDbType.VarChar, 25).Value = adfecharegistro;
                cmd.Parameters.Add("_dt_fechafin", MySqlDbType.VarChar, 25).Value = adfechafin;

                result = Convert.ToInt32(cmd.ExecuteScalar());

                return result;
            }
            catch (Exception ex)
            {
                //utllog.towrite(utlconstantes.tprocessad, utlconstantes.lognamespace_tprocessad, this.gettype().name.tostring(), methodbase.getcurrentmethod().name, utlconstantes.logtipoerror, "", ex.stacktrace.tostring(), ex.message.tostring());
                throw ex;
            }
        }

        public int adNotificarPago(int adidusuario, int adimes, string adfechaacceso, string adfechavalidar)
        {
            try
            {
                int iresultado = 0;
                using (MySqlCommand cmd = new MySqlCommand("sp_pago_notificar", cnMysql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("_idusuario", MySqlDbType.Int32).Value = adidusuario;
                    cmd.Parameters.Add("_i_mes", MySqlDbType.Int32).Value = adimes;
                    cmd.Parameters.Add("_fechaacceso", MySqlDbType.VarChar, 25).Value = adfechaacceso;
                    cmd.Parameters.Add("_fechavalidar", MySqlDbType.VarChar, 25).Value = adfechavalidar;

                    using (MySqlDataReader mdrd = cmd.ExecuteReader())
                    {
                        if (mdrd != null)
                        {
                            int pos_iresultado = mdrd.GetOrdinal("_result");

                            while (mdrd.Read())
                            {
                                iresultado = (mdrd.IsDBNull(pos_iresultado) ? 0 : mdrd.GetInt32(pos_iresultado));
                            }
                        }
                    }
                    return iresultado;
                }
            }
            catch (Exception ex)
            {
                //UtlLog.toWrite(UtlConstantes.TProcessAD, UtlConstantes.LogNamespace_TProcessAD, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                throw ex;
            }
        }

        public List<edPago> adRptListarSumaPagos(int adidnivel, int adidgrado, int adidcurso
                , string adfechaini, string adfechafin, int admes, int adanio)
        {
            try
            {
                List<edPago> slenUsuario = new List<edPago>();
                using (MySqlCommand cmd = new MySqlCommand("sp_reporte_sumapagos", cnMysql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("_idnivel", MySqlDbType.Int32).Value = adidnivel;
                    cmd.Parameters.Add("_idgrado", MySqlDbType.Int32).Value = adidgrado;
                    cmd.Parameters.Add("_idcurso", MySqlDbType.Int32).Value = adidcurso;
                    cmd.Parameters.Add("_fechaini", MySqlDbType.VarChar, 25).Value = adfechaini;
                    cmd.Parameters.Add("_fechafin", MySqlDbType.VarChar, 25).Value = adfechafin;
                    cmd.Parameters.Add("_i_mes", MySqlDbType.Int32).Value = admes;
                    cmd.Parameters.Add("_i_anio", MySqlDbType.Int32).Value = adanio;

                    using (MySqlDataReader mdrd = cmd.ExecuteReader())
                    {
                        if (mdrd != null)
                        {
                            edPago enUsuario = null;
                            int pos_dmonto = mdrd.GetOrdinal("d_monto");
                            int pos_ipago = mdrd.GetOrdinal("i_pago");
                            int pos_costopromediodec = mdrd.GetOrdinal("costo_promedio_dec");

                            while (mdrd.Read())
                            {
                                enUsuario = new edPago();
                                enUsuario.Dmonto = (mdrd.IsDBNull(pos_dmonto) ? 0 : mdrd.GetDecimal(pos_dmonto));
                                enUsuario.idpago = (mdrd.IsDBNull(pos_ipago) ? 0 : mdrd.GetInt32(pos_ipago));
                                enUsuario.Dcomision_nombre = (mdrd.IsDBNull(pos_costopromediodec) ? 0 : mdrd.GetDecimal(pos_costopromediodec));

                                slenUsuario.Add(enUsuario);
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

        public int adListarUsuarioTotal(int adidnivel, int adidgrado, int adidcurso)
        {
            try
            {
                int iresultado = 0;
                using (MySqlCommand cmd = new MySqlCommand("sp_listar_usuario_total", cnMysql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("_idnivel", MySqlDbType.Int32).Value = adidnivel;
                    cmd.Parameters.Add("_idgrado", MySqlDbType.Int32).Value = adidgrado;
                    cmd.Parameters.Add("_idcurso", MySqlDbType.Int32).Value = adidcurso;

                    using (MySqlDataReader mdrd = cmd.ExecuteReader())
                    {
                        if (mdrd != null)
                        {
                            int pos_iusuario = mdrd.GetOrdinal("i_usuario");

                            while (mdrd.Read())
                            {
                                iresultado = (mdrd.IsDBNull(pos_iusuario) ? 0 : mdrd.GetInt32(pos_iusuario));
                            }
                        }
                    }
                    return iresultado;
                }
            }
            catch (Exception ex)
            {
                //UtlLog.toWrite(UtlConstantes.TProcessAD, UtlConstantes.LogNamespace_TProcessAD, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                throw ex;
            }
        }

        public List<edPago> adListarPagoPendiente(int adidusuario, int adidnivel, int adidgrado
                                                    , int adidcurso, int adbactivo)
        {
            try
            {
                List<edPago> slenUsuario = new List<edPago>();
                using (MySqlCommand cmd = new MySqlCommand("sp_listar_pago_pendiente", cnMysql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("_idusuario", MySqlDbType.Int32).Value = adidusuario;
                    cmd.Parameters.Add("_idnivel", MySqlDbType.Int32).Value = adidnivel;
                    cmd.Parameters.Add("_idgrado", MySqlDbType.Int32).Value = adidgrado;
                    cmd.Parameters.Add("_idcurso", MySqlDbType.Int32).Value = adidcurso;
                    cmd.Parameters.Add("_b_activo", MySqlDbType.Int32).Value = adbactivo;

                    using (MySqlDataReader mdrd = cmd.ExecuteReader())
                    {
                        if (mdrd != null)
                        {
                            edPago enPagodetalle = null;
                            int pos_idpagodetalle = mdrd.GetOrdinal("idpagodetalle");
                            int pos_idpago = mdrd.GetOrdinal("idpago");
                            int pos_idia = mdrd.GetOrdinal("i_dia");
                            int pos_imes = mdrd.GetOrdinal("i_mes");
                            int pos_ianio = mdrd.GetOrdinal("i_anio");
                            while (mdrd.Read())
                            {
                                enPagodetalle = new edPago();
                                enPagodetalle.idpagodetalle = (mdrd.IsDBNull(pos_idpagodetalle) ? 0 : mdrd.GetInt32(pos_idpagodetalle));
                                enPagodetalle.idpago = (mdrd.IsDBNull(pos_idpago) ? 0 : mdrd.GetInt32(pos_idpago));
                                enPagodetalle.Idia = (mdrd.IsDBNull(pos_idia) ? 0 : mdrd.GetInt32(pos_idia));
                                enPagodetalle.Imes = (mdrd.IsDBNull(pos_imes) ? 0 : mdrd.GetInt32(pos_imes));
                                enPagodetalle.Ianio = (mdrd.IsDBNull(pos_ianio) ? 0 : mdrd.GetInt32(pos_ianio));
                                slenUsuario.Add(enPagodetalle);
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

        public List<edPago> adRptListarUsuarioPagos(string adusuario, string adfechaini, string adfechafin, int adidcurso)
        {
            try
            {
                if(adusuario == null )
                {
                    adusuario = "";
                }
                List<edPago> slenPago = new List<edPago>();
                using (MySqlCommand cmd = new MySqlCommand("sp_reporte_usuario_pagos", cnMysql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("_usuario", MySqlDbType.VarChar, 100).Value = adusuario;
                    cmd.Parameters.Add("_fechaini", MySqlDbType.VarChar, 25).Value = adfechaini;
                    cmd.Parameters.Add("_fechafin", MySqlDbType.VarChar, 25).Value = adfechafin;
                    cmd.Parameters.Add("_idcurso", MySqlDbType.Int32).Value = adidcurso;

                    using (MySqlDataReader mdrd = cmd.ExecuteReader())
                    {
                        if (mdrd != null)
                        {
                            edPago enPago = null;
                            int pos_usuario = mdrd.GetOrdinal("APELLIDO_NOMBRES");
                            int pos_curso = mdrd.GetOrdinal("CURSO");
                            int pos_fechapago = mdrd.GetOrdinal("FECHA_PAGO");
                            int pos_monto = mdrd.GetOrdinal("MONTO");
                            int pos_mes = mdrd.GetOrdinal("MESES_QUE_PAGO");
                            int pos_anio = mdrd.GetOrdinal("i_anio");

                            while (mdrd.Read())
                            {
                                enPago = new edPago();
                                //Nombre y apellidos concatenado
                                enPago.Snombres = (mdrd.IsDBNull(pos_usuario) ? "-" : mdrd.GetString(pos_usuario));
                                //Nombre y apellidos concatenado
                                enPago.SnombreCurso = (mdrd.IsDBNull(pos_curso) ? "-" : mdrd.GetString(pos_curso));
                                enPago.Sfecharegistro = (mdrd.IsDBNull(pos_fechapago) ? "-" : mdrd.GetString(pos_fechapago));
                                enPago.Dmonto = (mdrd.IsDBNull(pos_monto) ? 0 : mdrd.GetDecimal(pos_monto));
                                enPago.Imes = (mdrd.IsDBNull(pos_mes) ? 0 : mdrd.GetInt32(pos_mes));
                                enPago.Ianio = (mdrd.IsDBNull(pos_anio) ? 0 : mdrd.GetInt32(pos_anio));
                                slenPago.Add(enPago);
                            }
                        }
                    }
                    return slenPago;
                }
            }
            catch (Exception ex)
            {
                //UtlLog.toWrite(UtlConstantes.TProcessAD, UtlConstantes.LogNamespace_TProcessAD, this.GetType().Name.ToString(), MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                throw ex;
            }
        }

        public List<edPago> adRptVentasTotales()
        {
            try
            {
                List<edPago> slenPago = new List<edPago>();
                using (MySqlCommand cmd = new MySqlCommand("sp_reporte_ventas_totales", cnMysql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (MySqlDataReader mdrd = cmd.ExecuteReader())
                    {
                        if (mdrd != null)
                        {
                            edPago enPago = null;
                            int pos_monto_total = mdrd.GetOrdinal("MONTO_TOTAL");
                            int pos_idcurso = mdrd.GetOrdinal("ID_CURSO");
                            int pos_curso = mdrd.GetOrdinal("CURSO");

                            while (mdrd.Read())
                            {
                                enPago = new edPago();
                                enPago.Dmonto = (mdrd.IsDBNull(pos_monto_total) ? 0 : mdrd.GetDecimal(pos_monto_total));
                                enPago.idcurso = (mdrd.IsDBNull(pos_idcurso) ? 0 : mdrd.GetInt32(pos_idcurso));
                                enPago.SnombreCurso = (mdrd.IsDBNull(pos_curso) ? "-" : mdrd.GetString(pos_curso));
                                slenPago.Add(enPago);
                            }
                        }
                    }
                    return slenPago;
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
