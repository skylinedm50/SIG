<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Mineria/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">Fichas Por Censo y Año</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="titleNavBarText" runat="server">
   Cantidad de Fichas Por Censo y Año
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ButtonNavBar" runat="server">
    <a class="navbar-brand" href="/Mineria/Hogares/Home"><i class="fa fa-arrow-left fa-2x" aria-hidden="true"></i></a>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.DevExpress().GetStyleSheets(
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.Editors},
        New StyleSheet With {.ExtensionSuite = ExtensionSuite.PivotGrid})%>
    <% Html.DevExpress().GetScripts(
        New Script With {.ExtensionSuite = ExtensionSuite.Editors},
        New Script With {.ExtensionSuite = ExtensionSuite.PivotGrid})%>

    <% Html.BeginForm("exportarFichasPorCensoAno", "Hogares")%>
    <div>
        <% Html.RenderAction("pv_ControlesExportar", "Shared")%>
    </div>
    <div>
        <% Html.RenderAction("pv_pvgFichasPorCensoAno", "Hogares")%>
    </div>
    <% Html.EndForm()%>
</asp:Content>
