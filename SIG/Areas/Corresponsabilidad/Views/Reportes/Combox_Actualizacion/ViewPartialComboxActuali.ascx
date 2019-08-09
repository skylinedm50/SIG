<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    Html.DevExpress.ComboBox(Sub(configuracionComboBox As ComboBoxSettings)
                                 configuracionComboBox.Name = "AspxComboBoxActulizacion"
                                 configuracionComboBox.CallbackRouteValues = New With {Key .Controller = "Reportes",
                                                                                        Key .Action = "AspxComboBoxActulizacion",
                                                                                        Key .intCodTipCarga = ViewData("intCodTipCom"),
                                                                                        Key .intTipoForm = ViewData("intTipoForm")}
                                 configuracionComboBox.Properties.ClientInstanceName = "AspxComboBoxActulizacion"
                                 configuracionComboBox.Properties.DropDownStyle = DropDownStyle.DropDownList
                                 configuracionComboBox.Properties.IncrementalFilteringMode = IncrementalFilteringMode.StartsWith
                                 configuracionComboBox.Properties.ValueField = "num_car_log_car"
                                 configuracionComboBox.Properties.TextField = "actuali"
                                 configuracionComboBox.Properties.ValueType = GetType(Integer)
                             End Sub).BindList(Model).Render()

%>