<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<div id="DropDownEditMunicipio">
<% 
    Html.DevExpress.ComboBox(Sub(configuracionComboBox)
                                 configuracionComboBox.Name = "AspxComboBoxMunicipioSACE"
                                 configuracionComboBox.Properties.ClientInstanceName = "AspxComboBoxMunicipioSACE"
                                 configuracionComboBox.Properties.DropDownStyle = DropDownStyle.DropDownList
                                 configuracionComboBox.Properties.IncrementalFilteringMode = IncrementalFilteringMode.StartsWith
                                 configuracionComboBox.Properties.ValueField = "cod_sac_mun_sac"
                                 configuracionComboBox.Properties.TextField = "nom_mun_sac"
                                 configuracionComboBox.Properties.ValueType = GetType(Integer)
                                 configuracionComboBox.Properties.ClientSideEvents.SelectedIndexChanged =
                                     "function(s, e){" + _
                                        "fnc_cargar_aldea_sace(s.GetValue());" + _
                                     "}"
                             End Sub).BindList(Model).GetHtml()
%>
</div>