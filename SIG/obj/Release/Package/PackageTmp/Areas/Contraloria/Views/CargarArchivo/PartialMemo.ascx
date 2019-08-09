<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<style>
    .hidden {
        display:none;
    }
</style>
<p>Resumen</p>
<%
    Html.DevExpress.Label(Sub(setting)
                              setting.Name = "LblFchFueraProgramacion"
                              setting.Text = ViewData("FchfueraProgramacion")
                              setting.ControlStyle.CssClass = "hidden"
                          End Sub).GetHtml()
%>
<%
    Html.DevExpress().Label(Sub(setting)
                                setting.Name = "LblPoderCargar"
                                setting.Text = ViewData("PoderCargar")
                                setting.ControlStyle.CssClass = "hidden"
                            End Sub).GetHtml()
%>
<br />
<%
    Html.DevExpress().Memo(
       Sub(settings)
           settings.Name = "txtMemo"
           If Not IsNothing(ViewData("nombre")) Then
               settings.Text = "Nombre del Archivo: " + ViewData("nombre") + vbCrLf & _
               "Cantidad total de registros: " + ViewData("totalRegistros").ToString() + vbCrLf & _
               "Cantidad de registros pagados:" + ViewData("totalPagados").ToString() + vbCrLf & _
               "Cantidad de registros no pagados: " + ViewData("totalNoPagados").ToString() + vbCrLf & _
               "Cantidad de registros pagados fuera de programación: " + ViewData("FchfueraProgramacion").ToString() + vbCrLf & _
               "" + vbCrLf & _
               "Monto total: " + ViewData("montoTotal") + vbCrLf & _
               "Monto total pagado: " + ViewData("montoPagado") + vbCrLf & _
               "Monto total no pagado: " + ViewData("montoNoPagado") + vbCrLf & _
               "Monto total pagado fuera de la programación :" + ViewData("montoFchNoProgramado") + vbCrLf & _
               "" + vbCrLf & _
               "Contador de estado de los registros:"
               
               For i = 1 To ViewData("num")
                   settings.Text += vbCr + "    " + ViewData(i)
               Next
               
           End If
           settings.Properties.Columns = 80
           settings.ReadOnly = True
           settings.Properties.Rows = 25
       End Sub).Render()%>