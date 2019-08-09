<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Corresponsabilidad/Views/PaginasMaestras/Principal.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPrincipal" runat="server">
    <% 
        Html.DevExpress().RenderScripts(Page,
                                        New Script With {.ExtensionSuite = ExtensionSuite.GridView},
                                        New Script With {.ExtensionSuite = ExtensionSuite.Editors},
                                        New Script With {.ExtensionSuite = ExtensionSuite.NavigationAndLayout},
                                        New Script With {.ExtensionSuite = ExtensionSuite.Report}
                                            )

        Html.DevExpress().RenderStyleSheets(Page,
                            New StyleSheet With {.ExtensionSuite = ExtensionSuite.GridView},
                            New StyleSheet With {.ExtensionSuite = ExtensionSuite.Editors},
                            New StyleSheet With {.ExtensionSuite = ExtensionSuite.NavigationAndLayout},
                            New StyleSheet With {.ExtensionSuite = ExtensionSuite.Report}
                        )
    %>
    <div id="tit-form-corres">
        <h2>Búsqueda de Corresponsabilidades de Beneficiario</h2>
    </div>
    <div id="content-form-corres">
            <%
                Html.RenderPartial("Busq_Correspon_Beneficia/_ControlesBusqCorresBenef")
                Html.RenderAction("AspxGridViewResultBusqCorrBenef", New With {Key .intCodBenef = 0,
                                                                                Key .intCodRUPBenef = 0,
                                                                                Key .strIdentiBenef = "null",
                                                                                Key .strNom1Benef = "null",
                                                                                Key .strNom2Benef = "null",
                                                                                Key .strApe1Benef = "null",
                                                                                Key .strApe2Benef = "null",
                                                                                Key .strIdentiTit = "null",
                                                                                Key .intCodHogRUP = 0,
                                                                                Key .intCodHogSIG = 0})
                Html.RenderPartial("Busq_Correspon_Beneficia/_PopupControlViewPartialReport")
            %>
    </div>
    <script src="/Areas/Corresponsabilidad/Scripts/Repor_Busq_Corres_Benef/report_busq_corres_benef.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
