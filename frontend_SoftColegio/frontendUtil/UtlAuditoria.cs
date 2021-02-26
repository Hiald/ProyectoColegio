using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Web;

namespace frontendUtil
{
    public class UtlAuditoria
    {
        private const string SESSION_IDUSUARIO = "SESSION_IDALUMNO";
        private const string SESSION_IDNIVEL = "SESSION_SNOMBRE";
        private const string SESSION_IDGRADO = "SESSION_SEMAIL";
        private const string SESSION_IDSEDE = "SESSION_IETAPAESCOLAR";
        private const string SESSION_IDSECCION = "SESSION_IGRADO";
        private const string SESSION_NOMBRE = "SESSION_ISECCION";
        private const string SESSION_APELLIDOPARTERNO = "SESSION_ITIPODOC";
        private const string SESSION_APELLIDOMATERNO = "SESSION_SDOCUMENTO";
        private const string SESSION_CORREO = "SESSION_ISUSCRIPCIONESTADO";
        private const string SESSION_IMATRICULADO = "SESSION_IMATRICULADO";
        private const string SESSION_TIPOUSUARIO = "SESSION_TIPOUSUARIO";

        #region "Obtiene Datos del Usuario"

        public static int ObtenerIdUsuario()
        {
            return ((HttpContext.Current.Session[SESSION_IDUSUARIO] == null) ? -1 : int.Parse(HttpContext.Current.Session[SESSION_IDUSUARIO].ToString()));
        }
        public static int ObtenerIdNivel()
        {
            return ((HttpContext.Current.Session[SESSION_IDNIVEL] == null) ? -1 : int.Parse(HttpContext.Current.Session[SESSION_IDNIVEL].ToString()));
        }
        public static int ObtenerIdGrado()
        {
            return ((HttpContext.Current.Session[SESSION_IDGRADO] == null) ? -1 : int.Parse(HttpContext.Current.Session[SESSION_IDGRADO].ToString()));
        }
        public static int ObtenerIdSede()
        {
            return ((HttpContext.Current.Session[SESSION_IDSEDE] == null) ? -1 : int.Parse(HttpContext.Current.Session[SESSION_IDSEDE].ToString()));
        }
        public static int ObtenerIdSeccion()
        {
            return ((HttpContext.Current.Session[SESSION_IDSECCION] == null) ? -1 : int.Parse(HttpContext.Current.Session[SESSION_IDSECCION].ToString()));
        }
        public static string ObtenerNombre()
        {
            return ((HttpContext.Current.Session[SESSION_NOMBRE] == null) ? "" : HttpContext.Current.Session[SESSION_NOMBRE].ToString());
        }
        public static string ObtenerApellidoPaterno()
        {
            return ((HttpContext.Current.Session[SESSION_APELLIDOPARTERNO] == null) ? "" : HttpContext.Current.Session[SESSION_APELLIDOPARTERNO].ToString());
        }
        public static string ObtenerApellidoMaterno()
        {
            return ((HttpContext.Current.Session[SESSION_APELLIDOMATERNO] == null) ? "" : HttpContext.Current.Session[SESSION_APELLIDOMATERNO].ToString());
        }
        public static string ObtenerCorreo()
        {
            return ((HttpContext.Current.Session[SESSION_CORREO] == null) ? "-" : HttpContext.Current.Session[SESSION_CORREO].ToString());
        }
        public static int ObtenerMatriculado()
        {
            return ((HttpContext.Current.Session[SESSION_IMATRICULADO] == null) ? -1 : int.Parse(HttpContext.Current.Session[SESSION_IMATRICULADO].ToString()));
        }
        public static int ObtenerTipoUsuario()
        {
            return ((HttpContext.Current.Session[SESSION_TIPOUSUARIO] == null) ? -1 : int.Parse(HttpContext.Current.Session[SESSION_TIPOUSUARIO].ToString()));
        }
        public static string ObtenerFechaSistema()
        {
            return DateTime.Now.ToShortDateString();
        }

        public static void SetSessionValues(Dictionary<string, string> DVariables)
        {
            try
            {
                HttpContext.Current.Session[SESSION_IDUSUARIO] = DVariables["IDUSUARIO"];
                HttpContext.Current.Session[SESSION_IDNIVEL] = DVariables["IDNIVEL"];
                HttpContext.Current.Session[SESSION_IDGRADO] = DVariables["IDGRADO"];
                HttpContext.Current.Session[SESSION_IDSEDE] = DVariables["IDSEDE"];
                HttpContext.Current.Session[SESSION_IDSECCION] = DVariables["IDSECCION"];
                HttpContext.Current.Session[SESSION_NOMBRE] = DVariables["NOMBRE"];
                HttpContext.Current.Session[SESSION_APELLIDOPARTERNO] = DVariables["APELLIDOPARTERNO"];
                HttpContext.Current.Session[SESSION_APELLIDOMATERNO] = DVariables["APELLIDOMATERNO"];
                HttpContext.Current.Session[SESSION_CORREO] = DVariables["CORREO"];
                HttpContext.Current.Session[SESSION_TIPOUSUARIO] = DVariables["TIPOUSUARIO"];
                HttpContext.Current.Session.Timeout = 24 * 60;
            }
            catch (ArgumentOutOfRangeException kfe)
            {
                UtlLog.toWrite(UtlConstantes.PizarraUTL, UtlConstantes.LogNamespace_PizarraUTL, "SetSessionValues", MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", kfe.StackTrace.ToString(), kfe.Message.ToString());
                throw new ArgumentOutOfRangeException("Se requiere que se tenga todas las llaves :  idUsuario, sCodUsuario , sNombreCompleto, sNombres, sCorreo, sMenu", kfe);
            }
            catch (Exception ex)
            {
                UtlLog.toWrite(UtlConstantes.PizarraUTL, UtlConstantes.LogNamespace_PizarraUTL, "SetSessionValues", MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                throw ex;
            }
        }

        #endregion

        #region "Obtiene la dirección IP del Usuario"

        /// <summary>
        /// Obtiene la dirección IP del cliente, desde donde está conectado al sistema
        /// </summary>
        /// <returns>Devuelve la dirección IP</returns>
        public static string ObtenerDireccionIP()
        {
            HttpRequest currentRequest = HttpContext.Current.Request;
            string ipAddress = currentRequest.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ipAddress == null || ipAddress.ToLower() == "unknown")
                ipAddress = currentRequest.ServerVariables["REMOTE_ADDR"];

            //ADD IPLAN;
            if (ipAddress == "::1")
            {
                foreach (IPAddress IPA in Dns.GetHostAddresses(Dns.GetHostName()))
                {
                    if (IPA.AddressFamily.ToString() == "InterNetwork")
                    {
                        ipAddress = IPA.ToString();
                        break;
                    }
                }
            }

            return ipAddress;
        }

        #endregion

        #region "Obtiene la dirección MAC del Usuario"

        /// <summary>
        /// Obtiene la dirección MAC del cliente, desde donde está conectado el sistema
        /// </summary>
        /// <returns>Devuelve la dirección MAC</returns>
        public static string ObtenerDireccionMAC()
        {
            string strClientIP = ObtenerDireccionIP();
            string mac_dest = "";
            try
            {
                Int32 ldest = inet_addr(strClientIP);
                Int32 lhost = inet_addr("");
                Int64 macinfo = new Int64();
                Int32 len = 6;
                int res = SendARP(ldest, 0, ref macinfo, ref len);
                string mac_src = macinfo.ToString("X");

                while (mac_src.Length < 12)
                {
                    mac_src = mac_src.Insert(0, "0");
                }

                for (int i = 0; i < 11; i++)
                {
                    if (0 == (i % 2))
                    {
                        if (i == 10)
                        {
                            mac_dest = mac_dest.Insert(0, mac_src.Substring(i, 2));
                        }
                        else
                        {
                            mac_dest = "-" + mac_dest.Insert(0, mac_src.Substring(i, 2));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return (mac_dest);
        }

        [DllImport("Iphlpapi.dll")]
        private static extern int SendARP(Int32 dest, Int32 host, ref Int64 mac, ref Int32 length);

        [DllImport("Ws2_32.dll")]
        private static extern Int32 inet_addr(string ip);

        #endregion

        public static bool ValidarSession()
        {
            try
            {
                if (HttpContext.Current.Session[SESSION_IDUSUARIO] == null ||
                HttpContext.Current.Session[SESSION_IDNIVEL] == null ||
                HttpContext.Current.Session[SESSION_IDGRADO] == null ||
                HttpContext.Current.Session[SESSION_IDSEDE] == null ||
                HttpContext.Current.Session[SESSION_NOMBRE] == null ||
                HttpContext.Current.Session[SESSION_TIPOUSUARIO] == null ||
                HttpContext.Current.Session[SESSION_APELLIDOPARTERNO] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                UtlLog.toWrite(UtlConstantes.PizarraUTL, UtlConstantes.LogNamespace_PizarraUTL, "ValidarSession", MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                return false;
            }

        }

        public static bool ValidarMenu()
        {
            //string sMenu = UtlAuditoria.ObtenerMenu();
            return true;
        }

        public static bool CerrarSession()
        {
            try
            {
                HttpContext.Current.Session[SESSION_IDUSUARIO] = null;
                HttpContext.Current.Session[SESSION_IDNIVEL] = null;
                HttpContext.Current.Session[SESSION_IDGRADO] = null;
                HttpContext.Current.Session[SESSION_IDSEDE] = null;
                HttpContext.Current.Session[SESSION_IDSECCION] = null;
                HttpContext.Current.Session[SESSION_NOMBRE] = null;
                HttpContext.Current.Session[SESSION_APELLIDOPARTERNO] = null;
                HttpContext.Current.Session[SESSION_APELLIDOMATERNO] = null;
                HttpContext.Current.Session[SESSION_CORREO] = null;
                HttpContext.Current.Session[SESSION_TIPOUSUARIO] = null;

                return true;
            }
            catch (Exception ex)
            {
                UtlLog.toWrite(UtlConstantes.PizarraUTL, UtlConstantes.LogNamespace_PizarraUTL, "ValidarSession", MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                return false;
            }
        }

        public static int MostrarTiempoSesion()
        {
            try
            {
                var iTiempoSesion = HttpContext.Current.Session.Timeout;
                return iTiempoSesion;
            }
            catch (Exception ex)
            {
                UtlLog.toWrite(UtlConstantes.PizarraUTL, UtlConstantes.LogNamespace_PizarraUTL, "ValidarSession", MethodBase.GetCurrentMethod().Name, UtlConstantes.LogTipoError, "", ex.StackTrace.ToString(), ex.Message.ToString());
                return 0;
            }

        }

    }
}
