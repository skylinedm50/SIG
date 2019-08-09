<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Dim settings As GridViewSettings = SIG.SIG.Areas.Mineria.Controllers.exportGdvHistorialPagos.ExportGridViewSettings
    
    settings.CallbackRouteValues = New With {Key .Controller = "Hogares", Key .Action = "pv_gdvHistorialPagos", Key .hogar = ViewData("hogar")}
    
    settings.SetDetailRowTemplateContent(
        Sub(row)
            Html.RenderAction("pv_PagoHogar", New With {Key .pago = DataBinder.Eval(row.DataItem, "cod_pago"), Key .hogar = ViewData("hogar")})
        End Sub)
    %>

<% Html.DevExpress().GridView(settings).Bind(Model).GetHtml()%>