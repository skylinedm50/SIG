Namespace SIG.Areas.Mineria.Models

    Public Class Cl_Shared

        Dim conexion As SIG.Areas.Mineria.Models.Cl_Conexion = New SIG.Areas.Mineria.Models.Cl_Conexion

#Region "Funciones de área geográfica"

        Public Function Fnc_obtener_departamentos() As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr + _
                "SELECT DISTINCT cod_departamento, desc_departamento " + vbCr + _
                "   FROM t_sig_ubicaciones_geograficas " + vbCr + _
                "   ORDER BY cod_departamento"
            Try
                MyResult = conexion.GetDataSet(sql)
            Catch ex As Exception
                End Try

            Return MyResult.Tables(0)

        End Function

        Public Function Fnc_obtener_municipios(ByVal departamento As String) As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr + _
                "SELECT DISTINCT cod_municipio, desc_municipio " + vbCr + _
                "   FROM t_sig_ubicaciones_geograficas " + vbCr + _
                "   WHERE cod_departamento = '" + departamento + "'" + vbCr + _
                "   ORDER BY cod_municipio"
            Try
                MyResult = conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)

        End Function

        Public Function Fnc_obtener_aldeas(ByVal municipio As String) As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr + _
                "SELECT DISTINCT cod_aldea, desc_aldea " + vbCr + _
                "   FROM t_sig_ubicaciones_geograficas " + vbCr + _
                "   WHERE cod_municipio = '" + municipio + "'" + vbCr + _
                "   ORDER BY cod_aldea"
            Try
                MyResult = conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)

        End Function

        Public Function Fnc_obtener_caserios(ByVal aldea As String) As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr + _
                "SELECT cod_caserio, desc_caserio " + vbCr + _
                "   FROM t_sig_ubicaciones_geograficas " + vbCr + _
                "   WHERE cod_aldea = '" + aldea + "'" + vbCr + _
                "   ORDER BY cod_caserio"
            Try
                MyResult = conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)

        End Function

#End Region

#Region "Funciones para información de los pagos"

        Public Function Fnc_obtener_anos() As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr + _
                "SELECT DISTINCT año_pago " + vbCr + _
                "   FROM t_sig_planillas " + vbCr + _
                "   ORDER BY año_pago DESC"
            Try
                MyResult = conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)

        End Function

        Public Function Fnc_obtener_pagos_ano(ByVal ano As String) As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr + _
                "SELECT DISTINCT cod_pago, numero_pago, descripcion_pago " + vbCr + _
                "   FROM sig_c.dbo.t_sig_planillas " + vbCr + _
                "   WHERE año_pago = " + ano + vbCr + _
                "   ORDER BY cod_pago, numero_pago DESC"
            Try
                MyResult = conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)

        End Function

        Public Function Fnc_obtener_tabla_pagos() As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr + _
                "SELECT DISTINCT cod_pago, año_pago, numero_pago, descripcion_pago" + vbCr + _
                "	FROM SIG_C.dbo.t_sig_planillas" + vbCr + _
                "   ORDER BY cod_pago DESC"
            Try
                MyResult = conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)

        End Function

        Public Function Fnc_obtener_nombre_pago(ByVal pago As String) As String

            Dim sql As String = "" + vbCr + _
                "SELECT TOP 1 descripcion_pago " + vbCr + _
                "   FROM sig_c.dbo.t_sig_planillas " + vbCr + _
                "   WHERE cod_pago = " + pago
            Try
                Return conexion.returnScalar(sql)
            Catch ex As Exception
                Return Nothing
            End Try

        End Function

#End Region

#Region "Funciones para información de las entidades pagadoras"

        Public Function Fnc_obtener_pagadores() As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr + _
                "SELECT DISTINCT cod_pagador, nombre_pagador" + vbCr + _
                "	FROM SIG_C.dbo.t_sig_planillas" + vbCr + _
                "   WHERE nombre_pagador IS NOT NULL" + vbCr + _
                "   ORDER BY cod_pagador"
            Try
                MyResult = conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)

        End Function

        Public Function Fnc_obtener_agencias(ByVal entidad As String) As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr + _
                "SELECT DISTINCT cod_agencia, desc_agencia" + vbCr + _
                "   FROM SIG_C.dbo.t_sig_planillas" + vbCr + _
                "   WHERE desc_agencia IS NOT NULL AND cod_pagador = " + entidad + vbCr + _
                "   ORDER BY cod_agencia"
            Try
                MyResult = conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)

        End Function

        Public Function Fnc_obtener_entidades_pagadoras_por_pago(ByVal pago As String) As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr + _
                "SELECT DISTINCT cod_pagador, nombre_pagador" + vbCr + _
                "   FROM SIG_C.dbo.t_sig_planillas" + vbCr + _
                "   WHERE nombre_pagador IS NOT NULL AND cod_pago = " + pago + vbCr + _
                "   ORDER BY cod_pagador"
            Try
                MyResult = conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            'Dim array() As String = {"0", "Todos"}
            'MyResult.Tables(0).Rows.Add(array)

            Dim row As DataRow = MyResult.Tables(0).NewRow()
            row.Item("cod_pagador") = 0
            row.Item("nombre_pagador") = "Todos"

            MyResult.Tables(0).Rows.InsertAt(row, 0)
            Return MyResult.Tables(0)

        End Function

#End Region

#Region "Funciones para la información de los fondo"

        Public Function Fnc_obtener_fondos_por_pago(ByVal pago As String)

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr + _
                "SELECT DISTINCT cod_fondo, nombre_fondo" + vbCr + _
                "   FROM SIG_C.dbo.t_sig_planillas" + vbCr + _
                "   WHERE cod_pago = " + pago + vbCr + _
                "   ORDER BY cod_fondo"
            Try
                MyResult = conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            'Dim array() As String = {"0", "Todos"}
            'MyResult.Tables(0).Rows.Add(array)

            Dim row As DataRow = MyResult.Tables(0).NewRow()
            row.Item("cod_fondo") = 0
            row.Item("nombre_fondo") = "Todos"

            MyResult.Tables(0).Rows.InsertAt(row, 0)
            Return MyResult.Tables(0)

        End Function

#End Region

#Region "Funciones que devuelven valores"

        Function fnc_obtener_tipo_grafico(ByVal opcion As String) As Object

            Select Case (opcion)
                Case "Bar"
                    Return DevExpress.XtraCharts.ViewType.Bar
                Case "StackedBar"
                    Return DevExpress.XtraCharts.ViewType.StackedBar
                Case "FullStackedBar"
                    Return DevExpress.XtraCharts.ViewType.FullStackedBar
                Case "SideBySideStackedBar"
                    Return DevExpress.XtraCharts.ViewType.SideBySideStackedBar
                Case "SideBySideFullStackedBar"
                    Return DevExpress.XtraCharts.ViewType.SideBySideFullStackedBar
                Case "Pie"
                    Return DevExpress.XtraCharts.ViewType.Pie
                Case "Doughnut"
                    Return DevExpress.XtraCharts.ViewType.Doughnut
                    'Case "NestedDoughnut"
                    'Return DevExpress.XtraCharts.ViewType.NestedDoughnut
                Case "Funnel"
                    Return DevExpress.XtraCharts.ViewType.Funnel
                Case "Point"
                    Return DevExpress.XtraCharts.ViewType.Point
                Case "Bubble"
                    Return DevExpress.XtraCharts.ViewType.Bubble
                Case "Line"
                    Return DevExpress.XtraCharts.ViewType.Line
                Case "StackedLine"
                    Return DevExpress.XtraCharts.ViewType.StackedLine
                Case "FullStackedLine"
                    Return DevExpress.XtraCharts.ViewType.FullStackedLine
                Case "StepLine"
                    Return DevExpress.XtraCharts.ViewType.StepLine
                Case "Spline"
                    Return DevExpress.XtraCharts.ViewType.Spline
                Case "ScatterLine"
                    Return DevExpress.XtraCharts.ViewType.ScatterLine
                    'Case "SwiftPlot"
                    'Return DevExpress.XtraCharts.ViewType.SwiftPlot
                Case "Area"
                    Return DevExpress.XtraCharts.ViewType.Area
                Case "StepArea"
                    Return DevExpress.XtraCharts.ViewType.StepArea
                Case "SplineArea"
                    Return DevExpress.XtraCharts.ViewType.SplineArea
                Case "StackedArea"
                    Return DevExpress.XtraCharts.ViewType.StackedArea
                    'Case "StackedStepArea"
                    '    Return DevExpress.XtraCharts.ViewType.StackedStepArea
                Case "StackedSplineArea"
                    Return DevExpress.XtraCharts.ViewType.StackedSplineArea
                Case "FullStackedArea"
                    Return DevExpress.XtraCharts.ViewType.FullStackedArea
                Case "FullStackedSplineArea"
                    Return DevExpress.XtraCharts.ViewType.FullStackedSplineArea
                    'Case "FullStackedStepArea"
                    '    Return DevExpress.XtraCharts.ViewType.FullStackedStepArea
                Case "RangeArea"
                    Return DevExpress.XtraCharts.ViewType.RangeArea
                Case "Stock"
                    Return DevExpress.XtraCharts.ViewType.Stock
                Case "CandleStick"
                    Return DevExpress.XtraCharts.ViewType.CandleStick
                Case "SideBySideRangeBar"
                    Return DevExpress.XtraCharts.ViewType.SideBySideRangeBar
                Case "SideBySideGantt"
                    Return DevExpress.XtraCharts.ViewType.SideBySideGantt
                Case "Gantt"
                    Return DevExpress.XtraCharts.ViewType.Gantt
                    'Case "PolarPoint"
                    '    Return DevExpress.XtraCharts.ViewType.PolarPoint
                    'Case "PolarLine"
                    '    Return DevExpress.XtraCharts.ViewType.PolarLine
                    'Case "ScatterPolarLine"
                    '    Return DevExpress.XtraCharts.ViewType.ScatterPolarLine
                    'Case "PolarArea"
                    '    Return DevExpress.XtraCharts.ViewType.PolarArea
                    'Case "PolarRangeArea"
                    '    Return DevExpress.XtraCharts.ViewType.PolarRangeArea
                Case "RadarPoint"
                    Return DevExpress.XtraCharts.ViewType.RadarPoint
                Case "RadarLine"
                    Return DevExpress.XtraCharts.ViewType.RadarLine
                    'Case "ScatterRadarLine"
                    'Return DevExpress.XtraCharts.ViewType.ScatterRadarLine
                Case "RadarArea"
                    Return DevExpress.XtraCharts.ViewType.RadarArea
                    'Case "RadarRangeArea"
                    '    Return DevExpress.XtraCharts.ViewType.RadarRangeArea
                Case "Bar3D"
                    Return DevExpress.XtraCharts.ViewType.Bar3D
                Case "StackedBar3D"
                    Return DevExpress.XtraCharts.ViewType.StackedBar3D
                Case "FullStackedBar3D"
                    Return DevExpress.XtraCharts.ViewType.FullStackedBar3D
                Case "ManhattanBar"
                    Return DevExpress.XtraCharts.ViewType.ManhattanBar
                Case "SideBySideStackedBar3D"
                    Return DevExpress.XtraCharts.ViewType.SideBySideStackedBar3D
                Case "SideBySideFullStackedBar3D"
                    Return DevExpress.XtraCharts.ViewType.SideBySideFullStackedBar3D
                Case "Pie3D"
                    Return DevExpress.XtraCharts.ViewType.Pie3D
                Case "Doughnut3D"
                    Return DevExpress.XtraCharts.ViewType.Doughnut3D
                Case "Funnel3D"
                    Return DevExpress.XtraCharts.ViewType.Funnel3D
                Case "Line3D"
                    Return DevExpress.XtraCharts.ViewType.Line3D
                Case "StackedLine3D"
                    Return DevExpress.XtraCharts.ViewType.StackedLine3D
                Case "FullStackedLine3D"
                    Return DevExpress.XtraCharts.ViewType.FullStackedLine3D
                Case "StepLine3D"
                    Return DevExpress.XtraCharts.ViewType.StepLine3D
                Case "Area3D"
                    Return DevExpress.XtraCharts.ViewType.Area3D
                Case "StackedArea3D"
                    Return DevExpress.XtraCharts.ViewType.StackedArea3D
                Case "FullStackedArea3D"
                    Return DevExpress.XtraCharts.ViewType.FullStackedArea3D
                Case "StepArea3D"
                    Return DevExpress.XtraCharts.ViewType.StepArea3D
                Case "Spline3D"
                    Return DevExpress.XtraCharts.ViewType.Spline3D
                Case "SplineArea3D"
                    Return DevExpress.XtraCharts.ViewType.SplineArea3D
                Case "StackedSplineArea3D"
                    Return DevExpress.XtraCharts.ViewType.StackedSplineArea3D
                Case "FullStackedSplineArea3D"
                    Return DevExpress.XtraCharts.ViewType.FullStackedSplineArea3D
                Case "RangeArea3D"
                    Return DevExpress.XtraCharts.ViewType.RangeArea3D
                Case Else
                    Return DevExpress.XtraCharts.ViewType.Bar
            End Select
        End Function

        Public Function fnc_limitar_area_geografica(ByVal usuario As String) As Boolean

            Dim sql As String = "" + vbCr +
                "SELECT COUNT(*)" + vbCr +
                "	FROM SIG_T.dbo.t_usuario_departamentos" + vbCr +
                "	WHERE cod_usuario =  " + usuario
            Try
                If Convert.ToInt16(conexion.returnScalar(sql)) > 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try

        End Function

#End Region

    End Class

End Namespace
