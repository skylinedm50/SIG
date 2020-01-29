<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Seguridad/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
SIG | Cambio de Contraseña
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
    <script type="text/javascript" src='/Areas/Seguridad/Scripts/cambioContraseña.js'></script>

    <h2 align="left">Cambio de Contraseña</h2>
    <div align="left">
        <% Html.DevExpress().FormLayout(Sub(frmLayout)
                                             frmLayout.Name = "frmLaout"

                                             frmLayout.Items.AddGroupItem(Sub(group)
                                                                              group.ColCount = 2
                                                                              group.Caption = "Datos Personales"
                                                                              group.SettingsItemCaptions.HorizontalAlign = FormLayoutHorizontalAlign.Left
                                                                              group.SettingsItemCaptions.VerticalAlign = FormLayoutVerticalAlign.Middle
                                                                              group.Items.Add(Sub(item)
                                                                                                  item.Caption = "Nro. Identidad"
                                                                                                  'item.ColSpan = 2
                                                                                                  item.SetNestedContent(Sub()
                                                                                                                            Html.DevExpress().TextBox(Sub(txt)
                                                                                                                                                          txt.Name = "txtIdentidad"
                                                                                                                                                          txt.Properties.MaxLength = 13
                                                                                                                                                          txt.Enabled = False
                                                                                                                                                          txt.Text = ViewData("identidad")
                                                                                                                                                      End Sub).GetHtml()
                                                                                                                        End Sub)
                                                                                              End Sub)
                                                                              group.Items.Add(Sub(item)
                                                                                                  item.Caption = "Teléfono"
                                                                                                  item.SetNestedContent(Sub()
                                                                                                                            Html.DevExpress().TextBox(Sub(txt)
                                                                                                                                                          txt.Name = "txtTelefono"
                                                                                                                                                          txt.Properties.MaxLength = 8
                                                                                                                                                          txt.Enabled = False
                                                                                                                                                          txt.Text = ViewData("telefono")
                                                                                                                                                      End Sub).GetHtml()
                                                                                                                        End Sub)
                                                                                              End Sub)
                                                                              group.Items.Add(Sub(item)
                                                                                                  item.Caption = "Nombre"
                                                                                                  item.ColSpan = 2
                                                                                                  item.SetNestedContent(Sub()
                                                                                                                            Html.DevExpress().TextBox(Sub(txt)
                                                                                                                                                          txt.Name = "txtNombre"
                                                                                                                                                          txt.Enabled = False
                                                                                                                                                          txt.Text = ViewData("nombre")
                                                                                                                                                          txt.Width = 250
                                                                                                                                                      End Sub).GetHtml()
                                                                                                                        End Sub)
                                                                                              End Sub)

                                                                              group.Items.Add(Sub(item)
                                                                                                  item.Caption = "Correo"
                                                                                                  item.ColSpan = 2
                                                                                                  item.SetNestedContent(Sub()
                                                                                                                            Html.DevExpress().TextBox(Sub(txt)
                                                                                                                                                          txt.Name = "txtCorreo"
                                                                                                                                                          txt.Enabled = False
                                                                                                                                                          txt.Text = ViewData("correo")
                                                                                                                                                          txt.Width = 250
                                                                                                                                                      End Sub).GetHtml()
                                                                                                                        End Sub)
                                                                                              End Sub)
                                                                          End Sub)
                                             frmLayout.Items.AddGroupItem(Sub(group)
                                                                              group.Caption = "Datos de Usuario"
                                                                              group.ColCount = 2
                                                                              group.SettingsItemCaptions.HorizontalAlign = FormLayoutHorizontalAlign.Left
                                                                              group.SettingsItemCaptions.VerticalAlign = FormLayoutVerticalAlign.Middle
                                                                              group.Items.Add(Sub(item)
                                                                                                  item.Caption = "Usuario"
                                                                                                  item.VerticalAlign = FormLayoutVerticalAlign.Middle
                                                                                                  item.SetNestedContent(Sub()
                                                                                                                            Html.DevExpress().TextBox(Sub(txt)
                                                                                                                                                          txt.Name = "txtUsuario"
                                                                                                                                                          txt.Enabled = False
                                                                                                                                                          txt.Text = ViewData("usuario")
                                                                                                                                                      End Sub).GetHtml()
                                                                                                                        End Sub)
                                                                                              End Sub)

                                                                              group.Items.Add(Sub(item)
                                                                                                  item.Caption = "Nro Critico"
                                                                                                  item.HelpText = "Módulo de Levantamiento e Incorporaciones"
                                                                                                  item.SetNestedContent(Sub()
                                                                                                                            Html.DevExpress().SpinEdit(Sub(spe)
                                                                                                                                                           spe.Name = "txtNroCritico"
                                                                                                                                                           spe.Properties.MaxValue = 100
                                                                                                                                                           spe.Properties.MinValue = 0
                                                                                                                                                           spe.Enabled = False
                                                                                                                                                           spe.Number = ViewData("critico")
                                                                                                                                                       End Sub).GetHtml()
                                                                                                                        End Sub)
                                                                                              End Sub)
                                                                              group.Items.Add(Sub(item)
                                                                                                  item.Caption = "Contraseña"
                                                                                                  item.SetNestedContent(Sub()
                                                                                                                            Html.DevExpress().TextBox(Sub(txt)
                                                                                                                                                          txt.Name = "txtClave"
                                                                                                                                                          txt.Properties.Password = True
                                                                                                                                                          txt.Properties.ClientSideEvents.Init = "OnPasswordTextBoxInit"
                                                                                                                                                          txt.Properties.ClientSideEvents.KeyUp = "OnPassChanged"
                                                                                                                                                      End Sub).GetHtml()
                                                                                                                            Html.DevExpress().Label(Sub(lbl)
                                                                                                                                                        lbl.Name = "lblMensaje"
                                                                                                                                                        lbl.ClientVisible = False
                                                                                                                                                    End Sub).GetHtml()
                                                                                                                        End Sub)
                                                                                              End Sub)
                                                                              group.Items.Add(Sub(item)
                                                                                                  item.Name = "itempassrating"
                                                                                                  item.ShowCaption = DefaultBoolean.False
                                                                                                  item.ClientVisible = True
                                                                                                  item.SetNestedContent(Sub()
                                                                                                                            Html.DevExpress.RatingControl(Sub(rc)
                                                                                                                                                              rc.Name = "ratingControl"
                                                                                                                                                              rc.ReadOnly = True
                                                                                                                                                              rc.ItemCount = 5
                                                                                                                                                          End Sub).GetHtml()
                                                                                                                            Html.DevExpress.Label(Sub(lbl)
                                                                                                                                                      lbl.Name = "ratingLabel"
                                                                                                                                                      lbl.Properties.EnableClientSideAPI = True
                                                                                                                                                      lbl.Text = "Contraseña segura"
                                                                                                                                                  End Sub).GetHtml()
                                                                                                                        End Sub)
                                                                                              End Sub)
                                                                              group.Items.Add(Sub(item)
                                                                                                  item.Caption = "Confirmar Contraseña"
                                                                                                  item.SetNestedContent(Sub()
                                                                                                                            Html.DevExpress().TextBox(Sub(txt)
                                                                                                                                                          txt.Name = "txtConfirmacionClave"
                                                                                                                                                          txt.Properties.ClientSideEvents.KeyUp = "txtConfirmacionClaveChange"
                                                                                                                                                          txt.Properties.Password = True
                                                                                                                                                      End Sub).GetHtml()
                                                                                                                            Html.DevExpress().Label(Sub(lbl)
                                                                                                                                                        lbl.Name = "lblMensaje2"
                                                                                                                                                        lbl.Text = "Coincide"
                                                                                                                                                        lbl.ClientVisible = False
                                                                                                                                                        lbl.ControlStyle.ForeColor = System.Drawing.Color.Red
                                                                                                                                                    End Sub).GetHtml()
                                                                                                                        End Sub)
                                                                                              End Sub)
                                                                              group.Items.Add(Sub(item)
                                                                                                  item.ShowCaption = DefaultBoolean.False
                                                                                                  item.Name = "itemlblMensaje2"
                                                                                                  item.ClientVisible = True
                                                                                                  item.SetNestedContent(Sub()
                                                                                                                            Html.DevExpress().Label(Sub(lbl)
                                                                                                                                                        lbl.Name = "lblmensaje2"

                                                                                                                                                        lbl.ClientVisible = True
                                                                                                                                                        lbl.Text = "*La contraseña debe contener: Al menos 8 caracteres de longitud, un caracter especial(@&.-$ por ejemplo) y un número."
                                                                                                                                                        lbl.ControlStyle.ForeColor = System.Drawing.Color.Gray
                                                                                                                                                    End Sub).GetHtml()
                                                                                                                        End Sub)
                                                                                              End Sub)
                                                                          End Sub)
                                         End Sub).GetHtml()%>
    </div>
    <br />
    <div align="center" style="width: 700px">
        <% Html.DevExpress().Button(Sub(btn)
                                         btn.Name = "btnActualizar"
                                         btn.Text = "Actualizar Contraseña"
                                         btn.ClientEnabled = False

                                         btn.ClientSideEvents.Click = "btnActualizarClick"

                                     End Sub).GetHtml()%>
       
    </div>
    <div>
                  <%Html.DevExpress.PopupControl(Sub(popup)
                                                       popup.Name = "popupMensaje"

                                                       popup.Width = 380
                                                       popup.AllowDragging = True
                                                       popup.PopupAnimationType = AnimationType.Fade
                                                       popup.HeaderText = "Mensaje"
                                                       popup.CloseAnimationType = AnimationType.Fade
                                                       popup.Modal = True
                                                       popup.AutoUpdatePosition = True
                                                       popup.PopupHorizontalAlign = PopupHorizontalAlign.WindowCenter
                                                       popup.PopupVerticalAlign = PopupVerticalAlign.WindowCenter
                                                       popup.SetContent(Sub()
                                                                            Html.DevExpress.FormLayout(
                                                                            Sub(frm)
                                                                                frm.Name = "frmMensaje"

                                                                                frm.ColCount = 1
                                                                                frm.Items.Add(Sub(item)
                                                                                                  item.Name = "txt"
                                                                                                  item.ShowCaption = DefaultBoolean.False
                                                                                                  item.SetNestedContent(Sub()
                                                                                                                            Html.DevExpress.Label(Sub(lbl)
                                                                                                                                                      lbl.Name = "lblmensaje"
                                                                                                                                                      lbl.Properties.EnableClientSideAPI = True
                                                                                                                                                      lbl.Text = ""
                                                                                                                                                      lbl.Width = 380
                                                                                                                                                  End Sub).GetHtml()
                                                                                                                        End Sub)
                                                                                              End Sub)
                                                                                frm.Items.Add(Sub(item)
                                                                                                  item.ShowCaption = DefaultBoolean.False
                                                                                                  item.HorizontalAlign = FormLayoutHorizontalAlign.Center
                                                                                                  item.SetNestedContent(Sub()

                                                                                                                            Html.DevExpress.Button(Sub(btn)
                                                                                                                                                       btn.Name = "btnAceptar"
                                                                                                                                                       btn.Text = "Aceptar"
                                                                                                                                                       btn.Width = 100
                                                                                                                                                       btn.ClientSideEvents.Click = "function (s, e) { { window.location = '/Home/Login';} }"
                                                                                                                                                   End Sub).GetHtml()
                                                                                                                        End Sub)
                                                                                              End Sub)
                                                                            End Sub).GetHtml()
                                                                        End Sub)
                                                   End Sub).GetHtml() %>
              </div>
</asp:Content>
