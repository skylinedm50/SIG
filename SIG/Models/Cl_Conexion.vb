Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Sql
Imports System.Web.HttpContext


Public MustInherit Class Cl_Conexion

    Protected conexion As New SqlConnection()
    Protected Str_Query As String
    Protected Comando As New SqlCommand()
    Protected Obj_Tabla_Result As New DataTable()

    'Contructor  de la clase  en la cual se establece el string de para realizar la conexion a la base de datos
    'los parametros de conexion se establecen al ejecutar la función Cl_ReadFile.Fnc_ReadFileConexion()
    Public Sub New()
        Dim parametros As New Cl_ParamConexion()
        Cl_ReadFile.Fnc_ReadFileConexion()
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
            Me.Comando.CommandTimeout = 864000
            Me.Comando.ExecuteNonQuery()
            Me.Fnc_CloseConexion()
        Catch ex As SqlException
            Me.Fnc_CloseConexion()
            Return ex.Message
        End Try
        Return 1
    End Function

    ' esta función registra en el log del sistema la información relacionada a las actividades realizadas por el usuario
    Protected Sub Fnc_Log_sistema(ByVal operacion As String, ByVal user As String, ByVal tabla As String, ByVal suceso As Integer)
        Me.Fnc_OpenConexion()
        Me.Str_Query = " exec P_Log_Sistema @Operacion='" + operacion + "', @userLog = '" + user + "' , @Tabla = '" + tabla + "' , @Suceso = " + suceso.ToString()
        Me.Comando = New SqlCommand(Str_Query, conexion)
        Comando.CommandTimeout = 864000
        Comando.ExecuteNonQuery()
        Me.Fnc_CloseConexion()
    End Sub

    ' esta función ejecuta los script T-SQL que devuelven un solo valor escalar en su resultado
    Protected Function Fnc_GetSingledataConexion()
        Me.Fnc_OpenConexion()
        Me.Comando = New SqlCommand(Str_Query, conexion)
        Try
            Dim result = Comando.ExecuteScalar()
            Me.Fnc_CloseConexion()
            Return Convert.ToInt16(result)
        Catch ex As Exception
            Me.Fnc_CloseConexion()
            Return Nothing
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
            Me.Comando.ExecuteNonQuery()
            Me.Fnc_CloseConexion()
        Catch ex As Exception
            Me.Fnc_CloseConexion()
            HttpContext.Current.Session("error") = ex.Message
            Return ex.Message
        End Try

        Return Obj_tabla
    End Function

End Class
