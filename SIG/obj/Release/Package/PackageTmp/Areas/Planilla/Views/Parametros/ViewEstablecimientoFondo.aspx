<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Planilla/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
Establecimiento de Fondo
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.DevExpress().RenderScripts(Page,
            New Script With {.ExtensionSuite = ExtensionSuite.GridView},
            New Script With {.ExtensionSuite = ExtensionSuite.PivotGrid},
            New Script With {.ExtensionSuite = ExtensionSuite.HtmlEditor},
            New Script With {.ExtensionSuite = ExtensionSuite.Editors},
            New Script With {.ExtensionSuite = ExtensionSuite.NavigationAndLayout},
            New Script With {.ExtensionSuite = ExtensionSuite.Chart},
            New Script With {.ExtensionSuite = ExtensionSuite.Report},
            New Script With {.ExtensionSuite = ExtensionSuite.Scheduler},
            New Script With {.ExtensionSuite = ExtensionSuite.TreeList}
     )
        
        Html.DevExpress().RenderStyleSheets(Page,
            New StyleSheet With {.ExtensionSuite = ExtensionSuite.GridView},
            New StyleSheet With {.ExtensionSuite = ExtensionSuite.PivotGrid},
            New StyleSheet With {.ExtensionSuite = ExtensionSuite.HtmlEditor},
            New StyleSheet With {.ExtensionSuite = ExtensionSuite.Editors},
            New StyleSheet With {.ExtensionSuite = ExtensionSuite.NavigationAndLayout},
            New StyleSheet With {.ExtensionSuite = ExtensionSuite.Chart},
            New StyleSheet With {.ExtensionSuite = ExtensionSuite.Report},
            New StyleSheet With {.ExtensionSuite = ExtensionSuite.Scheduler},
            New StyleSheet With {.ExtensionSuite = ExtensionSuite.TreeList}
        )
    %>

    <div id="tit-form-pla"><h2>Establecimiento de Fondos</h2></div>
    <div id="content-form-pla">
        <%
            Html.RenderAction("FormLayoutControlEsquemasEnlazados", New With {Key .Controller = "Shared", Key .intTipoConfig = 1})
            Html.RenderAction("FormLayoutDetalleEsquemaEnlazado", New With {Key .Controller = "Shared", Key .intTipoConfig = 1, .intCodEsquema = 0})
            Html.RenderPartial("Esquemas_enlazados\_PopupDesEnlazarEsquema", "Shared")
        %>
    </div>
    <script src="/Areas/Planilla/Scripts/Shared/script_esquemas_enlazados.js"></script>
    <script src="/Areas/Planilla/Scripts/Estable_fondos/script_establecimiento_fondo.js"></script>
    <script src="/Areas/Planilla/Scripts/Shared/script_ubi_sig_grid.js"></script>

</asp:Content>
