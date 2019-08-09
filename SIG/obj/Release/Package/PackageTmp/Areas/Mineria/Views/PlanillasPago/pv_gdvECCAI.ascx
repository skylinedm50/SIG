<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% 
    Dim settings As GridViewSettings = SIG.SIG.Areas.Mineria.Controllers.exportGdvECCAI.ExportGridViewSettings()

    settings.CallbackRouteValues = New With {Key .Controller = "PlanillasPago", Key .Action = "pv_gdvECCAI", Key .pago = ViewData("pago"), Key .hogares = ViewData("hogares")}

    'If Not ViewData("referencias") Is Nothing Then
    '    settings.CallbackRouteValues = New With {Key .Controller = "PlanillasPago", Key .Action = "pv_gdvECCAI", Key .referencias = ViewData("referencias")}
    'Else
    '    'settings.CallbackRouteValues = New With {Key .Controller = "PlanillasPago", Key .Action = "pv_gdvECCAI", Key .pago = ViewData("pago"), Key .hogares = ViewData("hogares")}
    'End If
%>

<% Html.DevExpress().GridView(settings).Bind(Model).GetHtml()%>

