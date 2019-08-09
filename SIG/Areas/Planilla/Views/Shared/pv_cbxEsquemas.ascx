<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Html.DevExpress().ComboBox(
    Sub(settings)
        settings.Name = "cbxEsquemas"
        settings.Width = 180
        settings.Properties.DropDownStyle = DropDownStyle.DropDownList
        settings.CallbackRouteValues = New With {.Controller = "Shared", .Action = "pv_cbxEsquemas"}
        settings.Properties.TextField = "nombre_esquema"
        settings.Properties.ValueField = "esq_codigo"
        settings.Properties.NullText = "Seleccione un esquema"
        settings.Properties.ClientSideEvents.BeginCallback = "function(s, e) { e.customArgs['pago'] = cbxPagos.GetValue(); }"
        settings.Properties.ClientSideEvents.SelectedIndexChanged = "function(s, e){ try{fnc_cbxEsquemasIndexChanged();}catch(err){console.log('No se encontro la función fnc_cbxEsquemasIndexChanged.')};} "
    End Sub).BindList(Model).Render()%>