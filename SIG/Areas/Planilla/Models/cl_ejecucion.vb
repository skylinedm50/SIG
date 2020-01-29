Namespace SIG.Areas.Planilla.Models

    Public Class cl_ejecucion

        Dim conexion As SIG.Areas.Planilla.Models.cl_conexion_db = New SIG.Areas.Planilla.Models.cl_conexion_db

        Public Function fnc_generar_planilla(ByVal esquema As String) As Integer

            Dim MyResult As Integer
            Dim aprovada As Boolean
            Dim generada As Integer

            Dim sql As String = "" + vbCr + _
                "DECLARE	@return_value int" + vbCr + _
                "" + vbCr + _
                "EXEC	@return_value = [SIG_T].[dbo].[P_Generar_P1]" + vbCr + _
                "       @P_proy_o_plan = 1," + vbCr + _
                "		@p_esquema = " + esquema + "," + vbCr + _
                "		@cod_usuario = " + HttpContext.Current.Session("usuario").ToString() + "," + vbCr + _
                "		@des = 0" + vbCr + _
                "" + vbCr + _
                "SELECT	'Return Value' = @return_value"

            '0 inicio el procedimiento pero no termino, así que no se puedo generar
            '1 la planilla se genero bien
            '2 planilla no generada, pero falta aprobación de los parámetros (o no fueron aprovados por Don Luis)
            '3 planilla aprobada, pero ya generada
            '4 falta aprobación de los parámetros y la planilla ya fue generada

            Try
                aprovada = fnc_verificar_aprovacion_esquema(esquema)
                generada = fnc_verificar_generacion_esquema(esquema)

                If aprovada And generada = 0 Then           '(11) planilla aprobada y no generada
                    MyResult = conexion.fnc_obtener_scalar(sql)

                    If MyResult = 1 Then
                        sql = "EXEC SIG_T.dbo.p_pla_actualizar_estado_esquemas " + esquema
                        conexion.fnc_ejecutar_simple_comando(sql)
                    End If

                ElseIf Not aprovada And generada = 0 Then   '(01) planilla no aprobada y no generada
                    MyResult = 2
                ElseIf aprovada And generada <> 0 Then      '(10) planilla probada y generada
                    MyResult = 3
                ElseIf Not aprovada And generada <> 0 Then  '(00) planilla no aprovada y generada
                    MyResult = 4
                End If
            Catch ex As Exception
                MyResult = -1
            End Try

            Return MyResult

        End Function

        Public Function fnc_obtener_nombre_esquema(ByVal esquema As String) As String

            Dim MyResult As String
            Dim sql As String = "" + vbCr + _
                "SELECT nombre_esquema" + vbCr + _
                "	FROM SIG_T.dbo.f_pla_esquema " + vbCr + _
                "   WHERE esq_codigo = " + esquema

            Try
                MyResult = conexion.fnc_obtener_scalar(sql)
            Catch ex As Exception
                Return ex.Message
            End Try

            Return MyResult

        End Function

        Public Function fnc_verificar_aprovacion_esquema(ByVal esquema As String) As Boolean

            'el código de usuario 8 pertenece a Don Luis, verifico que la aprovación de los parámetros del esquema se hayan hecho con su usuario
            Dim MyResult As New Boolean
            Dim sql As String = "" + vbCr +
                "SELECT *" + vbCr +
                "	FROM SIG_T.dbo.f_pla_esquema AS esq" + vbCr +
                "		INNER JOIN SIG_T.dbo.f_pla_esquemaH AS esqh ON esqh.esq_codigo = esq.esq_codigo" + vbCr +
                "	WHERE esq.esq_aprobado = 1 AND esqh.esq_desc_cambio = 'Aprobado' AND esqh.esq_aprobado = 0 " + vbCr +
                "		AND esq.esq_codigo = " + esquema

            Try
                MyResult = conexion.fnc_verificar_existe_registros(sql)
            Catch ex As Exception
            End Try

            Return MyResult

        End Function

        Public Function fnc_verificar_generacion_esquema(ByVal esquema As String) As Integer

            Dim MyResult As New Integer
            Dim sql As String = "" + vbCr + _
                "SELECT COUNT(*)" + vbCr + _
                "	FROM SIG_T.dbo.f_pla_titulares AS tit" + vbCr + _
                "		INNER JOIN SIG_T.dbo.f_pla_esquema AS esq ON esq.esq_codigo = tit.tit_esquema" + vbCr + _
                "   WHERE esq.esq_tipo_esquema = 1 And esq.esq_codigo = " + esquema

            Try
                MyResult = conexion.fnc_obtener_scalar(sql)
            Catch ex As Exception
                Return -1
            End Try

            Return MyResult

        End Function

        Public Function fnc_verificar_estado_esquema(ByVal esquema As String) As Integer

            Dim MyResult As New Integer
            Dim sql As String = "" + vbCr + _
                "SELECT Esq_Estado" + vbCr + _
                "	FROM SIG_T.dbo.f_pla_esquema " + vbCr + _
                "   WHERE esq_codigo = " + esquema

            Try
                MyResult = conexion.fnc_obtener_scalar(sql)
            Catch ex As Exception
                Return -1
            End Try

            Return MyResult

        End Function

        Public Function fnc_verificar_pagos_esquema(ByVal esquema As String) As Integer

            Dim MyResult As New Integer
            Dim sql As String = "" + vbCr + _
                "SELECT COUNT(*)" + vbCr + _
                "	FROM SIG_T.dbo.f_pla_titulares AS tit" + vbCr + _
                "		INNER JOIN SIG_T.dbo.f_pla_esquema AS esq ON esq.esq_codigo = tit.tit_esquema" + vbCr + _
                "   WHERE tit.tit_cobro = 1 And esq.esq_codigo = " + esquema

            Try
                MyResult = conexion.fnc_obtener_scalar(sql)
            Catch ex As Exception
                Return -1
            End Try

            Return MyResult

        End Function

        Public Function fnc_obtener_planilla_generada(ByVal esquema As String) As DataTable

            Dim MyResult As New DataTable
            Dim sql As String = "" + vbCr + _
                "SELECT	geo.desc_departamento, geo.desc_municipio," + vbCr + _
                "		fon.fond_nombre, pag.Nombre_Pagador," + vbCr + _
                "		ele.eleg_nombre, CAST(esq.nombre_esquema AS nvarchar(255)) AS 'periodo'," + vbCr + _
                "		COUNT(*) AS 'total_hogares'," + vbCr + _
                "		SUM(tit.tit_monto_total) AS 'monto_total'" + vbCr + _
                "	FROM SIG_T.dbo.f_pla_titulares AS tit" + vbCr + _
                "		INNER JOIN SIG_T.dbo.f_pla_esquema AS esq ON esq.esq_codigo = tit.tit_esquema" + vbCr + _
                "		INNER JOIN SIG_T.dbo.V_Glo_caserios AS geo ON geo.cod_caserio = tit.tit_cas_codigo" + vbCr + _
                "		INNER JOIN SIG_T.dbo.f_pla_fondos AS fon ON fon.fond_codigo = tit.tit_fondo" + vbCr + _
                "		INNER JOIN SIG_T.dbo.f_pla_pagadores AS pag ON pag.Codigo_Pagador = tit.tit_pagador" + vbCr + _
                "		INNER JOIN SIG_T.dbo.f_pla_elegibilidades AS ele ON ele.eleg_codigo = tit.tit_elegibilidad" + vbCr + _
                "	WHERE esq.esq_tipo_esquema = 1 AND tit.tit_planilla = 1 AND esq.esq_codigo IN (" + esquema + ") " + vbCr + _
                "	GROUP BY geo.desc_departamento, geo.desc_municipio, " + vbCr + _
                "		fon.fond_nombre, pag.Nombre_Pagador," + vbCr + _
                "		ele.eleg_nombre, CAST(esq.nombre_esquema AS nvarchar(255))"

            Try
                MyResult = conexion.fnc_crear_datatable(sql)
            Catch ex As Exception
            End Try

            Return MyResult

        End Function

        Public Function fnc_anular_planilla(ByVal esquema As String, ByVal observacion As String)

            Dim MyResult As Integer
            Dim sql As String = "" + vbCr + _
                "DECLARE	@return_value int" + vbCr + _
                "" + vbCr + _
                "EXEC	@return_value = [SIG_T].[dbo].[p_pla_anular_esquema]" + vbCr + _
                "		@intCodUsuario = " + HttpContext.Current.Session("usuario").ToString() + "," + vbCr + _
                "		@intCodEsquema = " + esquema + "," + vbCr + _
                "		@strObservaciones = N'" + observacion + "'" + vbCr + _
                "" + vbCr + _
                "SELECT	'Return Value' = @return_value"

            Try

                If fnc_verificar_generacion_esquema(esquema) > 0 And fnc_verificar_estado_esquema(esquema) <> 9 And fnc_verificar_pagos_esquema(esquema) = 0 Then
                    MyResult = conexion.fnc_obtener_scalar(sql)
                Else
                    MyResult = 0
                End If

            Catch ex As Exception
                Dim o = 0
            End Try

            Return MyResult

        End Function

    End Class

End Namespace
