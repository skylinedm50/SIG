Namespace SIG.Areas.Corresponsabilidad.Models
    Public Class cl_gestion_observaciones
        Private objConexionDB As New SIG.Areas.Corresponsabilidad.Models.cl_conexion_db
        Private strQuery As String
        Private objTabla As DataTable
        Dim objFila As DataRow
        Private arrObjeto As Array

        Function nueva_gestion(ByRef intCodSolicitud As Integer, ByRef strObservación As String, ByRef intCodUser As Integer) As Integer
            strQuery = String.Format("INSERT INTO SIG_T.dbo.t_corr_gestion_observaciones " &
                                    "				( " &
                                    "					cod_soli, " &
                                    "					obser_ges_obs, " &
                                    "					cod_usuario " &
                                    "				) " &
                                    "VALUES({0}, LTRIM(RTRIM('{1}')), {2}) ", intCodSolicitud, strObservación, intCodUser)

            Return objConexionDB.fnc_ejecutar_simple_comando(strQuery)
        End Function




    End Class
End Namespace
