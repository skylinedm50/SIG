Namespace SIG.Areas.Mineria.Models

    Public Class Cl_PlanillasPago

        Dim conexion As SIG.Areas.Mineria.Models.Cl_Conexion = New SIG.Areas.Mineria.Models.Cl_Conexion
        Dim share As Cl_Shared = New Cl_Shared

#Region "Funciones para el listado de titulares"

        Public Function Fnc_listado_particitantes(ByVal pago As String, ByVal tipo As String, ByVal departamento As String, ByVal municipio As String, ByVal aldea As String) As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr +
                "SELECT geo.cod_departamento, geo.desc_departamento, geo.cod_municipio, geo.desc_municipio, geo.cod_aldea, geo.desc_aldea," + vbCr +
                "       geo.cod_caserio, geo.desc_caserio, det.cod_hogar, det.identidad_titular, det.nombre_titular," + vbCr +
                "       CASE" + vbCr +
                "		    WHEN det.programado = 0 AND det.cobro = 0 THEN 'Excluido'" + vbCr +
                "			WHEN det.programado = 1 AND det.cobro = 0 THEN 'No Pagado'" + vbCr +
                "			WHEN det.programado = 1 AND det.cobro = 1 THEN 'Pagado'" + vbCr +
                "		END AS 'Estado'," + vbCr +
                "		ficha.numero_ficha, det.año_ficha," + vbCr +
                "		CASE WHEN ficha.censo = 'ISAAC' THEN 'SSIS' ELSE ficha.censo END AS 'censo'" + vbCr +
                "   FROM SIG_C.dbo.t_sig_detalle_planillas AS det" + vbCr +
                "       INNER JOIN SIG_C.dbo.t_sig_ubicaciones_geograficas AS geo ON geo.cod_caserio = det.cod_caserio" + vbCr +
                "       LEFT JOIN SIG_C.dbo.t_sig_fichas AS ficha ON ficha.cod_ficha = det.cod_ficha" + vbCr +
                "   WHERE det.cod_pago = " + pago

            Select Case tipo
                Case "Programados"
                    sql += " AND det.programado = 1"
                Case "Excluidos"
                    sql += " AND det.programado = 0"
            End Select

            If Not aldea Is Nothing Then
                sql += " AND geo.cod_aldea = '" + aldea + "'"
            ElseIf Not municipio Is Nothing Then
                sql += " AND geo.cod_municipio = '" + municipio + "'"
            ElseIf Not departamento Is Nothing Then
                sql += " AND geo.cod_departamento = '" + departamento + "'"
            End If

            If share.fnc_limitar_area_geografica(HttpContext.Current.Session("usuario")) Then

                sql += "" + vbCr +
                    "		AND geo.cod_departamento IN (" + vbCr +
                    "			SELECT cod_departamento" + vbCr +
                    "				FROM SIG_T.dbo.t_usuario_departamentos" + vbCr +
                    "				WHERE cod_usuario = " + HttpContext.Current.Session("usuario").ToString() + vbCr +
                    "		)"

            End If

            sql += vbCr + "   ORDER BY geo.cod_departamento, geo.cod_municipio, geo.cod_aldea, det.nombre_titular"
            Try
                MyResult = conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)

        End Function

        Public Function Fnc_estado_cuenta_participante(ByVal pago As String, ByVal identidad As String) As DataSet

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr +
                "SELECT ISNULL(identidad_titular,'') AS 'identidad_titular'," + vbCr +
                "       ISNULL(nombre_titular,'') AS 'nombre_titular'," + vbCr +
                "		CASE WHEN programado = 1 THEN 'Programado' ELSE 'Excluido' END AS 'estado'," + vbCr +
                "       ISNULL(monto_total,'') AS 'monto_total'," + vbCr +
                "       ISNULL(deducciones,'') AS 'deducciones'," + vbCr +
                "       ISNULL(nombre_elegibilidad,'') AS 'nombre_elegibilidad'," + vbCr +
                "       ISNULL(desc_elegibilidad,'') AS 'desc_elegibilidad', " + vbCr +
                "       ISNULL(nombre_componente,'') AS 'nombre_componente'," + vbCr +
                "       ISNULL(desc_componente,'') AS 'desc_componente'," + vbCr +
                "       ISNULL(nombre_corresponsabilidad,'') AS 'nombre_corresponsabilidad'," + vbCr +
                "       ISNULL(desc_correponsabilidad,'') AS 'desc_correponsabilidad'," + vbCr +
                "       proyeccion,proyeccion_corta," + vbCr +
                "       CASE WHEN cobro = 1 THEN 'Cobró' ELSE 'No Cobró' END AS 'cobro', fecha_cobro," + vbCr +
                "       ISNULL(estado_hogar,'') AS 'estado_hogar'" + vbCr +
                "   FROM SIG_C.dbo.t_sig_detalle_planillas As det" + vbCr +
                "       INNER JOIN SIG_C.dbo.t_sig_ubicaciones_geograficas As geo On geo.cod_caserio = det.cod_caserio" + vbCr +
                "   WHERE cod_pago = " + pago + " And identidad_titular = '" + identidad + "'" + vbCr +
                "" + vbCr +
                "SELECT nivel,nivel_ciclo,niños_total,niños_cumpliendo,niños_apercibidos," + vbCr +
                "		niños_no_cumpliendo,monto_nivel_neto" + vbCr +
                "   FROM SIG_C.dbo.t_sig_estado_cuenta_participantes" + vbCr +
                "   WHERE cod_pago = " + pago + " AND identidad_titular = '" + identidad + "'"

            Try
                MyResult = conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            MyResult.Tables(0).TableName = "info_planilla"
            MyResult.Tables(1).TableName = "estado_cuenta"

            Return MyResult

        End Function

        Public Function Fnc_listado_ninos(ByVal pago As String, ByVal identidad As String) As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr + _
                "SELECT cod_persona, identidad, nombre_persona, desc_nivel_elegibilidad ,desc_corresponsabilidad, estado_corresponsabilidad" + vbCr + _
                "   FROM SIG_C.dbo.t_sig_corresp_pers_planillas" + vbCr + _
                "	WHERE cod_pago = " + pago + " AND cod_hogar = (" + vbCr + _
                "        SELECT cod_hogar" + vbCr + _
                "			FROM SIG_C.dbo.t_sig_detalle_planillas" + vbCr + _
                "			WHERE identidad_titular = '" + identidad + "' AND cod_pago = " + pago + vbCr + _
                "	)" + vbCr + _
                "   ORDER BY nombre_persona"

            Try
                MyResult = conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)

        End Function

        Public Function Fnc_detalle_correponsabilidad_nino(ByVal pago As String, ByVal cod_persona As String) As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr + _
                "SELECT identidad, fecha_nacimiento, sexo_persona, nombre_persona, elegibilidad, desc_nivel_elegibilidad, desc_corresponsabilidad," + vbCr + _
                "       cod_centro_educativo_salud, nombre_centro_educativo_salud, año, nombre_grado, numero_visitas_centro_salud," + vbCr + _
                "       fecha_ultima_visita, estado_corresponsabilidad" + vbCr + _
                "   FROM SIG_C.dbo.t_sig_corresp_pers_planillas" + vbCr + _
                "   WHERE cod_pago = " + pago + " And cod_persona = " + cod_persona

            Try
                MyResult = conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)

        End Function



#End Region

#Region "Funciones para el consolidado por pago"

        Public Function Fnc_consolidado_pago(ByVal pago As String) As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr +
                "SELECT geo.cod_departamento, geo.desc_departamento, geo.cod_municipio, geo.desc_municipio," + vbCr +
                "		geo.cod_aldea, geo.desc_aldea, geo.cod_caserio, geo.desc_caserio," + vbCr +
                "		pla.cod_fondo, pla.nombre_fondo, pla.cod_esquema, pla.nombre_esquema,det.cod_elegibilidad," + vbCr +
                "		det.nombre_elegibilidad," + vbCr +
                "		CASE" + vbCr +
                "		    WHEN detalle_bono = 'BONO: Educación' THEN 'Educación'" + vbCr +
                "		    WHEN detalle_bono = 'BONO: Educación y Salud' THEN 'Educación y Salud'" + vbCr +
                "		    WHEN detalle_bono = 'BONO: Salud' THEN 'Salud'" + vbCr +
                "		    WHEN detalle_bono = 'NO' THEN 'Acumulado'" + vbCr +
                "		END AS 'nombre_componente'," + vbCr +
                "       pla.cod_pagador, pla.nombre_pagador, det.monto_total, pla.tipo_pago," + vbCr +
                "		COUNT(*) AS 'hogares_programados'," + vbCr +
                "		SUM(det.monto_total) AS 'monto_programado'," + vbCr +
                "		COUNT(CASE WHEN det.cobro = 1 THEN 1 ELSE NULL END) AS 'hogares_pagados'," + vbCr +
                "		SUM(CASE WHEN det.cobro = 1 THEN det.monto_total ELSE 0 END) AS 'monto_pagado'," + vbCr +
                "		COUNT(CASE WHEN det.cobro = 0 THEN 1 ELSE NULL END) AS 'hogares_no_pagados'," + vbCr +
                "		SUM(CASE WHEN det.cobro = 0 THEN det.monto_total ELSE 0 END) AS 'monto_no_pagado'" + vbCr +
                "	FROM SIG_C.dbo.t_sig_planillas AS pla" + vbCr +
                "		INNER JOIN SIG_C.dbo.t_sig_ubicaciones_geograficas AS geo ON geo.cod_caserio = pla.cod_caserio" + vbCr +
                "		INNER JOIN SIG_C.dbo.t_sig_detalle_planillas AS det" + vbCr +
                "			ON det.cod_pago = pla.cod_pago AND det.cod_caserio = pla.cod_caserio AND det.esquema = pla.cod_esquema" + vbCr +
                "            WHERE det.programado = 1 AND pla.cod_pago = " + pago + vbCr +
                "	GROUP BY geo.cod_departamento, geo.desc_departamento, geo.cod_municipio, geo.desc_municipio," + vbCr +
                "		geo.cod_aldea, geo.desc_aldea, geo.cod_caserio, geo.desc_caserio," + vbCr +
                "		pla.cod_fondo, pla.nombre_fondo, pla.cod_esquema, pla.nombre_esquema," + vbCr +
                "		det.cod_elegibilidad, det.nombre_elegibilidad, det.detalle_bono, pla.cod_pagador, pla.nombre_pagador, det.monto_total, pla.tipo_pago" + vbCr +
                "	ORDER BY cod_departamento, cod_municipio, desc_municipio, desc_aldea, cod_elegibilidad, pla.tipo_pago"

            Try
                MyResult = conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)

        End Function

#End Region

#Region "Funciones para la comparación entre planillas"

        Public Function Fnc_comparacion_planillas(ByVal planilla1 As String, ByVal planilla2 As String) As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr + _
                "SELECT geo.cod_departamento, geo.desc_departamento, geo.cod_municipio, geo.desc_municipio," + vbCr + _
                "		geo.cod_aldea, geo.desc_aldea, geo.cod_caserio, geo.desc_caserio," + vbCr + _
                "		COUNT(planilla_1.cod_hogar) AS 'total_planilla_1'," + vbCr + _
                "		COUNT(planilla_2.cod_hogar) AS 'total_planilla_2'," + vbCr + _
                "		COUNT(" + vbCr + _
                "			CASE WHEN planilla_1.cod_hogar IS NOT NULL AND planilla_2.cod_hogar IS NOT NULL THEN 1 ELSE NULL END" + vbCr + _
                "		) AS 'arrastre'," + vbCr + _
                "		COUNT(" + vbCr + _
                "			CASE WHEN planilla_1.cod_hogar IS NOT NULL AND planilla_2.cod_hogar IS NULL THEN 1 ELSE NULL END" + vbCr + _
                "		) AS 'bajas'," + vbCr + _
                "		COUNT(" + vbCr + _
                "			CASE WHEN planilla_1.cod_hogar IS NULL AND planilla_2.cod_hogar IS NOT NULL THEN 1 ELSE NULL END" + vbCr + _
                "		) AS 'altas'" + vbCr + _
                "	FROM SIG_C.dbo.t_sig_ubicaciones_geograficas AS geo" + vbCr + _
                "		INNER JOIN SIG_C.dbo.t_sig_hogares AS hog ON hog.cod_caserio = geo.cod_caserio" + vbCr + _
                "		LEFT JOIN (" + vbCr + _
                "			SELECT cod_hogar" + vbCr + _
                "				FROM SIG_C.dbo.t_sig_detalle_planillas" + vbCr + _
                "				WHERE cod_pago = " + planilla1 + " AND programado = 1" + vbCr + _
                "		) AS planilla_1 ON planilla_1.cod_hogar = hog.cod_hogar" + vbCr + _
                "		LEFT JOIN (" + vbCr + _
                "			SELECT cod_hogar" + vbCr + _
                "				FROM SIG_C.dbo.t_sig_detalle_planillas" + vbCr + _
                "				WHERE cod_pago = " + planilla2 + " AND programado = 1" + vbCr + _
                "		) AS planilla_2 ON planilla_2.cod_hogar = hog.cod_hogar" + vbCr + _
                "	GROUP BY geo.cod_departamento, geo.desc_departamento, geo.cod_municipio, geo.desc_municipio," + vbCr + _
                "		geo.cod_aldea, geo.desc_aldea, geo.cod_caserio, geo.desc_caserio" + vbCr + _
                "	ORDER BY geo.cod_departamento, geo.cod_municipio, geo.cod_aldea, geo.cod_caserio"

            Try
                MyResult = conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)

        End Function

        Public Function Fnc_diferencia_entre_altas_bajas_planillas(ByVal planilla1 As String, ByVal planilla2 As String)

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr + _
                "SELECT /*geo.cod_departamento,*/" + vbCr + _
                "		CASE" + vbCr + _
                "			WHEN geo.cod_departamento = '01' THEN 'hn-at'" + vbCr + _
                "			WHEN geo.cod_departamento = '02' THEN 'hn-cl'" + vbCr + _
                "			WHEN geo.cod_departamento = '03' THEN 'hn-cm'" + vbCr + _
                "			WHEN geo.cod_departamento = '04' THEN 'hn-cp'" + vbCr + _
                "			WHEN geo.cod_departamento = '05' THEN 'hn-cr'" + vbCr + _
                "			WHEN geo.cod_departamento = '06' THEN 'hn-ch'" + vbCr + _
                "			WHEN geo.cod_departamento = '07' THEN 'hn-ep'" + vbCr + _
                "			WHEN geo.cod_departamento = '08' THEN 'hn-fm'" + vbCr + _
                "			WHEN geo.cod_departamento = '09' THEN 'hn-gd'" + vbCr + _
                "			WHEN geo.cod_departamento = '10' THEN 'hn-in'" + vbCr + _
                "			WHEN geo.cod_departamento = '11' THEN 'hn-ib'" + vbCr + _
                "			WHEN geo.cod_departamento = '12' THEN 'hn-lp'" + vbCr + _
                "			WHEN geo.cod_departamento = '13' THEN 'hn-le'" + vbCr + _
                "			WHEN geo.cod_departamento = '14' THEN 'hn-oc'" + vbCr + _
                "			WHEN geo.cod_departamento = '15' THEN 'hn-ol'" + vbCr + _
                "			WHEN geo.cod_departamento = '16' THEN 'hn-sb'" + vbCr + _
                "			WHEN geo.cod_departamento = '17' THEN 'hn-va'" + vbCr + _
                "			WHEN geo.cod_departamento = '18' THEN 'hn-yo'" + vbCr + _
                "		END AS 'hc-key'," + vbCr + _
                "       COUNT(" + vbCr + _
                "			CASE WHEN planilla_1.cod_hogar IS NULL AND planilla_2.cod_hogar IS NOT NULL THEN 1 ELSE NULL END" + vbCr + _
                "		) - " + vbCr + _
                "		COUNT(" + vbCr + _
                "			CASE WHEN planilla_1.cod_hogar IS NOT NULL AND planilla_2.cod_hogar IS NULL THEN 1 ELSE NULL END" + vbCr + _
                "		) AS 'value'" + vbCr + _
                "	FROM SIG_C.dbo.t_sig_ubicaciones_geograficas AS geo" + vbCr + _
                "		INNER JOIN SIG_C.dbo.t_sig_hogares AS hog ON hog.cod_caserio = geo.cod_caserio" + vbCr + _
                "		LEFT JOIN (" + vbCr + _
                "			SELECT cod_hogar" + vbCr + _
                "				FROM SIG_C.dbo.t_sig_detalle_planillas" + vbCr + _
                "				WHERE cod_pago = " + planilla1 + " AND programado = 1" + vbCr + _
                "		) AS planilla_1 ON planilla_1.cod_hogar = hog.cod_hogar" + vbCr + _
                "		LEFT JOIN (" + vbCr + _
                "			SELECT cod_hogar" + vbCr + _
                "				FROM SIG_C.dbo.t_sig_detalle_planillas" + vbCr + _
                "				WHERE cod_pago = " + planilla2 + " AND programado = 1" + vbCr + _
                "		) AS planilla_2 ON planilla_2.cod_hogar = hog.cod_hogar" + vbCr + _
                "	GROUP BY geo.cod_departamento" + vbCr + _
                "	ORDER BY geo.cod_departamento"

            Try
                MyResult = conexion.GetDataSet(sql)
                Return MyResult.Tables(0)
            Catch ex As Exception
                Return Nothing
            End Try

        End Function

        Public Function Fnc_diferencia_entre_altas_bajas_planillas_departamento(ByVal planilla1 As String, ByVal planilla2 As String, ByVal departamento As String) As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr + _
                "SELECT geo.cod_municipio AS 'codigo'," + vbCr + _
                "       COUNT(" + vbCr + _
                "			CASE WHEN planilla_1.cod_hogar IS NULL AND planilla_2.cod_hogar IS NOT NULL THEN 1 ELSE NULL END" + vbCr + _
                "		) - " + vbCr + _
                "		COUNT(" + vbCr + _
                "			CASE WHEN planilla_1.cod_hogar IS NOT NULL AND planilla_2.cod_hogar IS NULL THEN 1 ELSE NULL END" + vbCr + _
                "		) AS 'value'" + vbCr + _
                "	FROM SIG_C.dbo.t_sig_ubicaciones_geograficas AS geo" + vbCr + _
                "		INNER JOIN SIG_C.dbo.t_sig_hogares AS hog ON hog.cod_caserio = geo.cod_caserio" + vbCr + _
                "		LEFT JOIN (" + vbCr + _
                "			SELECT cod_hogar" + vbCr + _
                "				FROM SIG_C.dbo.t_sig_detalle_planillas" + vbCr + _
                "				WHERE cod_pago = " + planilla1 + " AND programado = 1" + vbCr + _
                "		) AS planilla_1 ON planilla_1.cod_hogar = hog.cod_hogar" + vbCr + _
                "		LEFT JOIN (" + vbCr + _
                "			SELECT cod_hogar" + vbCr + _
                "				FROM SIG_C.dbo.t_sig_detalle_planillas" + vbCr + _
                "				WHERE cod_pago = " + planilla2 + " AND programado = 1" + vbCr + _
                "		) AS planilla_2 ON planilla_2.cod_hogar = hog.cod_hogar" + vbCr + _
                "   WHERE geo.cod_departamento = '" + departamento + "'" + vbCr + _
                "	GROUP BY geo.cod_municipio" + vbCr + _
                "	ORDER BY geo.cod_municipio"

            Try
                MyResult = conexion.GetDataSet(sql)
                Return MyResult.Tables(0)
            Catch ex As Exception
                Return Nothing
            End Try

        End Function

        Public Function fnc_razon_caida_hogares(ByVal planilla1 As String, ByVal planilla2 As String) As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr + _
                "SELECT geo.cod_departamento, geo.desc_departamento, geo.cod_municipio, geo.desc_municipio," + vbCr + _
                "		geo.cod_aldea, geo.desc_aldea, geo.cod_caserio, geo.desc_caserio," + vbCr + _
                "       det.proyeccion_corta, COUNT(*) AS 'total'" + vbCr + _
                "   FROM SIG_C.dbo.t_sig_ubicaciones_geograficas AS geo" + vbCr + _
                "		INNER JOIN SIG_C.dbo.t_sig_detalle_planillas AS det ON det.cod_caserio = geo.cod_caserio AND cod_pago = " + planilla2 + vbCr + _
                "		INNER JOIN (" + vbCr + _
                "            Select cod_hogar" + vbCr + _
                "				FROM SIG_C.dbo.t_sig_detalle_planillas" + vbCr + _
                "				WHERE programado = 1 AND cod_pago = " + planilla1 + vbCr + _
                "			EXCEPT" + vbCr + _
                "			SELECT cod_hogar" + vbCr + _
                "				FROM SIG_C.dbo.t_sig_detalle_planillas" + vbCr + _
                "				WHERE programado = 1 AND cod_pago = " + planilla2 + vbCr + _
                "		) AS BAJAS ON BAJAS.cod_hogar = det.cod_hogar" + vbCr + _
                "	GROUP BY geo.cod_departamento, geo.desc_departamento, geo.cod_municipio, geo.desc_municipio," + vbCr + _
                "		geo.cod_aldea, geo.desc_aldea, geo.cod_caserio, geo.desc_caserio, det.proyeccion_corta" + vbCr + _
                "	ORDER BY geo.cod_departamento, geo.cod_municipio, geo.cod_aldea, geo.cod_caserio"

            Try
                MyResult = conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)

        End Function

        

#End Region

#Region "Funciones para los elegibles contra los programados"

        Public Function Fnc_elegibles_contra_programados(ByVal pago As String) As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr + _
                "SELECT geo.cod_departamento, 	geo.desc_departamento, geo.cod_municipio, 	geo.desc_municipio, geo.cod_aldea, geo.desc_aldea, geo.cod_caserio, geo.desc_caserio," + vbCr + _
                "       info.total_hogares," + vbCr + _
                "       info.total_hogares_evaluados," + vbCr + _
                "       info.total_hogares_elegibles," + vbCr + _
                "       info.total_hogares_programados," + vbCr + _
                "       info.hogares_elegibles_educacion," + vbCr + _
                "       info.hogares_elegibles_educacion_salud," + vbCr + _
                "       info.hogares_elegibles_salud," + vbCr + _
                "       info.hogares_programados_educacion," + vbCr + _
                "       info.hogares_programados_educacion_salud," + vbCr + _
                "       info.hogares_programados_salud," + vbCr + _
                "       info.hogares_elegibles_primer_pago," + vbCr + _
                "       info.hogares_elegibles_repago," + vbCr + _
                "       info.hogares_programados_primer_pago," + vbCr + _
                "       info.hogares_programados_repago" + vbCr + _
                "	FROM SIG_C.dbo.t_sig_info_planillas AS info" + vbCr + _
                "		INNER JOIN SIG_C.dbo.t_sig_ubicaciones_geograficas AS geo ON geo.cod_caserio = info.cod_caserio" + vbCr + _
                "            WHERE info.cod_pago = " + pago + vbCr + _
                "	ORDER BY geo.cod_departamento, geo.cod_municipio, geo.cod_aldea, geo.cod_caserio"

            Try
                MyResult = conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)

        End Function

#End Region

#Region "Funciones para la razon de exclusiones de hogares"

        Public Function Fnc_razon_exclusion_hogares(ByVal pago As String) As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr + _
                "SELECT geo.cod_departamento, 	geo.desc_departamento, geo.cod_municipio, 	geo.desc_municipio, geo.cod_aldea, geo.desc_aldea, geo.cod_caserio, geo.desc_caserio," + vbCr + _
                "       proyeccion_corta, proyeccion, cant_hogares" + vbCr + _
                "	FROM SIG_C.dbo.t_sig_exclusiones_planillas AS exc" + vbCr + _
                "       INNER JOIN SIG_C.dbo.t_sig_ubicaciones_geograficas AS geo ON geo.cod_caserio = exc.cod_caserio" + vbCr + _
                "   WHERE exc.cod_pago = " + pago + vbCr + _
                "	ORDER BY geo.cod_departamento, geo.cod_municipio, geo.cod_aldea, geo.cod_caserio, proyeccion"

            Try
                MyResult = conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)

        End Function

#End Region

#Region "Funciones para censos y fichas utilizadas en un pago"

        Public Function Fnc_censo_ano_fichas_pago(ByVal pago As String) As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr + _
                "SELECT geo.cod_departamento, geo.desc_departamento, geo.cod_municipio, geo.desc_municipio," + vbCr + _
                "		geo.cod_aldea, geo.desc_aldea, geo.cod_caserio, geo.desc_caserio, det.censo_ficha, det.año_ficha," + vbCr + _
                "		COUNT(*) AS 'fichas'" + vbCr + _
                "	FROM SIG_C.dbo.t_sig_detalle_planillas AS det" + vbCr + _
                "		INNER JOIN SIG_C.dbo.t_sig_ubicaciones_geograficas AS geo ON geo.cod_caserio = det.cod_caserio" + vbCr + _
                "   WHERE det.cod_pago = " + pago + " AND det.programado = 1" + vbCr + _
                "   GROUP BY geo.cod_departamento, geo.desc_departamento, geo.cod_municipio, geo.desc_municipio," + vbCr + _
                "         geo.cod_aldea, geo.desc_aldea, geo.cod_caserio, geo.desc_caserio, det.censo_ficha, det.año_ficha" + vbCr + _
                "	ORDER BY geo.cod_departamento, geo.cod_municipio, geo.cod_aldea, geo.cod_caserio"

            Try
                MyResult = conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)

        End Function

#End Region

#Region "Funciones niños pagado por ciclo"

        Public Function Fnc_ninos_pagados_ciclo(ByVal pago As String) As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr + _
                "SELECT geo.cod_departamento, geo.desc_departamento, geo.cod_municipio, geo.desc_municipio," + vbCr + _
                "		geo.cod_aldea, geo.desc_aldea, geo.cod_caserio, geo.desc_caserio," + vbCr + _
                "		ciclos.descripcion_ciclo," + vbCr + _
                "		SUM(ciclos.total_niños) AS 'total_niños'," + vbCr + _
                "		SUM(ciclos.total_niños_cumpliento) AS 'total_niños_cumpliendo'," + vbCr + _
                "		SUM(ciclos.total_niños_apercibidos) AS 'total_niños_apercibidos'," + vbCr + _
                "		SUM(ciclos.total_niños_no_cumpliendo) AS 'total_niños_no_cumpliendo'," + vbCr + _
                "		SUM(ciclos.total_niños_programados) AS 'total_niños_programados'," + vbCr + _
                "		SUM(ciclos.total_niños_pagados) AS 'total_niños_pagados'," + vbCr + _
                "		SUM(ciclos.total_niños_programados - ciclos.total_niños_pagados) AS 'total_niños_no_pagados'" + vbCr + _
                "	FROM SIG_C.dbo.t_sig_ciclos_planillas AS ciclos" + vbCr + _
                "		INNER JOIN SIG_C.dbo.t_sig_ubicaciones_geograficas AS geo ON geo.cod_caserio = ciclos.cod_caserio" + vbCr + _
                "   WHERE ciclos.cod_ciclo > 0 AND ciclos.cod_pago = " + pago + vbCr + _
                "   GROUP BY geo.cod_departamento, geo.desc_departamento, geo.cod_municipio, geo.desc_municipio," + vbCr + _
                "         geo.cod_aldea, geo.desc_aldea, geo.cod_caserio, geo.desc_caserio, ciclos.descripcion_ciclo, ciclos.cod_ciclo" + vbCr + _
                "	ORDER BY geo.cod_departamento, geo.cod_municipio, geo.cod_aldea, geo.cod_caserio, ciclos.cod_ciclo"

            Try
                MyResult = conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)

        End Function

        Public Function Fnc_cantidad_ninos_ciclo(ByVal pago As String) As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr + _
                "SELECT /*cod_departamento,*/" + vbCr + _
                "		CASE" + vbCr + _
                "			WHEN cod_departamento = '01' THEN 'hn-at'" + vbCr + _
                "			WHEN cod_departamento = '02' THEN 'hn-cl'" + vbCr + _
                "			WHEN cod_departamento = '03' THEN 'hn-cm'" + vbCr + _
                "			WHEN cod_departamento = '04' THEN 'hn-cp'" + vbCr + _
                "			WHEN cod_departamento = '05' THEN 'hn-cr'" + vbCr + _
                "			WHEN cod_departamento = '06' THEN 'hn-ch'" + vbCr + _
                "			WHEN cod_departamento = '07' THEN 'hn-ep'" + vbCr + _
                "			WHEN cod_departamento = '08' THEN 'hn-fm'" + vbCr + _
                "			WHEN cod_departamento = '09' THEN 'hn-gd'" + vbCr + _
                "			WHEN cod_departamento = '10' THEN 'hn-in'" + vbCr + _
                "			WHEN cod_departamento = '11' THEN 'hn-ib'" + vbCr + _
                "			WHEN cod_departamento = '12' THEN 'hn-lp'" + vbCr + _
                "			WHEN cod_departamento = '13' THEN 'hn-le'" + vbCr + _
                "			WHEN cod_departamento = '14' THEN 'hn-oc'" + vbCr + _
                "			WHEN cod_departamento = '15' THEN 'hn-ol'" + vbCr + _
                "			WHEN cod_departamento = '16' THEN 'hn-sb'" + vbCr + _
                "			WHEN cod_departamento = '17' THEN 'hn-va'" + vbCr + _
                "			WHEN cod_departamento = '18' THEN 'hn-yo'" + vbCr + _
                "		END AS 'hc-key'," + vbCr + _
                "       ciclo_1, ciclo_2, ciclo_3, ciclo_4, ciclo_5, ciclo_6," + vbCr + _
                "		CASE" + vbCr + _
                "			WHEN ciclo_1 <> 0 AND ciclo_1 = (SELECT MAX (cicloX) FROM (VALUES  (ciclo_1), (ciclo_2), (ciclo_3), (ciclo_4), (ciclo_5), (ciclo_6)) AS UNIQUECOLUMN(cicloX)) THEN 1" + vbCr + _
                "			WHEN ciclo_2 <> 0 AND ciclo_2 = (SELECT MAX (cicloX) FROM (VALUES  (ciclo_1), (ciclo_2), (ciclo_3), (ciclo_4), (ciclo_5), (ciclo_6)) AS UNIQUECOLUMN(cicloX)) THEN 2" + vbCr + _
                "			WHEN ciclo_3 <> 0 AND ciclo_3 = (SELECT MAX (cicloX) FROM (VALUES  (ciclo_1), (ciclo_2), (ciclo_3), (ciclo_4), (ciclo_5), (ciclo_6)) AS UNIQUECOLUMN(cicloX)) THEN 3" + vbCr + _
                "			WHEN ciclo_4 <> 0 AND ciclo_4 = (SELECT MAX (cicloX) FROM (VALUES  (ciclo_1), (ciclo_2), (ciclo_3), (ciclo_4), (ciclo_5), (ciclo_6)) AS UNIQUECOLUMN(cicloX)) THEN 4" + vbCr + _
                "			WHEN ciclo_5 <> 0 AND ciclo_5 = (SELECT MAX (cicloX) FROM (VALUES  (ciclo_1), (ciclo_2), (ciclo_3), (ciclo_4), (ciclo_5), (ciclo_6)) AS UNIQUECOLUMN(cicloX)) THEN 5" + vbCr + _
                "			WHEN ciclo_6 <> 0 AND ciclo_6 = (SELECT MAX (cicloX) FROM (VALUES  (ciclo_1), (ciclo_2), (ciclo_3), (ciclo_4), (ciclo_5), (ciclo_6)) AS UNIQUECOLUMN(cicloX)) THEN 6" + vbCr + _
                "			ELSE 0" + vbCr + _
                "		END AS 'value'" + vbCr + _
                "	FROM (" + vbCr + _
                "		SELECT geo.cod_departamento," + vbCr + _
                "				COUNT(CASE WHEN corr.ciclo_elegibilidad = 1 THEN 1 ELSE NULL END) AS 'ciclo_1'," + vbCr + _
                "				COUNT(CASE WHEN corr.ciclo_elegibilidad = 2 THEN 1 ELSE NULL END) AS 'ciclo_2'," + vbCr + _
                "				COUNT(CASE WHEN corr.ciclo_elegibilidad = 3 THEN 1 ELSE NULL END) AS 'ciclo_3'," + vbCr + _
                "				COUNT(CASE WHEN corr.ciclo_elegibilidad = 4 THEN 1 ELSE NULL END) AS 'ciclo_4'," + vbCr + _
                "				COUNT(CASE WHEN corr.ciclo_elegibilidad = 5 THEN 1 ELSE NULL END) AS 'ciclo_5'," + vbCr + _
                "				COUNT(CASE WHEN corr.ciclo_elegibilidad = 6 THEN 1 ELSE NULL END) AS 'ciclo_6'" + vbCr + _
                "			FROM SIG_C.dbo.t_sig_ubicaciones_geograficas AS geo" + vbCr + _
                "				LEFT JOIN SIG_C.dbo.t_sig_detalle_planillas AS det ON det.cod_caserio = geo.cod_caserio AND det.cod_pago = " + pago + vbCr + _
                "				LEFT JOIN SIG_C.dbo.t_sig_corresp_pers_planillas AS corr ON corr.cod_hogar = det.cod_hogar AND corr.cod_pago = " + pago + vbCr + _
                "           --WHERE det.cod_pago = " + pago + " AND corr.cod_pago = " + pago + vbCr + _
                "			GROUP BY geo.cod_departamento" + vbCr + _
                "	) AS TAB" + vbCr + _
                "	ORDER BY cod_departamento"

            Try
                MyResult = conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)

        End Function

        Public Function Fnc_cantidad_ninos_ciclo_departamento(ByVal pago As String, ByVal dpto As String) As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr + _
                "SELECT cod_municipio AS 'codigo'," + vbCr + _
                "       ciclo_1, ciclo_2, ciclo_3, ciclo_4, ciclo_5, ciclo_6," + vbCr + _
                "		CASE" + vbCr + _
                "			WHEN ciclo_1 <> 0 AND ciclo_1 = (SELECT MAX (cicloX) FROM (VALUES  (ciclo_1), (ciclo_2), (ciclo_3), (ciclo_4), (ciclo_5), (ciclo_6)) AS UNIQUECOLUMN(cicloX)) THEN 1" + vbCr + _
                "			WHEN ciclo_2 <> 0 AND ciclo_2 = (SELECT MAX (cicloX) FROM (VALUES  (ciclo_1), (ciclo_2), (ciclo_3), (ciclo_4), (ciclo_5), (ciclo_6)) AS UNIQUECOLUMN(cicloX)) THEN 2" + vbCr + _
                "			WHEN ciclo_3 <> 0 AND ciclo_3 = (SELECT MAX (cicloX) FROM (VALUES  (ciclo_1), (ciclo_2), (ciclo_3), (ciclo_4), (ciclo_5), (ciclo_6)) AS UNIQUECOLUMN(cicloX)) THEN 3" + vbCr + _
                "			WHEN ciclo_4 <> 0 AND ciclo_4 = (SELECT MAX (cicloX) FROM (VALUES  (ciclo_1), (ciclo_2), (ciclo_3), (ciclo_4), (ciclo_5), (ciclo_6)) AS UNIQUECOLUMN(cicloX)) THEN 4" + vbCr + _
                "			WHEN ciclo_5 <> 0 AND ciclo_5 = (SELECT MAX (cicloX) FROM (VALUES  (ciclo_1), (ciclo_2), (ciclo_3), (ciclo_4), (ciclo_5), (ciclo_6)) AS UNIQUECOLUMN(cicloX)) THEN 5" + vbCr + _
                "			WHEN ciclo_6 <> 0 AND ciclo_6 = (SELECT MAX (cicloX) FROM (VALUES  (ciclo_1), (ciclo_2), (ciclo_3), (ciclo_4), (ciclo_5), (ciclo_6)) AS UNIQUECOLUMN(cicloX)) THEN 6" + vbCr + _
                "			ELSE 0" + vbCr + _
                "		END AS 'value'" + vbCr + _
                "	FROM (" + vbCr + _
                "		SELECT geo.cod_municipio," + vbCr + _
                "				COUNT(CASE WHEN corr.ciclo_elegibilidad = 1 THEN 1 ELSE NULL END) AS 'ciclo_1'," + vbCr + _
                "				COUNT(CASE WHEN corr.ciclo_elegibilidad = 2 THEN 1 ELSE NULL END) AS 'ciclo_2'," + vbCr + _
                "				COUNT(CASE WHEN corr.ciclo_elegibilidad = 3 THEN 1 ELSE NULL END) AS 'ciclo_3'," + vbCr + _
                "				COUNT(CASE WHEN corr.ciclo_elegibilidad = 4 THEN 1 ELSE NULL END) AS 'ciclo_4'," + vbCr + _
                "				COUNT(CASE WHEN corr.ciclo_elegibilidad = 5 THEN 1 ELSE NULL END) AS 'ciclo_5'," + vbCr + _
                "				COUNT(CASE WHEN corr.ciclo_elegibilidad = 6 THEN 1 ELSE NULL END) AS 'ciclo_6'" + vbCr + _
                "			FROM SIG_C.dbo.t_sig_ubicaciones_geograficas AS geo" + vbCr + _
                "				LEFT JOIN SIG_C.dbo.t_sig_detalle_planillas AS det ON det.cod_caserio = geo.cod_caserio AND det.cod_pago = " + pago + vbCr + _
                "				LEFT JOIN SIG_C.dbo.t_sig_corresp_pers_planillas AS corr ON corr.cod_hogar = det.cod_hogar AND corr.cod_pago = " + pago + vbCr + _
                "           WHERE geo.cod_departamento = '" + dpto + "'" + vbCr + _
                "			GROUP BY geo.cod_municipio" + vbCr + _
                "	) AS TAB" + vbCr + _
                "	ORDER BY cod_municipio"

            Try
                MyResult = conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)

        End Function

#End Region

#Region "Funciones listado de niños con corresponsabilidada en planilla"

        Public Function Fnc_listado_ninos_con_correponsabilidad_pago(ByVal pago As String, ByVal departamento As String, ByVal municipio As String, ByVal aldea As String) As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr + _
                "SELECT geo.cod_departamento, geo.desc_departamento, geo.cod_municipio, geo.desc_municipio," + vbCr + _
                "		geo.cod_aldea, geo.desc_aldea, geo.cod_caserio, geo.desc_caserio," + vbCr + _
                "		corr.cod_hogar, corr.identidad, corr.nombre_persona, corr.elegibilidad, corr.desc_nivel_elegibilidad, " + vbCr + _
                "		corr.desc_corresponsabilidad, corr.estado_corresponsabilidad, " + vbCr + _
                "		CASE WHEN det.cobro = 1 THEN 'pagado' ELSE 'no pagado' END AS 'pagado'" + vbCr + _
                "	FROM SIG_C.dbo.t_sig_corresp_pers_planillas AS corr" + vbCr + _
                "		INNER JOIN SIG_C.dbo.t_sig_detalle_planillas AS det ON det.cod_hogar = corr.cod_hogar" + vbCr + _
                "		INNER JOIN SIG_C.dbo.t_sig_ubicaciones_geograficas AS geo ON geo.cod_caserio = det.cod_caserio" + vbCr + _
                "   WHERE corr.cod_pago = " + pago + " AND det.cod_pago = " + pago + " AND det.programado = 1"

            If Not aldea Is Nothing Then
                sql += " AND geo.cod_aldea = '" + aldea + "'"
            ElseIf Not municipio Is Nothing Then
                sql += " AND geo.cod_municipio = '" + municipio + "'"
            ElseIf Not departamento Is Nothing Then
                sql += " AND geo.cod_departamento = '" + departamento + "'"
            End If

            sql += vbCr + " ORDER BY geo.cod_departamento, geo.cod_municipio, geo.cod_aldea, geo.cod_caserio, cod_hogar"

            Try
                MyResult = conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)

        End Function

#End Region

#Region "Funciones para los nucleos famiales de la muestra"

        Public Function Fnc_nucleo_familiar_muestra(ByVal pago As String, ByVal hogares As String) As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr + _
                "SELECT" + vbCr + _
                "/*INFORMACION DE LA PLANILLA*/ " + vbCr + _
                "		det.año_ficha," + vbCr + _
                "		det.censo_ficha," + vbCr + _
                "		det.numero_ficha," + vbCr + _
                "		det.cod_hogar," + vbCr + _
                "		det.fecha_emision_planilla," + vbCr + _
                "		det.umbral," + vbCr + _
                "		pla.nombre_fondo," + vbCr + _
                "		pla.nombre_pagador," + vbCr + _
                "		det.nombre_componente AS 'Corresponsabilidad_Det'," + vbCr + _
                "		'' AS 'Nombre_Bono_Acum'	,		--no se, ya estaba así" + vbCr + _
                "		det.proyeccion," + vbCr + _
                "		det.nombre_corresponsabilidad," + vbCr + _
                "		det.cod_titular," + vbCr + _
                "		CONVERT(NVARCHAR(15), det.pagina) + '-' + " + vbCr + _
                "		CASE WHEN det.linea = 10 THEN '10' ELSE '0' + CONVERT(NVARCHAR(1), det.linea) END AS 'referencia'," + vbCr + _
                " /*UBICACION GEOGRAFICA DE LOS HOGARES*/" + vbCr + _
                "		geo.cod_departamento," + vbCr + _
                "		geo.desc_departamento," + vbCr + _
                "		geo.cod_municipio," + vbCr + _
                "		geo.desc_municipio," + vbCr + _
                "		geo.cod_aldea," + vbCr + _
                "		geo.desc_aldea," + vbCr + _
                "		geo.cod_caserio," + vbCr + _
                "		geo.desc_caserio," + vbCr + _
                " /*INFORMACION DE LAS PERSONAS*/" + vbCr + _
                "		per.cod_persona," + vbCr + _
                "		--per.edad," + vbCr + _
                "		per.identidad," + vbCr + _
                "		per.nombre," + vbCr + _
                "/*INFORMACION DE LAS CORRESPONSABILIDAD DE EDUCACION*/" + vbCr + _
                "		CASE WHEN corr.elegibilidad = 'EDUCACION' THEN corr.cod_centro_sace_renpi ELSE NULL END AS 'Cod_CentroSace'," + vbCr + _
                "		CASE WHEN corr.elegibilidad = 'EDUCACION' THEN corr.nombre_centro_educativo_salud ELSE NULL END AS 'Centro_Educativo'," + vbCr + _
                "		-- dirección centro educativo" + vbCr + _
                "		CASE WHEN corr.elegibilidad = 'EDUCACION' THEN corr.nombre_grado ELSE NULL END AS 'Grado'," + vbCr + _
                "		CASE WHEN corr.elegibilidad = 'EDUCACION' THEN corr.año ELSE NULL END AS 'Año'," + vbCr + _
                " /*INFORMACION DE LAS CORRESPONSABILIDAD DE SALUD*/" + vbCr + _
                "		CASE WHEN corr.elegibilidad = 'SALUD' THEN corr.cod_centro_sace_renpi ELSE NULL END AS 'CodigoCentroDeSaludRENPI'," + vbCr + _
                "		CASE WHEN corr.elegibilidad = 'SALUD' THEN corr.nombre_centro_educativo_salud ELSE NULL END AS 'DescripcionCentroDeSaludRENPI'," + vbCr + _
                " /*MONTOS*/" + vbCr + _
                "		det.esquema AS 'Periodo_Esquema'," + vbCr + _
                "		CASE WHEN per.cod_persona = det.cod_persona THEN 1 ELSE 0 END AS 'titular'," + vbCr + _
                "		CASE WHEN per.cod_persona = det.cod_persona THEN det.monto_total ELSE 0 END AS 'Monto'" + vbCr + _
                "	FROM  SIG_C.dbo.t_sig_planillas AS pla" + vbCr + _
                "		INNER JOIN SIG_C.dbo.t_sig_detalle_planillas AS det " + vbCr + _
                "			ON det.cod_pago = pla.cod_pago AND det.cod_caserio = pla.cod_caserio" + vbCr + _
                "		INNER JOIN SIG_C.dbo.t_sig_ubicaciones_geograficas AS geo" + vbCr + _
                "			ON geo.cod_caserio = pla.cod_caserio" + vbCr + _
                "		LEFT JOIN (" + vbCr + _
                "			SELECT cod_hogar, cod_persona, identidad_titular AS 'identidad', nombre_titular AS 'nombre'" + vbCr + _
                "				FROM SIG_C.dbo.t_sig_detalle_planillas					" + vbCr + _
                "               WHERE cod_pago = " + pago + vbCr + _
                "			UNION" + vbCr + _
                "			SELECT cod_hogar, cod_persona, identidad, nombre_persona" + vbCr + _
                "				FROM SIG_C.dbo.t_sig_corresp_pers_planillas" + vbCr + _
                "               WHERE cod_pago = " + pago + vbCr + _
                "		) AS per" + vbCr + _
                "			ON per.cod_hogar = det.cod_hogar AND per.cod_persona = per.cod_persona" + vbCr + _
                "		LEFT JOIN SIG_C.dbo.t_sig_corresp_pers_planillas AS corr" + vbCr + _
                "			ON corr.cod_persona = per.cod_persona AND corr.cod_pago = " + pago + vbCr + _
                "   WHERE pla.cod_pago = " + pago + " AND det.cod_pago = " + pago + " AND det.cod_hogar IN (" + hogares + ")" + vbCr + _
                "   ORDER BY cod_hogar"

            Try
                MyResult = conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)

        End Function

#End Region

#Region "Funciones para base de lo pagado"
        Public Function Fnc_base_pagado(ByVal pago As String) As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr +
                "SELECT" + vbCr +
                "		det.bono," + vbCr +
                "		det.cod_hogar," + vbCr +
                "		YEAR(det.fecha_emision_planilla) AS 'año'," + vbCr +
                "		pla.nombre_fondo," + vbCr +
                "		pla.nombre_pagador," + vbCr +
                "		geo.cod_departamento + '-' + geo.desc_departamento AS depto," + vbCr +
                "		geo.cod_municipio + '-' + geo.desc_municipio AS muni," + vbCr +
                "		geo.cod_aldea + '-' + geo.desc_aldea AS aldea," + vbCr +
                "		det.esquema," + vbCr +
                "		CONVERT(DATE,det.fecha_cobro,101) AS 'fecha_pago'," + vbCr +
                "		det.nombre_titular," + vbCr +
                "		det.identidad_titular," + vbCr +
                "		det.pagina," + vbCr +
                "		det.linea," + vbCr +
                "		CONVERT(NVARCHAR(15), det.pagina) + '-' + CASE WHEN det.linea = 10 THEN '10' ELSE '0' + CONVERT(NVARCHAR(1), det.linea) END AS 'referencia'," + vbCr +
                "		base.*" + vbCr +
                "	FROM SIG_C.dbo.t_sig_planillas AS pla" + vbCr +
                "		INNER JOIN SIG_C.dbo.t_sig_detalle_planillas AS det" + vbCr +
                "			ON det.cod_pago = pla.cod_pago AND det.cod_caserio = pla.cod_caserio" + vbCr +
                "		INNER JOIN SIG_C.dbo.t_sig_ubicaciones_geograficas AS geo" + vbCr +
                "			ON geo.cod_caserio = det.cod_caserio" + vbCr +
                "		INNER JOIN SIG_C.dbo.t_sig_base_pagado AS base " + vbCr +
                "			ON base.cod_pago = det.cod_pago AND base.cod_hogar = det.cod_hogar" + vbCr +
                "   WHERE det.cod_pago = " + pago + " AND pla.cod_pago = " + pago + " AND base.cod_pago = " + pago + vbCr +
                "	ORDER BY det.esquema"

            Try
                MyResult = conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)

        End Function

#End Region

#Region "Funciones para hogares pagados por período de tiempo"


        Public Function fnc_obtener_cantidad_hogares_pagados(ByVal tipo As String, ByVal strCampos As String, ByVal planillas As String) As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = ""
            Dim agrupar As String = ""
            Dim comilla As Boolean
            Dim campos() As String = strCampos.Split(",")

            Select Case tipo

                Case "Anual"

                    sql = "" + vbCr +
                        "SELECT pla.año_pago"

                    agrupar = "pla.año_pago"

                    comilla = True

                Case "Semestral"

                    sql = "" + vbCr +
                        "SELECT pla.año_pago" + vbCr +
                        "		,CASE" + vbCr +
                        "			WHEN MONTH(det.fecha_cobro) BETWEEN 01 AND 06 THEN 'Enero a Junio'" + vbCr +
                        "		    WHEN MONTH(det.fecha_cobro) BETWEEN 07 AND 12 THEN 'Julio a Diciembre'" + vbCr +
                        "		END AS 'semestre'"

                    agrupar = "pla.año_pago," + vbCr +
                        "       fecha_cobro"
                    comilla = True

                Case "Acumulado Planillas"

                    sql = "" + vbCr +
                        "SELECT "

            End Select

            For i = 0 To campos.Length - 1

                If comilla Then
                    sql = sql + "," + vbCr
                    agrupar = agrupar + "," + vbCr
                    comilla = False
                End If


                Select Case campos(i)
                    Case "Área Geografica"

                        sql = sql + "       geo.desc_departamento," + vbCr +
                            "       geo.desc_municipio," + vbCr +
                            "       geo.desc_aldea," + vbCr +
                            "       geo.desc_caserio"

                        agrupar = agrupar + "       geo.desc_departamento," + vbCr +
                            "       geo.desc_municipio," + vbCr +
                            "       geo.desc_aldea," + vbCr +
                            "       geo.desc_caserio"


                    Case "Fondo"
                        sql = sql + "       pla.nombre_fondo"
                        agrupar = agrupar + "       pla.nombre_fondo"
                    Case "Pagador"
                        sql = sql + "       pla.nombre_pagador"
                        agrupar = agrupar + "       pla.nombre_pagador"
                    Case "Componente"
                        sql = sql + "       det.nombre_componente"
                        agrupar = agrupar + "       det.nombre_componente"
                End Select


                If i < campos.Length - 1 Then
                    comilla = True
                End If

            Next

            sql = sql + "," + vbCr +
                "       COUNT(DISTINCT det.cod_hogar) AS 'hogares_pagados'," + vbCr +
                "       SUM(det.monto_total) AS 'monto_pagado'" + vbCr +
                "   FROM SIG_C.dbo.t_sig_planillas AS pla" + vbCr +
                "       INNER JOIN SIG_C.dbo.t_sig_ubicaciones_geograficas AS geo ON geo.cod_caserio = pla.cod_caserio" + vbCr +
                "       INNER JOIN SIG_C.dbo.t_sig_detalle_planillas AS det" + vbCr +
                "           ON det.cod_caserio = pla.cod_caserio AND det.cod_pago = pla.cod_pago" + vbCr +
                "   WHERE det.cobro = 1"

            If planillas.Length > 0 Then
                sql = sql + " AND pla.cod_pago IN (" + planillas + ")" + vbCr +
                    "   GROUP BY " + agrupar
            Else
                sql = sql + vbCr +
                    "   GROUP BY " + agrupar
            End If


            Try
                MyResult = conexion.GetDataSet(sql)
                Return MyResult.Tables(0)
            Catch ex As Exception
                Return Nothing
            End Try

        End Function


#End Region

#Region "Funciones para el reporte de ECCAI"

        Public Function fnc_obtener_eccai(ByVal referencias As String) As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr +
                "SELECT	 geo.desc_departamento" + vbCr +
                "		,geo.desc_municipio" + vbCr +
                "		,geo.desc_aldea" + vbCr +
                "		,det.cod_titular" + vbCr +
                "		,det.proyeccion_corta" + vbCr +
                "		,det.detalle_bono" + vbCr +
                "		,CASE WHEN det.programado = 1 THEN 'Si' ELSE 'No' END AS 'programado'" + vbCr +
                "		,det.cod_hogar" + vbCr +
                "		,det.identidad_titular" + vbCr +
                "		,det.nombre_titular" + vbCr +
                "		--hog_rup_hogar" + vbCr +
                "		,CASE WHEN det.cobro = 1 THEN 'Si' ELSE 'No' END AS 'cobro'" + vbCr +
                "		,det.monto_total" + vbCr +
                "		--------------------------------------------------------------------------" + vbCr +
                "		,pla.num_meses" + vbCr +
                "		,EST.BÁSICO" + vbCr +
                "		,EST.SALUD" + vbCr +
                "		,EST.[1 Y 2 CICLO EDUCACIÓN]" + vbCr +
                "		,EST.[3 CICLO EDUCACIÓN]" + vbCr +
                "		,det.monto_total_neto" + vbCr +
                "		,det.deducciones" + vbCr +
                "		,det.monto_acumulado" + vbCr +
                "		,det.monto_total AS 'monto_total_final'" + vbCr +
                "		--------------------------------------------------------------------------" + vbCr +
                "" + vbCr +
                "		,ISNULL(corr.identidad,'') AS 'identidad_niño'" + vbCr +
                "		,corr.cod_persona" + vbCr +
                "		,corr.nombre_persona" + vbCr +
                "		,corr.ciclo_elegibilidad AS 'ciclo_eleg_niño'" + vbCr +
                "		,ISNULL(corr.sexo_persona,'') AS 'sexo_niño'" + vbCr +
                "" + vbCr +
                "		,CASE WHEN corr.estado_corresponsabilidad = 'Cumple' AND corr.cod_componente = 2 THEN 1 ELSE 0 END 'niños_cump_salud'" + vbCr +
                "		--------------------------------------------------------------------------" + vbCr +
                "		,CASE WHEN corr.estado_corresponsabilidad = 'Cumple' AND corr.cod_componente = 2 AND corr.ciclo_elegibilidad = 1 THEN 1 ELSE 0 END 'niños_cump_salud_c1'" + vbCr +
                "		,CASE WHEN corr.estado_corresponsabilidad = 'Cumple' AND corr.cod_componente = 2 AND corr.ciclo_elegibilidad = 2 THEN 1 ELSE 0 END 'niños_cump_salud_c2'" + vbCr +
                "" + vbCr +
                "		--------------------------------------------------------------------------" + vbCr +
                "" + vbCr +
                "		,CASE WHEN corr.estado_corresponsabilidad = 'Cumple' AND corr.cod_componente = 3 AND corr.nivel_corresp = 2 THEN 1 ELSE 0 END 'niños_cump_educ_1'" + vbCr +
                "		,CASE WHEN corr.estado_corresponsabilidad = 'Cumple' AND corr.cod_componente = 3 AND corr.nivel_corresp = 3 THEN 1 ELSE 0 END 'niños_cump_educ_2_3'" + vbCr +
                "		,CASE WHEN corr.estado_corresponsabilidad = 'Cumple' AND corr.cod_componente = 3 THEN 1 ELSE 0 END 'niños_cump_educ_tot'" + vbCr +
                "" + vbCr +
                "		,CASE WHEN corr.estado_corresponsabilidad = 'Apercibido' AND corr.cod_componente = 2 AND corr.cant_incumplimiento = 1 THEN 1 ELSE 0 END 'niños_aper_salud'" + vbCr +
                "		--------------------------------------------------------------------------" + vbCr +
                "		,CASE WHEN corr.estado_corresponsabilidad = 'Apercibido' AND corr.cod_componente = 2 AND corr.cant_incumplimiento = 1 AND corr.ciclo_elegibilidad = 1 THEN 1 ELSE 0 END 'niños_aper_salud_c1'" + vbCr +
                "		,CASE WHEN corr.estado_corresponsabilidad = 'Apercibido' AND corr.cod_componente = 2 AND corr.cant_incumplimiento = 1 AND corr.ciclo_elegibilidad = 2 THEN 1 ELSE 0 END 'niños_aper_salud_c2'" + vbCr +
                "" + vbCr +
                "		--------------------------------------------------------------------------" + vbCr +
                "		,CASE WHEN corr.estado_corresponsabilidad = 'Apercibido' AND corr.cod_componente = 3 AND corr.cant_incumplimiento = 1 AND corr.nivel_corresp = 2 THEN 1 ELSE 0 END 'niños_aper_educ_1'" + vbCr +
                "		,CASE WHEN corr.estado_corresponsabilidad = 'Apercibido' AND corr.cod_componente = 3 AND corr.cant_incumplimiento = 1 AND corr.nivel_corresp = 3 THEN 1 ELSE 0 END 'niños_aper_educ_2_3'" + vbCr +
                "		,CASE WHEN corr.estado_corresponsabilidad = 'Apercibido' AND corr.cod_componente = 3 AND corr.cant_incumplimiento = 1 THEN 1 ELSE 0 END 'niños_aper_educ_tot'" + vbCr +
                "" + vbCr +
                "		,CASE " + vbCr +
                "			WHEN " + vbCr +
                "				(corr.estado_corresponsabilidad = 'No Cumple' AND corr.desc_nivel_elegibilidad = 'SALUD') " + vbCr +
                "				OR	(corr.estado_corresponsabilidad = 'Apercibido' AND corr.cod_componente = 2 AND corr.cant_incumplimiento > 1)" + vbCr +
                "			THEN 1 " + vbCr +
                "			ELSE 0 " + vbCr +
                "		END 'niños_no_cumple_salud'" + vbCr +
                "		--------------------------------------------------------------------------" + vbCr +
                "		,CASE " + vbCr +
                "			WHEN " + vbCr +
                "				(corr.estado_corresponsabilidad = 'No Cumple' AND corr.desc_nivel_elegibilidad = 'SALUD') " + vbCr +
                "				OR	(corr.estado_corresponsabilidad = 'Apercibido' AND corr.cod_componente = 2 AND corr.cant_incumplimiento > 1)" + vbCr +
                "				AND	corr.ciclo_elegibilidad = 1" + vbCr +
                "			THEN 1 " + vbCr +
                "			ELSE 0 " + vbCr +
                "		END 'niños_no_cumple_salud_c1'" + vbCr +
                "		,CASE " + vbCr +
                "			WHEN " + vbCr +
                "				(corr.estado_corresponsabilidad = 'No Cumple' AND corr.desc_nivel_elegibilidad = 'SALUD') " + vbCr +
                "				OR	(corr.estado_corresponsabilidad = 'Apercibido' AND corr.cod_componente = 2 AND corr.cant_incumplimiento > 1)" + vbCr +
                "				AND	corr.ciclo_elegibilidad = 2" + vbCr +
                "			THEN 1 " + vbCr +
                "			ELSE 0 " + vbCr +
                "		END 'niños_no_cumple_salud_c2'" + vbCr +
                "		--------------------------------------------------------------------------" + vbCr +
                "		,CASE " + vbCr +
                "			WHEN " + vbCr +
                "				(corr.estado_corresponsabilidad = 'No Cumple' AND corr.desc_nivel_elegibilidad = '1 Y 2 CICLO EDUCACIÓN' AND corr.nivel_corresp = 2) " + vbCr +
                "				OR	(corr.estado_corresponsabilidad = 'Apercibido' AND corr.cod_componente = 3 AND corr.nivel_corresp = 2 AND corr.cant_incumplimiento > 1)" + vbCr +
                "			THEN 1 " + vbCr +
                "			ELSE 0 " + vbCr +
                "		END 'niños_no_cumple_educ_1'" + vbCr +
                "		,CASE " + vbCr +
                "			WHEN " + vbCr +
                "				(corr.estado_corresponsabilidad = 'No Cumple' AND corr.desc_nivel_elegibilidad = '3 CICLO EDUCACIÓN' AND corr.nivel_corresp = 3) " + vbCr +
                "				OR	(corr.estado_corresponsabilidad = 'Apercibido' AND corr.cod_componente = 3 AND corr.nivel_corresp = 3 AND corr.cant_incumplimiento > 1)" + vbCr +
                "			THEN 1 " + vbCr +
                "			ELSE 0 " + vbCr +
                "		END 'niños_no_cumple_educ_2_3'" + vbCr +
                "" + vbCr +
                "		,corr.nombre_corresponsabilidad" + vbCr +
                "		,CASE WHEN corr.cod_centro_educativo_salud = 0 THEN '' ELSE CONVERT(NVARCHAR(10),corr.cod_centro_educativo_salud) END AS 'cod_centro_educativo_salud'" + vbCr +
                "		,corr.nombre_centro_educativo_salud" + vbCr +
                "		,ISNULL(corr.nombre_grado,0) AS 'nombre_grado'" + vbCr +
                "		,corr.numero_visitas_centro_salud" + vbCr +
                "" + vbCr +
                "" + vbCr +
                "	FROM SIG_C.dbo.t_sig_planillas AS pla" + vbCr +
                "		INNER JOIN SIG_C.dbo.t_sig_ubicaciones_geograficas AS geo ON geo.cod_caserio = pla.cod_caserio" + vbCr +
                "		INNER JOIN SIG_C.dbo.t_sig_detalle_planillas AS det " + vbCr +
                "			ON det.cod_pago = pla.cod_pago AND det.cod_caserio = pla.cod_caserio AND det.esquema = pla.cod_esquema" + vbCr +
                "		INNER JOIN SIG_C.dbo.t_sig_corresp_pers_planillas AS corr " + vbCr +
                "			ON corr.cod_pago = det.cod_pago AND corr.cod_hogar = det.cod_hogar" + vbCr +
                "		INNER JOIN (" + vbCr +
                "			SELECT *" + vbCr +
                "				FROM (" + vbCr +
                "					SELECT cod_pago, cod_hogar, nivel_ciclo, monto_nivel_neto" + vbCr +
                "						FROM SIG_C.dbo.t_sig_estado_cuenta_participantes" + vbCr +
                "				) PVT" + vbCr +
                "				PIVOT (MAX(monto_nivel_neto) FOR nivel_ciclo IN ([BÁSICO],[SALUD],[1 Y 2 CICLO EDUCACIÓN],[3 CICLO EDUCACIÓN])) AS nivel_ciclo" + vbCr +
                "		) EST ON est.cod_pago = det.cod_pago AND est.cod_hogar = det.cod_hogar" + vbCr +
                "	WHERE det.referencia IN (" + vbCr +
                "       '" + referencias.Replace(",", "','") + "'" + vbCr +
                "	)" + vbCr +
                "	ORDER BY cod_departamento, cod_municipio, cod_aldea, cod_hogar, cod_persona, nombre_persona"

            Try
                MyResult = conexion.GetDataSet(sql)
                Return MyResult.Tables(0)
            Catch ex As Exception
                Return Nothing
            End Try


        End Function

        Public Function fnc_obtener_eccai(ByVal pago As String, ByVal hogares As String) As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr +
                "SELECT	 geo.desc_departamento" + vbCr +
                "		,geo.desc_municipio" + vbCr +
                "		,geo.desc_aldea" + vbCr +
                "		,det.cod_titular" + vbCr +
                "		,det.proyeccion_corta" + vbCr +
                "		,det.detalle_bono" + vbCr +
                "		,CASE WHEN det.programado = 1 THEN 'Si' ELSE 'No' END AS 'programado'" + vbCr +
                "		,det.cod_hogar" + vbCr +
                "		,det.identidad_titular" + vbCr +
                "		,det.nombre_titular" + vbCr +
                "		--hog_rup_hogar" + vbCr +
                "		,CASE WHEN det.cobro = 1 THEN 'Si' ELSE 'No' END AS 'cobro'" + vbCr +
                "		,det.monto_total" + vbCr +
                "		--------------------------------------------------------------------------" + vbCr +
                "		,pla.num_meses" + vbCr +
                "		,EST.BÁSICO" + vbCr +
                "		,EST.SALUD" + vbCr +
                "		,EST.[1 Y 2 CICLO EDUCACIÓN]" + vbCr +
                "		,EST.[3 CICLO EDUCACIÓN]" + vbCr +
                "		,det.monto_total_neto" + vbCr +
                "		,det.deducciones" + vbCr +
                "		,det.monto_acumulado" + vbCr +
                "		,det.monto_total AS 'monto_total_final'" + vbCr +
                "		--------------------------------------------------------------------------" + vbCr +
                "" + vbCr +
                "		,ISNULL(corr.identidad,'') AS 'identidad_niño'" + vbCr +
                "		,corr.cod_persona" + vbCr +
                "		,corr.nombre_persona" + vbCr +
                "		,corr.ciclo_elegibilidad AS 'ciclo_eleg_niño'" + vbCr +
                "		,ISNULL(corr.sexo_persona,'') AS 'sexo_niño'" + vbCr +
                "" + vbCr +
                "		,CASE WHEN corr.estado_corresponsabilidad = 'Cumple' AND corr.cod_componente = 2 THEN 1 ELSE 0 END 'niños_cump_salud'" + vbCr +
                "		--------------------------------------------------------------------------" + vbCr +
                "		,CASE WHEN corr.estado_corresponsabilidad = 'Cumple' AND corr.cod_componente = 2 AND corr.ciclo_elegibilidad = 1 THEN 1 ELSE 0 END 'niños_cump_salud_c1'" + vbCr +
                "		,CASE WHEN corr.estado_corresponsabilidad = 'Cumple' AND corr.cod_componente = 2 AND corr.ciclo_elegibilidad = 2 THEN 1 ELSE 0 END 'niños_cump_salud_c2'" + vbCr +
                "" + vbCr +
                "		--------------------------------------------------------------------------" + vbCr +
                "" + vbCr +
                "		,CASE WHEN corr.estado_corresponsabilidad = 'Cumple' AND corr.cod_componente = 3 AND corr.nivel_corresp = 2 THEN 1 ELSE 0 END 'niños_cump_educ_1'" + vbCr +
                "		,CASE WHEN corr.estado_corresponsabilidad = 'Cumple' AND corr.cod_componente = 3 AND corr.nivel_corresp = 3 THEN 1 ELSE 0 END 'niños_cump_educ_2_3'" + vbCr +
                "		,CASE WHEN corr.estado_corresponsabilidad = 'Cumple' AND corr.cod_componente = 3 THEN 1 ELSE 0 END 'niños_cump_educ_tot'" + vbCr +
                "" + vbCr +
                "		,CASE WHEN corr.estado_corresponsabilidad = 'Apercibido' AND corr.cod_componente = 2 AND corr.cant_incumplimiento = 1 THEN 1 ELSE 0 END 'niños_aper_salud'" + vbCr +
                "		--------------------------------------------------------------------------" + vbCr +
                "		,CASE WHEN corr.estado_corresponsabilidad = 'Apercibido' AND corr.cod_componente = 2 AND corr.cant_incumplimiento = 1 AND corr.ciclo_elegibilidad = 1 THEN 1 ELSE 0 END 'niños_aper_salud_c1'" + vbCr +
                "		,CASE WHEN corr.estado_corresponsabilidad = 'Apercibido' AND corr.cod_componente = 2 AND corr.cant_incumplimiento = 1 AND corr.ciclo_elegibilidad = 2 THEN 1 ELSE 0 END 'niños_aper_salud_c2'" + vbCr +
                "" + vbCr +
                "		--------------------------------------------------------------------------" + vbCr +
                "		,CASE WHEN corr.estado_corresponsabilidad = 'Apercibido' AND corr.cod_componente = 3 AND corr.cant_incumplimiento = 1 AND corr.nivel_corresp = 2 THEN 1 ELSE 0 END 'niños_aper_educ_1'" + vbCr +
                "		,CASE WHEN corr.estado_corresponsabilidad = 'Apercibido' AND corr.cod_componente = 3 AND corr.cant_incumplimiento = 1 AND corr.nivel_corresp = 3 THEN 1 ELSE 0 END 'niños_aper_educ_2_3'" + vbCr +
                "		,CASE WHEN corr.estado_corresponsabilidad = 'Apercibido' AND corr.cod_componente = 3 AND corr.cant_incumplimiento = 1 THEN 1 ELSE 0 END 'niños_aper_educ_tot'" + vbCr +
                "" + vbCr +
                "		,CASE " + vbCr +
                "			WHEN " + vbCr +
                "				(corr.estado_corresponsabilidad = 'No Cumple' AND corr.desc_nivel_elegibilidad = 'SALUD') " + vbCr +
                "				OR	(corr.estado_corresponsabilidad = 'Apercibido' AND corr.cod_componente = 2 AND corr.cant_incumplimiento > 1)" + vbCr +
                "			THEN 1 " + vbCr +
                "			ELSE 0 " + vbCr +
                "		END 'niños_no_cumple_salud'" + vbCr +
                "		--------------------------------------------------------------------------" + vbCr +
                "		,CASE " + vbCr +
                "			WHEN " + vbCr +
                "				(corr.estado_corresponsabilidad = 'No Cumple' AND corr.desc_nivel_elegibilidad = 'SALUD') " + vbCr +
                "				OR	(corr.estado_corresponsabilidad = 'Apercibido' AND corr.cod_componente = 2 AND corr.cant_incumplimiento > 1)" + vbCr +
                "				AND	corr.ciclo_elegibilidad = 1" + vbCr +
                "			THEN 1 " + vbCr +
                "			ELSE 0 " + vbCr +
                "		END 'niños_no_cumple_salud_c1'" + vbCr +
                "		,CASE " + vbCr +
                "			WHEN " + vbCr +
                "				(corr.estado_corresponsabilidad = 'No Cumple' AND corr.desc_nivel_elegibilidad = 'SALUD') " + vbCr +
                "				OR	(corr.estado_corresponsabilidad = 'Apercibido' AND corr.cod_componente = 2 AND corr.cant_incumplimiento > 1)" + vbCr +
                "				AND	corr.ciclo_elegibilidad = 2" + vbCr +
                "			THEN 1 " + vbCr +
                "			ELSE 0 " + vbCr +
                "		END 'niños_no_cumple_salud_c2'" + vbCr +
                "		--------------------------------------------------------------------------" + vbCr +
                "		,CASE " + vbCr +
                "			WHEN " + vbCr +
                "				(corr.estado_corresponsabilidad = 'No Cumple' AND corr.desc_nivel_elegibilidad = '1 Y 2 CICLO EDUCACIÓN' AND corr.nivel_corresp = 2) " + vbCr +
                "				OR	(corr.estado_corresponsabilidad = 'Apercibido' AND corr.cod_componente = 3 AND corr.nivel_corresp = 2 AND corr.cant_incumplimiento > 1)" + vbCr +
                "			THEN 1 " + vbCr +
                "			ELSE 0 " + vbCr +
                "		END 'niños_no_cumple_educ_1'" + vbCr +
                "		,CASE " + vbCr +
                "			WHEN " + vbCr +
                "				(corr.estado_corresponsabilidad = 'No Cumple' AND corr.desc_nivel_elegibilidad = '3 CICLO EDUCACIÓN' AND corr.nivel_corresp = 3) " + vbCr +
                "				OR	(corr.estado_corresponsabilidad = 'Apercibido' AND corr.cod_componente = 3 AND corr.nivel_corresp = 3 AND corr.cant_incumplimiento > 1)" + vbCr +
                "			THEN 1 " + vbCr +
                "			ELSE 0 " + vbCr +
                "		END 'niños_no_cumple_educ_2_3'" + vbCr +
                "" + vbCr +
                "		,corr.nombre_corresponsabilidad" + vbCr +
                "		,CASE WHEN corr.cod_centro_educativo_salud = 0 THEN '' ELSE CONVERT(NVARCHAR(10),corr.cod_centro_educativo_salud) END AS 'cod_centro_educativo_salud'" + vbCr +
                "		,corr.nombre_centro_educativo_salud" + vbCr +
                "		,ISNULL(corr.nombre_grado,0) AS 'nombre_grado'" + vbCr +
                "		,corr.numero_visitas_centro_salud" + vbCr +
                "" + vbCr +
                "" + vbCr +
                "	FROM SIG_C.dbo.t_sig_planillas AS pla" + vbCr +
                "		INNER JOIN SIG_C.dbo.t_sig_ubicaciones_geograficas AS geo ON geo.cod_caserio = pla.cod_caserio" + vbCr +
                "		INNER JOIN SIG_C.dbo.t_sig_detalle_planillas AS det " + vbCr +
                "			ON det.cod_pago = pla.cod_pago AND det.cod_caserio = pla.cod_caserio AND det.esquema = pla.cod_esquema" + vbCr +
                "		INNER JOIN SIG_C.dbo.t_sig_corresp_pers_planillas AS corr " + vbCr +
                "			ON corr.cod_pago = det.cod_pago AND corr.cod_hogar = det.cod_hogar" + vbCr +
                "		INNER JOIN (" + vbCr +
                "			SELECT *" + vbCr +
                "				FROM (" + vbCr +
                "					SELECT cod_pago, cod_hogar, nivel_ciclo, monto_nivel_neto" + vbCr +
                "						FROM SIG_C.dbo.t_sig_estado_cuenta_participantes" + vbCr +
                "				) PVT" + vbCr +
                "				PIVOT (MAX(monto_nivel_neto) FOR nivel_ciclo IN ([BÁSICO],[SALUD],[1 Y 2 CICLO EDUCACIÓN],[3 CICLO EDUCACIÓN])) AS nivel_ciclo" + vbCr +
                "		) EST ON est.cod_pago = det.cod_pago AND est.cod_hogar = det.cod_hogar" + vbCr +
                "	WHERE pla.cod_pago = " + pago + " AND det.programado = 1 AND det.cod_hogar IN (" + vbCr +
                "       " + hogares + vbCr +
                "   )" + vbCr +
                "	ORDER BY cod_departamento, cod_municipio, cod_aldea, cod_hogar, cod_persona, nombre_persona"

            Try
                MyResult = conexion.GetDataSet(sql)
                Return MyResult.Tables(0)
            Catch ex As Exception
                Return Nothing
            End Try

        End Function

#End Region

    End Class



End Namespace
