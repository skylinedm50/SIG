<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    Html.DevExpress.ComboBox(Sub(configuracionComboBox As ComboBoxSettings)
                                 configuracionComboBox.Name = "AspxComboBoxMunicipioSIG"
                                 configuracionComboBox.Properties.ClientInstanceName = "AspxComboBoxMunicipioSIG"
                                 configuracionComboBox.Properties.DropDownStyle = DropDownStyle.DropDownList
                                 configuracionComboBox.Properties.IncrementalFilteringMode = IncrementalFilteringMode.StartsWith
                                 configuracionComboBox.Properties.ValueField = "cod_municipio"
                                 configuracionComboBox.Properties.TextField = "desc_municipio"
                                 configuracionComboBox.Properties.ValueType = GetType(String)
                                 configuracionComboBox.Properties.ClientSideEvents.SelectedIndexChanged =
                                     "function(s, e)" &
                                     "{fnc_cargar_aldea(s.GetValue());}"
                             End Sub).GetHtml()
%>