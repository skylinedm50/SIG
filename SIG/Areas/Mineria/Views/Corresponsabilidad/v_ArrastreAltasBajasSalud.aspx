<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Mineria/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
Arrastre Altas y Bajas de Salud
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
    <script type="text/javascript" src='<%: ResolveUrl("~/Areas/Mineria/Scripts/corresponsabilidad.js")%>'></script>
    <script>
        $(document).ready(function () {
            $("#NombreReporte p").text("Arrastre, Altas y Bajas de Niños en Salud entre dos Pagos")
        })
    </script>

    <style>
        #divGridView {
            padding-right:20px
        }

        #divMapa {
            padding-left:20px
        }

        #reportes {
            margin:auto;
            margin-top:50px;
        }

        #exportar {
            margin-left:25%;
            margin-top:50px;
        }
    </style>

     
    <div id="hide">
        <% Html.BeginForm("exportarArrastreAltasBajasSalud", "Corresponsabilidad")%>
        <div>
            <% Html.DevExpress().FormLayout(
                           Sub(frmLayoutComparacion)
                               frmLayoutComparacion.Name = "frmLayoutComparacion"
                               frmLayoutComparacion.Width = 900
                               frmLayoutComparacion.Items.AddGroupItem(
                                   Sub(groupComparacion)
                                       groupComparacion.Caption = "Planillas"
                                       groupComparacion.Items.Add(
                                           Sub(item)
                                               item.Caption = "Pagos"
                                               item.HelpText = "Debe de seleccionar solamente dos pagos."
                                               item.SetNestedContent(
                                                   Sub()
                                                       Html.RenderAction("pv_cbxGridPagos", "Shared")
                                                       Html.DevExpress().Label(
                                                           Sub(lbl)
                                                               lbl.Width = 900
                                                               lbl.Text = "Los pagos seleccionados se mostraran año-número, año-número."
                                                           End Sub).GetHtml()
                                                   End Sub)
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
                        frmLayoutButtons.Width = 1050
                        frmLayoutButtons.Width = Unit.Percentage(50)
                        frmLayoutButtons.Items.Add(
                            Sub(item)
                                item.ShowCaption = DefaultBoolean.False
                                item.SetNestedContent(
                                    Sub()
                                        Html.DevExpress().Button(
                                            Sub(btnConsultar)
                                                btnConsultar.Width = 1010
                                                btnConsultar.Name = "btnConsultarArrastreAltasBajasSalud"
                                                btnConsultar.UseSubmitBehavior = False
                                                btnConsultar.Text = "Consultar"
                                                btnConsultar.ClientSideEvents.Click = "btnConsultarArrastreAltasBajasSaludClick"
                                            End Sub).GetHtml()
                                    End Sub)
                            End Sub)
                    End Sub).GetHtml()%>
        </div>
        <br />
    </div>
    <br />
    <div id="exportar">
        <% Html.RenderAction("pv_ControlesExportar", "Shared")%>
    </div>
    <br />
    <table id="reportes" >
        <tr>
            <td><div id="divGridView"></div></td>
            <td><div id="divMapa"></div></td>
        </tr>
    </table>

    
    <% Html.DevExpress().TextBox(
           Sub(txt)
               txt.Name = "txtPagosSeleccionados"
               txt.Text = "text"
               txt.ClientVisible = False
           End Sub).GetHtml()%>
    <% Html.EndForm()%>
    <% Html.RenderPartial("pv_Spinner", "Shared")%>

</asp:Content>
