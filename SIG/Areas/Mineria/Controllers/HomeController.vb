Namespace SIG.Areas.Mineria.Controllers
    Public Class HomeController
        Inherits System.Web.Mvc.Controller

        '
        ' GET: /Mineria/Home
        Dim login As New Global.SIG.Cl_Login

        Function Index() As ActionResult
            'Return View()

            Response.Cookies("opciones").Expires = DateTime.Now.AddDays(-1)
            Response.Cookies("actividad").Expires = DateTime.Now.AddDays(-1)

            If login.Fnc_loggeado() IsNot Nothing Then
                'Menu.Fnc_MenuModulo()  
                If login.fnc_validarModulo(7) Then
                    login.Fnc_MenuModulo(7)
                    Return View()
                End If
            Else
                Return Redirect("/Home/Login")
            End If

        End Function

    End Class
End Namespace