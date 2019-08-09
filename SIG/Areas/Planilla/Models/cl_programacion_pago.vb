Namespace SIG.Areas.Planilla.Models

    Public Class cl_programacion_pago

        Dim conexion As cl_conexion_db = New SIG.Areas.Planilla.Models.cl_conexion_db

        Public Function fnc_obtener_pagadores_fechas(ByVal pago As String) As DataTable

            Dim MyResult As New DataTable
            Dim sql As String = "" + vbCr +
                "SELECT DISTINCT pag.Codigo_Pagador, pag.Nombre_Pagador, pro.fecha_inicio, pro.fecha_fin" + vbCr +
                "	FROM SIG_T.dbo.f_pla_titulares AS tit" + vbCr +
                "		INNER JOIN SIG_T.dbo.f_pla_esquema AS esq ON esq.esq_codigo = tit.tit_esquema" + vbCr +
                "		INNER JOIN SIG_T.dbo.f_pla_pagadores AS pag ON pag.Codigo_Pagador = tit.tit_pagador" + vbCr +
                "		INNER JOIN SIG_T.dbo.f_pla_receptor_pagador AS recp ON recp.pag_codigo = pag.Codigo_Pagador" + vbCr +
                "		INNER JOIN SIG_T.dbo.f_pla_receptor_config AS rec ON rec.recep_codigo = recp.recep_codigo" + vbCr +
                "		INNER JOIN SIG_T.dbo.f_pla_receptor_archivos AS reca ON reca.recep_codigo = rec.recep_codigo" + vbCr +
                "		LEFT JOIN SIG_T.dbo.f_pla_prog_bancarizacion AS pro" + vbCr +
                "			ON pro.pag_codigo = esq.pag_codigo And pro.codigo_pagador = tit.tit_pagador" + vbCr +
                "	WHERE esq.esq_tipo_esquema = 1 AND tit.tit_planilla = 1 AND reca.recep_codigo IN (4,5)" + vbCr +
                "		AND esq.pag_codigo = " + pago

            Try
                MyResult = conexion.fnc_crear_datatable(sql)
            Catch ex As Exception
            End Try

            Return MyResult

        End Function

        Public Function fnc_insertar_actualizar_periodo_apertuar(ByVal pago As String, ByVal pagador As String, ByVal inicio As String, ByVal fin As String) As DataTable

            Dim MyResult As New DataTable
            Dim sql As String = "" + vbCr +
                "IF EXISTS(SELECT * FROM SIG_T.dbo.f_pla_prog_bancarizacion WHERE pag_codigo = " + pago + " AND codigo_pagador = " + pagador + ")" + vbCr +
                "BEGIN" + vbCr +
                "	UPDATE [SIG_T].[dbo].[f_pla_prog_bancarizacion]" + vbCr +
                "		SET  [fecha_inicio] = CONVERT(DATE, '" + inicio + "', 101)" + vbCr +
                "			,[fecha_fin] = CONVERT(DATE, '" + fin + "', 101)" + vbCr +
                "			,[fecha_registro] = GETDATE()" + vbCr +
                "		WHERE pag_codigo = " + pago + " And codigo_pagador = " + pagador + vbCr +
                "END" + vbCr +
                "ELSE" + vbCr +
                "BEGIN" + vbCr +
                "	INSERT INTO [SIG_T].[dbo].[f_pla_prog_bancarizacion] ([pag_codigo],[codigo_pagador],[fecha_inicio],[fecha_fin],[cod_usuario])" + vbCr +
                "		 VALUES (" + pago + ", " + pagador + ", CONVERT(DATE, '" + inicio + "', 101), CONVERT(DATE, '" + fin + "', 101), " + HttpContext.Current.Session("usuario").ToString() + ")" + vbCr +
                "END"

            Try
                MyResult = conexion.fnc_ejecutar_simple_comando(sql)
            Catch ex As Exception
            End Try

            Return MyResult

        End Function

    End Class

End Namespace