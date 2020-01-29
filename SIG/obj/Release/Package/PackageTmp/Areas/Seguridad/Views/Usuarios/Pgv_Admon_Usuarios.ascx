<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>


<%
    Html.DevExpress.GridView(Sub(gdv)
                                 gdv.Name = "GdvUsuarios"
                                 gdv.Width = Unit.Percentage(100)

                                 gdv.KeyFieldName = "cod_usuario"
                                 gdv.SettingsBehavior.AllowFocusedRow = True
                                 gdv.CallbackRouteValues = New With {Key .Controller = "Usuarios", Key .Action = "Pgv_Admon_Usuarios"}
                                 gdv.SettingsBehavior.EnableRowHotTrack = True

                                 gdv.Settings.ShowFilterRow = True
                                 gdv.Settings.ShowFilterRowMenu = True
                                 gdv.SettingsPager.PageSizeItemSettings.Visible = True
                                 gdv.SettingsPager.PageSizeItemSettings.Items = New String() {"10", "20", "50"}
                                 gdv.Columns.Add("nom_usuario", " Usuario")
                                 gdv.Columns.Add("nombre_completo", "Nombre Completo")
                                 gdv.Columns.Add("ide_usr_persona", "Identidad")
                                 gdv.Columns.Add("num_tel_usr_persona", "Teléfono")
                                 gdv.Columns.Add("email_usr_persona", "E-mail")
                                 gdv.Columns.Add(Sub(col)
                                                     col.Caption = "Habilitado"
                                                     col.FieldName = "Estado"
                                                     col.EditorProperties().ComboBox(Sub(cb)
                                                                                         cb.Items.Add("Si")
                                                                                         cb.Items.Add("No")
                                                                                     End Sub)
                                                 End Sub)
                                 gdv.Columns.Add(Sub(col)
                                                     col.Caption = "Info"
                                                     col.SetDataItemTemplateContent(Sub(container)
                                                                                        Html.DevExpress.Button(Sub(hl)
                                                                                                                   hl.Name = String.Format("hlusuarioedit_{0}", container.VisibleIndex)
                                                                                                                   hl.ClientSideEvents.Click = String.Format("function(s, e) {{ GdvUsuariosEditClick('{0}'); }}", container.KeyValue.ToString())
                                                                                                                   hl.RenderMode = ButtonRenderMode.Link
                                                                                                                   hl.ControlStyle.CssClass = "fixStyles fa-pencil-square-o"
                                                                                                               End Sub).Render()
                                                                                    End Sub)
                                                 End Sub)
                                 gdv.Columns.Add(Sub(col)
                                                     col.Caption = "Roles"
                                                     col.SetDataItemTemplateContent(Sub(container)
                                                                                        Html.DevExpress.Button(Sub(hl)
                                                                                                                   hl.Name = String.Format("hlroledit_{0}", container.VisibleIndex)
                                                                                                                   hl.ClientSideEvents.Click = String.Format("function(s, e) {{ GdvRolesEditClick('{0}'); }}", container.KeyValue.ToString())
                                                                                                                   hl.RenderMode = ButtonRenderMode.Link
                                                                                                                   hl.ControlStyle.CssClass = "fixStyles fa-cog"
                                                                                                               End Sub).Render()
                                                                                    End Sub)
                                                 End Sub)
                                 gdv.Columns.Add(Sub(col)
                                                     col.SetDataItemTemplateContent(Sub(container)
                                                                                        Html.DevExpress.HyperLink(Sub(hl)
                                                                                                                      hl.Name = String.Format("hlusuariocontra_{0}", container.VisibleIndex)
                                                                                                                      hl.NavigateUrl = "javascript:void(0)"
                                                                                                                      hl.Properties.ClientSideEvents.Click = String.Format("function(s, e) {{ PopupCambioContrasenaClick('{0}'); }}", container.KeyValue.ToString())
                                                                                                                      hl.Properties.Text = "Cambiar Contraseña"
                                                                                                                  End Sub).Render()
                                                                                    End Sub)
                                                 End Sub)
                             End Sub).Bind(Model).GetHtml() %>