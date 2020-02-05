<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Mineria/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
Hogares Pagados
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="titleNavBarText" runat="server">
  Reporte de Cantidad de Hogares Pagados
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ButtonNavBar" runat="server">
    <a class="navbar-brand" href="/Mineria/PlanillasPago/Home"><i class="fa fa-arrow-left fa-2x" aria-hidden="true"></i></a>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.DevExpress().GetStyleSheets(
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.GridView},
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.PivotGrid}, 
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.Editors})%>
    <% Html.DevExpress().GetScripts(
        New Script With {.ExtensionSuite = ExtensionSuite.GridView},
        New Script With {.ExtensionSuite = ExtensionSuite.PivotGrid},
        New Script With {.ExtensionSuite = ExtensionSuite.Editors})%>
    <script type="text/javascript" src='<%: ResolveUrl("~/Areas/Mineria/Scripts/planillasPago.js")%>'></script>


    <% Html.BeginForm("exportarHogaresPagados", "PlanillasPago")%>
    <% Html.DevExpress().FormLayout(
         Sub(frmLayoutHogaresPagados)
             frmLayoutHogaresPagados.Name = "frmLayoutHogaresPagados"
             frmLayoutHogaresPagados.ColCount = 2
             frmLayoutHogaresPagados.Items.AddGroupItem(
                 Sub(groupHogPag As MVCxFormLayoutGroup)
                     groupHogPag.Caption = "Tipo de Reporte"
                     groupHogPag.Width = Unit.Pixel(200)
                     groupHogPag.Items.Add(
                         Sub(itemHogPag)
                             itemHogPag.ShowCaption = DefaultBoolean.False
                             itemHogPag.SetNestedContent(
                                 Sub()
                                     Html.DevExpress().RadioButtonList(
                                         Sub(rbTiempo)
                                             rbTiempo.Name = "rblPeriodoTiempo"
                                             rbTiempo.Properties.RepeatLayout = RepeatLayout.Table
                                             rbTiempo.Properties.RepeatColumns = 1
                                             rbTiempo.Properties.RepeatDirection = RepeatDirection.Vertical
                                             rbTiempo.ControlStyle.Border.BorderWidth = 0
                                             rbTiempo.Properties.Items.Add("Anual").Selected = True
                                             rbTiempo.Properties.Items.Add("Semestral")
                                             rbTiempo.Properties.Items.Add("Acumulado Planillas")
                                             rbTiempo.Properties.ValueType = GetType(String)
                                             rbTiempo.Properties.ClientSideEvents.SelectedIndexChanged = "rblPeriodoTiempoChanged"
                                         End Sub).GetHtml()
                                 End Sub)
                         End Sub)
                 End Sub)
             frmLayoutHogaresPagados.Items.Add(
                 Sub(itemHogPag)
                     itemHogPag.ShowCaption = DefaultBoolean.False
                     itemHogPag.VerticalAlign = FormLayoutVerticalAlign.Top
                     itemHogPag.SetNestedContent(
                         Sub()
                             Html.ViewContext.Writer.Write("<div id='divFiltro'>")
                             Html.ViewContext.Writer.Write("</div>")
                         End Sub)
                 End Sub)
             frmLayoutHogaresPagados.Items.AddGroupItem(
                 Sub(groupHogPag As MVCxFormLayoutGroup)
                     groupHogPag.Caption = "Campos a obtener"
                     groupHogPag.ColSpan = 2
                     groupHogPag.ColCount = 2
                     groupHogPag.Items.Add(
                         Sub(itemHogPag)
                             itemHogPag.ShowCaption = DefaultBoolean.False
                             itemHogPag.ColSpan = 2
                             itemHogPag.HelpText = "Los datos mostrados en el reporte pueden variar, dependiendo de los campos que elija."
                             itemHogPag.SetNestedContent(
                                 Sub()
                                     Html.DevExpress().CheckBoxList(
                                     Sub(cbl)
                                         cbl.Name = "cblCampos"
                                         cbl.Properties.RepeatLayout = RepeatLayout.Table
                                         cbl.Properties.RepeatDirection = RepeatDirection.Horizontal
                                         cbl.Properties.RepeatColumns = 4
                                         cbl.ControlStyle.BorderStyle = BorderStyle.None
                                         cbl.Properties.Items.Add("Área Geografica").Selected = True
                                         cbl.Properties.Items.Add("Fondo")
                                         cbl.Properties.Items.Add("Pagador")
                                     End Sub).GetHtml()
                                 End Sub)
                         End Sub)
                 End Sub)
         End Sub).GetHtml()%>
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
                                           btnConsultar.Name = "btnConsultarHogPagados"
                                           btnConsultar.UseSubmitBehavior = False
                                           btnConsultar.Text = "Consultar"
                                           btnConsultar.ClientSideEvents.Click = "btnConsultarHogPagadosClick"
                                       End Sub).GetHtml()
                               End Sub)
                       End Sub)
               End Sub).GetHtml()%>
    </div>
    <br />
    <% Html.DevExpress().FormLayout(
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
                                       btnConsultar.Name = "btnExportarHogPagados"
                                       btnConsultar.UseSubmitBehavior = True
                                       btnConsultar.Text = "Exportar"
                                       'btnConsultar.ClientSideEvents.Click = "btnConsultarHogPagadosClick"
                                   End Sub).GetHtml()
                           End Sub)
                   End Sub)
           End Sub).GetHtml()%>
    <div id="divGridView"></div>
    <% Html.EndForm()%>
    <% Html.RenderPartial("pv_Spinner", "Shared")%>
</asp:Content>
