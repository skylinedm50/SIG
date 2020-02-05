<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Mineria/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Listado de Titulares
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="titleNavBarText" runat="server">
  Listado de Titulares
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ButtonNavBar" runat="server">
    <a class="navbar-brand" href="/Mineria/PlanillasPago/Home"><i class="fa fa-arrow-left fa-2x" aria-hidden="true"></i></a>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.DevExpress().GetStyleSheets(
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.GridView},
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.Editors})%>
    <% Html.DevExpress().GetScripts(
        New Script With {.ExtensionSuite = ExtensionSuite.GridView},
        New Script With {.ExtensionSuite = ExtensionSuite.Editors})%>
    <script type="text/javascript" src='<%: ResolveUrl("~/Areas/Mineria/Scripts/planillasPago.js")%>'></script>


    <% Html.BeginForm("exportarListadoTitulares", "PlanillasPago")%>
    <div id="divControles">
        <% Html.DevExpress.FormLayout(
           Sub(settings)
               settings.Name = "frmLayoutFiltros"
               'settings.SettingsItems.HorizontalAlign = FormLayoutHorizontalAlign.Right
               settings.ColCount = 3
               settings.Items.AddGroupItem(
                   Sub(group)
                       group.Caption = "Filtros de pago"
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
               'settings.Items.Add(
               '    Sub(item)
               '        item.ShowCaption = DefaultBoolean.False
               '        item.SetNestedContent(
               '            Sub()
               '                Html.RenderPartial("../Shared/Area_Geografica/pv_ControlesAreaGeografica")
               '            End Sub)
               '    End Sub)
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
               settings.Items.AddGroupItem(
                   Sub(group)
                       group.Caption = "Filtro de Hogares"
                       group.Items.Add(
                           Sub(item)
                               item.ShowCaption = DefaultBoolean.False
                               item.SetNestedContent(
                                   Sub()
                                       Html.DevExpress().RadioButtonList(
                                           Sub(rb)
                                               rb.Name = "rbTipo"
                                               rb.Properties.RepeatLayout = RepeatLayout.Table
                                               rb.Properties.RepeatColumns = 1
                                               rb.Properties.RepeatDirection = RepeatDirection.Vertical
                                               rb.ControlStyle.Border.BorderWidth = 0
                                               rb.Properties.Items.Add("Todos").Selected = True
                                               rb.Properties.Items.Add("Programados")
                                               rb.Properties.Items.Add("Excluidos")
                                               rb.Properties.ValueType = GetType(String)
                                               'rb.Properties.ClientSideEvents.ValueChanged = "rbTipovalueChanged"
                                           End Sub).GetHtml()
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
                                       btn.Name = "btnConsultarListadoParticipantes"
                                       btn.Text = "Consultar"
                                       'btn.ClientSideEvents.Click = "btnConsultarListadoParticipantes"
                                       'btn.ClientSideEvents.Click = "btnConsultar"
                                       btn.ClientSideEvents.Click = "btnListadoParticipantes"
                                   End Sub).GetHtml()
                           End Sub)
                   End Sub)
           End Sub).GetHtml()%>
    </div>
    <br />
    <%--<div>
        <% Html.RenderAction("pv_ControlesExportar", "Shared")%>
    </div>--%>
    <br />
    <div id="divGridView">
        <% Html.RenderPartial("pv_gdvMaestroListadoTitulares")%>
    </div>
    <% Html.EndForm()%>
    <% Html.RenderPartial("pv_Spinner", "Shared")%>
</asp:Content>
