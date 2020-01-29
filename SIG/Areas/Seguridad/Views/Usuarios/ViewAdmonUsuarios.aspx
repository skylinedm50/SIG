<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Seguridad/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
SIG | Módulo de Seguridad 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>ADMINISTRACIÓN DE USUARIOS</h2>
    <% 

          Html.DevExpress().RenderScripts(Page,
                                          New Script With {.ExtensionSuite = ExtensionSuite.GridView},
                                          New Script With {.ExtensionSuite = ExtensionSuite.Editors},
                                          New Script With {.ExtensionSuite = ExtensionSuite.NavigationAndLayout}
    )

          Html.DevExpress().RenderStyleSheets(Page,
                                New StyleSheet With {.ExtensionSuite = ExtensionSuite.GridView},
                                New StyleSheet With {.ExtensionSuite = ExtensionSuite.Editors},
                                New StyleSheet With {.ExtensionSuite = ExtensionSuite.NavigationAndLayout}
  )
    %>

    <script type="text/javascript" src='/Areas/Seguridad/Scripts/usuarios.js'></script>
    
    <style type="text/css">
            .fixStyles {
                font: normal normal normal 25px FontAwesome;
                color: white ;
       
            }
            .fixStyles span {
                display: none;
            }
    </style>
    <div>
        <%Html.DevExpress.FormLayout(Sub(frm)
                                           frm.Name = "frmLayout2"
                                           frm.Width = Unit.Percentage(100)
                                           frm.ColCount = 1
                                           frm.Items.Add(Sub(item)
                                                             item.ShowCaption = DefaultBoolean.False
                                                             item.Width = Unit.Percentage(100)
                                                             item.SetNestedContent(Sub()
                                                                                       ViewContext.Writer.Write("<div id='divGdvUsuarios'>")
                                                                                       Html.RenderAction("Pgv_Admon_Usuarios")
                                                                                       ViewContext.Writer.Write("</div>")
                                                                                   End Sub)
                                                         End Sub)
                                       End Sub).GetHtml() %>
    </div>

    <div>
        <%Html.DevExpress().PopupControl(Sub(popup)
                                               popup.Name = "MdlEditarUsuario"
                                               popup.Width = 380
                                               popup.AllowDragging = True
                                               popup.CloseAction = CloseAction.CloseButton
                                               popup.CloseOnEscape = True
                                               popup.PopupAnimationType = AnimationType.Fade
                                               popup.CloseAnimationType = AnimationType.Fade
                                               popup.HeaderText = "Información de Usuario"
                                               popup.Modal = True
                                               popup.AutoUpdatePosition = True
                                               popup.PopupHorizontalAlign = PopupHorizontalAlign.WindowCenter
                                               popup.PopupVerticalAlign = PopupVerticalAlign.WindowCenter
                                               popup.SetContent(Sub()
                                                                    Html.DevExpress().PageControl(Sub(pc)
                                                                                                      pc.Name = "pcEditarUsuario"
                                                                                                      pc.ActivateTabPageAction = ActivateTabPageAction.Click
                                                                                                      pc.EnableHotTrack = True
                                                                                                      pc.TabAlign = TabAlign.Center
                                                                                                      pc.TabPosition = TabPosition.Top
                                                                                                      pc.SaveStateToCookies = False
                                                                                                      pc.Width = 800
                                                                                                      pc.TabPages.Add("Datos de Usuario").SetContent(Sub()
                                                                                                                                                         Html.DevExpress.FormLayout(Sub(frm)
                                                                                                                                                                                        frm.Name = "frmEditarUsuario"
                                                                                                                                                                                        frm.ColCount = 2
                                                                                                                                                                                        frm.SettingsItemCaptions.VerticalAlign = FormLayoutVerticalAlign.Middle
                                                                                                                                                                                        frm.Items.Add(Sub(item)
                                                                                                                                                                                                          item.Caption = "Usuario"
                                                                                                                                                                                                          item.NestedExtensionSettings.Width = 380
                                                                                                                                                                                                          item.SetNestedContent(Sub()
                                                                                                                                                                                                                                    Html.DevExpress().TextBox(Sub(txt)
                                                                                                                                                                                                                                                                  txt.Name = "txtEditUsuario"
                                                                                                                                                                                                                                                                  txt.ReadOnly = True
                                                                                                                                                                                                                                                                  txt.Width = 380
                                                                                                                                                                                                                                                              End Sub).GetHtml()
                                                                                                                                                                                                                                End Sub)
                                                                                                                                                                                                      End Sub)
                                                                                                                                                                                        frm.Items.Add(Sub(item)
                                                                                                                                                                                                          item.Name = "txtEditIdentidad"
                                                                                                                                                                                                          item.Caption = "Identidad"
                                                                                                                                                                                                          item.NestedExtensionType = FormLayoutNestedExtensionItemType.TextBox
                                                                                                                                                                                                          item.NestedExtensionSettings.Width = 380
                                                                                                                                                                                                      End Sub)
                                                                                                                                                                                        frm.Items.Add(Sub(item)
                                                                                                                                                                                                          item.Name = "txtEditPnombre"
                                                                                                                                                                                                          item.Caption = "Primer Nombre"
                                                                                                                                                                                                          item.NestedExtensionType = FormLayoutNestedExtensionItemType.TextBox
                                                                                                                                                                                                          item.NestedExtensionSettings.Width = 380
                                                                                                                                                                                                      End Sub)
                                                                                                                                                                                        frm.Items.Add(Sub(item)
                                                                                                                                                                                                          item.Name = "txtEditSnombre"
                                                                                                                                                                                                          item.Caption = "Segundo Nombre"
                                                                                                                                                                                                          item.NestedExtensionType = FormLayoutNestedExtensionItemType.TextBox
                                                                                                                                                                                                          item.NestedExtensionSettings.Width = 380
                                                                                                                                                                                                      End Sub)
                                                                                                                                                                                        frm.Items.Add(Sub(item)
                                                                                                                                                                                                          item.Name = "txtEditPapellido"
                                                                                                                                                                                                          item.Caption = "Primer Apellido"
                                                                                                                                                                                                          item.NestedExtensionType = FormLayoutNestedExtensionItemType.TextBox
                                                                                                                                                                                                          item.NestedExtensionSettings.Width = 380
                                                                                                                                                                                                      End Sub)
                                                                                                                                                                                        frm.Items.Add(Sub(item)
                                                                                                                                                                                                          item.Name = "txtEditSapellido"
                                                                                                                                                                                                          item.Caption = "Segundo Apellido"
                                                                                                                                                                                                          item.NestedExtensionType = FormLayoutNestedExtensionItemType.TextBox
                                                                                                                                                                                                          item.NestedExtensionSettings.Width = 380
                                                                                                                                                                                                      End Sub)
                                                                                                                                                                                        frm.Items.Add(Sub(item)
                                                                                                                                                                                                          item.Name = "txtEditEmail"
                                                                                                                                                                                                          item.Caption = "Correo Electrónico"
                                                                                                                                                                                                          item.NestedExtensionType = FormLayoutNestedExtensionItemType.TextBox
                                                                                                                                                                                                          item.NestedExtensionSettings.Width = 380
                                                                                                                                                                                                      End Sub)
                                                                                                                                                                                        frm.Items.Add(Sub(item)
                                                                                                                                                                                                          item.Name = "txtEditTelefono"
                                                                                                                                                                                                          item.Caption = "Teléfono"
                                                                                                                                                                                                          item.NestedExtensionType = FormLayoutNestedExtensionItemType.TextBox
                                                                                                                                                                                                          item.NestedExtensionSettings.Width = 380
                                                                                                                                                                                                      End Sub)
                                                                                                                                                                                        frm.Items.Add(Sub(item)
                                                                                                                                                                                                          item.Name = "cbJefe"
                                                                                                                                                                                                          item.Caption = "Jefe Inmediato"
                                                                                                                                                                                                          item.NestedExtensionType = FormLayoutNestedExtensionItemType.CheckBox
                                                                                                                                                                                                          item.NestedExtensionSettings.Width = 380

                                                                                                                                                                                                      End Sub)
                                                                                                                                                                                        frm.Items.Add(Sub(item)
                                                                                                                                                                                                          item.Name = "txtEditProyecto"
                                                                                                                                                                                                          item.Caption = "Proyecto o Unidad"
                                                                                                                                                                                                          ' item.NestedExtensionType = FormLayoutNestedExtensionItemType.TextBox
                                                                                                                                                                                                          item.NestedExtensionSettings.Width = 380
                                                                                                                                                                                                          item.SetNestedContent(Sub()
                                                                                                                                                                                                                                    Html.RenderAction("PartialViewCbProyecto")
                                                                                                                                                                                                                                End Sub)
                                                                                                                                                                                                      End Sub)
                                                                                                                                                                                        frm.Items.Add(Sub(item)
                                                                                                                                                                                                          item.Name = "cbEditHabilitado"
                                                                                                                                                                                                          item.Caption = "Habilitado"
                                                                                                                                                                                                          item.NestedExtensionType = FormLayoutNestedExtensionItemType.CheckBox
                                                                                                                                                                                                          item.NestedExtensionSettings.Width = 380
                                                                                                                                                                                                      End Sub)
                                                                                                                                                                                        frm.Items.Add(Sub(item)
                                                                                                                                                                                                          item.Caption = ""
                                                                                                                                                                                                          item.ShowCaption = DefaultBoolean.False
                                                                                                                                                                                                          item.NestedExtensionSettings.Width = 380
                                                                                                                                                                                                      End Sub)
                                                                                                                                                                                        frm.Items.Add(Sub(item)
                                                                                                                                                                                                          item.Caption = ""
                                                                                                                                                                                                          item.ShowCaption = DefaultBoolean.False
                                                                                                                                                                                                          item.NestedExtensionSettings.Width = 380
                                                                                                                                                                                                      End Sub)
                                                                                                                                                                                        frm.Items.Add(Sub(item)
                                                                                                                                                                                                          item.HorizontalAlign = FormLayoutHorizontalAlign.Right
                                                                                                                                                                                                          item.ShowCaption = DefaultBoolean.False
                                                                                                                                                                                                          item.NestedExtensionSettings.Width = 380
                                                                                                                                                                                                          item.SetNestedContent(Sub()
                                                                                                                                                                                                                                    Html.DevExpress().Button(Sub(btn)
                                                                                                                                                                                                                                                                 btn.Name = "btnRecordar"

                                                                                                                                                                                                                                                                 btn.Text = "Recordar Contraseña"
                                                                                                                                                                                                                                                                 btn.ToolTip = "Se le enviará un correo electrónico en donde se mostrará su contraseña de usuario"
                                                                                                                                                                                                                                                                 btn.ClientSideEvents.Click = "btnRecordarClick"
                                                                                                                                                                                                                                                             End Sub).GetHtml()
                                                                                                                                                                                                                                    ViewContext.Writer.Write("&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp")
                                                                                                                                                                                                                                    Html.DevExpress().Button(Sub(btn)
                                                                                                                                                                                                                                                                 btn.Name = "btnActualizarUsuario"
                                                                                                                                                                                                                                                                 btn.Text = "Guardar Cambios"
                                                                                                                                                                                                                                                                 btn.ClientSideEvents.Click = "btnActualizarUsuarioClick"
                                                                                                                                                                                                                                                             End Sub).GetHtml()
                                                                                                                                                                                                                                    ViewContext.Writer.Write("&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp")
                                                                                                                                                                                                                                    Html.DevExpress().Button(Sub(btn)
                                                                                                                                                                                                                                                                 btn.Name = "btnSalir"
                                                                                                                                                                                                                                                                 btn.Text = "Salir"
                                                                                                                                                                                                                                                                 btn.ClientSideEvents.Click = "btnSalirClick"
                                                                                                                                                                                                                                                             End Sub).GetHtml()
                                                                                                                                                                                                                                End Sub)
                                                                                                                                                                                                      End Sub)
                                                                                                                                                                                    End Sub).GetHtml()
                                                                                                                                                     End Sub)

                                                                                                  End Sub).GetHtml()
                                                                End Sub)
                                               '    popup.SetContent()
                                           End Sub).GetHtml() %>
    </div>
    
    <%Html.DevExpress.PopupControl(Sub(popup)
                                         popup.Name = "MdlEditRoles"

                                         popup.Width = 800
                                         popup.AllowDragging = True
                                         popup.CloseAction = CloseAction.CloseButton
                                         popup.CloseOnEscape = True
                                         popup.PopupAnimationType = AnimationType.Fade
                                         popup.CloseAnimationType = AnimationType.Fade
                                         popup.HeaderText = "Roles"
                                         popup.Modal = True
                                         popup.AutoUpdatePosition = True
                                         popup.PopupHorizontalAlign = PopupHorizontalAlign.WindowCenter
                                         popup.PopupVerticalAlign = PopupVerticalAlign.WindowCenter
                                         popup.SetContent(Sub()
                                                              ViewContext.Writer.Write("<div id='divGdvPermisosEdit'>")
                                                              Html.RenderPartial("PVGPermisosEditarUsuario")
                                                              ViewContext.Writer.Write("</div><br><br>")
                                                              ViewContext.Writer.Write("<center>")
                                                              Html.DevExpress().Button(Sub(btn)
                                                                                           btn.Name = "btnSalirEditar"

                                                                                           btn.Text = "Salir"
                                                                                           btn.ClientSideEvents.Click = "function (s, e) { { MdlEditRoles.Hide();} }"
                                                                                       End Sub).GetHtml()
                                                              ViewContext.Writer.Write("</center>")
                                                          End Sub)
                                     End Sub).GetHtml() %>

    <div>
        <%Html.DevExpress().PopupControl(Sub(popup)
                                               popup.Name = "MdlCambioContrasena"

                                               popup.Width = 380
                                               popup.AllowDragging = True
                                               popup.CloseAction = CloseAction.CloseButton
                                               popup.CloseOnEscape = True
                                               popup.PopupAnimationType = AnimationType.Fade
                                               popup.CloseAnimationType = AnimationType.Fade
                                               popup.HeaderText = "Cambio de Contraseña"
                                               popup.Modal = True
                                               popup.AutoUpdatePosition = True
                                               popup.PopupHorizontalAlign = PopupHorizontalAlign.WindowCenter
                                               popup.PopupVerticalAlign = PopupVerticalAlign.WindowCenter
                                               popup.SetContent(Sub()
                                                                    Html.DevExpress.FormLayout(Sub(frm)
                                                                                                   frm.Name = "frmCambioContrasena"

                                                                                                   frm.ColCount = 1
                                                                                                   frm.SettingsItemCaptions.VerticalAlign = FormLayoutVerticalAlign.Middle
                                                                                                   frm.Items.Add(Sub(item)
                                                                                                                     item.ShowCaption = DefaultBoolean.False
                                                                                                                     item.SetNestedContent(Sub()
                                                                                                                                               Html.DevExpress().Label(Sub(lbl)
                                                                                                                                                                           lbl.Name = "txtMensaje"

                                                                                                                                                                           lbl.Text = "¿Está seguro que desea habilitar el servicio de cambio de contraseña para este usuario?"
                                                                                                                                                                           lbl.Width = 380
                                                                                                                                                                       End Sub).GetHtml()
                                                                                                                                           End Sub)
                                                                                                                 End Sub)
                                                                                                   frm.Items.Add(Sub(item)
                                                                                                                     item.ShowCaption = DefaultBoolean.False
                                                                                                                     item.HorizontalAlign = FormLayoutHorizontalAlign.Center
                                                                                                                     item.SetNestedContent(Sub()
                                                                                                                                               Html.DevExpress().Button(Sub(btn)
                                                                                                                                                                            btn.Name = "btnCambiarContrasena"

                                                                                                                                                                            btn.ClientSideEvents.Click = "btnCambiarContrasenaClick"
                                                                                                                                                                            btn.Text = "Si"
                                                                                                                                                                            btn.Width = 100

                                                                                                                                                                        End Sub).GetHtml()
                                                                                                                                               ViewContext.Writer.Write("&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp")
                                                                                                                                               Html.DevExpress().Button(Sub(btn)
                                                                                                                                                                            btn.Name = "btnSalirCC"

                                                                                                                                                                            btn.Text = "No"
                                                                                                                                                                            btn.ClientSideEvents.Click = "function (s, e) { { MdlCambioContrasena.Hide();} }"
                                                                                                                                                                            btn.Width = 100
                                                                                                                                                                            btn.Styles.Style.BackColor = System.Drawing.Color.White
                                                                                                                                                                            btn.Styles.Style.ForeColor = System.Drawing.Color.FromArgb(0, 150, 136)
                                                                                                                                                                        End Sub).GetHtml()
                                                                                                                                           End Sub)
                                                                                                                 End Sub)
                                                                                               End Sub).GetHtml()
                                                                End Sub)
                                           End Sub).GetHtml() %>
    </div>
</asp:Content>
