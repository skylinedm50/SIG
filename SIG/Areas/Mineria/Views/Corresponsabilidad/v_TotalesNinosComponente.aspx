<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Mineria/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
Totales Componente
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="titleNavBarText" runat="server">
    Totales Componente
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ButtonNavBar" runat="server">
    <a class="navbar-brand" href="/Mineria/Corresponsabilidad/Home"><i class="fa fa-arrow-left fa-2x" aria-hidden="true"></i></a>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.DevExpress().GetStyleSheets(
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.Editors},
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.PivotGrid})%>
    <% Html.DevExpress().GetScripts(
        New Script With {.ExtensionSuite = ExtensionSuite.Editors},
        New Script With {.ExtensionSuite = ExtensionSuite.PivotGrid})%>
    <script type="text/javascript" src='<%: ResolveUrl("~/Areas/Mineria/Scripts/corresponsabilidad.js")%>'></script>

    <h2>Totales de Niños por Componente en Pago</h2>
    <% Html.BeginForm("exportarTotalesNinosComponente", "Corresponsabilidad")%>
    <div>
        <% Html.DevExpress.FormLayout(
               Sub(frmLayoutTotalesNinos)
                   frmLayoutTotalesNinos.Name = "frmLayoutTotalesNinos"
                   'frmLayoutTotalesNinos.ColCount = 2
                   frmLayoutTotalesNinos.Items.Add(
                       Sub(itemElegVsProg)
                           itemElegVsProg.ShowCaption = DefaultBoolean.False
                           itemElegVsProg.VerticalAlign = FormLayoutVerticalAlign.Top
                           itemElegVsProg.SetNestedContent(
                               Sub()
                                   Html.RenderAction("pv_ControlesPagos", "Shared")
                               End Sub)
                       End Sub)
                   frmLayoutTotalesNinos.Items.Add(
                       Sub(item)
                           item.ShowCaption = DefaultBoolean.False
                           item.HorizontalAlign = FormLayoutHorizontalAlign.Center
                           item.SetNestedContent(
                               Sub()
                                   Html.DevExpress().Button(
                                       Sub(btnConsultar)
                                           btnConsultar.Name = "btnConsultarTotalesNinosComponente"
                                           btnConsultar.UseSubmitBehavior = False
                                           btnConsultar.Text = "Consultar"
                                           btnConsultar.ClientSideEvents.Click = "btnConsultarTotalesNinosComponenteClick"
                                       End Sub).GetHtml()
                               End Sub)
                       End Sub)
               End Sub).GetHtml()%>
    </div>
    <%--<br />
    <div>
         <% Html.DevExpress().FormLayout(
               Sub(frmLayoutButtons)
                   frmLayoutButtons.Name = "frmLayoutButtons"
                   frmLayoutButtons.ColCount = 3
                   frmLayoutButtons.Width = Unit.Percentage(50)
                   frmLayoutButtons.Items.Add(
                       Sub(item)
                           item.ShowCaption = DefaultBoolean.False
                           item.SetNestedContent(
                               Sub()
                                   Html.DevExpress().Button(
                                       Sub(btnConsultar)
                                           btnConsultar.Name = "btnConsultarTotalesNinosComponente"
                                           btnConsultar.UseSubmitBehavior = False
                                           btnConsultar.Text = "Consultar"
                                           btnConsultar.ClientSideEvents.Click = "btnConsultarTotalesNinosComponenteClick"
                                       End Sub).GetHtml()
                               End Sub)
                       End Sub)
               End Sub).GetHtml()%>
    </div>--%>
    <br />
    <div>
        <% Html.RenderAction("pv_ControlesExportar", "Shared")%>
    </div>
    <br />
    <div id="divGridView"></div>
    <% Html.EndForm()%>
    <% Html.RenderPartial("pv_Spinner", "Shared")%>


</asp:Content>
