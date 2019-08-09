<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>


<%
    Html.DevExpress.ComboBox(Sub(configuracionComboBox)
                                 configuracionComboBox.Name = "AspxComboBoxAldeaSACE"
                                 configuracionComboBox.Properties.ClientInstanceName = "AspxComboBoxAldeaSACE"
                                 configuracionComboBox.Properties.DropDownStyle = DropDownStyle.DropDownList
                                 configuracionComboBox.Properties.IncrementalFilteringMode = IncrementalFilteringMode.StartsWith
                                 configuracionComboBox.Properties.ValueField = "cod_sac_ald_sac"
                                 configuracionComboBox.Properties.TextField = "nom_ald_sac"
                                 configuracionComboBox.Properties.ValueType = GetType(Integer)
                                 configuracionComboBox.Properties.ClientSideEvents.SelectedIndexChanged =
                                     "function(s, e){" + _
                                        "fnc_cargar_caserio_sace(s.GetValue());" + _
                                     "}"
                             End Sub).BindList(Model).Render()
    
    
%>