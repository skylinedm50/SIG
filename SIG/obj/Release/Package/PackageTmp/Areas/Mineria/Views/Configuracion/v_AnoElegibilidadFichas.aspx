<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Mineria/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
Configuración de Parámetros
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.DevExpress().GetStyleSheets(
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.Editors})%>
    <% Html.DevExpress().GetScripts(
        New Script With {.ExtensionSuite = ExtensionSuite.Editors})%>
    <script type="text/javascript" src='<%: ResolveUrl("~/Areas/Mineria/Scripts/configuracion.js")%>'></script>

    <h2>Configuración del Año Valido</h2>
    <div>
        <p>

        </p>
    </div>
    <div>
        <p>
            Parámetro que indica el año de elegibilidad valido para las fichas de los hogares del Bono Vida Mejor.
        </p>
        <% Html.DevExpress().FormLayout(
               Sub(frmLayoutConf)
                   frmLayoutConf.Name = "frmLayoutConf"
                   frmLayoutConf.Items.Add(
                       Sub(item)
                           item.Caption = "Año Valido de Fichas"
                           item.SetNestedContent(
                               Sub()
                                   Html.DevExpress().TextBox(
                                       Sub(txt)
                                           txt.Name = "txtAñoFichas"
                                           txt.Text = ViewData("año_valido_fichas")
                                           txt.Width = 50
                                       End Sub).GetHtml()
                               End Sub)
                       End Sub)
                   frmLayoutConf.Items.Add(
                       Sub(item)
                           item.ShowCaption = DefaultBoolean.False
                           item.SetNestedContent(
                               Sub()
                                   Html.DevExpress().Button(
                                       Sub(btnConsultar)
                                           btnConsultar.Name = "btnActualizarAñoValido"
                                           btnConsultar.UseSubmitBehavior = False
                                           btnConsultar.Text = "Actualizar Año"
                                           btnConsultar.ClientSideEvents.Click = "btnActualizarAñoValidoClick"
                                       End Sub).GetHtml()
                               End Sub)
                       End Sub)
               End Sub).GetHtml()%>
    </div>
    <% Html.RenderPartial("pv_Spinner", "Shared")%>
</asp:Content>
