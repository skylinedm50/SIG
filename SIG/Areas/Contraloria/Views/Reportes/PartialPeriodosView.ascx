<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Html.DevExpress().ComboBox(
     Sub(settings)
         settings.Name = "cbxPeriodo"
         settings.Width = 180
         settings.Properties.DropDownWidth = 550
         settings.Properties.DropDownStyle = DropDownStyle.DropDownList
         settings.CallbackRouteValues = New With {Key .Controller = "Reportes", Key .Action = "PartialPeriodosView"}
         settings.Properties.CallbackPageSize = 30
         settings.Properties.IncrementalFilteringMode = IncrementalFilteringMode.StartsWith
         settings.Properties.TextFormatString = "{1}"
         settings.Properties.ValueField = "pag_codigo"
         settings.Properties.ValueType = GetType(Integer)
         settings.Properties.Columns.Add("pag_codigo", "Nro Periodo").Width = 20
         settings.Properties.Columns.Add("pag_nombre", "Nombre Periodo")
         settings.Properties.ClientSideEvents.SelectedIndexChanged = "function(s, e) { cbxEsquemas.PerformCallback(); }"
     End Sub).BindList(Model).GetHtml()%>