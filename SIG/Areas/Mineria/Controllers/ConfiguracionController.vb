Namespace SIG.Areas.Mineria.Controllers

    Public Class ConfiguracionController
        Inherits System.Web.Mvc.Controller

        '
        ' GET: /Mineria/Configuracion
        Dim login As New Global.SIG.Cl_Login
        Dim config As SIG.Areas.Mineria.Models.Cl_Configuracion = New SIG.Areas.Mineria.Models.Cl_Configuracion

        Function v_AnoElegibilidadFichas() As ActionResult

            Response.Cookies("opciones").Expires = DateTime.Now.AddDays(-1)
            Response.Cookies("actividad").Expires = DateTime.Now.AddDays(-1)

            If login.Fnc_loggeado() IsNot Nothing Then
                'Menu.Fnc_MenuModulo()  
                If login.fnc_validarModulo(7) Then
                    login.Fnc_MenuModulo(7)

                    ViewData("año_valido_fichas") = config.Fnc_obtener_ano_valido_fichas()


                    Return View()
                End If
            Else
                Return Redirect("/Home/Login")
            End If

        End Function

        Function actualizarAnoElegibilidadFichas(ByVal ano As String) As JavaScriptResult
            'Return config.Fnc_actulizar_ano_elegibilidad_valido(ano)
            Return JavaScript(config.Fnc_actulizar_ano_elegibilidad_valido(ano))
        End Function

    End Class
End Namespace