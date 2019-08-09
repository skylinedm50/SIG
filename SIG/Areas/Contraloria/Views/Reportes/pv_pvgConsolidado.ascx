<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% 
    Dim settings As PivotGridSettings = SIG.SIG.Areas.Contraloria.Controllers.exportPvgConsolidadoPago.ExportPivotGridSettings
    settings.CallbackRouteValues = New With {Key .Controller = "Reportes", Key .Action = "pv_pvgConsolidado", Key .pago = ViewData("pago")}
    Html.DevExpress().PivotGrid(settings).Bind(Model).GetHtml()
%>

