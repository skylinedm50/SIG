Imports System.Web.Script.Serialization

Namespace SIG.Areas.Seguridad.Models
    Public Class Roles
        Inherits SIG.Areas.Seguridad.Models.Cl_Conexion
        Function fnc_lista_roles()
            Me.Str_Query = "SELECT roles.id_rol, roles.desc_rol, case when roles.estado_rol = 1 then 'Si' else 'No' end estado_rol, modu.nom_modulo" + vbCr +
               "FROM T_Roles AS roles" + vbCr +
               "INNER JOIN T_Modulos modu on roles.id_modulo = modu.id_modulos" + vbCr +
               "ORDER BY desc_rol"
            Return Me.FncGetTableQuery()
        End Function

        Function fnc_agregar_rol(ByVal desc_rol As String, ByVal estado_rol As String, ByVal id_modulo As String)
            Me.Str_Query = "INSERT INTO [dbo].[T_Roles]([desc_rol], [estado_rol], [id_modulo])" + vbCr +
                            "VALUES('" + desc_rol + "','" + estado_rol + "','" + id_modulo + "')"
            Return Me.Fnc_GetSingledataConexion()
        End Function

        Function fnc_actualizar_rol(ByVal desc_rol As String, ByVal estado_rol As String, ByVal id_modulo As String, ByVal id_rol As String)
            Me.Str_Query = "UPDATE [dbo].[T_Roles] SET [desc_rol] = '" + desc_rol + "', [estado_rol] ='" + estado_rol + "', [id_modulo] = '" + id_modulo + "'" + vbCr +
                            "WHERE id_rol = '" + id_rol + "'"
            Return Me.Fnc_GetSingledataConexion()
        End Function

        Function fnc_obtener_info_rol(ByVal id_rol As String)
            Me.Str_Query = "SELECT id_rol, desc_rol, estado_rol, id_modulo FROM T_Roles WHERE id_rol = " + id_rol
            Dim table As DataTable = Me.FncGetTableQuery()
            Return GetJson(table)
        End Function

        Private Function GetJson(dt As DataTable) As String
            Dim JSSerializer As New JavaScriptSerializer()
            Dim DtRows As New List(Of Dictionary(Of String, Object))()
            Dim newrow As Dictionary(Of String, Object) = Nothing

            For Each drow As DataRow In dt.Rows
                newrow = New Dictionary(Of String, Object)()
                For Each col As DataColumn In dt.Columns
                    newrow.Add(col.ColumnName.Trim(), drow(col))
                Next
                DtRows.Add(newrow)
            Next

            Return JSSerializer.Serialize(DtRows)
        End Function
    End Class
End Namespace