<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Html.DevExpress().GridLookup(Sub(gdlPagos)

                                     gdlPagos.Name = "gdlPagos"
                                     gdlPagos.KeyFieldName = "cod_pago"
                                     gdlPagos.CommandColumn.Visible = True
                                     gdlPagos.CommandColumn.ShowSelectCheckbox = True
                                     gdlPagos.Columns.Add("año_pago", "Año")
                                     gdlPagos.Columns.Add("numero_pago", "Nro.")
                                     gdlPagos.Columns.Add("descripcion_pago", "Descripción")
                                     gdlPagos.Properties.SelectionMode = GridLookupSelectionMode.Multiple
                                     gdlPagos.Properties.ClientSideEvents.ValueChanged = "gdlPagosValueChanged"
                                     gdlPagos.Properties.TextFormatString = "{0}-{1}"
                                     gdlPagos.Properties.MultiTextSeparator = ", "
                                     gdlPagos.Width = 900 'Unit.Percentage(100)
                                     gdlPagos.Properties.Width = Unit.Percentage(100)

                                     gdlPagos.GridViewProperties.CallbackRouteValues = New With {Key .Controller = "Shared", Key .Action = "pv_cbxGridPagos"}
                                     gdlPagos.GridViewProperties.Settings.ShowStatusBar = GridViewStatusBarMode.Visible
                                     gdlPagos.GridViewProperties.SettingsPager.PageSize = 5
                                     gdlPagos.GridViewProperties.Settings.ColumnMinWidth = 300
                                     gdlPagos.GridViewProperties.SetStatusBarTemplateContent(
                                         Sub()
                                             ViewContext.Writer.Write("<div style='padding: 2px 8px 2px 0; float: right'>")
                                             Html.DevExpress().Button(
                                                 Sub(btn)
                                                     btn.Name = "btnCerrar"
                                                     btn.UseSubmitBehavior = False
                                                     btn.Text = "Cerrar"
                                                     btn.ClientSideEvents.Click = "function (s, e) { gdlPagos.ConfirmCurrentSelection(); gdlPagos.HideDropDown();}"
                                                 End Sub).GetHtml()
                                             ViewContext.Writer.Write("</div>")
                                         End Sub)

                                     'no se que hace esto
                                     gdlPagos.DataBound = Sub(sender, e)
                                                              Dim gridLookup = DirectCast(sender, MVCxGridLookup)
                                                              gridLookup.GridView.Width = 900
                                                          End Sub

                                 End Sub).BindList(Model).GetHtml()%>
