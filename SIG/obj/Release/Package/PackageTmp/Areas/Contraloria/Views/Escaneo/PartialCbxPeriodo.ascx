<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Html.DevExpress().ComboBox(
       Sub(cbxPeriodo)
           cbxPeriodo.Name = "cbxPeriodo"
           cbxPeriodo.Width = 180
           cbxPeriodo.Properties.DropDownWidth = 550
           cbxPeriodo.Properties.DropDownStyle = DropDownStyle.DropDownList
           cbxPeriodo.CallbackRouteValues = New With {Key .Controller = "Escaneo", Key .Action = "PartialCbxPeriodo"}
           cbxPeriodo.Properties.CallbackPageSize = 30
           cbxPeriodo.Properties.IncrementalFilteringMode = IncrementalFilteringMode.StartsWith
           cbxPeriodo.Properties.TextFormatString = "{1}"
           'cbxPeriodo.Properties.ValueField = "cod_periodo"
           cbxPeriodo.Properties.ValueField = "pag_codigo"
           'cbxPeriodo.Properties.TextField = "nom_periodo"
           cbxPeriodo.Properties.ValueType = GetType(Integer)
           'cbxPeriodo.Properties.ValueField = "{0}"
           cbxPeriodo.Properties.ClientSideEvents.SelectedIndexChanged = "cbxPeriodoChanged"
                                   
           cbxPeriodo.Properties.Columns.Add("pag_codigo", "Nro Periodo").Width = 20
           cbxPeriodo.Properties.Columns.Add("pag_nombre", "Nombre Periodo")
           cbxPeriodo.Properties.NullText = "Seleccione un Periodo"
       End Sub).BindList(Model).GetHtml()%>