<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Html.DevExpress().ComboBox(
    Sub(settings)
        settings.Name = "cbxBanco"
        settings.Width = 180
        'settings.Properties.IncrementalFilteringMode = IncrementalFilteringMode.StartsWith
        settings.CallbackRouteValues = New With {.Controller = "Reportes", .Action = "PartialBancoView"}
        settings.Properties.DropDownStyle = DropDownStyle.DropDownList
        settings.Properties.TextField = "nombre_banco"
        settings.Properties.ValueField = "cod_banco"
        settings.Properties.NullText = "Seleccione un Banco"
        settings.Properties.ClientSideEvents.SelectedIndexChanged = "function(s, e) { cbxSucursal.PerformCallback(); }"
    End Sub).BindList(Model).Render()%>