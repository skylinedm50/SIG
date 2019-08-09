Imports System.ComponentModel.DataAnnotations

Namespace SIG.Areas.Corresponsabilidad.Models

    Public Class cl_tipo_corresponsabilidad
        Private objConexionDB As New cl_conexion_db()
        Private strQuery As String
        Private objTabla As DataTable
        Property cod_tip_cor As Integer
        Property nom_tip_cor As String
        Property des_tip_cor As String
        Property cod_com As Integer

        Function fnc_obtener_tipo_corrresponsabilidad() As DataTable 'Funcion que crea un DataTble apartir de los tipos de corresponsabilidad.
            strQuery = "SELECT  " &
                        "	TIP_CORR.cod_tip_cor,  " &
                        "	TIP_CORR.nom_tip_cor,  " &
                        "	TIP_CORR.des_tip_cor,  " &
                        "	COMP.comp_nombre " &
                        "FROM  " &
                        "	SIG_T.dbo.t_corr_tipo_corresponsabilidad AS TIP_CORR " &
                        "INNER JOIN " &
                        "	SIG_T.dbo.f_pla_tipo_componente AS COMP " &
                        "	ON TIP_CORR.cod_com = COMP.comp_codigo "
            objTabla = objConexionDB.fnc_crear_datatable(strQuery)

            Return objTabla
        End Function

        Public Sub met_nuevo_tipo_corrresponsabilidad()
            strQuery = String.Format("INSERT INTO t_corr_tipo_corresponsabilidad(nom_tip_cor, des_tip_cor, cod_com)" + _
                                                  "VALUES('{0}', '{1}', {2})", Replace(nom_tip_cor, Chr(34), ""), Replace(des_tip_cor, Chr(34), ""), cod_com)
            objConexionDB.fnc_ejecutar_simple_comando(strQuery)
        End Sub

        Public Sub met_actualizar_tipo_corrresponsabilidad()
            strQuery = String.Format("UPDATE t_corr_tipo_corresponsabilidad SET " + _
                                     "nom_tip_cor = '{0}', " + _
                                     "des_tip_cor = '{1}', " + _
                                     "cod_com = {2} " + _
                                     "WHERE cod_tip_cor = {3}", Replace(nom_tip_cor, Chr(34), ""), Replace(des_tip_cor, Chr(34), ""), cod_com, cod_tip_cor)
            objConexionDB.fnc_ejecutar_simple_comando(strQuery)
        End Sub

        Public Sub met_borrar_tipo_corrresponsabilidad()
            strQuery = String.Format("DELETE FROM t_corr_tipo_corresponsabilidad WHERE cod_tip_cor = {0}", cod_tip_cor)
            objConexionDB.fnc_ejecutar_simple_comando(strQuery)
        End Sub

        Function fnc_obtener_componente()
            strQuery = String.Format("SELECT comp_codigo, comp_nombre FROM SIG_T.dbo.f_pla_tipo_componente")
            objTabla = objConexionDB.fnc_crear_datatable(strQuery)
            Return objTabla
        End Function
    End Class

End Namespace
