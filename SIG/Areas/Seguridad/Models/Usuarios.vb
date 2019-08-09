Imports System.Web.Script.Serialization
Imports System.Data.SqlClient

Namespace SIG.Areas.Seguridad.Models

    Public Class Usuarios
        Inherits SIG.Areas.Seguridad.Models.Cl_Conexion

        Function Fnc_obtener_info_persona(ByVal identidad As String)

            Me.Str_Query = "" + vbCr + _
                "SELECT per.ide_usr_persona, nom1_usr_persona, nom2_usr_persona, ape1_usr_persona, ape2_usr_persona," + vbCr + _
                "   num_tel_usr_persona, email_usr_persona, nom_usuario, clv_usuario, cod_critico_usuario, " + vbCr + _
                "   Estado, clave_def_usuario, per.cod_usr_persona" + vbCr + _
                "FROM t_usr_personas AS per" + vbCr + _
                "	INNER JOIN T_usuarios AS usu ON usu.cod_usr_persona = per.cod_usr_persona" + vbCr + _
                "WHERE per.ide_usr_persona = '" + identidad + "'"

            Dim table As DataTable = Me.FncGetTableQuery()
            Return GetJson(table)

        End Function

        Function Fnc_obtener_info_usuario(ByVal usuario As String)

            Me.Str_Query = "" + vbCr + _
                "SELECT per.ide_usr_persona, per.nom1_usr_persona + ' ' + per.nom2_usr_persona + ' ' + per.ape1_usr_persona + ' ' " + vbCr + _
                "   + per.ape2_usr_persona AS 'nombre', per.num_tel_usr_persona, per.email_usr_persona, usu.nom_usuario," + vbCr + _
                "   usu.cod_critico_usuario" + vbCr + _
                "FROM t_usr_personas AS per" + vbCr + _
                "	INNER JOIN T_usuarios AS usu ON usu.cod_usr_persona = per.cod_usr_persona" + vbCr + _
                "WHERE usu.cod_usuario = " + usuario

            'Dim tabla As DataTable = Me.FncGetTableQuery()
            Return Me.FncGetTableQuery()

        End Function

        Function Fnc_obtener_roles_persona(ByVal identidad As String)

            Me.Str_Query = "" + vbCr + _
                "SELECT usu_rol.id_rol" + vbCr + _
                "FROM t_usr_personas AS per" + vbCr + _
                "	INNER JOIN t_usuarios AS usu ON usu.cod_usr_persona = per.cod_usr_persona" + vbCr + _
                "	INNER JOIN t_usuarios_roles AS usu_rol ON usu_rol.cod_usuario = usu.cod_usuario" + vbCr + _
                "WHERE per.ide_usr_persona = '" + identidad + "'"

            Dim table As DataTable = Me.FncGetTableQuery()
            Return GetJson(table)
        End Function

        Function Fnc_lista_roles()

            Me.Str_Query = "" + vbCr + _
                "SELECT rol.id_rol," + vbCr + _
                "	CASE" + vbCr + _
                "		WHEN modu.id_modulos = 1 THEN 'Incorporaciones'" + vbCr + _
                "		WHEN modu.id_modulos = 2 THEN 'Corresponsabilidad'" + vbCr + _
                "		WHEN modu.id_modulos = 3 THEN 'Planilla'" + vbCr + _
                "		WHEN modu.id_modulos = 4 THEN 'Contraloría y Cierre'" + vbCr + _
                "		WHEN modu.id_modulos = 5 THEN 'Atención al Participante'" + vbCr + _
                "		WHEN modu.id_modulos = 6 THEN 'Seguridad'" + vbCr + _
                "	END AS 'modulo',  rol.desc_rol" + vbCr + _
                "FROM T_Modulos AS modu" + vbCr + _
                "	 INNER JOIN T_Roles AS rol ON rol.id_modulo = modu.id_modulos" + vbCr + _
                "ORDER BY modu.id_modulos"

            Return Me.FncGetTableQuery()

        End Function

        Function Fnc_guardar_nuevo_usuario(ByVal identidad As String, ByVal nombre1 As String, ByVal nombre2 As String, ByVal apellido1 As String, _
                                ByVal apellido2 As String, ByVal telefono As String, ByVal correo As String, ByVal usuario As String, _
                                 ByVal clave As String, ByVal nroCritico As String, ByVal roles() As String)

            Dim t_rol_lista As New DataTable("t_rol_lista")
            Dim comand As New SqlCommand()

            t_rol_lista.Columns.Add("id", System.Type.GetType("System.Int32"))
            t_rol_lista.Columns.Add("id_rol", System.Type.GetType("System.Int32"))

            Dim i As Integer = 1

            For Each rol In roles
                Dim row As DataRow = t_rol_lista.NewRow()
                row.Item("id") = i
                row.Item("id_rol") = rol
                t_rol_lista.Rows.Add(row)

                i += 1
            Next

            'Me.Str_Query = "EXEC p_nuevo_usuario '" + identidad + "', '" + nombre1 + "', '" + nombre2 + "', '" + apellido1 + _
            '    "', '" + apellido2 + "', '" + telefono + "', '" + correo + "', '" + usuario + "', '" + clave + "', " + _
            '    nroCritico + ", " + t_rol_lista

            comand.CommandText = "p_nuevo_usuario"
            comand.CommandType = CommandType.StoredProcedure

            comand.Parameters.AddWithValue("@identidad", identidad)
            comand.Parameters.AddWithValue("@nombre1", nombre1)
            comand.Parameters.AddWithValue("@nombre2", nombre2)
            comand.Parameters.AddWithValue("@apellido1", apellido1)
            comand.Parameters.AddWithValue("@apellido2", apellido2)
            comand.Parameters.AddWithValue("@telefono", telefono)
            comand.Parameters.AddWithValue("@correo", correo)
            comand.Parameters.AddWithValue("@usuario", usuario)
            comand.Parameters.AddWithValue("@clave", clave)
            comand.Parameters.AddWithValue("@nroCritico", nroCritico)
            comand.Parameters.AddWithValue("@roles", t_rol_lista)


            Return Me.FncEjecutarProcedimiento(comand)
        End Function

        Function Fnc_actualizar_usuario(ByVal identidad As String, ByVal estado As String, ByVal nombre1 As String, ByVal nombre2 As String, ByVal apellido1 As String, _
                                ByVal apellido2 As String, ByVal telefono As String, ByVal correo As String, ByVal usuario As String, _
                                 ByVal clave As String, ByVal nroCritico As String, ByVal claveDef As String, ByVal codPersona As String, ByVal roles() As String)

            Dim t_rol_lista As New DataTable("t_rol_lista")
            Dim comand As New SqlCommand()

            t_rol_lista.Columns.Add("id", System.Type.GetType("System.Int32"))
            t_rol_lista.Columns.Add("id_rol", System.Type.GetType("System.Int32"))

            Dim i As Integer = 1

            For Each rol In roles
                Dim row As DataRow = t_rol_lista.NewRow()
                row.Item("id") = i
                row.Item("id_rol") = rol
                t_rol_lista.Rows.Add(row)

                i += 1
            Next

            comand.CommandText = "p_actualizar_usuario"
            comand.CommandType = CommandType.StoredProcedure

            comand.Parameters.AddWithValue("@identidad", identidad)
            comand.Parameters.AddWithValue("@estado", estado)
            comand.Parameters.AddWithValue("@nombre1", nombre1)
            comand.Parameters.AddWithValue("@nombre2", nombre2)
            comand.Parameters.AddWithValue("@apellido1", apellido1)
            comand.Parameters.AddWithValue("@apellido2", apellido2)
            comand.Parameters.AddWithValue("@telefono", telefono)
            comand.Parameters.AddWithValue("@correo", correo)
            comand.Parameters.AddWithValue("@usuario", usuario)
            comand.Parameters.AddWithValue("@clave", clave)
            comand.Parameters.AddWithValue("@nroCritico", nroCritico)
            comand.Parameters.AddWithValue("@claveDef", claveDef)
            comand.Parameters.AddWithValue("@codPersona", codPersona)
            comand.Parameters.AddWithValue("@roles", t_rol_lista)


            Return Me.FncEjecutarProcedimiento(comand)
        End Function

        Function Fnc_actualizar_contrasena(ByVal usuario As String, ByVal pass As String)

            Me.Str_Query = "" + vbCr + _
                "UPDATE T_usuarios" + vbCr + _
                "SET clv_usuario = '" + pass + "', clave_def_usuario = 1" + vbCr + _
                "WHERE cod_usuario = " + usuario

            Return Me.Fnc_ExecQuery()

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
