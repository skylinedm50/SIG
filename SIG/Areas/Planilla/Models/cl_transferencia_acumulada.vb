Namespace SIG.Areas.Planilla.Models
    Public Class cl_transferencia_acumulada
        Private objConexionDB As New SIG.Areas.Planilla.Models.cl_conexion_db()
        Private objManejoDatos As New SIG.Areas.Planilla.Models.cl_manejo_datos()
        Private strQuery As String
        Private objTabla As DataTable
        Private arrObjeto As Array


        Function fnc_obtener_pago_para_acumulado(ByRef intCodPagoPadre As Integer) As DataTable
            strQuery = String.Format("SELECT " & _
                                    "	pag_anyo, " & _
                                    "   pag_numero " & _
                                    "FROM " & _
                                    "	SIG_T.dbo.f_pla_pago " & _
                                    "WHERE " & _
                                    "	pag_codigo = {0} ", intCodPagoPadre)
            arrObjeto = objConexionDB.fnc_crear_arreglo_regis_unico(strQuery, 2)

            If arrObjeto(0) Is Nothing Then
                arrObjeto(0) = 0
                arrObjeto(1) = 0
            End If

            strQuery = String.Format("SELECT  " & _
                                    "	pag_codigo,  " & _
                                    "	pag_numero,  " & _
                                    "	pag_anyo,  " & _
                                    "	pag_nombre,  " & _
                                    "	pag_descripcion  " & _
                                    "FROM  " & _
                                    "	SIG_T.dbo.f_pla_pago " & _
                                    "WHERE " & _
                                    "	pag_anyo < {0} " & _
                                    "	OR " & _
                                    "	( " & _
                                    "		pag_anyo = {0} " & _
                                    "		AND pag_numero < {1} " & _
                                    "	) ", arrObjeto(0), arrObjeto(1))
            Return objConexionDB.fnc_crear_datatable(strQuery)
        End Function

        Function fnc_obtener_detalle_esquema_para_acumulado(ByRef intCodPago As Integer) As DataTable
            strQuery = String.Format("SELECT " & _
                                    "	esq_codigo, " & _
                                    "	esq_tipo_pago, " & _
                                    "	nombre_esquema, " & _
                                    "	esq_detalle_meses " & _
                                    "FROM " & _
                                    "	SIG_T.dbo.f_pla_esquema " & _
                                    "WHERE " & _
                                    "	pag_codigo = {0} ", intCodPago)
            Return objConexionDB.fnc_crear_datatable(strQuery)
        End Function

        Function fnc_obtener_detalle_esquemas_acumulados(ByRef intCodEsquemaPadre As Integer) As DataTable
            Dim objNuevaTabla As New DataTable
            Dim strNameColumns() As String = {"bonacnc_codigo", "pag_codigo", "pag_numero", "pag_anyo", "pag_nombre", "pag_descripcion", "esq_codigo", "esq_tipo_pago", "nombre_esquema"}
            Dim intContador As Integer = -1
            Dim bolYaColumnas As Boolean = False

            strQuery = String.Format("SELECT " & _
                                    "   AC.bonacnc_codigo, " & _
                                    "	PAG.pag_codigo, " & _
                                    "	PAG.pag_numero, " & _
                                    "	PAG.pag_anyo, " & _
                                    "	PAG.pag_nombre, " & _
                                    "	PAG.pag_descripcion, " & _
                                    "	ESQ.esq_codigo, " & _
                                    "	ESQ.esq_tipo_pago, " & _
                                    "	ESQ.nombre_esquema " & _
                                    "FROM  " & _
                                    "	SIG_T.dbo.f_pla_bono_ac_no_cobro AS AC " & _
                                    "INNER JOIN " & _
                                    "	SIG_T.dbo.f_pla_esquema AS ESQ " & _
                                    "	ON AC.bonacnc_planillas = ESQ.esq_codigo " & _
                                    "INNER JOIN " & _
                                    "	SIG_T.dbo.f_pla_pago AS PAG " & _
                                    "	ON ESQ.pag_codigo = PAG.pag_codigo " & _
                                    "WHERE " & _
                                    "	AC.esq_codigo = {0} ", intCodEsquemaPadre)
            objTabla = objConexionDB.fnc_crear_datatable(strQuery)


            For i = 0 To strNameColumns.Length - 1
                Dim objColumna As New DataColumn
                objColumna.DataType = GetType(String)
                objColumna.ReadOnly = False
                objColumna.ColumnName = strNameColumns(i)
                objNuevaTabla.Columns.Add(objColumna)
            Next

            For Each objFila As DataRow In objTabla.Rows
                Dim objNuevaRow As DataRow = objNuevaTabla.NewRow()

                objNuevaRow("bonacnc_codigo") = objFila("bonacnc_codigo")
                objNuevaRow("pag_codigo") = objFila("pag_codigo")
                objNuevaRow("pag_numero") = objFila("pag_numero")
                objNuevaRow("pag_anyo") = objFila("pag_anyo")
                objNuevaRow("pag_nombre") = objFila("pag_nombre")
                objNuevaRow("pag_descripcion") = objFila("pag_descripcion")
                objNuevaRow("esq_codigo") = objFila("esq_codigo")
                objNuevaRow("esq_tipo_pago") = objFila("esq_tipo_pago")
                objNuevaRow("nombre_esquema") = objFila("nombre_esquema")

                objNuevaTabla.Rows.Add(objNuevaRow)
            Next


            Return objNuevaTabla
        End Function

        Function fnc_esquema_fue_ejecutado(ByRef intCodEsquema As Integer) As Boolean
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

        Function fnc_obtener_detalle_esquema(ByRef intCodEsquema As Integer) As Array
            strQuery = String.Format("SELECT " & _
                                    "      PAG.pag_codigo, " & _
                                    "      PAG.pag_numero, " & _
                                    "      PAG.pag_anyo, " & _
                                    "      PAG.pag_nombre, " & _
                                    "      PAG.pag_descripcion, " & _
                                    "      ESQ.esq_codigo, " & _
                                    "      ESQ.esq_tipo_pago, " & _
                                    "      ESQ.nombre_esquema " & _
                                    "FROM  " & _
                                    "      SIG_T.dbo.f_pla_esquema AS ESQ " & _
                                    "INNER JOIN " & _
                                    "      SIG_T.dbo.f_pla_pago AS PAG " & _
                                    "      ON ESQ.pag_codigo = PAG.pag_codigo " & _
                                    "WHERE " & _
                                    "      ESQ.esq_codigo = {0}", intCodEsquema)
            Return objConexionDB.fnc_crear_arreglo_regis_unico(strQuery, 8)
        End Function

        Function fnc_existe_esquema_acumulado(ByRef intCodEsqPadre As Integer, ByRef intCodEsqHaAcumu As Integer) As Boolean
            strQuery = String.Format("SELECT " & _
                                    "      COUNT(bonacnc_codigo) " & _
                                    "FROM  " & _
                                    "      SIG_T.dbo.f_pla_bono_ac_no_cobro " & _
                                    "WHERE " & _
                                    "      esq_codigo = {0} " & _
                                    "      AND bonacnc_planillas = {1} ", intCodEsqPadre, intCodEsqHaAcumu)
            arrObjeto = objConexionDB.fnc_crear_arreglo_regis_unico(strQuery, 1)

            If arrObjeto(0) = 0 Then
                Return False
            Else
                Return True
            End If
        End Function

        Function fnc_nuevo_bono_acumu(ByRef intCodUsuario As Integer, ByRef intCodEsquemaPadre As Integer, intCodEsquemaAcumu As Integer) As Integer
            If objConexionDB.fnc_p_pla_operaciones_transferencia_acumulada(intCodUsuario, intCodEsquemaPadre, intCodEsquemaAcumu, 1, 0) = 0 _
                And objConexionDB.fnc_p_pla_desaprobar_esquema_cambio(intCodEsquemaPadre, intCodUsuario) = 0 Then
                Return 0
            Else
                Return 1
            End If
        End Function

        Function fnc_borrar_bono_acumu(ByRef intCodUsuario As Integer, ByRef intCodKeyEsquemaAcumu As Integer) As Integer
            If fnc_esquema_principal_ejecutado(fnc_obtener_cod_esquema(intCodKeyEsquemaAcumu)) = False Then 'Verificamos que el esquema al que se le van acumular esquema no alla sido ejecutado.
                If objConexionDB.fnc_p_pla_operaciones_transferencia_acumulada(intCodUsuario, 0, 0, 2, intCodKeyEsquemaAcumu) = 0 And _
                    objConexionDB.fnc_p_pla_desaprobar_esquema_cambio(fnc_obtener_cod_esquema(intCodKeyEsquemaAcumu), intCodUsuario) = 0 Then
                    Return 0
                Else
                    Return 1
                End If
            Else
                Return 2
            End If
        End Function

        Function fnc_obtener_cod_esquema(ByRef intCodEsquema As Integer) As Integer
            strQuery = String.Format("SELECT " & _
                                    "	esq_codigo " & _
                                    "FROM " & _
                                    "	SIG_T.[dbo].[f_pla_bono_ac_no_cobro] " & _
                                    "WHERE " & _
                                    "	bonacnc_codigo = {0} ", intCodEsquema)

            arrObjeto = objConexionDB.fnc_crear_arreglo_regis_unico(strQuery, 1)

            Return CInt(arrObjeto(0))
        End Function

        Function fnc_esquema_principal_ejecutado(ByRef intCodEsquema As Integer) As Boolean
            strQuery = String.Format("SELECT " & _
                                    "	TOP 1 1 " & _
                                    "FROM " & _
                                    "	SIG_T.[dbo].[f_pla_bono_ac_no_cobro] AS TRA_ACU " & _
                                    "INNER JOIN " & _
                                    "	SIG_T.dbo.f_pla_titulares AS TIT " & _
                                    "	ON TRA_ACU.esq_codigo = TIT.tit_esquema " & _
                                    "WHERE " & _
                                    "	TRA_ACU.esq_codigo = {0} ", intCodEsquema)

            arrObjeto = objConexionDB.fnc_crear_arreglo_regis_unico(strQuery, 1)

            If arrObjeto(0) Is Nothing Then
                Return False
            Else
                Return True
            End If
        End Function


    End Class
End Namespace
