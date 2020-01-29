<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>
<%Html.DevExpress.GridView(Sub(gdv)
                                 gdv.Name = "GdvRoles"
                                 gdv.Width = Unit.Percentage(100)

                                 gdv.KeyFieldName = "id_rol"
                                 gdv.CallbackRouteValues = New With {Key .Controller = "Accesos", Key .Action = "PartialViewGridRolesAccesos"}
                                 gdv.SettingsBehavior.AllowFixedGroups = True
                                 gdv.Settings.VerticalScrollBarMode = ScrollBarMode.Visible
                                 gdv.Settings.VerticalScrollableHeight = 500
                                 gdv.CommandColumn.Visible = True
                                 gdv.CommandColumn.ShowSelectCheckbox = True
                                 gdv.SettingsBehavior.AllowSelectSingleRowOnly = True
                                 gdv.SettingsBehavior.AutoExpandAllGroups = True
                                 gdv.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords
                                 gdv.Columns.Add("desc_rol", "Seleccione un Rol").Width = Unit.Percentage(80)
                                 gdv.Columns.Add("nom_modulo", "Módulo").GroupIndex = 0
                                 gdv.SettingsBehavior.AllowSelectByRowClick = True
                                 gdv.SettingsBehavior.AllowFocusedRow = True
                                 gdv.ClientSideEvents.FocusedRowChanged = "GdvRolesChange"
                             End Sub).Bind(Model).GetHtml()%>

