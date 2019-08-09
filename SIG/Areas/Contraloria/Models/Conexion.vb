Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.IO

Namespace SIG.Areas.Contraloria.Models

    Public Class Conexion

        Dim StrCadena() As String
        Dim StrServidor, StrBaseDatos As String

        Private _StrConexion As String
        Public Property ConnectionString()
            Get
                Return _StrConexion
            End Get
            Set(ByVal value)
                _StrConexion = value
            End Set
        End Property

        Public Sub New()

            Dim parametros As New Cl_ParamConexion()
            Cl_ReadFile.Fnc_ReadFileConexion()
            ConnectionString = String.Format("Data Source ={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3};", parametros.source, parametros.db, parametros.name, parametros.pwd)

        End Sub


        Public Function OpenConnectionInicioSesion(ByVal ConnectionString As String, ByRef MySqlConnection As SqlConnection)

            MySqlConnection = New SqlConnection(ConnectionString)
            Try
                MySqlConnection.Open()
            Catch ex As Exception
                Return False
            End Try
            Return True
        End Function

        Public Sub modSettings(ByVal usuario As String, ByVal contraseña As String)
            'MessageBox.Show(String.Format("usuario (antes): {0}, contraseña: {1}", My.Settings.UsuarioPago, My.Settings.ClavePago))

            'My.Settings.UsuarioPago = usuario
            'My.Settings.ClavePago = contraseña

            'MessageBox.Show(String.Format("usuario (despues): {0}, contraseña: {1}", My.Settings.UsuarioPago, My.Settings.ClavePago))
        End Sub


        Public Function OpenConnection(ByVal ConnectionString As String) As SqlConnection
            Dim MySqlConnection As New SqlConnection(ConnectionString)
            Try
                MySqlConnection.Open()
            Catch ex As Exception
            End Try
            Return MySqlConnection
        End Function

        Public Function CloseConnection(ByVal mySqlConnection As SqlConnection) As Boolean
            Try
                If mySqlConnection.State = ConnectionState.Open Then
                    mySqlConnection.Close()
                End If
            Catch myException As Exception
                Return False
            End Try
            Return True
        End Function

        Public Function GetDataSet(ByVal strSQL As String) As DataSet
            Dim MiBase As New Conexion()
            Dim myDataSet As New DataSet()
            Try
                Dim myConn As SqlConnection = OpenConnection(MiBase.ConnectionString)
                Dim myDataAdapter As SqlDataAdapter = New SqlDataAdapter(strSQL, myConn)
                myDataAdapter.SelectCommand.CommandTimeout = 500000 'tiempo de respuesta en los querys
                myDataAdapter.Fill(myDataSet, "MySrcTable")
                CloseConnection(myConn)
            Catch myException As Exception
            End Try
            Return myDataSet
        End Function

        Public Function GetRow(ByVal strSQL As String) As DataRow
            Dim MiBase As New Conexion()
            Dim myDataSet As New DataSet()

            Try
                Dim myConn As SqlConnection = OpenConnection(MiBase.ConnectionString)
                Dim myDataAdapter As SqlDataAdapter = New SqlDataAdapter(strSQL, myConn)
                myDataAdapter.SelectCommand.CommandTimeout = 5000 'tiempo de respuesta en los querys
                myDataAdapter.Fill(myDataSet, "MySrcTable")
                CloseConnection(myConn)
            Catch myException As Exception
            End Try

            If myDataSet.Tables(0).Rows.Count = 1 Then
                Return myDataSet.Tables(0).Rows.Item(0)
            Else
                Return Nothing
            End If
        End Function

        Public Function getInt(ByVal strSQL As String) As Integer
            Dim MiBase As New Conexion()
            Dim myDataSet As New DataSet()

            Try
                Dim myConn As SqlConnection = OpenConnection(MiBase.ConnectionString)
                Dim myDataAdapter As SqlDataAdapter = New SqlDataAdapter(strSQL, myConn)
                myDataAdapter.SelectCommand.CommandTimeout = 5000 'tiempo de respuesta en los querys
                myDataAdapter.Fill(myDataSet, "MySrcTable")
                CloseConnection(myConn)
            Catch myException As Exception
            End Try

            If myDataSet.Tables(0).Rows.Count = 1 Then
                Return myDataSet.Tables(0).Rows.Item(0).Item(0)
            Else
                Return -1
            End If
        End Function

        Public Function getString(ByVal strSQL As String) As String
            Dim MiBase As New Conexion()
            Dim myDataSet As New DataSet()

            Try
                Dim myConn As SqlConnection = OpenConnection(MiBase.ConnectionString)
                Dim myDataAdapter As SqlDataAdapter = New SqlDataAdapter(strSQL, myConn)
                myDataAdapter.SelectCommand.CommandTimeout = 5000 'tiempo de respuesta en los querys
                myDataAdapter.Fill(myDataSet, "MySrcTable")
                CloseConnection(myConn)
            Catch myException As Exception
            End Try

            If myDataSet.Tables(0).Rows.Count = 1 Then
                Return myDataSet.Tables(0).Rows.Item(0).Item(0)
            Else
                Return Nothing
            End If
        End Function

        Public Function insertRow(ByVal strSql As String) As Boolean

            Dim MiBase As New Conexion()

            Try
                Dim myConn As SqlConnection = OpenConnection(MiBase.ConnectionString)
                Dim MyCommand As New SqlCommand(strSql, myConn)

                If MyCommand.ExecuteNonQuery() = 1 Then
                    Return True
                Else
                    Return False
                End If

                CloseConnection(myConn)
            Catch ex As Exception
                Return False
            End Try

        End Function

        Public Function insertRowAndReturnID(ByVal strSql As String)

            Dim MiBase As New Conexion()
            Dim id As Integer

            Try
                Dim myConn As SqlConnection = OpenConnection(MiBase.ConnectionString)
                Dim MyCommand As New SqlCommand(strSql, myConn)
                If MyCommand.ExecuteNonQuery() = 1 Then
                    strSql = "SELECT @@IDENTITY"
                    MyCommand.CommandText = strSql
                    id = MyCommand.ExecuteScalar()
                Else
                    Return -1
                End If

                CloseConnection(myConn)
            Catch ex As Exception
            End Try
            Return id
        End Function

        Public Function updateTable(ByVal strSql As String) As Integer

            Dim MiBase As New Conexion()

            Try
                Dim myConn As SqlConnection = OpenConnection(MiBase.ConnectionString)
                Dim MyCommand As New SqlCommand(strSql, myConn)

                Dim rows As Integer = MyCommand.ExecuteNonQuery()
                CloseConnection(myConn)
                Return rows
            Catch ex As Exception
                Return -1
            End Try
        End Function

        Public Function returnLastID()
            Dim MiBase As New Conexion()
            Dim strSql As String = "SELECT @@IDENTITY"

            Try
                Dim myConn As SqlConnection = OpenConnection(MiBase.ConnectionString)
                Dim MyCommand As New SqlCommand(strSql, myConn)

                Dim id As Integer = MyCommand.ExecuteScalar()
                CloseConnection(myConn)
                Return id
            Catch ex As Exception
                Return -1
            End Try
        End Function

        Public Function returnBoolean(ByVal strSql As String)
            Dim MiBase As New Conexion()
            Dim myDataSet As New DataSet()

            Try
                Dim myConn As SqlConnection = OpenConnection(MiBase.ConnectionString)
                Dim myDataAdapter As SqlDataAdapter = New SqlDataAdapter(strSql, myConn)
                myDataAdapter.SelectCommand.CommandTimeout = 5000 'tiempo de respuesta en los querys
                myDataAdapter.Fill(myDataSet, "MySrcTable")
                Me.CloseConnection(myConn)
            Catch myException As Exception
            End Try

            If myDataSet.Tables(0).Rows.Count = 0 Then
                Return False
            ElseIf myDataSet.Tables(0).Rows.Count = 1 Then
                Return True
            End If

            Return Nothing
        End Function

        Public Function returnScalar(ByVal strSql As String)

            Dim MyResult As Object
            Dim MiBase As New Conexion()

            Try
                Dim myConn As SqlConnection = OpenConnection(MiBase.ConnectionString)
                Dim MyCommand As New SqlCommand(strSql, myConn)

                MyResult = MyCommand.ExecuteScalar()
                CloseConnection(myConn)

            Catch ex As Exception
                Return -1
            End Try
            Return MyResult
        End Function

        Public Sub insertLog(ByVal usuario As String, ByVal operacion As String, ByVal tabla As String, ByVal suceso As String, ByVal registro As String)

            Dim strSql As String = "" + vbCr +
                "INSERT INTO t_log_sistema (cod_usuario,operac_reali_log,tabla_afectada_log,suceso_log,registro_afectado_log)" + vbCr +
                "   VALUES (" + usuario + ", '" + operacion + "', '" + tabla + "', " + suceso + ", " + registro + ")"

            Try
                Dim myConn As SqlConnection = OpenConnection(ConnectionString)
                Dim MyCommand As New SqlCommand(strSql, myConn)
                MyCommand.ExecuteNonQueryAsync()
                CloseConnection(myConn)
            Catch ex As Exception
            End Try
        End Sub

        Public Function FncEjecutarProcedimiento(ByVal comand As SqlCommand)

            Dim objComando As New SqlCommand
            Dim objConexionDB As New SqlConnection
            Dim objResultado As Object

            objConexionDB.ConnectionString = ConnectionString

            Try
                objConexionDB.Open()
                comand.Connection = objConexionDB
                objResultado = comand.ExecuteScalar()
                objConexionDB.Close()
                Return objResultado
            Catch ex As Exception
                objConexionDB.Close()
                Return 0
            End Try
        End Function

        Sub FncEjecutarScript(ByVal strQuery As String)

            Dim objComando As New SqlCommand
            Dim objConexionDB As New SqlConnection
            Dim objResultado As Object

            objConexionDB.ConnectionString = ConnectionString

            Try
                objComando.CommandText = strQuery

                objComando.Connection = objConexionDB
                objComando.CommandTimeout = 864000
                objConexionDB.Open()
                objResultado = objComando.ExecuteNonQuery()
                objConexionDB.Close()
            Catch ex As Exception

            End Try

        End Sub

        Public Function FncRetornarFilasAfectadas(ByVal sql As String) As Integer
            Dim result As Integer

            Try
                Dim myConn As SqlConnection = OpenConnection(ConnectionString)
                Dim MyCommand As New SqlCommand(sql, myConn)
                result = MyCommand.ExecuteNonQuery()
                CloseConnection(myConn)
            Catch ex As Exception
            End Try

            Return result

        End Function

    End Class

End Namespace
