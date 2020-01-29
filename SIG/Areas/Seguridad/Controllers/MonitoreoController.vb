Namespace SIG.Areas.Seguridad.Controllers
    Public Class MonitoreoController
        Inherits System.Web.Mvc.Controller
        Dim login As New Global.SIG.Cl_Login
        Dim Monitoreos As New Global.SIG.SIG.Areas.Seguridad.Models.Monitoreo


        Function ViewVerActividad() As ActionResult
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

        Function ViewInicioSesion() As ActionResult
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

        Function ViewIntentos() As ActionResult
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

        Function PVGV_InicioSesion() As ActionResult
            Return PartialView(Monitoreos.fnc_Obtener_Inicios_Sesion)
        End Function

        Function PVGV_Actividad() As ActionResult
            Return PartialView(Monitoreos.fnc_Obtener_Log_Sistema)
        End Function

        Function PVGV_Intentos() As ActionResult
            Return PartialView(Monitoreos.fnc_Obtener_Log_Intentos_logeo)
        End Function
    End Class
End Namespace