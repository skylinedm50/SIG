Namespace SIG.Areas.Mineria.Models

    Public Class Cl_Proyecciones

        Dim conexion As SIG.Areas.Mineria.Models.Cl_Conexion = New SIG.Areas.Mineria.Models.Cl_Conexion

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
                "	FROM SIG_C.dbo.t_sig_info_proyecciones AS info" + vbCr + _
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
                "SELECT geo.cod_departamento, geo.desc_departamento, geo.cod_municipio, geo.desc_municipio, geo.cod_aldea, geo.desc_aldea, geo.cod_caserio, geo.desc_caserio," + vbCr + _
                "       proyeccion_corta, proyeccion, cant_hogares" + vbCr + _
                "	FROM SIG_C.dbo.t_sig_exclusiones_proyecciones AS exc" + vbCr + _
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

    End Class

End Namespace
