<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Html.DevExpress().ComboBox(
    Sub(settings)
        settings.Name = "cbxDpto"
        settings.Width = 180
        'settings.Properties.IncrementalFilteringMode = IncrementalFilteringMode.StartsWith
        settings.CallbackRouteValues = New With {.Controller = "Reportes", .Action = "PartialDptoView"}
        settings.Properties.DropDownStyle = DropDownStyle.DropDownList
        settings.Properties.TextField = "desc_departamento"
        settings.Properties.ValueField = "cod_departamento"
        settings.Properties.NullText = "Seleccione un Departamento"
        'settings.Properties.ClientSideEvents.SelectedIndexChanged = "function(s, e) {alert(s.GetValue())}"
        'settings.Properties.ClientSideEvents.SelectedIndexChanged = "function(s, e) { debugger;cbxMuni.PerformCallback() }"
        settings.Properties.ClientSideEvents.SelectedIndexChanged = "function(s, e) { cbxAldea.SetValue(null);cbxMuni.PerformCallback(); }"
    End Sub).BindList(Model).Render()%>