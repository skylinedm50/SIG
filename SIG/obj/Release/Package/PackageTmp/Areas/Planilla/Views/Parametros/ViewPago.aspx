<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Planilla/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Pago
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
<div id="tit-form-pla"><h2>Pago</h2></div>
    <div id="content-form-pla">
        <%
            Html.RenderPartial("Pago/_ControlesViewPartialPago")
        
            Html.RenderAction("GridViewPartialPagos", "Parametros")
        %>  
        <div id="mensaje-pla"></div>
    </div>
    

     <script src="/Areas/Planilla/Scripts/Pago/script_pago.js"></script>
</asp:Content>
