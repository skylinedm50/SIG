<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Html.DevExpress().ComboBox(
    Sub(settings)
        settings.Name = "cbxDpto"
        settings.Width = 180
        settings.CallbackRouteValues = New With {.Controller = "Shared", .Action = "pv_cbxDepartamentos"}
        settings.Properties.DropDownStyle = DropDownStyle.DropDownList
        settings.Properties.TextField = "desc_departamento"
        settings.Properties.ValueField = "cod_departamento"
        settings.Properties.NullText = "Seleccione un Departamento"
        settings.Properties.ClientSideEvents.SelectedIndexChanged = "function(s, e) { cbxAldea.SetValue(null);cbxMuni.PerformCallback(); }"
    End Sub).BindList(Model).GetHtml()%>