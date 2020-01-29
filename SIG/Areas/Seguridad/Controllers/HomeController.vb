Namespace SIG.Areas.Seguridad.Controllers

    Public Class HomeController
        Inherits System.Web.Mvc.Controller

        '
        ' GET: /Seguridad/Home
        Dim login As New Global.SIG.Cl_Login

        Function Index() As ActionResult
            Response.Cookies("opciones").Expires = DateTime.Now.AddDays(-1)
            Response.Cookies("actividad").Expires = DateTime.Now.AddDays(-1)

            If login.Fnc_loggeado() IsNot Nothing Then
                'Menu.Fnc_MenuModulo()

                If login.fnc_validarModulo(6) Then
                    login.Fnc_MenuModulo(6)
                    Return View("Index")
                End If
            Else
                Return Me.Redirect("/Home/Login")
            End If

        End Function

        Function Logout() As ActionResult

            Session.Remove("MenuModulo")
            Session.Remove("Actividades")
            Session.Remove("modulo")
            Session.Remove("Rol")
            Session.Remove("usuario")
            Session.Remove("Asignacion")
            Session.Remove("menu")
            Session.RemoveAll()
            Session.Clear()
            Session.Abandon()

            Return RedirectToAction("Index")

        End Function


    End Class

End Namespace