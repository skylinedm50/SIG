<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    Html.DevExpress.ComboBox(Sub(configuracionComboBox)
                                 configuracionComboBox.Name = "AspxComboBoxFondo"
                                 configuracionComboBox.Properties.IncrementalFilteringMode = IncrementalFilteringMode.StartsWith
                                 configuracionComboBox.Properties.DropDownStyle = DropDownStyle.DropDownList
                                 configuracionComboBox.Properties.TextField = "fond_nombre"
                                 configuracionComboBox.Properties.ValueField = "fond_codigo"
                                 configuracionComboBox.Width = 400
                             End Sub).BindList(Model).Render()
%>