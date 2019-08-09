Imports System.Data.SqlClient

Namespace SIG.Areas.Contraloria.Models

    Public Class Carga

        Dim Conexion As Conexion = New Conexion()

        Public Sub New()
        End Sub

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

        'obtiene todos los períodos
        Public Function getAllPeriodos() As DataTable

            Dim MyResult As New DataSet
            Dim sql As String = "SELECT pag_codigo, pag_nombre, pag_anyo " &
                "FROM f_pla_pago ORDER BY pag_codigo DESC"
            Try
                MyResult = Conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)

        End Function


        ' crea el dataset para mostrar en el gridview
        Public Function getDataSetPagos() As DataSet

            Dim MyResult As New DataSet

            'Dim sql As String = " " + vbCr + _
            '    "SELECT dep.desc_departamento AS 'Departamento', mun.desc_municipio AS 'Municipio', ald.desc_aldea AS 'Aldea', " + vbCr + _
            '    "   tit.tit_nombre1 + ' ' + tit.tit_nombre2 + ' ' + tit.tit_apellido1 + ' ' + tit.tit_apellido2 AS 'Nombre Titular', " + vbCr + _
            '    "   tit.tit_identidad AS 'Identidad', tit.tit_pagina AS 'Página', tit.tit_linea AS 'Linea', " + vbCr + _
            '    "   tit.tit_monto_total AS 'Monto', pag.fecha_pago AS 'Fecha de Pago', ban.nombre_banco AS 'Banco', " + vbCr + _
            '    "   suc.desc_sucursal AS 'Sucursal', fon.nombre_fondo AS 'Fondo', tit.tit_codigo " + vbCr + _
            '    "FROM dbo.f_pla_titulares AS tit " + vbCr + _
            '    "   INNER JOIN dbo.f_pla_planilla AS pla ON pla.Pla_Numero = tit.tit_pla_numero " + vbCr + _
            '    "   INNER JOIN dbo.t_glo_aldeas AS ald ON ald.cod_aldea = pla.Ald_Codigo " + vbCr + _
            '    "   INNER JOIN dbo.t_glo_municipios AS mun ON mun.cod_municipio = ald.cod_municipio " + vbCr + _
            '    "   INNER JOIN dbo.t_glo_departamentos AS dep ON dep.cod_departamento = mun.cod_departamento " + vbCr + _
            '    "   INNER JOIN dbo.t_glo_fondos AS fon ON fon.cod_fondo = tit.tit_fondo " + vbCr + _
            '    "   INNER JOIN dbo.t_pln_fechas_pagos AS fchpag ON fchpag.cod_planilla = pla.Pla_Numero " + vbCr + _
            '    "   INNER JOIN dbo.t_glo_bancos AS ban ON ban.cod_banco = fchpag.cod_banco " + vbCr + _
            '    "   INNER JOIN dbo.t_pln_pagos_sucursales AS sucpag ON sucpag.cod_planilla = pla.Pla_Numero " + vbCr + _
            '    "   INNER JOIN dbo.t_glo_sucursales AS suc ON suc.cod_sucursal = sucpag.cod_sucursal " + vbCr + _
            '    "   LEFT JOIN dbo.t_cnt_pagos AS pag ON pag.cod_titular = tit.tit_codigo " + vbCr + _
            '    "WHERE tit.tit_pagina = -1 And tit.tit_linea = -1"

            Dim sql As String = " " + vbCr + _
                "SELECT dep.desc_departamento AS 'Departamento', mun.desc_municipio AS 'Municipio', ald.desc_aldea AS 'Aldea', " + vbCr + _
                "   tit.tit_nombre1 + ' ' + tit.tit_nombre2 + ' ' + tit.tit_apellido1 + ' ' + tit.tit_apellido2 AS 'Nombre Titular', " + vbCr + _
                "   tit.tit_identidad AS 'Identidad', tit.tit_pagina AS 'Página', tit.tit_linea AS 'Linea', " + vbCr + _
                "   tit.tit_monto_total AS 'Monto', pag.fecha_pago AS 'Fecha de Pago', ban.nombre_banco AS 'Banco', " + vbCr + _
                "   suc.desc_sucursal AS 'Sucursal', fon.fond_nombre AS 'Fondo', tit.tit_codigo " + vbCr + _
                "FROM dbo.f_pla_titulares AS tit " + vbCr + _
                "   INNER JOIN dbo.f_pla_planilla AS pla ON pla.Pla_Numero = tit.tit_pla_numero " + vbCr + _
                "   INNER JOIN dbo.t_glo_aldeas AS ald ON ald.cod_aldea = pla.Ald_Codigo " + vbCr + _
                "   INNER JOIN dbo.t_glo_municipios AS mun ON mun.cod_municipio = ald.cod_municipio " + vbCr + _
                "   INNER JOIN dbo.t_glo_departamentos AS dep ON dep.cod_departamento = mun.cod_departamento " + vbCr + _
                "   INNER JOIN dbo.f_pla_fondos AS fon ON fon.fond_codigo = tit.tit_fondo " + vbCr + _
                "   INNER JOIN dbo.t_pln_fechas_pagos AS fchpag ON fchpag.cod_planilla = pla.Pla_Numero " + vbCr + _
                "   INNER JOIN dbo.t_glo_bancos AS ban ON ban.cod_banco = fchpag.cod_banco " + vbCr + _
                "   INNER JOIN dbo.t_pln_pagos_sucursales AS sucpag ON sucpag.cod_planilla = pla.Pla_Numero " + vbCr + _
                "   INNER JOIN dbo.t_glo_sucursales AS suc ON suc.cod_sucursal = sucpag.cod_sucursal " + vbCr + _
                "   LEFT JOIN dbo.t_cnt_pagos AS pag ON pag.cod_titular = tit.tit_codigo " + vbCr + _
                "WHERE tit.tit_pagina = -1 And tit.tit_linea = -1"

            Try
                MyResult = Conexion.GetDataSet(sql)

                With MyResult.Tables(0).Columns
                    .Add("Nro", Type.GetType("System.String"))
                    .Item("Nro").SetOrdinal(0)
                    .Add("Estado", Type.GetType("System.String"))
                    .Item("Estado").SetOrdinal(1)
                    .Add("Página Archivo", Type.GetType("System.String"))
                    .Item("Página Archivo").SetOrdinal(2)
                    '.Add("Registro Archivo", Type.GetType("System.String"))
                    '.Item("Registro Archivo").SetOrdinal(3)
                    .Add("Linea Archivo", Type.GetType("System.String"))
                    .Item("Linea Archivo").SetOrdinal(3)
                    .Add("Monto Archivo", Type.GetType("System.String"))
                    .Item("Monto Archivo").SetOrdinal(4)
                    .Add("Fecha Archivo", Type.GetType("System.String"))
                    .Item("Fecha Archivo").SetOrdinal(5)
                    .Add("Estado Archivo", Type.GetType("System.String"))
                    .Item("Estado Archivo").SetOrdinal(6)
                End With

            Catch ex As Exception
            End Try

            Return MyResult
        End Function

        'inserta el registro de la precarga en la base de datos
        Public Function insertPreCarga(ByVal codPeriodo As String, ByVal codBanco As String, ByVal nombre As String, ByVal archivo() As Byte, ByVal nombreCarga As String, ByVal usuario As String)

            Dim cod As Integer
            Dim MiBase As New Conexion()
            Dim myConn As SqlConnection = Conexion.OpenConnection(MiBase.ConnectionString)

            Dim query As String = "INSERT INTO t_cnt_pre_cargas (cod_pago, cod_banco, nombre_archivo_pre_carga, data_archivo_pre_carga, inicio_pre_carga, nombre_pre_carga, cod_usuario) " & vbCr & vbLf &
                "VALUES (@cod_periodo, @cod_banco, @nombre_archivo, @data_archivo, GETDATE(), @nombre_pre_carga, @usuario)"

            Dim cmd As New SqlCommand(query, myConn)

            cmd.Parameters.AddWithValue("@cod_periodo", codPeriodo)
            cmd.Parameters.AddWithValue("@cod_banco", codBanco)
            cmd.Parameters.AddWithValue("@nombre_archivo", nombre)
            cmd.Parameters.AddWithValue("@nombre_pre_carga", nombreCarga)
            cmd.Parameters.AddWithValue("@usuario", usuario)

            Dim archivoParam As SqlParameter = cmd.Parameters.Add("@data_archivo", System.Data.SqlDbType.Image)
            archivoParam.Value = archivo

            cmd.ExecuteNonQuery()

            'retornar el ultimo identity insertado
            query = "SELECT @@IDENTITY"
            cmd.CommandText = query

            Try
                cod = cmd.ExecuteScalar()
                'Conexion.insertLog(HttpContext.Current.Session("usuario"), "Inicio la pre carga de un archivo de pago", "t_cnt_pre_cargas", 1, cod)
                insertLog(HttpContext.Current.Session("usuario"), "Inicio la pre carga de un archivo de pago", "t_cnt_pre_cargas", 1, cod)
            Catch ex As Exception

            End Try

            'Dim cod As Integer = cmd.ExecuteScalar()
            'Conexion.CloseConnection(myConn)
            Return cod

        End Function

        'ingresa el registro de la carga en la base de datos
        Public Function insertCarga(ByVal codPreCarga As String)

            Dim MyResult As Integer
            Dim sql As String
            'antes de que se agregara el usuario que ingreso el registro
            'sql = String.Format("INSERT INTO t_cnt_cargas (cod_pre_carga, inicio_carga)" + vbCr + _
            '                    "VALUES ({0}, GETDATE())", codPreCarga)
            sql = String.Format("INSERT INTO t_cnt_cargas (cod_pre_carga, inicio_carga, cod_usuario)" + vbCr + _
                                "VALUES ({0}, GETDATE(), {1})", codPreCarga, HttpContext.Current.Session("usuario"))
            Try
                MyResult = Conexion.insertRowAndReturnID(sql)
                'Conexion.insertLog(HttpContext.Current.Session("usuario"), "Inicio la carga de un archivo de pago", "t_cnt_cargas", 1, MyResult)s
                insertLog(HttpContext.Current.Session("usuario"), "Inicio la carga de un archivo de pago", "t_cnt_cargas", 1, MyResult)

            Catch ex As Exception
            End Try

            Return MyResult
        End Function

        'inserta el pago de la carga por medio de un procedimiento almacenado, almacenando tambien los registro en el historial de cargas
        Public Sub insertPagosCarga(ByVal codCarga As String, ByVal codRegistro As String, ByVal codTitular As String, ByVal fecha As String, ByVal banco As String, ByVal usuario As String, ByVal tipoFecha As String)

            Dim MiBase As New Conexion()
            Dim myConn As SqlConnection = Conexion.OpenConnection(MiBase.ConnectionString)

            Dim cmd As New SqlCommand("p_insertar_pago", myConn)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.AddWithValue("@codCarga", codCarga)
            cmd.Parameters.AddWithValue("@codRegistro", codRegistro)
            cmd.Parameters.AddWithValue("@codTitular", codTitular)

            If tipoFecha = "dd/mm/yyyy" Then
                cmd.Parameters.Add("@fecha", SqlDbType.DateTime).Value = fecha
            Else
                cmd.Parameters.AddWithValue("@fecha", fecha)
            End If

            cmd.Parameters.AddWithValue("@banco", banco)
            cmd.Parameters.AddWithValue("@usuario", usuario)
            cmd.Parameters.AddWithValue("@tipoFecha", tipoFecha)

            'cmd.ExecuteNonQuery()
            'Dim MyResult As Integer = cmd.ExecuteScalar()
            cmd.ExecuteNonQuery()
            Conexion.CloseConnection(myConn)
            'If Not MyResult > 0 Then
            '    Return False
            'End If

            'Return True

        End Sub

        'actualiza el registro de la precarga una vez finalizada
        Public Function actulizarFinPreCarga(ByVal codPreCarga As String)

            Dim MyResult As Integer
            Dim sql As String
            sql = "UPDATE t_cnt_pre_cargas SET fin_pre_carga = GETDATE() WHERE cod_pre_carga = " + codPreCarga
            Try
                MyResult = Conexion.updateTable(sql)

                'Conexion.insertLog(HttpContext.Current.Session("usuario"), "Finalizo la pre carga de un archivo de pago", "t_cnt_pre_cargas", 2, codPreCarga)
                insertLog(HttpContext.Current.Session("usuario"), "Finalizo la pre carga de un archivo de pago", "t_cnt_pre_cargas", 2, codPreCarga)

                If Not MyResult = 1 Then
                    Return False
                End If

            Catch ex As Exception
                Return False
            End Try
            Return True
        End Function

        'actuliza el registro de la carga una vez finalizada esta
        Public Function actulizarFinCarga(ByVal codCarga As String)

            Dim MyResult As Integer
            Dim sql As String
            sql = "UPDATE t_cnt_cargas SET fin_carga = GETDATE() WHERE cod_carga = " + codCarga
            Try
                MyResult = Conexion.updateTable(sql)

                'Conexion.insertLog(HttpContext.Current.Session("usuario"), "Finalizo la carga de un archivo de pagos", "t_cargas", 2, codCarga)
                insertLog(HttpContext.Current.Session("usuario"), "Finalizo la carga de un archivo de pagos", "t_cargas", 2, codCarga)

                If Not MyResult = 1 Then
                    Return False
                End If

            Catch ex As Exception
                Return False
            End Try
            Return True
        End Function

        Public Function getCargas(ByVal tipo As String, ByVal periodo As String, ByVal banco As String)

            Dim MyResult As DataSet
            Dim sql As String

            Select Case tipo
                Case "preCarga"
                    sql = "SELECT 'Precarga' AS 'Tipo', pre.cod_pre_carga AS codigo, pag.pag_nombre AS 'Periodo', ban.nombre_banco AS 'Banco', pre.cod_pre_carga as 'codigo'," + vbCr + _
                            "   pre.nombre_archivo_pre_carga AS 'Nombre Archivo', nombre_pre_carga AS 'Nombre Carga', per.nom1_usr_persona + ' ' + per.ape1_usr_persona AS 'Usuario'," + vbCr + _
                            "   pre.inicio_pre_carga AS 'Inicio de Carga', pre.fin_pre_carga AS 'Fin de Carga'" + vbCr + _
                            "FROM t_cnt_pre_cargas AS pre" + vbCr + _
                            "   INNER JOIN f_pla_pago AS pag ON pag.pag_codigo = pre.cod_pago " + vbCr + _
                            "   INNER JOIN t_glo_bancos AS ban ON ban.cod_banco = pre.cod_banco" + vbCr + _
                            "   INNER JOIN T_usuarios AS usu ON usu.cod_usuario = pre.cod_usuario" + vbCr + _
                            "   INNER JOIN t_usr_personas AS per ON per.cod_usr_persona = usu.cod_usr_persona" + vbCr + _
                            ""
                Case "carga"
                    sql = "SELECT 'Carga' AS 'Tipo', car.cod_carga AS codigo, pag.pag_nombre AS 'Periodo', ban.nombre_banco AS 'Banco', car.cod_carga AS 'codigo', " + vbCr + _
                            "   pre.nombre_archivo_pre_carga AS 'Nombre Archivo', nombre_pre_carga AS 'Nombre Carga', per.nom1_usr_persona + ' ' + per.ape1_usr_persona AS 'Usuario'," + vbCr + _
                            "   car.inicio_carga AS 'Inicio de Carga', car.fin_carga AS 'Fin de Carga'" + vbCr + _
                            "FROM t_cnt_cargas AS car " + vbCr + _
                            "   INNER JOIN t_cnt_pre_cargas AS pre ON pre.cod_pre_carga = car.cod_pre_carga" + vbCr + _
                            "   INNER JOIN f_pla_pago AS pag ON pag.pag_codigo = pre.cod_pago " + vbCr + _
                            "   INNER JOIN t_glo_bancos AS ban ON ban.cod_banco = pre.cod_banco" + vbCr + _
                            "   INNER JOIN T_usuarios AS usu ON usu.cod_usuario = pre.cod_usuario" + vbCr + _
                            "   INNER JOIN t_usr_personas AS per ON per.cod_usr_persona = usu.cod_usr_persona" + vbCr + _
                            ""
            End Select

            If periodo <> "" Then
                sql += "WHERE pag.pag_codigo = " + periodo

                If banco <> "" Then
                    sql += " AND ban.cod_banco = " + banco
                End If
            Else
                sql += "WHERE ban.cod_banco = " + banco
            End If

            Try
                MyResult = Conexion.GetDataSet(sql)
            Catch ex As Exception
                Return Nothing
            End Try

            Return MyResult.Tables(0)
        End Function

        Public Function getRegistrosPreCargas(ByVal codPreCarga As String) As DataTable

            Dim MiBase As New Conexion()
            Dim MyResult As New DataTable
            Dim myConn As SqlConnection = Conexion.OpenConnection(MiBase.ConnectionString)
            'Dim myDataAdapter As SqlDataAdapter = New SqlDataAdapter(strSQL, myConn)
            Dim myDataAdapter As SqlDataAdapter = New SqlDataAdapter()

            Dim cmd As New SqlCommand("p_obtener_pre_cargas", myConn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@codPrecarga", codPreCarga)

            myDataAdapter.SelectCommand = cmd
            myDataAdapter.SelectCommand.CommandTimeout = 10000
            myDataAdapter.Fill(MyResult)

            'cambiar cambiar nombre de las columna para introducirlas en el grid
            With MyResult
                .Columns("numero_linea_pre_carga").ColumnName = "Nro"
                .Columns("descripcion_estado_registro").ColumnName = "Estado"
                .Columns("pagina_registro_archivo").ColumnName = "Página Archivo"
                .Columns("linea_registro_archivo").ColumnName = "Linea Archivo"
                .Columns("monto_registro_archivo").ColumnName = "Monto Archivo"
                .Columns("fecha_registro_archivo").ColumnName = "Fecha Archivo"
                .Columns("estado_registro_archivo").ColumnName = "Estado Archivo"
                .Columns("desc_departamento").ColumnName = "Departamento"
                .Columns("desc_municipio").ColumnName = "Municipio"
                .Columns("desc_aldea").ColumnName = "Aldea"
                .Columns("nombre").ColumnName = "Nombre Titular"
                .Columns("tit_identidad").ColumnName = "Identidad"
                .Columns("tit_pagina").ColumnName = "Página"
                .Columns("tit_linea").ColumnName = "Linea"
                .Columns("tit_monto_total").ColumnName = "Monto"
                .Columns("fecha_pago").ColumnName = "Fecha de Pago"
                .Columns("nombre_banco").ColumnName = "Banco"
                .Columns("desc_sucursal").ColumnName = "Sucursal"
                '.Columns("nombre_fondo").ColumnName = "Fondo"
                .Columns("fond_nombre").ColumnName = "Fondo"
            End With


            Return MyResult
        End Function

        Public Function getRegistrosCargas(ByVal codCarga As String) As DataTable

            Dim MiBase As New Conexion()
            Dim MyResult As New DataTable
            Dim myConn As SqlConnection = Conexion.OpenConnection(MiBase.ConnectionString)
            'Dim myDataAdapter As SqlDataAdapter = New SqlDataAdapter(strSQL, myConn)
            Dim myDataAdapter As SqlDataAdapter = New SqlDataAdapter()

            Dim cmd As New SqlCommand("p_obtener_registro_carga", myConn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@codCarga", codCarga)

            myDataAdapter.SelectCommand = cmd
            myDataAdapter.Fill(MyResult)

            'cambiar cambiar nombre de las columna para introducirlas en el grid
            With MyResult
                .Columns("numero_linea_pre_carga").ColumnName = "Nro"
                .Columns("descripcion_estado_registro").ColumnName = "Estado"
                .Columns("pagina_registro_archivo").ColumnName = "Página Archivo"
                .Columns("linea_registro_archivo").ColumnName = "Linea Archivo"
                .Columns("monto_registro_archivo").ColumnName = "Monto Archivo"
                .Columns("fecha_registro_archivo").ColumnName = "Fecha Archivo"
                .Columns("estado_registro_archivo").ColumnName = "Estado Archivo"
                .Columns("desc_departamento").ColumnName = "Departamento"
                .Columns("desc_municipio").ColumnName = "Municipio"
                .Columns("desc_aldea").ColumnName = "Aldea"
                .Columns("nombre").ColumnName = "Nombre Titular"
                .Columns("tit_identidad").ColumnName = "Identidad"
                .Columns("tit_pagina").ColumnName = "Página"
                .Columns("tit_linea").ColumnName = "Linea"
                .Columns("tit_monto_total").ColumnName = "Monto"
                .Columns("fecha_pago").ColumnName = "Fecha de Pago"
                .Columns("nombre_banco").ColumnName = "Banco"
                .Columns("desc_sucursal").ColumnName = "Sucursal"
                '.Columns("nombre_fondo").ColumnName = "Fondo"
                .Columns("fond_nombre").ColumnName = "Fondo"
            End With

            Return MyResult
        End Function

        Public Function getArchivoPreCarga(ByVal codPreCarga As String)

            Dim MyResult As DataRow
            Dim sql As String
            'Dim data() As Byte

            sql = "SELECT nombre_archivo_pre_carga, data_archivo_pre_carga FROM t_cnt_pre_cargas WHERE cod_pre_carga = " + codPreCarga

            Try
                MyResult = Conexion.GetRow(sql)
                'MyResult = Conexion.returnScalar(sql)
                'data = CType(MyResult, Byte())
            Catch ex As Exception
                Return Nothing
            End Try
            'Return data
            Return MyResult
        End Function

        Public Function getCodPreCarga(ByVal codCarga As String)

            Dim MyResult As Integer
            Dim sql As String

            sql = "SELECT cod_pre_carga FROM t_cnt_cargas WHERE cod_carga = " + codCarga

            Try
                MyResult = Conexion.getInt(sql)
            Catch ex As Exception
            End Try

            Return MyResult

        End Function

        'llamado a procedimiento añadido para verificar pagos
        Public Sub verificarPago(ByVal num As String, ByVal pag As String, ByVal reg As String, ByVal monto As String, ByVal fecha As String, ByVal estado As String, ByVal codPreCarga As Integer, ByVal tipoFecha As String)

            Dim MiBase As New Conexion()
            Dim MyResult As New DataSet
            Dim myConn As SqlConnection = Conexion.OpenConnection(MiBase.ConnectionString)
            'Dim myDataAdapter As SqlDataAdapter = New SqlDataAdapter(strSQL, myConn)
            'Dim myDataAdapter As SqlDataAdapter = New SqlDataAdapter()

            Dim cmd As New SqlCommand("p_verificar_pago", myConn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@num_linea", num)
            cmd.Parameters.AddWithValue("@pagina", pag)
            cmd.Parameters.AddWithValue("@linea", reg)
            cmd.Parameters.AddWithValue("@monto", monto)
            cmd.Parameters.AddWithValue("@fecha", fecha)
            cmd.Parameters.AddWithValue("@estado", estado)
            cmd.Parameters.AddWithValue("@pre_carga", codPreCarga)
            cmd.Parameters.AddWithValue("@tipo_fecha", tipoFecha)

            cmd.ExecuteNonQuery()

            'myDataAdapter.SelectCommand = cmd
            'myDataAdapter.Fill(MyResult)
            Conexion.CloseConnection(myConn)
            'Return MyResult

        End Sub

        Public Function getContadorEstadosPreCarga(ByVal codPreCarga As String)

            Dim MyResult As DataSet
            Dim sql As String = "" + vbCr + _
                "SELECT reg_est.descripcion_estado_registro, COUNT(reg_pre.cod_registro_archivo) " + vbCr + _
                "FROM t_cnt_pre_cargas AS pre " + vbCr + _
                "	INNER JOIN t_cnt_registros_pre_cargas AS reg_pre ON reg_pre.cod_pre_carga = pre.cod_pre_carga " + vbCr + _
                "	INNER JOIN t_cnt_estados_reg_pre_cargas AS reg_est ON reg_est.cod_estado_registro_pre_carga = reg_pre.cod_estado_registro_pre_carga " + vbCr + _
                "WHERE pre.cod_pre_carga = " + codPreCarga + vbCr + _
                "GROUP BY reg_est.descripcion_estado_registro"

            Try
                MyResult = Conexion.GetDataSet(sql)
                Return MyResult.Tables(0)
            Catch ex As Exception
                Return Nothing
            End Try

        End Function

        Public Sub insertLog(ByVal usuario As String, ByVal operacion As String, ByVal tabla As String, ByVal suceso As String, ByVal registro As String)
            Conexion.insertLog(usuario, operacion, tabla, suceso, registro)
        End Sub

        Public Function getDetalleRegistrosPreCarga(ByVal precarga As String, ByVal cargado As String, ByVal permitirFueraProgramacion As String) As DataTable

            Dim MyResult As DataSet
            Dim sql As String = "" + vbCr + _
                "SELECT	reg_pre.numero_linea_pre_carga AS 'Nro',"

            If cargado = 1 And permitirFueraProgramacion = 0 Then
                sql += "" + vbCr + _
                    "       CASE WHEN est.cod_estado_registro_pre_carga IN (11, 12) THEN 'Registro guardado correctamente' ELSE est.descripcion_estado_registro END AS 'Estado',"
            ElseIf cargado = 1 And permitirFueraProgramacion = 1 Then
                sql += "" + vbCr + _
                    "       CASE WHEN est.cod_estado_registro_pre_carga = 11 THEN 'Registro guardado correctamente' ELSE est.descripcion_estado_registro END AS 'Estado',"
            ElseIf cargado = 0 Then
                sql += "" + vbCr + _
                    "       est.descripcion_estado_registro AS 'Estado',"
            End If

            sql += "" + vbCr + _
                "		reg.pagina_registro_archivo AS 'Página Archivo'," + vbCr + _
                "		reg.linea_registro_archivo AS 'Linea Archivo'," + vbCr + _
                "		reg.monto_registro_archivo AS 'Monto Archivo'," + vbCr + _
                "		reg.fecha_registro_archivo AS 'Fecha Archivo'," + vbCr + _
                "		reg.estado_registro_archivo AS 'Estado Archivo'," + vbCr + _
                "		geo.desc_departamento AS 'Departamento'," + vbCr + _
                "		geo.desc_municipio AS 'Municipio'," + vbCr + _
                "		geo.desc_aldea AS 'Aldea'," + vbCr + _
                "		fon.fond_nombre AS 'Fondo'," + vbCr + _
                "		ban.nombre_banco AS 'Banco'," + vbCr + _
                "		suc.desc_sucursal AS 'Sucursal'," + vbCr + _
                "		tit.tit_nombre1 + ' ' + tit.tit_nombre2 + ' ' + tit.tit_apellido1 + ' ' + tit.tit_apellido2 AS 'Nombre Titular'," + vbCr + _
                "       tit.tit_identidad AS 'Identidad'," + vbCr + _
                "       tit.tit_pagina AS 'Página', tit.tit_linea AS 'Linea', tit.tit_monto_total AS 'Monto', tit.tit_fecha_cobro AS 'Fecha de Pago'" + vbCr + _
                "	FROM SIG_T.dbo.t_cnt_registros_pre_cargas AS reg_pre" + vbCr + _
                "		INNER JOIN SIG_T.dbo.t_cnt_registros_archivos AS reg ON reg.cod_registro_archivo = reg_pre.cod_registro_archivo" + vbCr + _
                "		INNER JOIN SIG_T.dbo.t_cnt_estados_reg_pre_cargas AS est ON est.cod_estado_registro_pre_carga = reg_pre.cod_estado_registro_pre_carga" + vbCr + _
                "		INNER JOIN SIG_T.dbo.f_pla_titulares AS tit	ON tit.tit_pagina = reg.pagina_registro_archivo AND tit.tit_linea = reg.linea_registro_archivo" + vbCr + _
                "		INNER JOIN SIG_T.dbo.V_Glo_caserios AS geo ON geo.cod_caserio = tit.tit_cas_codigo" + vbCr + _
                "		INNER JOIN SIG_T.dbo.f_pla_fondos AS fon ON fon.fond_codigo = tit.tit_fondo" + vbCr + _
                "		INNER JOIN SIG_T.dbo.t_pln_fechas_pagos AS fchpag ON fchpag.cod_planilla = tit.tit_pla_numero" + vbCr + _
                "		INNER JOIN SIG_T.dbo.t_glo_bancos AS ban ON ban.cod_banco = fchpag.cod_banco" + vbCr + _
                "		INNER JOIN SIG_T.dbo.t_pln_pagos_sucursales AS sucpag ON sucpag.cod_planilla = tit.tit_pla_numero" + vbCr + _
                "		INNER JOIN SIG_T.dbo.t_glo_sucursales AS suc ON suc.cod_sucursal = sucpag.cod_sucursal" + vbCr + _
                "	WHERE cod_pre_carga = " + precarga + vbCr + _
                "	ORDER BY numero_linea_pre_carga"

            Try
                MyResult = Conexion.GetDataSet(sql)
                Return MyResult.Tables(0)
            Catch ex As Exception
                Return Nothing
            End Try

        End Function

        Public Function getTotalEstadosMontos(ByVal precarga As String) As DataTable

            Dim MyResult As DataSet
            Dim sql As String = "" + vbCr + _
                "SELECT	COUNT(*) AS 'totalRegistros'," + vbCr + _
                "		COUNT(CASE WHEN reg_pre.cod_estado_registro_pre_carga = 11 AND reg.estado_registro_archivo = 1 THEN 1 ELSE NULL END) AS 'totalPagados'," + vbCr + _
                "		COUNT(CASE WHEN reg_pre.cod_estado_registro_pre_carga = 15 AND reg.estado_registro_archivo = 0 THEN 1 ELSE NULL END) AS 'totalNoPagados'," + vbCr + _
                "		COUNT(CASE WHEN reg_pre.cod_estado_registro_pre_carga = 12 AND reg.estado_registro_archivo = 1 THEN 1 ELSE NULL END) AS 'totalFueraProgramacion'," + vbCr + _
                "		SUM(CAST(reg.monto_registro_archivo AS INT)) AS 'montoTotal'," + vbCr + _
                "		SUM(CASE WHEN reg_pre.cod_estado_registro_pre_carga = 11 AND reg.estado_registro_archivo = 1 THEN CAST(reg.monto_registro_archivo AS INT) ELSE 0 END) AS 'montoPagado'," + vbCr + _
                "		SUM(CASE WHEN reg_pre.cod_estado_registro_pre_carga = 15 AND reg.estado_registro_archivo = 0 THEN CAST(reg.monto_registro_archivo AS INT) ELSE 0 END) AS 'montoNoPagados'," + vbCr + _
                "		SUM(CASE WHEN reg_pre.cod_estado_registro_pre_carga = 12 AND reg.estado_registro_archivo = 1 THEN CAST(reg.monto_registro_archivo AS INT) ELSE 0 END) AS 'montoFueraProgramacion'" + vbCr + _
                "	FROM SIG_T.dbo.t_cnt_registros_pre_cargas AS reg_pre" + vbCr + _
                "		INNER JOIN SIG_T.dbo.t_cnt_registros_archivos AS reg ON reg.cod_registro_archivo = reg_pre.cod_registro_archivo" + vbCr + _
                "   WHERE cod_pre_carga = " + precarga

            Try
                MyResult = Conexion.GetDataSet(sql)
                Return MyResult.Tables(0)
            Catch ex As Exception
                Return Nothing
            End Try

        End Function

        Public Function getRegistrosParaGuardar(ByVal precarga As String) As DataTable

            Dim MyResult As DataSet
            Dim sql As String = "" + vbCr + _
                "SELECT tit.tit_codigo, est.descripcion_estado_registro, reg_pre.numero_linea_pre_carga," + vbCr + _
                "		reg.pagina_registro_archivo, reg.linea_registro_archivo, reg.monto_registro_archivo," + vbCr + _
                "		reg.fecha_registro_archivo," + vbCr + _
                "		CASE" + vbCr + _
                "			WHEN reg_pre.cod_estado_registro_pre_carga = 11 THEN 1" + vbCr + _
                "			WHEN reg_pre.cod_estado_registro_pre_carga = 12 THEN 2" + vbCr + _
                "			ELSE 0" + vbCr + _
                "		END AS 'guardar'" + vbCr + _
                "	FROM SIG_T.dbo.t_cnt_registros_pre_cargas AS reg_pre" + vbCr + _
                "		INNER JOIN SIG_T.dbo.t_cnt_registros_archivos AS reg ON reg.cod_registro_archivo = reg_pre.cod_registro_archivo" + vbCr + _
                "		INNER JOIN SIG_T.dbo.t_cnt_estados_reg_pre_cargas AS est ON est.cod_estado_registro_pre_carga = reg_pre.cod_estado_registro_pre_carga" + vbCr + _
                "		INNER JOIN SIG_T.dbo.f_pla_titulares AS tit	ON tit.tit_pagina = reg.pagina_registro_archivo AND tit.tit_linea = reg.linea_registro_archivo" + vbCr + _
                "	WHERE reg_pre.cod_estado_registro_pre_carga IN (11, 12) AND cod_pre_carga = " + precarga + vbCr + _
                "   ORDER BY reg_pre.numero_linea_pre_carga"

            Try
                MyResult = Conexion.GetDataSet(sql)
                Return MyResult.Tables(0)
            Catch ex As Exception
                Return Nothing
            End Try

        End Function

        Public Sub preCargarArchivo(ByVal path As String, ByVal codPrecarga As Integer, ByVal tipoFecha As String)

            Dim MiBase As New Conexion()
            Dim MyResult As New DataSet
            Dim myConn As SqlConnection = Conexion.OpenConnection(MiBase.ConnectionString)

            Dim cmd As New SqlCommand("p_cnt_precargar_archivo", myConn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@path", path)
            cmd.Parameters.AddWithValue("@cod_precarga", codPrecarga)
            cmd.Parameters.AddWithValue("@tipo_fecha", tipoFecha)

            cmd.CommandTimeout = 6000
            Try
                cmd.ExecuteNonQuery()
                Conexion.CloseConnection(myConn)
            Catch ex As Exception
                Conexion.CloseConnection(myConn)
            End Try
        End Sub

#Region "Funciones para la carga de archivos de bancarización"

        Public Function fnc_obtener_archivos_bancarizacion(ByVal periodo As String, ByVal banco As String) As DataTable

            Dim MyResult As DataSet
            Dim sql As String = "" + vbCr +
                "SELECT arc.cod_arch_bancarizacion, pag.pag_nombre, ban.nombre_banco, tarc.desc_tipo_arch_banc, arc.nombre_arch_bancarizacion, est.desc_estado_arch_bancarizacion, arc.fecha_registro" + vbCr +
                "	FROM SIG_T.dbo.t_cnt_archivos_bancarizacion AS arc" + vbCr +
                "		LEFT JOIN SIG_T.dbo.f_pla_pago AS pag ON pag.pag_codigo = arc.cod_pago" + vbCr +
                "		LEFT JOIN SIG_T.dbo.t_glo_bancos AS ban ON ban.cod_banco = arc.cod_banco" + vbCr +
                "		LEFT JOIN SIG_T.dbo.t_cnt_tipos_arch_bancarizacion AS tarc ON tarc.cod_tipo_arch_banc = arc.cod_tipo_arch_banc" + vbCr +
                "		LEFT JOIN SIG_T.dbo.t_cnt_est_arch_bancarizacion AS est ON est.cod_estado_arch_bancarizacion = arc.cod_estado_arch_bancarizacion"

            If periodo <> "" Then
                sql += "    WHERE arc.ultimo_arch_bancarizacion = 1 AND pag.pag_codigo = " + periodo

                If banco <> "" Then
                    sql += " AND ban.cod_banco = " + banco
                End If
                'Else
                '    sql += "    WHERE ban.cod_banco = " + banco
            End If

            Try
                MyResult = Conexion.GetDataSet(sql)
                Return MyResult.Tables(0)
            Catch ex As Exception
                Return Nothing
            End Try

        End Function

        Public Function fnc_obtener_consolidado_carga_bancarizacion(ByVal pago As String, ByVal banco As String, ByVal archivo As String) As DataTable

            'en where devuelve el código del registro de cierre de detalle de pagos, en la ventana final debo distinguir
            'si desean cargar la confirmación de detalle de pago, esto para cargar los pagos de las cuentas que ya estaban aperturadas
            Dim MyResult As DataSet
            Dim sql As String = "" + vbCr +
                "SELECT fon.fond_nombre, pag.Nombre_Pagador, geo.desc_departamento, desc_municipio," + vbCr +
                "		COUNT(*) AS 'hogares_programados'," + vbCr +
                "		SUM(tit.tit_monto_total) AS 'monto_programado'," + vbCr +
                "		COUNT(CASE WHEN BAN.pagado = 1 THEN 1 ELSE NULL END) AS 'hogares_pagados'," + vbCr +
                "		SUM(CASE WHEN BAN.pagado = 1 THEN tit.tit_monto_total ELSE 0 END) AS 'monto_pagado'," + vbCr +
                "		COUNT(CASE WHEN BAN.pagado = 0 THEN 1 ELSE NULL END) AS 'hogares_no_pagados'," + vbCr +
                "		SUM(CASE WHEN BAN.pagado = 0 THEN tit.tit_monto_total ELSE 0 END) AS 'monto_no_pagado'" + vbCr +
                "	FROM SIG_T.dbo.f_pla_titulares AS tit" + vbCr +
                "		INNER JOIN SIG_T.dbo.f_pla_esquema AS esq ON esq.esq_codigo = tit.tit_esquema" + vbCr +
                "		INNER JOIN SIG_T.dbo.f_pla_pagadores AS pag ON pag.Codigo_Pagador = tit.tit_pagador" + vbCr +
                "       INNER JOIN SIG_T.dbo.f_pla_fondos AS fon ON fon.fond_codigo = tit.tit_fondo" + vbCr +
                "		INNER JOIN SIG_T.dbo.V_Glo_caserios AS geo ON geo.cod_caserio = tit.tit_cas_codigo" + vbCr +
                "		LEFT JOIN (" + vbCr +
                "			SELECT DISTINCT referencia," + vbCr +
                "					CASE WHEN det.cod_estado_registro_pre_carga = 11 THEN 1 ELSE 0 END AS 'pagado'" + vbCr +
                "				FROM SIG_T.dbo.t_cnt_archivos_bancarizacion AS arc" + vbCr +
                "					INNER JOIN SIG_T.dbo.t_cnt_arch_banc_reg_detalle_p AS det ON det.cod_arch_bancarizacion = arc.cod_arch_bancarizacion" + vbCr +
                "					INNER JOIN SIG_T.dbo.t_cnt_registros_arch_detalle_p AS reg ON reg.cod_registro_detalle_pagos = det.cod_registro_detalle_pagos" + vbCr +
                "				WHERE arc.ultimo_arch_bancarizacion = 1 AND arc.cod_arch_bancarizacion = " + archivo + vbCr +
                "		) BAN ON BAN.referencia = tit.tit_referencia" + vbCr +
                "	WHERE esq.esq_tipo_esquema = 1 AND tit_planilla = 1" + vbCr +
                "		AND esq.pag_codigo = " + pago + " AND pag.cod_banco = " + banco + vbCr +
                "	GROUP BY fon.fond_nombre, pag.Nombre_Pagador, cod_departamento, desc_departamento, cod_municipio, desc_municipio" + vbCr +
                "	ORDER BY fon.fond_nombre, pag.Nombre_Pagador, cod_departamento, cod_municipio"

            Try
                MyResult = Conexion.GetDataSet(sql)
                Return MyResult.Tables(0)
            Catch ex As Exception
                Return Nothing
            End Try

        End Function

        Function fnc_cargar_pagos_bancarizacion(ByVal pago As String, ByVal banco As String, ByVal archivo As String) As Integer

            Dim MiBase As New Conexion()
            Dim MyResult As Integer
            Dim myConn As SqlConnection = Conexion.OpenConnection(MiBase.ConnectionString)

            Dim cmd As New SqlCommand("p_cnt_cargar_pagos_bancarizacion", myConn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@cod_pago", pago)
            cmd.Parameters.AddWithValue("@cod_banco", banco)
            cmd.Parameters.AddWithValue("@cod_archivo", archivo)

            cmd.CommandTimeout = 6000
            Try
                MyResult = cmd.ExecuteScalar()
                Conexion.CloseConnection(myConn)
            Catch ex As Exception
                MyResult = 0
                Conexion.CloseConnection(myConn)
            End Try

            Return MyResult
        End Function

        Public Function fnc_obtener_nombre_banco(ByVal banco As String) As String

            Dim MyResult As String
            Dim sql As String = "" + vbCr +
                "SELECT nombre_banco" + vbCr +
                "   FROM SIG_T.dbo.t_glo_bancos" + vbCr +
                "   WHERE cod_banco = " + banco

            Try
                MyResult = Conexion.getString(sql)
                Return MyResult
            Catch ex As Exception
                Return Nothing
            End Try

        End Function

        Public Function fnc_obtener_nombre_archivo(ByVal archivo As String) As String

            Dim MyResult As String
            Dim sql As String = "" + vbCr +
                "SELECT nombre_arch_bancarizacion" + vbCr +
                "   FROM SIG_T.dbo.t_cnt_archivos_bancarizacion" + vbCr +
                "   WHERE cod_arch_bancarizacion = " + archivo

            Try
                MyResult = Conexion.getString(sql)
                Return MyResult
            Catch ex As Exception
                Return Nothing
            End Try

        End Function

        Public Function fnc_obtener_nombre_pago(ByVal pago As String) As String

            Dim MyResult As String
            Dim sql As String = "" + vbCr +
                "SELECT pag_nombre" + vbCr +
                "   FROM SIG_T.dbo.f_pla_pago" + vbCr +
                "   WHERE pag_codigo = " + pago

            Try
                MyResult = Conexion.getString(sql)
                Return MyResult
            Catch ex As Exception
                Return Nothing
            End Try

        End Function

        Public Function fnc_obtener_total_registros_arch_bancarizacion(ByVal archivo As String) As Integer

            Dim MyResult As String
            Dim sql As String = "" + vbCr +
                "SELECT total_registros" + vbCr +
                "	FROM (" + vbCr +
                "		SELECT COUNT(*) AS 'total_registros'" + vbCr +
                "			FROM SIG_T.dbo.t_cnt_arch_banc_reg_apertura" + vbCr +
                "			WHERE cod_arch_bancarizacion = " + archivo + vbCr +
                "		UNION" + vbCr +
                "		SELECT COUNT(*) AS 'total_registros'" + vbCr +
                "			FROM SIG_T.dbo.t_cnt_arch_banc_reg_detalle_p" + vbCr +
                "			WHERE cod_arch_bancarizacion = " + archivo + vbCr +
                "	) TMP" + vbCr +
                "	WHERE total_registros > 0"

            Try
                MyResult = Conexion.getInt(sql)
                Return MyResult
            Catch ex As Exception
                Return Nothing
            End Try

        End Function

        Public Function fnc_contador_est_reg_bancarizacion(ByVal archivo As String) As DataTable

            Dim MyResult As DataSet
            Dim sql As String = "" + vbCr +
                "SELECT desc_estado, total_registros" + vbCr +
                "	FROM (" + vbCr +
                "		SELECT est.descripcion_estado_registro AS 'desc_estado', COUNT(*) AS 'total_registros'" + vbCr +
                "			FROM SIG_T.dbo.t_cnt_arch_banc_reg_apertura AS reg_ape" + vbCr +
                "				INNER JOIN SIG_T.dbo.t_cnt_estados_reg_pre_cargas AS est ON est.cod_estado_registro_pre_carga = reg_ape.cod_estado_registro_pre_carga" + vbCr +
                "			WHERE reg_ape.cod_arch_bancarizacion = " + archivo + vbCr +
                "			GROUP BY est.descripcion_estado_registro" + vbCr +
                "		UNION" + vbCr +
                "		SELECT est.descripcion_estado_registro AS 'desc_estado', COUNT(*) AS 'total_registros'" + vbCr +
                "			FROM SIG_T.dbo.t_cnt_arch_banc_reg_detalle_p AS reg_det" + vbCr +
                "				INNER JOIN SIG_T.dbo.t_cnt_estados_reg_pre_cargas AS est ON est.cod_estado_registro_pre_carga = reg_det.cod_estado_registro_pre_carga" + vbCr +
                "			WHERE reg_det.cod_arch_bancarizacion = " + archivo + vbCr +
                "			GROUP BY est.descripcion_estado_registro" + vbCr +
                "	) AS TMP" + vbCr +
                "	WHERE total_registros > 0"

            Try
                MyResult = Conexion.GetDataSet(sql)
                Return MyResult.Tables(0)
            Catch ex As Exception
                Return Nothing
            End Try

        End Function

        Public Function fnc_obtener_datos_archivo(ByVal archivo As String) As DataTable

            Dim MyResult As DataSet
            Dim sql As String = "" + vbCr +
                "SELECT pago.pag_nombre, ban.nombre_banco, tipo.desc_tipo_arch_banc, est.desc_estado_arch_bancarizacion, arc.nombre_arch_bancarizacion, arc.fecha_registro" + vbCr +
                "	FROM SIG_T.dbo.t_cnt_archivos_bancarizacion AS arc" + vbCr +
                "		LEFT JOIN SIG_T.dbo.t_cnt_est_arch_bancarizacion AS est ON est.cod_estado_arch_bancarizacion = arc.cod_estado_arch_bancarizacion" + vbCr +
                "		LEFT JOIN SIG_T.dbo.t_cnt_tipos_arch_bancarizacion AS tipo ON tipo.cod_tipo_arch_banc = arc.cod_tipo_arch_banc" + vbCr +
                "		LEFT JOIN SIG_T.dbo.f_pla_pago AS pago ON pago.pag_codigo = arc.cod_pago" + vbCr +
                "		LEFT JOIN SIG_T.dbo.t_glo_bancos AS ban ON ban.cod_banco = arc.cod_banco" + vbCr +
                "	WHERE cod_arch_bancarizacion = " + archivo

            Try
                MyResult = Conexion.GetDataSet(sql)
                Return MyResult.Tables(0)
            Catch ex As Exception
                Return Nothing
            End Try

        End Function

        Public Function fnc_verificar_arch_detalle_pago(ByVal archivo As String) As Boolean

            Dim MyResult As String
            Dim sql As String = "" + vbCr +
                "SELECT CASE WHEN cod_tipo_arch_banc IN (2,5) THEN 1 ELSE 0 END" + vbCr +
                "	FROM SIG_T.dbo.t_cnt_archivos_bancarizacion" + vbCr +
                "	WHERE cod_arch_bancarizacion = " + archivo

            Try
                MyResult = Conexion.returnScalar(sql)

                If MyResult = 1 Then
                    Return True
                Else
                    Return False
                End If

            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Public Function fnc_obtener_detalle_registro_archivo(ByVal archivo As String) As DataTable

            Dim MyResult As DataSet
            Dim sql As String

            If fnc_verificar_arch_detalle_pago(archivo) Then

                sql = "" + vbCr +
                    "SELECT	est.descripcion_estado_registro AS 'Estado'," + vbCr +
                    "		arc.numero_linea AS 'Número Linea'," + vbCr +
                    "		reg.cod_archivo AS 'Código Archivo'," + vbCr +
                    "		reg.tipo_bono AS 'Tipo Bono'," + vbCr +
                    "		reg.cod_persona AS 'Código Persona'," + vbCr +
                    "		reg.identidad_persona AS 'Identidad'," + vbCr +
                    "		reg.nombre_fondo AS 'Fondo'," + vbCr +
                    "		reg.nombre_componente AS 'Componente'," + vbCr +
                    "		reg.cod_esquema AS 'Esquema'," + vbCr +
                    "		reg.referencia AS 'Referencia'," + vbCr +
                    "		reg.total_pago AS 'Total Pago'," + vbCr +
                    "		ISNULL(reg.numero_gestion,'') AS 'Número Gestión'," + vbCr +
                    "		ISNULL(reg.usuario_gestion,'') AS 'Usuario Gestión'," + vbCr +
                    "		ISNULL(reg.fecha_gestion,'') AS 'Fecha Gestión'," + vbCr +
                    "		ISNULL(reg.usuario_aprueba_gestion,'') AS 'Usuario Aprueba'," + vbCr +
                    "		ISNULL(reg.codigo_error,'') AS 'Código Error'" + vbCr +
                    "	FROM SIG_T.dbo.t_cnt_arch_banc_reg_detalle_p AS arc" + vbCr +
                    "		INNER JOIN SIG_T.dbo.t_cnt_registros_arch_detalle_p AS reg ON reg.cod_registro_detalle_pagos = arc.cod_registro_detalle_pagos" + vbCr +
                    "		INNER JOIN SIG_T.dbo.t_cnt_estados_reg_pre_cargas AS est ON est.cod_estado_registro_pre_carga = arc.cod_estado_registro_pre_carga" + vbCr +
                    "	WHERE cod_arch_bancarizacion = " + archivo + vbCr +
                    "   ORDER BY [Número Linea]"

            Else

                sql = "" + vbCr +
                    "SELECT	est.descripcion_estado_registro AS 'Estado'," + vbCr +
                    "       arc.numero_linea AS 'Número Linea'," + vbCr +
                    "		reg.cod_archivo AS 'Código Archivo'," + vbCr +
                    "		reg.cod_departamento AS 'Código Departamento'," + vbCr +
                    "		reg.desc_departamento AS 'Departamento'," + vbCr +
                    "		reg.cod_municipio AS 'Código Municipio'," + vbCr +
                    "		reg.desc_municipio AS 'Municipio'," + vbCr +
                    "		reg.cod_aldea AS 'Código Aldea'," + vbCr +
                    "		reg.desc_aldea AS 'Aldea'," + vbCr +
                    "		reg.cod_caserio AS 'Código Caserío'," + vbCr +
                    "		reg.desc_caserio AS 'Caserío'," + vbCr +
                    "		reg.cod_hogar AS 'Código Hogar'," + vbCr +
                    "		reg.cod_persona AS 'Código Persona'," + vbCr +
                    "		ISNULL(reg.primer_nombre,'') AS 'Primer Nombre'," + vbCr +
                    "		ISNULL(reg.segundo_nombre,'') AS 'Segundo Nombre'," + vbCr +
                    "		ISNULL(reg.primer_apellido,'') AS 'Primer Apellido'," + vbCr +
                    "		ISNULL(reg.segundo_apellido,'') AS 'Segundo Apellido'," + vbCr +
                    "		reg.proceso AS 'Proceso'," + vbCr +
                    "		ISNULL(reg.numero_gestion,'') AS 'Número Gestión'," + vbCr +
                    "		ISNULL(reg.usuario_gestion,'') AS 'Usuario Gestión'," + vbCr +
                    "		ISNULL(reg.fecha_gestion,'') AS 'Fecha Gestión'," + vbCr +
                    "		ISNULL(reg.usuario_aprueba_gestion,'') AS 'Usuario Aprueba'," + vbCr +
                    "		ISNULL(reg.codigo_error,'') AS 'Código Error'" + vbCr +
                    "	FROM SIG_T.dbo.t_cnt_arch_banc_reg_apertura AS arc" + vbCr +
                    "		INNER JOIN SIG_T.dbo.t_cnt_registros_arch_apertura AS reg ON reg.cod_registro_apertura = arc.cod_registro_apertura" + vbCr +
                    "		INNER JOIN SIG_T.dbo.t_cnt_estados_reg_pre_cargas AS est ON est.cod_estado_registro_pre_carga = arc.cod_estado_registro_pre_carga" + vbCr +
                    "	WHERE cod_arch_bancarizacion = " + archivo + vbCr +
                    "   ORDER BY [Número Linea]"

            End If

            Try
                MyResult = Conexion.GetDataSet(sql)
                Return MyResult.Tables(0)
            Catch ex As Exception
                Return Nothing
            End Try

        End Function

        Public Function fnc_obtener_listado_titulares_pagados(ByVal pago As String, ByVal banco As String, ByVal archivo As String) As DataTable

            Dim MyResult As DataSet
            Dim sql As String = "" + vbCr +
                "SELECT desc_departamento AS 'Departamento'," + vbCr +
                "	    desc_municipio As 'Municipio'," + vbCr +
                "       desc_aldea AS 'Aldea'," + vbCr +
                "		desc_caserio AS 'Caserío'," + vbCr +
                "		CONVERT(NVARCHAR(10),tit.tit_persona) AS 'Código Persona'," + vbCr +
                "		tit_identidad AS 'Identidad Titular'," + vbCr +
                "		tit_nombre1 + ' ' + tit_nombre2 + ' ' + tit_apellido1 + ' ' + tit_apellido2 AS 'Nombre Titular'," + vbCr +
                "		fon.fond_nombre AS 'Fondo'," + vbCr +
                "		CASE" + vbCr +
                "			WHEN tit_detalle_bonos = 'BONO: Educación y Salud' THEN 'Educación y Salud'" + vbCr +
                "			WHEN tit_detalle_bonos = 'BONO: Salud' THEN 'Salud'" + vbCr +
                "			WHEN tit_detalle_bonos = 'BONO: Educación' THEN 'Educación'" + vbCr +
                "			ELSE tit_acumulado_det" + vbCr +
                "		END AS 'Componente'," + vbCr +
                "		tit_referencia AS 'Referencia'," + vbCr +
                "		tit_monto_total AS 'Monto'," + vbCr +
                "		BAN.pagado AS 'Pagado'," + vbCr +
                "		SUBSTRING(fecha_gestion,1,10) AS 'Fecha Pago'" + vbCr +
                "	FROM SIG_T.dbo.f_pla_titulares AS tit" + vbCr +
                "		INNER JOIN SIG_T.dbo.f_pla_esquema AS esq ON esq.esq_codigo = tit.tit_esquema" + vbCr +
                "		INNER JOIN SIG_T.dbo.f_pla_pagadores AS pag ON pag.Codigo_Pagador = tit.tit_pagador" + vbCr +
                "       INNER JOIN SIG_T.dbo.f_pla_fondos AS fon ON fon.fond_codigo = tit.tit_fondo" + vbCr +
                "		INNER JOIN SIG_T.dbo.V_Glo_caserios AS geo ON geo.cod_caserio = tit.tit_cas_codigo" + vbCr +
                "		LEFT JOIN (" + vbCr +
                "			SELECT  DISTINCT referencia" + vbCr +
                "					,CASE WHEN det.cod_estado_registro_pre_carga = 11 THEN 1 ELSE 0 END AS 'pagado'" + vbCr +
                "					,fecha_gestion" + vbCr +
                "				FROM SIG_T.dbo.t_cnt_archivos_bancarizacion AS arc" + vbCr +
                "					INNER JOIN SIG_T.dbo.t_cnt_arch_banc_reg_detalle_p AS det ON det.cod_arch_bancarizacion = arc.cod_arch_bancarizacion" + vbCr +
                "					INNER JOIN SIG_T.dbo.t_cnt_registros_arch_detalle_p AS reg ON reg.cod_registro_detalle_pagos = det.cod_registro_detalle_pagos" + vbCr +
                "				WHERE  arc.cod_arch_bancarizacion = " + archivo + vbCr +
                "		) BAN ON BAN.referencia = tit.tit_referencia" + vbCr +
                "	WHERE esq.esq_tipo_esquema = 1 AND tit_planilla = 1" + vbCr +
                "		AND esq.pag_codigo = " + pago + " AND pag.cod_banco = " + banco + vbCr +
                "	ORDER BY fon.fond_nombre, pag.Nombre_Pagador, cod_departamento, cod_municipio"

            Try
                MyResult = Conexion.GetDataSet(sql)
                Return MyResult.Tables(0)
            Catch ex As Exception
                Return Nothing
            End Try

        End Function

#End Region

#Region "Funciones para la verificación"

        Public Function fnc_obtener_tipo_archivo(ByVal tipo As String, ByVal archivo As String) As Integer

            Dim sql As String

            If archivo = "PRE" Then
                sql = "" + vbCr +
                    "SELECT cod_tipo_arch_banc" + vbCr +
                    "   FROM SIG_T.dbo.t_cnt_tipos_arch_bancarizacion" + vbCr +
                    "	WHERE tipo_respuesta_tipo_arch_banc = '" + tipo + "' AND tipo_arch_banc = '" + archivo + "'"
            Else
                sql = "" + vbCr +
                "SELECT cod_tipo_arch_banc" + vbCr +
                "   FROM SIG_T.dbo.t_cnt_tipos_arch_bancarizacion" + vbCr +
                "	WHERE tipo_respuesta_tipo_arch_banc = '" + tipo + "' AND SUBSTRING(tipo_arch_banc, 1, 5) = '" + archivo.Substring(0, 5) + "'"

            End If

            Return Conexion.returnScalar(sql)

        End Function

        Public Function fnc_obtener_codigo_pago(ByVal numero As String, ByVal año As String) As Integer

            Dim sql As String

            sql = "" + vbCr +
            "SELECT pag_codigo" + vbCr +
            "   FROM SIG_T.dbo.f_pla_pago" + vbCr +
            "   WHERE Pag_Estado IN (1,2) AND pag_anyo = " + año + " AND pag_numero = " + numero

            Return Conexion.returnScalar(sql)

        End Function

        Public Function fnc_obtener_codigo_banco(ByVal pagador As String) As Integer

            Dim sql As String

            sql = "" + vbCr +
                "SELECT cod_banco" + vbCr +
                "	FROM SIG_T.dbo.f_pla_pagadores" + vbCr +
                "	WHERE Nombre_Pagador = '" + pagador + "'"

            Return Conexion.returnScalar(sql)

        End Function

        Sub fnc_guardar_archivo(ByVal pago As String, ByVal banco As String, ByVal tipo_archivo As String, ByVal nombre As String, ByVal data() As Byte, ByVal estado As String, ByVal path1 As String, ByVal path2 As String)

            Dim sql As String

            sql = "" + vbCr +
                "INSERT INTO [SIG_T].[dbo].[t_cnt_archivos_bancarizacion] ([cod_pago],[cod_banco],[cod_tipo_arch_banc],[nombre_arch_bancarizacion],[data_arch_bancarizacion],[cod_estado_arch_bancarizacion])" + vbCr +
                "   VALUES (@pago, @banco, @tipo_archivo, @nombre, @data, @estado)" + vbCr +
                "" + vbCr +
                "SELECT @@IDENTITY"

            Dim cmd As New SqlCommand
            cmd.CommandText = Sql

            cmd.Parameters.AddWithValue("@pago", pago)
            cmd.Parameters.AddWithValue("@banco", banco)
            cmd.Parameters.AddWithValue("@tipo_archivo", tipo_archivo)
            cmd.Parameters.AddWithValue("@nombre", nombre)
            cmd.Parameters.AddWithValue("@estado", estado)

            Dim archivoParam As SqlParameter = cmd.Parameters.Add("@data", SqlDbType.Image)
            archivoParam.Value = data

            cmd.CommandType = CommandType.Text

            Dim cod_archivo As Integer = Convert.ToInt16(Conexion.FncEjecutarProcedimiento(cmd))

            Try
                If cod_archivo > 0 And estado = 5 Then

                    Dim result As String

                    cmd.Parameters.Clear()

                    cmd.CommandText = "p_cnt_carga_bancarizacion"
                    cmd.CommandType = CommandType.StoredProcedure

                    cmd.Parameters.AddWithValue("@path1", path1)
                    cmd.Parameters.AddWithValue("@path2", path2)
                    cmd.Parameters.AddWithValue("@cod_archivo", cod_archivo)
                    cmd.Parameters.AddWithValue("@tipo_archivo", tipo_archivo)
                    cmd.Parameters.AddWithValue("@cod_banco", banco)

                    result = Conexion.FncEjecutarProcedimiento(cmd)

                    If result = 0 Then

                        sql = "" + vbCr +
                        "UPDATE [SIG_T].[dbo].[t_cnt_archivos_bancarizacion]" + vbCr +
                        "   SET [cod_estado_arch_bancarizacion] = 8" + vbCr +
                        "   WHERE [cod_arch_bancarizacion] = " + cod_archivo.ToString()

                    Else

                        sql = "" + vbCr +
                        "UPDATE [SIG_T].[dbo].[t_cnt_archivos_bancarizacion]" + vbCr +
                        "   SET [cod_estado_arch_bancarizacion] = 6" + vbCr +
                        "   WHERE [cod_arch_bancarizacion] = " + cod_archivo.ToString()

                    End If

                    Conexion.FncEjecutarScript(sql)
                End If

            Catch ex As Exception
            End Try

        End Sub

        Public Function fnc_obtener_archivos_procesados(ByVal strArchivos As String, ByVal inicio As String, ByVal fin As String) As DataTable

            Dim MyResult As DataSet
            Dim sql As String = "" + vbCr +
                "SELECT arc.cod_arch_bancarizacion, pag.pag_nombre, ban.nombre_banco, tarc.desc_tipo_arch_banc, arc.nombre_arch_bancarizacion, est.desc_estado_arch_bancarizacion, arc.fecha_registro" + vbCr +
                "	FROM SIG_T.dbo.t_cnt_archivos_bancarizacion AS arc" + vbCr +
                "		LEFT JOIN SIG_T.dbo.f_pla_pago AS pag ON pag.pag_codigo = arc.cod_pago" + vbCr +
                "		LEFT JOIN SIG_T.dbo.t_glo_bancos AS ban ON ban.cod_banco = arc.cod_banco" + vbCr +
                "		LEFT JOIN SIG_T.dbo.t_cnt_tipos_arch_bancarizacion AS tarc ON tarc.cod_tipo_arch_banc = arc.cod_tipo_arch_banc" + vbCr +
                "		LEFT JOIN SIG_T.dbo.t_cnt_est_arch_bancarizacion AS est ON est.cod_estado_arch_bancarizacion = arc.cod_estado_arch_bancarizacion" + vbCr +
                "   WHERE nombre_arch_bancarizacion IN (" + strArchivos + ")" + vbCr +
                "       AND arc.fecha_registro BETWEEN CONVERT(DATETIME2,'" + inicio + "') AND CONVERT(DATETIME2,'" + fin + "')"

            Try
                MyResult = Conexion.GetDataSet(sql)
                Return MyResult.Tables(0)
            Catch ex As Exception
                Return Nothing
            End Try

        End Function

        Sub fnc_anular_archivos_anteriores(ByVal pago As String, ByVal banco As String, ByVal inicio As String)

            Dim sql As String = "" + vbCr +
                "UPDATE [SIG_T].[dbo].[t_cnt_archivos_bancarizacion]" + vbCr +
                "   SET [ultimo_arch_bancarizacion] = 0" + vbCr +
                "   WHERE [cod_pago] = " + pago + " AND [cod_banco] = " + banco + vbCr +
                "       AND [fecha_registro] < CONVERT(DATETIME2, '" + inicio + "')"

            Conexion.FncEjecutarScript(sql)

        End Sub

#End Region

    End Class
End Namespace
