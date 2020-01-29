Imports System.Web.Script.Serialization
Namespace SIG.Areas.Seguridad.Models
    Public Class Accesos
        Inherits SIG.Areas.Seguridad.Models.Cl_Conexion

        Function fnc_lista_roles()
            Me.Str_Query = "SELECT roles.id_rol, roles.desc_rol, modu.nom_modulo" + vbCr +
               "FROM T_Roles AS roles" + vbCr +
               "INNER JOIN T_Modulos modu on roles.id_modulo = modu.id_modulos" + vbCr +
               "ORDER BY nom_modulo"
            Return Me.FncGetTableQuery()
        End Function

        Function fnc_lista_pantallas(ByVal id_rol As String)
            Me.Str_Query = "SELECT id_rol_actividad,act.desc_actividad,opc.desc_opcion,modu.nom_modulo," + vbCr +
               "case when estado_rol_actividad = 1 then 'Si' else 'No' end estado_rol_actividad" + vbCr +
               "FROM T_rol_actividad AS rolact" + vbCr +
               "INNER JOIN T_actividad act ON act.id_actividad = rolact.id_actividad " + vbCr +
               "INNER JOIN T_opciones opc ON opc.id_opcion = act.id_opcion" + vbCr +
               "INNER JOIN T_Modulos modu ON modu.id_modulos = opc.id_modulo" + vbCr +
               "WHERE rolact.id_rol = " + id_rol
            Return Me.FncGetTableQuery()
        End Function

        Function fnc_obtener_rol_pantalla(ByVal id_rol_actividad As String)
            Me.Str_Query = "SELECT id_rol_actividad, rol.desc_rol, act.desc_actividad FROM T_rol_actividad rolact" + vbCr +
                "INNER JOIN T_Roles rol ON rolact.id_rol= rol.id_rol" + vbCr +
                "INNER JOIN T_actividad act ON act.id_actividad =rolact.id_actividad" + vbCr +
                "WHERE rolact.id_rol_actividad = '" + id_rol_actividad + "'"
            Dim table As DataTable = Me.FncGetTableQuery()
            Return GetJson(table)
        End Function

        Function fnc_agregar_acceso(ByVal id_actividad As String, ByVal id_rol As String)
            Me.Str_Query = "INSERT INTO T_rol_actividad(id_rol,id_actividad,estado_rol_actividad) VALUES ('" + id_rol + "','" + id_actividad + "',1)"
            Return Me.Fnc_GetSingledataConexion()
        End Function

        Function fnc_eliminar_acceso(ByVal id_rol_actividad As String)
            Me.Str_Query = "DELETE FROM T_rol_actividad WHERE id_rol_actividad = '" + id_rol_actividad + "'"
            Return Me.FncGetTableQuery()
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