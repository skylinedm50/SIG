<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Html.DevExpress().ComboBox(
    Sub(cbxLeitz)
        cbxLeitz.Name = "cbxLeitz"
        cbxLeitz.Width = 180
        cbxLeitz.Properties.DropDownWidth = 550
        cbxLeitz.Properties.DropDownStyle = DropDownStyle.DropDownList
        'cbxLeitz.CallbackRouteValues = New With {Key .Controller = "Reportes", Key .Action = "PartialPeriodosView"}
        cbxLeitz.Properties.CallbackPageSize = 30
        cbxLeitz.Properties.IncrementalFilteringMode = IncrementalFilteringMode.StartsWith
        cbxLeitz.Properties.TextFormatString = "{0}"
        cbxLeitz.Properties.ValueField = "cod_leitz"
        'cbxLeitz.Properties.TextField = "nom_periodo"
        cbxLeitz.Properties.ValueType = GetType(Integer)
        'cbxLeitz.Properties.ValueField = "{0}"        
        
        cbxLeitz.Properties.ClientSideEvents.BeginCallback = "cbxLeitzBeginCallback"
        cbxLeitz.CallbackRouteValues = New With {Key .Controller = "Escaneo", Key .Action = "PartialCbxLeitz"}
        cbxLeitz.Properties.NullText = "Seleccione un Leitz"
                                   
        cbxLeitz.Properties.Columns.Add("numero_leitz", "Nro Leitz").Width = 20
        cbxLeitz.Properties.Columns.Add("descripcion_leitz", "Descripción")
        
        
        cbxLeitz.Properties.ClientSideEvents.EndCallback = "cbxLeitzEndCallback"
    End Sub).BindList(Model).GetHtml()%>