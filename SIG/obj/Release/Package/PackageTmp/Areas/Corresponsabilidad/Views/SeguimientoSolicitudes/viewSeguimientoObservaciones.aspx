<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Corresponsabilidad/Views/PaginasMaestras/Principal.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPrincipal" runat="server">
    <% 

        Html.DevExpress().RenderScripts(Page,
                                        New Script With {.ExtensionSuite = ExtensionSuite.GridView},
                                        New Script With {.ExtensionSuite = ExtensionSuite.Editors},
                                        New Script With {.ExtensionSuite = ExtensionSuite.NavigationAndLayout},
                                        New Script With {.ExtensionSuite = ExtensionSuite.Chart}
                                        )

        Html.DevExpress().RenderStyleSheets(Page,
                                    New StyleSheet With {.ExtensionSuite = ExtensionSuite.GridView},
                                    New StyleSheet With {.ExtensionSuite = ExtensionSuite.Editors},
                                    New StyleSheet With {.ExtensionSuite = ExtensionSuite.NavigationAndLayout},
                                    New StyleSheet With {.ExtensionSuite = ExtensionSuite.Chart}
                                    )
    %>

    <div id="tit-form-corres">
        <h2>Seguimiento de Observaciones</h2>
    </div>
    <% Html.BeginForm("exportGridViewSeguimientoObservaciones", "SeguimientoSolicitudes")%>
    <div>
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
                                                        btnConsultar.Name = "Exportar"
                                                        btnConsultar.UseSubmitBehavior = True
                                                        btnConsultar.Text = "Exportar"
                                                    End Sub).GetHtml()
                                        End Sub)
                                End Sub)
                End Sub).GetHtml()%>
    </div>
    <div id="content-form-corres">
           <div id="div-report">
                <% 
                    Html.RenderAction("GridViewSeguimientoObservaciones", "SeguimientoSolicitudes")
                %>
        </div>
    </div>
    <% Html.EndForm()%>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

</asp:Content>
