
Namespace SIG.Areas.Contraloria.Controllers

    Public Class ActasController
        Inherits System.Web.Mvc.Controller


        Dim Actas As SIG.Areas.Contraloria.Models.Actas = New SIG.Areas.Contraloria.Models.Actas
        Dim login As New Global.SIG.Cl_Login

        Function ViewRecepcionAgencia()

            If login.Fnc_loggeado() IsNot Nothing Then
                If login.fnc_validarModulo(4) Then
                    'login.Fnc_MenuModulo()
                    If login.Fnc_ValidarPermisoUrl(Request.Url.AbsolutePath) Then
                        ViewData("bancos") = Actas.getAllBancos()
                        ViewData("periodos") = Actas.getAllPeriodos()
                        ViewData("fondos") = Actas.getAllFondo()
                        ViewData("departamentos") = Actas.getAllDptos()
                        ViewData("userName") = Session("username")
                        Return View()

                    Else
                        Return Redirect("/Contraloria/Home")
                    End If
                Else
                    Return Redirect("/Home/MenuPrincipal")
                End If
            Else
                Return Redirect("/Home/Login")
            End If

        End Function

        Function ViewRecepcionMovil()

            If login.Fnc_loggeado() IsNot Nothing Then
                If login.fnc_validarModulo(4) Then
                    'login.Fnc_MenuModulo()
                    If login.Fnc_ValidarPermisoUrl(Request.Url.AbsolutePath) Then
                        ViewData("bancos") = Actas.getAllBancos()
                        ViewData("periodos") = Actas.getAllPeriodos()
                        ViewData("fondos") = Actas.getAllFondo()
                        ViewData("departamentos") = Actas.getAllDptos()
                        ViewData("userName") = Session("username")
                        Return View()
                    Else
                        Return Redirect("/Contraloria/Home")
                    End If
                Else
                    Return Redirect("/Home/MenuPrincipal")
                End If
            Else
                Return Redirect("/Home/Login")
            End If

        End Function

        Function ViewInconsistencias()

            If login.Fnc_loggeado() IsNot Nothing Then
                If login.fnc_validarModulo(4) Then
                    'login.Fnc_MenuModulo()
                    If login.Fnc_ValidarPermisoUrl(Request.Url.AbsolutePath) Then
                        ViewData("departamentos") = Actas.getAllDptos()
                        ViewData("userName") = Session("username")
                        Return View()
                    Else
                        Return Redirect("/Contraloria/Home")
                    End If
                Else
                    Return Redirect("/Home/MenuPrincipal")
                End If
            Else
                Return Redirect("/Home/Login")
            End If

        End Function

        Function ViewDescargo()

            If login.Fnc_loggeado() IsNot Nothing Then
                If login.fnc_validarModulo(4) Then
                    'login.Fnc_MenuModulo()
                    If login.Fnc_ValidarPermisoUrl(Request.Url.AbsolutePath) Then
                        ViewData("departamentos") = Actas.getAllDptos()
                        ViewData("userName") = Session("username")
                        Return View()
                    Else
                        Return Redirect("/Contraloria/Home")
                    End If
                Else
                    Return Redirect("/Home/MenuPrincipal")
                End If
            Else
                Return Redirect("/Home/Login")
            End If

        End Function

        '<HttpPost> _
        Function infoRecibo(ByVal pagina As String, ByVal registro As String) As JsonResult

            Dim recibo As New infoRecibo

            Try
                Dim row As DataRow = Actas.infoRecibo(pagina, registro)

                recibo.dpto = row.Item(0)
                recibo.muni = row.Item(1)
                recibo.aldea = row.Item(2)
                recibo.nombre = row.Item(3)
                recibo.monto = row.Item(4)

                If Not IsDBNull(row.Item(5)) Then
                    recibo.fecha = row.Item(5)
                End If

            Catch ex As Exception
                Return Nothing
            End Try

            Return Json(recibo, JsonRequestBehavior.AllowGet)
        End Function

        Function PartialSucursalView() As ActionResult
            Dim banco As String = Request.Params("banco")
            Return PartialView(Actas.getSucursalByBanco(banco))
        End Function

        Function nombreUsuario() As String

            Dim nombre As String = Session("username")
            Return nombre

        End Function

    End Class

    'clase para enviar el Json a la página de las inconsistencias
    Public Class infoRecibo
        Public Property dpto As String
        Public Property muni As String
        Public Property aldea As String
        Public Property nombre As String
        Public Property monto As String
        Public Property fecha As String
    End Class

End Namespace