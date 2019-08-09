<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% 
    'Dim settings As GridViewSettings = SIG.SIG.Areas.Mineria.Controllers.exportGdvListadoParticipantes.ExportGridViewSettings(ViewData("pago"))
    Dim settings As GridViewSettings = SIG.SIG.Areas.Mineria.Controllers.exportGdvListadoParticipantes.ExportGridViewSettings()
    
    settings.CallbackRouteValues =
        New With {
            Key .Controller = "PlanillasPago",
            Key .Action = "pv_gdvMaestroListadoTitulares",
            Key .pago = ViewData("pago"),
            Key .tipo = ViewData("tipo"),
            Key .departamento = ViewData("departamento"),
            Key .municipio = ViewData("municipio"),
            Key .aldea = ViewData("aldea")
        }
    settings.SetDetailRowTemplateContent(
        Sub(row)
            Html.RenderAction("pv_EstadoCuentaTitular", New With {Key .pago = ViewData("pago"), Key .identidad = DataBinder.Eval(row.DataItem, "identidad_titular")})
        End Sub)
    
    %>

<% Html.DevExpress().GridView(settings).Bind(Model).GetHtml()%>