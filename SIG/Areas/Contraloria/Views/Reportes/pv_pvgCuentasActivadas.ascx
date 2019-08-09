<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% 
    Dim settings As PivotGridSettings = SIG.SIG.Areas.Contraloria.Controllers.exportPvgCuentasActivadas.ExportPivotGridSettings
    settings.CallbackRouteValues = New With {Key .Controller = "Reportes", Key .Action = "pv_pvgCuentasActivadas", Key .pago = ViewData("pago")}
    Html.DevExpress().PivotGrid(settings).Bind(Model).GetHtml()
%>