<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Mineria/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
 Proyección Elegibles Contra Programados
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.DevExpress().GetStyleSheets(
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.Editors},
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.PivotGrid})%>
    <% Html.DevExpress().GetScripts(
        New Script With {.ExtensionSuite = ExtensionSuite.Editors},
        New Script With {.ExtensionSuite = ExtensionSuite.PivotGrid})%>
    <script type="text/javascript" src='<%: ResolveUrl("~/Areas/Mineria/Scripts/proyecciones.js")%>'></script>

    <h2>Elegibles Contra Programados de Proyección</h2>
    <% Html.BeginForm("exportarElegiblesContraProgramado", "Proyecciones")%>
    <div>
        <% Html.DevExpress.FormLayout(
               Sub(frmLayoutElegVsProg)
                   frmLayoutElegVsProg.Name = "frmLayoutElegVsProg"
                   frmLayoutElegVsProg.ColCount = 2
                   frmLayoutElegVsProg.Items.Add(
                       Sub(itemElegVsProg)
                           itemElegVsProg.ShowCaption = DefaultBoolean.False
                           itemElegVsProg.VerticalAlign = FormLayoutVerticalAlign.Top
                           itemElegVsProg.SetNestedContent(
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
                                           btnConsultar.Name = "btnConsultarElegibleVsProgramados"
                                           btnConsultar.UseSubmitBehavior = False
                                           btnConsultar.Text = "Consultar"
                                           btnConsultar.ClientSideEvents.Click = "btnConsultarElegibleVsProgramadosClick"
                                       End Sub).GetHtml()
                               End Sub)
                       End Sub)
               End Sub).GetHtml()%>
    </div>
    <br />
    <div>
        <%--<% Html.DevExpress().FormLayout(
               Sub(frmLayoutExport)
                   frmLayoutExport.Name = "frmLayoutExport"
                   frmLayoutExport.ColCount = 2
                   frmLayoutExport.Items.Add(
                       Sub(item)
                           item.Caption = "Exportar a"
                           item.SetNestedContent(
                               Sub()
                                   Html.DevExpress().ComboBox(
                                       Sub(cbx)
                                           cbx.Name = "cbxExpotar"
                                           cbx.Width = 100
                                           cbx.SelectedIndex = 0
                                           cbx.Properties.Items.Add("Excel")
                                           cbx.Properties.Items.Add("Csv")
                                           cbx.Properties.Items.Add("Pdf")
                                           cbx.Properties.Items.Add("Rtf")
                                           cbx.Properties.Items.Add("Html")
                                           cbx.Properties.ValueType = GetType(String)
                                       End Sub).GetHtml()
                               End Sub)
                       End Sub)
                   frmLayoutExport.Items.Add(
                       Sub(item)
                           item.ShowCaption = DefaultBoolean.False
                           item.SetNestedContent(
                               Sub()
                                   Html.DevExpress().Button(
                                       Sub(btnConsultar)
                                           btnConsultar.Name = "btnExportarElegibleVsProgramados"
                                           btnConsultar.UseSubmitBehavior = True
                                           btnConsultar.Text = "Exportar"
                                           'btnConsultar.ClientSideEvents.Click = "btnConsultarHogPagadosClick"
                                       End Sub).GetHtml()
                               End Sub)
                       End Sub)
               End Sub).GetHtml()%>--%>
        <% Html.RenderAction("pv_ControlesExportar", "Shared")%>
    </div>
    <br />
    <div id="divGridView"></div>
    <% Html.EndForm()%>
    <% Html.RenderPartial("pv_Spinner", "Shared")%>
</asp:Content>
