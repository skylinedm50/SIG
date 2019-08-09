<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% 
    Dim settings As PivotGridSettings = SIG.SIG.Areas.Contraloria.Controllers.exportPvgConsolidadoCargaBancarizacion.ExportPivotGridSettings
    settings.CallbackRouteValues = New With {Key .Controller = "CargarArchivo", Key .Action = "pv_pvgConsolidadoCargaBancarizacion", Key .periodo = ViewData("periodo"), Key .banco = ViewData("banco"), Key .archivo = ViewData("archivo")}
    Html.DevExpress().PivotGrid(settings).Bind(Model).GetHtml()
%>