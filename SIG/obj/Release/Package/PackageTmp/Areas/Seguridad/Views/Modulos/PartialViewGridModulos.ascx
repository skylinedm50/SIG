<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%Html.DevExpress.GridView(Sub(gdv)
                                 gdv.Name = "GdvModulos"
                                 gdv.Width = Unit.Percentage(100)

                                 gdv.SettingsBehavior.AllowFocusedRow = True
                                 gdv.CallbackRouteValues = New With {Key .Controller = "Modulos", Key .Action = "PartialViewGridModulos"}
                                 gdv.KeyFieldName = "id_modulos"
                                 gdv.Columns.Add("nom_modulo", "Módulo")

                                 gdv.Columns.Add(Sub(col)

                                                     col.SetDataItemTemplateContent(Sub(container)
                                                                                        Html.DevExpress.Button(Sub(hl)
                                                                                                                   hl.Name = String.Format("hlmodulo_{0}", container.VisibleIndex)
                                                                                                                   hl.ClientSideEvents.Click = String.Format("function(s, e) {{ GdvModuloEditClick('{0}'); }}", container.KeyValue.ToString())
                                                                                                                   hl.RenderMode = ButtonRenderMode.Link
                                                                                                                   hl.Text = ""
                                                                                                                   hl.ControlStyle.CssClass = "fixStyles fa-pencil-square-o"

                                                                                                               End Sub).Render()
                                                                                    End Sub)
                                                 End Sub)
                                 gdv.ClientSideEvents.FocusedRowChanged = "GdvModulosChange"

                             End Sub).Bind(Model).GetHtml()%>