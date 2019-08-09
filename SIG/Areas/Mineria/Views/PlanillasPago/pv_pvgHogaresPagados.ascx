<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Dim settings As PivotGridSettings = SIG.SIG.Areas.Mineria.Controllers.exportPvgHogaresPagados.exportPvgSettings(ViewData("tipo"), ViewData("campos"))

    settings.CallbackRouteValues = New With {Key .Controller = "PlanillasPago", Key .Action = "pv_pvgHogaresPagados", Key .tipo = ViewData("tipo"), Key .campos = ViewData("campos"), Key .strPlanillas = ViewData("planillas")}

    Html.DevExpress().PivotGrid(settings).Bind(Model).GetHtml()

    %>

