<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    Dim objEsquema As New SIG.SIG.Areas.Planilla.Models.cl_esquema
    Html.DevExpress.GridView(Sub(configuracionGridView)
                                 configuracionGridView.Name = "AspxGridViewConfMonto"
                                 configuracionGridView.CallbackRouteValues = New With {Key .Controller = "Parametros", Key .Action = "GridViewPartialConfiguracionMontos"}
                                 configuracionGridView.SettingsBehavior.AllowSelectByRowClick = False
                                 
                                 configuracionGridView.KeyFieldName = "confm_codigo"
                                 configuracionGridView.Columns.AddBand(Sub(configuracionColumn)
                                                                           configuracionColumn.Caption = "Configuración Montos"
                                                                           configuracionColumn.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                                                                           configuracionColumn.Columns.Add(Sub(configuracionSubColumn)
                                                                                                               configuracionSubColumn.FieldName = "confm_basico"
                                                                                                               configuracionSubColumn.Caption = "Básico"
                                                                                                               configuracionSubColumn.PropertiesEdit.DisplayFormatString = "c"
                                                                                                               configuracionSubColumn.Width = 70
                                                                                                           End Sub)
                                                                           configuracionColumn.Columns.AddBand(Sub(configuracionSubBandColumn)
                                                                                                                   configuracionSubBandColumn.Caption = "Salud"
                                                                                                                   configuracionSubBandColumn.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                                                                                                                   configuracionSubBandColumn.Columns.Add(Sub(configuracionSubColumn)
                                                                                                                                                              configuracionSubColumn.FieldName = "confm_nivel1_1"
                                                                                                                                                              configuracionSubColumn.Caption = "1 Niños(a)"
                                                                                                                                                              configuracionSubColumn.Settings.AllowSort = DefaultBoolean.False
                                                                                                                                                              configuracionSubColumn.PropertiesEdit.DisplayFormatString = "c"
                                                                                                                                                          End Sub)
                                                                                                                   configuracionSubBandColumn.Columns.Add(Sub(configuracionSubColumn)
                                                                                                                                                              configuracionSubColumn.FieldName = "confm_nivel1_2"
                                                                                                                                                              configuracionSubColumn.Caption = "2 Niños(a) o más"
                                                                                                                                                              configuracionSubColumn.Settings.AllowSort = DefaultBoolean.False
                                                                                                                                                              configuracionSubColumn.PropertiesEdit.DisplayFormatString = "c"
                                                                                                                                                          End Sub)
                                                                                                               End Sub)
                                                                           configuracionColumn.Columns.AddBand(Sub(configuracionSubBandColumn)
                                                                                                                   configuracionSubBandColumn.Caption = "1er y 2do Ciclo Educación"
                                                                                                                   configuracionSubBandColumn.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                                                                                                                   configuracionSubBandColumn.Columns.Add(Sub(configuracionSubColumn)
                                                                                                                                                              configuracionSubColumn.FieldName = "confm_nivel2_1"
                                                                                                                                                              configuracionSubColumn.Caption = "1 Niños(a)"
                                                                                                                                                              configuracionSubColumn.Settings.AllowSort = DefaultBoolean.False
                                                                                                                                                              configuracionSubColumn.PropertiesEdit.DisplayFormatString = "c"
                                                                                                                                                          End Sub)
                                                                                                                   configuracionSubBandColumn.Columns.Add(Sub(configuracionSubColumn)
                                                                                                                                                              configuracionSubColumn.FieldName = "confm_nivel2_2"
                                                                                                                                                              configuracionSubColumn.Caption = "2 Niños(a) o más"
                                                                                                                                                              configuracionSubColumn.Settings.AllowSort = DefaultBoolean.False
                                                                                                                                                              configuracionSubColumn.PropertiesEdit.DisplayFormatString = "c"
                                                                                                                                                          End Sub)
                                                                                                               End Sub)
                                                                           configuracionColumn.Columns.AddBand(Sub(configuracionSubBandColumn)
                                                                                                                   configuracionSubBandColumn.Caption = "3er Ciclo Educación"
                                                                                                                   configuracionSubBandColumn.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                                                                                                                   configuracionSubBandColumn.Columns.Add(Sub(configuracionSubColumn)
                                                                                                                                                              configuracionSubColumn.FieldName = "confm_nivel3_1"
                                                                                                                                                              configuracionSubColumn.Caption = "1 Niños(a)"
                                                                                                                                                              configuracionSubColumn.Settings.AllowSort = DefaultBoolean.False
                                                                                                                                                              configuracionSubColumn.PropertiesEdit.DisplayFormatString = "c"
                                                                                                                                                          End Sub)
                                                                                                                   configuracionSubBandColumn.Columns.Add(Sub(configuracionSubColumn)
                                                                                                                                                              configuracionSubColumn.FieldName = "confm_nivel3_2"
                                                                                                                                                              configuracionSubColumn.Caption = "2 Niños(a) o más"
                                                                                                                                                              configuracionSubColumn.Settings.AllowSort = DefaultBoolean.False
                                                                                                                                                              configuracionSubColumn.PropertiesEdit.DisplayFormatString = "c"
                                                                                                                                                          End Sub)
                                                                                                               End Sub)
                                                                           configuracionColumn.Columns.Add(Sub(configuracionSubBandColumn)
                                                                                                               If ViewData("intCodConfMon") IsNot Nothing Then
                                                                                                                   configuracionSubBandColumn.Caption = "Valor seleccionado"
                                                                                                               Else
                                                                                                                   configuracionSubBandColumn.Caption = "Seleccionar"
                                                                                                               End If
                                                                                                               configuracionSubBandColumn.FieldName = "confm_codigo"
                                                                                                               configuracionSubBandColumn.Settings.AllowSort = DefaultBoolean.False
                                                                                                               configuracionSubBandColumn.SetDataItemTemplateContent(Sub(configuracionItemContent)
                                                                                                                                                                         configuracionItemContent.ViewStateMode = System.Web.UI.ViewStateMode.Enabled
                                                                                                                                                                         Dim strCheck As String
                                                                                                                                                                         
                                                                                                                                                                         If ViewData("intCodConfMon") IsNot Nothing Then
                                                                                                                                                                             If CInt(ViewData("intCodConfMon")) = CInt(configuracionItemContent.Text) Then
                                                                                                                                                                                 strCheck = String.Format("<input type='radio' name='radioButtonConfMontoDetalle' id='radioButtonConfMontoDetalle' value={0} disabled='true' checked>", configuracionItemContent.Text)
                                                                                                                                                                             Else
                                                                                                                                                                                 strCheck = String.Format("<input type='radio' name='radioButtonConfMontoDetalle' id='radioButtonConfMontoDetalle' value={0} disabled='true' >", configuracionItemContent.Text)
                                                                                                                                                                             End If
                                                                                                                                                                         Else
                                                                                                                                                                             strCheck = String.Format("<input type='radio' name='radioButtonConfMonto' id='{0}' value={0}>", configuracionItemContent.Text)
                                                                                                                                                                         End If
                                                                                                                                                                         ViewContext.Writer.Write(strCheck)
                                                                                                                                                                     End Sub)
                                                                                                           End Sub)
                                                                       End Sub)
                             End Sub).Bind(objEsquema.fnc_obtener_montos).GetHtml()
%>