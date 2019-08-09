<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%@ Import Namespace="modulo_corresponsabilidad" %>
<div id="DropDownEditCentroEducativo">

    <%       
        Html.DevExpress.ComboBox(Sub(configuracionComboBox)
                                     configuracionComboBox.Name = "AspxComboBoxCentroEducativoSACE"
                                     configuracionComboBox.Properties.ClientInstanceName = "AspxComboBoxCentroEducativoSACE"
                                     configuracionComboBox.Properties.DropDownStyle = DropDownStyle.DropDownList
                                     configuracionComboBox.Properties.IncrementalFilteringMode = IncrementalFilteringMode.StartsWith
                                     configuracionComboBox.Properties.ValueField = "cod_sac_cen_edu"
                                     configuracionComboBox.Properties.TextField = "nom_cen_edu"
                                     configuracionComboBox.Properties.ValueType = GetType(Integer)
                                 End Sub).BindList(Model).Render()
        
        %>
</div>