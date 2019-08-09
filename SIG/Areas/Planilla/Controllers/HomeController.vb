Namespace SIG.Areas.Planilla.Controllers
    Public Class HomeController
        Inherits System.Web.Mvc.Controller
        Dim login As New Global.SIG.Cl_Login
        '
        ' GET: /Planilla/Home

        Function Index() As ActionResult
            Response.Cookies("opciones").Expires = DateTime.Now.AddDays(-1)
            Response.Cookies("actividad").Expires = DateTime.Now.AddDays(-1)

            If login.Fnc_loggeado() IsNot Nothing Then 
                If login.fnc_validarModulo(3) Then
                    login.Fnc_MenuModulo(3)
                    Return View()
                End If
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