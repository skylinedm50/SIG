<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    Html.DevExpress.GridView(
       Sub(configuracionGridView)
           configuracionGridView.Name = "GridViewEsquemasPorPago"
           configuracionGridView.Caption = "Esquemas"
           configuracionGridView.CallbackRouteValues = New With {Key .Controller = "Shared", Key .Action = "GridViewEsquemasPorPago"}
           configuracionGridView.SettingsPager.PageSize = 100
           configuracionGridView.KeyFieldName = "esq_codigo"
           configuracionGridView.ClientSideEvents.BeginCallback = "function(s, e) { e.customArgs['pago'] = cbxPagos.GetValue(); }"
           configuracionGridView.Width = 650
           
           configuracionGridView.EnableCallbackAnimation = True
           configuracionGridView.EnablePagingCallbackAnimation = True
           configuracionGridView.EnableCallbackCompression = True
           
           configuracionGridView.Columns.Add("nombre_esquema", "Nombre Esquema")
           configuracionGridView.Columns.Add("esq_tipo_pago", "Tipo Pago")
           configuracionGridView.Columns.Add("esq_detalle_meses", "Detalle Meses")
           configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                 configuracionColumn.Caption = "Seleccionar"
                                                 configuracionColumn.SetDataItemTemplateContent(Sub(configuracionItemContent)
                                                                                                    configuracionItemContent.ViewStateMode = System.Web.UI.ViewStateMode.Enabled
                                                                                                    Dim strNombreEsquema As String = DataBinder.Eval(configuracionItemContent.DataItem, "nombre_esquema")
                                                                                                    Dim intCodEsquema As Integer = DataBinder.Eval(configuracionItemContent.DataItem, "esq_codigo")
                                                                                                    Dim strCheck As String
                                                                                                    
                                                                                                    strCheck = String.Format("<input type='checkbox' name='checkBoxEsqueByPago' id='{0}' value='{1}' onchange='objManejoEsquema.Esquemas.SelectEsquemaByBuscar(this)'>", intCodEsquema, strNombreEsquema)
                                                                                                    ViewContext.Writer.Write(strCheck)
                                                                                                End Sub)
                                             End Sub)
       End Sub).Bind(Model).GetHtml()
%>