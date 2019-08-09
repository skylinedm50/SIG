<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Dim settings As PivotGridSettings = SIG.SIG.Areas.Mineria.Controllers.exportPvgFichasPorCensoAno.exportPvgSettings
    
    settings.CallbackRouteValues = New With {Key .Controller = "Hogares", Key .Action = "pv_pvgFichasPorCensoAno"}
       
    Html.DevExpress().PivotGrid(settings).Bind(Model).GetHtml()
    
    %>