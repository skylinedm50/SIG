<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    Html.DevExpress.ComboBox(Sub(configuracionComboBox)
                                 configuracionComboBox.Name = "AspxComboBoxCorresponsabilidad"
                                 configuracionComboBox.CallbackRouteValues = New With {.Controller = "Shared", .Action = "AspxComboBoxCorresponsabilidad", Key .intCodComponente = 0}
                                 configuracionComboBox.Properties.IncrementalFilteringMode = IncrementalFilteringMode.StartsWith
                                 configuracionComboBox.Properties.DropDownStyle = DropDownStyle.DropDownList
                                 configuracionComboBox.Properties.TextField = "corr_nombre"
                                 configuracionComboBox.Properties.ValueField = "corr_codigo"
                                 configuracionComboBox.Properties.ClientSideEvents.BeginCallback = "function(s, e){e.customArgs['intCodComponente'] = AspxComboBoxComponente.GetValue();}"
                             End Sub).BindList(Model).Render()
%>