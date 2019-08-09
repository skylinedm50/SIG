<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% 
    Dim settings As GridViewSettings = SIG.SIG.Areas.Contraloria.Controllers.exportGridCuentasBasicas.ExportGridViewSettings

    settings.CallbackRouteValues = New With {Key .Controller = "Reportes", Key .Action = "pv_gdvCuentasBasicas", Key .pago = ViewData("pago")}

     %>

<% Html.DevExpress().GridView(settings).Bind(Model).GetHtml()%>

