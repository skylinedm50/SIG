<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Mineria/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
Niños Pagados Por Ciclo
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.DevExpress().GetStyleSheets(
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.Editors},
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.PivotGrid})%>
    <% Html.DevExpress().GetScripts(
        New Script With {.ExtensionSuite = ExtensionSuite.Editors},
        New Script With {.ExtensionSuite = ExtensionSuite.PivotGrid})%>
    <script type="text/javascript" src='<%: ResolveUrl("~/Areas/Mineria/Scripts/planillasPago.js")%>'></script>

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
      <%--  <h2>Cantidad de Niños Pagados Por Ciclo</h2>--%>
        <% Html.BeginForm("exportarNinosPagadosPorCiclo", "PlanillasPago", New With {Key .variante = ViewData("variante")})%>
        <div>
            <% Html.DevExpress.FormLayout(
                           Sub(frmLayoutNinosPagados)
                               frmLayoutNinosPagados.Width = 1050
                               frmLayoutNinosPagados.Name = "frmLayoutNinosPagados"
                               frmLayoutNinosPagados.ColCount = 2
                               frmLayoutNinosPagados.Items.Add(
                                   Sub(itemNinosPagados)
                                       itemNinosPagados.Width = 1050
                                       itemNinosPagados.ShowCaption = DefaultBoolean.False
                                       itemNinosPagados.VerticalAlign = FormLayoutVerticalAlign.Top
                                       itemNinosPagados.SetNestedContent(
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
                                               btnConsultar.Width = 1050
                                               btnConsultar.Name = "btnConsultarNinosPagadosCiclo"
                                               btnConsultar.UseSubmitBehavior = False
                                               btnConsultar.Text = "Consultar"
                                               btnConsultar.ClientSideEvents.Click = " function(s,e){ btnConsultarNinosPagadosCicloClick(s,e," + ViewData("variante").ToString() + ") }"
                                           End Sub).GetHtml()
                                   End Sub)
                           End Sub)
                   End Sub).GetHtml()%>
        </div>
        <br />
   
        <br />
        <div>
            <% Html.RenderAction("pv_ControlesExportar", "Shared")%>
        </div>
    </div>
    <br />
    <table id="reportes" >
        <tr>
            <td><div id="divGridView"></div></td>
            <td><div id="divMapa"></div></td>
        </tr>
    </table>
    
    <% Html.EndForm()%>
    <% Html.RenderPartial("pv_Spinner", "Shared")%>
</asp:Content>
