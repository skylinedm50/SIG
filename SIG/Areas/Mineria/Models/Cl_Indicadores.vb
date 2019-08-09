Namespace SIG.Areas.Mineria.Models

    Public Class Cl_Indicadores

        Dim conexion As SIG.Areas.Mineria.Models.Cl_Conexion = New SIG.Areas.Mineria.Models.Cl_Conexion

        Public Function Fnc_obtener_hogares_pobreza_extrema() As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr + _
                "SELECT TOP 2 universo AS 'UNIVERSO', numerador AS 'NUMERADOR', denominador AS 'DENOMINADOR', resultado AS 'RESULTADO', fecha_calculo AS 'FECHA CÁLCULO'" + vbCr + _
                "   FROM SIG_C.dbo.t_sig_valores_indicadares" + vbCr + _
                "   WHERE cod_indicador = 5" + vbCr + _
                "	ORDER BY fecha_calculo DESC"

            Try
                MyResult = conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)

        End Function

        Public Function Fnc_obtener_asistencia_1ro_6to_asistiendo() As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr + _
                "SELECT TOP 3 universo AS 'UNIVERSO', numerador AS 'NUMERADOR', denominador AS 'DENOMINADOR', resultado AS 'RESULTADO', fecha_calculo AS 'FECHA CÁLCULO'" + vbCr + _
                "   FROM SIG_C.dbo.t_sig_valores_indicadares" + vbCr + _
                "   WHERE cod_indicador = 1" + vbCr + _
                "	ORDER BY fecha_calculo DESC"

            Try
                MyResult = conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)

        End Function

        Public Function Fnc_obtener_asistencia_7mo_9no_asistiendo() As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr + _
                "SELECT TOP 3 universo AS 'UNIVERSO', numerador AS 'NUMERADOR', denominador AS 'DENOMINADOR', resultado AS 'RESULTADO', fecha_calculo AS 'FECHA CÁLCULO'" + vbCr + _
                "   FROM SIG_C.dbo.t_sig_valores_indicadares" + vbCr + _
                "   WHERE cod_indicador = 2" + vbCr + _
                "	ORDER BY fecha_calculo DESC"

            Try
                MyResult = conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)

        End Function

        Public Function Fnc_obtener_13_15_aprovaron_primaria() As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr + _
                "SELECT TOP 3 universo AS 'UNIVERSO', numerador AS 'NUMERADOR', denominador AS 'DENOMINADOR', resultado AS 'RESULTADO', fecha_calculo AS 'FECHA CÁLCULO'" + vbCr + _
                "   FROM SIG_C.dbo.t_sig_valores_indicadares" + vbCr + _
                "   WHERE cod_indicador = 6" + vbCr + _
                "	ORDER BY fecha_calculo DESC"

            Try
                MyResult = conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)

        End Function

        Public Function Fnc_obtener_16_18_aprovaron_noveno() As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr + _
                "SELECT TOP 3 universo AS 'UNIVERSO', numerador AS 'NUMERADOR', denominador AS 'DENOMINADOR', resultado AS 'RESULTADO', fecha_calculo AS 'FECHA CÁLCULO'" + vbCr + _
                "   FROM SIG_C.dbo.t_sig_valores_indicadares" + vbCr + _
                "   WHERE cod_indicador = 7" + vbCr + _
                "	ORDER BY fecha_calculo DESC"

            Try
                MyResult = conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)

        End Function

        Public Function Fnc_obtener_matriculados_6to_aprovaron() As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr + _
                "SELECT TOP 3 universo AS 'UNIVERSO', numerador AS 'NUMERADOR', denominador AS 'DENOMINADOR', resultado AS 'RESULTADO', fecha_calculo AS 'FECHA CÁLCULO'" + vbCr + _
                "   FROM SIG_C.dbo.t_sig_valores_indicadares" + vbCr + _
                "   WHERE cod_indicador = 8" + vbCr + _
                "	ORDER BY fecha_calculo DESC"

            Try
                MyResult = conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)

        End Function

        Public Function Fnc_obtener_matriculados_9no_aprovaron() As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr + _
                "SELECT TOP 3 universo AS 'UNIVERSO', numerador AS 'NUMERADOR', denominador AS 'DENOMINADOR', resultado AS 'RESULTADO', fecha_calculo AS 'FECHA CÁLCULO'" + vbCr + _
                "   FROM SIG_C.dbo.t_sig_valores_indicadares" + vbCr + _
                "   WHERE cod_indicador = 9" + vbCr + _
                "	ORDER BY fecha_calculo DESC"

            Try
                MyResult = conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)

        End Function

        Public Function Fnc_obtener_hogares_informacion_actualizada() As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr + _
                "SELECT TOP 1 universo AS 'UNIVERSO', numerador AS 'NUMERADOR', denominador AS 'DENOMINADOR', resultado AS 'RESULTADO', fecha_calculo AS 'FECHA CÁLCULO'" + vbCr + _
                "   FROM SIG_C.dbo.t_sig_valores_indicadares" + vbCr + _
                "   WHERE cod_indicador = 10" + vbCr + _
                "	ORDER BY fecha_calculo DESC"

            Try
                MyResult = conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)

        End Function

        Public Function Fnc_obtener_hogares_reciben_todos_pagos() As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr + _
                "SELECT TOP 1 universo AS 'UNIVERSO', resultado AS 'RESULTADO', fecha_calculo AS 'FECHA CÁLCULO'" + vbCr + _
                "   FROM SIG_C.dbo.t_sig_valores_indicadares" + vbCr + _
                "   WHERE cod_indicador = 11 AND universo = 'Todos'" + vbCr + _
                "	ORDER BY fecha_calculo DESC"

            Try
                MyResult = conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)

        End Function

        Public Function Fnc_obtener_hogares_reciben_todos_pagos_bm() As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr + _
                "SELECT TOP 1 universo AS 'UNIVERSO', resultado AS 'RESULTADO', fecha_calculo AS 'FECHA CÁLCULO'" + vbCr + _
                "   FROM SIG_C.dbo.t_sig_valores_indicadares" + vbCr + _
                "   WHERE cod_indicador = 11 AND universo = 'Banco Mundial'" + vbCr + _
                "	ORDER BY fecha_calculo DESC"

            Try
                MyResult = conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)

        End Function

        Public Function Fnc_obtener_centros_salud_reportan_cumplimiento() As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr + _
                "SELECT TOP 1 universo AS 'UNIVERSO', numerador AS 'NUMERADOR', denominador AS 'DENOMINADOR', resultado AS 'RESULTADO', fecha_calculo AS 'FECHA CÁLCULO'" + vbCr + _
                "   FROM SIG_C.dbo.t_sig_valores_indicadares" + vbCr + _
                "   WHERE cod_indicador = 3" + vbCr + _
                "	ORDER BY fecha_calculo DESC"

            Try
                MyResult = conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)

        End Function

        Public Function Fnc_obtener_hogares_pagados_mecanismos_alternos() As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr + _
                "SELECT TOP 1 universo AS 'UNIVERSO', numerador AS 'NUMERADOR', denominador AS 'DENOMINADOR', resultado AS 'RESULTADO', fecha_calculo AS 'FECHA CÁLCULO'" + vbCr + _
                "   FROM SIG_C.dbo.t_sig_valores_indicadares" + vbCr + _
                "   WHERE cod_indicador = 4 AND universo = 'Todos'" + vbCr + _
                "	ORDER BY fecha_calculo DESC"

            Try
                MyResult = conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)

        End Function

        Public Function Fnc_obtener_hogares_pagados_mecanismos_alternos_bm() As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr + _
                "SELECT TOP 1 universo AS 'UNIVERSO', numerador AS 'NUMERADOR', denominador AS 'DENOMINADOR', resultado AS 'RESULTADO', fecha_calculo AS 'FECHA CÁLCULO'" + vbCr + _
                "   FROM SIG_C.dbo.t_sig_valores_indicadares" + vbCr + _
                "   WHERE cod_indicador = 4 AND universo = 'Banco Mundial'" + vbCr + _
                "	ORDER BY fecha_calculo DESC"

            Try
                MyResult = conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)

        End Function


    End Class

End Namespace


