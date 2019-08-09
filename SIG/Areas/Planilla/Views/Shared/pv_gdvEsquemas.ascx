<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Html.DevExpress.GridView(
       Sub(settings)
           settings.Name = "gdvEsquemas"
           settings.Caption = "Esquemas"
           'settings.CallbackRouteValues = New With {Key .Controller = "Shared", Key .Action = "pv_gdvEsquemas", Key .pago = ViewData("pago")}
           settings.CallbackRouteValues = New With {Key .Controller = "Shared", Key .Action = "pv_gdvEsquemas"}
           settings.SettingsPager.PageSize = 10
           settings.KeyFieldName = "esq_codigo"
           settings.CommandColumn.Visible = True
           settings.CommandColumn.ShowSelectCheckbox = True
           settings.CommandColumn.Caption = "Slc"
           settings.ClientSideEvents.BeginCallback = "function(s, e) { e.customArgs['pago'] = cbxPagos.GetValue(); }"
           settings.ClientSideEvents.SelectionChanged = "function(s,e){OnSelectionChanged(s,e)}"
           settings.Width = 650
           
           settings.EnableCallbackAnimation = True
           settings.EnablePagingCallbackAnimation = True
           settings.EnableCallbackCompression = True
           
           
           
           'settings.SettingsBehavior.AllowSelectSingleRowOnly = True
           'settings.Properties.ClientSideEvents.BeginCallback = "function(s, e) { e.customArgs['dpto'] = cbxDpto.GetValue(); }"
           
           
           settings.Columns.Add("nombre_esquema", "Nombre Esquema")
           settings.Columns.Add("esq_tipo_pago", "Tipo Pago")
           settings.Columns.Add("esq_detalle_meses", "Detalle Meses")
       End Sub).Bind(Model).GetHtml()%>