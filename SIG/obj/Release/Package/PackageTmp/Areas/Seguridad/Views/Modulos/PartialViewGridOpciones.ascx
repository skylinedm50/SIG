<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%Html.DevExpress.GridView(Sub(gdv)
                                 gdv.Name = "GdvOpciones"
                                 gdv.Width = Unit.Percentage(100)

                                 gdv.CallbackRouteValues = New With {Key .Controller = "Modulos", Key .Action = "PartialViewGridOpciones", Key .id_modulos = ViewData("id_modulos")}
                                 gdv.SettingsBehavior.AllowFocusedRow = True
                                 gdv.KeyFieldName = "id_opcion"
                                 gdv.Columns.Add("desc_opcion", "Opciones")
                                 gdv.Columns.Add(Sub(col)
                                                     col.SetDataItemTemplateContent(Sub(container)
                                                                                        Html.DevExpress.Button(Sub(hl)
                                                                                                                   hl.Name = String.Format("hlopcion_{0}", container.VisibleIndex)
                                                                                                                   hl.ClientSideEvents.Click = String.Format("function(s, e) {{ GdvOpcionEditClick('{0}'); }}", container.KeyValue.ToString())
                                                                                                                   hl.RenderMode = ButtonRenderMode.Link
                                                                                                                   hl.Text = ""
                                                                                                                   hl.ControlStyle.CssClass = "fixStyles fa-pencil-square-o"
                                                                                                               End Sub).Render()
                                                                                    End Sub)
                                                 End Sub)
                                 gdv.ClientSideEvents.FocusedRowChanged = "GdvOpcionesChange"
                                 '                gdv.ClientSideEvents.BeginCallback = "GdvOpcionEditClick"
                             End Sub).Bind(Model).GetHtml() %>