Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.IO

Namespace SIG.Areas.Mineria.Models

    Public Class Cl_Conexion

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

            'ConnectionString = String.Format("Data Source ={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3};", My.Settings.ServidorCnt, My.Settings.BaseDatosMin, My.Settings.UsuarioCnt, My.Settings.ClaveCnt)

            Dim parametros As New Cl_ParamConexion()
            Cl_ReadFile.Fnc_ReadFileConexion()

            'ConnectionString = String.Format("Data Source ={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3};", My.Settings.ServidorCnt, My.Settings.BaseDatosCnt, My.Settings.UsuarioCnt, My.Settings.ClaveCnt)
            ConnectionString = String.Format("Data Source ={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3};", parametros.source, "SIG_C", parametros.name, parametros.pwd)


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
            Dim MiBase As New Cl_Conexion()
            Dim myDataSet As New DataSet()
            Try
                'Dim myConn As SqlConnection = Conexion.OpenConnection(MiBase.ConnectionString)
                Dim myConn As SqlConnection = Me.OpenConnection(MiBase.ConnectionString)
                Dim myDataAdapter As SqlDataAdapter = New SqlDataAdapter(strSQL, myConn)
                myDataAdapter.SelectCommand.CommandTimeout = 5000 'tiempo de respuesta en los querys
                myDataAdapter.Fill(myDataSet, "MySrcTable")
                'Conexion.CloseConnection(myConn)
                Me.CloseConnection(myConn)
            Catch myException As Exception
                Dim l = 0
            End Try
            Return myDataSet
        End Function

        Public Function GetRow(ByVal strSQL As String) As DataRow
            Dim MiBase As New Cl_Conexion()
            Dim myDataSet As New DataSet()

            Try
                'Dim myConn As SqlConnection = Conexion.OpenConnection(MiBase.ConnectionString)
                Dim myConn As SqlConnection = Me.OpenConnection(MiBase.ConnectionString)
                Dim myDataAdapter As SqlDataAdapter = New SqlDataAdapter(strSQL, myConn)
                myDataAdapter.SelectCommand.CommandTimeout = 5000 'tiempo de respuesta en los querys
                myDataAdapter.Fill(myDataSet, "MySrcTable")
                'Conexion.CloseConnection(myConn)
                Me.CloseConnection(myConn)
            Catch myException As Exception
            End Try

            If myDataSet.Tables(0).Rows.Count = 1 Then
                Return myDataSet.Tables(0).Rows.Item(0)
            Else
                Return Nothing
            End If
        End Function

        Public Function returnScalar(ByVal strSql As String) As Object

            Dim MyResult As Object
            Dim MiBase As New Cl_Conexion()

            Try
                'Dim myConn As SqlConnection = Conexion.OpenConnection(MiBase.ConnectionString)
                Dim myConn As SqlConnection = Me.OpenConnection(MiBase.ConnectionString)
                Dim MyCommand As New SqlCommand(strSql, myConn)

                MyResult = MyCommand.ExecuteScalar()
                'Conexion.CloseConnection(myConn)
                Me.CloseConnection(myConn)
                '            Return MyResult
            Catch ex As Exception
                Return -1
            End Try
            Return MyResult
        End Function

        Public Function updateTable(ByVal strSql As String) As Integer

            Dim MiBase As New Cl_Conexion()

            Try
                'Dim myConn As SqlConnection = Conexion.OpenConnection(MiBase.ConnectionString)
                Dim myConn As SqlConnection = Me.OpenConnection(MiBase.ConnectionString)
                Dim MyCommand As New SqlCommand(strSql, myConn)

                Dim rows As Integer = MyCommand.ExecuteNonQuery()
                'Conexion.CloseConnection(myConn)
                Me.CloseConnection(myConn)
                Return rows
            Catch ex As Exception
                Return -1
            End Try
        End Function

        Public Sub insertLog(ByVal usuario As String, ByVal operacion As String, ByVal tabla As String, ByVal suceso As String, ByVal registro As String)

            Dim strSql As String = "" + vbCr + _
                "INSERT INTO t_log_sistema (cod_usuario,operac_reali_log,tabla_afectada_log,suceso_log,registro_afectado_log)" + vbCr + _
                "   VALUES (" + usuario + ", '" + operacion + "', '" + tabla + "', " + suceso + ", " + registro + ")"

            Try
                Dim myConn As SqlConnection = Me.OpenConnection(Me.ConnectionString)
                Dim MyCommand As New SqlCommand(strSql, myConn)
                MyCommand.ExecuteNonQueryAsync()
                Me.CloseConnection(myConn)
            Catch ex As Exception
            End Try
        End Sub

    End Class

End Namespace
