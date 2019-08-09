<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    Dim strCodMunicipioSelect As String = "0000"
    Dim strCodMuniConHijos As String = "0000"
    
    Dim bolCheckedCodMuni, bolCheckedCodDepa As Boolean
    Dim objArrCodMuniSelect, objArrCodMuniConHijos As Array
    objArrCodMuniSelect = ViewData("strCodMuniSelect")
    objArrCodMuniConHijos = ViewData("strCodMuniConHijos")
    bolCheckedCodDepa = ViewData("bolCheckedDepa")
    bolCheckedCodMuni = False
    
    Html.DevExpress.GridView(Sub(configuracionGridView)
                                 configuracionGridView.Name = "AspxGridViewMunicipioSIG"
                                 configuracionGridView.CallbackRouteValues = New With {Key .Controller = "Shared", _
                                                                                       Key .Action = "GridViewPartialMunicipioSIG", _
                                                                                       Key .strCodDepa = ViewData("strCodDepa"), _
                                                                                       Key .bolCheckedDepa = bolCheckedCodDepa, _
                                                                                       Key .strCodMuniSelect = String.Join(",", objArrCodMuniSelect), _
                                                                                       Key .strCodMuniConHijos = String.Join(",", objArrCodMuniConHijos), _
                                                                                       Key .strCodAldSelect = ViewData("strCodAldSelect"), _
                                                                                       Key .strCodAldConHijos = ViewData("strCodAldConHijos"), _
                                                                                       Key .strCodCaseSelectByAld = ViewData("strCodCaseSelectByAld")}
                                 configuracionGridView.CommandColumn.Visible = False
                                 
                                 configuracionGridView.KeyFieldName = "cod_municipio"
		
                                 configuracionGridView.SettingsPager.Visible = True
                                 configuracionGridView.Settings.ShowGroupPanel = False
                                 configuracionGridView.Settings.ShowFilterRow = False
                                 configuracionGridView.SettingsBehavior.AllowDragDrop = False
                                 configuracionGridView.SettingsBehavior.AllowGroup = False
                                 configuracionGridView.SettingsBehavior.AllowSelectByRowClick = False
                                 configuracionGridView.SettingsDetail.AllowOnlyOneMasterRowExpanded = True
                                 configuracionGridView.SettingsDetail.ShowDetailRow = True
                                 configuracionGridView.ClientSideEvents.BeginCallback = "function(s, e){fnc_llamar_funciones_main(2, null); " & _
                                                                                                         "e.customArgs['strCodDepa'] = objUbicaSIG.Municipio.CodLastPadre; " & _
                                                                                                         "e.customArgs['bolCheckedDepa'] = objUbicaSIG.Municipio.LastPadreChecked; " & _
                                                                                                         "e.customArgs['strCodMuniSelect'] = objUbicaSIG.Municipio.CodigosSelectLastPadre; " & _
                                                                                                         "e.customArgs['strCodMuniConHijos'] = objUbicaSIG.Municipio.ConHijos; " & _
                                                                                                         "e.customArgs['strCodAldSelect'] = objUbicaSIG.Aldea.CodigosSelectLastPadre; " &
                                                                                                         "e.customArgs['strCodAldConHijos'] = objUbicaSIG.Aldea.ConHijos; " &
                                                                                                         "e.customArgs['strCodCaseSelectByAld'] = objUbicaSIG.Aldea.CodHijos; " &
                                                                                                        "} "
                                 
                                 configuracionGridView.ClientSideEvents.DetailRowExpanding = "function(s, e){ fnc_llamar_funciones_main(2, s.GetRowKey(e.visibleIndex)); }"
                                 configuracionGridView.ClientSideEvents.DetailRowCollapsing = "function(s, e){ fnc_llamar_funciones_main(2, s.GetRowKey(e.visibleIndex)); }"
                                 
                                 configuracionGridView.SettingsPager.PageSize = "1000"
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                       configuracionColumn.FieldName = "cod_municipio"
                                                                       configuracionColumn.Caption = "Código"
                                                                   End Sub)
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                       configuracionColumn.FieldName = "desc_municipio"
                                                                       configuracionColumn.Caption = "Municipio"
                                                                   End Sub)
                                 configuracionGridView.SetDetailRowTemplateContent(Sub(configuracionDetailRow)
                                                                                       Dim strCodMuni As String = DataBinder.Eval(configuracionDetailRow.DataItem, "cod_municipio")
                                                                                       
                                                                                       For index = 0 To objArrCodMuniSelect.Length - 1
                                                                                           If strCodMuni = objArrCodMuniSelect(index) Then
                                                                                               bolCheckedCodMuni = True
                                                                                           End If
                                                                                       Next
                                                                                       
                                                                                       Html.RenderAction("GridViewPartialAldeaSIG", "Shared", New With {Key .strCodMuni = strCodMuni, _
                                                                                                                                                        Key .bolCheckedMuni = bolCheckedCodMuni, _
                                                                                                                                                        Key .strCodAldSelect = ViewData("strCodAldSelect"), _
                                                                                                                                                        Key .strCodAldeaConHijos = ViewData("strCodAldConHijos"), _
                                                                                                                                                        Key .strCodCaseSelectByAld = ViewData("strCodCaseSelectByAld")})
                                                                                   End Sub)
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                       configuracionColumn.Caption = "Seleccionar"
                                                                       configuracionColumn.SetDataItemTemplateContent(Sub(configuracionItemContent)
                                                                                                                          configuracionItemContent.ViewStateMode = System.Web.UI.ViewStateMode.Enabled
                                                                                                                          Dim strCodMunicipio As String = DataBinder.Eval(configuracionItemContent.DataItem, "cod_municipio")
                                                                                                                          Dim strCheck As String
                                                                                                                          
                                                                                                                          For index = 0 To objArrCodMuniSelect.Length - 1
                                                                                                                              If strCodMunicipio = objArrCodMuniSelect(index) Then
                                                                                                                                  strCodMunicipioSelect = strCodMunicipio
                                                                                                                              End If
                                                                                                                          Next
                                                                                                                          
                                                                                                                          If bolCheckedCodDepa = True Or strCodMunicipioSelect = strCodMunicipio Then
                                                                                                                              strCheck = String.Format("<input type='checkbox' name='checkboxMunicipio' id='{0}' value='{1}' onchange='fnc_cambio_checked(this, 2)' checked>", configuracionItemContent.ItemIndex, strCodMunicipio)
                                                                                                                          Else
                                                                                                                              strCheck = String.Format("<input type='checkbox' name='checkboxMunicipio' id='{0}' value='{1}' onchange='fnc_cambio_checked(this, 2)'>", configuracionItemContent.ItemIndex, strCodMunicipio)
                                                                                                                          End If
                                                                                                                          ViewContext.Writer.Write(strCheck)
                                                                                                                      End Sub)
                                                                   End Sub)
                                 configuracionGridView.HtmlRowPrepared = Sub(sender, e)
                                                                             For index = 0 To objArrCodMuniSelect.Length - 1
                                                                                 If e.GetValue("cod_municipio") = objArrCodMuniSelect(index) Then
                                                                                     strCodMunicipioSelect = e.GetValue("cod_municipio")
                                                                                 End If
                                                                             Next
                                                                             
                                                                             For index = 0 To objArrCodMuniConHijos.Length - 1
                                                                                 If e.GetValue("cod_municipio") = objArrCodMuniConHijos(index) Then
                                                                                     strCodMuniConHijos = e.GetValue("cod_municipio")
                                                                                 End If
                                                                             Next
                                                                             
                                                                             If bolCheckedCodDepa = True Or e.GetValue("cod_municipio") = strCodMunicipioSelect Or e.GetValue("cod_municipio") = strCodMuniConHijos Then
                                                                                 e.Row.BackColor = System.Drawing.Color.FromArgb(242, 242, 242)
                                                                             End If
                                                                         End Sub
                                 
                             End Sub).Bind(Model).GetHtml()
%>