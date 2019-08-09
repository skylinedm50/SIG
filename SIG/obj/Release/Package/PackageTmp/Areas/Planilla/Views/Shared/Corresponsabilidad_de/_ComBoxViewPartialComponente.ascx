<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    Dim objShared As New SIG.SIG.Areas.Planilla.Models.cl_shared
    Html.DevExpress.ComboBox(Sub(configuracionComboBox)
                                 configuracionComboBox.Name = "AspxComboBoxComponente"
                                 configuracionComboBox.Properties.IncrementalFilteringMode = IncrementalFilteringMode.StartsWith
                                 configuracionComboBox.Properties.DropDownStyle = DropDownStyle.DropDownList
                                 configuracionComboBox.Properties.TextField = "comp_nombre"
                                 configuracionComboBox.Properties.ValueField = "comp_codigo"
                                 configuracionComboBox.Properties.ClientSideEvents.SelectedIndexChanged = "function(){ AspxComboBoxCorresponsabilidad.PerformCallback(); }"
                             End Sub).BindList(objShared.fnc_obtener_componente()).GetHtml()
%>