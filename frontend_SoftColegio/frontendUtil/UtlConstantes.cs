
namespace frontendUtil
{
    public class UtlConstantes
    {
        //LOG TIPOS
        public const string LogTipoDebug = "DEBUG";
        public const string LogTipoInfo = "INFO";
        public const string LogTipoWarning = "WARN";
        public const string LogTipoError = "ERROR";
        public const string LogTipoFatal = "FATAL";

        public const string LogNamespace_PizarraUTL = "LogPizarraUTL";
        public const string LogNamespace_PIzarraAD = "LogPizarraAD";
        public const string LogNamespace_PizarraRN = "LogPizarraRN";
        public const string LogNamespace_PizarraWEB = "LogPizarraWEB";

        public const string PizarraUTL = "PizarraUTL";
        public const string PizarraAD = "PizarraAD";
        public const string PizarraRN = "PizarraRN";
        public const string PizarraWEB = "PizarraWEB";
        public const int iTipoError = 0;

        public const string msgError = "Tenemos inconvenientes al ejecutar la acción, inténtelo en unos minutos.";
        public const string msgErrorSesion = "Su sesión ha expirado por exceso de tiempo de inactividad. Por favor ingrese nuevamente al Sistema.";
        public const string msgMotivoBloqueo = "Cuenta desactivada por demasiados intentos permitidos.";

        #region ====== ERROR SESION TIMEOUT,ACCESO DENEGADO, 405 ,500  =======

        public const string iTimeoutSession = "E80";
        public const string i404 = "E404";
        public const string i405 = "E405";
        public const string i500 = "E500";
        public const string iErrorGeneral = "E0";
        public const string ErrorTimeoutSession = "Lo sentimos";
        public const string ErrorAccesoDenegado = "Acceso Denegado";
        public const string Error404 = "Error 404";
        public const string Error405 = "Error 405";
        public const string Error500 = "Error 500";
        public const string ErrorGeneral = "Lo sentimos";
        public const string msgTimeoutSession = "Su sesión acaba de expirar. Por favor ingrese de nuevo para continuar.";
        public const string msgError404 = "¡Lo sentimos! La página que estás buscando fue movida o nunca existió, asegúrese si accedió al URL correctamente o si es válido.";
        public const string msgError405 = "Disculpe, el método no se encuentra alojado en nuestro sistema, inténtelo nuevamente.";
        public const string msgError500 = "¡Lo sentimos! No eres tú. Somos nosotros. Por favor inténtelo nuevamente o contacte con soporte técnico.";
        public const string msgErrorGeneral = "Acaba de ocurrir un error, por favor intentelo nuevamente o contacte con soporte técnico.";
        public const string msgAccesoDenegado = "Lo sentimos. Usted no tiene permisos para acceder a este sitio.";
        public const string msgUsuarioBloqueo = "El usuario se encuentra bloqueado.";
        public const string msgErrorCuenta = "Usuario Y/O contraseña incorrectos.";
        #endregion

        #region ================ CONSTANTES CADENA =================

        public const string ValorNull = null;
        public const string ValorDefecto = "";

        #endregion

        #region ================ VALORES NUMÉRICOS =================

        public const int ValorCero = 0;
        public const int ValorUno = 1;
        public const int ValorDos = 2;
        public const int ValorTres = 3;
        public const int ValorCuatro = 4;
        public const int ValorCinco = 5;
        public const int ValorSeis = 6;
        public const int ValorSiete = 7;
        public const int ValorOcho = 8;
        public const int ValorNueve = 9;
        public const int ValorDiez = 10;
        public const int ValorNumeroMaximo = 2147483647;

        #endregion

        #region ================ VALORES COMBOS =================

        public const string sCboSeleccione = "--Seleccione--";
        public const string sCboTodos = "--Todos--";
        public const string sCboTodas = "--Todas--";
        public const string sCboNoTiene = "--No tiene--";

        #endregion

        #region ================ VALORES BOOL =================

        public const bool bValorTrue = true;
        public const bool bValorFalse = false;

        #endregion

        #region Paginacion

        public const int iTotalElementosMostrar = 10;

        #endregion

        #region ================ CODIGOS TABLAS MAESTRAS =========
        public const int iCodigoTablaEstadosUsuario = 1;
        #endregion
    }
}
