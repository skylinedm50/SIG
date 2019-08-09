Namespace SIG.Areas.Planilla.Models
    Public Class cl_transferencia_actual
        Private objConexionDB As New SIG.Areas.Planilla.Models.cl_conexion_db()
        Private objManejoDatos As New SIG.Areas.Planilla.Models.cl_manejo_datos()
        Private strQuery As String
        Private objTabla As DataTable
        Private arrObjeto As Array

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

        Function fnc_obtener_transferencias_actuales(ByVal strCodEsquemas As String) As DataTable
            strQuery = String.Format("SELECT " & _
                                    "	ESQ.esq_codigo, " & _
                                    "	ESQ.nombre_esquema, " & _
                                    "	BON.bon_codigo, " & _
                                    "	BON.bon_numero, " & _
                                    "   BON.bon_tipo_intervalo, " & _
                                    "	BON.bon_intervalo,	 " & _
                                    "	BON.bon_anyo, " & _
                                    "	CONVERT(DATE, BON.bon_fecha_ini, 103) AS bon_fecha_ini, " & _
                                    "	CONVERT(DATE, BON.bon_fecha_fin, 103) AS bon_fecha_fin, " & _
                                    "	BON.bon_detalle_meses, " & _
                                    "	BON.bon_meses_cubrir " & _
                                    "FROM  " & _
                                    "	SIG_T.dbo.f_pla_bonos_act AS BON " & _
                                    "INNER JOIN " & _
                                    "	SIG_T.dbo.f_pla_esquema AS ESQ " & _
                                    "	ON BON.esq_codigo = ESQ.esq_codigo " & _
                                    "WHERE " & _
                                    "	ESQ.esq_codigo IN({0}) ", strCodEsquemas)

            Return objConexionDB.fnc_crear_datatable(strQuery)
        End Function

        Public Function fnc_obtener_detalle_meses(ByVal dateFechIni As DateTime, ByVal dateFechFin As DateTime) As String
            Dim strArrMeses() As String = {"Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"}
            Dim intMesInicial As Integer = dateFechIni.Month - 1
            Dim intMesFinal As Integer = dateFechFin.Month - 1
            Dim strDetalle As String = ""

            For i = intMesInicial To intMesFinal
                If intMesInicial = intMesFinal Then
                    strDetalle = strArrMeses(i)
                    Exit For
                Else
                    If (i + 1) = intMesFinal Then
                        strDetalle += strArrMeses(i) & " y "
                    Else
                        If i = intMesFinal Then
                            strDetalle += strArrMeses(i)
                        Else
                            strDetalle += strArrMeses(i) & ", "
                        End If
                    End If
                End If
            Next

            Return strDetalle
        End Function

        Function fnc_obtener_fecha_fin(ByVal intMes As Integer, ByVal intAño As Integer) As String
            Dim dateFecha As DateTime = DateTime.Parse(intAño & "/" & intMes & "/01")

            Return Format(dateFecha.AddMonths(1).AddDays(-1), "yyyy/MM/dd")
        End Function

        Function fnc_bono_generado(ByVal intCodEsquema As Integer) As Boolean
            strQuery = String.Format("SELECT " & _
                                    "	COUNT(esq_codigo) " & _
                                    "FROM " & _
                                    "	SIG_T.dbo.f_pla_bonos_act " & _
                                    "WHERE " & _
                                    "	esq_codigo = {0} ", intCodEsquema)
            arrObjeto = objConexionDB.fnc_crear_arreglo_regis_unico(strQuery, 1)

            If arrObjeto(0) = 0 Then
                Return False
            Else
                Return True
            End If
        End Function

        Function fnc_generar_bono(ByVal strArrCodEsquema() As String, ByVal intCodUsuario As Integer) As Integer
            Dim strArrDatosEsquemas() As String
            Dim intCantEsqNoGene As Integer = 0
            Dim intCantEsqSiGene As Integer = 0
            Dim strNumMeses() As String
            Dim dateByBonoFechIni As DateTime
            Dim datebyBonoFechFin As DateTime
            Dim intNumeMesFin As Integer = 1
            Dim intInterval As Integer
            Dim intNum As Integer = 0
            Dim intRespuesta As Integer = 1
            Dim bolEstaGeneradoBono As Boolean
            Dim boolEsqGenerado As Boolean = False

            For i = 0 To strArrCodEsquema.Length - 1
                If fnc_se_genero_esquema(strArrCodEsquema(i)) = True Then
                    boolEsqGenerado = True
                    Exit For
                End If
            Next

            If boolEsqGenerado = False Then
                For i = 0 To strArrCodEsquema.Length - 1
                    bolEstaGeneradoBono = fnc_bono_generado(strArrCodEsquema(i))
                    If bolEstaGeneradoBono = False And intRespuesta = 1 Then
                        intCantEsqSiGene += 1
                        strQuery = String.Format("SELECT  " & _
                                                "	esq_meses, " & _
                                                "	esq_tipo_intervalo, " & _
                                                "	esq_anyo, " & _
                                                "	esq_cant_bonos_act, " & _
                                                "	CONVERT(DATE, esq_fecha_elegibilidad, 103) AS esq_fecha_elegibilidad, " & _
                                                "	esq_detalle_meses, " & _
                                                "	CONVERT(DATE, esq_fecha_ini, 103) AS esq_fecha_ini, " & _
                                                "	CONVERT(DATE, esq_fecha_fin, 103) AS esq_fecha_fin " & _
                                                "FROM " & _
                                                "	SIG_T.dbo.f_pla_esquema " & _
                                                "WHERE " & _
                                                "	esq_codigo = {0} ", strArrCodEsquema(i))

                        strArrDatosEsquemas = objConexionDB.fnc_crear_arreglo_regis_unico(strQuery, 8)

                        strQuery = String.Format("SELECT " & _
                                                "	int_cant_meses " & _
                                                "FROM " & _
                                                "	SIG_T.dbo.f_pla_intervalo " & _
                                                "WHERE " & _
                                                "	int_codigo = {0} ", strArrDatosEsquemas(1))

                        strNumMeses = objConexionDB.fnc_crear_arreglo_regis_unico(strQuery, 1)

                        dateByBonoFechIni = DateTime.Parse(Format(Convert.ToDateTime(strArrDatosEsquemas(6)), "yyyy/MM/dd"))
                        datebyBonoFechFin = DateTime.Parse(Format(Convert.ToDateTime(strArrDatosEsquemas(7)), "yyyy/MM/dd"))

                        For j = 1 To CInt(strArrDatosEsquemas(3))
                            If intRespuesta = 1 Then
                                intNum += 1
                                intNumeMesFin = ((dateByBonoFechIni.Month - 1) + CInt(strNumMeses(0)))
                                datebyBonoFechFin = DateTime.Parse(fnc_obtener_fecha_fin(intNumeMesFin, CInt(strArrDatosEsquemas(2))))
                                intInterval = datebyBonoFechFin.Month / CInt(strNumMeses(0))

                                intRespuesta = objConexionDB.fnc_p_pla_nuevo_bono(strArrCodEsquema(i), _
                                                                    intNum, _
                                                                    intInterval, _
                                                                    strArrDatosEsquemas(1), _
                                                                    strArrDatosEsquemas(2), _
                                                                    Format(dateByBonoFechIni, "yyyy/MM/dd"), _
                                                                    Format(datebyBonoFechFin, "yyyy/MM/dd"), _
                                                                    strArrDatosEsquemas(4), _
                                                                    fnc_obtener_detalle_meses(dateByBonoFechIni, datebyBonoFechFin), _
                                                                    CInt(strNumMeses(0)), _
                                                                    intCodUsuario)
                                dateByBonoFechIni = dateByBonoFechIni.AddMonths(1)
                                intRespuesta = objConexionDB.fnc_p_pla_desaprobar_esquema_cambio(strArrCodEsquema(i), intCodUsuario)
                            Else
                                Exit For
                            End If
                        Next
                    Else
                        intCantEsqNoGene += 1
                    End If
                Next

                If intRespuesta = 1 And intCantEsqNoGene = 0 Then
                    Return 1 'De todos los esquemas seleccionados se generaron los bonos.
                ElseIf intCantEsqNoGene > 0 And intCantEsqSiGene > 0 Then
                    Return 3 'No a todos los esquemas se les generarón los bonos, debido a que ya se les habia ejecutado los bonos.
                ElseIf intCantEsqSiGene > 1 And intRespuesta = 0 Then
                    Return 6 'No se generaron todos los bonos.
                ElseIf intRespuesta = 1 And intCantEsqNoGene > 0 And intCantEsqSiGene = 0 Then
                    Return 2 'De todos los esquemas ninguno se le generaron los bonos ya se habían generado.
                Else
                    Return 0 'Error en el servidor.
                End If
            Else
                Return 5
            End If
        End Function

        Function fnc_borrar_bonos(ByVal intCodBono As Integer, ByVal intCodUsuario As Integer) As Integer
            Dim arrYaSeEjecuto() As String

            strQuery = String.Format("SELECT " & _
                                    "	esq_codigo " & _
                                    "FROM " & _
                                    "	SIG_T.[dbo].[f_pla_bonos_act] " & _
                                    "WHERE " & _
                                    "	bon_codigo = {0} ", intCodBono)
            arrObjeto = objConexionDB.fnc_crear_arreglo_regis_unico(strQuery, 1)

            strQuery = String.Format("SELECT  " & _
                                    "        TOP 1 1 " & _
                                    "FROM  " & _
                                    "    SIG_T.dbo.f_pla_titulares  " & _
                                    "WHERE  " & _
                                    "    tit_esquema =  {0} ", arrObjeto(0))
            arrYaSeEjecuto = objConexionDB.fnc_crear_arreglo_regis_unico(strQuery, 1)

            If arrYaSeEjecuto(0) Is Nothing Then
                If objConexionDB.fnc_p_pla_borrar_bono(intCodBono, intCodUsuario) = 0 And objConexionDB.fnc_p_pla_desaprobar_esquema_cambio(arrObjeto(0), intCodUsuario) = 0 Then
                    Return 0
                Else
                    Return 1
                End If
            Else
                Return 2 'El esquema ya fue ejecutado, imposible borrar.
            End If
        End Function

        Function fnc_se_genero_esquema(ByRef intCodEsqeuma As Integer) As Boolean
            strQuery = String.Format("SELECT  " & _
                                    "        TOP 1 1 " & _
                                    "FROM  " & _
                                    "    SIG_T.dbo.f_pla_titulares  " & _
                                    "WHERE  " & _
                                    "    tit_esquema =  {0} ", intCodEsqeuma)
            arrObjeto = objConexionDB.fnc_crear_arreglo_regis_unico(strQuery, 1)

            If arrObjeto(0) Is Nothing Then
                Return False
            Else
                Return True
            End If
        End Function

    End Class
End Namespace
