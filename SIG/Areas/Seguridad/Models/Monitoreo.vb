Imports System.Web.Script.Serialization
Namespace SIG.Areas.Seguridad.Models
    Public Class Monitoreo
        Inherits SIG.Areas.Seguridad.Models.Cl_Conexion
        Function fnc_Obtener_Inicios_Sesion()
            Me.Str_Query = "SELECT TOP 500 [nombre_usuario_intento_logeo], [mac_intento_logeo], [ip_intento_logeo], [ip_proxy_intento_logeo], [hostname_intento_logeo], [fecha_intento_logeo]" + vbCr +
                            "FROM [SIG_T].[dbo].[t_log_intento_logeo] a  INNER JOIN [SIG_T].[dbo].[t_log_inicios_sesion] b ON a.cod_intento_logeo = b.cod_intento_logeo" + vbCr +
                            "ORDER BY fecha_intento_logeo desc"

            Return Me.FncGetTableQuery()
        End Function

        Function fnc_Obtener_Log_Sistema()
            Me.Str_Query = "SELECT b.nom_usuario, a.operac_reali_log, a.fch_ejecucion_log, 'Corresponsabilidad' AS modulo FROM [SIG_T].[dbo].[t_log_sistema] a" + vbCr +
                            "INNER JOIN [T_usuarios] b ON a.cod_usuario = b.cod_usuario" + vbCr +
                            "UNION SELECT b.nom_usuario, [operac_reali_log], [fch_ejecucion_log], 'Incorporación y Actualización' As modulo" + vbCr +
                            "FROM [SIG_T].[dbo].[t_incor_log_sistemas] a INNER JOIN T_usuarios b ON a.user_log = b.cod_usuario" + vbCr +
                            "ORDER BY fch_ejecucion_log desc"
            Return Me.FncGetTableQuery()
        End Function

        Function fnc_Obtener_Log_Intentos_logeo()
            Me.Str_Query = "SELECT DISTINCT TOP 500 a.nombre_usuario_intento_logeo,a.mac_intento_logeo, a.ip_intento_logeo, a.ip_proxy_intento_logeo," + vbCr +
                            "a.hostname_intento_logeo, a.fecha_intento_logeo FROM [SIG_T].[dbo].[t_log_intento_logeo] a" + vbCr +
                            "LEFT JOIN t_log_inicios_sesion b  ON a.cod_intento_logeo = b.cod_intento_logeo" + vbCr +
                            "WHERE b.cod_intento_logeo IS NULL ORDER BY fecha_intento_logeo desc"

            Return Me.FncGetTableQuery()
        End Function
    End Class
End Namespace