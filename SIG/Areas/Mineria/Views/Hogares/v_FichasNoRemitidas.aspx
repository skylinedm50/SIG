<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Mineria/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
Fichas no Remitidas
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.DevExpress().RenderStyleSheets(Page,
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.Editors},
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.Chart})%>
    <% Html.DevExpress().RenderScripts(Page,
        New Script With {.ExtensionSuite = ExtensionSuite.Editors},
        New Script With {.ExtensionSuite = ExtensionSuite.Chart})%>
    <script type="text/javascript" src='<%: ResolveUrl("~/Areas/Mineria/Scripts/hogares.js")%>'></script>

<h2>Fichas No Procesadas por CENISS</h2>

    <div id="divControles">
        <% Html.DevExpress.FormLayout(
           Sub(settings)
               settings.Name = "frmLayoutFiltros"
               settings.ColCount = 2
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
                       group.Caption = "Otros Filtros"
                       group.Items.Add(
                           Sub(item)
                               item.Caption = "Fecha de Envío"
                               item.SetNestedContent(
                                   Sub()
                                       Html.DevExpress.ComboBox(
                                           Sub(cbx)
                                               cbx.Name = "cbxFechas"
                                               cbx.Width = 100
                                               cbx.Properties.DropDownStyle = DropDownStyle.DropDownList
                                               cbx.Properties.TextField = "fecha_envio"
                                               cbx.Properties.ValueField = "fecha_envio"
                                               cbx.Properties.NullText = "Seleccione una Fecha"
                                               cbx.Properties.AnimationType = AnimationType.Slide                                              
                                               cbx.Properties.ValueType = GetType(String)
                                           End Sub).BindList(ViewData("fechas")).GetHtml()
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
                                       btn.Name = "btnConsultarFichasNoRemitidas"
                                       btn.Text = "Consultar"
                                       btn.ClientSideEvents.Click = "btnConsultarFichasNoRemitidasClick"
                                   End Sub).GetHtml()
                           End Sub)
                   End Sub)
           End Sub).GetHtml()%>
    </div>
    <div>
        <% Html.RenderAction("pv_ControlesExportChart", "Shared")%>
    </div>
    <div id="divGrafico">
        <% Html.RenderAction("pv_crtFichasNoRemitidas", "Hogares")%>
    </div>
    <% Html.RenderPartial("pv_Spinner", "Shared")%>
</asp:Content>
