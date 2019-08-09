<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Dim settings As PivotGridSettings = SIG.SIG.Areas.Mineria.Controllers.exportPvgResultadoFichas.exportPvgSettings
    
    settings.CallbackRouteValues = New With {Key .Controller = "Hogares", Key .Action = "pv_pvgResultadoFichas"}
       
    Html.DevExpress().PivotGrid(settings).Bind(Model).GetHtml()
    
    %>