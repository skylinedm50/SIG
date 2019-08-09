<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% 
    Dim settings As GridViewSettings = SIG.SIG.Areas.Mineria.Controllers.exportGdvNinosIncumpliendo.ExportGridViewSettings()
    
    'settings.Settings.ShowHeaderFilterButton = True
    'settings.Settings.ShowFilterRow = True
    'settings.SettingsPopup.HeaderFilter.Height = 200
    
    settings.CallbackRouteValues =
        New With {
            Key .Controller = "Corresponsabilidad",
            Key .Action = "pv_gdvNinoIncumplenComp",
            Key .pago = ViewData("pago"),
            Key .hogares = ViewData("hogares"),
            Key .departamento = ViewData("departamento"),
            Key .municipio = ViewData("municipio"),
            Key .aldea = ViewData("aldea")
        }
    
    %>

<% Html.DevExpress().GridView(settings).Bind(Model).GetHtml()%>