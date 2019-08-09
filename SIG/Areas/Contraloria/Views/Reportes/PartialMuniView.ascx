<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Html.DevExpress().ComboBox(
    Sub(settings)
        settings.Name = "cbxMuni"
        settings.Width = 180
        settings.Properties.DropDownStyle = DropDownStyle.DropDownList
        settings.CallbackRouteValues = New With {.Controller = "Reportes", .Action = "PartialMuniView"}
        settings.Properties.TextField = "desc_municipio"
        settings.Properties.ValueField = "cod_municipio"
        settings.Properties.NullText = "Seleccione un Municipio"
        settings.Properties.ClientSideEvents.BeginCallback = "function(s, e) { e.customArgs['dpto'] = cbxDpto.GetValue(); }"
        settings.Properties.ClientSideEvents.SelectedIndexChanged = "function(s, e) { cbxAldea.PerformCallback(); }"
    End Sub).BindList(Model).Render()%>