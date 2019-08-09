<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Contraloria/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
v_cuentasBasicas
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.DevExpress().GetStyleSheets(
                        New StyleSheet With {.ExtensionSuite = ExtensionSuite.GridView},
                        New StyleSheet With {.ExtensionSuite = ExtensionSuite.Editors})%>
    <% Html.DevExpress().GetScripts(
                  New Script With {.ExtensionSuite = ExtensionSuite.GridView},
                  New Script With {.ExtensionSuite = ExtensionSuite.Editors})%>
    <script type="text/javascript" src='<%: ResolveUrl("~/Areas/Contraloria/Scripts/reportes.js")%>'></script>
    <style>

        .btnCuentasBasicas, #divGridView {
            margin: 20px 0 0 20px;
        }

        #divReporte {
            display: none;
        }

    </style>

    <h2>Detalle de las cuentas Básicas</h2>
    <% Html.BeginForm("exportDetalleCuentasBasicas", "Reportes")%>
    <% Html.DevExpress().FormLayout(
                    Sub(frmFiltros)
                        frmFiltros.Name = "frmFiltros"
                        frmFiltros.Items.AddGroupItem(
                        Sub(group)
                            group.Caption = "Filtros"
                            group.ColCount = 2
                            group.Items.Add(
                            Sub(item)
                                item.Caption = "Año"
                                item.SetNestedContent(
                                Sub()
                                    Html.RenderAction("pv_cbxAnyos")
                                End Sub)
                            End Sub)
                            group.Items.Add(
                            Sub(item)
                                item.Caption = "Pago"
                                item.SetNestedContent(
                                Sub()
                                    Html.RenderPartial("pv_cbxPagos")
                                End Sub)
                            End Sub)
                        End Sub)

                    End Sub).GetHtml() %>
    <% Html.DevExpress().Button(
                     Sub(settings)
                         settings.Name = "btnCuentasBasicas"
                         settings.Text = "Consultar"
                         settings.ClientSideEvents.Click = "btnCuentasBasicasClick"
                         settings.ControlStyle.CssClass = "btnCuentasBasicas"
                     End Sub).GetHtml()%>
    <div id="divReporte">
        <div>
            <% Html.DevExpress().FormLayout(
            Sub(frmLayoutExport)
                frmLayoutExport.Name = "frmLayoutExport"
                frmLayoutExport.ColCount = 2
               'frmLayoutExport.Items.Add(
               '    Sub(item)
               '        item.Caption = "Exportar a"
               '        item.SetNestedContent(
               '            Sub()
               '                Html.DevExpress().ComboBox(
               '                    Sub(cbx)
               '                        cbx.Name = "cbxExpotar"
               '                        cbx.Width = 100
               '                        cbx.SelectedIndex = 0
               '                        cbx.Properties.Items.Add("Excel")
               '                        cbx.Properties.Items.Add("Csv")
               '                        cbx.Properties.Items.Add("Pdf")
               '                        cbx.Properties.Items.Add("Rtf")
               '                        cbx.Properties.Items.Add("Html")
               '                        cbx.Properties.ValueType = GetType(String)
               '                    End Sub).GetHtml()
               '            End Sub)
               '    End Sub)
               frmLayoutExport.Items.Add(
                    Sub(item)
                            item.ShowCaption = DefaultBoolean.False
                            item.SetNestedContent(
                               Sub()
                                Html.DevExpress().Button(
                                    Sub(btnConsultar)
                                        btnConsultar.Name = "Exportar"
                                        btnConsultar.UseSubmitBehavior = True
                                        btnConsultar.Text = "Exportar"
                                    End Sub).GetHtml()
                            End Sub)
                        End Sub)
            End Sub).GetHtml()%>
        </div>
        <div id="divGridView"></div>
    </div>
    
    <% Html.EndForm()%>
    <% Html.RenderPartial("pv_Spinner") %>

</asp:Content>
