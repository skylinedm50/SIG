<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%@ Import Namespace="modulo_corresponsabilidad" %>
<%
    Html.DevExpress.ComboBox(Sub(configuracionComboBox)
                                 configuracionComboBox.Name = "AspxComboBoxCentroSalud"
                                 configuracionComboBox.Properties.ClientInstanceName = "AspxComboBoxCentroSalud"
                                 configuracionComboBox.Properties.DropDownStyle = DropDownStyle.DropDownList
                                 configuracionComboBox.Properties.IncrementalFilteringMode = IncrementalFilteringMode.StartsWith
                                 configuracionComboBox.Properties.ValueField = "cod_cen_sal"
                                 configuracionComboBox.Properties.TextField = "nom_cen_sal"
                                 configuracionComboBox.Properties.ValueType = GetType(Integer)
                             End Sub).GetHtml()
        
%>