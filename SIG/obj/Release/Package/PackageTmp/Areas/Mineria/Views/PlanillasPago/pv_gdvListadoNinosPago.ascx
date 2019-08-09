<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% 
    Dim settings As GridViewSettings = SIG.SIG.Areas.Mineria.Controllers.exportGdvListadoNinosPago.ExportGridViewSettings()
    
    settings.CallbackRouteValues =
        New With {
            Key .Controller = "PlanillasPago",
            Key .Action = "pv_gdvListadoNinosPago",
            Key .pago = ViewData("pago"),
            Key .departamento = ViewData("departamento"),
            Key .municipio = ViewData("municipio"),
            Key .aldea = ViewData("aldea")
        }
    
    %>

<% Html.DevExpress().GridView(settings).Bind(Model).GetHtml()%>