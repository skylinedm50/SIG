<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Corresponsabilidad/Views/PaginasMaestras/Principal.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPrincipal" runat="server">
     <% 

        Html.DevExpress().RenderScripts(Page,
                                        New Script With {.ExtensionSuite = ExtensionSuite.GridView},
                                        New Script With {.ExtensionSuite = ExtensionSuite.PivotGrid},
                                        New Script With {.ExtensionSuite = ExtensionSuite.HtmlEditor},
                                        New Script With {.ExtensionSuite = ExtensionSuite.Editors},
                                        New Script With {.ExtensionSuite = ExtensionSuite.NavigationAndLayout},
                                        New Script With {.ExtensionSuite = ExtensionSuite.Chart}
                                        )

        Html.DevExpress().RenderStyleSheets(Page,
                                        New StyleSheet With {.ExtensionSuite = ExtensionSuite.GridView},
                                        New StyleSheet With {.ExtensionSuite = ExtensionSuite.PivotGrid},
                                        New StyleSheet With {.ExtensionSuite = ExtensionSuite.HtmlEditor},
                                        New StyleSheet With {.ExtensionSuite = ExtensionSuite.Editors},
                                        New StyleSheet With {.ExtensionSuite = ExtensionSuite.NavigationAndLayout},
                                        New StyleSheet With {.ExtensionSuite = ExtensionSuite.Chart}
                                        )
    %>

    <style>
        .colorTitular {
            background-color: rgb(230, 242, 255);
        }
    </style>
    <div id="tit-form-corres">
        <h2>Reporte CAI (Cumplimiento Apercibimiento e Incumplimiento) Corresponsabilidad</h2>
    </div>
    <div id="content-form-corres">
            <%
                Html.RenderPartial("CAI/_ControlesReportCAI")
            %>
        <div id="div-bloqueo-carga">
            <img id="gif-carga-datos" src="/Areas/Corresponsabilidad/Images/Gif/gif_carga_data.GIF"/>
        </div>
        <div id="div-report">
            <%
                Html.RenderAction("AspxGridViewCAI", "Reportes", New With {Key .intCodTipBusq = 0, Key .intAño = 0, Key .strCodPago = 0, Key .strCodCorres = 0,
                                                                           Key .strCodDep = "0", Key .strCodMun = "0", Key .strCodAld = "0", Key .strCodCas = "0"})
            %>
        </div>
    </div>
    <script src="/Areas/Corresponsabilidad/Scripts/Report_CAI/report_cai.js"></script>
    <script src="/Areas/Corresponsabilidad/Scripts/Shared/ScriptComBoxUbicacionSIG.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
