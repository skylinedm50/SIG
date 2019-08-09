<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%       
    Dim settings As PivotGridSettings = SIG.SIG.Areas.Mineria.Controllers.exportPvgNucleoFamiliarMuestra.exportPvgSettings
    
    settings.CallbackRouteValues =
        New With {
            Key .Controller = "PlanillasPago",
            Key .Action = "pv_pvgNucleoFamiliarMuestra",
            Key .pago = ViewData("pago"),
            Key .hogares = ViewData("hogares")
        }
    
    %>

<% Html.DevExpress().PivotGrid(settings).Bind(Model).GetHtml()%>