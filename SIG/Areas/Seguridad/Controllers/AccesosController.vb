Namespace SIG.Areas.Seguridad.Controllers
    Public Class AccesosController
        Inherits System.Web.Mvc.Controller
        Dim login As New Global.SIG.Cl_Login
        Dim Accesos As New Global.SIG.SIG.Areas.Seguridad.Models.Accesos
        Dim Modulos As New Global.SIG.SIG.Areas.Seguridad.Models.Modulos
        '
        ' GET: /Seguridad/Accesos



        Function ViewAdministraciondeAccesos() As ActionResult
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

        Function PartialViewGridRolesAccesos()
            Return PartialView(Accesos.fnc_lista_roles())
        End Function

        Function PartialViewGridPantallasAccesos(ByVal id_rol As String) As ActionResult
            If id_rol <> "" Then
                ViewData("id_rol") = id_rol
                Return PartialView(Accesos.fnc_lista_pantallas(id_rol))
            End If
            Return PartialView()
        End Function

        <HttpPost>
        Function Obtener_info_rol_pantalla(ByVal id_rol_actividad As String)
            Return Accesos.fnc_obtener_rol_pantalla(id_rol_actividad)
        End Function

        <HttpPost>
        Function Agregar_acceso(ByVal id_actividad As String, ByVal id_rol As String)
            Accesos.fnc_agregar_acceso(id_actividad, id_rol)
            Return 0
        End Function

        <HttpPost>
        Function Eliminar_acceso(ByVal id_rol_actividad As String)
            Return Accesos.fnc_eliminar_acceso(id_rol_actividad)
        End Function
        Function PartialViewCbModulos()
            Return PartialView(Modulos.fnc_lista_modulos())
        End Function

        Function PartialViewCbOpciones(ByVal id_modulos As String) As ActionResult
            ViewData("id_modulos") = id_modulos
            Return PartialView(Modulos.fnc_lista_opciones(id_modulos))
        End Function

        Function PartialViewCbPantallas(ByVal id_opcion As String) As ActionResult
            ViewData("id_opcion") = id_opcion
            Return PartialView(Modulos.fnc_lista_pantallas(id_opcion))
        End Function
    End Class
End Namespace
