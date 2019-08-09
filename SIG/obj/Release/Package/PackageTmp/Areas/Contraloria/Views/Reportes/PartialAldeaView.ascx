<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Html.DevExpress().ComboBox(
    Sub(settings)
        settings.Name = "cbxAldea"
        settings.Width = 180
        settings.Properties.DropDownStyle = DropDownStyle.DropDownList
        settings.CallbackRouteValues = New With {.Controller = "Reportes", .Action = "PartialAldeaView"}
        settings.Properties.TextField = "desc_aldea"
        settings.Properties.ValueField = "cod_aldea"
        settings.Properties.NullText = "Seleccione una aldea"
        settings.Properties.ClientSideEvents.BeginCallback = "function(s, e) { e.customArgs['muni'] = cbxMuni.GetValue(); }"
    End Sub).BindList(Model).Render()%>