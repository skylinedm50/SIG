Namespace SIG.Areas.Mineria.Models

    Public Class Cl_Configuracion

        Dim conexion As SIG.Areas.Mineria.Models.Cl_Conexion = New SIG.Areas.Mineria.Models.Cl_Conexion

        Public Function Fnc_obtener_ano_valido_fichas()

            Dim MyResult As New Object
            Dim sql As String = "" + vbCr + _
                "SELECT valor" + vbCr + _
                "   FROM SIG_C.dbo.t_sig_variables_mineria" + vbCr + _
                "   WHERE cod_variable = 1"

            Try
                MyResult = conexion.returnScalar(sql)
            Catch ex As Exception
            End Try

            Return MyResult

        End Function

        Public Function Fnc_actulizar_ano_elegibilidad_valido(ByVal ano As String) As Integer

            Dim MyResult As Integer
            Dim sql As String = "" + vbCr + _
                "UPDATE SIG_C.dbo.t_sig_variables_mineria" + vbCr + _
                "   SET valor = " + ano + vbCr + _
                "   WHERE cod_variable = 1"

            Try
                MyResult = conexion.updateTable(sql)
            Catch ex As Exception
            End Try

            Return MyResult

        End Function

    End Class

End Namespace


