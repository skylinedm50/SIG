<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Html.DevExpress().ComboBox(
    Sub(settings)
        settings.Name = "cbxPagos"
        settings.Width = 180
        settings.CallbackRouteValues = New With {.Controller = "Shared", .Action = "pv_cbxPagos"}
        settings.Properties.DropDownStyle = DropDownStyle.DropDownList
        settings.Properties.TextField = "pag_nombre"
        settings.Properties.ValueField = "pag_codigo"
        settings.Properties.NullText = "Seleccione un pago"
        settings.Properties.ClientSideEvents.SelectedIndexChanged = "function(){ try{ fnc_cbxPagosIndexChaged();}catch(err){console.log('La función fnc_cbxPagosIndexChaged no esta definida.');} }"
    End Sub).BindList(Model).GetHtml()%>