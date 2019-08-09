<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    Dim intTipoCorres As Integer = CInt(ViewData("intTipoCorres"))
    Dim intFocusIndice As Integer = CInt(ViewData("intFocusIndice"))
    Dim strNameKey() As String = {"bonc_codigo", "bonca_codigo"}
    Dim strNameCaption() As String = {"Detalle Orden Corresponsabilidad Por Transferencia", "Detalle Orden Corresponsabilidad Por Apercibimiento"}
    
    Html.DevExpress.GridView(Sub(configuracionGridView)
                                 configuracionGridView.Name = "GridViewOrdenCorresTransfer"
                                 configuracionGridView.ControlStyle.CssClass = "AspxFormLayoutControlesPlanilla"
                                 configuracionGridView.Caption = strNameCaption(intTipoCorres)
                                 configuracionGridView.CallbackRouteValues = New With {Key .Controller = "Shared", _
                                                                                       Key .Action = "GridViewOrdenCorresTransfer", _
                                                                                       Key .intTipoCorres = ViewData("intTipoCorres"), _
                                                                                       Key .intCodTransfer = ViewData("intCodTransfer"), _
                                                                                       Key .intFocusIndice = ViewData("intFocusIndice"), _
                                                                                       Key .intCodKeyGrid = ViewData("intCodKeyGrid")}
                                 configuracionGridView.SettingsPager.PageSize = 1000
                                 configuracionGridView.KeyFieldName = "bon_codigo_key"
                                 
                                 configuracionGridView.ClientSideEvents.BeginCallback = "function(s, e) { e.customArgs['intTipoCorres'] = objCorrespDe.TipoCorres; e.customArgs['intCodTransfer'] = objCorrespDe.CodTransferBuscar; }"
                                 configuracionGridView.CommandColumn.CustomButtons.Add(New GridViewCommandColumnCustomButton() With {.ID = "btnDelete", .Text = "Borrar"})
                                 configuracionGridView.ClientSideEvents.CustomButtonClick = "function(s,e){ objCorrespDe.Borrar(s.GetRowKey(e.visibleIndex)); }"
                                 configuracionGridView.EnableCallbackAnimation = True
                                 configuracionGridView.EnablePagingCallbackAnimation = True
                                 configuracionGridView.EnableCallbackCompression = True
                                 configuracionGridView.CommandColumn.ShowSelectCheckbox = False
                                 configuracionGridView.CommandColumn.Visible = True
                                 
                                 configuracionGridView.Columns.Add("bon_codigo_key", "Código")
                                 configuracionGridView.Columns.Add("comp_nombre", "Componente")
                                 configuracionGridView.Columns.Add("corr_nombre", "Corresponsabilidad")
                                 configuracionGridView.Columns.Add("order", "Orden").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
                                 
                                 configuracionGridView.SettingsBehavior.AllowSort = False
                                 configuracionGridView.SettingsBehavior.AllowSelectByRowClick = True
                                 configuracionGridView.SettingsBehavior.AllowFocusedRow = True
                                 configuracionGridView.BeforeGetCallbackResult = Sub(s, e)
                                                                                     Dim objGridView As MVCxGridView = s
                                                                                     If intFocusIndice > 0 Then
                                                                                         objGridView.FocusedRowIndex = intFocusIndice - 1
                                                                                         objGridView.DataBind()
                                                                                     End If
                                                                                 End Sub
                             End Sub).Bind(Model).GetHtml()
%>