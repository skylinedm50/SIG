Namespace SIG.Areas.Corresponsabilidad.Controllers
    Public Class HomeController
        Inherits System.Web.Mvc.Controller

        Dim login As New Global.SIG.Cl_Login

        Function Index() As ActionResult
            Response.Cookies("opciones").Expires = DateTime.Now.AddDays(-1)
            Response.Cookies("actividad").Expires = DateTime.Now.AddDays(-1)

            If login.Fnc_loggeado() IsNot Nothing Then
                'Menu.Fnc_MenuModulo()
                login.Fnc_MenuModulo(2)
                Return View("Index")
            Else
                Return Redirect("/Home/Login")
            End If

        End Function

        Function Logout() As ActionResult
            Session.Remove("MenuModulo")
            Session.Remove("modulo")
            Session.Remove("Rol")
            Session.Remove("usuario")
            Session.Remove("Asignacion")
            Session.Remove("menu")
            Return RedirectToAction("Index")
        End Function
    End Class
End Namespace