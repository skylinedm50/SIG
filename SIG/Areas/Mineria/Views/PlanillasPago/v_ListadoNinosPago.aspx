<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Mineria/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
Listado de Niños en Pago
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.DevExpress().GetStyleSheets(
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.GridView},
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.Editors})%>
    <% Html.DevExpress().GetScripts(
        New Script With {.ExtensionSuite = ExtensionSuite.GridView},
        New Script With {.ExtensionSuite = ExtensionSuite.Editors})%>
    <script type="text/javascript" src='<%: ResolveUrl("~/Areas/Mineria/Scripts/planillasPago.js")%>'></script>

    <h2>Listado de Niños en Pago con Corresponsabilidad</h2>
    <% Html.BeginForm("exportarListadoNinoPago", "PlanillasPago")%>
    <div>
        <% Html.DevExpress.FormLayout(
           Sub(settings)
               settings.Name = "frmLayoutFiltros"
               'settings.SettingsItems.HorizontalAlign = FormLayoutHorizontalAlign.Right
               settings.ColCount = 2
               settings.Items.AddGroupItem(
                   Sub(group)
                       group.Caption = "Filtros para el Pago"
                       'group.ColCount = 2
                       group.VerticalAlign = FormLayoutVerticalAlign.Middle
                       group.Items.Add(
                           Sub(item)
                               item.Caption = "Año"
                               item.SetNestedContent(
                                   Sub()
                                       Html.RenderAction("pv_cbxAnos", "Shared")
                                   End Sub)
                           End Sub)
                       group.Items.Add(
                           Sub(item)
                               item.Caption = "Pago"
                               item.SetNestedContent(
                                   Sub()
                                       Html.RenderPartial("../Shared/Pagos/pv_cbxPagosAno")
                                   End Sub)
                           End Sub)
                   End Sub)
               settings.Items.AddGroupItem(
                   Sub(group)
                       group.Caption = "Área Geográfica"
                       group.Items.Add(
                           Sub(item)
                               item.Caption = "Departamento"
                               item.SetNestedContent(
                                   Sub()
                                       Html.RenderAction("pv_cbxDepartamentos", "Shared")
                                   End Sub)
                           End Sub)
                       group.Items.Add(
                           Sub(item)
                               item.Caption = "Municipio"
                               item.SetNestedContent(
                                   Sub()
                                       Html.RenderPartial("../Shared/Area_Geografica/pv_cbxMunicipios")
                                   End Sub)
                           End Sub)
                       group.Items.Add(
                           Sub(item)
                               item.Caption = "Aldea"
                               item.SetNestedContent(
                                   Sub()
                                       Html.RenderPartial("../Shared/Area_Geografica/pv_cbxAldeas")
                                   End Sub)
                           End Sub)
                   End Sub)
               settings.Items.Add(
                   Sub(item)
                       item.ShowCaption = DefaultBoolean.False
                       item.HorizontalAlign = FormLayoutHorizontalAlign.Center
                       item.SetNestedContent(
                           Sub()
                               Html.DevExpress().Button(
                                   Sub(btn)
                                       btn.Name = "btnConsultarListadoNinosPago"
                                       btn.Text = "Consultar"
                                       btn.ClientSideEvents.Click = "btnConsultarListadoNinosPagoClick"
                                   End Sub).GetHtml()
                           End Sub)
                   End Sub)
           End Sub).GetHtml()%>
    </div>
    <br />
    <div>
        <% Html.RenderAction("pv_ControlesExportar", "Shared")%>
    </div>
    <br />
    <div id="divGridView"></div>
    <% Html.EndForm()%>
    <% Html.RenderPartial("pv_Spinner", "Shared")%>
</asp:Content>
