Namespace SIG.Areas.Planilla.Models
    Public Class cl_otra_verificacion
        Private objConexionDB As New SIG.Areas.Planilla.Models.cl_conexion_db()
        Private objManejoDatos As New SIG.Areas.Planilla.Models.cl_manejo_datos()
        Private strQuery As String
        Private objTabla As DataTable
        Private arrObjeto As Array


        Function fnc_obetener_verificaciones() As DataTable
            strQuery = "SELECT  " & _
                        "      ver_codigo, " & _
                        "      ver_nombre " & _
                        "FROM " & _
                        "      SIG_T.dbo.f_pla_verificaciones "
            Return objConexionDB.fnc_crear_datatable(strQuery)
        End Function

        Function fnc_obtener_verificaciones_esquemas(ByRef intCodEsquema As Integer) As DataTable
            Dim objDataTable As DataTable
            Dim objDataColumn As DataColumn
            Dim objDataRow As DataRow
            Dim strNombreColumn() As String = {"veresq_codigo", "ver_codigo"}

            strQuery = String.Format("SELECT " & _
                                    "      veresq_codigo, " & _
                                    "      ver_codigo " & _
                                    "FROM  " & _
                                    "      SIG_T.dbo.f_pla_verif_esq " & _
                                    "WHERE " & _
                                    "      esq_codigo = {0} ", intCodEsquema)


            objTabla = objConexionDB.fnc_crear_datatable(strQuery)
            objDataTable = New DataTable

            For i = 0 To strNombreColumn.Length - 1
                objDataColumn = New DataColumn
                objDataColumn.DataType = GetType(String)
                objDataColumn.ReadOnly = False
                objDataColumn.ColumnName = strNombreColumn(i)
                objDataTable.Columns.Add(objDataColumn)
            Next

            For Each objFila In objTabla.Rows
                objDataRow = objDataTable.NewRow()
                objDataRow(strNombreColumn(0)) = objFila("veresq_codigo")
                objDataRow(strNombreColumn(1)) = objFila("ver_codigo")

                objDataTable.Rows.Add(objDataRow)
            Next

            Return objDataTable
        End Function

        Function fnc_crear_otra_verificacion(ByRef intCodUsuario As Integer, ByRef intCodEsquema As Integer, ByRef intCodVerificacion As Integer) As Integer
            If objConexionDB.fnc_p_pla_operaciones_otra_verificacion(intCodUsuario, intCodEsquema, intCodVerificacion, 1, 0) = 0 And _
                objConexionDB.fnc_p_pla_desaprobar_esquema_cambio(intCodEsquema, intCodUsuario) = 0 Then
                Return 0
            Else
                Return 1
            End If
        End Function

        Function fnc_borrar_otra_verificacion(ByRef intCodUsuario As Integer, ByRef intCodCodVerifiEsque As Integer) As Integer
            If objConexionDB.fnc_p_pla_operaciones_otra_verificacion(intCodUsuario, 0, 0, 2, intCodCodVerifiEsque) = 0 And
                objConexionDB.fnc_p_pla_desaprobar_esquema_cambio(fnc_obtener_cod_esquema(intCodCodVerifiEsque), intCodUsuario) = 0 Then
                Return 0
            Else
                Return 1
            End If
        End Function

        Function fnc_obtener_primary_key_verifiacion_esquema(ByRef intCodEsquema As Integer, ByRef intCodVerificacion As Integer) As Integer
            strQuery = String.Format("SELECT " & _
                                    "       veresq_codigo " & _
                                    "FROM  " & _
                                    "      SIG_T.dbo.f_pla_verif_esq " & _
                                    "WHERE " & _
                                    "      esq_codigo = {0} " & _
                                    "      AND ver_codigo = {1} ", intCodEsquema, intCodVerificacion)
            arrObjeto = objConexionDB.fnc_crear_arreglo_regis_unico(strQuery, 1)

            Return arrObjeto(0)
        End Function

        Function fnc_esquema_ya_ejecutado(ByRef intCodEsquema As Integer) As Boolean
            strQuery = String.Format("SELECT  " & _
                                    "        TOP 1 1 " & _
                                    "FROM  " & _
                                    "    SIG_T.dbo.f_pla_titulares  " & _
                                    "WHERE  " & _
                                    "    tit_esquema =  {0} ", intCodEsquema)
            arrObjeto = objConexionDB.fnc_crear_arreglo_regis_unico(strQuery, 1)

            If arrObjeto(0) Is Nothing Then
                Return False
            Else
                Return True
            End If
        End Function

        Function fnc_obtener_cod_esquema(ByRef intCodVeriEsque As Integer) As Integer
            strQuery = String.Format("SELECT " & _
                                    "	esq_codigo " & _
                                    "FROM " & _
                                    "	SIG_T.dbo.f_pla_verif_esq " & _
                                    "WHERE " & _
                                    "	veresq_codigo = {0} ", intCodVeriEsque)

            arrObjeto = objConexionDB.fnc_crear_arreglo_regis_unico(strQuery, 1)

            Return CInt(arrObjeto(0))
        End Function
    End Class
End Namespace
