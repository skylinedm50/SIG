Imports System.Data.SqlClient

Namespace SIG.Areas.Contraloria.Models

    Public Class Cierre_Planilla

        Dim Conexion As SIG.Areas.Contraloria.Models.Conexion = New SIG.Areas.Contraloria.Models.Conexion

        'devuelve los departamentos que tienen planillas abiertas
        Public Function getDepartamentos() As DataTable
            'Dim CnxBase As New Conexion
            Dim MyResult As New DataSet
            'Dim sql As String = "SELECT DISTINCT t_glo_depto.cod_depto, nom_depto " & _
            '    "FROM t_glo_depto " & _
            '    "INNER JOIN t_glo_muni ON t_glo_muni.cod_depto = t_glo_depto.cod_depto " & _
            '    "INNER JOIN t_glo_aldeas ON t_glo_aldeas.cod_muni = t_glo_muni.cod_muni " & _
            '    "INNER JOIN t_pla_planillas ON t_pla_planillas.cod_aldea = t_glo_aldeas.cod_aldea " & _
            '    "WHERE estado_planilla >= 0 OR estado_planilla <= 2 " & _
            '    "ORDER BY cod_depto"

            Dim sql As String = "SELECT cod_departamento, desc_departamento " & _
                "FROM t_glo_departamentos "
            'falta colocar un count en el where para obtener solo los departamentos que poseen titulares
            Try
                MyResult = Conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)
        End Function

        'funcion para obtener las planillas abiertas de los departamentos seleccionados
        Public Function getPlanillasAbiertas(ByVal deptos() As String) As DataTable

            Dim where As String = ""
            Dim count As Integer = deptos.Count


            ' código para contruir el IN(valor1, valor2, valor...) de la clausula WHERE
            If count = 1 Then
                where = String.Format("('{0}')", deptos(0))
            Else
                where = String.Format("('{0}'", deptos(0))
                For i = 1 To count - 2
                    where += ",'" + deptos(i) + "'"
                Next
                where += ",'" + deptos(count - 1) + "')"
            End If

            'Dim CnxBase As New Conexion
            Dim MyResult As New DataSet

            Dim sql = "" + vbCr + _
                "SELECT dep.cod_departamento, dep.desc_departamento, mun.cod_municipio, mun.desc_municipio, " + vbCr + _
                "   ald.cod_aldea, ald.desc_aldea, " + vbCr + _
                "   ISNULL(COUNT(tit.tit_codigo),0)AS 'hogProgramados', " + vbCr + _
                "   CONVERT(VARCHAR(30), CONVERT(MONEY, SUM(tit.tit_monto_total)), 1) AS 'montoProgramado', " + vbCr + _
                "   ISNULL(COUNT(PAG.tit_codigo), 0) AS 'hogPagados', " + vbCr + _
                "   ISNULL(CONVERT(VARCHAR(30), CONVERT(MONEY, SUM(PAG.tit_monto_total)), 1),0) AS 'montoPagado', " + vbCr + _
                "   ISNULL(COUNT(tit.tit_codigo), 0) - ISNULL(COUNT(PAG.tit_codigo), 0) AS 'hogNoPagados',  " + vbCr + _
                "   CONVERT(VARCHAR(30),CONVERT(MONEY,SUM(tit.tit_monto_total) - ISNULL(SUM(PAG.tit_monto_total),0)),1) AS 'montoNoPagado', " + vbCr + _
                "   CONVERT(VARCHAR(31),CONVERT(DECIMAL(10, 2), CONVERT(DECIMAL(10, 2), ISNULL(COUNT(PAG.tit_codigo), 0)) / CONVERT(DECIMAL(10, 2), ISNULL(COUNT(tit.tit_codigo), 0)) * 100)) + '%' AS 'cumplimiento' " + vbCr + _
                "FROM f_pla_planilla AS pla " + vbCr + _
                "   INNER JOIN f_pla_esquema AS esq ON esq.esq_codigo = pla.Pri_Numero " + vbCr + _
                "   INNER JOIN f_pla_titulares AS tit ON pla.Pla_Numero = tit.tit_pla_numero " + vbCr + _
                "   INNER JOIN t_glo_aldeas AS ald ON pla.Ald_Codigo = ald.cod_aldea  " + vbCr + _
                "   INNER JOIN t_glo_municipios AS mun ON mun.cod_municipio = ald.cod_municipio  " + vbCr + _
                "   INNER JOIN t_glo_departamentos AS dep ON dep.cod_departamento = mun.cod_departamento  " + vbCr + _
                "   LEFT JOIN ( SELECT DISTINCT f_pla_titulares.tit_codigo, f_pla_titulares.tit_monto_total  " + vbCr + _
                "               FROM f_pla_titulares " + vbCr + _
                "					INNER JOIN t_cnt_pagos ON t_cnt_pagos.cod_titular = f_pla_titulares.tit_codigo " + vbCr + _
                "				) PAG ON PAG.tit_codigo = tit.tit_codigo  " + vbCr + _
                "WHERE esq.esq_tipo_esquema = 1 AND tit.tit_planilla = 1 AND pla.Pla_Estado = 1 AND dep.cod_departamento IN " + where + " " + vbCr + _
                "GROUP BY dep.cod_departamento, dep.desc_departamento, mun.cod_municipio, mun.desc_municipio,  " + vbCr + _
                "    ald.cod_aldea, ald.desc_aldea " + vbCr + _
                "ORDER BY ald.cod_aldea"

            Try
                MyResult = Conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)
        End Function


        Public Function cerrarPlanillas(ByVal values() As String) As Integer

            Dim where As String = ""
            Dim count As Integer = values.Count

            If count = 1 Then
                where = String.Format("('{0}')", values(0))
            Else
                where = String.Format("('{0}'", values(0))
                For i = 1 To count - 2
                    where += ",'" + values(i) + "'"
                Next
                where += ",'" + values(count - 1) + "')"
            End If

            'Dim CnxBase As New Conexion
            Dim MyResult As Integer
            'Dim sql As String = "" + vbCr + _
            '    "UPDATE f_pla_planilla SET Pla_Estado = 3 " + vbCr + _
            '    "   WHERE (Pla_Estado >= 0 AND Pla_Estado <= 2) AND Ald_Codigo IN " + where + " " + vbCr + _
            '    "       Pri_Numero IN (SELECT esq_codigo FROM f_pla_esquema WHERE pag_codigo = )"

            Dim sql As String = "" + vbCr + _
                "UPDATE SIG_T.dbo.f_pla_planilla SET Pla_Estado = 3" + vbCr + _
                "	WHERE Pla_Estado >= 0 AND Pla_Estado <= 2 AND Ald_Codigo IN " + where + vbCr + _
                "		AND Pri_Numero IN (" + vbCr + _
                "            Select esq_codigo" + vbCr + _
                "				FROM SIG_T.dbo.f_pla_esquema " + vbCr + _
                "				WHERE esq_tipo_esquema = 1 AND pag_codigo = (" + vbCr + _
                "					SELECT pag_codigo" + vbCr + _
                "						FROM SIG_T.dbo.f_pla_pago" + vbCr + _
                "						WHERE Pag_Estado = 2" + vbCr + _
                "				)" + vbCr + _
                "		)"

            MyResult = Conexion.updateTable(sql)

            If MyResult > 0 Then
                Conexion.insertLog(HttpContext.Current.Session("usuario"), "Cerro las planilla de las aldeas", "t_pla_planilla", 2, where)
            End If

            Return MyResult
        End Function

        ' función que obtiene un consolidado de pago por caserio según la aldea deseada
        Public Function detalleAldea(ByVal codAldea As String)

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr + _
                "SELECT desc_caserio,  " + vbCr + _
                "   ISNULL(COUNT(tit.tit_codigo),0)AS 'hogProgramados', " + vbCr + _
                "   CONVERT(VARCHAR(30), CONVERT(MONEY, SUM(tit.tit_monto_total)), 1) AS 'montoProgramado', " + vbCr + _
                "   ISNULL(COUNT(PAG.tit_codigo), 0) AS 'hogPagados', " + vbCr + _
                "   ISNULL(CONVERT(VARCHAR(30), CONVERT(MONEY, SUM(PAG.tit_monto_total)), 1),0) AS 'montoPagado', " + vbCr + _
                "   ISNULL(COUNT(tit.tit_codigo), 0) - ISNULL(COUNT(PAG.tit_codigo), 0) AS 'hogNoPagados',  " + vbCr + _
                "   CONVERT(VARCHAR(30),CONVERT(MONEY,SUM(tit.tit_monto_total) - ISNULL(SUM(PAG.tit_monto_total),0)),1) AS 'montoNoPagado', " + vbCr + _
                "   CONVERT(VARCHAR(31),CONVERT(DECIMAL(10, 2), CONVERT(DECIMAL(10, 2), ISNULL(COUNT(PAG.tit_codigo), 0)) / CONVERT(DECIMAL(10, 2), ISNULL(COUNT(tit.tit_codigo), 0)) * 100)) + '%' AS 'cumplimiento' " + vbCr + _
                "FROM f_pla_planilla AS pla " + vbCr + _
                "   INNER JOIN f_pla_esquema AS esq ON esq.esq_codigo = pla.Pri_Numero " + vbCr + _
                "   INNER JOIN f_pla_titulares AS tit ON tit.tit_pla_numero = pla.Pla_Numero " + vbCr + _
                "   INNER JOIN t_glo_caserios AS cas ON cas.cod_caserio = tit.tit_cas_codigo " + vbCr + _
                "   INNER JOIN t_glo_aldeas AS ald ON ald.cod_aldea = pla.Ald_Codigo " + vbCr + _
                "   LEFT JOIN (	SELECT DISTINCT f_pla_titulares.tit_codigo, f_pla_titulares.tit_monto_total " + vbCr + _
                "               FROM f_pla_titulares " + vbCr + _
                "					INNER JOIN t_cnt_pagos ON t_cnt_pagos.cod_titular = f_pla_titulares.tit_codigo " + vbCr + _
                "				) PAG ON PAG.tit_codigo = tit.tit_codigo " + vbCr + _
                "WHERE esq.esq_tipo_esquema = 1 AND tit.tit_planilla = 1 AND pla.Pla_Estado = 1 AND ald.cod_aldea = '" + codAldea + "' " + vbCr + _
                "GROUP BY desc_caserio"

            Try
                MyResult = Conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)
        End Function

        ' obtiene el consolidado por general del período, obteniendo el período que se esta ejecutando
        Public Function getPeriodo()

            Dim MyResult As DataSet
            Dim sql As String

            sql = "" + vbCr + _
                "SELECT pago.pag_codigo, pago.pag_nombre, " + vbCr + _
                "   ISNULL(COUNT(tit.tit_codigo),0)AS 'hogProgramados', " + vbCr + _
                "   CONVERT(VARCHAR(30), CONVERT(MONEY, SUM(tit.tit_monto_total)), 1) AS 'montoProgramado', " + vbCr + _
                "   ISNULL(COUNT(PAG.tit_codigo), 0) AS 'hogPagados', " + vbCr + _
                "   ISNULL(CONVERT(VARCHAR(30), CONVERT(MONEY, SUM(PAG.tit_monto_total)), 1),0) AS 'montoPagado', " + vbCr + _
                "   ISNULL(COUNT(tit.tit_codigo), 0) - ISNULL(COUNT(PAG.tit_codigo), 0) AS 'hogNoPagados',  " + vbCr + _
                "   CONVERT(VARCHAR(30),CONVERT(MONEY,SUM(tit.tit_monto_total) - ISNULL(SUM(PAG.tit_monto_total),0)),1) AS 'montoNoPagado', " + vbCr + _
                "   CONVERT(VARCHAR(31),CONVERT(DECIMAL(10, 2), CONVERT(DECIMAL(10, 2), ISNULL(COUNT(PAG.tit_codigo), 0)) / CONVERT(DECIMAL(10, 2), ISNULL(COUNT(tit.tit_codigo), 0)) * 100)) + '%' AS 'cumplimiento' " + vbCr + _
                "FROM f_pla_titulares AS tit " + vbCr + _
                "	INNER JOIN f_pla_esquema AS esq ON esq.esq_codigo = tit.tit_esquema " + vbCr + _
                "	INNER JOIN f_pla_pago AS pago ON pago.pag_codigo = esq.pag_codigo " + vbCr + _
                "	LEFT JOIN (	SELECT DISTINCT f_pla_titulares.tit_codigo, f_pla_titulares.tit_monto_total " + vbCr + _
                "               FROM f_pla_titulares " + vbCr + _
                "	                INNER JOIN t_cnt_pagos ON t_cnt_pagos.cod_titular = f_pla_titulares.tit_codigo " + vbCr + _
                "				) PAG ON PAG.tit_codigo = tit.tit_codigo " + vbCr + _
                "WHERE esq.esq_tipo_esquema = 1 AND tit.tit_planilla = 1 AND pago.pag_codigo = (SELECT DISTINCT per.pag_codigo FROM f_pla_pago AS per WHERE per.pag_estado = 2) " + vbCr + _
                "GROUP BY pago.pag_codigo, pago.pag_nombre"

            Try
                MyResult = Conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)
        End Function


        ' obtiene el consolidado de pago de los departamentos
        Public Function getPlanillasDepartamentos()

            Dim MyResult As DataSet
            Dim sql As String

            sql = "" + vbCr + _
                "SELECT dep.cod_departamento, dep.desc_departamento, " + vbCr + _
                "   ISNULL(COUNT(tit.tit_codigo),0)AS 'hogProgramados', " + vbCr + _
                "   CONVERT(VARCHAR(30), CONVERT(MONEY, SUM(tit.tit_monto_total)), 1) AS 'montoProgramado', " + vbCr + _
                "   ISNULL(COUNT(PAG.tit_codigo), 0) AS 'hogPagados', " + vbCr + _
                "   ISNULL(CONVERT(VARCHAR(30), CONVERT(MONEY, SUM(PAG.tit_monto_total)), 1),0) AS 'montoPagado', " + vbCr + _
                "   ISNULL(COUNT(tit.tit_codigo), 0) - ISNULL(COUNT(PAG.tit_codigo), 0) AS 'hogNoPagados',  " + vbCr + _
                "   CONVERT(VARCHAR(30),CONVERT(MONEY,SUM(tit.tit_monto_total) - ISNULL(SUM(PAG.tit_monto_total),0)),1) AS 'montoNoPagado', " + vbCr + _
                "   CONVERT(VARCHAR(31),CONVERT(DECIMAL(10, 2), CONVERT(DECIMAL(10, 2), ISNULL(COUNT(PAG.tit_codigo), 0)) / CONVERT(DECIMAL(10, 2), ISNULL(COUNT(tit.tit_codigo), 0)) * 100)) + '%' AS 'cumplimiento' " + vbCr + _
                "FROM f_pla_planilla AS pla " + vbCr + _
                "	INNER JOIN f_pla_esquema AS esq ON esq.esq_codigo = pla.Pri_Numero " + vbCr + _
                "	INNER JOIN f_pla_pago AS pago ON pago.pag_codigo = esq.pag_codigo " + vbCr + _
                "	INNER JOIN f_pla_titulares AS tit ON tit.tit_pla_numero = pla.Pla_Numero " + vbCr + _
                "	INNER JOIN t_glo_aldeas AS ald ON ald.cod_aldea = pla.Ald_Codigo " + vbCr + _
                "	INNER JOIN t_glo_municipios AS mun ON mun.cod_municipio = ald.cod_municipio " + vbCr + _
                "	INNER JOIN t_glo_departamentos AS dep ON dep.cod_departamento = mun.cod_departamento " + vbCr + _
                "	LEFT JOIN ( SELECT DISTINCT f_pla_titulares.tit_codigo, f_pla_titulares.tit_monto_total " + vbCr + _
                "               FROM f_pla_titulares " + vbCr + _
                "   				INNER JOIN t_cnt_pagos ON t_cnt_pagos.cod_titular = f_pla_titulares.tit_codigo " + vbCr + _
                "				) AS pag ON PAG.tit_codigo = tit.tit_codigo " + vbCr + _
                "WHERE esq.esq_tipo_esquema = 1 AND tit.tit_planilla = 1 AND pago.pag_codigo = (SELECT DISTINCT per.pag_codigo FROM f_pla_pago AS per WHERE per.pag_estado = 2) " + vbCr + _
                "GROUP BY dep.cod_departamento, dep.desc_departamento " + vbCr + _
                "ORDER BY dep.cod_departamento"

            Try
                MyResult = Conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)
        End Function

        Public Function cierrePeriodo()

            Dim MiBase As New Conexion()
            Dim myConn As SqlConnection = Conexion.OpenConnection(MiBase.ConnectionString)

            Dim cmd As New SqlCommand("p_cierre_periodo", myConn)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.AddWithValue("@usuario", HttpContext.Current.Session("usuario"))

            Return cmd.ExecuteScalar()

        End Function
    End Class

End Namespace
