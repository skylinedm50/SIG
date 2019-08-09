<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%

    Dim settings As GridViewSettings = SIG.SIG.Areas.Contraloria.Controllers.exportGridViewRecibosPagados.ExportGridViewSettings(ViewData("tipo"))

    settings.CallbackRouteValues =
        New With {
            Key .Controller = "Reportes",
            Key .Action = "PartialGridViewRecibosPagados",
            Key .dpto = ViewData("dpto"),
            Key .muni = ViewData("muni"),
            Key .aldea = ViewData("aldea"),
            Key .fondo = ViewData("fondo"),
            Key .banco = ViewData("banco"),
            Key .sucursal = ViewData("sucursal"),
            Key .inicio = ViewData("inicio"),
            Key .fin = ViewData("fin"),
            Key .periodo = ViewData("periodo"),
            Key .esquema = ViewData("esquema"),
            Key .tipo = ViewData("tipo"),
            Key .filtro = ViewData("filtro")}

    %>


    <% Html.DevExpress().GridView(settings).Bind(Model).GetHtml()%>