<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    Dim bolCheckedMuni, bolCheckedAld As Boolean
    Dim strCodAldSelect As String = "000000"
    Dim strCodAldConHijos As String = "000000"
    Dim arrCodAldSelect, arrCodAldConHijos As Array
    arrCodAldSelect = ViewData("strCodAldSelect")
    arrCodAldConHijos = ViewData("strCodAldeaConHijos")
    bolCheckedMuni = ViewData("bolCheckedMuni")
    bolCheckedAld = False
    
    Html.DevExpress.GridView(Sub(configuracionGridView)
                                 configuracionGridView.Name = "AspxGridViewAldeaSIG"
                                 configuracionGridView.CallbackRouteValues = New With {Key .Controller = "Shared", _
                                                                                       Key .Action = "GridViewPartialAldeaSIG", _
                                                                                       Key .strCodMuni = ViewData("strCodMuni"), _
                                                                                       Key .bolCheckedMuni = ViewData("bolCheckedMuni"), _
                                                                                       Key .strCodAldSelect = String.Join(",", arrCodAldSelect), _
                                                                                       Key .strCodAldeaConHijos = String.Join(",", arrCodAldConHijos), _
                                                                                       Key .strCodCaseSelectByAld = ViewData("strCodCaseSelectByAld")}
                                 configuracionGridView.CommandColumn.Visible = False
                                 
                                 configuracionGridView.KeyFieldName = "cod_aldea"
		
                                 configuracionGridView.SettingsPager.Visible = True
                                 configuracionGridView.Settings.ShowGroupPanel = False
                                 configuracionGridView.Settings.ShowFilterRow = False
                                 configuracionGridView.SettingsBehavior.AllowDragDrop = False
                                 configuracionGridView.SettingsBehavior.AllowGroup = False
                                 configuracionGridView.SettingsBehavior.AllowSelectByRowClick = True
                                 configuracionGridView.SettingsDetail.AllowOnlyOneMasterRowExpanded = True
                                 configuracionGridView.SettingsDetail.ShowDetailRow = True
                                 configuracionGridView.ClientSideEvents.BeginCallback = "function(s, e) { fnc_llamar_funciones_main(3, null); " &
                                                                                                        "e.customArgs['strCodMuni'] = objUbicaSIG.Aldea.CodLastPadre;  " &
                                                                                                        "e.customArgs['bolCheckedMuni'] = objUbicaSIG.Aldea.LastPadreChecked;  " &
                                                                                                        "e.customArgs['strCodAldSelect'] = objUbicaSIG.Aldea.CodigosSelectLastPadre;  " &
                                                                                                        "e.customArgs['strCodAldeaConHijos'] = objUbicaSIG.Aldea.ConHijos; " &
                                                                                                        "e.customArgs['strCodCaseSelectByAld'] = objUbicaSIG.Aldea.CodHijos; " &
                                                                                                       "}"
                                 
                                 configuracionGridView.ClientSideEvents.DetailRowExpanding = "function(s, e){ fnc_llamar_funciones_main(3, s.GetRowKey(e.visibleIndex)); }"
                                 configuracionGridView.ClientSideEvents.DetailRowCollapsing = "function(s, e){ fnc_llamar_funciones_main(3, s.GetRowKey(e.visibleIndex)); }"
                                 
                                 configuracionGridView.SettingsPager.PageSize = "1000"
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                       configuracionColumn.FieldName = "cod_aldea"
                                                                       configuracionColumn.Caption = "Código"
                                                                   End Sub)
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                       configuracionColumn.FieldName = "desc_aldea"
                                                                       configuracionColumn.Caption = "Aldea"
                                                                   End Sub)
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                       configuracionColumn.Caption = "Seleccionar"
                                                                       configuracionColumn.SetDataItemTemplateContent(Sub(configuracionItemContent)
                                                                                                                          configuracionItemContent.ViewStateMode = System.Web.UI.ViewStateMode.Enabled
                                                                                                                          Dim strCodAldea As String = DataBinder.Eval(configuracionItemContent.DataItem, "cod_aldea")
                                                                                                                          Dim strCheck As String
                                                                                                                          
                                                                                                                          For index = 0 To arrCodAldSelect.Length - 1
                                                                                                                              If strCodAldea = arrCodAldSelect(index) Then
                                                                                                                                  strCodAldSelect = strCodAldea
                                                                                                                              End If
                                                                                                                          Next
                                                                                                                          
                                                                                                                          If bolCheckedMuni = True Or strCodAldSelect = strCodAldea Then
                                                                                                                              strCheck = String.Format("<input type='checkbox' name='checkboxAldea' id='{0}' value='{1}' onchange='fnc_cambio_checked(this, 3)' checked>", configuracionItemContent.ItemIndex, strCodAldea)
                                                                                                                          Else
                                                                                                                              strCheck = String.Format("<input type='checkbox' name='checkboxAldea' id='{0}' value='{1}' onchange='fnc_cambio_checked(this, 3)'>", configuracionItemContent.ItemIndex, strCodAldea)
                                                                                                                          End If
                                                                                                                          
                                                                                                                          ViewContext.Writer.Write(strCheck)
                                                                                                                      End Sub)
                                                                   End Sub)
                                 configuracionGridView.SetDetailRowTemplateContent(Sub(configuracionDetailRow)
                                                                                       Dim strCodAld As String = DataBinder.Eval(configuracionDetailRow.DataItem, "cod_aldea")
                                                                                       
                                                                                       For index = 0 To arrCodAldSelect.Length - 1
                                                                                           If strCodAld = arrCodAldSelect(index) Then
                                                                                               bolCheckedAld = True
                                                                                           End If
                                                                                       Next
                                                                                       
                                                                                       Html.RenderAction("GridViewPartialCaserioSIG", "Shared", New With {Key .strCodAld = strCodAld, _
                                                                                                                                                          Key .bolCheckedAld = bolCheckedAld, _
                                                                                                                                                          Key .strCodCaseSelect = ViewData("strCodCaseSelectByAld")})
                                                                                   End Sub)
                                 configuracionGridView.HtmlRowPrepared = Sub(sender, e)
                                                                             For index = 0 To arrCodAldSelect.Length - 1
                                                                                 If e.GetValue("cod_aldea") = arrCodAldSelect(index) Then
                                                                                     strCodAldSelect = e.GetValue("cod_aldea")
                                                                                 End If
                                                                             Next
                                                                             
                                                                             For index = 0 To arrCodAldConHijos.Length - 1
                                                                                 If e.GetValue("cod_aldea") = arrCodAldConHijos(index) Then
                                                                                     strCodAldConHijos = e.GetValue("cod_aldea")
                                                                                 End If
                                                                             Next
                                                                             
                                                                             If bolCheckedMuni = True Or e.GetValue("cod_aldea") = strCodAldSelect Or e.GetValue("cod_aldea") = strCodAldConHijos Then
                                                                                 e.Row.BackColor = System.Drawing.Color.FromArgb(242, 242, 242)
                                                                             End If
                                                                         End Sub
                                 
                             End Sub).Bind(Model).GetHtml()
%>