<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    Html.DevExpress.GridView(Sub(configuracionGridView)
                                 configuracionGridView.Name = "GridViewTransferencias"
                                 configuracionGridView.Caption = "Transferencias"
                                 configuracionGridView.CallbackRouteValues = New With {Key .Controller = "Shared", Key .Action = "GridViewTransferencias", Key .intCodEsquema = ViewData("intCodEsquema")}
                                 configuracionGridView.SettingsPager.PageSize = 100
                                 configuracionGridView.KeyFieldName = "bon_codigo"
                                 configuracionGridView.ClientSideEvents.BeginCallback = "function(s, e) { e.customArgs['intCodEsquema'] = ((cbxEsquemas.GetValue() !== null)? cbxEsquemas.GetValue(): 0); }"
                                 configuracionGridView.Width = 650
           
                                 configuracionGridView.EnableCallbackAnimation = True
                                 configuracionGridView.EnablePagingCallbackAnimation = True
                                 configuracionGridView.EnableCallbackCompression = True
                                 configuracionGridView.CommandColumn.ShowSelectCheckbox = False
                                 configuracionGridView.CommandColumn.Visible = False
                                 
                                 configuracionGridView.Columns.Add("bon_numero", "Número")
                                 configuracionGridView.Columns.Add("bon_detalle_meses", "Detalle")
                                 configuracionGridView.Columns.Add("bon_meses_cubrir", "Cantidad de Meses")
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                       configuracionColumn.Caption = "Agregar corresponsabilidad"
                                                                       configuracionColumn.SetDataItemTemplateContent(Sub(configuracionItemContent)
                                                                                                                          configuracionItemContent.ViewStateMode = System.Web.UI.ViewStateMode.Enabled
                                                                                                                          Dim intCodBono As Integer = DataBinder.Eval(configuracionItemContent.DataItem, "bon_codigo")
                                                                                                                          Dim strCheck As String
                                                                                                    
                                                                                                                          strCheck = String.Format("<input type='checkbox' name='checkBoxBono' id='{0}' value='{1}' onchange='objCorrespDe.Transferencia.Agregar()'>", configuracionItemContent.ItemIndex, intCodBono)
                                                                                                                          ViewContext.Writer.Write(strCheck)
                                                                                                                      End Sub)
                                                                   End Sub)
                                 
                                 configuracionGridView.SettingsBehavior.AllowSort = False
                                 configuracionGridView.SettingsBehavior.AllowFocusedRow = True
                                 configuracionGridView.SettingsBehavior.AllowDragDrop = True
                             End Sub).Bind(Model).GetHtml()
%>