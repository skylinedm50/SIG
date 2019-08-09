Imports DevExpress.Web.Mvc
Imports System.Net.Mail
Imports System.Net

Namespace SIG.Areas.Contraloria.Controllers
    Public Class CierrePlanillaController
        Inherits System.Web.Mvc.Controller

        Dim Cierre_Planilla As SIG.Areas.Contraloria.Models.Cierre_Planilla = New SIG.Areas.Contraloria.Models.Cierre_Planilla
        Dim login As New Global.SIG.Cl_Login

#Region "Cierre Parcial"

        Function Index() As ActionResult

            'Dim dt As DataTable = Cierre_Planilla.getDepartamentos()

            'If dt.Rows.Count > 1 Then
            '    Dim newRow As DataRow = dt.NewRow
            '    newRow.Item("cod_departamento") = "00"
            '    newRow.Item("desc_departamento") = "(Seleccionar Todos)"
            '    dt.Rows.InsertAt(newRow, 0)
            'End If

            'ViewData("dptos") = dt

            'Return View()

            If login.Fnc_loggeado() IsNot Nothing Then
                If login.fnc_validarModulo(4) Then
                    'login.Fnc_MenuModulo()
                    If login.Fnc_ValidarPermisoUrl(Request.Url.AbsolutePath) Then

                        Dim dt As DataTable = Cierre_Planilla.getDepartamentos()

                        If dt.Rows.Count > 1 Then
                            Dim newRow As DataRow = dt.NewRow
                            newRow.Item("cod_departamento") = "00"
                            newRow.Item("desc_departamento") = "(Seleccionar Todos)"
                            dt.Rows.InsertAt(newRow, 0)
                        End If

                        ViewData("dptos") = dt

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

        Function PartialGridViewPlanillas(ByVal strDeptos As String) As ActionResult

            Dim separador() As String = {","}
            Dim deptos() As String = strDeptos.Split(separador, StringSplitOptions.RemoveEmptyEntries)

            Dim dt As DataTable = Cierre_Planilla.getPlanillasAbiertas(deptos)
            dt.PrimaryKey = New DataColumn() {dt.Columns("cod_aldea")}

            ViewData("deptos") = strDeptos

            Return PartialView(dt)
        End Function

        <HttpPost> _
        Sub getPlanillasAbiertas(ByVal deptos() As String)

            Dim dt As DataTable = Cierre_Planilla.getPlanillasAbiertas(deptos)
            dt.PrimaryKey = New DataColumn() {dt.Columns("cod_aldea")}

            Session("ds") = dt
        End Sub

        <HttpPost> _
        Function cerrarPlanillas(ByVal values As String) As String

            Dim aldeas() As String = Split(values, ",")
            Dim rows As Integer = Cierre_Planilla.cerrarPlanillas(aldeas)

            If rows >= 0 Then

                'Dim dt As DataTable = TryCast(Session("ds"), DataTable)
                'For Each aldea In aldeas
                '    dt.Rows.Find(aldea).Delete()
                'Next
                'Session("ds") = dt

                Return "true"
            Else
                Return "false"
            End If
        End Function

        Function detalleAldea(ByVal codAldea As String) As ActionResult
            ViewData("cod_aldea") = codAldea
            Return PartialView("PartialDetailGridView", Cierre_Planilla.detalleAldea(codAldea))
        End Function

#End Region

#Region "funciones cierre total de planilla"

        Function ViewCierrePlanilla() As ActionResult

            'Try
            '    Dim periodo As DataTable = Cierre_Planilla.getPeriodo()

            '    ViewData("nombre") = periodo.Rows.Item(0).Item(1)
            '    ViewData("periodo") = periodo
            '    ViewData("departamentos") = Cierre_Planilla.getPlanillasDepartamentos()
            '    ViewData("flag") = True
            'Catch ex As Exception
            '    ViewData("flag") = False
            'End Try

            'Return View()

            If login.Fnc_loggeado() IsNot Nothing Then
                If login.fnc_validarModulo(4) Then
                    'login.Fnc_MenuModulo()
                    If login.Fnc_ValidarPermisoUrl(Request.Url.AbsolutePath) Then

                        Try
                            Dim periodo As DataTable = Cierre_Planilla.getPeriodo()

                            ViewData("nombre") = periodo.Rows.Item(0).Item(1)
                            ViewData("periodo") = periodo
                            ViewData("departamentos") = Cierre_Planilla.getPlanillasDepartamentos()
                            ViewData("flag") = True
                        Catch ex As Exception
                            ViewData("flag") = False
                        End Try

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

        <HttpPost> _
        Function cierrePeriodo()

            Dim result As Integer = Cierre_Planilla.cierrePeriodo()

            If result = 1 Then
                'enviarCorreo()
                Return True
            Else
                Return False
            End If

        End Function

        'Function getPeriodo()
        'End Function
        Sub enviarCorreo()

            '*---------------------------------MENSAJE DE CORREO----------------------------*
            Dim correo As New MailMessage()

            'añado todos los correos a los que se les va a enviar el correo
            correo.To.Add("kaled.almendares@gmail.com")
            correo.To.Add("kaled.almendares@outlook.com")

            'Asunto
            correo.Subject = "CIERRE DE PERIODO DE PAGO BONO VIDA MEJOR"
            correo.SubjectEncoding = System.Text.Encoding.UTF8

            'Direccion de correo electronico que queremos que reciba una copia del mensaje
            'mmsg.Bcc.Add("destinatariocopia@servidordominio.com"); //Opcional

            'Cuerpo del Mensaje
            correo.Body = "Este es el contenido del correo electronico."
            correo.BodyEncoding = System.Text.Encoding.UTF8
            correo.IsBodyHtml = False

            'Correo electronico desde la que enviamos el mensaje
            correo.From = New System.Net.Mail.MailAddress("kaled.almendares@gmail.com")

            '*-------------------------CLIENTE DE CORREO----------------------*

            'Creamos un objeto de cliente de correo
            Dim smtp As New SmtpClient()

            'credenciales del correo emisor
            'Dim NetworkCred As New NetworkCredential("kaled.almendares@gmail.com", "Stein6420537")
            'smtp.Credentials = NetworkCred
            'smtp.UseDefaultCredentials = True
            smtp.Credentials = New NetworkCredential("kaled.almendares@gmail.com", "Stein6420537")

            'necesario para enviar correo desde gmail
            smtp.Port = 587
            smtp.Host = "smtp.gmail.com"
            smtp.EnableSsl = True

            '*-------------------------ENVIO DE CORREO----------------------*
            Try
                smtp.Send(correo)
            Catch ex As Exception

            End Try

            'Using mm As New MailMessage("kaled.almendares@gmail.com", "kaled.almendares@gmail.com")
            '    mm.Subject = "prueba de enviar correo"
            '    mm.Body = "Este es el contenido del correo electronico."
            '    mm.IsBodyHtml = False
            '    smtp.Send(mm)
            'End Using


            '///////////////////////////////////////////////////////////////////////////////////////////////
            'código de ejemplo para enviar un correo funciona
            'Using correo As New MailMessage("kaled.almendares@gmail.com", "kaled.almendares@gmail.com")
            '    correo.Subject = "prueba de enviar correo"
            '    correo.Body = "Este es el contenido del correo electronico."
            '    'If fuAttachment.HasFile Then
            '    '    Dim FileName As String = Path.GetFileName(fuAttachment.PostedFile.FileName)
            '    '    mm.Attachments.Add(New Attachment(fuAttachment.PostedFile.InputStream, FileName))
            '    'End If
            '    correo.IsBodyHtml = False
            '    Dim smtp As New SmtpClient()
            '    smtp.Host = "smtp.gmail.com"
            '    smtp.EnableSsl = True
            '    Dim NetworkCred As New NetworkCredential("kaled.almendares@gmail.com", "Stein6420537")
            '    smtp.UseDefaultCredentials = True
            '    smtp.Credentials = NetworkCred
            '    smtp.Port = 587
            '    smtp.Send(correo)
            '    'ClientScript.RegisterStartupScript(Me.GetType, "alert", "alert('Email sent.');", True)
            'End Using

        End Sub

#End Region



    End Class
End Namespace