Imports System.Data.SqlClient

Namespace SIG.Areas.Planilla.Models

    Public Class cl_generacion_documentos

        Dim conexion As SIG.Areas.Planilla.Models.cl_conexion_db = New SIG.Areas.Planilla.Models.cl_conexion_db

        Public Function fnc_validar_esquemas(ByVal strEsquemas As String) As Boolean

            Dim MyResult As Integer
            Dim sql As String = "" + vbCr + _
                "SELECT COUNT(DISTINCT pag_codigo)" + vbCr + _
                "	FROM SIG_T.dbo.f_pla_esquema " + vbCr + _
                "   WHERE esq_codigo IN (" + strEsquemas + ")"

            Try
                MyResult = conexion.fnc_obtener_scalar(sql)
            Catch ex As Exception
                Return ex.Message
            End Try

            If MyResult = 1 Then
                Return True
            Else
                Return False
            End If

        End Function

        Public Function fnc_generar_archivos(ByVal strEsquemas As String) As String

            Dim comand As New SqlCommand()
            Dim ds As New DataSet
            Dim dt As New DataTable
            Dim cmd As New System.Diagnostics.Process()

            comand.CommandText = "p_pla_generar_archivos"
            comand.CommandType = CommandType.StoredProcedure
            comand.CommandTimeout = 600
            comand.Parameters.AddWithValue("@strEsquemas", strEsquemas)

            ds = conexion.fnc_ejecutar_procedimiento(comand)

            cmd.StartInfo.WorkingDirectory = "C:\"
            cmd.StartInfo.UseShellExecute = False
            cmd.StartInfo.FileName = "cmd.exe"
            cmd.StartInfo.CreateNoWindow = True
            cmd.StartInfo.RedirectStandardInput = False
            cmd.StartInfo.RedirectStandardOutput = False
            cmd.StartInfo.RedirectStandardError = False


            Dim objParametros As Cl_ParamConexion = New Cl_ParamConexion()

            For Each row As DataRow In ds.Tables(0).Rows

                If row(1) = 1 Then
                    cmd.StartInfo.Arguments = "/c " + row(0) + " -U " + objParametros.name + " -P " + objParametros.pwd + " -S " + objParametros.source
                Else
                    cmd.StartInfo.Arguments = "/c " + row(0)
                End If

                cmd.Start()
                cmd.WaitForExit()
                cmd.Close()

            Next

            conexion.fnc_ejecutar_simple_comando("TRUNCATE TABLE SIG_T.dbo.f_pla_tmp_pre_apertura")

            Return ds.Tables(1).Rows.Item(0).Item(0)
        End Function


    End Class

End Namespace
