using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColegioAD
{
    public class adAsistencia : ad_aglobal
    {

        public adAsistencia(MySqlConnection cn)
        {
            cnMysql = cn;
        }

        public int adInsertarAsistencia(int idclase, int idtipoasistencia, 
                                        string fechaingreso, string observacion)
        {
            try
            {
                int result = -2;
                MySqlCommand cmd = new MySqlCommand("sp_insertar_asistencia", cnMysql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("_idclase", MySqlDbType.Int32).Value = idclase;
                cmd.Parameters.Add("_idtipoasistencia", MySqlDbType.Int32).Value = idtipoasistencia;
                cmd.Parameters.Add("_dt_fechaingreso", MySqlDbType.VarChar,10).Value = fechaingreso;
                cmd.Parameters.Add("_dt_fecharegistro", MySqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("_b_estado", MySqlDbType.Bit).Value = 1;
                cmd.Parameters.Add("_v_observacion", MySqlDbType.VarChar, 50).Value = observacion;
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
