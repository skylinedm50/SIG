<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%@ Import Namespace="modulo_corresponsabilidad" %>

<%
    Dim objDatosReporte As New SIG.SIG.Areas.Corresponsabilidad.Models.cl_data_reporte
     Html.DevExpress.ComboBox(Sub(configuracionComboBox)
                                 configuracionComboBox.Name = "AspxComboBoxDepartamentoSACE"
                                 configuracionComboBox.Properties.ClientInstanceName = "AspxComboBoxDepartamentoSACE"
                                 configuracionComboBox.Properties.DropDownStyle = DropDownStyle.DropDownList
                                 configuracionComboBox.Properties.IncrementalFilteringMode = IncrementalFilteringMode.StartsWith
                                 configuracionComboBox.Properties.ValueField = "cod_sac_dep_sac"
                                 configuracionComboBox.Properties.TextField = "nom_dep_sac"
                                 configuracionComboBox.Properties.ValueType = GetType(Integer)
                                 configuracionComboBox.Properties.ClientSideEvents.SelectedIndexChanged =
                                      "function(s, e){" + _
                                         "fnc_cargar_municipio_sace(s.GetValue());" + _
                                      "}"
                             End Sub).BindList(objDatosReporte.fnc_obtener_departamentos_sace()).GetHtml()
%>