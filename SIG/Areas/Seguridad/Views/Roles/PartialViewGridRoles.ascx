<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%Html.DevExpress.GridView(Sub(gdv)
                                 gdv.Name = "GdvRoles"
                                 gdv.Width = Unit.Percentage(100)

                                 gdv.CallbackRouteValues = New With {Key .Controller = "Roles", Key .Action = "PartialViewGridRoles"}
                                 gdv.SettingsSearchPanel.Visible = True
                                 gdv.SettingsSearchPanel.ShowApplyButton = True
                                 gdv.SettingsBehavior.AllowFixedGroups = True
                                 gdv.SettingsBehavior.AllowFocusedRow = True
                                 gdv.SettingsBehavior.AutoExpandAllGroups = True
                                 gdv.Settings.VerticalScrollBarMode = ScrollBarMode.Visible
                                 gdv.Settings.VerticalScrollableHeight = 600
                                 gdv.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords
                                 gdv.KeyFieldName = "id_rol"
                                 gdv.Columns.Add("desc_rol", "Rol")
                                 gdv.Columns.Add("nom_modulo", "Módulo").GroupIndex = 0
                                 gdv.Columns.Add("estado_rol", "Habilitado")
                                 gdv.Columns.Add(Sub(col)
                                                     col.SetDataItemTemplateContent(Sub(container)

                                                                                        Html.DevExpress.Button(Sub(hl)

                                                                                                                   hl.Name = String.Format("hlrol_{0}", container.VisibleIndex)
                                                                                                                   '  hl.NavigateUrl = "javascript:void(0)"
                                                                                                                   hl.ClientSideEvents.Click = String.Format("function(s, e) {{ GdvRolEditClick('{0}'); }}", container.KeyValue.ToString())
                                                                                                                   hl.RenderMode = ButtonRenderMode.Link
                                                                                                                   hl.Text = ""
                                                                                                                   hl.ControlStyle.CssClass = "fixStyles fa-pencil-square-o"
                                                                                                               End Sub).Render()

                                                                                    End Sub)
                                                 End Sub)
                             End Sub).Bind(Model).GetHtml()%>

