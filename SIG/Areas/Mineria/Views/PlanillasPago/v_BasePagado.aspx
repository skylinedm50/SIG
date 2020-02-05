<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Mineria/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
Base Pagado
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="titleNavBarText" runat="server">
  Base Pagado
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ButtonNavBar" runat="server">
    <a class="navbar-brand" href="/Mineria/PlanillasPago/Home"><i class="fa fa-arrow-left fa-2x" aria-hidden="true"></i></a>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.DevExpress().GetStyleSheets(
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.Editors},
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.PivotGrid})%>
    <% Html.DevExpress().GetScripts(
        New Script With {.ExtensionSuite = ExtensionSuite.Editors},
        New Script With {.ExtensionSuite = ExtensionSuite.PivotGrid})%>
    <script type="text/javascript" src='<%: ResolveUrl("~/Areas/Mineria/Scripts/planillasPago.js")%>'></script>


    <% Html.BeginForm("exportarBasePagado", "PlanillasPago")%>
    <div>
        <% Html.DevExpress.FormLayout(
           Sub(settings)
               settings.Name = "frmLayoutFiltros"
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
               settings.Items.Add(
                   Sub(item)
                       item.ShowCaption = DefaultBoolean.False
                       item.HorizontalAlign = FormLayoutHorizontalAlign.Center
                       item.SetNestedContent(
                           Sub()
                               Html.DevExpress().Button(
                                   Sub(btn)
                                       btn.Name = "btnConsultarBasePagado"
                                       btn.Text = "Consultar"
                                       btn.ClientSideEvents.Click = "btnConsultarBasePagadoClick"
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
