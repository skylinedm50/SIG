<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Contraloria/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
Reporte Consolidado
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.DevExpress().GetStyleSheets(
                                                New StyleSheet With {.ExtensionSuite = ExtensionSuite.GridView},
                                                New StyleSheet With {.ExtensionSuite = ExtensionSuite.Editors},
                                                New StyleSheet With {.ExtensionSuite = ExtensionSuite.PivotGrid})%>
    <% Html.DevExpress().GetScripts(
                                    New Script With {.ExtensionSuite = ExtensionSuite.GridView},
                                    New Script With {.ExtensionSuite = ExtensionSuite.Editors},
                                    New Script With {.ExtensionSuite = ExtensionSuite.PivotGrid})%>
    <script type="text/javascript" src='<%: ResolveUrl("~/Areas/Contraloria/Scripts/reportes.js")%>'></script>
<h2>Reporte Consolidado</h2>
    <style type="text/css">
        fieldset {
            border: 1px solid #C2C4CB;
            border-radius: 4px;
            font: 11px Verdana, Geneva, sans-serif;
            /*width: 441px;*/
            padding-right: 9px;
        }

        legend {
            color: #A1A3AA;
            text-align: left;
        }
        
        .col33 {
            /*background-color: aqua;*/
            display: inline-block;
            /*height: 200px;*/
            margin: 0.8% 0 0 0.6%;
            /*width: 32%;*/
            /*width: 297%;*/
            vertical-align: top;
        }

        .col66 {
            display: inline-block;
            margin: 0.8% 0 0 0.6%;
            vertical-align: middle;
        }

        .buttonReporteConsolidado {
            /*margin: 20px 0 0 20px;*/
            margin: 0 auto;
        }

        .button2 {
            /*margin: 20px 0 0 20px;*/
            margin-right: 10px;
        }

        .centrar {
            width: 40%;
            margin: 0 auto;
        }

        #divControlesConsolidado {
            width: 1100px;
        }

        #divBotones {
            width: 45%;
            margin: 0 auto;
        }
    </style>
    <% Html.BeginForm("exportConsolidadoToExcel", "Reportes")%>
    <div id="divPeriodos">
        <div id="divGrid">
            <% Html.RenderAction("PartialGridPeriodos") %>
        </div>
        <% Html.Hidden("selectedIDsHF")%>
        <br />
        <div id="divBotones">
            <% Html.DevExpress().Button(
                                   Sub(settings)
                                       settings.Name = "btnConsultar"
                                       settings.Text = "Consultar"
                                       settings.ClientSideEvents.Click = "obtenerConsolidado"
                                       settings.ControlStyle.CssClass = "buttonReporteConsolidado"
                                   End Sub).GetHtml()%>
            &nbsp&nbsp&nbsp&nbsp&nbsp
            <% Html.DevExpress().Button(
                                   Sub(settings)
                                       settings.Name = "btnExportar"
                                       settings.Text = "Exportar"
                                       settings.ControlStyle.CssClass = "buttonReporteConsolidado"
                                       settings.UseSubmitBehavior = True
                                       settings.RouteValues = New With {Key .Controller = "Reportes", Key .Action = "exportConsolidadoToExcel"}
                                   End Sub).GetHtml()%>
        </div>
    </div>
    <br />
    <div id="divGridConsolidado"></div>
    <% Html.DevExpress().TextBox(
           Sub(txt)
               txt.Name = "txtPago"
               txt.Text = "text"
               txt.ClientVisible = False
           End Sub).GetHtml()%>
    <% Html.EndForm()%>
</asp:Content>
