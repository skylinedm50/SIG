Namespace SIG.Areas.Seguridad.Controllers

    Public Class UsuariosController
        Inherits System.Web.Mvc.Controller

        '
        ' GET: /Seguridad/Usuarios
        Dim login As New Global.SIG.Cl_Login
        Dim Usuarios As New Global.SIG.SIG.Areas.Seguridad.Models.Usuarios

        'Function Index() As ActionResult
        '    Return View()
        'End Function

        Function ViewAdministrarUsuarios() As ActionResult
            'Return View()

            If login.Fnc_loggeado() IsNot Nothing Then
                If login.fnc_validarModulo(6) Then
                    'login.Fnc_MenuModulo()
                    If login.Fnc_ValidarPermisoUrl(Request.Url.AbsolutePath) Then
                        Return View()
                    Else
                        Return Redirect("/Seguridad/Home")
                    End If
                Else
                    Return Redirect("/Home/MenuPrincipal")
                End If
            Else
                Return Redirect("/Home/Login")
            End If

        End Function

        <HttpPost> _
        Function obtenerInfoPersona(ByVal identidad As String)
            Return Usuarios.Fnc_obtener_info_persona(identidad)
        End Function

        <HttpPost> _
        Function obtenerRolesPersona(ByVal identidad As String)
            Return Usuarios.Fnc_obtener_roles_persona(identidad)
        End Function

        <HttpPost> _
        Function guardarUsuario(ByVal identidad As String, ByVal nombre1 As String, ByVal nombre2 As String, ByVal apellido1 As String, _
                                ByVal apellido2 As String, ByVal telefono As String, ByVal correo As String, ByVal usuario As String, _
                                 ByVal clave As String, ByVal nroCritico As String, ByVal roles() As String)

            Return Usuarios.Fnc_guardar_nuevo_usuario(identidad, nombre1, nombre2, apellido1, apellido2, telefono, correo, usuario, clave, nroCritico, roles)

        End Function

        <HttpPost> _
        Function actualizarUsuario(ByVal identidad As String, ByVal estado As String, ByVal nombre1 As String, ByVal nombre2 As String, ByVal apellido1 As String, _
                                ByVal apellido2 As String, ByVal telefono As String, ByVal correo As String, ByVal usuario As String, _
                                 ByVal clave As String, ByVal nroCritico As String, ByVal claveDef As String, ByVal codPersona As String, ByVal roles() As String)

            Return Usuarios.Fnc_actualizar_usuario(identidad, estado, nombre1, nombre2, apellido1, apellido2, telefono, correo, usuario, clave, nroCritico, claveDef, codPersona, roles)

        End Function

        Function PartialViewGridPermisos()
            Return PartialView(Usuarios.Fnc_lista_roles())
        End Function

        Function ViewCambioContraseña() As ActionResult

            If login.Fnc_loggeado() IsNot Nothing Then

                Dim table As DataTable = Usuarios.Fnc_obtener_info_usuario(Session("usuario"))
                ViewData("identidad") = table.Rows(0).Item("ide_usr_persona")
                ViewData("nombre") = table.Rows(0).Item("nombre")
                ViewData("telefono") = table.Rows(0).Item("num_tel_usr_persona")
                ViewData("correo") = table.Rows(0).Item("email_usr_persona")
                ViewData("usuario") = table.Rows(0).Item("nom_usuario")
                ViewData("critico") = table.Rows(0).Item("cod_critico_usuario")

                Return View()
            Else
                Return Redirect("/Home/Login")
            End If

        End Function

        <HttpPost> _
        Function actualizarContrasena(ByVal pass As String)

            Return Usuarios.Fnc_actualizar_contrasena(Session("usuario"), pass)

        End Function

    End Class

End Namespace