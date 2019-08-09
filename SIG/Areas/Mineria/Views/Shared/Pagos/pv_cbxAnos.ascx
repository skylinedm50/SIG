<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Html.DevExpress().ComboBox(
    Sub(settings)
        settings.Name = "cbxAnos"
        settings.Width = 180
        settings.CallbackRouteValues = New With {.Controller = "Shared", .Action = "pv_cbxAnosPago"}
        settings.Properties.DropDownStyle = DropDownStyle.DropDownList
        settings.Properties.TextField = "año_pago"
        settings.Properties.ValueField = "año_pago"
        settings.Properties.NullText = "Seleccione un Año"
        'settings.Properties.ClientSideEvents.SelectedIndexChanged = "function(s, e) { cbxPagosAno.PerformCallback(); }"
        settings.Properties.ClientSideEvents.SelectedIndexChanged = "function(s, e) { try { cbxPagosAno.PerformCallback(); } catch (err) { console.log('error no se encontro el comboBox de los pagos.'); } }"
    End Sub).BindList(Model).GetHtml()%>