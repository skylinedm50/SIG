<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    Dim intCodTipFrom As Integer? = ViewData("intTipoForm")
    Dim arrStrTipoCarga() As String = {"Educación", "Salud"}
    Dim objTabla As New System.Data.DataTable

    objTabla.Columns.Add("cod_tip_com", GetType(Integer))
    objTabla.Columns.Add("nom_tip_com", GetType(String))

    For i = 1 To arrStrTipoCarga.Length
        objTabla.Rows.Add(i, arrStrTipoCarga(i - 1))
    Next

    If intCodTipFrom Is Nothing Then
        intCodTipFrom = 0
    End If

    Html.DevExpress.ComboBox(Sub(configuracionComboBox As ComboBoxSettings)
                                 configuracionComboBox.Name = "AspxComboBoxTipComponente"
                                 configuracionComboBox.Properties.ClientInstanceName = "AspxComboBoxTipComponente"
                                 configuracionComboBox.Properties.DropDownStyle = DropDownStyle.DropDownList
                                 configuracionComboBox.Properties.IncrementalFilteringMode = IncrementalFilteringMode.StartsWith
                                 configuracionComboBox.Properties.ValueField = "cod_tip_com"
                                 configuracionComboBox.Properties.TextField = "nom_tip_com"
                                 configuracionComboBox.Properties.ValueType = GetType(Integer)
                                 configuracionComboBox.Properties.ClientSideEvents.ValueChanged = "function(){ AspxComboBoxActulizacion.PerformCallback({intCodTipCom: AspxComboBoxTipComponente.GetValue(), intTipoForm: " & intCodTipFrom & " }); }"
                             End Sub).BindList(objTabla).Render()

%>