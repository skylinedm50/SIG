<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Dim settings As PivotGridSettings = SIG.SIG.Areas.Mineria.Controllers.exportPvgComparacionPlanillas.ExportPivotGridSettings(ViewData("pago1"), ViewData("pago2"))
    settings.CallbackRouteValues = New With {Key .Controller = "PlanillasPago", Key .Action = "pv_pvgComparacionPlanillas", Key .strPagos = ViewData("strPagos")}
    Html.DevExpress().PivotGrid(settings).Bind(Model).GetHtml()
    %>