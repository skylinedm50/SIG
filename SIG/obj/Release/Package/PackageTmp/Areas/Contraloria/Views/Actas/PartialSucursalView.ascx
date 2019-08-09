<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Html.DevExpress().ComboBox(
    Sub(settings)
        settings.Name = "cbxSucursal"
        'settings.Width = 180
        'settings.Properties.IncrementalFilteringMode = IncrementalFilteringMode.StartsWith
        settings.Properties.DropDownStyle = DropDownStyle.DropDownList
        settings.CallbackRouteValues = New With {.Controller = "Actas", .Action = "PartialSucursalView"}
        settings.Properties.TextField = "desc_sucursal"
        settings.Properties.ValueField = "cod_sucursal"
        settings.Properties.NullText = "Seleccione una sucursal"
        settings.Properties.ClientSideEvents.BeginCallback = "function(s, e) { e.customArgs['banco'] = cbxBanco.GetValue(); }"
    End Sub).BindList(Model).Render()%>