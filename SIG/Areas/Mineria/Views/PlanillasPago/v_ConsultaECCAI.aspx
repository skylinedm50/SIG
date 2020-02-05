<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Mineria/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
ECCAI
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="titleNavBarText" runat="server">
  Estado Cuenta de Cumplimiento, Apercibimiento e Incumplimiento
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ButtonNavBar" runat="server">
    <a class="navbar-brand" href="/Mineria/PlanillasPago/Home"><i class="fa fa-arrow-left fa-2x" aria-hidden="true"></i></a>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.DevExpress().GetStyleSheets(
              New StyleSheet With {.ExtensionSuite = ExtensionSuite.Editors},
              New StyleSheet With {.ExtensionSuite = ExtensionSuite.GridView})%>
    <% Html.DevExpress().GetScripts(
              New Script With {.ExtensionSuite = ExtensionSuite.Editors},
              New Script With {.ExtensionSuite = ExtensionSuite.GridView})%>
    <script type="text/javascript" src='<%: ResolveUrl("~/Areas/Mineria/Scripts/planillasPago.js")%>'></script>
    <link type="text/css" rel="stylesheet" href="/Areas/Mineria/Styles/styles_mineria.css" />


    <% Html.BeginForm("exportarECCAI", "PlanillasPago")%>

    <div>
        <% Html.DevExpress().FormLayout(
                  Sub(flo)
                      flo.Name = "floControles"
                      flo.ColCount = 2
                      flo.Width = Unit.Pixel(500)
                      flo.Items.AddGroupItem(
                      Sub(group As MVCxFormLayoutGroup)
                          group.Caption = "Filtrar Por"
                          group.ColCount = 2
                          group.ColSpan = 2
                          group.Items.Add(
                          Sub(item)
                              item.ShowCaption = DefaultBoolean.False
                              item.ColSpan = 2
                              item.HorizontalAlign = FormLayoutHorizontalAlign.Center
                              item.SetNestedContent(
                              Sub()
                                  Html.DevExpress().RadioButtonList(
                                  Sub(rbl)
                                      rbl.Name = "rblFiltro"
                                      rbl.Properties.RepeatLayout = RepeatLayout.Table
                                      rbl.Properties.RepeatColumns = 2
                                      rbl.Properties.RepeatDirection = RepeatDirection.Horizontal
                                      rbl.ControlStyle.Border.BorderWidth = 0
                                      rbl.Properties.Items.Add("Referencia").Selected = True
                                      rbl.Properties.Items.Add("Código de Hogar y Pago")
                                      rbl.Properties.ClientSideEvents.SelectedIndexChanged = "rblFiltroSelectedIndexChanged"
                                  End Sub).GetHtml()
                              End Sub)
                          End Sub)
                          group.Items.Add(
                          Sub(item)
                              item.Caption = "Año"
                              item.Name = "itemAño"
                              item.ClientVisible = False
                              item.SetNestedContent(
                              Sub()
                                  Html.RenderAction("pv_cbxAnos", "Shared")
                              End Sub)
                          End Sub)
                          group.Items.Add(
                          Sub(item)
                              item.Caption = "Pago"
                              item.Name = "itemPago"
                              item.ClientVisible = False
                              item.SetNestedContent(
                              Sub()
                                  Html.RenderPartial("../Shared/Pagos/pv_cbxPagosAno")
                              End Sub)
                          End Sub)
                      End Sub)
                  End Sub).GetHtml() %>
    </div>
    <br />
    <div>    
        <div class="col_25" >
            <% Html.DevExpress.FormLayout(
                Sub(settings)
                    settings.Name = "frmLayoutHogares"
                    settings.Items.Add(
                        Sub(item)
                            item.Caption = "Referencias"
                            item.Name = "lblFiltro"
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
                                            btn.Name = "btnECCAI"
                                            btn.Text = "Obtener Estado"
                                            btn.ClientSideEvents.Click = "btnECCAIClick"
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
