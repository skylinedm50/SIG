<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%Html.DevExpress.GridView(Sub(configuracionGridView)
                               configuracionGridView.Name = "GridViewPagoAcumulado"
                               configuracionGridView.ControlStyle.CssClass = "AspxFormLayoutControlesPlanilla"
                               configuracionGridView.Caption = "Pago a Acumular"
                               configuracionGridView.CallbackRouteValues = New With {Key .Controller = "Parametros", _
                                                                                     Key .Action = "GridViewPagoAcumulado", _
                                                                                     Key .intCodPago = ViewData("intCodPago")}
                               configuracionGridView.SettingsPager.PageSize = 1000
                               configuracionGridView.KeyFieldName = "pag_codigo"
                               configuracionGridView.EnableCallbackAnimation = True
                               configuracionGridView.EnablePagingCallbackAnimation = True
                               configuracionGridView.EnableCallbackCompression = True
                               configuracionGridView.CommandColumn.ShowSelectCheckbox = False
                               configuracionGridView.CommandColumn.Visible = False
                               configuracionGridView.ClientSideEvents.FocusedRowChanged = "function(){ objTrasnferAcumu.BuscarEsquemaAcumu(); }"
                                 
                               configuracionGridView.Columns.Add("pag_codigo", "Código").Width = 50
                               configuracionGridView.Columns.Add("pag_numero", "Número").Width = 50
                               configuracionGridView.Columns.Add("pag_anyo", "Año").Width = 50
                               configuracionGridView.Columns.Add("pag_nombre", "Nombre")
                               configuracionGridView.Columns.Add("pag_descripcion", "Descripción")
                                 
                               configuracionGridView.SettingsBehavior.AllowSort = False
                               configuracionGridView.SettingsBehavior.AllowSelectByRowClick = True
                               configuracionGridView.SettingsBehavior.AllowFocusedRow = True
                               configuracionGridView.SettingsBehavior.AllowDragDrop = False
                               configuracionGridView.Width = 900
                           End Sub).Bind(Model).GetHtml()
%>