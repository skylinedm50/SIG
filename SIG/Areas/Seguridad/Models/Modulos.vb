Imports System.Web.Script.Serialization
Imports System.Data.SqlClient

Namespace SIG.Areas.Seguridad.Models
    Public Class Modulos
        Inherits SIG.Areas.Seguridad.Models.Cl_Conexion
        Function fnc_lista_modulos()
            Me.Str_Query = "" + vbCr +
               "SELECT id_modulos" + vbCr +
               ",nom_modulo " + vbCr +
               ",estado_modulo" + vbCr +
               "FROM T_Modulos ORDER BY nom_modulo"
            Return Me.FncGetTableQuery()
        End Function

        Function fnc_lista_opciones(ByVal id_modulos As String)
            Me.Str_Query = "SELECT id_opcion,desc_opcion FROM T_opciones WHERE id_modulo = '" + id_modulos + "' ORDER BY desc_opcion"
            Return Me.FncGetTableQuery()

        End Function

        Function fnc_lista_pantallas(ByVal id_opcion As String)
            Me.Str_Query = "SELECT id_actividad , desc_actividad FROM T_actividad WHERE id_opcion = '" + id_opcion + "' ORDER BY desc_actividad"
            Return Me.FncGetTableQuery()
        End Function

        Function fnc_agregar_modulo(ByVal nom_modulo As String, ByVal estado_modulo As String, ByVal icono As String)
            Dim url As String
            url = "/" + nom_modulo.Replace(" "c, String.Empty) + "/Home"
            Me.Str_Query = "INSERT INTO T_Modulos(nom_modulo,estado_modulo,url,icono)" + vbCr +
                            "VALUES('" + nom_modulo + "','" + estado_modulo + "','" + url + "','" + icono + "')"
            Return Me.Fnc_GetSingledataConexion()
        End Function

        Function fnc_agregar_opcion(ByVal desc_opcion As String, ByVal estado_opcion As String, ByVal id_modulo As String)
            Me.Str_Query = "INSERT INTO T_opciones(desc_opcion, estado_opcion, id_modulo)" + vbCr +
                             "VALUES('" + desc_opcion + "','" + estado_opcion + "','" + id_modulo + "')"
            Return Me.Fnc_GetSingledataConexion()
        End Function

        Function fnc_agregar_pantalla(ByVal desc_actividad As String, ByVal id_opcion As String, ByVal estado_actividad As String, ByVal url As String)
            Me.Str_Query = "INSERT INTO T_actividad(desc_actividad, id_opcion, estado_actividad, url)" + vbCr +
                            "VALUES('" + desc_actividad + "','" + id_opcion + "','" + estado_actividad + "','" + url + "')"
            Return Me.Fnc_GetSingledataConexion()
        End Function

        Function fnc_obtener_info_modulo(ByVal id_modulo As String)
            Me.Str_Query = "SELECT id_modulos, nom_modulo, estado_modulo, url, icono FROM T_Modulos WHERE id_modulos = '" + id_modulo + "'"
            Dim table As DataTable = Me.FncGetTableQuery()
            Return GetJson(table)
        End Function

        Function fnc_obtener_info_opcion(ByVal id_opcion As String)
            Me.Str_Query = "SELECT id_opcion, desc_opcion, estado_opcion FROM T_opciones WHERE id_opcion = '" + id_opcion + "'"
            Dim table As DataTable = Me.FncGetTableQuery()
            Return GetJson(table)
        End Function

        Function fnc_obtener_info_pantalla(ByVal id_pantalla As String)
            Me.Str_Query = "SELECT id_actividad,desc_actividad,estado_actividad,url FROM T_actividad WHERE id_actividad = '" + id_pantalla + "'"
            Dim table As DataTable = Me.FncGetTableQuery()
            Return GetJson(table)
        End Function

        Function fnc_actualizar_modulo(ByVal nom_modulo As String, ByVal estado_modulo As String, ByVal icono As String, ByVal id_modulo As String)
            Me.Str_Query = "UPDATE [dbo].[T_Modulos] SET [nom_modulo] = '" + nom_modulo + "', [estado_modulo] ='" + estado_modulo + "', [icono] = '" + icono + "'" + vbCr +
                            "WHERE id_modulos = '" + id_modulo + "'"
            Return Me.Fnc_GetSingledataConexion()
        End Function

        Function fnc_actualizar_opcion(ByVal desc_opcion As String, ByVal estado_opcion As String, ByVal id_modulo As String, ByVal id_opcion As String)
            Me.Str_Query = "UPDATE [dbo].[T_opciones] SET [desc_opcion] = '" + desc_opcion + "', [estado_opcion] = '" + estado_opcion + "', [id_modulo] = '" + id_modulo + "'" + vbCr +
                           "WHERE id_opcion = '" + id_opcion + "'"
            Return Me.Fnc_GetSingledataConexion()
        End Function

        Function fnc_actualizar_pantalla(ByVal desc_actividad As String, ByVal id_opcion As String, ByVal estado_actividad As String, ByVal url As String, ByVal id_actividad As String)
            Me.Str_Query = "UPDATE [dbo].[T_actividad] SET [desc_actividad] = '" + desc_actividad + "', [id_opcion] = '" + id_opcion + "', [estado_actividad] = '" + estado_actividad + "', [url] = '" + url + "'" + vbCr +
                            "WHERE id_actividad = '" + id_actividad + "'"
            Return Me.Fnc_GetSingledataConexion()
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