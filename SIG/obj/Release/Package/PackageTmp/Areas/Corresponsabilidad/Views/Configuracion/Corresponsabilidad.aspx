
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
   <%-- <style>
        .AspxGridViewDetalleCorresponsabilidad_col0 {
                content: 'Nuevo';
                fon
            }
    </style>--%>
    <div id="tit-form-corres">
        <h2>Corresponsabilidades</h2>
    </div>
    <div id="content-form-corres">
        
        <%
            Html.RenderPartial("Corresponsabilidad\_FormLayoutViewPartialCorresponsabilidad")
            Html.RenderPartial("Corresponsabilidad\_PopupControlViewPartialEditCorrespo")
            Html.RenderPartial("Corresponsabilidad\_PopupControlViewPartialBorrarCorrespo")
        %>
   </div>
     

    <script src="/Areas/Corresponsabilidad/Scripts/ScriptsCorresponsabilidad.js"></script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
