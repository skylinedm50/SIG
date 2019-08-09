<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%Html.DevExpress.GridView(Sub(configuracionGridView)
                               configuracionGridView.Name = "GridViewEsquemaAcumulado"
                               configuracionGridView.ControlStyle.CssClass = "AspxFormLayoutControlesPlanilla"
                               configuracionGridView.Caption = "Esquema a Acumular"
                               configuracionGridView.CallbackRouteValues = New With {Key .Controller = "Parametros", _
                                                                                     Key .Action = "GridViewEsquemaAcumulado", _
                                                                                     Key .intCodPago = ViewData("intCodPago")}
                               configuracionGridView.SettingsPager.PageSize = 1000
                               configuracionGridView.KeyFieldName = "esq_codigo"
                               configuracionGridView.EnableCallbackAnimation = True
                               configuracionGridView.EnablePagingCallbackAnimation = True
                               configuracionGridView.EnableCallbackCompression = True
                               configuracionGridView.CommandColumn.ShowSelectCheckbox = False
                               configuracionGridView.CommandColumn.Visible = False
                                 
                               configuracionGridView.Columns.Add("esq_codigo", "Código").Width = 50
                               configuracionGridView.Columns.Add("esq_tipo_pago", "Tipo Pago").Width = 100
                               configuracionGridView.Columns.Add("nombre_esquema", "Nombre")
                               configuracionGridView.Columns.Add("esq_detalle_meses", "Detalle Meses")
                                 
                               configuracionGridView.SettingsBehavior.AllowSort = False
                               configuracionGridView.SettingsBehavior.AllowSelectByRowClick = True
                               configuracionGridView.SettingsBehavior.AllowFocusedRow = True
                               configuracionGridView.Width = 900
                           End Sub).Bind(Model).GetHtml()
%>