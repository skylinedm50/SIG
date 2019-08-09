
Namespace SIG.Areas.Incorporaciones.Models

    Public MustInherit Class Cl_Hogar
        Inherits SIG.Areas.Incorporaciones.Models.Cl_Conexion

#Region "Campos"

        Protected obj_tabla_personas_actualizar As New DataTable()
        Protected _row_fila As DataRow
        Protected _parametro_tabla As SqlClient.SqlParameter
        Protected _Personas As New Cl_Persona()

#End Region

    End Class

End Namespace
