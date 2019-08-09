<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%@ Import Namespace="modulo_corresponsabilidad" %>

<%
    Dim objDatosReporte As New SIG.SIG.Areas.Corresponsabilidad.Models.cl_data_reporte

    Html.DevExpress.ComboBox(Sub(configuracionComboBox As ComboBoxSettings)
                                 configuracionComboBox.Name = "AspxComboBoxDepartamentoSIG"
                                 configuracionComboBox.Properties.ClientInstanceName = "AspxComboBoxDepartamentoSIG"
                                 configuracionComboBox.Properties.DropDownStyle = DropDownStyle.DropDownList
                                 configuracionComboBox.Properties.IncrementalFilteringMode = IncrementalFilteringMode.StartsWith
                                 configuracionComboBox.Properties.ValueField = "cod_departamento"
                                 configuracionComboBox.Properties.TextField = "desc_departamento"
                                 configuracionComboBox.Properties.ValueType = GetType(String)
                                 configuracionComboBox.Properties.ClientSideEvents.SelectedIndexChanged =
                                     "function(s, e){fnc_cargar_municipios(s.GetValue());}"
                                 configuracionComboBox.Properties.ClearButton.DisplayMode = ClearButtonDisplayMode.OnHover
                             End Sub).BindList(objDatosReporte.fnc_obtener_departamento).GetHtml()
%>