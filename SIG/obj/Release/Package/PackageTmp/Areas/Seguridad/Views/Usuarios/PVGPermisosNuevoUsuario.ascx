<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%Html.DevExpress().GridView(Sub(settings)
                                   settings.Name = "gdvPermisos"
                                   settings.Width = Unit.Percentage(100)
                                   settings.CallbackRouteValues = New With {.Controller = "Usuarios", .Action = "PVGPermisosNuevoUsuario"}
                                   settings.CommandColumn.Visible = True
                                   settings.CommandColumn.ShowSelectCheckbox = True
                                   settings.SettingsBehavior.AllowFixedGroups = True
                                   settings.SettingsBehavior.AutoExpandAllGroups = True
                                   settings.Settings.VerticalScrollBarMode = ScrollBarMode.Visible
                                   settings.Settings.VerticalScrollableHeight = 500
                                   settings.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords
                                   settings.ClientSideEvents.SelectionChanged = "Prueba"
                                   settings.KeyFieldName = "id_rol"
                                   settings.Columns.Add(Sub(col)
                                                            col.FieldName = "modulo"
                                                            col.Caption = "Modulo"
                                                            col.GroupIndex = 0
                                                        End Sub)
                                   settings.Columns.Add("desc_rol", "Seleccione el Rol(es) del Usuario").Width = Unit.Percentage(90)
                               End Sub).Bind(Model).GetHtml()%>