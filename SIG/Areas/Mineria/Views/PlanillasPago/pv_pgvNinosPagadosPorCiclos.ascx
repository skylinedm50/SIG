<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Dim settings As PivotGridSettings = SIG.SIG.Areas.Mineria.Controllers.exportPvgNinosPagadosPorCiclo.exportPvgSettings
    
    settings.CallbackRouteValues = New With {Key .Controller = "PlanillasPago", Key .Action = "pv_pgvNinosPagadosPorCiclos", Key .pago = ViewData("pago")}
       
    Html.DevExpress().PivotGrid(settings).Bind(Model).GetHtml()
    
    %>