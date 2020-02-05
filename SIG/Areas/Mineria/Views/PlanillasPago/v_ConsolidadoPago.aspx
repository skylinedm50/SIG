<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Mineria/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Consolidado de Pago
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="titleNavBarText" runat="server">
  Consolidado de pago por planilla
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


    <% Html.BeginForm("exportarConsolidadoPago", "PlanillasPago")%>
    <div>
        <% Html.DevExpress.FormLayout(
               Sub(frmLayoutConsolidado)
                   frmLayoutConsolidado.Name = "floConsolidadoPago"
                   frmLayoutConsolidado.ColCount = 2
                   frmLayoutConsolidado.Items.Add(
                       Sub(itemConsolidado)
                           itemConsolidado.ShowCaption = DefaultBoolean.False
                           itemConsolidado.VerticalAlign = FormLayoutVerticalAlign.Top
                           itemConsolidado.SetNestedContent(
                               Sub()
                                   Html.RenderAction("pv_ControlesPagos", "Shared")
                               End Sub)
                       End Sub)
               End Sub).GetHtml()%>
    </div>
    <br />
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
                                           btnConsultar.Name = "btnConsultarConsolidadoPago"
                                           btnConsultar.UseSubmitBehavior = False
                                           btnConsultar.Text = "Consultar"
                                           btnConsultar.ClientSideEvents.Click = "btnConsultarConsolidadoPagoClick"
                                       End Sub).GetHtml()
                               End Sub)
                       End Sub)
               End Sub).GetHtml()%>
    </div>
    <br />
    <div>
        <% Html.RenderAction("pv_ControlesExportar", "Shared")%>
    </div>
    <div id="divGridView"></div>
    <br />
    <%--<div id="divChart" style="display: none;">--%>
    <div id="divChart">
        <% Html.RenderAction("pv_ControlesTiposGrafico", "Shared") %>   
        <% Html.RenderPartial("pv_ControlesExportChart", "Shared")%>
        <br />
        <div>
            <% Html.RenderPartial("pv_chrConsolidadoPago")%>
        </div> 
    </div>
    <% Html.EndForm()%>
    <% Html.RenderPartial("pv_Spinner", "Shared")%>
</asp:Content>
