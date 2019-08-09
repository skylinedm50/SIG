<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    Dim strCodDepaSelect As String = "00"
    Dim strCodDepaConHijos As String = "00"
    Dim strCaptionSelectAllOrNot() = {"Seleccionar <input type='checkbox' name='selectDeSelect' onchange='fnc_manejo_select_de_select_all(this)' >", "Seleccionar <input type='checkbox' name='selectDeSelect' onchange='fnc_manejo_select_de_select_all(this)' checked>"}
    Dim intSelectAllOrNot = ViewData("intSelectAllOrNot")
    
    Html.DevExpress.GridView(Sub(configuracionGridView)
                                 configuracionGridView.Name = "AspxGridViewDepartamentoSIG"
                                 configuracionGridView.CallbackRouteValues = New With {Key .Controller = "Shared", Key .Action = "GridViewPartialDepartamentoSIG", _
                                                                                       Key .strCodDepaSelect = ViewData("strCodDepaSelect"), _
                                                                                       Key .strCodDepaConHijos = ViewData("strCodDepaConHijos"), _
                                                                                       Key .strCodMuniSelectByDepa = ViewData("strCodMuniSelectByDepa"), _
                                                                                       Key .strCodMuniConHijos = ViewData("strCodMuniConHijos"), _
                                                                                       Key .intSelectAllOrNot = ViewData("intSelectAllOrNot")}
                                 configuracionGridView.CommandColumn.Visible = True
                                 
                                 configuracionGridView.KeyFieldName = "cod_departamento"
                                 configuracionGridView.Caption = "Ubicación Geográfica"
		                            
                                 configuracionGridView.SettingsPager.Visible = True
                                 configuracionGridView.Settings.ShowGroupPanel = False
                                 configuracionGridView.Settings.ShowFilterRow = False
                                 configuracionGridView.SettingsBehavior.AllowDragDrop = False
                                 configuracionGridView.SettingsBehavior.AllowGroup = False
                                 
                                 configuracionGridView.SettingsBehavior.AllowSelectByRowClick = False
                                 
                                 configuracionGridView.SettingsDetail.AllowOnlyOneMasterRowExpanded = True
                                 configuracionGridView.SettingsDetail.ShowDetailRow = True
                                 
                                 configuracionGridView.ClientSideEvents.BeginCallback = "function(s, e) { fnc_llamar_funciones_main(1, null); e.customArgs['strCodDepaSelect'] = objUbicaSIG.Departamento.Codigos.join(','); " & _
                                                                                        "e.customArgs['strCodDepaConHijos'] = objUbicaSIG.Departamento.ConHijos; e.customArgs['strCodMuniSelectByDepa'] = objUbicaSIG.Departamento.CodHijos; " & _
                                                                                        " e.customArgs['strCodMuniConHijos'] = objUbicaSIG.Municipio.ConHijos; " & _
                                                                                        " e.customArgs['intSelectAllOrNot'] = objUbicaSIG.SelectAllOrNot; }"
                                 
                                 configuracionGridView.ClientSideEvents.DetailRowExpanding = "function(s, e){ fnc_llamar_funciones_main(1, s.GetRowKey(e.visibleIndex)); }"
                                 configuracionGridView.ClientSideEvents.DetailRowCollapsing = "function(s, e){ fnc_llamar_funciones_main(1, s.GetRowKey(e.visibleIndex)); }"
                                 
                                 configuracionGridView.SettingsPager.PageSize = "20"
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                       configuracionColumn.FieldName = "cod_departamento"
                                                                       configuracionColumn.Caption = "Código"
                                                                   End Sub)
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                       configuracionColumn.FieldName = "desc_departamento"
                                                                       configuracionColumn.Caption = "Departamento"
                                                                   End Sub)
                                 configuracionGridView.SetDetailRowTemplateContent(Sub(configuracionDetailRow)
                                                                                       Dim strCodDepartamento As String = DataBinder.Eval(configuracionDetailRow.DataItem, "cod_departamento")
                                                                                       Dim bolEstaChecked As Boolean = False
                                                                                       
                                                                                       For index = 0 To ViewData("strCodDepaSelect").Length - 1
                                                                                           If strCodDepartamento = ViewData("strCodDepaSelect")(index) Then
                                                                                               bolEstaChecked = True
                                                                                           End If
                                                                                       Next
                                                                                       
                                                                                       Html.RenderAction("GridViewPartialMunicipioSIG", "Shared", New With {Key .strCodDepa = strCodDepartamento, _
                                                                                                                                                            Key .bolCheckedDepa = bolEstaChecked, _
                                                                                                                                                            Key .strCodMuniSelect = String.Join(",", ViewData("strCodMuniSelectByDepa")), _
                                                                                                                                                            Key .strCodMuniConHijos = ViewData("strCodMuniConHijos"), _
                                                                                                                                                            Key .strCodAldSelect = "000000", _
                                                                                                                                                            Key .strCodAldConHijos = "000000", _
                                                                                                                                                            Key .strCodCaseSelectByAld = "000000000"})
                                                                                   End Sub)
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                       configuracionColumn.Caption = strCaptionSelectAllOrNot(intSelectAllOrNot)
                                                                       configuracionColumn.SetDataItemTemplateContent(Sub(configuracionItemContent)
                                                                                                                          configuracionItemContent.ViewStateMode = System.Web.UI.ViewStateMode.Enabled
                                                                                                                          Dim strCodDepartamento As String = DataBinder.Eval(configuracionItemContent.DataItem, "cod_departamento")
                                                                                                                          Dim strCheck As String
                                                                                                                          
                                                                                                                          For index = 0 To ViewData("strCodDepaSelect").Length - 1
                                                                                                                              If strCodDepartamento = ViewData("strCodDepaSelect")(index) Then
                                                                                                                                  strCodDepaSelect = strCodDepartamento
                                                                                                                              End If
                                                                                                                          Next
                                                                                                                          
                                                                                                                          If strCodDepartamento = strCodDepaSelect Then
                                                                                                                              strCheck = String.Format("<input type='checkbox' name='checkboxDepartamento' id='{0}' value='{1}' onchange='fnc_cambio_checked(this, 1)' checked>", configuracionItemContent.ItemIndex, strCodDepartamento)
                                                                                                                          Else
                                                                                                                              strCheck = String.Format("<input type='checkbox' name='checkboxDepartamento' id='{0}' value='{1}' onchange='fnc_cambio_checked(this, 1)'>", configuracionItemContent.ItemIndex, strCodDepartamento)
                                                                                                                          End If
                                                                                                                          ViewContext.Writer.Write(strCheck)
                                                                                                                      End Sub)
                                                                   End Sub)
                                 configuracionGridView.HtmlRowPrepared = Sub(sender, e)
                                                                             For index = 0 To ViewData("strCodDepaSelect").Length - 1
                                                                                 If e.GetValue("cod_departamento") = ViewData("strCodDepaSelect")(index) Then
                                                                                     strCodDepaSelect = e.GetValue("cod_departamento")
                                                                                 End If
                                                                             Next
                                                                             
                                                                             For index = 0 To ViewData("strCodDepaConHijos").Length - 1
                                                                                 If e.GetValue("cod_departamento") = ViewData("strCodDepaConHijos")(index) Then
                                                                                     strCodDepaConHijos = e.GetValue("cod_departamento")
                                                                                 End If
                                                                             Next
                                                                             
                                                                             If e.GetValue("cod_departamento") = strCodDepaSelect Or e.GetValue("cod_departamento") = strCodDepaConHijos Then
                                                                                 e.Row.BackColor = System.Drawing.Color.FromArgb(242, 242, 242)
                                                                             End If
                                                                         End Sub
                             End Sub).Bind(Model).GetHtml()
%>