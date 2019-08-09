Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Sql
Imports System.Web.HttpContext


Namespace SIG.Areas.Incorporaciones.Models

    Public MustInherit Class Cl_Conexion

        Protected conexion As New SqlConnection()
        Protected Str_Query As String
        Protected Comando As New SqlCommand()
        Protected Obj_Tabla_Result As New DataTable()

        'Contructor  de la clase  en la cual se establece el string de para realizar la conexion a la base de datos
        'los parametros de conexion se establecen al ejecutar la función Cl_ReadFile.Fnc_ReadFileConexion()
        Public Sub New()
            Dim parametros As New SIG.Areas.Incorporaciones.Class.Cl_ParamConexion()
            'Dim parametros As SIG.Areas.Incorporaciones.Class.Cl_ParamConexion = New SIG.Areas.Incorporaciones.Class.Cl_ParamConexion()
            SIG.Areas.Incorporaciones.Class.Cl_ReadFile.Fnc_ReadFileConexion()
            Me.conexion = New SqlConnection("Data Source =" + parametros.source + ";Initial Catalog=" + parametros.db + " ; Persist Security Info=True; User ID=" + parametros.name + "; Password = " + parametros.pwd + "")
        End Sub

        Private Sub Fnc_OpenConexion()
            Me.conexion.Open()
        End Sub

        Private Sub Fnc_CloseConexion()
            Me.conexion.Close()
        End Sub

        'esta función ejecuta los script T-SQL que no devuelven ningun valor (Delete , update,insert)
        Protected Function Fnc_ExecQuery()
            Try
                Me.Fnc_OpenConexion()
                Me.Comando = New SqlCommand(Str_Query, conexion)
                Me.Comando.ExecuteNonQuery()
                Me.Fnc_CloseConexion()
            Catch ex As SqlException
                Me.Fnc_CloseConexion()
                Return ex.HResult
            End Try
            Return 1
        End Function



        ' esta función ejecuta los script T-SQL que devuelven un solo valor escalar en su resultado
        Protected Function Fnc_GetSingledataConexion()
            Try
                Me.Fnc_OpenConexion()
                Me.Comando = New SqlCommand(Str_Query, conexion)
                Dim result = Comando.ExecuteScalar()
                Me.Fnc_CloseConexion()
                Return result
            Catch ex As Exception
                Me.Fnc_CloseConexion()
                Return ex.HResult
            End Try
          
        End Function

        'esta función ejecuta los scripr T-SQL que devuelven una colección de registros en su resultado 
        Protected Function FncGetTableQuery()
            Dim Obj_tabla As New DataTable()
            Try
                Me.Comando = New SqlCommand(Me.Str_Query, Me.conexion)
                Me.Fnc_OpenConexion()
                Dim Obj_Adapter As New SqlDataAdapter(Me.Comando)
                Obj_Adapter.SelectCommand.CommandTimeout = 864000
                Obj_Adapter.Fill(Obj_tabla)
                'Me.Comando.ExecuteNonQuery()
                Me.Fnc_CloseConexion()
            Catch ex As Exception
                Me.Fnc_CloseConexion()
                HttpContext.Current.Session("error") = ex.Message
                Return ex.Message
            End Try

            Return Obj_tabla
        End Function

    End Class

End Namespace
