<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    
    Dim arrListCodVerifica As ArrayList = ViewData("arrListCodVerificacion")
    
    Html.DevExpress.GridView(Sub(configuracionGridView)
                                 configuracionGridView.Name = "GridViewOtrasVerificaciones"
                                 configuracionGridView.CallbackRouteValues = New With {Key .Controller = "Parametros", Key .Action = "GridViewOtrasVerificaciones", _
                                                                                       Key .strCodEsquemas = ViewData("strCodEsquemas")}
                                 configuracionGridView.CommandColumn.Visible = False
                                 
                                 configuracionGridView.KeyFieldName = "ver_codigo"
                                 configuracionGridView.Caption = "Otras verificaciones"
		                            
                                 configuracionGridView.SettingsPager.Visible = False
                                 configuracionGridView.Settings.ShowGroupPanel = False
                                 configuracionGridView.Settings.ShowFilterRow = False
                                 
                                 configuracionGridView.SettingsBehavior.AllowSelectByRowClick = False
                                 
                                 configuracionGridView.SettingsBehavior.AllowSort = False
                                 configuracionGridView.SettingsBehavior.AllowDragDrop = False
                                 
                                 
                                 configuracionGridView.SettingsPager.PageSize = "2000"
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                       configuracionColumn.FieldName = "ver_nombre"
                                                                       configuracionColumn.Caption = "Nombre"
                                                                   End Sub)
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                       configuracionColumn.Caption = "Seleccionar"
                                                                       configuracionColumn.SetDataItemTemplateContent(Sub(configuracionItemContent)
                                                                                                                          configuracionItemContent.ViewStateMode = System.Web.UI.ViewStateMode.Enabled
                                                                                                                          Dim intCodVerifi As Integer = DataBinder.Eval(configuracionItemContent.DataItem, "ver_codigo")
                                                                                                                          Dim strCheck As String
                                                                                                                          Dim intNumero As Integer
                                                                                                                          Dim intCodVerSelect As Integer
                                                                                                                          
                                                                                                                          strCheck = String.Format("<input type='checkbox' name='checkboxOtraVeri' id='{0}' value='{1}' onchange='objOtraVeri.Verificacion.Agregar()'>", configuracionItemContent.ItemIndex, intCodVerifi)
                                                                                                                          
                                                                                                                          If arrListCodVerifica.Count > 0 Then
                                                                                                                              For Each intNumero In arrListCodVerifica
                                                                                                                                  If intCodVerifi = intNumero Then
                                                                                                                                      intCodVerSelect = intNumero
                                                                                                                                      Exit For
                                                                                                                                  End If
                                                                                                                              Next
                                                                                                                              If intCodVerSelect = intCodVerifi Then
                                                                                                                                  strCheck = String.Format("<input type='checkbox' name='checkboxOtraVeri' id='{0}' value='{1}' onchange='objOtraVeri.Verificacion.Agregar()' checked>", configuracionItemContent.ItemIndex, intCodVerifi)
                                                                                                                              End If
                                                                                                                          End If
                                                                                                                          
                                                                                                                          ViewContext.Writer.Write(strCheck)
                                                                                                                      End Sub)
                                                                   End Sub)
                                 configuracionGridView.Width = 650
                             End Sub).Bind(Model).GetHtml()
%>