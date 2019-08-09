Namespace SIG.Areas.Mineria.Models

    Public Class Cl_Hogares

        Dim conexion As SIG.Areas.Mineria.Models.Cl_Conexion = New SIG.Areas.Mineria.Models.Cl_Conexion
        Dim share As Cl_Shared = New Cl_Shared

#Region "Funciones para el historial de pagos del participantes"

        Public Function Fnc_obtener_historial_pagos_hogar(ByVal hogar As String) As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr + _
                "SELECT DISTINCT pla.cod_pago, pla.año_pago, pla.numero_pago, pla.descripcion_pago, " + vbCr + _
                "		CASE " + vbCr + _
                "           WHEN det.cobro = 1 AND det.cobro = 1 THEN 'Pagado'" + vbCr + _
                "			WHEN det.programado = 1 AND det.cobro = 0 THEN 'No Pagado'" + vbCr + _
                "			WHEN det.programado = 0 AND det.cobro = 0 THEN 'Excluido'" + vbCr + _
                "		END AS 'estado'" + vbCr + _
                "	FROM SIG_C.dbo.t_sig_detalle_planillas AS det" + vbCr + _
                "		INNER JOIN SIG_C.dbo.t_sig_planillas AS pla ON pla.cod_pago = det.cod_pago AND pla.cod_caserio = det.cod_caserio" + vbCr + _
                "	WHERE det.cod_hogar = " + hogar + vbCr + _
                "   ORDER BY pla.año_pago, pla.numero_pago"

            Try
                MyResult = conexion.GetDataSet(sql)
                Return MyResult.Tables(0)
            Catch ex As Exception
                Return Nothing
            End Try

            'Return MyResult.Tables(0)

        End Function

        Public Function Fnc_obtener_pago_hogar(ByVal pago As String, ByVal hogar As String)

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
                "   FROM SIG_C.dbo.t_sig_detalle_planillas AS det" + vbCr +
                "       INNER JOIN SIG_C.dbo.t_sig_ubicaciones_geograficas AS geo ON geo.cod_caserio = det.cod_caserio" + vbCr +
                "   WHERE cod_pago = " + pago + " AND cod_hogar = " + hogar + vbCr +
                "" + vbCr +
                "SELECT nivel,nivel_ciclo,niños_total,niños_cumpliendo,niños_apercibidos," + vbCr +
                "		niños_no_cumpliendo,monto_nivel_neto" + vbCr +
                "   FROM SIG_C.dbo.t_sig_estado_cuenta_participantes" + vbCr +
                "   WHERE cod_pago = " + pago + " AND cod_hogar = " + hogar

            Try
                MyResult = conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            MyResult.Tables(0).TableName = "info_planilla"
            MyResult.Tables(1).TableName = "estado_cuenta"

            Return MyResult

        End Function

        Public Function Fnc_obtener_info_hogar(ByVal identidad As String) As DataRow

            Dim MyResult As DataRow

            Dim sql As String = "" + vbCr + _
                "SELECT TOP 1 det.identidad_titular, det.nombre_titular, geo.desc_departamento," + vbCr + _
                "		geo.desc_municipio, geo.desc_aldea, geo.desc_caserio" + vbCr + _
                "   FROM SIG_C.dbo.t_sig_detalle_planillas AS det" + vbCr + _
                "		INNER JOIN SIG_C.dbo.t_sig_ubicaciones_geograficas AS geo ON geo.cod_caserio = det.cod_caserio" + vbCr + _
                "    WHERE det.identidad_titular = '" + identidad + "'" + vbCr + _
                "	ORDER BY cod_pago DESC"

            Try
                MyResult = conexion.GetRow(sql)
            Catch ex As Exception
            End Try

            Return MyResult

        End Function

        Public Function fnc_obtener_personas(ByVal identidad As String, ByVal nombre As String) As DataTable

            Dim MyResult As DataSet

            Dim where As String = ""

            If nombre.Length > 0 And identidad.Length > 0 Then
                where += "nombre LIKE '%" + Replace(nombre, " ", "%") + "%' OR identidad LIKE '%" + identidad + "%'"
            ElseIf nombre.Length = 0 And identidad.Length > 0 Then
                where += "identidad LIKE '%" + identidad + "%'"
            ElseIf nombre.Length > 0 And identidad.Length = 0 Then
                where += "nombre LIKE '%" + Replace(nombre, " ", "%") + "%'"
            End If

            Dim sql As String = "" + vbCr +
                "SELECT geo.desc_departamento, geo.desc_municipio, desc_aldea, desc_caserio," + vbCr +
                "       hog.cod_hogar, per.titular, per.identidad, per.nombre" + vbCr +
                "	FROM SIG_C.dbo.t_sig_personas AS per" + vbCr +
                "		INNER JOIN SIG_C.dbo.t_sig_hogares AS hog ON hog.cod_hogar = per.cod_hogar" + vbCr +
                "		INNER JOIN SIG_C.dbo.t_sig_ubicaciones_geograficas AS geo ON geo.cod_caserio = hog.cod_caserio" + vbCr +
                "	WHERE " + where

            If share.fnc_limitar_area_geografica(HttpContext.Current.Session("usuario")) Then

                sql += "" + vbCr +
                    "		AND geo.cod_departamento IN (" + vbCr +
                    "			SELECT cod_departamento" + vbCr +
                    "				FROM SIG_T.dbo.t_usuario_departamentos" + vbCr +
                    "				WHERE cod_usuario = " + HttpContext.Current.Session("usuario").ToString() + vbCr +
                    "		)"

            End If

            sql += "" + vbCr +
                "ORDER BY geo.cod_departamento, geo.cod_municipio, geo.cod_aldea, " + vbCr +
                "		geo.cod_caserio, hog.cod_hogar, per.titular, per.nombre"

            Try
                MyResult = conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)

        End Function

        Public Function Fnc_listado_ninos(ByVal pago As String, ByVal hogar As String) As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr + _
                "SELECT cod_persona, identidad, nombre_persona, desc_nivel_elegibilidad ,desc_corresponsabilidad, estado_corresponsabilidad" + vbCr + _
                "   FROM SIG_C.dbo.t_sig_corresp_pers_planillas" + vbCr + _
                "	WHERE cod_pago = " + pago + " AND cod_hogar = " + hogar + vbCr + _
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
                "   WHERE cod_pago = " + pago + " AND cod_persona = " + cod_persona

            Try
                MyResult = conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)

        End Function
#End Region

#Region "Funciones para la cantidad de fichas por censo y año"

        Public Function Fnc_obtener_fichas_censo_ano() As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr + _
                "DECLARE @año INT" + vbCr + _
                "DECLARE @año_actual INT" + vbCr + _
                "" + vbCr + _
                "SET @año = (" + vbCr + _
                "    Select valor" + vbCr + _
                "		FROM SIG_C.dbo.t_sig_variables_mineria" + vbCr + _
                "		WHERE cod_variable = 1" + vbCr + _
                ")" + vbCr + _
                "SET @año_actual = YEAR(GETDATE())" + vbCr + _
                "" + vbCr + _
                "SELECT geo.cod_departamento, geo.desc_departamento, geo.cod_municipio, geo.desc_municipio, " + vbCr + _
                "		geo.cod_aldea, geo.desc_aldea, geo.cod_caserio, geo.desc_caserio,ficha.censo," + vbCr + _
                "		CASE" + vbCr + _
                "			WHEN hog.año_ficha = @año THEN CONVERT(NVARCHAR(4),@año)" + vbCr + _
                "			WHEN hog.año_ficha <=  @año_actual AND hog.año_ficha = @año + 1 THEN CONVERT(NVARCHAR(4),@año + 1)" + vbCr + _
                "			WHEN hog.año_ficha <=  @año_actual AND hog.año_ficha = @año + 2 THEN CONVERT(NVARCHAR(4),@año + 2)" + vbCr + _
                "			WHEN hog.año_ficha <=  @año_actual AND hog.año_ficha = @año + 3 THEN CONVERT(NVARCHAR(4),@año + 3)" + vbCr + _
                "			WHEN hog.año_ficha <=  @año_actual AND hog.año_ficha = @año + 4 THEN CONVERT(NVARCHAR(4),@año + 4)" + vbCr + _
                "			WHEN hog.año_ficha <=  @año_actual AND hog.año_ficha = @año + 5 THEN CONVERT(NVARCHAR(4),@año + 5)" + vbCr + _
                "			WHEN hog.año_ficha <=  @año_actual AND hog.año_ficha = @año + 6 THEN CONVERT(NVARCHAR(4),@año + 6)" + vbCr + _
                "			WHEN hog.año_ficha <=  @año_actual AND hog.año_ficha = @año + 7 THEN CONVERT(NVARCHAR(4),@año + 7)" + vbCr + _
                "			WHEN hog.año_ficha <=  @año_actual AND hog.año_ficha = @año + 8 THEN CONVERT(NVARCHAR(4),@año + 8)" + vbCr + _
                "			WHEN hog.año_ficha <=  @año_actual AND hog.año_ficha = @año + 9 THEN CONVERT(NVARCHAR(4),@año + 9)" + vbCr + _
                "			WHEN hog.año_ficha <=  @año_actual AND hog.año_ficha = @año + 10 THEN CONVERT(NVARCHAR(4),@año + 10)" + vbCr + _
                "			WHEN hog.año_ficha > @año_actual THEN 'No validas'" + vbCr + _
                "			ELSE 'Menores a ' + CONVERT(NVARCHAR(4), @año) " + vbCr + _
                "		END AS 'año_ficha'," + vbCr + _
                "		COUNT(*) AS 'fichas'" + vbCr + _
                "	FROM SIG_C.dbo.t_sig_hogares AS hog" + vbCr + _
                "		INNER JOIN SIG_C.dbo.t_sig_fichas AS ficha ON ficha.cod_ficha = hog.cod_ficha" + vbCr + _
                "		INNER JOIN SIG_C.dbo.t_sig_ubicaciones_geograficas AS geo ON geo.cod_caserio = hog.cod_caserio" + vbCr + _
                "	--WHERE det.cod_pago = 2" + vbCr + _
                "	GROUP BY geo.cod_departamento, geo.desc_departamento, geo.cod_municipio, geo.desc_municipio, " + vbCr + _
                "		geo.cod_aldea, geo.desc_aldea, geo.cod_caserio, geo.desc_caserio, ficha.censo, hog.año_ficha" + vbCr + _
                "	ORDER BY geo.cod_departamento, geo.cod_municipio, geo.cod_aldea, geo.cod_caserio"



            'Dim sql As String = "" + vbCr + _
            '    "SELECT geo.cod_departamento, geo.desc_departamento, geo.cod_municipio, geo.desc_municipio," + vbCr + _
            '    "		geo.cod_aldea, geo.desc_aldea, geo.cod_caserio, geo.desc_caserio, fichas.censo," + vbCr + _
            '    "		CASE" + vbCr + _
            '    "			WHEN año_ficha = 2016 THEN '2016'" + vbCr + _
            '    "			WHEN año_ficha = 2015 THEN '2015'" + vbCr + _
            '    "			WHEN año_ficha = 2014 THEN '2014'" + vbCr + _
            '    "			WHEN año_ficha = 2013 THEN '2013'" + vbCr + _
            '    "			WHEN año_ficha > YEAR(GETDATE()) THEN 'Fichas no Validas'" + vbCr + _
            '    "			ELSE 'Menores a 2013'" + vbCr + _
            '    "		END AS 'año_ficha'," + vbCr + _
            '    "		COUNT(*) AS 'fichas'" + vbCr + _
            '    "	FROM SIG_C.dbo.t_sig_fichas AS fichas" + vbCr + _
            '    "		INNER JOIN SIG_C.dbo.t_sig_hogares AS hog ON hog.cod_ficha = fichas.cod_ficha" + vbCr + _
            '    "		INNER JOIN SIG_C.dbo.t_sig_ubicaciones_geograficas AS geo ON geo.cod_caserio = hog.cod_caserio" + vbCr + _
            '    "	GROUP BY geo.cod_departamento, geo.desc_departamento, geo.cod_municipio, geo.desc_municipio," + vbCr + _
            '    "		geo.cod_aldea, geo.desc_aldea, geo.cod_caserio, geo.desc_caserio, fichas.censo, año_ficha" + vbCr + _
            '    "	ORDER BY geo.cod_departamento, geo.cod_municipio, geo.cod_aldea, geo.cod_caserio"


            Try
                MyResult = conexion.GetDataSet(sql)
                Return MyResult.Tables(0)
            Catch ex As Exception
                Return Nothing
            End Try

        End Function

#End Region

#Region "Resultado de Fichas"

        Public Function Fnc_obtener_resultado_fichas() As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr + _
                "SELECT geo.cod_departamento, geo.desc_departamento, geo.cod_municipio, geo.desc_municipio," + vbCr + _
                "		geo.cod_aldea, geo.desc_aldea, geo.cod_caserio, geo.desc_caserio, fichas.estado, COUNT(*) AS 'fichas'" + vbCr + _
                "	FROM SIG_C.dbo.t_sig_hogares AS hog" + vbCr + _
                "		INNER JOIN SIG_C.dbo.t_sig_fichas AS fichas ON fichas.cod_ficha = hog.cod_ficha" + vbCr + _
                "		INNER JOIN SIG_C.dbo.t_sig_ubicaciones_geograficas AS geo ON geo.cod_caserio = hog.cod_caserio" + vbCr + _
                "	GROUP BY geo.cod_departamento, geo.desc_departamento, geo.cod_municipio, geo.desc_municipio," + vbCr + _
                "		geo.cod_aldea, geo.desc_aldea, geo.cod_caserio, geo.desc_caserio, fichas.estado" + vbCr + _
                "	ORDER BY geo.cod_departamento, geo.cod_municipio, geo.cod_aldea, geo.cod_caserio, estado"

            Try
                MyResult = conexion.GetDataSet(sql)
                Return MyResult.Tables(0)
            Catch ex As Exception
                Return Nothing
            End Try

        End Function

#End Region

#Region "Fichas no remitidas por CENNISS"

        Public Function Fnc_obtener_fichas_no_remitidas(ByVal departamento As String, ByVal municipio As String, ByVal aldea As String, ByVal fecha As String) As DataTable

            Dim MyResult As New DataSet
            Dim sql As String

            If Not aldea Is Nothing Then
                sql += vbCr + _
                    "SELECT geo.desc_caserio AS 'area',"
            ElseIf Not municipio Is Nothing Then
                sql += vbCr + _
                    "SELECT geo.desc_aldea AS 'area',"
            ElseIf Not departamento Is Nothing Then
                sql += vbCr + _
                    "SELECT geo.desc_municipio AS 'area',"
            Else
                sql += vbCr + _
                    "SELECT geo.desc_departamento AS 'area',"
            End If

            sql += "" + vbCr + _
                "       COUNT(CASE WHEN fichas.estado = 'No Recibidas' THEN 1 ELSE NULL END) AS 'fichas'" + vbCr + _
                "	FROM SIG_C.dbo.t_sig_ubicaciones_geograficas AS geo" + vbCr + _
                "		LEFT JOIN SIG_C.dbo.t_sig_levantamientos AS lev ON lev.cod_caserio = geo.cod_caserio" + vbCr + _
                "		LEFT JOIN SIG_C.dbo.t_sig_fichas AS fichas ON fichas.cod_levantamiento = lev.cod_levantamiento"

            If Not fecha Is Nothing Then
                sql += vbCr +
                    "   WHERE lev.fecha_envio = '" + fecha + "'"

                If Not aldea Is Nothing Then
                    sql += " AND geo.cod_aldea = '" + aldea + "'"
                ElseIf Not municipio Is Nothing Then
                    sql += " AND geo.cod_municipio = '" + municipio + "'"
                ElseIf Not departamento Is Nothing Then
                    sql += " AND geo.cod_departamento = '" + departamento + "'"
                End If

            Else

                If Not aldea Is Nothing Then
                    sql += vbCr +
                        "   WHERE geo.cod_aldea = '" + aldea + "'"
                ElseIf Not municipio Is Nothing Then
                    sql += vbCr +
                        "   WHERE geo.cod_municipio = '" + municipio + "'"
                ElseIf Not departamento Is Nothing Then
                    sql += vbCr +
                        "   WHERE geo.cod_departamento = '" + departamento + "'"
                End If

            End If

            If Not aldea Is Nothing Then
                sql += vbCr + _
                    "   GROUP BY geo.desc_caserio" + vbCr + _
                    "   ORDER BY geo.desc_caserio"
            ElseIf Not municipio Is Nothing Then
                sql += vbCr + _
                    "   GROUP BY geo.desc_aldea" + vbCr + _
                    "   ORDER BY geo.desc_aldea"
            ElseIf Not departamento Is Nothing Then
                sql += vbCr + _
                    "   GROUP BY geo.desc_municipio" + vbCr + _
                    "   ORDER BY geo.desc_municipio"
            Else
                sql += vbCr + _
                    "   GROUP BY geo.desc_departamento" + vbCr + _
                    "   ORDER BY geo.desc_departamento"
            End If

            Try
                MyResult = conexion.GetDataSet(sql)
                Return MyResult.Tables(0)
            Catch ex As Exception
                Return Nothing
            End Try

        End Function

        Function Fnc_obtener_fechas_envio() As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr + _
                "SELECT (" + vbCr + _
                "	    CASE WHEN DAY(lev.fecha_envio) < 10 THEN  '0' + CONVERT(CHAR(1),DAY(lev.fecha_envio)) ELSE CONVERT(CHAR(2),DAY(lev.fecha_envio)) END" + vbCr + _
                "	    + '/' + " + vbCr + _
                "	    CASE WHEN MONTH(lev.fecha_envio) < 10 THEN  '0' + CONVERT(CHAR(1),MONTH(lev.fecha_envio)) ELSE CONVERT(CHAR(2),MONTH(lev.fecha_envio)) END" + vbCr + _
                "	    + '/' +" + vbCr + _
                "       CONVERT(CHAR(4), Year(lev.fecha_envio))" + vbCr + _
                "   ) AS 'fecha_envio'" + vbCr + _
                "	FROM SIG_C.dbo.t_sig_levantamientos AS lev" + vbCr + _
                "   ORDER BY fecha_envio DESC"

            Try
                MyResult = conexion.GetDataSet(sql)
                Return MyResult.Tables(0)
            Catch ex As Exception
                Return Nothing
            End Try

        End Function

        'BORRAR, lo utilizo para prueba de los gráficos que no de momento no tienen información
        Function Fnc_obtener_prueba() As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr + _
                "DECLARE @TABLE TABLE (" + vbCr + _
                "	area NVARCHAR(50)," + vbCr + _
                "	fichas INT" + vbCr + _
                ")" + vbCr + _
                "" + vbCr + _
                "INSERT INTO @TABLE (area, fichas)" + vbCr + _
                "	VALUES ('ATLANTIDA', 542)" + vbCr + _
                "INSERT INTO @TABLE (area, fichas)" + vbCr + _
                "	VALUES ('COLON', 201)" + vbCr + _
                "INSERT INTO @TABLE (area, fichas)" + vbCr + _
                "	VALUES ('COMAYAGUA', 453)" + vbCr + _
                "INSERT INTO @TABLE (area, fichas)" + vbCr + _
                "	VALUES ('COPAN', 48)" + vbCr + _
                "INSERT INTO @TABLE (area, fichas)" + vbCr + _
                "	VALUES ('CORTES', 365)" + vbCr + _
                "INSERT INTO @TABLE (area, fichas)" + vbCr + _
                "	VALUES ('CHOLUTECA', 89)" + vbCr + _
                "INSERT INTO @TABLE (area, fichas)" + vbCr + _
                "	VALUES ('EL PARAISO', 472)" + vbCr + _
                "INSERT INTO @TABLE (area, fichas)" + vbCr + _
                "	VALUES ('FRANCISCO MORAZAN', 205)" + vbCr + _
                "INSERT INTO @TABLE (area, fichas)" + vbCr + _
                "	VALUES ('GRACIAS A DIOS', 198)" + vbCr + _
                "INSERT INTO @TABLE (area, fichas)" + vbCr + _
                "	VALUES ('INTIBUCA', 174)" + vbCr + _
                "INSERT INTO @TABLE (area, fichas)" + vbCr + _
                "	VALUES ('ISLAS DE LA BAHIA', 256)" + vbCr + _
                "INSERT INTO @TABLE (area, fichas)" + vbCr + _
                "	VALUES ('LA PAZ', 426)" + vbCr + _
                "INSERT INTO @TABLE (area, fichas)" + vbCr + _
                "	VALUES ('LEMPIRA', 361)" + vbCr + _
                "INSERT INTO @TABLE (area, fichas)" + vbCr + _
                "	VALUES ('OCOTEPEQUE', 278)" + vbCr + _
                "INSERT INTO @TABLE (area, fichas)" + vbCr + _
                "	VALUES ('OLANCHO', 193)" + vbCr + _
                "INSERT INTO @TABLE (area, fichas)" + vbCr + _
                "	VALUES ('SANTA  BARBARA', 36)" + vbCr + _
                "INSERT INTO @TABLE (area, fichas)" + vbCr + _
                "	VALUES ('VALLE', 168)" + vbCr + _
                "INSERT INTO @TABLE (area, fichas)" + vbCr + _
                "	VALUES ('YORO', 263)" + vbCr + _
                "" + vbCr + _
                "SELECT *" + vbCr + _
                "	FROM @TABLE"

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
