<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Corresponsabilidad/Views/PaginasMaestras/Principal.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPrincipal" runat="server">

<% 

    Html.DevExpress().RenderScripts(Page,
                                    New Script With {.ExtensionSuite = ExtensionSuite.GridView},
                                    New Script With {.ExtensionSuite = ExtensionSuite.HtmlEditor},
                                    New Script With {.ExtensionSuite = ExtensionSuite.Editors},
                                    New Script With {.ExtensionSuite = ExtensionSuite.NavigationAndLayout}
                                    )

    Html.DevExpress().RenderStyleSheets(Page,
                    New StyleSheet With {.ExtensionSuite = ExtensionSuite.GridView},
                    New StyleSheet With {.ExtensionSuite = ExtensionSuite.HtmlEditor},
                    New StyleSheet With {.ExtensionSuite = ExtensionSuite.Editors},
                    New StyleSheet With {.ExtensionSuite = ExtensionSuite.NavigationAndLayout}
                    )
    %>

    <div id="tit-form-corres">
        <h2>Errores detectados en peticiones de actualización de corresponsabilidades</h2>
    </div>
    <div id="content-form-corres">
            <%
                Html.RenderPartial("Err_petic_actuali/_ControlesErroPeticActu")
                Html.RenderAction("AspxGridViewErrorPetiActua", New With {Key .intCodComp = 0, Key .intNumActua = 0})
            %>
    </div>
    <script src="/Areas/Corresponsabilidad/Scripts/Repor_err_petic_actuali/report_erro_peti_actu.js"></script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
