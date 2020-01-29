<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%Html.DevExpress.GridView(Sub(gdv)
                                 gdv.Name = "GdvDepartamentos"
                                 gdv.Width = Unit.Percentage(100)

                                 gdv.KeyFieldName = "id_unidad"
                                 gdv.SettingsBehavior.AllowFocusedRow = True
                                 gdv.CallbackRouteValues = New With {Key .Controller = "Usuarios", Key .Action = "PVGDepartamentos"}
                                 gdv.Columns.Add("desc_unidad", "Departamento")
                                 gdv.Columns.Add("habilitado", "Habilitado")
                                 gdv.Columns.Add(Sub(col)

                                                     col.SetDataItemTemplateContent(Sub(container)

                                                                                        Html.DevExpress.Button(Sub(hl)

                                                                                                                   hl.Name = String.Format("hldepartedit_{0}", container.VisibleIndex)
                                                                                                                   '   hl.NavigateUrl = "javascript:void(0)"
                                                                                                                   hl.ClientSideEvents.Click = String.Format("function(s, e) {{ GdvDepartamentoEditClick('{0}'); }}", container.KeyValue.ToString())
                                                                                                                   hl.RenderMode = ButtonRenderMode.Link
                                                                                                                   hl.Text = ""
                                                                                                                   hl.ControlStyle.CssClass = "fixStyles fa-pencil-square-o"
                                                                                                               End Sub).Render()
                                                                                    End Sub)
                                                 End Sub)
                             End Sub).Bind(Model).GetHtml() %>