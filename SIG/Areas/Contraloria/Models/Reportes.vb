
Namespace SIG.Areas.Contraloria.Models

    Public Class Reportes

        Dim Conexion As Conexion = New Conexion

        Public Sub New()
        End Sub

#Region "funciones generales"

        'Obtiene todos los pagos registrados en la base de datos
        Public Function getAllPagos() As DataTable
            Dim MyResult As New DataSet
            Dim sql As String = "select pag_codigo,pag_nombre from SIG_T.dbo.f_pla_pago"
            Try
                MyResult = Conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try
            Return MyResult.Tables(0)
        End Function


        Public Function Programado(pago As Integer, departamento As String) As DataTable
            Dim myResult As New DataSet
            Dim sql As String = "SELECT  V_Pla_Programado_2.Pti_Bono, " & _
"                       " & _
"            V_Pla_Programado_2.Hog_Hogar AS HOGAR, " & _
"            DATEPART(Year,V_Pla_Programado_2.Fecha) as AÑO, " & _
"            Fondo AS PROYECTO,  " & _
"             " & _
"            F_Pla_Pagadores.Nombre_Pagador, " & _
"            V_Pla_Programado_2.Dep_Codigo + '-' + V_Pla_Programado_2.Dep_Descripcion AS DEPTO,   " & _
"                      V_Pla_Programado_2.Mun_Codigo + '-' + V_Pla_Programado_2.Mun_Descripcion AS MUNI,  " & _
"            V_Pla_Programado_2.Ald_Codigo + '-' + V_Pla_Programado_2.Ald_DescAldea AS ALDEA,  " & _
"            V_Pla_Programado_2.periodo AS PERIODO,   " & _
"            V_Pla_Programado_2.Fecha_Cobro Fecha_Pago, " & _
"            V_Pla_Programado_2.Nombres + ', ' + V_Pla_Programado_2.Apellidos AS NOMBRE,  " & _
"            V_Pla_Programado_2.Per_Num_Identidad  AS IDENTIDAD,  " & _
"            V_Pla_Programado_2.Pagina, " & _
"             " & _
"            V_Pla_Programado_2.Linea ,  " & _
"            SUBSTRING(CAST(V_Pla_Programado_2.Pagina AS varchar(6))  " & _
"                      + '-' + CASE WHEN LEN(CAST(V_Pla_Programado_2.Linea AS VARCHAR(2))) = 1 THEN '0' + CAST(V_Pla_Programado_2.Linea AS VARCHAR(2))  " & _
"                      ELSE CAST(V_Pla_Programado_2.Linea AS VARCHAR(2)) END, 1, 10) AS [REFERENCIA SISTEMA],  " & _
"                      (CASE WHEN ((pti_bono = 3 ) and V_Pla_Programado_2.Pti_MontoBono > 0 )  THEN 1 ELSE 0 END) AS HOG_PROG_ESC_corresp,                      " & _
"                      (CASE WHEN ((pti_bono in (0) and Pti_Bono_Acum in (3,6,7,8) ) or ((pti_bono =3 and Pti_Bono_Acum in (0,2,3,4,5,6,7,8)))) and (V_Pla_Programado_2.Pti_MontoBonoAcum>0)  THEN 1 ELSE 0 END) AS HOG_PROG_ESC_acum,               " & _
"                      (CASE WHEN ((pti_bono = 3 and Pti_Bono_Acum in (0,2,3,4,5,6,7,8)) or (pti_bono = 0 and Pti_Bono_Acum in (3,6,7,8)) )  THEN 1 ELSE 0 END) AS HOG_PROG_ESC,  " & _
"                       " & _
"                      (CASE WHEN ((pti_bono = 2 ) and V_Pla_Programado_2.Pti_MontoBono > 0 )  THEN 1 ELSE 0 END) AS HOG_PROG_SAL_corresp,                      " & _
"                      (CASE WHEN ((pti_bono in (0,2) and Pti_Bono_Acum in (2,3,4,5,6,7,8))) and (V_Pla_Programado_2.Pti_MontoBonoAcum > 0)  THEN 1 ELSE 0 END) AS HOG_PROG_SAL_acum, " & _
"                      (CASE WHEN ((pti_bono = 2 and Pti_Bono_Acum in (0,2,3,4,5,6,7,8)) or (pti_bono = 0 and Pti_Bono_Acum in (2,4,5))) THEN 1 ELSE 0 END) AS HOG_PROG_SAL,  " & _
"                        " & _
"                      (CASE WHEN V_Pla_Programado_2.Monto_Total > 0 THEN 1 ELSE 0 END) AS HOG_PROG_TOT,  " & _
"                       " & _
"                      (CASE WHEN ((pti_bono = 3 ) )  THEN V_Pla_Programado_2.Pti_MontoBono ELSE 0 END) AS MONTO_PROG_ESC_corresp,                      " & _
"                      (CASE WHEN ((pti_bono in (0) and Pti_Bono_Acum in (3,6,7,8) ) or ((pti_bono =3 and Pti_Bono_Acum in (0,2,3,4,5,6,7,8))))  THEN V_Pla_Programado_2.Pti_MontoBonoAcum ELSE 0 END) AS MONTO_PROG_ESC_acum,               " & _
"                      (CASE WHEN ((pti_bono = 3 and Pti_Bono_Acum in (0,2,3,4,5,6,7,8)) or (pti_bono = 0 and Pti_Bono_Acum in (3,6,7,8)) )  THEN V_Pla_Programado_2.Monto_Total ELSE 0 END) AS MONTO_PROG_ESC,  " & _
"                       " & _
"                      (CASE WHEN ((pti_bono = 2 ) )  THEN V_Pla_Programado_2.Pti_MontoBono ELSE 0 END) AS MONTO_PROG_SAL_corresp,                      " & _
"                      (CASE WHEN ((pti_bono in (0,2) and Pti_Bono_Acum in (2,3,4,5,6,7,8)))  THEN V_Pla_Programado_2.Pti_MontoBonoAcum ELSE 0 END) AS MONTO_PROG_SAL_acum, " & _
"                      (CASE WHEN ((pti_bono = 2 and Pti_Bono_Acum in (0,2,3,4,5,6,7,8)) or (pti_bono = 0 and Pti_Bono_Acum in (2,4,5))) THEN V_Pla_Programado_2.Monto_Total ELSE 0 END) AS MONTO_PROG_SAL,  " & _
"                       " & _
"                      (CASE WHEN V_Pla_Programado_2.Monto_Total > 0 THEN V_Pla_Programado_2.Monto_Total ELSE 0 END) AS MONTO_PROG_TOT,  " & _
"                       " & _
"                       " & _
"                      (CASE WHEN ((pti_bono = 3 and V_Pla_Programado_2.Pti_MontoBono > 0  and V_Pla_Programado_2.Monto_Cobro > 0) )  THEN 1 ELSE 0 END) AS HOG_PAG_ESC_corresp,                      " & _
"                      (CASE WHEN ((pti_bono in (0) and Pti_Bono_Acum in (3,6,7,8) and V_Pla_Programado_2.Pti_MontoBonoAcum > 0 and V_Pla_Programado_2.Monto_Cobro > 0) or ((pti_bono =3 and Pti_Bono_Acum in (0,2,3,4,5,6,7,8)  and V_Pla_Programado_2.Pti_MontoBonoAcum > 0 and V_Pla_Programado_2.Monto_Cobro > 0)))  THEN 1 ELSE 0 END) AS HOG_PAG_ESC_acum,  " & _
"                      (CASE WHEN ((pti_bono = 3 and Pti_Bono_Acum in (0,2,3,4,5,6,7,8) and V_Pla_Programado_2.Monto_Cobro > 0) or (pti_bono = 0 and Pti_Bono_Acum in (3,6,7,8) and V_Pla_Programado_2.Monto_Cobro > 0))  THEN 1 ELSE 0 END) AS HOG_PAG_ESC,  " & _
"                       " & _
"                      (CASE WHEN ((pti_bono = 2  and V_Pla_Programado_2.Pti_MontoBono > 0 and V_Pla_Programado_2.Monto_Cobro > 0) )  THEN 1 ELSE 0 END) AS HOG_PAG_SAL_corresp,                     " & _
"                      (CASE WHEN ((pti_bono in (0,2) and Pti_Bono_Acum in (2,3,4,5,6,7,8) and V_Pla_Programado_2.Pti_MontoBonoAcum > 0 and V_Pla_Programado_2.Monto_Cobro > 0))  THEN 1 ELSE 0 END) AS HOG_PAG_SAL_acum,  " & _
"                      (CASE WHEN ((pti_bono = 2 and Pti_Bono_Acum in (0,2,3,4,5,6,7,8) and V_Pla_Programado_2.Monto_Cobro > 0) or (pti_bono = 0 and Pti_Bono_Acum in (2,4,5) and V_Pla_Programado_2.Monto_Cobro > 0)) THEN 1 ELSE 0 END) AS HOG_PAG_SAL,  " & _
"                      " & _
"                      (CASE WHEN (V_Pla_Programado_2.Monto_Cobro  > 0) THEN 1 ELSE 0 END) AS HOG_PAG_TOT,  " & _
"                                             " & _
"                       " & _
"                      (CASE WHEN ((pti_bono = 3  and V_Pla_Programado_2.Monto_Cobro  > 0) )  THEN V_Pla_Programado_2.Pti_MontoBono ELSE 0 END) AS MONTO_PAG_ESC_corresp,                  " & _
"                      (CASE WHEN ((pti_bono in (0) and Pti_Bono_Acum in (3,6,7,8) and V_Pla_Programado_2.Monto_Cobro  > 0) or ((pti_bono =3 and Pti_Bono_Acum in (0,2,3,4,5,6,7,8) and V_Pla_Programado_2.Monto_Cobro  > 0)))   THEN V_Pla_Programado_2.Pti_MontoBonoAcum ELSE 0 END) AS MONTO_PAG_ESC_acum,                      " & _
"                      (CASE WHEN ((pti_bono = 3 and Pti_Bono_Acum in (0,2,3,4,5,6,7,8) and V_Pla_Programado_2.Monto_Cobro  > 0) or (pti_bono = 0 and Pti_Bono_Acum in (3,6,7,8) and V_Pla_Programado_2.Monto_Cobro  > 0)) THEN V_Pla_Programado_2.Monto_Total ELSE 0 END) AS MONTO_PAG_ESC, " & _
"                       " & _
"                       " & _
"                      (CASE WHEN ((pti_bono = 2  and V_Pla_Programado_2.Monto_Cobro  > 0) )  THEN V_Pla_Programado_2.Pti_MontoBono ELSE 0 END) AS MONTO_PAG_SAL_corresp,                     " & _
"                      (CASE WHEN ((pti_bono in (0) and Pti_Bono_Acum in (2,4,5) and V_Pla_Programado_2.Monto_Cobro  > 0) or ((pti_bono =2 and Pti_Bono_Acum in (0,2,3,4,5,6,7,8) and V_Pla_Programado_2.Monto_Cobro  > 0)))   THEN V_Pla_Programado_2.Pti_MontoBonoAcum ELSE 0 END) AS MONTO_PAG_SAL_acum," & _
"                      (CASE WHEN ((pti_bono = 2 and Pti_Bono_Acum in (0,2,3,4,5,6,7,8) and V_Pla_Programado_2.Monto_Cobro  > 0) or (pti_bono = 0 and Pti_Bono_Acum in (2,4,5) and V_Pla_Programado_2.Monto_Cobro  > 0)) THEN V_Pla_Programado_2.Monto_Total ELSE 0 END) AS MONTO_PAG_SAL, " & _
"                       " & _
"                      (CASE WHEN (V_Pla_Programado_2.Monto_Cobro  > 0) THEN V_Pla_Programado_2.Monto_Cobro ELSE 0 END) AS MONTO_PAG_TOT     " & _
"FROM          v_pla_pro_pag V_Pla_Programado_2  " & _
"           INNER JOIN  " & _
"           F_Pla_Pagadores " & _
"              ON V_Pla_Programado_2.Codigo_Pagador = F_Pla_Pagadores.Codigo_Pagador INNER JOIN " & _
"                      t_ben_hogares ON V_Pla_Programado_2.Hog_Hogar = t_ben_hogares.Hog_Hogar " & _
"                      inner join F_Pla_Planilla  ON F_Pla_Planilla.Pla_Numero = V_Pla_Programado_2.Pla_Numero  " & _
"WHERE v_pla_programado_2.Periodo in  " & _
"  (select esq_codigo from f_pla_esquema where esq_tipo_esquema = 1) and v_pla_programado_2.pag_codigo = " + pago.ToString() + " and Dep_Codigo = " + departamento & _
" order by Periodo "
            Try
                myResult = Conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try
            Return myResult.Tables(0)
        End Function



        'obtiene todos los departamentos de la base de datos
        Public Function getAllDptos() As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "SELECT cod_departamento, desc_departamento " & _
                "FROM t_glo_departamentos "
            Try
                MyResult = Conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)
        End Function

        'obtiene los municipio del departamento especificado
        Public Function getMuniByDpto(ByVal dpto As String) As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "SELECT cod_municipio, desc_municipio " & _
                "FROM t_glo_municipios " & _
                "WHERE cod_departamento = '" + dpto + "'"
            Try
                MyResult = Conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)
        End Function

        'obtiene todas las aldeas del municipio especificado
        Public Function getAldeasByMuni(ByVal muni As String) As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "SELECT cod_aldea, desc_aldea " & _
                "FROM t_glo_aldeas " & _
                "WHERE cod_municipio = '" + muni + "'"
            Try
                MyResult = Conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)
        End Function

        Public Function getAllFondos() As DataTable

            Dim MyResult As New DataSet
            'Dim sql As String = "SELECT cod_fondo, nombre_fondo " & _
            '    "FROM t_glo_fondos "

            Dim sql As String = "SELECT fond_codigo, fond_nombre " & _
                "FROM f_pla_fondos "

            Try
                MyResult = Conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)
        End Function

        Public Function getAllBancos() As DataTable
            Dim MyResult As New DataSet
            Dim sql As String = "SELECT cod_banco, nombre_banco " & _
                "FROM t_glo_bancos "
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

        'obtiene el nombre del departamento
        Public Function getNombreDepartamento(ByVal dpto As String) As String

            Dim MyResult As String
            Dim sql As String

            sql = "SELECT desc_departamento FROM t_glo_departamentos WHERE cod_departamento = " + dpto

            Try
                MyResult = Conexion.getString(sql)

            Catch ex As Exception
                Return Nothing
            End Try

            Return MyResult

        End Function

        'obtiene el nombre del municipio
        Public Function getNombreMunicipio(ByVal muni As String) As String

            Dim MyResult As String
            Dim sql As String

            sql = "SELECT desc_municipio FROM t_glo_municipios WHERE cod_municipio = " + muni

            Try
                MyResult = Conexion.getString(sql)

            Catch ex As Exception
                Return Nothing
            End Try

            Return MyResult

        End Function

        'obtiene el nombre del banco
        Public Function getNombreBanco(ByVal banco As String) As String

            Dim MyResult As String
            Dim sql As String

            sql = "SELECT nombre_banco FROM t_glo_bancos WHERE cod_banco = " + banco

            Try
                MyResult = Conexion.getString(sql)

            Catch ex As Exception
                Return Nothing
            End Try

            Return MyResult

        End Function

        Public Function getAllAnios() As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr +
                "SELECT DISTINCT pag_anyo" + vbCr +
                "   FROM SIG_T.dbo.f_pla_pago"
            Try
                MyResult = Conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)

        End Function

        Public Function getPagosAnios(ByVal anio As String) As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr +
                "SELECT pag_codigo, pag_nombre" + vbCr +
                "   FROM SIG_T.dbo.f_pla_pago" + vbCr +
                "   WHERE pag_anyo = " + anio
            Try
                MyResult = Conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)

        End Function

#End Region

#Region "funciones reporte recibos pagados"

        'obtiene todos los períodos
        Public Function getAllPeriodos() As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "SELECT pag_codigo, pag_nombre " &
                "FROM f_pla_pago "
            Try
                MyResult = Conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)

        End Function

        Public Function getEsquemas(ByVal pago As String) As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "SELECT esq_codigo, nombre_esquema FROM f_pla_esquema WHERE pag_codigo = " + pago
            Try
                MyResult = Conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)

        End Function

        'obtiene el detalle de todos los recibos pagados según los filtros seleccionados por el usuario
        Public Function getDetalleRecibosPagados(ByVal dpto As String, ByVal muni As String, ByVal aldea As String, ByVal fondo As String, ByVal banco As String, ByVal sucursal As String, ByVal inicio As Date, ByVal fin As Date, ByVal periodo As String, ByVal esquema As String, ByVal filtro As String) As DataTable

            Dim myResult As New DataSet
            Dim sql As String = " " + vbCr +
                "SELECT dep.desc_departamento, mun.desc_municipio, ald.desc_aldea, fon.fond_nombre, ban.nombre_banco, suc.desc_sucursal, " + vbCr +
                "   tit.tit_identidad, tit.tit_nombre1 + ' ' + tit.tit_nombre2 AS 'nombres', " + vbCr +
                "   tit.tit_apellido1 + ' ' + tit.tit_apellido2 AS 'apellidos', pago.pag_nombre, tit.tit_pagina, " + vbCr +
                "   tit.tit_linea, tit.tit_monto_total, pag.fecha_pago " + vbCr +
                "FROM t_cnt_pagos AS pag " + vbCr +
                "   INNER JOIN f_pla_titulares AS tit ON tit.tit_codigo = pag.cod_titular " + vbCr +
                "   INNER JOIN f_pla_planilla AS pla ON pla.Pla_Numero = tit.tit_pla_numero " + vbCr +
                "   INNER JOIN f_pla_esquema AS esq ON esq.esq_codigo = pla.Pri_Numero " + vbCr +
                "   INNER JOIN f_pla_pago AS pago ON pago.pag_codigo = esq.pag_codigo " + vbCr +
                "   INNER JOIN f_pla_fondos AS fon ON fon.fond_codigo = tit.tit_fondo " + vbCr +
                "   INNER JOIN t_glo_aldeas AS ald ON ald.cod_aldea = pla.Ald_Codigo " + vbCr +
                "   INNER JOIN t_glo_municipios AS mun ON mun.cod_municipio = ald.cod_municipio " + vbCr +
                "   INNER JOIN t_glo_departamentos AS dep ON dep.cod_departamento = mun.cod_departamento " + vbCr +
                "   INNER JOIN t_glo_bancos AS ban ON ban.cod_banco = pag.cod_banco " + vbCr +
                "   LEFT JOIN t_glo_sucursales AS suc ON suc.cod_sucursal = pag.cod_sucursal " + vbCr +
                "WHERE esq.esq_tipo_esquema = 1 AND tit.tit_planilla = 1 AND pag.estado_pago = 0 AND"

            If filtro = "fecha" Then
                sql += " pag.fecha_pago BETWEEN '" + inicio + "' AND '" + fin + " 23:59:59'"
            End If

            If filtro = "periodo" Then
                sql += " pago.pag_codigo = " + periodo
            End If

            If Not esquema = "" And esquema <> "0" Then
                sql += " AND esq.esq_codigo = '" + esquema + "'"
            End If

            If Not dpto = "" Then
                sql += " AND dep.cod_departamento = '" + dpto + "'"

                If Not muni = "" Then
                    sql += " AND mun.cod_municipio = '" + muni + "'"

                    If Not aldea = "" Then
                        sql += " AND ald.cod_aldea = '" + aldea + "'"
                    End If
                End If
            End If

            If Not fondo = 0 Then
                sql += " AND fon.fond_codigo = " + fondo
            End If

            If Not banco = 0 Then
                sql += " AND ban.cod_banco = " + banco

                If Not sucursal = 0 Then
                    sql += " AND suc.cod_sucursal = " + sucursal
                End If
            End If

            Try
                myResult = Conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return myResult.Tables(0)
        End Function

        'obtiene el resumen de la cantidad de recibos pagados según los filtros escogidos el usuario
        Public Function getCantidadRecibosPagados(ByVal dpto As String, ByVal muni As String, ByVal aldea As String, ByVal fondo As String, ByVal banco As String, ByVal sucursal As String, ByVal inicio As Date, ByVal fin As Date, ByVal periodo As String, ByVal esquema As String, ByVal filtro As String) As DataTable
            Dim myResult As New DataSet
            Dim sql As String

            sql = "" + vbCr +
                "SELECT fon.fond_nombre, ban.nombre_banco, suc.desc_sucursal, dep.desc_departamento, mun.desc_municipio, " + vbCr +
                "   ald.desc_aldea, COUNT(cod_pago) AS cantidad " + vbCr +
                "FROM f_pla_titulares AS tit " + vbCr +
                "   INNER JOIN f_pla_planilla AS pla ON pla.Pla_Numero = tit.tit_pla_numero " + vbCr +
                "	INNER JOIN t_pln_fechas_pagos AS fchpag ON fchpag.cod_planilla = pla.Pla_Numero " + vbCr +
                "	INNER JOIN t_glo_bancos AS ban ON ban.cod_banco = fchpag.cod_banco  " + vbCr +
                "	INNER JOIN t_pln_pagos_sucursales AS sucpag ON sucpag.cod_planilla = pla.Pla_Numero " + vbCr +
                "	INNER JOIN t_glo_sucursales AS suc ON suc.cod_sucursal = sucpag.cod_sucursal " + vbCr +
                "	INNER JOIN f_pla_esquema AS esq ON esq.esq_codigo = pla.Pri_Numero " + vbCr +
                "	INNER JOIN f_pla_pago AS pago ON pago.pag_codigo = esq.pag_codigo " + vbCr +
                "	INNER JOIN t_glo_aldeas AS ald ON ald.cod_aldea = pla.Ald_Codigo " + vbCr +
                "	INNER JOIN t_glo_municipios AS mun ON mun.cod_municipio = ald.cod_municipio " + vbCr +
                "	INNER JOIN t_glo_departamentos AS dep ON dep.cod_departamento = mun.cod_departamento " + vbCr +
                "	INNER JOIN f_pla_fondos AS fon ON fon.fond_codigo = tit.tit_fondo " + vbCr +
                "	LEFT JOIN t_cnt_pagos AS pag ON pag.cod_titular = tit.tit_codigo " + vbCr +
                "WHERE esq.esq_tipo_esquema = 1 AND tit.tit_planilla = 1 AND"


            'arreglar el where, el estado_titular es temporal, es necesario validar si ya existe un campo en el where para agregar el and o no
            If filtro = "fecha" Then
                sql += " pag.fecha_pago BETWEEN '" + inicio + "' AND '" + fin + " 23:59:59'"
            End If

            If filtro = "periodo" Then
                sql += " pago.pag_codigo = " + periodo
            End If

            If Not esquema = "" And esquema <> "0" Then
                sql += " AND esq.esq_codigo = '" + esquema + "'"
            End If

            If Not dpto = "" Then
                sql += " AND dep.cod_departamento = '" + dpto + "'"

                If Not muni = "" Then
                    sql += " AND mun.cod_municipio = '" + muni + "'"

                    If Not aldea = "" Then
                        sql += " AND ald.cod_aldea = '" + aldea + "'"
                    End If
                End If
            End If

            If Not fondo = 0 Then
                sql += " AND fon.fond_codigo = " + fondo
            End If

            If Not banco = 0 Then
                sql += " AND ban.cod_banco = " + banco

                If Not sucursal = 0 Then
                    sql += " AND suc.cod_sucursal = " + sucursal
                End If
            End If

            sql += " GROUP BY ald.cod_aldea, fon.fond_nombre, ban.nombre_banco, suc.desc_sucursal, dep.desc_departamento, mun.desc_municipio, ald.desc_aldea " + vbCr &
                "ORDER BY ald.cod_aldea"

            Try
                myResult = Conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return myResult.Tables(0)
        End Function

#End Region

#Region "funciones reporte consolidado"

        'obtiene la información de los períodos de pago, obteniendo el código del pago, nombre del período, los hogares programados, la primera y ultima fecha de pago
        Public Function getPeriodos() As DataTable

            Dim MyResult As New DataSet
            Dim sql = "" + vbCr +
                "SELECT pago.pag_codigo, pago.pag_nombre AS nombrePeriodo, " + vbCr +
                "   ISNULL(COUNT(tit.tit_codigo),0) AS hogProgr, " + vbCr +
                "	CONVERT(VARCHAR(30),CONVERT(MONEY,SUM(tit.tit_monto_total)),1) AS montoProg, " + vbCr +
                "	ISNULL(CONVERT(VARCHAR,MIN(pag.fecha_pago),106), 0) AS 'primeraFecha', " + vbCr +
                "	ISNULL(CONVERT(VARCHAR,MAX(pag.fecha_pago),106), 0) AS 'ultimaFecha' " + vbCr +
                "FROM f_pla_esquema AS esq " + vbCr +
                "	INNER JOIN f_pla_titulares AS tit ON tit.tit_esquema = esq.esq_codigo " + vbCr +
                "	INNER JOIN f_pla_pago AS pago ON pago.pag_codigo = esq.pag_codigo " + vbCr +
                "	LEFT JOIN ( SELECT DISTINCT f_pla_titulares.tit_codigo, f_pla_titulares.tit_monto_total, t_cnt_pagos.fecha_pago " + vbCr +
                "               FROM f_pla_titulares " + vbCr +
                "	                INNER JOIN t_cnt_pagos ON t_cnt_pagos.cod_titular = f_pla_titulares.tit_codigo " + vbCr +
                "               ) AS pag ON pag.tit_codigo = tit.tit_codigo " + vbCr +
                "WHERE esq.esq_tipo_esquema = 1 AND tit.tit_planilla = 1 " + vbCr +
                "GROUP BY pago.pag_codigo, pago.pag_nombre " + vbCr +
                "ORDER BY pago.pag_codigo DESC"

            Try
                MyResult = Conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)
        End Function

        Public Function fnc_obtener_consolidado(ByVal pago As String) As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr +
                "SELECT	geo.desc_departamento, geo.desc_municipio, geo.desc_aldea, tit.tit_monto_total," + vbCr +
                "   	fon.fond_nombre, pag.Nombre_Pagador, CONVERT(NVARCHAR(255),nombre_esquema) AS 'nombre_esquema', --suc.desc_sucursal," + vbCr +
                "   	COUNT(*) AS 'hogares_programados'," + vbCr +
                "		SUM(tit.tit_monto_total) AS 'monto_programado'," + vbCr +
                "		COUNT(CASE WHEN tit.tit_cobro = 1 THEN 1 ELSE NULL END) AS 'hogares_pagados'," + vbCr +
                "		SUM(CASE WHEN tit.tit_cobro = 1 THEN tit.tit_monto_total ELSE 0 END) AS 'monto_pagado'," + vbCr +
                "		COUNT(CASE WHEN tit.tit_cobro = 0 THEN 1 ELSE NULL END) AS 'hogares_no_pagados'," + vbCr +
                "		SUM(CASE WHEN tit.tit_cobro = 0 THEN tit.tit_monto_total ELSE 0 END) AS 'monto_no_pagado'" + vbCr +
                "	FROM SIG_T.dbo.f_pla_titulares AS tit" + vbCr +
                "		INNER JOIN SIG_T.dbo.f_pla_esquema AS esq ON esq.esq_codigo = tit.tit_esquema" + vbCr +
                "		INNER JOIN SIG_T.dbo.V_Glo_caserios AS geo ON geo.cod_caserio = tit.tit_cas_codigo" + vbCr +
                "		INNER JOIN SIG_T.dbo.f_pla_fondos AS fon ON fon.fond_codigo = tit.tit_fondo" + vbCr +
                "		INNER JOIN SIG_T.dbo.f_pla_pagadores AS pag ON pag.Codigo_Pagador = tit.tit_pagador" + vbCr +
                "		--INNER JOIN SIG_T.dbo.t_glo_sucursales AS suc ON pag.cod_banco = suc.cod_banco" + vbCr +
                "	WHERE esq.pag_codigo = " + pago + " And esq.esq_tipo_esquema = 1 And tit.tit_planilla = 1" + vbCr +
                "	GROUP BY geo.cod_departamento, geo.desc_departamento, geo.cod_municipio, geo.desc_municipio," + vbCr +
                "		geo.cod_aldea, geo.desc_aldea, tit_monto_total, fon.fond_nombre, pag.Nombre_Pagador, CONVERT(NVARCHAR(255),nombre_esquema)--, suc.desc_sucursal" + vbCr +
                "	ORDER BY cod_departamento, geo.cod_municipio, geo.cod_aldea, fon.fond_nombre, pag.Nombre_Pagador, tit_monto_total"

            Try
                MyResult = Conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)

        End Function

        Public Sub insertLog(ByVal usuario As String, ByVal operacion As String, ByVal tabla As String, ByVal suceso As String, ByVal registro As String)
            Conexion.insertLog(usuario, operacion, tabla, suceso, registro)
        End Sub

#End Region

#Region "funciones cuentas actividas por pagos"

        Public Function fnc_resumen_cuentas_activadas(ByVal pago As String) As DataTable

            Dim MyResult As New DataSet
            Dim sql = "" + vbCr +
                "Select	ban.nombre_banco" + vbCr +
                "		,geo.desc_departamento" + vbCr +
                "		,geo.desc_municipio" + vbCr +
                "		,geo.desc_aldea" + vbCr +
                "		,geo.desc_caserio" + vbCr +
                "		,TMP.total_cuentas" + vbCr +
                "		,TMP.monto_total" + vbCr +
                "		,TMP.total_cuentas - ISNULL(TMP2.cuentas_nuevas,0) As 'cuentas_ya_activas'" + vbCr +
                "		,TMP.monto_total - ISNULL(TMP2.monto_nuevas,0) AS 'monto_ya_activas'" + vbCr +
                "		,ISNULL(TMP2.cuentas_nuevas,0) AS 'cuentas_nuevas'" + vbCr +
                "		,ISNULL(TMP2.monto_nuevas,0) AS 'monto_nuevas'" + vbCr +
                "		,ISNULL(TMP2.cuentas_activadas,0) AS 'cuentas_activadas'" + vbCr +
                "		,ISNULL(TMP2.monto_activadas,0) AS 'monto_activadas'" + vbCr +
                "		,ISNULL(TMP2.cuentas_no_activadas,0) AS 'cuentas_no_activadas'" + vbCr +
                "		,ISNULL(TMP2.monto_no_activadas,0) AS 'monto_no_activadas'" + vbCr +
                "	FROM SIG_T.dbo.V_Glo_caserios AS geo" + vbCr +
                "		LEFT JOIN (" + vbCr +
                "			SELECT	arc.cod_banco" + vbCr +
                "					,tit_cas_codigo" + vbCr +
                "					,COUNT(*) AS 'total_cuentas'" + vbCr +
                "					,SUM(tit_monto_total) AS 'monto_total'" + vbCr +
                "				FROM SIG_T.dbo.t_cnt_archivos_bancarizacion AS arc" + vbCr +
                "					INNER JOIN SIG_T.dbo.t_cnt_arch_banc_reg_detalle_p AS arc_reg ON arc.cod_arch_bancarizacion = arc_reg.cod_arch_bancarizacion" + vbCr +
                "					INNER JOIN SIG_T.dbo.t_cnt_registros_arch_detalle_p AS det ON det.cod_registro_detalle_pagos = arc_reg.cod_registro_detalle_pagos" + vbCr +
                "					INNER JOIN SIG_T.dbo.f_pla_titulares AS tit ON tit.tit_referencia = det.referencia" + vbCr +
                "					INNER JOIN SIG_T.dbo.f_pla_esquema AS esq ON esq.esq_codigo = tit.tit_esquema" + vbCr +
                "				WHERE arc.cod_tipo_arch_banc = 5" + vbCr +
                "					AND arc.cod_estado_arch_bancarizacion IN (6,7)" + vbCr +
                "					AND arc.ultimo_arch_bancarizacion = 1" + vbCr +
                "					AND arc.cod_pago =  " + pago + vbCr +
                "					AND esq.esq_tipo_esquema = 1" + vbCr +
                "					AND tit.tit_planilla = 1" + vbCr +
                "					AND tit.tit_pagador = 7" + vbCr +
                "					AND esq.pag_codigo = " + pago + vbCr +
                "				GROUP BY cod_banco" + vbCr +
                "					,tit_cas_codigo" + vbCr +
                "		) TMP" + vbCr +
                "			ON TMP.tit_cas_codigo = geo.cod_caserio" + vbCr +
                "		LEFT JOIN (" + vbCr +
                "			SELECT	arc.cod_banco" + vbCr +
                "					,reg.cod_caserio" + vbCr +
                "					,COUNT(*) AS 'cuentas_nuevas'" + vbCr +
                "					,SUM(CONVERT(INT,total_pago)) AS 'monto_nuevas'" + vbCr +
                "					,COUNT(CASE WHEN arc_reg.cod_estado_registro_pre_carga = 20 THEN 1 ELSE NULL END) 'cuentas_activadas'" + vbCr +
                "					,SUM(CASE WHEN arc_reg.cod_estado_registro_pre_carga = 20 THEN CONVERT(INT,total_pago) ELSE 0 END) 'monto_activadas'" + vbCr +
                "					,COUNT(*) - COUNT(CASE WHEN arc_reg.cod_estado_registro_pre_carga = 20 THEN 1 ELSE NULL END) AS 'cuentas_no_activadas'" + vbCr +
                "					,SUM(CONVERT(INT,total_pago)) - SUM(CASE WHEN arc_reg.cod_estado_registro_pre_carga = 20 THEN CONVERT(INT,total_pago) ELSE 0 END) AS 'monto_no_activadas'" + vbCr +
                "				FROM SIG_T.dbo.t_cnt_archivos_bancarizacion AS arc" + vbCr +
                "					INNER JOIN SIG_T.dbo.t_cnt_arch_banc_reg_apertura AS arc_reg ON arc_reg.cod_arch_bancarizacion = arc.cod_arch_bancarizacion" + vbCr +
                "					INNER JOIN SIG_T.dbo.t_cnt_registros_arch_apertura AS reg ON reg.cod_registro_apertura = arc_reg.cod_registro_apertura" + vbCr +
                "					INNER JOIN (" + vbCr +
                "						SELECT reg.identidad_persona, reg.total_pago" + vbCr +
                "							FROM SIG_T.dbo.t_cnt_archivos_bancarizacion AS arc" + vbCr +
                "								INNER JOIN SIG_T.dbo.t_cnt_arch_banc_reg_detalle_p AS arc_reg ON arc_reg.cod_arch_bancarizacion = arc.cod_arch_bancarizacion" + vbCr +
                "								INNER JOIN SIG_T.dbo.t_cnt_registros_arch_detalle_p AS reg ON reg.cod_registro_detalle_pagos = arc_reg.cod_registro_detalle_pagos" + vbCr +
                "							WHERE arc.cod_tipo_arch_banc = 5" + vbCr +
                "								AND arc.cod_estado_arch_bancarizacion IN (6,7)" + vbCr +
                "								AND arc.ultimo_arch_bancarizacion = 1" + vbCr +
                "								AND arc.cod_pago =  " + pago + vbCr +
                "					) TMP ON TMP.identidad_persona = reg.identidad_persona" + vbCr +
                "				WHERE arc.cod_tipo_arch_banc = 4" + vbCr +
                "					AND arc.cod_estado_arch_bancarizacion IN (6,7)" + vbCr +
                "					AND arc.ultimo_arch_bancarizacion = 1" + vbCr +
                "					AND arc.cod_pago =  " + pago + vbCr +
                "				GROUP BY arc.cod_banco" + vbCr +
                "					,reg.cod_caserio" + vbCr +
                "		) TMP2" + vbCr +
                "			ON tmp2.cod_caserio = geo.cod_caserio" + vbCr +
                "		INNER JOIN SIG_T.dbo.t_glo_bancos AS ban ON ban.cod_banco = tmp2.cod_banco OR ban.cod_banco = TMP.cod_banco"

            Try
                MyResult = Conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)

        End Function

#End Region

#Region "funciones para cuentas básicas"

        Public Function fnc_detalle_cuentas_basicas(ByVal pago As String) As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr +
                "SELECT	reg.desc_departamento" + vbCr +
                "		,reg.desc_municipio" + vbCr +
                "		,reg.desc_aldea" + vbCr +
                "		,reg.desc_caserio" + vbCr +
                "		,reg.identidad_persona" + vbCr +
                "		,ISNULL(reg.primer_nombre,'') + ' ' + ISNULL(reg.segundo_nombre,'') AS 'nombres'" + vbCr +
                "		,ISNULL(reg.primer_apellido,'') + ' ' + ISNULL(reg.segundo_apellido,'') AS 'apellidos'" + vbCr +
                "       ,ban.nombre_banco" + vbCr +
                "		,CASE WHEN arc_reg.cod_estado_registro_pre_carga = 20 THEN 'Cuenta aperturada' ELSE 'Cuenta no aperturada' END AS 'estado'" + vbCr +
                "		,CASE WHEN arc_reg.cod_estado_registro_pre_carga = 20 THEN reg.fecha_gestion ELSE '' END AS 'fecha_apertura'" + vbCr +
                "	FROM SIG_T.dbo.t_cnt_archivos_bancarizacion AS arc" + vbCr +
                "		INNER JOIN SIG_T.dbo.t_cnt_arch_banc_reg_apertura AS arc_reg" + vbCr +
                "			ON arc_reg.cod_arch_bancarizacion = arc.cod_arch_bancarizacion" + vbCr +
                "		INNER JOIN SIG_T.dbo.t_cnt_registros_arch_apertura AS reg" + vbCr +
                "			ON reg.cod_registro_apertura = arc_reg.cod_registro_apertura" + vbCr +
                "       INNER JOIN SIG_T.dbo.t_glo_bancos AS ban" + vbCr +
                "			ON ban.cod_banco = arc.cod_banco" + vbCr +
                "	WHERE arc.cod_tipo_arch_banc = 4" + vbCr +
                "		AND arc.cod_estado_arch_bancarizacion IN (6,7)" + vbCr +
                "		AND arc.cod_pago = " + pago + vbCr +
                "	ORDER BY cod_departamento, cod_municipio, cod_aldea, cod_caserio, nombres"

            Try
                MyResult = Conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)

        End Function

#End Region


    End Class

End Namespace
