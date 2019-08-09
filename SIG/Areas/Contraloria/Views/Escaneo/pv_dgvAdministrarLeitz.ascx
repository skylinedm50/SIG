<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Html.DevExpress().GridView(
                      Sub(settings)
                          settings.Name = "gvLeitz"
                          settings.KeyFieldName = "cod_leitz"
                          settings.Caption = "Administración de Leitz"
                          settings.CallbackRouteValues = New With {Key .Controller = "Escaneo", Key .Action = "pv_dgvAdministrarLeitz"}

                          settings.SettingsEditing.Mode = GridViewEditingMode.EditForm
                          settings.SettingsEditing.AddNewRowRouteValues = New With {Key .Controller = "Escaneo", Key .Action = "nuevoLeitz"}
                          settings.SettingsEditing.UpdateRowRouteValues = New With {Key .Controller = "Escaneo", Key .Action = "actualizarLeitz"}

                          settings.CommandColumn.Visible = True
                          settings.CommandColumn.ShowNewButton = True
                          settings.CommandColumn.ShowDeleteButton = False
                          settings.CommandColumn.ShowEditButton = True

                          settings.SettingsPager.Position = PagerPosition.Bottom
                          settings.SettingsPager.FirstPageButton.Visible = True
                          settings.SettingsPager.LastPageButton.Visible = True
                          settings.SettingsPager.PageSizeItemSettings.Visible = True
                          settings.SettingsPager.PageSizeItemSettings.Items = New String() {"10", "20", "50"}
                          settings.SettingsBehavior.AllowGroup = True
                          settings.SettingsBehavior.AllowSort = True
                          settings.Settings.ShowGroupPanel = True

                          settings.EnableCallbackAnimation = True
                          settings.EnablePagingCallbackAnimation = True
                          settings.EnableCallbackCompression = True

                          settings.Settings.ShowHeaderFilterButton = True
                          settings.SettingsPopup.HeaderFilter.Height = 200

                          settings.Columns.Add(
                                          Sub(col)
                                              col.FieldName = "cod_leitz"
                                              col.Visible = False

                                          End Sub)
                          settings.Columns.Add(
                                      Sub(col)
                                          col.FieldName = "cod_pago"
                                          col.Caption = "Pago"
                                          col.EditorProperties().ComboBox(
                                          Sub(cbx)
                                              cbx.TextField = "pag_nombre"
                                              cbx.ValueField = "pag_codigo"
                                              cbx.DataSource = ViewData("pagos")
                                          End Sub)
                                      End Sub)
                          settings.Columns.Add(
                                      Sub(col)
                                          col.FieldName = "cod_fondo"
                                          col.Caption = "Fondo"
                                          col.EditorProperties().ComboBox(
                                          Sub(cbx)

                                              cbx.TextField = "fond_nombre"
                                              cbx.ValueField = "fond_codigo"
                                              cbx.DataSource = ViewData("fondos")
                                          End Sub)
                                      End Sub)
                          settings.Columns.Add(
                          Sub(col)
                              col.FieldName = "numero_leitz"
                              col.Caption = "Número"
                              col.EditorProperties().SpinEdit(
                              Sub(spin)
                                  spin.NumberType = SpinEditNumberType.Integer
                                  spin.MinValue = 0
                                  spin.MaxValue = 10000
                              End Sub)
                          End Sub)
                          settings.Columns.Add(
                          Sub(col)
                              col.FieldName = "descripcion_leitz"
                              col.Caption = "Descripción"
                              col.EditFormSettings.ColumnSpan = 2
                              col.EditorProperties().Memo(
                              Sub(memo)
                                  memo.MaxLength = 255
                              End Sub)
                          End Sub)

                      End Sub).Bind(Model).Render()%>
