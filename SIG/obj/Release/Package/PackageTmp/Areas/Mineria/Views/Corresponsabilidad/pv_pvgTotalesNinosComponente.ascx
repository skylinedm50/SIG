<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Dim settings As PivotGridSettings = SIG.SIG.Areas.Mineria.Controllers.exportPvgTotalesNinosComponente.ExportPivotGridSettings
    
    settings.CallbackRouteValues = New With {Key .Controller = "Corresponsabilidad", Key .Action = "pv_pvgTotalesNinosComponente", Key .pago = ViewData("pago")}
       
    
    Html.DevExpress().PivotGrid(settings).Bind(Model).GetHtml()
    
    %>