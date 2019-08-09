<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    Dim strCodigosEsq() As String = Split(ViewData("strCodEsquemaAdd"), ",")
    
    Html.DevExpress.GridView(Sub(configuracionGridView)
                                 configuracionGridView.Name = "GridViewDetalleEsquemaAcumulado"
                                 configuracionGridView.ControlStyle.CssClass = "AspxFormLayoutControlesPlanilla"
                                 configuracionGridView.Caption = "Detalle Esquemas Acumulados"
                                 configuracionGridView.CallbackRouteValues = New With {Key .Controller = "Parametros", _
                                                                                       Key .Action = "GridViewDetalleEsquemaAcumulado", _
                                                                                       Key .strCodEsquemaAdd = ViewData("strCodEsquemaAdd"),
                                                                                       Key .intCodEsqBuscar = ViewData("intCodEsqBuscar")}
                                 configuracionGridView.SettingsPager.PageSize = 1000
                                 configuracionGridView.KeyFieldName = "bonacnc_codigo"
                                 
                                 configuracionGridView.EnableCallbackAnimation = True
                                 configuracionGridView.EnablePagingCallbackAnimation = True
                                 configuracionGridView.EnableCallbackCompression = True
                                 configuracionGridView.CommandColumn.ShowSelectCheckbox = False
                                 configuracionGridView.CommandColumn.Visible = True
                                 
                                 configuracionGridView.Columns.Add("pag_codigo", "Código Pago")
                                 configuracionGridView.Columns.Add("pag_numero", "Número")
                                 configuracionGridView.Columns.Add("pag_anyo", "Año")
                                 configuracionGridView.Columns.Add("pag_nombre", "Pago")
                                 configuracionGridView.Columns.Add("pag_descripcion", "Descripción")
                                 configuracionGridView.Columns.Add("esq_codigo", "Código Esquema")
                                 configuracionGridView.Columns.Add("esq_tipo_pago", "Tipo Pago")
                                 configuracionGridView.Columns.Add("nombre_esquema", "Esquema")
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                       configuracionColumn.Caption = "Borrar"
                                                                       configuracionColumn.SetDataItemTemplateContent(Sub(configuracionItemContent)
                                                                                                                          configuracionItemContent.ViewStateMode = System.Web.UI.ViewStateMode.Enabled
                                                                                                                          Dim strCodEsquema As String = DataBinder.Eval(configuracionItemContent.DataItem, "esq_codigo")
                                                                                                                          Dim intCodBonAcumu As Integer = DataBinder.Eval(configuracionItemContent.DataItem, "bonacnc_codigo")
                                                                                                                          Dim strButton As String
                                                                                                                          Dim strCodEsqTemporal As String = ""
                                                                                                                          
                                                                                                                          
                                                                                                                          For i = 0 To strCodigosEsq.Length - 1
                                                                                                                              If strCodEsquema = strCodigosEsq(i) Then
                                                                                                                                  strCodEsqTemporal = strCodEsquema
                                                                                                                                  Exit For
                                                                                                                              End If
                                                                                                                          Next
                                                                                                                          
                                                                                                                          
                                                                                                                          If strCodEsqTemporal = strCodEsquema Then
                                                                                                                              strButton = String.Format("<button type='button' onclick='objTrasnferAcumu.BorrarTemporal({0})'>Temporal</button>", strCodEsquema)
                                                                                                                          Else
                                                                                                                              strButton = String.Format("<button type='button' onclick='objTrasnferAcumu.BorrarBD({0})'>Permanente</button>", intCodBonAcumu)
                                                                                                                          End If
                                                                                                                          
                                                                                                                          ViewContext.Writer.Write(strButton)
                                                                                                                      End Sub)
                                                                   End Sub)
                                 configuracionGridView.HtmlRowPrepared = Sub(sender, e)
                                                                             Dim strCodEsqTemporal As String = "00"
                                                                             
                                                                             For index = 0 To strCodigosEsq.Length - 1
                                                                                 If e.GetValue("esq_codigo") = strCodigosEsq(index) Then
                                                                                     strCodEsqTemporal = e.GetValue("esq_codigo")
                                                                                     Exit For
                                                                                 End If
                                                                             Next
                                                                             
                                                                             If e.GetValue("esq_codigo") = strCodEsqTemporal Then
                                                                                 e.Row.BackColor = System.Drawing.Color.FromArgb(187, 255, 153)
                                                                             Else
                                                                                 e.Row.BackColor = System.Drawing.Color.FromArgb(242, 242, 242)
                                                                             End If
                                                                         End Sub
                                 
                                 configuracionGridView.SettingsBehavior.AllowSort = False
                                 configuracionGridView.SettingsBehavior.AllowDragDrop = False
                                 
                                 configuracionGridView.Width = 1400
                             End Sub).Bind(Model).GetHtml()
%>