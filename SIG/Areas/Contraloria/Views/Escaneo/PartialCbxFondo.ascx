<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Html.DevExpress().ComboBox(
    Sub(settings)
        settings.Name = "cbxFondo"
        settings.Width = 180
        'settings.Properties.IncrementalFilteringMode = IncrementalFilteringMode.StartsWith
        'settings.CallbackRouteValues = New With {.Controller = "Reportes", .Action = "PartialFondoView"}
        settings.CallbackRouteValues = New With {.Controller = "Escaneo", .Action = "PartialCbxFondo"}
        settings.Properties.DropDownStyle = DropDownStyle.DropDownList
        'settings.Properties.TextField = "nombre_fondo"
        'settings.Properties.ValueField = "cod_fondo"
        settings.Properties.TextField = "fond_nombre"
        settings.Properties.ValueField = "fond_codigo"
        settings.Properties.NullText = "Seleccione un Fondo"
        
        'settings.Properties.ClientSideEvents.SelectedIndexChanged = "function(s, e) { cbxMuni.PerformCallback(); }"        
        'settings.Properties.ClientSideEvents.BeginCallback = "function(s, e) { e.customArgs['dpto'] = cbxDpto.GetValue(); }"
        settings.Properties.ClientSideEvents.BeginCallback = "cbxFondoBeginCallback"
        settings.Properties.ClientSideEvents.SelectedIndexChanged = "cbxFondoChanged"
        
    End Sub).BindList(Model).Render()%>
