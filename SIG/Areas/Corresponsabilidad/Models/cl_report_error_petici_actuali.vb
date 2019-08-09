Namespace SIG.Areas.Corresponsabilidad.Models
    Public Class cl_report_error_petici_actuali
        Private objConexionDB As New SIG.Areas.Corresponsabilidad.Models.cl_conexion_db
        Private strQuery As String
        Private objTabla As DataTable
        Private arrObjeto As Array

        Function fnc_obtener_info_errores(ByRef intNumActu As Integer, intTipComp As Integer) As DataTable
            Dim strWhere As String = "WHERE "

            Select Case intTipComp
                Case 1
                    strWhere &= "   cod_des_ser < 15 "
                Case 2
                    strWhere &= "   cod_des_ser > 14 "
            End Select

            If intNumActu > 0 Then
                strWhere &= String.Format("   AND num_car_log_car = {0} ", intNumActu)
            End If

            strQuery = String.Format("SELECT " &
                                    "	cod_err_con_car, " &
                                    "	fec_err_con_car, " &
                                    "	int_err_con_car, " &
                                    "	num_car_log_car, " &
                                    "	cod_des_ser, " &
                                    "	err_err_con_car, " &
                                    "	ser_err_con_car " &
                                    "FROM " &
                                    "	SIG_R.dbo.t_corr_error_conexion_carga " &
                                    "{0} ", strWhere)

            Return objConexionDB.fnc_crear_datatable(strQuery)
        End Function


        Function fnc_obtener_servicios(ByRef intCodComp As Integer) As DataTable
            Dim strWhere As String = ""

            Select Case intCodComp
                Case 1
                    strWhere = "cod_des_ser < 15"
                Case 2
                    strWhere = "cod_des_ser > 14"
            End Select
            strQuery = String.Format("SELECT " &
                                    "  cod_des_ser, " &
                                    "  nom_des_ser " &
                                    "FROM " &
                                    "  SIG_R.dbo.t_corr_descripcion_servicios " &
                                    "WHERE " &
                                    "   {0} ", strWhere)

            Return objConexionDB.fnc_crear_datatable(strQuery)
        End Function


    End Class
End Namespace
