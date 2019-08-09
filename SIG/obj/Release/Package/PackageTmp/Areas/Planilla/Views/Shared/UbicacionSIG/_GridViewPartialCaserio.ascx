<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    Dim bolCheckedAld As Boolean = ViewData("bolCheckedAld")
    Dim arrCodCaseSelect As Array = ViewData("strCodCaseSelect")
    Dim strCodCaseSelect As String = "000000000"
    
    
    Html.DevExpress.GridView(Sub(configuracionGridView)
                                 configuracionGridView.Name = "AspxGridViewCaserioSIG"
                                 configuracionGridView.CallbackRouteValues = New With {Key .Controller = "Shared", _
                                                                                       Key .Action = "GridViewPartialCaserioSIG", _
                                                                                       Key .strCodAld = ViewData("strCodAld"), _
                                                                                       Key .bolCheckedAld = bolCheckedAld, _
                                                                                       Key .strCodCaseSelect = String.Join(",", strCodCaseSelect)}
                                 configuracionGridView.CommandColumn.Visible = False
                                 
                                 configuracionGridView.KeyFieldName = "cod_caserio"
		
                                 configuracionGridView.SettingsPager.Visible = True
                                 configuracionGridView.Settings.ShowGroupPanel = False
                                 configuracionGridView.Settings.ShowFilterRow = False
                                 configuracionGridView.SettingsBehavior.AllowDragDrop = False
                                 configuracionGridView.SettingsBehavior.AllowGroup = False
                                 
                                 configuracionGridView.ClientSideEvents.BeginCallback = "function(s, e) { fnc_llamar_funciones_main(4, null); " &
                                                                                                         "e.customArgs['strCodAld'] = objUbicaSIG.Caserio.CodLastPadre; " &
                                                                                                         "e.customArgs['bolCheckedAld'] = objUbicaSIG.Caserio.LastPadreChecked; " &
                                                                                                         "e.customArgs['strCodCaseSelect'] = objUbicaSIG.Caserio.CodigosSelectLastPadre; " &
                                                                                                        "}"
                                 
                                 configuracionGridView.ClientSideEvents.DetailRowExpanding = "function(s, e){ fnc_llamar_funciones_main(4, s.GetRowKey(e.visibleIndex)); }"
                                 configuracionGridView.ClientSideEvents.DetailRowCollapsing = "function(s, e){ fnc_llamar_funciones_main(4, s.GetRowKey(e.visibleIndex)); }"
                                 
                                 configuracionGridView.SettingsPager.PageSize = "1000"
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                       configuracionColumn.FieldName = "cod_caserio"
                                                                       configuracionColumn.Caption = "Código"
                                                                   End Sub)
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                       configuracionColumn.FieldName = "desc_caserio"
                                                                       configuracionColumn.Caption = "Caserio"
                                                                   End Sub)
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                       configuracionColumn.Caption = "Seleccionar"
                                                                       configuracionColumn.SetDataItemTemplateContent(Sub(configuracionItemContent)
                                                                                                                          configuracionItemContent.ViewStateMode = System.Web.UI.ViewStateMode.Enabled
                                                                                                                          Dim strCodCaserio As String = DataBinder.Eval(configuracionItemContent.DataItem, "cod_caserio")
                                                                                                                          Dim strCheck As String
                                                                                                                          
                                                                                                                          
                                                                                                                          For index = 0 To arrCodCaseSelect.Length - 1
                                                                                                                              If strCodCaserio = arrCodCaseSelect(index) Then
                                                                                                                                  strCodCaseSelect = strCodCaserio
                                                                                                                              End If
                                                                                                                          Next
                                                                                                                          
                                                                                                                          If bolCheckedAld = True Or strCodCaseSelect = strCodCaserio Then
                                                                                                                              strCheck = String.Format("<input type='checkbox' name='checkboxCaserio' id='{0}' value='{1}' onchange='fnc_cambio_checked(this, 4)' checked>", configuracionItemContent.ItemIndex, strCodCaserio)
                                                                                                                          Else
                                                                                                                              strCheck = String.Format("<input type='checkbox' name='checkboxCaserio' id='{0}' value='{1}' onchange='fnc_cambio_checked(this, 4)'>", configuracionItemContent.ItemIndex, strCodCaserio)
                                                                                                                          End If
                                                                                                                          
                                                                                                                          ViewContext.Writer.Write(strCheck)
                                                                                                                      End Sub)
                                                                   End Sub)
                                 configuracionGridView.HtmlRowPrepared = Sub(sender, e)
                                                                             For index = 0 To arrCodCaseSelect.Length - 1
                                                                                 If e.GetValue("cod_caserio") = arrCodCaseSelect(index) Then
                                                                                     strCodCaseSelect = e.GetValue("cod_caserio")
                                                                                 End If
                                                                             Next
                                                                             
                                                                             If bolCheckedAld = True Or e.GetValue("cod_caserio") = strCodCaseSelect Then
                                                                                 e.Row.BackColor = System.Drawing.Color.FromArgb(242, 242, 242)
                                                                             End If
                                                                         End Sub
                                 
                             End Sub).Bind(Model).GetHtml()
%>