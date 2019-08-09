Imports System.Data.SqlClient

Namespace SIG.Areas.Contraloria.Models

    Public Class Escaneo

        Dim Conexion As SIG.Areas.Contraloria.Models.Conexion = New SIG.Areas.Contraloria.Models.Conexion

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

        ' obtiene todos los departamentos de la tabla t_glo_departamentos
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

        'obtiene los fondo con los cuales se va a pagar el período
        Public Function getFondosByPeriodo(ByVal codPeriodo As String)

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr +
                "SELECT DISTINCT fon.fond_codigo, fon.fond_nombre" + vbCr +
                "	FROM SIG_T.dbo.f_pla_fondos AS fon" + vbCr +
                "		INNER JOIN SIG_T.dbo.f_pla_fondos_esquema AS fon_esq ON fon_esq.fond_codigo = fon.fond_codigo" + vbCr +
                "		INNER JOIN SIG_T.dbo.f_pla_esquema AS esq ON esq.esq_codigo = fon_esq.esq_codigo" + vbCr +
                "		INNER JOIN SIG_T.dbo.t_cnt_leitzs AS lei ON lei.cod_fondo = fon.fond_codigo AND lei.cod_pago = esq.pag_codigo" + vbCr +
                "	WHERE esq.esq_tipo_esquema = 1 AND esq.pag_codigo = " + codPeriodo

            Try
                MyResult = Conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult

        End Function

        'obtiene los tipos de documentos que se pueden almacenar en el SIG
        Public Function getTiposDocumento() As DataTable

            Dim MyResult As New DataSet
            Dim sql As String

            sql = "SELECT cod_tipo_documento, nombre_tipo_documento " + vbCr + _
                "FROM t_cnt_tipos_documentos"

            Try
                MyResult = Conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)
        End Function

        'obtiene la información de los leitz según el periodo y el fondo
        Public Function getInfoLeitz(ByVal codPeriodo As String, ByVal codFondo As String) As DataTable
            Dim MyResult As New DataSet
            Dim sql As String
            sql = "SELECT lei.cod_leitz, lei.numero_leitz, lei.descripcion_leitz " + vbCr + _
                "FROM t_cnt_leitzs AS lei " + vbCr + _
                "   WHERE lei.cod_pago = " + codPeriodo + " AND lei.cod_fondo = " + codFondo
            Try
                MyResult = Conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)
        End Function

        'obtiene el último número de leitz en la tabla con el período y el fondo indicado
        Public Function getLastNumLeitz(ByVal codPeriodo As String, ByVal codFondo As String) As Integer

            Dim MyResult As Integer
            Dim sql As String

            sql = "SELECT MAX(lei.numero_leitz)" + vbCr + _
                "FROM t_cnt_leitzs AS lei" + vbCr + _
                "WHERE lei.cod_pago = " + codPeriodo + " AND lei.cod_fondo = " + codFondo

            Try
                MyResult = Conexion.getInt(sql)
            Catch ex As Exception

            End Try

            Return MyResult
        End Function

        ''inserta un nuevo leitz en la tabla
        'Public Function insertNewLeitz(ByVal codPeriodo As String, ByVal codFondo As String, ByVal desc As String)

        '    Dim Myresult As Integer
        '    Dim sql As String
        '    Dim num As Integer = getLastNumLeitz(codPeriodo, codFondo) + 1

        '    sql = String.Format("INSERT INTO t_cnt_leitzs (cod_pago, cod_fondo, numero_leitz, descripcion_leitz, cod_usuario)" + vbCr + _
        '        "VALUES ({0}, {1}, {2}, '{3}', {4})", codPeriodo, codFondo, num, desc, HttpContext.Current.Session("usuario"))

        '    Try
        '        'Myresult = Conexion.insertRow(sql)
        '        Myresult = Conexion.insertRowAndReturnID(sql)
        '        Conexion.insertLog(HttpContext.Current.Session("usuario"), "Creo un nuevo leitz en la base de datos", "t_cnt_leitzs", 1, Myresult)

        '    Catch ex As Exception
        '        Myresult = False
        '    End Try

        '    Return Myresult
        'End Function

        'Public Function insertDocumento(ByVal leitz As String, ByVal dpto As String, ByVal banco As String, ByVal tipo As String, ByVal nombre As String, ByVal data() As Byte, ByVal inicio As String, ByVal fin As String) As Integer


        '    Dim MiBase As New Conexion()
        '    Dim myConn As SqlConnection = Conexion.OpenConnection(MiBase.ConnectionString)
        '    Dim query As String
        '    Dim cmd As New SqlCommand()
        '    Dim temp As String = inicio.ToString()

        '    ' verifico si la fecha de inicio es null (01/01/0100 valor null por defecto del dateedit de devexpress)
        '    ' para saber si es un reporte o no
        '    If temp = "01/01/0100" Then
        '        query = "INSERT INTO t_cnt_documentos (cod_leitz, cod_departamento, cod_banco, cod_tipo_documento, nombre_documento, data_documento, cod_usuario) " & vbCr & vbLf &
        '        "VALUES (@cod_leitz, @cod_depto, @cod_banco, @cod_tipo_documento, @nombre_documento, @data, @usuario)"
        '    Else
        '        query = "INSERT INTO t_cnt_documentos (cod_leitz, cod_departamento, cod_banco, cod_tipo_documento, nombre_documento, data_documento, fecha_inicio_documento, fecha_fin_documento, cod_usuario) " & vbCr & vbLf &
        '        "VALUES (@cod_leitz, @cod_depto, @cod_banco, @cod_tipo_documento, @nombre_documento, @data, @inicio, @fin, @usuario)"

        '        Dim _inicio As SqlParameter = cmd.Parameters.Add("@inicio", SqlDbType.Date)
        '        _inicio.Value = inicio

        '        Dim _fin As SqlParameter = cmd.Parameters.Add("@fin", SqlDbType.Date)
        '        _fin.Value = fin
        '    End If

        '    cmd.CommandText = query
        '    cmd.Connection = myConn

        '    'query = "INSERT INTO t_cnt_documentos (cod_leitz, cod_depto, cod_tipo_documento, nombre_documento, data_documento) " & vbCr & vbLf &
        '    '    "VALUES (@cod_leitz, @cod_depto, @cod_tipo_documento, @nombre_documento, @data)"

        '    cmd.Parameters.AddWithValue("@cod_leitz", leitz)
        '    cmd.Parameters.AddWithValue("@cod_depto", dpto)
        '    cmd.Parameters.AddWithValue("@cod_banco", banco)
        '    cmd.Parameters.AddWithValue("@cod_tipo_documento", tipo)
        '    cmd.Parameters.AddWithValue("@nombre_documento", nombre)
        '    cmd.Parameters.AddWithValue("@usuario", HttpContext.Current.Session("usuario"))

        '    Dim dataArchivo As SqlParameter = cmd.Parameters.Add("@data", System.Data.SqlDbType.Image)
        '    dataArchivo.Value = data

        '    Dim num As Integer = cmd.ExecuteNonQuery()

        '    If num = 1 Then

        '        cmd.CommandText = "SELECT @@IDENTITY"
        '        Dim MyResult As Integer = cmd.ExecuteScalar()
        '        Conexion.insertLog(HttpContext.Current.Session("usuario"), "Inserto un nuevo documento de pago.", "t_cnt_documentos", 1, MyResult)

        '    End If

        '    Conexion.CloseConnection(myConn)
        '    Return num
        'End Function

        Public Function guardarDocumento(ByVal leitz As String, ByVal dpto As String, ByVal banco As String, ByVal tipo As String, ByVal nombre As String, ByVal url As String, ByVal inicio As String, ByVal fin As String) As Integer

            Dim Myresult As Integer
            Dim sql As String
            Dim cmd As New SqlCommand()
            Dim temp As String = inicio.ToString()

            'sql = String.Format("INSERT INTO t_cnt_leitzs (cod_pago, cod_fondo, numero_leitz, descripcion_leitz, cod_usuario)" + vbCr +
            '    "VALUES ({0}, {1}, {2}, REPLACE('{3}','""',''), {4})", cod_pago, cod_fondo, numero_leitz, descripcion_leitz, HttpContext.Current.Session("usuario"))

            If temp = "1/1/0100" Then

                sql = "" + vbCr +
                    "INSERT INTO t_cnt_documentos (cod_leitz, cod_departamento, cod_banco, cod_tipo_documento, nombre_documento, url_documento, cod_usuario)" + vbCr +
                    "   VALUES (@cod_leitz, @cod_depto, @cod_banco, @cod_tipo_documento, @nombre_documento, @url, @usuario)" + vbCr +
                    "" + vbCr +
                    "SELECT @@IDENTITY"
            Else

                sql = "" + vbCr +
                    "INSERT INTO t_cnt_documentos (cod_leitz, cod_departamento, cod_banco, cod_tipo_documento, nombre_documento, url_documento, fecha_inicio_documento, fecha_fin_documento, cod_usuario)" + vbCr +
                    "   VALUES (@cod_leitz, @cod_depto, @cod_banco, @cod_tipo_documento, @nombre_documento, @url, @inicio, @fin, @usuario)" + vbCr +
                    "" + vbCr +
                    "SELECT @@IDENTITY"

                Dim _inicio As SqlParameter = cmd.Parameters.Add("@inicio", SqlDbType.Date)
                _inicio.Value = inicio

                Dim _fin As SqlParameter = cmd.Parameters.Add("@fin", SqlDbType.Date)
                _fin.Value = fin
            End If

            cmd.Parameters.AddWithValue("@cod_leitz", leitz)
            cmd.Parameters.AddWithValue("@cod_depto", dpto)
            cmd.Parameters.AddWithValue("@cod_banco", banco)
            cmd.Parameters.AddWithValue("@cod_tipo_documento", tipo)
            cmd.Parameters.AddWithValue("@nombre_documento", nombre)
            cmd.Parameters.AddWithValue("@url", url)
            cmd.Parameters.AddWithValue("@usuario", HttpContext.Current.Session("usuario"))

            cmd.CommandText = sql

            Try
                Myresult = Conexion.FncEjecutarProcedimiento(cmd)
            Catch ex As Exception
                Myresult = -1
            End Try

            Return Myresult

        End Function

        'obtiene los leitz según los parámetros seleccionados por el usuario
        Public Function getLeitz(ByVal periodo As String, ByVal fondo As String, ByVal dpto As String, ByVal banco As String, ByVal tipo As String)

            Dim MyResult As New DataSet
            Dim sql As String

            sql = "" + vbCr + _
                "SELECT DISTINCT pag.pag_nombre, fon.fond_nombre, lei.cod_leitz, lei.numero_leitz, lei.descripcion_leitz " + vbCr + _
                "FROM dbo.t_cnt_leitzs AS lei " + vbCr + _
                "   INNER JOIN dbo.t_cnt_documentos AS doc ON doc.cod_leitz = lei.cod_leitz " + vbCr + _
                "	INNER JOIN dbo.f_pla_pago AS pag ON pag.pag_codigo = lei.cod_pago  " + vbCr + _
                "	INNER JOIN dbo.f_pla_fondos AS fon ON fon.fond_codigo = lei.cod_fondo  " + vbCr + _
                "WHERE lei.cod_pago = " + periodo

            If fondo <> "" Then
                sql += " AND lei.cod_fondo = " + fondo
            End If

            If dpto <> "" Then
                sql += " AND doc.cod_departamento = " + dpto
            End If

            If banco <> "" Then
                sql += " AND doc.cod_banco = " + banco
            End If

            If tipo <> "" Then
                sql += " AND doc.cod_tipo_documento = " + tipo
            End If

            Try
                MyResult = Conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)

        End Function

        'obtiene los documentos contenidos en el leitz filtrando los resultados por los demas parámetros seleccionados por el usuario
        Public Function getDocumentosLeitz(ByVal periodo As String, ByVal fondo As String, ByVal dpto As String, ByVal banco As String, ByVal tipo As String, ByVal leitz As String)

            Dim MyResult As New DataSet
            Dim sql As String

            sql = "SELECT DISTINCT doc.cod_documento, dep.desc_departamento, ban.nombre_banco, tipo.nombre_tipo_documento, " + vbCr + _
                "   doc.nombre_documento, doc.fecha_inicio_documento, doc.fecha_fin_documento " + vbCr + _
                "FROM t_cnt_documentos AS doc " + vbCr + _
                "   INNER JOIN dbo.t_glo_departamentos AS dep ON dep.cod_departamento = doc.cod_departamento " + vbCr + _
                "	INNER JOIN dbo.t_glo_bancos AS ban ON ban.cod_banco = doc.cod_banco " + vbCr + _
                "	INNER JOIN dbo.t_cnt_tipos_documentos AS tipo ON tipo.cod_tipo_documento = doc.cod_tipo_documento " + vbCr + _
                "   INNER JOIN dbo.t_cnt_leitzs AS lei ON lei.cod_leitz = doc.cod_leitz " + vbCr + _
                "WHERE doc.cod_leitz = " + leitz
            '"WHERE doc.cod_leitz = " + leitz + " lei.cod_periodo = " + periodo

            If fondo <> "" Then
                sql += " AND lei.cod_fondo = " + fondo
            End If

            If dpto <> "" Then
                sql += " AND doc.cod_departamento = " + dpto
            End If

            If banco <> "" Then
                sql += " AND doc.cod_banco = " + banco
            End If

            If tipo <> "" Then
                sql += " AND doc.cod_tipo_documento = " + tipo
            End If

            Try
                MyResult = Conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult.Tables(0)

        End Function

        'obtiene el nombre del documento junto con los bits que lo componen
        'Public Function getDocumento(ByVal cod As String)

        '    Dim MyResult As DataRow
        '    Dim sql As String

        '    sql = "SELECT nombre_documento, data_documento FROM t_cnt_documentos WHERE cod_documento = " + cod

        '    Try
        '        MyResult = Conexion.GetRow(sql)
        '        Conexion.insertLog(HttpContext.Current.Session("usuario"), "Visualizo una copia de un documento de pago.", "ninguna", 1, cod)

        '    Catch ex As Exception
        '        Return Nothing
        '    End Try

        '    Return MyResult
        'End Function

        Public Function getUrlDocumento(ByVal cod As String)

            Dim sql As String = "" + vbCr +
                "SELECT url_documento" + vbCr +
                "   FROM SIG_T.dbo.t_cnt_documentos" + vbCr +
                "   WHERE cod_documento = " + cod

            Return Conexion.returnScalar(sql)

        End Function

#Region "funciones para la administración de leitz"

        Public Function getDatosAdministrarLeitz() As DataSet

            Dim MyResult As New DataSet
            Dim sql As String = "" + vbCr +
                "SELECT cod_leitz, cod_pago, cod_fondo, numero_leitz, descripcion_leitz" + vbCr +
                "	FROM SIG_T.dbo.t_cnt_leitzs" + vbCr +
                "" + vbCr +
                "SELECT pag_codigo, pag_nombre" + vbCr +
                "   FROM SIG_T.dbo.f_pla_pago" + vbCr +
                "   ORDER BY pag_codigo DESC" + vbCr +
                "" + vbCr +
                "SELECT fond_codigo, fond_nombre" + vbCr +
                "   FROM SIG_T.dbo.f_pla_fondos"

            Try
                MyResult = Conexion.GetDataSet(sql)
            Catch ex As Exception
            End Try

            Return MyResult

        End Function

        Public Function guardarLeitz(ByVal cod_pago As String, ByVal cod_fondo As String, ByVal numero_leitz As String, ByVal descripcion_leitz As String) As Integer

            Dim Myresult As Integer
            Dim sql As String

            sql = String.Format("INSERT INTO t_cnt_leitzs (cod_pago, cod_fondo, numero_leitz, descripcion_leitz, cod_usuario)" + vbCr +
                "VALUES ({0}, {1}, {2}, REPLACE('{3}','""',''), {4})", cod_pago, cod_fondo, numero_leitz, descripcion_leitz, HttpContext.Current.Session("usuario"))

            Try
                Myresult = Conexion.insertRowAndReturnID(sql)
                Conexion.insertLog(HttpContext.Current.Session("usuario"), "Creo un nuevo leitz en la base de datos", "t_cnt_leitzs", 1, Myresult)

            Catch ex As Exception
                Myresult = -1
            End Try

            Return Myresult
        End Function

        Public Function actualizarLeitz(ByVal cod_leitz As String, ByVal cod_pago As String, ByVal cod_fondo As String, ByVal numero_leitz As String, ByVal descripcion_leitz As String) As Boolean

            Dim sql As String = "" + vbCr +
                "UPDATE [SIG_T].[dbo].[t_cnt_leitzs]" + vbCr +
                "   SET cod_pago = " + cod_pago + vbCr +
                "       ,cod_fondo = " + cod_fondo + vbCr +
                "       ,numero_leitz = " + numero_leitz + vbCr +
                "       ,descripcion_leitz = REPLACE('" + descripcion_leitz + "','""','')" + vbCr +
                "   WHERE cod_leitz = " + cod_leitz

            Return Conexion.FncRetornarFilasAfectadas(sql)

        End Function

#End Region

    End Class

End Namespace
