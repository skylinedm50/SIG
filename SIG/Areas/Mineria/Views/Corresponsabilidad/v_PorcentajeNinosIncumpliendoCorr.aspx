<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Mineria/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
Porcentaje de Incumplimiento
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.DevExpress().GetStyleSheets(
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.GridView},
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.Editors})%>
    <% Html.DevExpress().GetScripts(
        New Script With {.ExtensionSuite = ExtensionSuite.GridView},
        New Script With {.ExtensionSuite = ExtensionSuite.Editors})%>
    <script type="text/javascript" src='<%: ResolveUrl("~/Areas/Mineria/Scripts/corresponsabilidad.js")%>'></script>

    <script>
        $(document).ready(function () {
            $("#NombreReporte p").text("Porcentaje de Niños Incumplieron por Hogar")
        })
    </script>
    <style>
        #exportar {
            margin-left:25%;
            margin-top:50px;
        }

        #divGridView {
            margin:30px;
        }
    </style>

    <% Html.BeginForm("exportarPorcentajeNinosIncumpliendoCorr", "Corresponsabilidad")%>
    <div id="hide">
        <% Html.DevExpress.FormLayout(
                         Sub(settings)
                             settings.Name = "frmLayoutFiltros"
                             settings.ColCount = 3
                             settings.Items.AddGroupItem(
                                 Sub(group)
                                     group.Caption = "Filtros para Pago"
                                     group.Height = 160
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
                             settings.Items.AddGroupItem(
                                 Sub(group)
                                     group.Height = 160
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
                                     group.Height = 160
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
                                     item.ColSpan = 3
                                     item.ShowCaption = DefaultBoolean.False
                                     item.HorizontalAlign = FormLayoutHorizontalAlign.Center
                                     item.SetNestedContent(
                                         Sub()
                                             Html.DevExpress().Button(
                                                 Sub(btn)
                                                     btn.Name = "btnConsultarPorcentajeNinosIncumplen"
                                                     btn.Text = "Consultar"
                                                     btn.Width = 1050
                                                     btn.ClientSideEvents.Click = "btnConsultarPorcentajeNinosIncumplenClick"
                                                 End Sub).GetHtml()
                                         End Sub)
                                 End Sub)
                         End Sub).GetHtml()%>
    </div>
    <br />
    <div id="exportar">
        <% Html.RenderAction("pv_ControlesExportar", "Shared")%>
    </div>
    <br />
    <div id="divGridView"></div>
    <% Html.EndForm()%>
    <% Html.RenderPartial("pv_Spinner", "Shared")%>
</asp:Content>
