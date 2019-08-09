Imports System.Net

Namespace Controllers

    Public Class HomeController
        Inherits System.Web.Mvc.Controller

        '
        ' GET: /Index
        Dim objLogin As New Cl_Login()
        Dim Str As New DataTable()

        Function Index() As ActionResult
            Return View()
        End Function

        Function Login() As ActionResult
            Return View()
        End Function


        Function Logout() As ActionResult

            Session.Remove("MenuModulo")
            Session.Remove("Actividades")
            Session.Remove("modulo")
            Session.Remove("Rol")
            Session.Remove("usuario")
            Session.Remove("Asignacion")
            Session.Remove("menu")
            Session.Abandon()
            Return RedirectToAction("Index")


            Return View()
        End Function

        Function MenuPrincipal() As ActionResult
            If (Session("Rol")) Then
                objLogin.Fnc_MenuPrincipal()
                'Session("menu") = objLogin.Fnc_MenuModulo()

                If objLogin.Fnc_verificar_estado_contraseña(Session("usuario")) Then
                    Return View()
                Else
                    Return RedirectToAction("ViewcambioContraseña", "Seguridad/Usuarios")
                End If


                'Return View()
            End If
            Return RedirectToAction("Login")
        End Function

        'Function MenuModulo()
        '    'Return objLogin.Fnc_MenuModulo()
        'End Function

        <HttpPost>
        Function Login(ByVal Username As String, ByVal Password As String) As ActionResult
            Dim hostname As String = String.Empty
            Dim ip As String = String.Empty
            Dim ipProxy As String = String.Empty
            Dim mac As String = String.Empty
            Dim cod_intento As Integer = 0

            Try
                Dim strHostName As String = Dns.GetHostName()
                Dim ipEntry As IPHostEntry = Dns.GetHostEntry(Request.UserHostAddress)

                ip = Convert.ToString(ipEntry.AddressList(ipEntry.AddressList.Length - 1))
                mac = Convert.ToString(ipEntry.AddressList(ipEntry.AddressList.Length - 2))
                hostname = Convert.ToString(ipEntry.HostName)

                'Find IP Address Behind Proxy Or Client Machine In ASP.NET  
                Dim IPAdd As String = String.Empty
                IPAdd = Request.ServerVariables("HTTP_X_FORWARDED_FOR")

                If String.IsNullOrEmpty(IPAdd) Then
                    IPAdd = Request.ServerVariables("REMOTE_ADDR")
                    ipProxy = IPAdd     'ip detras del proxy
                End If

                cod_intento = objLogin.fnc_guardar_intento_logeo(Username, Password, hostname, mac, ip, ipProxy)

            Catch ex As Exception
                ip = Request.ServerVariables("HTTP_X_FORWARDED_FOR")

                If String.IsNullOrEmpty(ip) Then
                    ip = Request.ServerVariables("REMOTE_ADDR")
                End If

                cod_intento = objLogin.fnc_guardar_intento_logeo(Username, Password, ip)
            End Try

            Me.Str = objLogin.Fnc_BuscarUsuario(Username, Password)

            'Dim id As Integer = (From dr In dt.Rows Where DirectCast(dr("CountryName"), String) = countryNameCInt(dr("id"))).FirstOrDefault()

            If Str IsNot Nothing Then

                If Str.Rows.Count Then
                    If Str.Rows(0).Item(7) <> 2 Then
                        If Password <> Str.Rows(0).Item(0) Then
                            'If (objLogin.Fnc_Bloquer(Username) = 50000) Then
                            Response.Write("<div class='Message' id='Message'>Contraseña o Usuario Incorrectos.</div>")
                            Return View("Login")

                            '  End If
                            Response.Write("<div class='Message' id='Message'>Contraseña o Usuario Incorrectos.</div>")
                            Return View("Login")

                        Else
                            Try
                                If cod_intento > 0 Then
                                    objLogin.fnc_guardar_inicio_sesion(cod_intento, Str.Rows(0).Item(8))
                                End If
                            Catch ex As Exception
                            End Try

                            Session("modulo") = Str
                            Session("Rol") = Str.Rows(0).Item(2)
                            Session("Asignacion") = Str
                            Session("usuario") = Str.Rows(0).Item(4)
                            Session("username") = Str.Rows(0).Item(8)
                            Return RedirectToAction("MenuPrincipal")
                        End If
                    Else
                        Response.Write("<div class='Message' id='Message'>Su usuario a sido bloqueado.</div>")
                        Return View("Login")

                    End If
                Else
                    objLogin.Fnc_Bloquer(Username)
                    Response.Write("<div class='Message' id='Message'>Contraseña o Usuario Incorrectos.</div>")
                    Return View("Login")

                End If
            Else
                Response.Write("<div class='Message' id='Message'>Contraseña o Usuario Incorrectos.</div>")
                Return View("Login")
            End If

        End Function

    End Class

End Namespace