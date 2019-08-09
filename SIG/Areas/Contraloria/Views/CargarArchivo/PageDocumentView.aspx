<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Contraloria/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
Consolidado de Archivo de Texto
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="<% Url.Content("~/Scripts/jquery-1.5.1.min.js")%>" type="text/javascript"></script>
    <script src="<% Url.Content("~/Scripts/MicrosoftAjax.js")%>" type="text/javascript"></script>
    <script src="<% Url.Content("~/Scripts/MicrosoftMvcValidation.js")%>" type="text/javascript"></script>
    <% Html.DevExpress().GetStyleSheets(
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.GridView},
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.PivotGrid},
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.HtmlEditor},
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.Editors},
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.NavigationAndLayout},
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.Chart},
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.Report},
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.Scheduler})%>
    <% Html.DevExpress().GetScripts(
        New Script With {.ExtensionSuite = ExtensionSuite.GridView},
        New Script With {.ExtensionSuite = ExtensionSuite.PivotGrid},
        New Script With {.ExtensionSuite = ExtensionSuite.HtmlEditor},
        New Script With {.ExtensionSuite = ExtensionSuite.Editors},
        New Script With {.ExtensionSuite = ExtensionSuite.NavigationAndLayout},
        New Script With {.ExtensionSuite = ExtensionSuite.Chart},
        New Script With {.ExtensionSuite = ExtensionSuite.Report},
        New Script With {.ExtensionSuite = ExtensionSuite.Scheduler})%>
<h2>PageDocumentView</h2>
    <% Html.RenderAction("PartialConsolidado")%>
</asp:Content>
