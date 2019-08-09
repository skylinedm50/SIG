
Namespace SIG.Areas.Contraloria.Models



    Public Class Actas

        Dim Conexion As SIG.Areas.Contraloria.Models.Conexion = New SIG.Areas.Contraloria.Models.Conexion

        Public Sub New()
        End Sub

        ' obtiene todos los departamentos de la tabla t_glo_departamentos
        Public Function getAllDptos() As DataTable

            Dim MyResult As New DataSet
            'Dim sql As String = "SELECT cod_depto, nom_depto " & _
            '    "FROM t_glo_depto "

            Dim sql As String = "SELECT cod_departamento, desc_departamento " & _
                "FROM t_glo_departamentos "

            Try
                MyResult = Conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)
        End Function

        ' obtiene todos los periodos o pagos en la tabla f_pla_pago
        Public Function getAllPeriodos() As DataTable

            Dim MyResult As New DataSet
            'Dim sql As String = "SELECT cod_periodo, nom_periodo " & _
            '    "FROM t_pla_periodos ORDER BY cod_periodo DESC "

            Dim sql As String = "SELECT pag_codigo, pag_nombre " & _
                "FROM f_pla_pago ORDER BY pag_codigo DESC "
            Try
                MyResult = Conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)
        End Function

        ' obtiene todos los bancos en la tabla t_glo_bancos
        Public Function getAllBancos() As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "SELECT cod_banco, nombre_banco " & _
                "FROM t_glo_bancos"
            Try
                MyResult = Conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)
        End Function

        Public Function getSucursalByBanco(ByVal banco As String) As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "SELECT cod_sucursal, desc_sucursal " & _
                "FROM t_glo_sucursales " & _
                "WHERE cod_banco = '" + banco + "'"
            Try
                MyResult = Conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)
        End Function

        ' obtiene todos los fondos de la tabla t_glo_fondos
        Public Function getAllFondo() As DataTable

            Dim MyResult As New DataSet
            'Dim sql As String = "SELECT cod_fondo, nom_fondo " & _
            '    "FROM t_glo_fondos"

            Dim sql As String = "SELECT fond_codigo, fond_nombre " & _
                "FROM f_pla_fondos"
            Try
                MyResult = Conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)
        End Function

        'obtiene la información del titular recibiendo el número de página y el número de linea del registro
        Public Function infoRecibo(ByVal pagina As String, ByVal registro As String)

            Dim MyResult As DataRow
            Dim sql As String

            sql = " " + vbCr + _
                "SELECT dep.desc_departamento AS nom_depto, mun.desc_municipio AS nom_muni, ald.desc_aldea AS nom_aldea,  " + vbCr + _
                "   tit.tit_nombre1 + ' ' + tit.tit_nombre2 + ' ' + tit.tit_apellido1 + ' ' + tit.tit_apellido2,  " + vbCr + _
                "   tit.tit_monto_total, pag.fecha_pago " + vbCr + _
                "FROM f_pla_titulares AS tit " + vbCr + _
                "   INNER JOIN f_pla_planilla AS pla ON pla.Pla_Numero = tit.tit_pla_numero " + vbCr + _
                "   INNER JOIN t_glo_aldeas AS ald ON ald.cod_aldea = pla.Ald_Codigo " + vbCr + _
                "   INNER JOIN t_glo_municipios AS mun ON mun.cod_municipio = ald.cod_municipio " + vbCr + _
                "   INNER JOIN t_glo_departamentos AS dep ON dep.cod_departamento = mun.cod_departamento " + vbCr + _
                "   LEFT JOIN t_cnt_pagos AS pag ON pag.cod_titular = tit.tit_codigo " + vbCr + _
                "WHERE tit.tit_pagina = " + pagina + " And tit.tit_linea = " + registro
            Try
                MyResult = Conexion.GetRow(sql)
            Catch ex As Exception
                Return Nothing
            End Try

            Return MyResult


        End Function

    End Class

End Namespace
