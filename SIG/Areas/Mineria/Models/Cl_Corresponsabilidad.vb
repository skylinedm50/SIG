Namespace SIG.Areas.Mineria.Models

    Public Class Cl_Corresponsabilidad

        Dim conexion As SIG.Areas.Mineria.Models.Cl_Conexion = New SIG.Areas.Mineria.Models.Cl_Conexion

#Region "Porcentaje de niños que incumplieron corresponsabilidad por hogar"

        Public Function Fnc_obtener_porcentaje_ninos_incumplieron_comp(ByVal pago As String, ByVal hogares As String, ByVal departamento As String, ByVal municipio As String, ByVal aldea As String) As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr + _
                "SELECT geo.cod_departamento, geo.desc_departamento, geo.cod_municipio, geo.desc_municipio," + vbCr + _
                "		geo.cod_aldea, geo.desc_aldea, geo.cod_caserio, geo.desc_caserio," + vbCr + _
                "		det.cod_hogar, det.identidad_titular, det.nombre_titular," + vbCr + _
                "		COUNT(DISTINCT corr.cod_persona) AS 'total_niños'," + vbCr + _
                "-- EDUCACIÓN" + vbCr + _
                "		COUNT(CASE WHEN corr.elegibilidad = 'EDUCACION' THEN 1 ELSE NULL END) AS 'total_niños_educacion'," + vbCr + _
                "		COUNT(CASE WHEN corr.elegibilidad = 'EDUCACION' AND corr.estado_corresponsabilidad = 'Cumple' THEN 1 ELSE NULL END) AS 'cumplen_educacion'," + vbCr + _
                "		COUNT(CASE WHEN corr.elegibilidad = 'EDUCACION' AND corr.estado_corresponsabilidad = 'Apercibido' THEN 1 ELSE NULL END) AS 'apercibidos_educacion'," + vbCr + _
                "		COUNT(CASE WHEN corr.elegibilidad = 'EDUCACION' AND corr.estado_corresponsabilidad = 'No Cumple' THEN 1 ELSE NULL END) AS 'no_cumple_educacion'," + vbCr + _
                "		CASE" + vbCr + _
                "			WHEN COUNT(CASE WHEN corr.elegibilidad = 'EDUCACION' THEN 1 ELSE NULL END) > 0 THEN" + vbCr + _
                "				CONVERT(NVARCHAR(3),(COUNT(CASE WHEN corr.elegibilidad = 'EDUCACION' AND corr.estado_corresponsabilidad = 'No Cumple' THEN 1 ELSE NULL END) * 100) / COUNT(CASE WHEN corr.elegibilidad = 'EDUCACION' THEN 1 ELSE NULL END)) + '%'" + vbCr + _
                "			ELSE '0%'" + vbCr + _
                "		END AS 'porcentaje_educacion'," + vbCr + _
                "-- SALUD" + vbCr + _
                "		COUNT(CASE WHEN corr.elegibilidad = 'SALUD' THEN 1 ELSE NULL END) AS 'total_niños_salud'," + vbCr + _
                "		COUNT(CASE WHEN corr.elegibilidad = 'SALUD' AND corr.estado_corresponsabilidad = 'Cumple' THEN 1 ELSE NULL END) AS 'cumplen_salud'," + vbCr + _
                "		COUNT(CASE WHEN corr.elegibilidad = 'SALUD' AND corr.estado_corresponsabilidad = 'Apercibido' THEN 1 ELSE NULL END) AS 'apercibidos_salud'," + vbCr + _
                "		COUNT(CASE WHEN corr.elegibilidad = 'SALUD' AND corr.estado_corresponsabilidad = 'No Cumple' THEN 1 ELSE NULL END) AS 'no_cumple_salud'," + vbCr + _
                "		CASE" + vbCr + _
                "			WHEN COUNT(CASE WHEN corr.elegibilidad = 'SALUD' THEN 1 ELSE NULL END) > 0 THEN" + vbCr + _
                "				CONVERT(NVARCHAR(3),(COUNT(CASE WHEN corr.elegibilidad = 'SALUD' AND corr.estado_corresponsabilidad = 'No Cumple' THEN 1 ELSE NULL END) * 100) / COUNT(CASE WHEN corr.elegibilidad = 'SALUD' THEN 1 ELSE NULL END)) + '%'" + vbCr + _
                "			ELSE '0%'" + vbCr + _
                "		END AS 'porcentaje_salud'" + vbCr + _
                "	FROM SIG_C.dbo.t_sig_detalle_planillas AS det" + vbCr + _
                "		INNER JOIN SIG_C.dbo.t_sig_corresp_pers_planillas AS corr ON corr.cod_hogar = det.cod_hogar" + vbCr + _
                "		INNER JOIN SIG_C.dbo.t_sig_ubicaciones_geograficas AS geo ON geo.cod_caserio = det.cod_caserio" + vbCr + _
                "   WHERE det.cod_pago = " + pago + " And corr.cod_pago = " + pago

            Select Case hogares
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

            sql += vbCr + _
                "	GROUP BY geo.cod_departamento, 	geo.desc_departamento, geo.cod_municipio, 	geo.desc_municipio, " + vbCr + _
                "		geo.cod_aldea, geo.desc_aldea, geo.cod_caserio, geo.desc_caserio," + vbCr + _
                "		det.cod_hogar, det.identidad_titular, det.nombre_titular" + vbCr + _
                "	ORDER BY geo.cod_departamento, geo.cod_municipio, geo.cod_aldea,  geo.cod_caserio, cod_hogar"

            Try
                MyResult = conexion.GetDataSet(sql)
                Return MyResult.Tables(0)
            Catch ex As Exception
                Return Nothing
            End Try

        End Function

#End Region

#Region "Funciones arrastre, altas y bajas de Educación"

        Public Function Fnc_obtener_arrastre_altas_bajas_educacion(ByVal planilla1 As String, ByVal planilla2 As String) As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr + _
                "SELECT geo.cod_departamento, geo.desc_departamento, geo.cod_municipio, geo.desc_municipio," + vbCr + _
                "		geo.cod_aldea, geo.desc_aldea, geo.cod_caserio, geo.desc_caserio," + vbCr + _
                "		COUNT(planilla_1.cod_persona) AS 'total_planilla_1'," + vbCr + _
                "		COUNT(planilla_2.cod_persona) AS 'total_planilla_2'," + vbCr + _
                "		COUNT(" + vbCr + _
                "			CASE WHEN planilla_1.cod_persona IS NOT NULL AND planilla_2.cod_persona IS NOT NULL THEN 1 ELSE NULL END" + vbCr + _
                "		) AS 'arrastre'," + vbCr + _
                "		COUNT(" + vbCr + _
                "			CASE WHEN planilla_1.cod_persona IS NOT NULL AND planilla_2.cod_persona IS NULL THEN 1 ELSE NULL END" + vbCr + _
                "		) AS 'bajas'," + vbCr + _
                "		COUNT(" + vbCr + _
                "			CASE WHEN planilla_1.cod_persona IS NULL AND planilla_2.cod_persona IS NOT NULL THEN 1 ELSE NULL END" + vbCr + _
                "		) AS 'altas'" + vbCr + _
                "	FROM SIG_C.dbo.t_sig_ubicaciones_geograficas AS geo" + vbCr + _
                "       INNER JOIN SIG_C.dbo.t_sig_hogares AS hog ON hog.cod_caserio = geo.cod_caserio" + vbCr + _
                "       INNER JOIN SIG_C.dbo.t_sig_personas AS per ON per.cod_hogar = hog.cod_hogar" + vbCr + _
                "		LEFT JOIN (" + vbCr + _
                "			SELECT corr.cod_persona" + vbCr + _
                "				FROM SIG_C.dbo.t_sig_corresp_pers_planillas AS corr" + vbCr + _
                "					INNER JOIN SIG_C.dbo.t_sig_detalle_planillas AS det " + vbCr + _
                "						ON corr.cod_pago = det.cod_pago AND det.cod_hogar = corr.cod_hogar" + vbCr + _
                "				WHERE corr.cod_pago = " + planilla1 + " AND corr.elegibilidad = 'EDUCACION' AND det.programado = 1 AND corr.estado_corresponsabilidad <> 'No Cumple'" + vbCr + _
                "		) AS planilla_1 ON planilla_1.cod_persona = per.cod_persona" + vbCr + _
                "		LEFT JOIN (" + vbCr + _
                "			SELECT corr.cod_persona" + vbCr + _
                "				FROM SIG_C.dbo.t_sig_corresp_pers_planillas AS corr" + vbCr + _
                "					INNER JOIN SIG_C.dbo.t_sig_detalle_planillas AS det " + vbCr + _
                "						ON corr.cod_pago = det.cod_pago AND det.cod_hogar = corr.cod_hogar" + vbCr + _
                "				WHERE corr.cod_pago = " + planilla2 + " AND corr.elegibilidad = 'EDUCACION' AND det.programado = 1 AND corr.estado_corresponsabilidad <> 'No Cumple'" + vbCr + _
                "		) planilla_2 ON planilla_2.cod_persona = per.cod_persona" + vbCr + _
                "	GROUP BY geo.cod_departamento, geo.desc_departamento, geo.cod_municipio, geo.desc_municipio," + vbCr + _
                "		geo.cod_aldea, geo.desc_aldea, geo.cod_caserio, geo.desc_caserio" + vbCr + _
                "ORDER BY geo.cod_departamento, geo.cod_municipio, geo.cod_aldea, geo.cod_caserio"

            Try
                MyResult = conexion.GetDataSet(sql)
                Return MyResult.Tables(0)
            Catch ex As Exception
                Return Nothing
            End Try

        End Function

        Public Function Fnc_obtener_diferencia_entre_altas_bajas_educacion(ByVal planilla1 As String, ByVal planilla2 As String) As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr + _
                "SELECT /*EDU.cod_departamento, EDU.desc_departamento, EDU.diferencia_edu, SAL.diferencia_sal*/" + vbCr + _
                "		CASE" + vbCr + _
                "			WHEN EDU.cod_departamento = '01' THEN 'hn-at'" + vbCr + _
                "			WHEN EDU.cod_departamento = '02' THEN 'hn-cl'" + vbCr + _
                "			WHEN EDU.cod_departamento = '03' THEN 'hn-cm'" + vbCr + _
                "			WHEN EDU.cod_departamento = '04' THEN 'hn-cp'" + vbCr + _
                "			WHEN EDU.cod_departamento = '05' THEN 'hn-cr'" + vbCr + _
                "			WHEN EDU.cod_departamento = '06' THEN 'hn-ch'" + vbCr + _
                "			WHEN EDU.cod_departamento = '07' THEN 'hn-ep'" + vbCr + _
                "			WHEN EDU.cod_departamento = '08' THEN 'hn-fm'" + vbCr + _
                "			WHEN EDU.cod_departamento = '09' THEN 'hn-gd'" + vbCr + _
                "			WHEN EDU.cod_departamento = '10' THEN 'hn-in'" + vbCr + _
                "			WHEN EDU.cod_departamento = '11' THEN 'hn-ib'" + vbCr + _
                "			WHEN EDU.cod_departamento = '12' THEN 'hn-lp'" + vbCr + _
                "			WHEN EDU.cod_departamento = '13' THEN 'hn-le'" + vbCr + _
                "			WHEN EDU.cod_departamento = '14' THEN 'hn-oc'" + vbCr + _
                "			WHEN EDU.cod_departamento = '15' THEN 'hn-ol'" + vbCr + _
                "			WHEN EDU.cod_departamento = '16' THEN 'hn-sb'" + vbCr + _
                "			WHEN EDU.cod_departamento = '17' THEN 'hn-va'" + vbCr + _
                "			WHEN EDU.cod_departamento = '18' THEN 'hn-yo'" + vbCr + _
                "		END AS 'hc-key'," + vbCr + _
                "		EDU.diferencia_edu AS 'value'" + vbCr + _
                "	FROM (" + vbCr + _
                "		SELECT geo.cod_departamento, /*geo.desc_departamento,*/" + vbCr + _
                "				COUNT(" + vbCr + _
                "					CASE WHEN planilla_1.cod_persona IS NULL AND planilla_2.cod_persona IS NOT NULL THEN 1 ELSE NULL END" + vbCr + _
                "				) - " + vbCr + _
                "				COUNT(" + vbCr + _
                "					CASE WHEN planilla_1.cod_persona IS NOT NULL AND planilla_2.cod_persona IS NULL THEN 1 ELSE NULL END" + vbCr + _
                "				) AS 'diferencia_edu'" + vbCr + _
                "			FROM SIG_C.dbo.t_sig_ubicaciones_geograficas AS geo" + vbCr + _
                "               INNER JOIN SIG_C.dbo.t_sig_hogares AS hog ON hog.cod_caserio = geo.cod_caserio" + vbCr + _
                "               INNER JOIN SIG_C.dbo.t_sig_personas AS per ON per.cod_hogar = hog.cod_hogar" + vbCr + _
                "				LEFT JOIN (" + vbCr + _
                "					SELECT corr.cod_persona" + vbCr + _
                "						FROM SIG_C.dbo.t_sig_corresp_pers_planillas AS corr" + vbCr + _
                "							INNER JOIN SIG_C.dbo.t_sig_detalle_planillas AS det " + vbCr + _
                "								ON corr.cod_pago = det.cod_pago AND det.cod_hogar = corr.cod_hogar" + vbCr + _
                "						WHERE corr.cod_pago = " + planilla1 + " AND corr.elegibilidad = 'EDUCACION' AND det.programado = 1 AND corr.estado_corresponsabilidad <> 'No Cumple'" + vbCr + _
                "				) AS planilla_1 ON planilla_1.cod_persona = per.cod_persona" + vbCr + _
                "				LEFT JOIN (" + vbCr + _
                "					SELECT corr.cod_persona" + vbCr + _
                "						FROM SIG_C.dbo.t_sig_corresp_pers_planillas AS corr" + vbCr + _
                "							INNER JOIN SIG_C.dbo.t_sig_detalle_planillas AS det " + vbCr + _
                "								ON corr.cod_pago = det.cod_pago AND det.cod_hogar = corr.cod_hogar" + vbCr + _
                "						WHERE corr.cod_pago = " + planilla2 + " AND corr.elegibilidad = 'EDUCACION' AND det.programado = 1 AND corr.estado_corresponsabilidad <> 'No Cumple'" + vbCr + _
                "				) planilla_2 ON planilla_2.cod_persona = per.cod_persona" + vbCr + _
                "			GROUP BY geo.cod_departamento, geo.desc_departamento" + vbCr + _
                "	) EDU" + vbCr + _
                "   ORDER BY EDU.cod_departamento"

            Try
                MyResult = conexion.GetDataSet(sql)
                Return MyResult.Tables(0)
            Catch ex As Exception
                Return Nothing
            End Try

        End Function

        Public Function Fnc_obtener_diferencia_entre_altas_bajas_educacion_departamento(ByVal planilla1 As String, ByVal planilla2 As String, ByVal dpto As String) As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr + _
                "SELECT geo.cod_municipio AS 'codigo'," + vbCr + _
                "	    COUNT(" + vbCr + _
                "			CASE WHEN planilla_1.cod_persona IS NULL AND planilla_2.cod_persona IS NOT NULL THEN 1 ELSE NULL END" + vbCr + _
                "		) - " + vbCr + _
                "		COUNT(" + vbCr + _
                "			CASE WHEN planilla_1.cod_persona IS NOT NULL AND planilla_2.cod_persona IS NULL THEN 1 ELSE NULL END" + vbCr + _
                "		) AS 'value'" + vbCr + _
                "	FROM SIG_C.dbo.t_sig_ubicaciones_geograficas AS geo" + vbCr + _
                "       INNER JOIN SIG_C.dbo.t_sig_hogares AS hog ON hog.cod_caserio = geo.cod_caserio" + vbCr + _
                "       INNER JOIN SIG_C.dbo.t_sig_personas AS per ON per.cod_hogar = hog.cod_hogar" + vbCr + _
                "		LEFT JOIN (" + vbCr + _
                "			SELECT corr.cod_persona" + vbCr + _
                "				FROM SIG_C.dbo.t_sig_corresp_pers_planillas AS corr" + vbCr + _
                "					INNER JOIN SIG_C.dbo.t_sig_detalle_planillas AS det " + vbCr + _
                "						ON corr.cod_pago = det.cod_pago AND det.cod_hogar = corr.cod_hogar" + vbCr + _
                "				WHERE corr.cod_pago = " + planilla1 + " AND corr.elegibilidad = 'EDUCACION' AND det.programado = 1 AND corr.estado_corresponsabilidad <> 'No Cumple'" + vbCr + _
                "		) AS planilla_1 ON planilla_1.cod_persona = per.cod_persona" + vbCr + _
                "		LEFT JOIN (" + vbCr + _
                "			SELECT corr.cod_persona" + vbCr + _
                "				FROM SIG_C.dbo.t_sig_corresp_pers_planillas AS corr" + vbCr + _
                "					INNER JOIN SIG_C.dbo.t_sig_detalle_planillas AS det " + vbCr + _
                "						ON corr.cod_pago = det.cod_pago AND det.cod_hogar = corr.cod_hogar" + vbCr + _
                "				WHERE corr.cod_pago = " + planilla2 + " AND corr.elegibilidad = 'EDUCACION' AND det.programado = 1 AND corr.estado_corresponsabilidad <> 'No Cumple'" + vbCr + _
                "		) planilla_2 ON planilla_2.cod_persona = per.cod_persona" + vbCr + _
                "   WHERE geo.cod_departamento = " + dpto + vbCr + _
                "	GROUP BY geo.cod_municipio" + vbCr + _
                "   ORDER BY geo.cod_municipio"

            Try
                MyResult = conexion.GetDataSet(sql)
                Return MyResult.Tables(0)
            Catch ex As Exception
                Return Nothing
            End Try

        End Function

#End Region

#Region "Funciones arrastre, altas y bajas de Salud"

        Public Function Fnc_obtener_arrastre_altas_bajas_salud(ByVal planilla1 As String, ByVal planilla2 As String) As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr + _
                "SELECT geo.cod_departamento, geo.desc_departamento, geo.cod_municipio, geo.desc_municipio," + vbCr + _
                "		geo.cod_aldea, geo.desc_aldea, geo.cod_caserio, geo.desc_caserio," + vbCr + _
                "		COUNT(planilla_1.cod_persona) AS 'total_planilla_1'," + vbCr + _
                "		COUNT(planilla_2.cod_persona) AS 'total_planilla_2'," + vbCr + _
                "		COUNT(" + vbCr + _
                "			CASE WHEN planilla_1.cod_persona IS NOT NULL AND planilla_2.cod_persona IS NOT NULL THEN 1 ELSE NULL END" + vbCr + _
                "		) AS 'arrastre'," + vbCr + _
                "		COUNT(" + vbCr + _
                "			CASE WHEN planilla_1.cod_persona IS NOT NULL AND planilla_2.cod_persona IS NULL THEN 1 ELSE NULL END" + vbCr + _
                "		) AS 'bajas'," + vbCr + _
                "		COUNT(" + vbCr + _
                "			CASE WHEN planilla_1.cod_persona IS NULL AND planilla_2.cod_persona IS NOT NULL THEN 1 ELSE NULL END" + vbCr + _
                "		) AS 'altas'" + vbCr + _
                "	FROM SIG_C.dbo.t_sig_ubicaciones_geograficas AS geo" + vbCr + _
                "       INNER JOIN SIG_C.dbo.t_sig_hogares AS hog ON hog.cod_caserio = geo.cod_caserio" + vbCr + _
                "       INNER JOIN SIG_C.dbo.t_sig_personas AS per ON per.cod_hogar = hog.cod_hogar" + vbCr + _
                "		LEFT JOIN (" + vbCr + _
                "			SELECT corr.cod_persona" + vbCr + _
                "				FROM SIG_C.dbo.t_sig_corresp_pers_planillas AS corr" + vbCr + _
                "					INNER JOIN SIG_C.dbo.t_sig_detalle_planillas AS det " + vbCr + _
                "						ON corr.cod_pago = det.cod_pago AND det.cod_hogar = corr.cod_hogar" + vbCr + _
                "				WHERE corr.cod_pago = " + planilla1 + " AND corr.elegibilidad = 'SALUD' AND det.programado = 1 AND corr.estado_corresponsabilidad <> 'No Cumple'" + vbCr + _
                "		) AS planilla_1 ON planilla_1.cod_persona = per.cod_persona" + vbCr + _
                "		LEFT JOIN (" + vbCr + _
                "			SELECT corr.cod_persona" + vbCr + _
                "				FROM SIG_C.dbo.t_sig_corresp_pers_planillas AS corr" + vbCr + _
                "					INNER JOIN SIG_C.dbo.t_sig_detalle_planillas AS det " + vbCr + _
                "						ON corr.cod_pago = det.cod_pago AND det.cod_hogar = corr.cod_hogar" + vbCr + _
                "				WHERE corr.cod_pago = " + planilla2 + " AND corr.elegibilidad = 'SALUD' AND det.programado = 1 AND corr.estado_corresponsabilidad <> 'No Cumple'" + vbCr + _
                "		) planilla_2 ON planilla_2.cod_persona = per.cod_persona" + vbCr + _
                "	GROUP BY geo.cod_departamento, geo.desc_departamento, geo.cod_municipio, geo.desc_municipio," + vbCr + _
                "		geo.cod_aldea, geo.desc_aldea, geo.cod_caserio, geo.desc_caserio" + vbCr + _
                "   ORDER BY geo.cod_departamento, geo.cod_municipio, geo.cod_aldea, geo.cod_caserio"

            Try
                MyResult = conexion.GetDataSet(sql)
                Return MyResult.Tables(0)
            Catch ex As Exception
                Return Nothing
            End Try

        End Function

        Public Function Fnc_obtener_diferencia_entre_altas_bajas_salud(ByVal planilla1 As String, ByVal planilla2 As String) As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr + _
                "SELECT /*EDU.cod_departamento, EDU.desc_departamento, EDU.diferencia_edu, SAL.diferencia_sal*/" + vbCr + _
                "		CASE" + vbCr + _
                "			WHEN SAL.cod_departamento = '01' THEN 'hn-at'" + vbCr + _
                "			WHEN SAL.cod_departamento = '02' THEN 'hn-cl'" + vbCr + _
                "			WHEN SAL.cod_departamento = '03' THEN 'hn-cm'" + vbCr + _
                "			WHEN SAL.cod_departamento = '04' THEN 'hn-cp'" + vbCr + _
                "			WHEN SAL.cod_departamento = '05' THEN 'hn-cr'" + vbCr + _
                "			WHEN SAL.cod_departamento = '06' THEN 'hn-ch'" + vbCr + _
                "			WHEN SAL.cod_departamento = '07' THEN 'hn-ep'" + vbCr + _
                "			WHEN SAL.cod_departamento = '08' THEN 'hn-fm'" + vbCr + _
                "			WHEN SAL.cod_departamento = '09' THEN 'hn-gd'" + vbCr + _
                "			WHEN SAL.cod_departamento = '10' THEN 'hn-in'" + vbCr + _
                "			WHEN SAL.cod_departamento = '11' THEN 'hn-ib'" + vbCr + _
                "			WHEN SAL.cod_departamento = '12' THEN 'hn-lp'" + vbCr + _
                "			WHEN SAL.cod_departamento = '13' THEN 'hn-le'" + vbCr + _
                "			WHEN SAL.cod_departamento = '14' THEN 'hn-oc'" + vbCr + _
                "			WHEN SAL.cod_departamento = '15' THEN 'hn-ol'" + vbCr + _
                "			WHEN SAL.cod_departamento = '16' THEN 'hn-sb'" + vbCr + _
                "			WHEN SAL.cod_departamento = '17' THEN 'hn-va'" + vbCr + _
                "			WHEN SAL.cod_departamento = '18' THEN 'hn-yo'" + vbCr + _
                "		END AS 'hc-key'," + vbCr + _
                "		SAL.diferencia_sal AS 'value'" + vbCr + _
                "	FROM (" + vbCr + _
                "			SELECT geo.cod_departamento," + vbCr + _
                "					COUNT(" + vbCr + _
                "						CASE WHEN planilla_1.cod_persona IS NULL AND planilla_2.cod_persona IS NOT NULL THEN 1 ELSE NULL END" + vbCr + _
                "					) -" + vbCr + _
                "					COUNT(" + vbCr + _
                "						CASE WHEN planilla_1.cod_persona IS NOT NULL AND planilla_2.cod_persona IS NULL THEN 1 ELSE NULL END" + vbCr + _
                "					) AS 'diferencia_sal'" + vbCr + _
                "				FROM SIG_C.dbo.t_sig_ubicaciones_geograficas AS geo" + vbCr + _
                "                   INNER JOIN SIG_C.dbo.t_sig_hogares AS hog ON hog.cod_caserio = geo.cod_caserio" + vbCr + _
                "                   INNER JOIN SIG_C.dbo.t_sig_personas AS per ON per.cod_hogar = hog.cod_hogar" + vbCr + _
                "					LEFT JOIN (" + vbCr + _
                "						SELECT corr.cod_persona" + vbCr + _
                "							FROM SIG_C.dbo.t_sig_corresp_pers_planillas AS corr" + vbCr + _
                "								INNER JOIN SIG_C.dbo.t_sig_detalle_planillas AS det " + vbCr + _
                "									ON corr.cod_pago = det.cod_pago AND det.cod_hogar = corr.cod_hogar" + vbCr + _
                "							WHERE corr.cod_pago = " + planilla1 + " AND corr.elegibilidad = 'SALUD' AND det.programado = 1 AND corr.estado_corresponsabilidad <> 'No Cumple'" + vbCr + _
                "					) AS planilla_1 ON planilla_1.cod_persona = per.cod_persona" + vbCr + _
                "					LEFT JOIN (" + vbCr + _
                "						SELECT corr.cod_persona" + vbCr + _
                "							FROM SIG_C.dbo.t_sig_corresp_pers_planillas AS corr" + vbCr + _
                "								INNER JOIN SIG_C.dbo.t_sig_detalle_planillas AS det " + vbCr + _
                "									ON corr.cod_pago = det.cod_pago AND det.cod_hogar = corr.cod_hogar" + vbCr + _
                "							WHERE corr.cod_pago = " + planilla2 + " AND corr.elegibilidad = 'SALUD' AND det.programado = 1 AND corr.estado_corresponsabilidad <> 'No Cumple'" + vbCr + _
                "					) planilla_2 ON planilla_2.cod_persona = per.cod_persona" + vbCr + _
                "				GROUP BY geo.cod_departamento" + vbCr + _
                "		) SAL" + vbCr + _
                "		ORDER BY SAL.cod_departamento"

            Try
                MyResult = conexion.GetDataSet(sql)
                Return MyResult.Tables(0)
            Catch ex As Exception
                Return Nothing
            End Try

        End Function

        Public Function Fnc_obtener_diferencia_entre_altas_bajas_salud_departamento(ByVal planilla1 As String, ByVal planilla2 As String, ByVal dpto As String) As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr + _
                "SELECT geo.cod_municipio AS 'codigo'," + vbCr + _
                "	    COUNT(" + vbCr + _
                "			CASE WHEN planilla_1.cod_persona IS NULL AND planilla_2.cod_persona IS NOT NULL THEN 1 ELSE NULL END" + vbCr + _
                "		) - " + vbCr + _
                "		COUNT(" + vbCr + _
                "			CASE WHEN planilla_1.cod_persona IS NOT NULL AND planilla_2.cod_persona IS NULL THEN 1 ELSE NULL END" + vbCr + _
                "		) AS 'value'" + vbCr + _
                "	FROM SIG_C.dbo.t_sig_ubicaciones_geograficas AS geo" + vbCr + _
                "       INNER JOIN SIG_C.dbo.t_sig_hogares AS hog ON hog.cod_caserio = geo.cod_caserio" + vbCr + _
                "       INNER JOIN SIG_C.dbo.t_sig_personas AS per ON per.cod_hogar = hog.cod_hogar" + vbCr + _
                "		LEFT JOIN (" + vbCr + _
                "			SELECT corr.cod_persona" + vbCr + _
                "				FROM SIG_C.dbo.t_sig_corresp_pers_planillas AS corr" + vbCr + _
                "					INNER JOIN SIG_C.dbo.t_sig_detalle_planillas AS det " + vbCr + _
                "						ON corr.cod_pago = det.cod_pago AND det.cod_hogar = corr.cod_hogar" + vbCr + _
                "				WHERE corr.cod_pago = " + planilla1 + " AND corr.elegibilidad = 'SALUD' AND det.programado = 1 AND corr.estado_corresponsabilidad <> 'No Cumple'" + vbCr + _
                "		) AS planilla_1 ON planilla_1.cod_persona = per.cod_persona" + vbCr + _
                "		LEFT JOIN (" + vbCr + _
                "			SELECT corr.cod_persona" + vbCr + _
                "				FROM SIG_C.dbo.t_sig_corresp_pers_planillas AS corr" + vbCr + _
                "					INNER JOIN SIG_C.dbo.t_sig_detalle_planillas AS det " + vbCr + _
                "						ON corr.cod_pago = det.cod_pago AND det.cod_hogar = corr.cod_hogar" + vbCr + _
                "				WHERE corr.cod_pago = " + planilla2 + " AND corr.elegibilidad = 'SALUD' AND det.programado = 1 AND corr.estado_corresponsabilidad <> 'No Cumple'" + vbCr + _
                "		) planilla_2 ON planilla_2.cod_persona = per.cod_persona" + vbCr + _
                "   WHERE geo.cod_departamento = " + dpto + vbCr + _
                "	GROUP BY geo.cod_municipio" + vbCr + _
                "   ORDER BY geo.cod_municipio"

            Try
                MyResult = conexion.GetDataSet(sql)
                Return MyResult.Tables(0)
            Catch ex As Exception
                Return Nothing
            End Try

        End Function

#End Region

#Region "Funciones para total de niños cumpliendo, apercibidos, no cumpliendo por componente - general"

        Public Function Fnc_obtener_totales_niños_componente(ByVal pago As String) As DataTable
            'utilizo un UNION por cuestión de rendimiento, la hacer el group by con el componente se tarda mucho
            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr + _
                "SELECT geo.cod_departamento, geo.desc_departamento, geo.cod_municipio, geo.desc_municipio," + vbCr + _
                "		geo.cod_aldea, geo.desc_aldea, geo.cod_caserio, geo.desc_caserio, 'EDUCACIÓN' AS 'componente'," + vbCr + _
                "		EDU.total_niños, EDU.cumplen, EDU.apercibidos/*, EDU.no_cumple," + vbCr + _
                "		(EDU.no_cumple * 100 / EDU.total_niños) AS 'porcentaje'*/" + vbCr + _
                "	FROM SIG_C.dbo.t_sig_ubicaciones_geograficas AS geo" + vbCr + _
                "		INNER JOIN (" + vbCr + _
                "			SELECT det.cod_caserio," + vbCr + _
                "					-- EDUCACIÓN" + vbCr + _
                "					COUNT(*) AS 'total_niños'," + vbCr + _
                "					COUNT(CASE WHEN corr.estado_corresponsabilidad = 'Cumple' THEN 1 ELSE NULL END) AS 'cumplen'," + vbCr + _
                "					COUNT(CASE WHEN corr.estado_corresponsabilidad = 'Apercibido' THEN 1 ELSE NULL END) AS 'apercibidos'/*," + vbCr + _
                "					COUNT(CASE WHEN corr.estado_corresponsabilidad = 'No Cumple' THEN 1 ELSE NULL END) AS 'no_cumple'*/" + vbCr + _
                "				FROM SIG_C.dbo.t_sig_detalle_planillas AS det" + vbCr + _
                "					INNER JOIN SIG_C.dbo.t_sig_corresp_pers_planillas AS corr ON corr.cod_hogar = det.cod_hogar" + vbCr + _
                "				WHERE det.cod_pago = " + pago + " AND corr.cod_pago = " + pago + " AND corr.elegibilidad = 'EDUCACION' AND det.programado = 1 AND corr.estado_corresponsabilidad <> 'No Cumple'" + vbCr + _
                "				GROUP BY det.cod_caserio" + vbCr + _
                "		) EDU ON EDU.cod_caserio = geo.cod_caserio" + vbCr + _
                "UNION" + vbCr + _
                "SELECT geo.cod_departamento, geo.desc_departamento, geo.cod_municipio, geo.desc_municipio," + vbCr + _
                "		geo.cod_aldea, geo.desc_aldea, geo.cod_caserio, geo.desc_caserio, 'SALUD' AS 'componente'," + vbCr + _
                "		SAL.total_niños, SAL.cumplen, SAL.apercibidos/*, SAL.no_cumple," + vbCr + _
                "		(SAL.no_cumple * 100 / SAL.total_niños) AS 'porcentaje'*/" + vbCr + _
                "	FROM SIG_C.dbo.t_sig_ubicaciones_geograficas AS geo" + vbCr + _
                "		INNER JOIN (" + vbCr + _
                "			SELECT det.cod_caserio," + vbCr + _
                "					-- EDUCACIÓN" + vbCr + _
                "					COUNT(*) AS 'total_niños'," + vbCr + _
                "					COUNT(CASE WHEN corr.estado_corresponsabilidad = 'Cumple' THEN 1 ELSE NULL END) AS 'cumplen'," + vbCr + _
                "					COUNT(CASE WHEN corr.estado_corresponsabilidad = 'Apercibido' THEN 1 ELSE NULL END) AS 'apercibidos'/*," + vbCr + _
                "					COUNT(CASE WHEN corr.estado_corresponsabilidad = 'No Cumple' THEN 1 ELSE NULL END) AS 'no_cumple'*/" + vbCr + _
                "				FROM SIG_C.dbo.t_sig_detalle_planillas AS det" + vbCr + _
                "					INNER JOIN SIG_C.dbo.t_sig_corresp_pers_planillas AS corr ON corr.cod_hogar = det.cod_hogar" + vbCr + _
                "				WHERE det.cod_pago = " + pago + " AND corr.cod_pago = " + pago + " AND corr.elegibilidad = 'SALUD' AND det.programado = 1 AND corr.estado_corresponsabilidad <> 'No Cumple'" + vbCr + _
                "				GROUP BY det.cod_caserio" + vbCr + _
                "		) SAL ON SAL.cod_caserio = geo.cod_caserio" + vbCr + _
                "ORDER BY geo.cod_departamento, geo.cod_municipio, geo.cod_aldea, geo.cod_caserio, componente"


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
