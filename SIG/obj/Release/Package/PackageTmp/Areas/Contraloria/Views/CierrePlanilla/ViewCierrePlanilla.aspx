<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Contraloria/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
ViewCierrePlanilla
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.DevExpress().GetStyleSheets(
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.GridView},
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.Editors})%>
    <% Html.DevExpress().GetScripts(
        New Script With {.ExtensionSuite = ExtensionSuite.GridView},
        New Script With {.ExtensionSuite = ExtensionSuite.Editors})%>

    <link type="text/css" rel="stylesheet" href="/Areas/Contraloria/Style/styles_contraloria.css" />

    <script type="text/javascript" src='<%: ResolveUrl("~/Areas/Contraloria/Scripts/cierrePlanilla.js")%>'></script>
    <h2>Cierre General de Planilla de Pago</h2>
    <%--<style>
        /*table {
            font-size: 11px;
        }*/
    </style>--%>
    <br />

    <% Html.DevExpress().Label(
                Sub(settings)
                    settings.Name = "lbTexto"
                    'settings.Width = New Unit(70, UnitType.Pixel)
                    
                    If ViewData("flag") Then
                        settings.Text = "Las planillas de pago del periodo " + ViewData("nombre") + " cerrara con las siguientes cifras:"
                    Else
                        settings.Text = "No hay periodo de pago abierto."
                    End If
                End Sub).GetHtml()%>

    <br />
    <br />
    <% Html.DevExpress().GridView(
       Sub(settings)
           settings.Name = "gvPeriodo"
           settings.Caption = ViewData("nombre")
           
           
           
           
                                 
           settings.Columns.AddBand(
               Sub(per)
                   per.Caption = "Periodo"
                   per.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                   'per.Columns.Add("cod_periodo", "Código")
                   'per.Columns.Add("nombrePeriodo", "Nombre")
                   per.Columns.Add("pag_codigo", "Código")
                   per.Columns.Add("pag_nombre", "Nombre")
               End Sub)
           settings.Columns.AddBand(
               Sub(prog)
                   prog.Caption = "Programado"
                   prog.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                   prog.Columns.Add("hogProgramados", "Hogares")
                   prog.Columns.Add("montoProgramado", "Monto")
               End Sub)
           settings.Columns.AddBand(
               Sub(pag)
                   pag.Caption = "Pagado"
                   pag.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                   pag.Columns.Add("hogPagados", "Hogares")
                   pag.Columns.Add("montoPagado", "Monto")
               End Sub)
           settings.Columns.AddBand(
               Sub(noPag)
                   noPag.Caption = "No Pagado"
                   noPag.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                   noPag.Columns.Add("hogNoPagados", "Hogares")
                   noPag.Columns.Add("montoNoPagado", "Monto")
               End Sub)
           settings.Columns.Add("cumplimiento", "Cumplimiento")
       End Sub).Bind(ViewData("periodo")).Render()%>
    <br />
    <% Html.DevExpress().GridView(
       Sub(settings)
           settings.Name = "gvDepartamento"
           settings.Caption = "Consolidado por Departamento"
           
           settings.SettingsPager.PageSize = 20
           settings.Columns.Add("desc_departamento", "Departamento")
           settings.Columns.AddBand(
               Sub(prog)
                   prog.Caption = "Programado"
                   prog.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                   prog.Columns.Add("hogProgramados", "Hogares").Settings.AllowHeaderFilter = DefaultBoolean.False
                   prog.Columns.Add("montoProgramado", "Monto").Settings.AllowHeaderFilter = DefaultBoolean.False
               End Sub)
           settings.Columns.AddBand(
               Sub(pag)
                   pag.Caption = "Pagado"
                   pag.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                   pag.Columns.Add("hogPagados", "Hogares").Settings.AllowHeaderFilter = DefaultBoolean.False
                   pag.Columns.Add("montoPagado", "Monto").Settings.AllowHeaderFilter = DefaultBoolean.False
               End Sub)
           settings.Columns.AddBand(
               Sub(noPag)
                   noPag.Caption = "No Pagado"
                   noPag.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                   noPag.Columns.Add("hogNoPagados", "Hogares").Settings.AllowHeaderFilter = DefaultBoolean.False
                   noPag.Columns.Add("montoNoPagado", "Monto").Settings.AllowHeaderFilter = DefaultBoolean.False
               End Sub)
           settings.Columns.Add("cumplimiento", "Cumplimiento").Settings.AllowHeaderFilter = DefaultBoolean.False
       End Sub).Bind(ViewData("departamentos")).Render()%>
    <br />
    <% Html.DevExpress.Button(
           Sub(settings)
               settings.Name = "btnCerrar"
               settings.Text = "Cerrar Periodo"
               settings.ClientSideEvents.Click = "btnCerrarClick"
           End Sub).GetHtml()%>

</asp:Content>
