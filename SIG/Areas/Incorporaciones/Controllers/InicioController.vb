Namespace SIG.Areas.Incorporaciones.Controllers
    Public Class InicioController
        Inherits System.Web.Mvc.Controller

        '
        ' GET: /Inicio
#Region "variables"
        Dim obj_usuario As New SIG.Areas.Incorporaciones.Models.Cl_Usuarios()
#End Region
        Dim Menu As New Global.SIG.Cl_Login

        Function Index() As ActionResult
            Response.Cookies("opciones").Expires = DateTime.Now.AddDays(-1)
            If Menu.Fnc_loggeado() IsNot Nothing Then
                If Menu.fnc_validarModulo(1) Then
                    Menu.Fnc_MenuModulo(1)
                    Return View()
                Else
                    Return Redirect("/Home/MenuPrincipal")
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
            Session.RemoveAll()
            Return RedirectToAction("Index")


            Return View()
        End Function


    End Class
End Namespace
