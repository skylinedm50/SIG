<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Dim settings As PivotGridSettings = SIG.SIG.Areas.Mineria.Controllers.exportPvgRazonCaidaHogares.ExportPivotGridSettings
    settings.CallbackRouteValues = New With {Key .Controller = "PlanillasPago", Key .Action = "pv_pvgRazonCaidaHogares", Key .strPagos = ViewData("strPagos")}
    Html.DevExpress().PivotGrid(settings).Bind(Model).GetHtml()
    %>