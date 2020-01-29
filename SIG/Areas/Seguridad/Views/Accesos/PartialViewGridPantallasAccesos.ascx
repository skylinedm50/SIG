<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>
<%Html.DevExpress.GridView(Sub(gdv)
                                 gdv.Name = "GdvPantallas"
                                 gdv.Width = Unit.Percentage(100)

                                 gdv.KeyFieldName = "id_rol_actividad"
                                 gdv.CallbackRouteValues = New With {Key .Controller = "Accesos", Key .Action = "PartialViewGridPantallasAccesos", Key .id_rol = ViewData("id_rol")}
                                 gdv.SettingsBehavior.AllowFocusedRow = True
                                 gdv.Settings.VerticalScrollBarMode = ScrollBarMode.Visible
                                 gdv.Settings.VerticalScrollableHeight = 450
                                 gdv.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords
                                 gdv.Columns.Add("desc_actividad", "Pantalla")
                                 ' gdv.Columns.Add("desc_opcion", "Opción")
                                 gdv.Columns.Add("nom_modulo", "Módulo")
                                 gdv.Columns.Add(Sub(col)
                                                     col.SetDataItemTemplateContent(Sub(container)
                                                                                        Html.DevExpress.HyperLink(Sub(hl)
                                                                                                                      hl.Name = String.Format("hlaccesopantalla_{0}", container.VisibleIndex)
                                                                                                                      hl.NavigateUrl = "javascript:void(0)"
                                                                                                                      hl.Properties.ClientSideEvents.Click = String.Format("function(s, e) {{ GdvPantallaEliminarClick('{0}'); }}", container.KeyValue.ToString())
                                                                                                                      hl.Properties.Text = "Quitar Acceso"
                                                                                                                  End Sub).Render()
                                                                                    End Sub)
                                                 End Sub)
                             End Sub).Bind(Model).GetHtml() %>
