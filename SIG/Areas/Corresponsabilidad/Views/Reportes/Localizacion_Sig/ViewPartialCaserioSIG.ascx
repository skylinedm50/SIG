<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>


<%
    Html.DevExpress.ComboBox(Sub(configuracionComboBox As ComboBoxSettings)
                                 configuracionComboBox.Name = "AspxComboBoxCaserioSIG"
                                 configuracionComboBox.Properties.ClientInstanceName = "AspxComboBoxCaserioSIG"
                                 configuracionComboBox.Properties.DropDownStyle = DropDownStyle.DropDownList
                                 configuracionComboBox.Properties.IncrementalFilteringMode = IncrementalFilteringMode.StartsWith
                                 configuracionComboBox.Properties.ValueField = "cod_caserio"
                                 configuracionComboBox.Properties.TextField = "desc_caserio"
                                 configuracionComboBox.Properties.ValueType = GetType(String)
                                 configuracionComboBox.Properties.ClientSideEvents.SelectedIndexChanged =
                                     "function(s, e)" &
                                     "{fnc_cargar_centro_salud_por_caserio(s.GetValue());}"
                             End Sub).GetHtml()

%>