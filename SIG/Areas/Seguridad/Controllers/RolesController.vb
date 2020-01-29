Namespace SIG.Areas.Seguridad.Controllers
    Public Class RolesController
        Inherits System.Web.Mvc.Controller
        Dim login As New Global.SIG.Cl_Login
        Dim Roles As New Global.SIG.SIG.Areas.Seguridad.Models.Roles
        Dim Modulos As New Global.SIG.SIG.Areas.Seguridad.Models.Modulos
        '
        ' GET: /Seguridad/Accesos


        Function ViewAdministraciondeRoles() As ActionResult
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

        Function PartialViewGridRoles() As ActionResult
            Return PartialView(Roles.fnc_lista_roles())
        End Function

        Function PartialViewCbNuevoModulo()
            Return PartialView(Modulos.fnc_lista_modulos())
        End Function

        Function PartialViewCbEditarModulo()
            Return PartialView(Modulos.fnc_lista_modulos)
        End Function

        <HttpPost>
        Function Obtener_info_rol(ByVal id_rol As String)
            Return Roles.fnc_obtener_info_rol(id_rol)
        End Function

        <HttpPost>
        Function Agregar_rol(ByVal desc_rol As String, ByVal estado_rol As String, ByVal id_modulo As String)
            Roles.fnc_agregar_rol(desc_rol, estado_rol, id_modulo)
            Return 0
        End Function

        <HttpPost>
        Function Actualizar_rol(ByVal desc_rol As String, ByVal id_modulo As String, ByVal estado_rol As String, ByVal id_rol As String)
            Roles.fnc_actualizar_rol(desc_rol, estado_rol, id_modulo, id_rol)
            Return 0
        End Function
    End Class
End Namespace