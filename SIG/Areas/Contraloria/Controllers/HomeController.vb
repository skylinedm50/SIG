Namespace SIG.Areas.Contraloria.Controllers
    Public Class HomeController
        Inherits System.Web.Mvc.Controller

        '
        ' GET: /Contraloria/Home
        Dim login As New Global.SIG.Cl_Login


        Function Index() As ActionResult

            Response.Cookies("opciones").Expires = DateTime.Now.AddDays(-1)
            Response.Cookies("actividad").Expires = DateTime.Now.AddDays(-1)

            If login.Fnc_loggeado() IsNot Nothing Then
                'Menu.Fnc_MenuModulo()  
                If login.fnc_validarModulo(4) Then
                    login.Fnc_MenuModulo(4)
                    Return View("Index")
                End If
            Else
                Return Redirect("/Home/Login")
            End If

        End Function

        Function Logout() As ActionResult

            Session.Remove("modulo")
            Session.Remove("Rol")
            Session.Remove("usuario")
            Session.Remove("Asignacion")
            Return RedirectToAction("Index")


            Return View()
        End Function
    End Class
End Namespace