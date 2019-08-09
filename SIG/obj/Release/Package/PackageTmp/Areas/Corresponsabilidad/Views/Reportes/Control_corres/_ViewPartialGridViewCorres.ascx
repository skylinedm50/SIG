<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    Dim intTipBusq As Integer = CInt(ViewData("intCodTipBusq"))
    Html.DevExpress.GridView(Sub(configuracionGridView As GridViewSettings)
                                 configuracionGridView.Name = "AspxGridViewCorres"
                                 configuracionGridView.CallbackRouteValues = New With {Key .Controller = "Reportes",
                                                                                       Key .Action = "AspxGridViewCorres",
                                                                                       Key .intAño = ViewData("intAño"),
                                                                                       Key .intCodTipBusq = ViewData("intCodTipBusq")}
                                 configuracionGridView.ClientSideEvents.BeginCallback = "function(s, e){ e.customArgs['intAño'] = ((AspxSpinEditAño.GetValue() == null)? 0: AspxSpinEditAño.GetValue()); " &
                                                                       " e.customArgs['intCodTipBusq'] = AspxRadioButtonListTipBusq.GetValue(); }"

                                 configuracionGridView.Caption = "Corresponsabilidades"
                                 configuracionGridView.Width = 500
                                 configuracionGridView.KeyFieldName = "corr_codigo"

                                 configuracionGridView.ControlStyle.CssClass = "GridViewCorres"
                                 configuracionGridView.Columns.Add("corr_codigo", "Código")
                                 configuracionGridView.Columns.Add("comp_nombre", "Componente")
                                 configuracionGridView.Columns.Add("pag_nombre", "Pago")
                                 configuracionGridView.Columns.Add("corr_nombre", "Nombre")
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                       configuracionColumn.Caption = "#"
                                                                       configuracionColumn.SetDataItemTemplateContent(Sub(configuracionItemContent)
                                                                                                                          configuracionItemContent.ViewStateMode = System.Web.UI.ViewStateMode.Enabled
                                                                                                                          Dim intCodCorres As Integer = DataBinder.Eval(configuracionItemContent.DataItem, "corr_codigo")
                                                                                                                          Dim strCheckBox As String
                                                                                                                          If intTipBusq = 1 Then
                                                                                                                              strCheckBox = String.Format("<input type='checkbox' name='checkBoxCodCorres' id={0}  value='{0}' checked>", intCodCorres)
                                                                                                                          Else
                                                                                                                              strCheckBox = String.Format("<input type='checkbox' name='checkBoxCodCorres' id={0}  value='{0}' disabled=true checked>", intCodCorres)
                                                                                                                          End If
                                                                                                                          ViewContext.Writer.Write(strCheckBox)
                                                                                                                      End Sub)
                                                                   End Sub)

                                 configuracionGridView.SettingsBehavior.AllowDragDrop = False
                                 configuracionGridView.SettingsBehavior.AllowGroup = False
                                 configuracionGridView.SettingsBehavior.AllowSort = False
                             End Sub).Bind(Model).GetHtml()

%>