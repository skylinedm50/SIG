Namespace SIG.Areas.Corresponsabilidad.Models
    Public Class cl_seguimiento_observaciones
        Private objConexionDB As New SIG.Areas.Corresponsabilidad.Models.cl_conexion_db
        Private strQuery As String
        Private objTabla As DataTable
        Dim objFila As DataRow
        Private arrObjeto As Array

        Function obtener_gestion_observaciones() As DataTable
            strQuery = "SELECT " &
                        "	GES.cod_ges_obs AS codigo_observacion, " &
                        "	GES.cod_soli AS codigo_solicitud, " &
                        "	GES.obser_ges_obs AS observacion, " &
                        "	GES.fech_real_obs AS fecha, " &
                        "	PER.nom1_usr_persona + ' ' + PER.ape1_usr_persona AS nombre_usuario " &
                        "FROM " &
                        "	SIG_T.dbo.t_corr_gestion_observaciones AS GES " &
                        "INNER JOIN " &
                        "	SIG_T.dbo.T_usuarios AS USU " &
                        "	ON GES.cod_usuario = USU.cod_usuario " &
                        "INNER JOIN " &
                        "	SIG_T.dbo.t_usr_personas AS PER " &
                        "	ON USU.cod_usr_persona = PER.cod_usr_persona " &
                        "WHERE " &
                        "	GES.est_ges_obs = 1 " &
                        "ORDER BY " &
                        "   GES.fech_real_obs DESC"
            objTabla = objConexionDB.fnc_crear_datatable(strQuery)
            Return objTabla
        End Function
    End Class
End Namespace
