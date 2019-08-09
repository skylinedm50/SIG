<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Planilla/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
Generación de Documentos
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.DevExpress().GetStyleSheets(
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.Editors},
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.GridView},
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.PivotGrid})%>
    <% Html.DevExpress().GetScripts(
        New Script With {.ExtensionSuite = ExtensionSuite.Editors},
        New Script With {.ExtensionSuite = ExtensionSuite.GridView},
        New Script With {.ExtensionSuite = ExtensionSuite.PivotGrid})%>
    <script type="text/javascript" src='<%: ResolveUrl("~/Areas/Planilla/Scripts/generacion_documentos.js")%>'></script>

    <h2>Generación de Documentos de Planilla</h2>
    <% Html.BeginForm()%>
    <div id="divControles">
        <% Html.DevExpress.FormLayout(
               Sub(settings)
                   settings.Name = "floControles"
                   settings.Width = 650
                   settings.Items.Add(
                       Sub(item)
                           item.Caption = "Pago"
                           item.HorizontalAlign = FormLayoutHorizontalAlign.Center
                           item.SetNestedContent(
                               Sub()
                                   Html.RenderAction("pv_cbxPagos", "Shared")
                               End Sub)
                       End Sub)
                   settings.Items.Add(
                       Sub(item)
                           item.ShowCaption = DefaultBoolean.False
                           item.SetNestedContent(
                               Sub()
                                   Html.RenderAction("pv_gdvEsquemas", "Shared")
                               End Sub)
                       End Sub)
                   settings.Items.Add(
                       Sub(item)
                           item.ShowCaption = DefaultBoolean.False
                           item.SetNestedContent(
                               Sub()
                                   Html.DevExpress().Button(
                                       Sub(btn)
                                           btn.Name = "btnGenerarDocumentos"
                                           btn.Text = "Generar Documentos"
                                           btn.UseSubmitBehavior = False
                                           btn.ClientSideEvents.Click = "btnGenerarDocumentosClick"
                                       End Sub).GetHtml()
                               End Sub)
                       End Sub)
               End Sub).GetHtml()%>
    </div>
    <br />
    <% Html.RenderPartial("pv_mensajes", "Shared")%>
    <br />
    <div id="divFileManager"></div>
    <% Html.EndForm()%>
    <% Html.RenderPartial("pv_Spinner", "Shared")%>
</asp:Content>
