<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.Security.AccessControl" %>
<%@ Import Namespace="System.Drawing" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Xml.Serialization" %>
<%@ Import Namespace="System.Web.Script.Serialization" %>
<%@ Import Namespace="System.Drawing.Imaging" %>    
<%@ Import Namespace="System.Security.Permissions" %>
<%@ Import Namespace="System.Diagnostics" %>
<%@ Import Namespace="System.Net.FtpClient" %>
<%@ Import Namespace="System.Net" %>

<style>
    #imagenExpediente {
        width: 700px;
        height: 800px;
    }
</style>


<div id="ImagenExpediente">
    <%
        Try
            Crear_Imagener_Expediente()
        Catch ex As Exception
            Html.DevExpress().DockZone(Sub(settings)
                                           settings.Name = "LeftZone"
                                           settings.ZoneUID = "leftZone"
                                           settings.Width = 400
                                           settings.PanelSpacing = 1
                                       End Sub).GetHtml()
            Html.DevExpress().DockPanel(Sub(Panel)
                                            Panel.Name = "panel1"
                                            Panel.PanelUID = "panel1"
                                            Panel.VisibleIndex = 0
                                            Panel.Top = 0
                                            Panel.Width = 400
                                            Panel.Height = 700
                                            Panel.ShowHeader = False
                                            Panel.AllowResize = True
                                            Panel.OwnerZoneUID = "leftZone"
                                            Panel.Styles.Content.Border.BorderStyle = BorderStyle.Solid
                                            Panel.Styles.Content.Border.BorderWidth = 1
                                            Panel.Styles.Content.Border.BorderColor = Color.FromArgb(1046463)
                                            Panel.Styles.Content.BackColor = Color.FromArgb(14415070)
                                            Panel.Styles.Content.Paddings.Padding = 0
                                            Panel.SetContent(Sub()
                                                                 ViewContext.Writer.Write(
                                                                         "<div class='panelNum' >Error al cargar las imagenes del hogar</div><input id='no_aprobar' type='hidden' value='hidden'></input>"
                                                                     )
                                                             End Sub)
                                            Panel.ControlStyle.Border.BorderStyle = BorderStyle.None
                                        End Sub).GetHtml()
        End Try
%>
    </div>
    <div id="dotNav" >
        <ul>            
        <%
            ViewContext.Writer.Write(dots)
        %>
        </ul>
    </div>
          
<script runat="server" >

    Dim dots As String = ""

    Sub Crear_Imagener_Expediente()

        Dim Expediente As String = ""
        Dim img As String = ""
        Dim c As Integer = 0

        Dim ftp As New FtpClient()
        ftp.Host = "192.168.41.13"
        ftp.Credentials = New NetworkCredential("backup", "Estad02017")
        ftp.Connect()

        If (ftp.DirectoryExists("/" + ViewData("Hogar").ToString)) Then
            ftp.SetWorkingDirectory("/" + ViewData("Hogar").ToString)

            For Each itemfolder In ftp.GetListing(ftp.GetWorkingDirectory())
                If (Model = c) Then
                    Dim ReturnValue As String = ""
                    Using Stream As Stream = ftp.OpenRead(itemfolder.FullName)
                        Try
                            Dim BinRead As BinaryReader = New BinaryReader(Stream)
                            Dim BinBytes As Byte() = BinRead.ReadBytes(CInt(Stream.Length))
                            ReturnValue = Convert.ToBase64String(BinBytes)
                            Stream.Close()
                        Catch ex As Exception
                        End Try
                    End Using
                    Expediente = "data:image/jpg;base64," + ReturnValue
                End If


                dots = dots + String.Format("<li onclick=""fnc_buscarExpedienteDigital" + "({0},{1})""", c, ViewData("Hogar"))
                If (c = Model) Then
                    dots = dots + " class='active'  ><a></a></li>"
                Else
                    dots = dots + " ><a></a></li> "
                End If
                c = c + 1
            Next

            img = "<img id='imagenExpediente' src=""" + Expediente + """  />"
        Else
            Html.DevExpress().DockZone(Sub(settings)
                                           settings.Name = "LeftZone"
                                           settings.ZoneUID = "leftZone"
                                           settings.Width = 400
                                           settings.PanelSpacing = 1
                                       End Sub).GetHtml()
            Html.DevExpress().DockPanel(Sub(Panel)
                                            Panel.Name = "panel1"
                                            Panel.PanelUID = "panel1"
                                            Panel.VisibleIndex = 0
                                            Panel.Top = 0
                                            Panel.Width = 400
                                            Panel.Height = 700
                                            Panel.ShowHeader = False
                                            Panel.AllowResize = True
                                            Panel.OwnerZoneUID = "leftZone"
                                            Panel.Styles.Content.Border.BorderStyle = BorderStyle.Solid
                                            Panel.Styles.Content.Border.BorderWidth = 1
                                            Panel.Styles.Content.Border.BorderColor = Color.FromArgb(1046463)
                                            Panel.Styles.Content.BackColor = Color.FromArgb(14415070)
                                            Panel.Styles.Content.Paddings.Padding = 0
                                            Panel.SetContent(Sub()
                                                                 ViewContext.Writer.Write(
                                                                         "<div class='panelNum' >No hay documentación escaneada para el hogar</div><input id='no_aprobar' type='hidden' value='hidden'></input>"
                                                                     )
                                                             End Sub)
                                            Panel.ControlStyle.Border.BorderStyle = BorderStyle.None
                                        End Sub).GetHtml()
        End If
        ftp.Disconnect()
        ViewContext.Writer.Write(img)
    End Sub

</script>                                                                    