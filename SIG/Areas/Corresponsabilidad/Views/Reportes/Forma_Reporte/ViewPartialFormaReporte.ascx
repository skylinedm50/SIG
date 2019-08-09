<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>


<%
    Html.DevExpress.ComboBox(Sub(configuracionComboBox)
                                 configuracionComboBox.Name = "AspxComboBoxFormaReporte"
                                 configuracionComboBox.Properties.ClientInstanceName = "AspxComboBoxFormaReporte"
                                 configuracionComboBox.Properties.DropDownStyle = DropDownStyle.DropDownList
                                 configuracionComboBox.Properties.IncrementalFilteringMode = IncrementalFilteringMode.StartsWith
                                 configuracionComboBox.Properties.ValueType = GetType(Integer)
                                 configuracionComboBox.Properties.Items.Add("Tabla", 1)
                                 configuracionComboBox.Properties.Items.Add("Gráfico de barra", 2)
                                 configuracionComboBox.Properties.Items.Add("Gráfico lineal", 3)
                                 configuracionComboBox.Properties.Items.Add("Gráfico de area", 4)
                             End Sub).GetHtml()
%>