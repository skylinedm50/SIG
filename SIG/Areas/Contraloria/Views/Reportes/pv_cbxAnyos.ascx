<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% 
    Html.DevExpress().ComboBox(
        Sub(settings)
            settings.Name = "cbxAnios"
            settings.Width = 180
            settings.CallbackRouteValues = New With {.Controller = "Reportes", .Action = "pv_cbxAnyos"}
            settings.Properties.DropDownStyle = DropDownStyle.DropDownList
            settings.Properties.TextField = "pag_anyo"
            settings.Properties.ValueField = "pag_anyo"
            settings.Properties.NullText = "Seleccione un Año"
            settings.Properties.ClientSideEvents.SelectedIndexChanged = "function(s, e) { cbxPagos.PerformCallback(); }"
        End Sub).BindList(Model).Render()
     %>