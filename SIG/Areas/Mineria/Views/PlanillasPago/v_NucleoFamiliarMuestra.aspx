<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Mineria/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
Nucleo Familiar
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.DevExpress().GetStyleSheets(
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.PivotGrid},
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.Editors})%>
    <% Html.DevExpress().GetScripts(
        New Script With {.ExtensionSuite = ExtensionSuite.PivotGrid},
        New Script With {.ExtensionSuite = ExtensionSuite.Editors})%>
    <script type="text/javascript" src='<%: ResolveUrl("~/Areas/Mineria/Scripts/planillasPago.js")%>'></script>
    <link type="text/css" rel="stylesheet" href="/Areas/Mineria/Styles/styles_mineria.css" />

    <h2>Nucleo Familiar</h2>
    <% Html.BeginForm("exportarNucleoFamiliarMuestra", "PlanillasPago")%>
    <div>
        <% Html.DevExpress.FormLayout(
            Sub(settings)
                settings.Name = "frmLayoutFiltrosPago"
                'settings.SettingsItems.HorizontalAlign = FormLayoutHorizontalAlign.Right
                'settings.ColCount = 2
                settings.Items.AddGroupItem(
                    Sub(group)
                        group.Caption = "Filtros para el Pago"
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
            End Sub).GetHtml()%>
    </div>
    <div>    
        <div class="col_25" >
            <% Html.DevExpress.FormLayout(
                Sub(settings)
                    settings.Name = "frmLayoutHogares"
                    'settings.SettingsItems.HorizontalAlign = FormLayoutHorizontalAlign.Right
                    'settings.ColCount = 2                    
                    settings.Items.Add(
                        Sub(item)
                            item.Caption = "Códigos de Hogares"
                            item.CaptionSettings.Location = LayoutItemCaptionLocation.Top
                            item.SetNestedContent(
                                Sub()
                                    Html.DevExpress().Memo(
                                        Sub(txtHogares)
                                            txtHogares.Name = "txtHogares"
                                            txtHogares.Width = 200
                                            txtHogares.Height = 400
                                        End Sub).GetHtml()
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
                                            btn.Name = "btnNucleoFamiliarMuestra"
                                            btn.Text = "Consultar"
                                            btn.ClientSideEvents.Click = "btnNucleoFamiliarMuestraClick"
                                        End Sub).GetHtml()
                                End Sub)
                        End Sub)
                End Sub).GetHtml()%>
        </div>
        <div class="col_75">
            <div>
                <% Html.RenderAction("pv_ControlesExportar", "Shared")%>
            </div>
            <br />
            <div id="divGridView"></div>
        </div>
    </div>
    <% Html.DevExpress().TextBox(
           Sub(txtHiden)
               txtHiden.Name = "txtHogaresHiden"
               txtHiden.ClientEnabled = True
               txtHiden.ClientVisible = False
           End Sub).GetHtml()%> 
    <% Html.EndForm()%>
    <% Html.RenderPartial("pv_Spinner", "Shared")%>

</asp:Content>
