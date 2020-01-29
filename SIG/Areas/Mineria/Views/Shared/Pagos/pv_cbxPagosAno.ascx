<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Html.DevExpress().ComboBox(
    Sub(settings)
        settings.Name = "cbxPagosAno"
        settings.Width = 900
        settings.Properties.DropDownWidth = 550
        settings.Properties.DropDownStyle = DropDownStyle.DropDownList
        settings.CallbackRouteValues = New With {Key .Controller = "Shared", Key .Action = "pv_cbxPagosAno"}
        settings.Properties.CallbackPageSize = 30
        settings.Properties.IncrementalFilteringMode = IncrementalFilteringMode.StartsWith
        settings.Properties.TextFormatString = "{1}"
        settings.Properties.ValueField = "cod_pago"
        settings.Properties.ValueType = GetType(Integer)
        settings.Properties.NullText = "Seleccione un Pago"
        settings.Properties.ClientSideEvents.BeginCallback = "function(s, e) { e.customArgs['ano'] = cbxAnos.GetValue(); }"
        settings.Properties.ClientSideEvents.SelectedIndexChanged = "function(s, e) { try { cbxCorreponsabilidad.PerformCallback(); /*cbxPagoAnoChanged(s.GetValue());*/  } catch (err) { console.log('error no se encontro la función cbxPagoAnoChanged para el cambio del pago.'); } }"
        
                                   
        settings.Properties.Columns.Add("numero_pago", "Nro Pago").Width = 20
        settings.Properties.Columns.Add("descripcion_pago", "Descripción")
    End Sub).BindList(Model).GetHtml()%>