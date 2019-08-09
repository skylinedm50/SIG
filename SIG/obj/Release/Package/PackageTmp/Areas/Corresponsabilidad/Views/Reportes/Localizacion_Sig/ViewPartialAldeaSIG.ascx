<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>


<%
    Html.DevExpress.ComboBox(Sub(configuracionComboBox As ComboBoxSettings)
                                 configuracionComboBox.Name = "AspxComboBoxAldeaSIG"
                                 configuracionComboBox.Properties.ClientInstanceName = "AspxComboBoxAldeaSIG"
                                 configuracionComboBox.Properties.DropDownStyle = DropDownStyle.DropDownList
                                 configuracionComboBox.Properties.IncrementalFilteringMode = IncrementalFilteringMode.StartsWith
                                 configuracionComboBox.Properties.ValueField = "cod_aldea"
                                 configuracionComboBox.Properties.TextField = "desc_aldea"
                                 configuracionComboBox.Properties.ValueType = GetType(String)
                                 configuracionComboBox.Properties.ClientSideEvents.SelectedIndexChanged =
                                     "function(s, e)" &
                                     "{fnc_cargar_caserio(s.GetValue());}"
                             End Sub).GetHtml()
%>