<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Html.DevExpress().ComboBox(
                  Sub(settings)
                      settings.Name = "cbxPagos"
                      settings.Width = 180
                      settings.CallbackRouteValues = New With {.Controller = "Reportes", .Action = "pv_cbxPagos"}
                      settings.Properties.DropDownStyle = DropDownStyle.DropDownList
                      settings.Properties.TextField = "pag_nombre"
                      settings.Properties.ValueField = "pag_codigo"
                      settings.Properties.NullText = "Seleccione un Pago"
                      settings.Properties.ClientSideEvents.BeginCallback = "function(s, e) { e.customArgs['anyo'] = cbxAnios.GetValue(); }"
                  End Sub).BindList(Model).Render() %>