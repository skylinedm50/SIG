<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Dim settings As PivotGridSettings = SIG.SIG.Areas.Planilla.Controllers.exportPvgPlanillaGenerada.ExportPivotGridSettings

    settings.CallbackRouteValues = New With {Key .Controller = "Ejecucion", Key .Action = "pv_gdvPlanillaGenerada", Key .strEsquemas = ViewData("strEsquemas")}
    Html.DevExpress().PivotGrid(settings).Bind(Model).GetHtml()

    %>