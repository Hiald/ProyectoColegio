using System;
using System.IO;
using System.Xml;


namespace frontendUtil
{
    public class UtlLog
    {
        /// <summary>
        /// toWrite
        /// Objetivo: Metodo que registra el Log de eventos de la Aplicación
        /// </summary>
        /// <param name="argSistema">Sistema donde ocurrio el evento</param>
        /// <param name="argNamespace">Namespace donde ocurrio el evento</param>
        /// <param name="argElemento">Elemento donde ocurrio el evento</param>
        /// <param name="argFuncion">Función donde ocurrio el evento</param>
        /// <param name="argTipo">Nivel de Error :
        ///                                     Debug = "DEBUG";
        ///                                     Info = "INFO";
        ///                                     Warning = "WARN";
        ///                                     Error = "ERROR";
        ///                                     Fatal = "FATAL";
        /// <param name="argErrOri">Grupo de Error Original</param>
        /// <param name="argMsgOri">Mensaje de Error Original</param>
        /// <returns>
        /// Resultados   :  0 - Log se escribio correctamente
        ///              : -1 - Ocurrio un problema al escribir log
        ///              : -2 - Ambiente no válido
        ///              : -3 - Código de tipo de log no valido
        /// </returns>
        public static int toWrite(string argSistema,
                            string argNamespace,
                            string argElemento,
                            string argFuncion,
                            string argTipo,
                            string argUsuario,
                            string argErrOri,
                            string argMsgOri)
        {

            //0.Declarar variables
            string strFile = null;
            int iRetorno = 0;

            try
            {
                //1.Inicializar variables
                iRetorno = -1;
                string strRuta = System.Web.Configuration.WebConfigurationManager.AppSettings["RutaLog"].ToString();

                //2.Ejecutar
                //2.1.Crear ruta
                if (!System.IO.Directory.Exists(strRuta))
                {
                    System.IO.Directory.CreateDirectory(strRuta);
                }
                strFile = String.Concat(strRuta.ToString(), "LOG_", argNamespace.ToString(), ".log");

                //2.2.Crear archivo
                string strOriginal = null;
                XmlTextWriter xmlWriter = null;
                if (!System.IO.File.Exists(strFile))
                {
                    xmlWriter = new XmlTextWriter(strFile, System.Text.Encoding.UTF8);
                    xmlWriter.WriteStartDocument();
                    xmlWriter.WriteStartElement("Logs");
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndDocument();
                    xmlWriter.Close();
                }
                else
                {

                    //Variables para calculo de Dias de LOG
                    DateTime dFechaArchivo = System.IO.File.GetLastWriteTime(strFile);
                    DateTime dFechaHoy = System.DateTime.Now;
                    double dDiferenciaDias = (dFechaHoy - dFechaArchivo).Days;
                    double dDiferenciaDiasTotal = (dFechaHoy - dFechaArchivo).TotalDays;

                    if (dDiferenciaDiasTotal > 5)
                    {

                        //Setea nombre archivo BK
                        String sFechaHoy = String.Format("{0:yyyyMMdd}", dFechaHoy);
                        String strFileBk = String.Concat(strRuta.ToString(), "LOG_", argNamespace.ToString(), "_", sFechaHoy, ".log");

                        //Renombrar Archivo
                        File.Move(strFile, strFileBk);

                        //Crear Nuevo Archivo
                        xmlWriter = new XmlTextWriter(strFile, System.Text.Encoding.UTF8);
                        xmlWriter.WriteStartDocument();
                        xmlWriter.WriteStartElement("Logs");
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteEndDocument();
                        xmlWriter.Close();

                    }
                    else
                    {
                        //Leer Archivo Existente
                        XmlTextReader xmlReader = new XmlTextReader(strFile);
                        xmlReader.WhitespaceHandling = WhitespaceHandling.None;
                        xmlReader.MoveToContent();
                        strOriginal = xmlReader.ReadInnerXml();
                        xmlReader.Close();
                    }

                }

                //2.3.Escribir log
                xmlWriter = new XmlTextWriter(strFile, System.Text.Encoding.UTF8);
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("Logs");
                xmlWriter.WriteRaw(strOriginal);
                xmlWriter.WriteStartElement("Log");
                xmlWriter.WriteElementString("Usuario", argUsuario);
                xmlWriter.WriteElementString("Fecha", System.DateTime.Now.ToString("yyyyMMdd"));
                xmlWriter.WriteElementString("Hora", System.DateTime.Now.ToString("hh:mm:ss"));
                xmlWriter.WriteElementString("Sistema", argSistema);
                xmlWriter.WriteElementString("Namespace", argNamespace);
                xmlWriter.WriteElementString("Elemento", argElemento);
                xmlWriter.WriteElementString("Funcion", argFuncion);
                xmlWriter.WriteElementString("Tipo", argTipo);
                xmlWriter.WriteElementString("ErrOri", argErrOri);
                xmlWriter.WriteElementString("MsgOri", argMsgOri);
                xmlWriter.WriteEndElement();
                xmlWriter.Close();
                iRetorno = 0;

                /*
                //2.4 Registrar Evento Windows
                string sSource = "Ayni";
                string sLog = "Application";
                string sEvent = "Prueba Log";

                if (!EventLog.SourceExists(sSource)) EventLog.CreateEventSource(sSource, sLog);
                EventLog.WriteEntry(sSource, sEvent);
                EventLog.WriteEntry(sSource, sEvent,EventLogEntryType.Warning, 234);
                */

                //2.5 Registro en BD

            }
            catch
            {
                // (Exception ex)
                iRetorno = -1;
            }

            return iRetorno;
        }

    }
}
