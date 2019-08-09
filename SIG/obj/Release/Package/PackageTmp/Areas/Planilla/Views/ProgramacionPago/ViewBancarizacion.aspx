<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Planilla/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
Tiempo Para Apertura de Cuenta
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.DevExpress().GetStyleSheets(
              New StyleSheet With {.ExtensionSuite = ExtensionSuite.Editors},
              New StyleSheet With {.ExtensionSuite = ExtensionSuite.GridView})%>
    <% Html.DevExpress().GetScripts(
              New Script With {.ExtensionSuite = ExtensionSuite.Editors},
              New Script With {.ExtensionSuite = ExtensionSuite.GridView})%>
    <script type="text/javascript" src='<%: ResolveUrl("~/Areas/Planilla/Scripts/programacion_pago.js")%>'></script>

    <h2>Tiempos para apertura de la cuenta básica</h2>
    <% Html.BeginForm()%>
    <div id="divControles">
        <% Html.DevExpress.FormLayout(
                      Sub(settings)
                          settings.Name = "floControles"
                          settings.Items.Add(
                              Sub(item)
                                  item.Caption = "Pago"
                                  item.HorizontalAlign = FormLayoutHorizontalAlign.Center
                                  item.SetNestedContent(
                                      Sub()
                                          Html.RenderAction("pv_cbxPagos", "Shared")
                                      End Sub)
                              End Sub)
                      End Sub).GetHtml()%>
    </div>
    <br />
    <div>
        <div id="divGridView"></div>
    </div>
    <% Html.EndForm()%>
    <% Html.RenderPartial("pv_Spinner", "Shared")%>
</asp:Content>
