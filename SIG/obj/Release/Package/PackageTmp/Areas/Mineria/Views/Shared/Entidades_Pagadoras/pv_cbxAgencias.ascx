<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Html.DevExpress().ComboBox(
    Sub(settings)
        settings.Name = "cbxAgencias"
        settings.Width = 180
        settings.Properties.DropDownStyle = DropDownStyle.DropDownList
        settings.CallbackRouteValues = New With {.Controller = "Shared", .Action = "pv_cbxAgencias"}
        settings.Properties.TextField = "desc_agencia"
        settings.Properties.ValueField = "cod_agencia"
        settings.Properties.NullText = "Seleccione una Agencia"
        settings.Properties.ClientSideEvents.BeginCallback = "function(s, e) { e.customArgs['entidad'] = cbxPagadores.GetValue(); }"
    End Sub).BindList(Model).Render()%>