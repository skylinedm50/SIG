<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
   Html.DevExpress.ComboBox(Sub(configuracionComboBox)
                                 configuracionComboBox.Name = "AspxComboBoxCaserioSACE"
                                 configuracionComboBox.Properties.ClientInstanceName = "AspxComboBoxCaserioSACE"
                                 configuracionComboBox.Properties.DropDownStyle = DropDownStyle.DropDownList
                                 configuracionComboBox.Properties.IncrementalFilteringMode = IncrementalFilteringMode.StartsWith
                                 configuracionComboBox.Properties.ValueField = "cod_sac_cas_sac"
                                 configuracionComboBox.Properties.TextField = "nom_cas_sac"
                                 configuracionComboBox.Properties.ValueType = GetType(Integer)
                                 configuracionComboBox.Properties.ClientSideEvents.SelectedIndexChanged = _
                                     "function(s, e)" + _
                                     "{fnc_cargar_centro_salud_por_caserio(s.GetValue());}"
                             End Sub).BindList(Model).Render()
%>