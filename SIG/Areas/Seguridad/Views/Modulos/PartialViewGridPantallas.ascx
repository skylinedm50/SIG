<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Html.DevExpress.GridView(Sub(gdv)
                                 gdv.Name = "GdvPantalla"
                                 gdv.Width = Unit.Percentage(100)

                                 gdv.CallbackRouteValues = New With {Key .Controller = "Modulos", Key .Action = "PartialViewGridPantallas", Key .id_opcion = ViewData("id_opcion")}
                                 gdv.SettingsBehavior.AllowFocusedRow = True
                                 gdv.KeyFieldName = "id_actividad"
                                 gdv.Columns.Add("desc_actividad", "Pantallas")
                                 gdv.Columns.Add(Sub(col)

                                                     col.SetDataItemTemplateContent(Sub(container)

                                                                                        Html.DevExpress.Button(Sub(hl)

                                                                                                                   hl.Name = String.Format("hlpantalla_{0}", container.VisibleIndex)
                                                                                                                   '  hl.NavigateUrl = "javascript:void(0)"
                                                                                                                   hl.ClientSideEvents.Click = String.Format("function(s, e) {{ GdvPantallaEditClick('{0}'); }}", container.KeyValue.ToString())
                                                                                                                   hl.RenderMode = ButtonRenderMode.Link
                                                                                                                   hl.Text = ""
                                                                                                                   hl.ControlStyle.CssClass = "fixStyles fa-pencil-square-o"
                                                                                                               End Sub).Render()
                                                                                    End Sub)
                                                 End Sub)
                             End Sub).Bind(Model).GetHtml() %>