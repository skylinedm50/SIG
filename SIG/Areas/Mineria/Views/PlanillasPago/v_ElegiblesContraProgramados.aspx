<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Mineria/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">

    <% If (ViewData("variante") = 1) Then %>
            Elegibles Contra Programados
    <% ElseIf (ViewData("variante") = 2) Then %>
            elegibles por componente 
    <% End If %>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="titleNavBarText" runat="server">
    
    <% If (ViewData("variante") = 1) Then %>
            Elegibles Contra Programados
    <% ElseIf (ViewData("variante") = 2) Then %>
            Elegibles por componente 
    <% End If %>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ButtonNavBar" runat="server">
    <a class="navbar-brand" href="/Mineria/PlanillasPago/Home"><i class="fa fa-arrow-left fa-2x" aria-hidden="true"></i></a>
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
            padding-left:20px;
            display:none;
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

<%--'' MODIFICACIÓN PARA PASAR AL PROYECTO FINAL--%>

    <% Html.BeginForm("exportarElegiblesContraProgramado", "PlanillasPago")%>
    <div>
        <% Html.DevExpress.FormLayout(
                Sub(frmLayoutElegVsProg)
                    frmLayoutElegVsProg.Name = "frmLayoutElegVsProg"
                    frmLayoutElegVsProg.Width = 1050
                    frmLayoutElegVsProg.ColCount = 2
                    frmLayoutElegVsProg.Items.Add(
                        Sub(itemElegVsProg)
                            itemElegVsProg.Width = 1050
                            itemElegVsProg.ShowCaption = DefaultBoolean.False
                            itemElegVsProg.VerticalAlign = FormLayoutVerticalAlign.Top
                            itemElegVsProg.SetNestedContent(
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
                                             btnConsultar.Name = "btnConsultarElegibleVsProgramados"
                                             btnConsultar.UseSubmitBehavior = False
                                             btnConsultar.Text = "Consultar"
                                             btnConsultar.ClientSideEvents.Click = "function(s,e){ btnConsultarElegibleVsProgramadosClick(s,e," + ViewData("variante").ToString() + ") }"
                                         End Sub).GetHtml()
                                 End Sub)
                         End Sub)
                 End Sub).GetHtml()%>
    </div>
    <br />
    <div>
        <% Html.RenderAction("pv_ControlesExportar", "Shared")%>
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
