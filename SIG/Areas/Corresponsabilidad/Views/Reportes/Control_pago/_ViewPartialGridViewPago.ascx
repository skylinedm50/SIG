<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    Html.DevExpress.GridView(Sub(configuracionGridView As GridViewSettings)
                                 configuracionGridView.Name = "AspxGridViewPago"
                                 configuracionGridView.CallbackRouteValues = New With {Key .Controller = "Reportes",
                                                                                       Key .Action = "AspxGridViewPago",
                                                                                       Key .intAño = ViewData("intAño"),
                                                                                       Key .intCodTipBusq = ViewData("intCodTipBusq")}
                                 configuracionGridView.ClientSideEvents.BeginCallback = "function(s, e){ e.customArgs['intAño'] = ((AspxSpinEditAño.GetValue() == null)? 0: AspxSpinEditAño.GetValue()); e.customArgs['intCodTipBusq'] = AspxRadioButtonListTipBusq.GetValue(); }"
                                 configuracionGridView.Caption = "Pagos"
                                 configuracionGridView.Width = 500
                                 configuracionGridView.KeyFieldName = "pag_codigo"
                                 configuracionGridView.ControlStyle.CssClass = "GridViewCorres"
                                 configuracionGridView.Columns.Add("pag_codigo", "Código")
                                 configuracionGridView.Columns.Add("pag_nombre", "Nombre")
                                 configuracionGridView.Columns.Add("pag_anyo", "Año")
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                       configuracionColumn.Caption = "#"
                                                                       configuracionColumn.SetDataItemTemplateContent(Sub(configuracionItemContent)
                                                                                                                          configuracionItemContent.ViewStateMode = System.Web.UI.ViewStateMode.Enabled
                                                                                                                          Dim intCodPago As Integer = DataBinder.Eval(configuracionItemContent.DataItem, "pag_codigo")
                                                                                                                          Dim strCheckBox As String

                                                                                                                          strCheckBox = String.Format("<input type='checkbox' name='checkBoxCodPagos' id={0}  value='{0}' disabled=true checked>", intCodPago)

                                                                                                                          ViewContext.Writer.Write(strCheckBox)
                                                                                                                      End Sub)
                                                                   End Sub)
                                 configuracionGridView.SettingsBehavior.AllowDragDrop = False
                                 configuracionGridView.SettingsBehavior.AllowGroup = False
                                 configuracionGridView.SettingsBehavior.AllowSort = False
                             End Sub).Bind(Model).GetHtml()

%>