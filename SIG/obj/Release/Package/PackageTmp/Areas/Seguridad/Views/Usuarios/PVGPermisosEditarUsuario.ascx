<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%Html.DevExpress().GridView(Sub(settings)
                                   settings.Name = "gdvPermisosEdit"

                                   settings.Width = Unit.Percentage(100)
                                   settings.CallbackRouteValues = New With {Key .Controller = "Usuarios", Key .Action = "PVGPermisosEditarUsuario", Key .cod_usuario = ViewData("cod_usuario")}
                                   '  settings.SettingsBehavior.AllowFocusedRow = True
                                   ' settings.SettingsEditing.Mode = GridViewEditingMode.EditForm
                                   settings.Settings.VerticalScrollBarMode = ScrollBarMode.Visible
                                   settings.Settings.VerticalScrollableHeight = 400
                                   settings.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords
                                   settings.SettingsBehavior.AutoExpandAllGroups = True
                                   settings.SettingsBehavior.AllowFixedGroups = True
                                   '  settings.ClientSideEvents.SelectionChanged = "Prueba"
                                   settings.KeyFieldName = "id_rol"

                                   settings.Columns.Add(Sub(col)
                                                            col.FieldName = "habilitado"
                                                            col.Caption = "Habilitado"
                                                            col.Width = Unit.Percentage(15)

                                                            col.SetDataItemTemplateContent(Sub(c)
                                                                                               Html.DevExpress.CheckBox(Sub(chk)
                                                                                                                            chk.Name = "cb" + c.KeyValue.ToString()
                                                                                                                            chk.Checked = Convert.ToBoolean(DataBinder.Eval(c.DataItem, "habilitado"))
                                                                                                                            ' chk.ClientEnabled = (Convert.ToBoolean(DataBinder.Eval(c.DataItem, "habilitado")) <> True)
                                                                                                                            chk.Properties.ClientSideEvents.CheckedChanged = String.Format("function(s, e) {{ chkHabilitadoEditChange('{0}','{1}'); }}", c.KeyValue.ToString(), Convert.ToBoolean(DataBinder.Eval(c.DataItem, "habilitado")))

                                                                                                                        End Sub).GetHtml()
                                                                                           End Sub)
                                                        End Sub)

                                   settings.Columns.Add("desc_rol", "Rol").Width = Unit.Percentage(85)
                                   settings.Columns.Add(Sub(col)
                                                            col.FieldName = "modulo"
                                                            col.Caption = "Modulo"
                                                            col.Width = Unit.Percentage(0)
                                                            col.GroupIndex = 0
                                                        End Sub)

                               End Sub).Bind(Model).GetHtml()%>
