<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Html.DevExpress().ComboBox(
      Sub(settings)
          settings.Name = "cbxEsquemas"
          settings.Width = 180
          settings.CallbackRouteValues = New With {Key .Controller = "Reportes", Key .Action = "pv_esquemas"}
          settings.Properties.DropDownStyle = DropDownStyle.DropDownList
          settings.Properties.TextField = "nombre_esquema"
          settings.Properties.ValueField = "esq_codigo"
          settings.Properties.NullText = "Seleccione un Esquema"
          settings.Properties.ClientSideEvents.BeginCallback = "function(s, e) { e.customArgs['pago'] = cbxPeriodo.GetValue(); }"

      End Sub).BindList(Model).GetHtml()%>

