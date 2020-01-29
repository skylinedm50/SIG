Imports System.Web.Script.Serialization
Imports System.Data.SqlClient
Imports System.Net.Mail
Imports System.Net.NetworkInformation


Namespace SIG.Areas.Seguridad.Models

    Public Class Usuarios
        Inherits SIG.Areas.Seguridad.Models.Cl_Conexion
        Private ReadOnly key() As Byte = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14,
          15, 16, 17, 18, 19, 20, 21, 22, 23, 24}
        Private ReadOnly iv() As Byte = {8, 7, 6, 5, 4, 3, 2, 1}
        Private des As New cTripleDES(key, iv)
        Function Fnc_obtener_info_persona(ByVal identidad As String)

            Me.Str_Query = "" + vbCr +
                "SELECT per.ide_usr_persona, nom1_usr_persona, nom2_usr_persona, ape1_usr_persona, ape2_usr_persona," + vbCr +
                "   num_tel_usr_persona, email_usr_persona, nom_usuario, clv_usuario, cod_critico_usuario, " + vbCr +
                "   Estado, clave_def_usuario, per.cod_usr_persona" + vbCr +
                "FROM t_usr_personas AS per" + vbCr +
                "	INNER JOIN T_usuarios AS usu ON usu.cod_usr_persona = per.cod_usr_persona" + vbCr +
                "WHERE per.ide_usr_persona = '" + identidad + "'"

            Dim table As DataTable = Me.FncGetTableQuery()
            Return GetJson(table)

        End Function

        Function Fnc_lista_departamentos()
            Me.Str_Query = "SELECT [id_unidad],[desc_unidad],CASE WHEN estado = 1 THEN 'SI' ELSE 'NO' END habilitado FROM [SIG_T].[dbo].[T_unidad]"
            Return Me.FncGetTableQuery()
        End Function

        Function Fnc_guardar_departamento(ByVal desc_unidad As String, ByVal estado As String)
            Me.Str_Query = "INSERT INTO [dbo].[T_unidad]([desc_unidad], [estado])" + vbCr +
                           "VALUES('" + desc_unidad + "','" + estado + "')"
            Return Me.Fnc_GetSingledataConexion()
        End Function

        Function fnc_obtener_info_usuario(ByVal cod_usuario As String)

            Me.Str_Query = "Select per.ide_usr_persona, per.nom1_usr_persona + ' ' + per.nom2_usr_persona + ' ' + per.ape1_usr_persona + ' '" + vbCr +
                "+ per.ape2_usr_persona AS 'nombre', per.num_tel_usr_persona, per.email_usr_persona, usu.nom_usuario," + vbCr +
                "usu.cod_critico_usuario, usu.Estado,usu.cod_usuario,per.cod_usr_persona, per.nom1_usr_persona, per.nom2_usr_persona, per.ape1_usr_persona, per.ape2_usr_persona," + vbCr +
                "usu.usr_jefe, usu.id_unidad" + vbCr +
                "FROM t_usr_personas AS per" + vbCr +
                "INNER JOIN T_usuarios AS usu ON usu.cod_usr_persona = per.cod_usr_persona" + vbCr +
                "WHERE usu.cod_usuario = " + cod_usuario

            Dim table As DataTable = Me.FncGetTableQuery()
            Return GetJson(table)
            ' Return Me.FncGetTableQuery()
        End Function

        Function fnc_actualizar_depto(ByVal id_unidad As String, ByVal desc_unidad As String, ByVal estado As String)
            Me.Str_Query = "UPDATE [dbo].[T_unidad]  SET [desc_unidad] = '" + desc_unidad + "', [estado] = '" + estado + "' WHERE id_unidad = '" + id_unidad + "'"
            Dim table As DataTable = Me.FncGetTableQuery()
            Return GetJson(table)
        End Function

        Function fnc_obtener_depto(ByVal id_unidad As String)
            Me.Str_Query = "SELECT [id_unidad], [desc_unidad], [estado] FROM [SIG_T].[dbo].[T_unidad] WHERE id_unidad = '" + id_unidad + "'"
            Dim table As DataTable = Me.FncGetTableQuery()
            Return GetJson(table)
        End Function

        Function fnc_obtener_info_usuario2(ByVal cod_usuario As String)

            Me.Str_Query = "SELECT per.ide_usr_persona, per.nom1_usr_persona + ' ' + per.nom2_usr_persona + ' ' + per.ape1_usr_persona + ' '" + vbCr +
                "+ per.ape2_usr_persona AS 'nombre', per.num_tel_usr_persona, per.email_usr_persona, usu.nom_usuario," + vbCr +
                "usu.cod_critico_usuario, usu.Estado,usu.cod_usuario,per.cod_usr_persona, per.nom1_usr_persona, per.nom2_usr_persona, per.ape1_usr_persona, per.ape2_usr_persona" + vbCr +
                "FROM t_usr_personas AS per" + vbCr +
                "INNER JOIN T_usuarios AS usu ON usu.cod_usr_persona = per.cod_usr_persona" + vbCr +
                "WHERE usu.cod_usuario = " + cod_usuario


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

        Function Fnc_obtener_Usuarios()
            Me.Str_Query = "SELECT DISTINCT usr.cod_usuario, usr.nom_usuario,ISNULL(per.nom1_usr_persona,'') + ' ' + ISNULL(per.nom2_usr_persona,'') + ' '" + vbCr +
                "+ ISNULL(per.ape1_usr_persona,'')+ ' ' + ISNULL(per.ape2_usr_persona,'') nombre_completo, per.ide_usr_persona,per.num_tel_usr_persona, per.email_usr_persona" + vbCr +
                ", case when usr.Estado = 1 then 'Si' else 'No' end Estado, usr.fecha_registro,usr.fecha_baja FROM T_usuarios AS usr" + vbCr +
                "INNER JOIN t_usr_personas AS per ON usr.cod_usr_persona = per.cod_usr_persona"

            Return Me.FncGetTableQuery()
        End Function

        Function Fnc_lista_roles()

            Me.Str_Query = "" + vbCr +
                "SELECT DISTINCT rol.id_rol,modu.nom_modulo AS 'modulo',rol.desc_rol FROM T_Roles rol" + vbCr +
                "INNER JOIN T_rol_actividad AS rolact ON rolact.id_rol =  rol.id_rol " + vbCr +
                "INNER JOIN T_actividad AS act ON act.id_actividad = rolact.id_actividad" + vbCr +
                "INNER JOIN T_opciones AS opc ON opc.id_opcion = act.id_opcion" + vbCr +
                "INNER JOIN T_Modulos AS modu ON modu.id_modulos = opc.id_modulo " + vbCr +
                "WHERE rol.estado_rol = 1" + vbCr +
                "ORDER BY modu.nom_modulo "
            Return Me.FncGetTableQuery()

        End Function

        Function Fnc_lista_roles_por_usuario(ByVal cod_usuario As String)
            'MODIFICACION
            Me.Str_Query = "SELECT roles.id_rol,roles.desc_rol,modu.nom_modulo AS modulo, 0 AS habilitado FROM T_Roles roles" + vbCr +
                " LEFT JOIN t_usuarios_roles usr_roles ON usr_roles.id_rol= roles.id_rol" + vbCr +
                " INNER JOIN T_Modulos modu ON modu.id_modulos = roles.id_modulo" + vbCr +
                " WHERE roles.id_rol NOT IN (SELECT DISTINCT rol.id_rol FROM t_usuarios_roles usr_rol" + vbCr +
                " INNER JOIN T_Roles rol ON rol.id_rol = usr_rol.id_rol" + vbCr +
                " INNER JOIN T_Modulos modu ON modu.id_modulos = rol.id_modulo WHERE cod_usuario = '" + cod_usuario + "')" + vbCr +
                " GROUP BY roles.id_rol,roles.desc_rol,modu.nom_modulo UNION SELECT DISTINCT rol.id_rol,rol.desc_rol,modu.nom_modulo,1 AS habilitado" + vbCr +
                " FROM t_usuarios_roles usr_rol" + vbCr +
                " INNER JOIN T_Roles rol ON rol.id_rol = usr_rol.id_rol" + vbCr +
                " INNER JOIN T_Modulos modu ON modu.id_modulos = rol.id_modulo" + vbCr +
                " WHERE cod_usuario = '" + cod_usuario + "' ORDER BY desc_rol"

            Return Me.FncGetTableQuery()
        End Function

        Function Fnc_RegistrarUsuario(ByVal identidad As String, ByVal nombre1 As String, ByVal nombre2 As String, ByVal apellido1 As String,
                                ByVal apellido2 As String, ByVal telefono As String, ByVal correo As String, ByVal nroCritico As String,
                                      ByVal roles() As String, ByVal jefe As String, ByVal id_unidad As String)
            If My.Computer.Network.IsAvailable() Then
                nombre1 = nombre1.ToUpper()
                nombre2 = nombre2.ToUpper()
                apellido1 = apellido1.ToUpper()
                apellido2 = apellido2.ToUpper()


                Try
                    Dim usuario As String
                    Dim clave As String
                    Dim ClaveEncp As String
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



                    usuario = nombre1 + "." + apellido1
                    usuario = usuario.ToLower()

                    Me.Str_Query = "SELECT CASE WHEN (SELECT [nom_usuario] FROM [SIG_T].[dbo].[T_usuarios] WHERE nom_usuario = '" + usuario + "') IS NULL THEN ''" + vbCr +
                                    "ELSE (SELECT [nom_usuario] FROM [SIG_T].[dbo].[T_usuarios] WHERE nom_usuario = '" + usuario + "') END nom_usuario"
                    Dim usr As String
                    Dim table As DataTable = Me.FncGetTableQuery()
                    usr = table.Rows(0).Item("nom_usuario")

                    If usuario.Equals(usr) Then
                        usuario = nombre1 + "_" + apellido1
                        usuario = usuario.ToLower()

                        Me.Str_Query = "SELECT CASE WHEN (SELECT [nom_usuario] FROM [SIG_T].[dbo].[T_usuarios] WHERE nom_usuario = '" + usuario + "') IS NULL THEN ''" + vbCr +
                                       "ELSE (SELECT [nom_usuario] FROM [SIG_T].[dbo].[T_usuarios] WHERE nom_usuario = '" + usuario + "') END nom_usuario"
                        Dim usr2 As String
                        Dim table2 As DataTable = Me.FncGetTableQuery()
                        usr2 = table2.Rows(0).Item("nom_usuario")

                        If usuario.Equals(usr2) Then
                            usuario = nombre1 + "-" + apellido1
                            usuario = usuario.ToLower()
                        End If

                    End If

                    clave = GenerarClave()

                    ClaveEncp = des.Encrypt(clave)

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
                    comand.Parameters.AddWithValue("@clave", ClaveEncp)
                    comand.Parameters.AddWithValue("@nroCritico", nroCritico)
                    comand.Parameters.AddWithValue("@roles", t_rol_lista)
                    comand.Parameters.AddWithValue("@jefe", jefe)
                    comand.Parameters.AddWithValue("@id_unidad", id_unidad)


                    EnviarCorreo(usuario, clave, nombre1, apellido1, correo, "REGISTRO USUARIO SIG", "Bienvenido al SIG",
                                 "Tu registro en el sistema se ha realizado exitosamente, el usuario y contraseña para acceder son los siguientes:",
                                 "Por motivos de seguridad deberás cambiar tu contraseña al ingresar por primera vez al sistema.")


                    Me.FncEjecutarProcedimiento(comand)
                    Return 1
                Catch ex As Exception

                    Return 0
                End Try
            Else

                Return 0
            End If
            Return 0
        End Function

        Function Fnc_actualizar_usuario(ByVal identidad As String, ByVal estado As String, ByVal nombre1 As String, ByVal nombre2 As String, ByVal apellido1 As String,
                                ByVal apellido2 As String, ByVal telefono As String, ByVal correo As String, ByVal codUsuario As String, ByVal jefe As String, ByVal id_unidad As String)


            Try

                Dim comand As New SqlCommand()

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
                comand.Parameters.AddWithValue("@codUsuario", codUsuario)
                comand.Parameters.AddWithValue("@usr_jefe", jefe)
                comand.Parameters.AddWithValue("@id_unidad", id_unidad)

                Me.FncEjecutarProcedimiento(comand)
                Return 1
            Catch ex As Exception
                MsgBox(ex.Message)
                Return 0
            End Try

            Return 0
        End Function

        Function Fnc_actualizar_contrasena(ByVal usuario As String, ByVal pass As String)
            Dim ClaveEncp As String
            ClaveEncp = des.Encrypt(pass)

            Me.Str_Query = "" + vbCr +
                "UPDATE T_usuarios" + vbCr +
                "SET clv_usuario = '" + ClaveEncp + "', clave_def_usuario = 1" + vbCr +
                "WHERE cod_usuario = " + usuario

            Return Me.Fnc_ExecQuery()

        End Function

        Function Fnc_EliminarRolporUsuario(ByVal id_rol As String, ByVal cod_usuario As String)
            Me.Str_Query = "DELETE FROM [dbo].[t_usuarios_roles] WHERE cod_usuario = '" + cod_usuario + "' AND id_rol = '" + id_rol + "'"
            Return Me.Fnc_GetSingledataConexion()
        End Function

        Function Fnc_AgregarRolporUsuario(ByVal id_rol As String, ByVal cod_usuario As String)
            Me.Str_Query = "INSERT INTO [dbo].[t_usuarios_roles]([cod_usuario],[id_rol]) VALUES('" + cod_usuario + "','" + id_rol + "')"
            Return Me.Fnc_GetSingledataConexion()
        End Function

        Function Fnc_Cambiar_contrasena(ByVal cod_usuario As String)
            Me.Str_Query = "UPDATE [dbo].[T_usuarios] SET [clave_def_usuario] = 0 WHERE cod_usuario = '" + cod_usuario + "'"
            Return Me.Fnc_GetSingledataConexion()
        End Function

        Function Fnc_Recordar_Contrasena(ByVal cod_usuario As String)
            If My.Computer.Network.IsAvailable() Then
                Try
                    Me.Str_Query = "SELECT per.nom1_usr_persona+' '+per.ape1_usr_persona as nombre, usu.nom_usuario, usu.clv_usuario, per.email_usr_persona" + vbCr +
                            "FROM T_usuarios usu INNER JOIN t_usr_personas per on usu.cod_usr_persona = per.cod_usr_persona WHERE usu.cod_usuario = '" + cod_usuario + "'"
                    Dim usuario As String
                    Dim nombre As String
                    Dim correo As String
                    Dim clave As String
                    Dim table As DataTable = Me.FncGetTableQuery()


                    nombre = table.Rows(0).Item("nombre")
                    usuario = table.Rows(0).Item("nom_usuario")
                    clave = table.Rows(0).Item("clv_usuario")
                    correo = table.Rows(0).Item("email_usr_persona")

                    clave = des.Decrypt(clave)

                    EnviarCorreo(usuario, clave, nombre, "", correo, "MENSAJE", "SIG", "El usuario y contraseña para ingresar al sistema son los siguientes:", "")

                    Return 0
                Catch ex As Exception
                    MsgBox("Hubo un error")
                    Return 1
                End Try
            End If
            Return 1
        End Function

        Function Fnc_Buscar_Correo(ByVal email As String, ByVal usuario As String)
            Dim nombre As String
            Dim clave As String
            Dim Nueva_clave As String
            Dim Nueva_Clave_enc As String
            usuario = usuario.Replace("'", "")
            email = email.Replace("'", "")
            If My.Computer.Network.IsAvailable() Then
                Try

                    Nueva_clave = GenerarClave()
                    Nueva_Clave_enc = des.Encrypt(Nueva_clave)

                    Me.Str_Query = "EXEC P_Obtener_Clave_Usuario @email ='" + email + "', @usuario ='" + usuario + "', @Nueva_clave ='" + Nueva_Clave_enc + "';"

                    Dim table As DataTable = Me.FncGetTableQuery()
                    nombre = table.Rows(0).Item("nombre")
                    clave = table.Rows(0).Item("clv_usuario")
                    clave = des.Decrypt(clave)

                    EnviarCorreo(usuario, clave, nombre, "", email, "Recuperación de Contraseña", "SIG", "El usuario y contraseña para ingresar al sistema son los siguientes:", "Por motivos de seguridad deberás cambiar tu contraseña al ingresar al sistema.")

                    Return 0

                Catch ex As Exception

                    Return 1
                End Try

            End If

            Return 1
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

        Private Function GenerarClave() As String
            Dim Random As New Random()
            Dim Letras() As String = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L",
        "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"} '25
            Dim Numeros() As String = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9"} '10
            Dim CarEsp() As String = {"@", ".", "-"}
            Dim Clave As String
            Clave = ""
            For i As Integer = 0 To 5
                Clave = Clave + Letras(Random.Next(25))
            Next
            Clave = Clave + CarEsp(Random.Next(3))
            Clave = Clave + Numeros(Random.Next(10))

            Return Clave
        End Function

        Sub EnviarCorreo(ByVal usuario As String, ByVal clave As String, ByVal Pnombre As String, ByVal Papellido As String, ByVal sendmail As String,
                ByVal Asunto As String, ByVal titulo As String, ByVal principal As String, ByVal descripcion As String)
            Dim SmtpServer As New SmtpClient("smtp.gmail.com", 587)
            Dim contenthtml As String
            contenthtml = "


            <!DOCTYPE html> 

            <head>
                <meta http-equiv='Content-Type' content='text/html; charset=utf-8' />
                <meta name='viewport' content='width=device-width'/>

                <!-- For development, pass document through inliner -->

                <style type='text/css'>
                * { margin: 0; padding: 0; font-size: 100%; font-family: 'Avenir Next', 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; line-height: 1.65; }

                                img { max-width: 100%; margin: 0 auto; display: block; }

                                body, .body-wrap { width: 100% !important; height: 100%; background: #f8f8f8; }

                                a { color: #71bc37; text-decoration: none; }

                                a:hover { text-decoration: underline; }

                                .text-center { text-align: center; }

                                .text-right { text-align: right; }

                                .text-left { text-align: left; }

                                .button { display: inline-block; color: white; background: #157dff; border: solid #157dff; border-width: 10px 20px 8px; font-weight: bold; border-radius: 4px; }

                                .button:hover { text-decoration: none; }

                                h1, h2, h3, h4, h5, h6 { margin-bottom: 20px; line-height: 1.25; }

                                h1 { font-size: 32px; }

                                h2 { font-size: 28px; }

                                h3 { font-size: 24px; }

                                h4 { font-size: 20px; }

                                h5 { font-size: 16px; }

                                p, ul, ol { font-size: 16px; font-weight: normal; margin-bottom: 20px; }

                                .container { display: block !important; clear: both !important; margin: 0 auto !important; max-width: 580px !important; }

                                .container table { width: 100% !important; border-collapse: collapse; }

                                .container .masthead { padding: 80px 0; background: #157dff; color: white; }

                                .container .masthead h1 { margin: 0 auto !important; max-width: 90%; text-transform: uppercase; }

                                .container .content { background: white; padding: 30px 35px; }

                                .container .content.footer { background: none; }

                                .container .content.footer p { margin-bottom: 0; color: #888; text-align: center; font-size: 14px; }

                                .container .content.footer a { color: #888; text-decoration: none; font-weight: bold; }

                                .container .content.footer a:hover { text-decoration: underline; }
                </style>
            </head>
            <body>
            <table class='body-wrap'>
                <tr>
                    <td class='container'>

                        <!-- Message start -->
                        <table>
                            <tr>
                                <td align='center' class='masthead'>

                                    <h1>" + titulo + "</h1>

                                </td>
                            </tr>
                            <tr>
                                <td class='content'>

                                    <h2>Hola " + Pnombre + " " + Papellido + "</h2>
                                    <p>" + principal + "</p>
                                    <h5>Nombre Usuario: " + usuario + "</h5>
                                    <h5>Contrase&ntilde;a: " + clave + "</h5>
                                    <p>" + descripcion + "</p>
                                    <p>Por favor, evita compartir tu contrase&ntilde;a con terceros. Si lo haces, estar&iacute;as exponiendo informaci&oacute;n que puede comprometer la privacidad y seguridad del sistema.</p>
                                    <table>
                                        <tr>
                                            <td align='center'>
                                                <p>
                                                    <a href='http://sigvm.com/' class='button'>Entrar</a>
                                                </p>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>

                    </td>
                </tr>
                <tr>
                    <td class='container'>
                        <!-- Message start -->
                        <table>
                            <tr>
                                <td class='content footer' align='center'>
                    	            <p>Este e-mail es de caracter informativo y se ha generado por un sistema autom&aacute;tico. Por favor, no respondas a este e-mail directamente.</p>
                                    <p>Enviado por <a href='#'>SIG</a></p>
                   
                                </td>
                            </tr>
                        </table>

                    </td>
                </tr>
            </table>
            </body>
            </html>
                                 "
            SmtpServer.Credentials = New Net.NetworkCredential("ModuloSeguridad.SIG@gmail.com", "LordD@rk9")
            Dim mail As New MailMessage("ModuloSeguridad.SIG@gmail.com", sendmail, Asunto, contenthtml)
            mail.IsBodyHtml = True

            SmtpServer.EnableSsl = True
            SmtpServer.Send(mail)
        End Sub
    End Class

End Namespace
