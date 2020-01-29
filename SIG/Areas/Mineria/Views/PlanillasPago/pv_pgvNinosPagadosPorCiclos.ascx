<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Dim settings As PivotGridSettings = SIG.SIG.Areas.Mineria.Controllers.exportPvgNinosPagadosPorCiclo.exportPvgSettings(ViewData("variante"))

    settings.CallbackRouteValues = New With {Key .Controller = "PlanillasPago", Key .Action = "pv_pgvNinosPagadosPorCiclos", Key .pago = ViewData("pago"), Key .variante = ViewData("variante")}

    Html.DevExpress().PivotGrid(settings).Bind(Model).GetHtml()

    %>