Namespace SIG.Areas.Planilla.Models
    Public Class cl_esquema
        Private objConexionDB As New SIG.Areas.Planilla.Models.cl_conexion_db()
        Private objManejoDatos As New SIG.Areas.Planilla.Models.cl_manejo_datos()
        Private strQuery As String
        Private objTabla As DataTable
        Private arrObjeto As Array
        Private arrCenso As Array = {"Urbano", "Rural"}
        Private arrTipoPago As Array = {"Primer pago", "Repago"}
        Private arrTipoEsquema As Array = {"Planilla", "Pre-planilla"}
        Private arrEstAprobado As Array = {"Desaprobado", "Aprobado"}
        Private arrMes As Array = {"Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"}

        Public esq_codigo As Integer
        Public pag_codigo As Integer
        Public nom_esquema As String
        Public esq_numero As Integer
        Public esq_censo As String
        Public esq_fecha_ini As String
        Public esq_fecha_fin As String
        Public esq_meses As Integer
        Public esq_tipo_intervalo As Integer
        Public esq_tipo_pago As String
        Public esq_fecha_elegibilidad As String
        Public esq_anyo As Integer
        Public esq_cant_bonos_act As Integer
        Public esq_detalle_meses As String
        Public esq_tipo_esquema As Integer
        Public esq_observaciones As String
        Public esq_usuario_accion As Integer
        Public esq_cof_monto As Integer

        Function fnc_obtener_esquemas() As DataTable
            strQuery = "SELECT   " & _
                        "	esq_codigo,  " & _
                        "	pag_codigo,  " & _
                        "	nombre_esquema,  " & _
                        "	esq_numero,  " & _
                        "	esq_anyo, " & _
                        "	CONVERT(DATE, esq_fecha_ini) AS esq_fecha_ini, " & _
                        "	CONVERT(DATE, esq_fecha_fin) AS esq_fecha_fin, " & _
                        "	esq_meses, " & _
                        "	esq_detalle_meses, " & _
                        "	esq_tipo_intervalo, " & _
                        "	esq_tipo_pago, " & _
                        "	esq_tipo_esquema, " & _
                        "	esq_aprobado " & _
                        "FROM  " & _
                        "	SIG_T.dbo.f_pla_esquema  " & _
                        "ORDER BY  " & _
                        "   esq_codigo DESC"
            Return objConexionDB.fnc_crear_datatable(strQuery)
        End Function

        Function fnc_obtener_pago() As DataTable
            strQuery = "SELECT " & _
                        "	 pag_codigo, " & _
                        "	 pag_nombre " & _
                        "FROM " & _
                        "	SIG_T.dbo.f_pla_pago "
            Return objConexionDB.fnc_crear_datatable(strQuery)
        End Function

        Function fnc_obtener_censo() As DataTable
            objTabla = New DataTable

            objTabla.Columns.Add("cod_censo", GetType(String))
            objTabla.Columns.Add("nombre_censo", GetType(String))

            For i = 1 To arrCenso.Length
                objTabla.Rows.Add(UCase(arrCenso(i - 1)), arrCenso(i - 1))
            Next

            Return objTabla
        End Function

        Function fnc_obtener_intervalo_tiempo() As DataTable
            strQuery = "SELECT " & _
                    "	int_codigo, " & _
                    "	int_nombre " & _
                    "FROM " & _
                    "	SIG_T.dbo.f_pla_intervalo " & _
                    "ORDER BY " & _
                    "	int_cant_meses ASC "

            Return objConexionDB.fnc_crear_datatable(strQuery)
        End Function

        Function fnc_obtener_tipo_pago() As DataTable
            objTabla = New DataTable

            objTabla.Columns.Add("cod_tipo_pago", GetType(String))
            objTabla.Columns.Add("nombre_tipo_pago", GetType(String))

            For i = 1 To arrTipoPago.Length
                objTabla.Rows.Add(arrTipoPago(i - 1), arrTipoPago(i - 1))
            Next

            Return objTabla
        End Function

        Function fnc_obtener_tipo_esquema() As DataTable
            objTabla = New DataTable

            objTabla.Columns.Add("cod_tipo_esquema", GetType(Integer))
            objTabla.Columns.Add("nombre_tipo_esquema", GetType(String))

            For i = 1 To arrTipoEsquema.Length
                objTabla.Rows.Add(i, arrTipoEsquema(i - 1))
            Next

            Return objTabla
        End Function

        Function fnc_obtener_montos() As DataTable
            strQuery = "SELECT  " & _
                        "	confm_codigo, " & _
                        "	confm_basico, " & _
                        "	confm_nivel1_1, " & _
                        "	confm_nivel1_2, " & _
                        "	confm_nivel2_1, " & _
                        "	confm_nivel2_2, " & _
                        "	confm_nivel3_1, " & _
                        "	confm_nivel3_2 " & _
                        "FROM " & _
                        "	SIG_T.dbo.F_Pla_Conf_Montos "
            Return objConexionDB.fnc_crear_datatable(strQuery)
        End Function

        Function fnc_obtener_estado_aprobado() As DataTable
            objTabla = New DataTable

            objTabla.Columns.Add("esq_aprobado", GetType(Integer))
            objTabla.Columns.Add("nombre_esq_aprobado", GetType(String))

            For i = 1 To arrEstAprobado.Length
                objTabla.Rows.Add(i - 1, arrEstAprobado(i - 1))
            Next

            Return objTabla
        End Function

        Function fnc_obtener_detalle_esquema(ByVal intCodEsquema As Integer) As Array
            strQuery = String.Format("SELECT  " & _
                                    "	ESQ.esq_cant_bonos_act,  " & _
                                    "	ESQ.Periodo,  " & _
                                    "	UPPER(ISNULL(ESQ.esq_observaciones, 'Sin Comentarios')) AS esq_observaciones,  " & _
                                    "	CONVERT(DATE, ESQ.esq_fecha_elegibilidad) AS esq_fecha_elegibilidad,  " & _
                                    "	ESQ.esq_censo, " & _
                                    "	MON.confm_codigo  " & _
                                    "FROM  " & _
                                    "	SIG_T.dbo.f_pla_esquema AS ESQ " & _
                                    "INNER JOIN " & _
                                    "	SIG_T.dbo.F_Pla_Conf_Montos_Esq AS MON " & _
                                    "	ON ESQ.esq_codigo = MON.esq_codigo " & _
                                    "WHERE  " & _
                                    "	ESQ.esq_codigo = {0}  ", intCodEsquema)
            Return objConexionDB.fnc_crear_arreglo_regis_unico(strQuery, 6)
        End Function

        Function fnc_obtener_un_esquema(ByVal intCodEsquema As Integer) As Array
            strQuery = String.Format("SELECT  " & _
                                    "	ESQ.esq_codigo,  " & _
                                    "	ESQ.pag_codigo,  " & _
                                    "	ESQ.nombre_esquema,  " & _
                                    "	ESQ.esq_censo,  " & _
                                    "	CONVERT(DATE, ESQ.esq_fecha_ini, 103) AS esq_fecha_ini,  " & _
                                    "	CONVERT(DATE, ESQ.esq_fecha_fin, 103) AS esq_fecha_fin,  " & _
                                    "	ESQ.esq_meses,  " & _
                                    "	ESQ.esq_tipo_intervalo,  " & _
                                    "	ESQ.esq_tipo_pago,  " & _
                                    "	CONVERT(DATE, ESQ.esq_fecha_elegibilidad, 103) AS esq_fecha_elegibilidad,  " & _
                                    "	ESQ.esq_anyo,  " & _
                                    "	ESQ.esq_cant_bonos_act,  " & _
                                    "	ESQ.esq_detalle_meses,  " & _
                                    "	UPPER(ISNULL(ESQ.esq_observaciones, 'Sin Comentarios')) AS esq_observaciones, " & _
                                    "	MON.confm_codigo,  " & _
                                    "	ESQ.esq_tipo_esquema,  " & _
                                    "   ESQ.esq_numero " & _
                                    "FROM  " & _
                                    "	SIG_T.dbo.f_pla_esquema AS ESQ " & _
                                    "INNER JOIN " & _
                                    "	SIG_T.dbo.F_Pla_Conf_Montos_Esq AS MON " & _
                                    "	ON ESQ.esq_codigo = MON.esq_codigo " & _
                                    "WHERE  " & _
                                    "	ESQ.esq_codigo =  {0}", intCodEsquema)
            arrObjeto = objConexionDB.fnc_crear_arreglo_regis_unico(strQuery, 17)
            arrObjeto(4) = Format(Convert.ToDateTime(arrObjeto(4)), "yyyy/MM/dd").ToString
            arrObjeto(5) = Format(Convert.ToDateTime(arrObjeto(5)), "yyyy/MM/dd").ToString
            arrObjeto(9) = Format(Convert.ToDateTime(arrObjeto(9)), "yyyy/MM/dd").ToString
            Return arrObjeto
        End Function

        Function fnc_obtener_meses() As DataTable
            objTabla = New DataTable

            objTabla.Columns.Add("cod_mes", GetType(Integer))
            objTabla.Columns.Add("nombre_mes", GetType(String))

            For i = 1 To arrMes.Length
                objTabla.Rows.Add(i, arrMes(i - 1))
            Next

            Return objTabla
        End Function

        Function fnc_obtener_cant_meses(ByVal intCodInterTiempo As Integer) As Integer
            strQuery = String.Format("SELECT " & _
                                    "	int_cant_meses " & _
                                    "FROM " & _
                                    "	SIG_T.dbo.f_pla_intervalo " & _
                                    "WHERE " & _
                                    "	int_codigo = {0} ", intCodInterTiempo)
            arrObjeto = objConexionDB.fnc_crear_arreglo_regis_unico(strQuery, 1)

            If CInt(arrObjeto(0)) > 0 Or arrObjeto(0) IsNot Nothing Then
                Return CInt(arrObjeto(0))
            Else
                Return 0
            End If
        End Function

        Function fnc_operacion_esquema(ByVal intCodOperacion As Integer) As Integer
            Select Case intCodOperacion
                Case 1
                    Return objConexionDB.fnc_p_pla_nuevo_esquema(pag_codigo, nom_esquema, esq_censo, esq_fecha_ini, _
                                                                esq_fecha_fin, esq_meses, esq_tipo_intervalo, esq_tipo_pago, _
                                                                esq_fecha_elegibilidad, esq_anyo, esq_cant_bonos_act, esq_detalle_meses, _
                                                                esq_tipo_esquema, esq_observaciones, esq_usuario_accion, esq_cof_monto)
                Case 2
                    Dim intRespuesta As Integer
                    intRespuesta = objConexionDB.fnc_p_pla_actualizar_esquema(esq_codigo, pag_codigo, nom_esquema, esq_censo, esq_fecha_ini, _
                                                                          esq_fecha_fin, esq_meses, esq_tipo_intervalo, esq_tipo_pago, _
                                                                          esq_fecha_elegibilidad, esq_anyo, esq_cant_bonos_act, esq_detalle_meses, _
                                                                          esq_tipo_esquema, esq_observaciones, esq_numero, esq_usuario_accion, esq_cof_monto)

                    If intRespuesta = 1 Then
                        Return objConexionDB.fnc_p_pla_desaprobar_esquema_cambio(esq_codigo, esq_usuario_accion)
                    Else
                        Return intRespuesta
                    End If
                Case 3
                    If fnc_esquema_fue_ejecutado(esq_codigo) = False Then
                        Dim intRespuesta As Integer = 1
                        Dim objCober As New cl_cobertura_planilla
                        Dim objMecanPago As New cl_mecanismo_pago
                        Dim objEstaFondo As New cl_establecimiento_fondo

                        For i = 1 To 5
                            If intRespuesta = 1 Then
                                Select Case i
                                    Case 1
                                        intRespuesta = objCober.fnc_desenlazar_cobertura_esquema(esq_codigo, esq_usuario_accion)
                                    Case 2
                                        intRespuesta = objMecanPago.fnc_desenlazar_mecani_pago(esq_codigo, esq_usuario_accion)
                                    Case 3
                                        intRespuesta = objEstaFondo.fnc_desenlazar_estable_fondo_esquema(esq_codigo, esq_usuario_accion)
                                    Case 4
                                        intRespuesta = objConexionDB.fnc_p_pla_borrar_hijos_esquema(esq_codigo, esq_usuario_accion)
                                    Case 5
                                        intRespuesta = objConexionDB.fnc_p_pla_eliminar_esquema(esq_codigo, esq_usuario_accion)
                                End Select
                            Else
                                Exit For
                            End If
                        Next
                        Return intRespuesta
                    Else
                        Return 2
                    End If
                Case Else
                    Return 0
            End Select
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

        Function fnc_se_generaron_bonos(ByRef intCodEsquema As Integer) As Boolean
            strQuery = String.Format("SELECT " & _
                                    "	COUNT(bon_codigo) " & _
                                    "FROM " & _
                                    "	SIG_T.[dbo].[f_pla_bonos_act] " & _
                                    "WHERE " & _
                                    "	esq_codigo = {0} ")
            arrObjeto = objConexionDB.fnc_crear_arreglo_regis_unico(strQuery, 1)

            If arrObjeto(0) = 0 Then
                Return False
            Else
                Return True
            End If
        End Function


    End Class
End Namespace
