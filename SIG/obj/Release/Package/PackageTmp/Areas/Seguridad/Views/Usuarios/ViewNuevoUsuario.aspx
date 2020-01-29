<%@ Page Title="" Language="VB" MasterPageFile="~/Areas/Seguridad/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
SIG | Módulo de Seguridad 
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

    <script type="text/javascript" src='/Areas/Seguridad/Scripts/usuarios.js'></script>
    
     
    
   
        <h2 class ="blue-text text-darken-3 flow-text">REGISTRO DE USUARIOS</h2>
     <div align="center" class="card grey lighten-5 z-depth-3">
        <% Html.DevExpress().FormLayout(Sub(frmLayout)
                                             frmLayout.Name = "frmLayout"
                                             frmLayout.ClientSideEvents.Init = "OnInit"
                                             frmLayout.Width = Unit.Percentage(100)
                                             frmLayout.SettingsItemCaptions.VerticalAlign = FormLayoutVerticalAlign.Middle
                                             frmLayout.SettingsItemHelpTexts.VerticalAlign = HelpTextVerticalAlign.Middle
                                             frmLayout.Items.AddGroupItem(Sub(group)
                                                                              group.ColCount = 2
                                                                              group.Caption = "Datos Personales"
                                                                              group.AlignItemCaptions = True
                                                                              group.SettingsItemCaptions.VerticalAlign = FormLayoutVerticalAlign.Middle
                                                                              group.GroupBoxDecoration = GroupBoxDecoration.HeadingLine
                                                                              group.Items.Add(Sub(item)
                                                                                                  item.Caption = "Nro. Identidad"
                                                                                                  item.RequiredMarkDisplayMode = FieldRequiredMarkMode.Required
                                                                                                  item.SetNestedContent(Sub()
                                                                                                                            Html.DevExpress().TextBox(Sub(txt)
                                                                                                                                                          txt.Name = "txtIdentidad"
                                                                                                                                                          txt.Properties.MaxLength = 13
                                                                                                                                                      End Sub).GetHtml()
                                                                                                                        End Sub)
                                                                                              End Sub)
                                                                              group.Items.Add(Sub(item)
                                                                                                  item.Caption = "Segundo Nombre"
                                                                                                  item.SetNestedContent(Sub()
                                                                                                                            Html.DevExpress().TextBox(Sub(txt)
                                                                                                                                                          txt.Name = "txtNombre2"
                                                                                                                                                      End Sub).GetHtml()
                                                                                                                        End Sub)
                                                                                              End Sub)
                                                                              group.Items.Add(Sub(item)
                                                                                                  item.Caption = "Primer Nombre"
                                                                                                  item.RequiredMarkDisplayMode = FieldRequiredMarkMode.Required
                                                                                                  item.SetNestedContent(Sub()
                                                                                                                            Html.DevExpress().TextBox(Sub(txt)
                                                                                                                                                          txt.Name = "txtNombre1"
                                                                                                                                                      End Sub).GetHtml()
                                                                                                                        End Sub)
                                                                                              End Sub)
                                                                              group.Items.Add(Sub(item)
                                                                                                  item.Caption = "Primer Apellido"
                                                                                                  item.RequiredMarkDisplayMode = FieldRequiredMarkMode.Required
                                                                                                  item.SetNestedContent(Sub()
                                                                                                                            Html.DevExpress().TextBox(Sub(txt)
                                                                                                                                                          txt.Name = "txtApellido1"
                                                                                                                                                      End Sub).GetHtml()
                                                                                                                        End Sub)
                                                                                              End Sub)
                                                                              group.Items.Add(Sub(item)
                                                                                                  item.Caption = "Segundo Apellido"
                                                                                                  item.SetNestedContent(Sub()
                                                                                                                            Html.DevExpress().TextBox(Sub(txt)
                                                                                                                                                          txt.Name = "txtApellido2"
                                                                                                                                                      End Sub).GetHtml()
                                                                                                                        End Sub)
                                                                                              End Sub)
                                                                              group.Items.Add(Sub(item)
                                                                                                  item.Caption = "Teléfono"
                                                                                                  item.RequiredMarkDisplayMode = FieldRequiredMarkMode.Required
                                                                                                  item.SetNestedContent(Sub()
                                                                                                                            Html.DevExpress().TextBox(Sub(txt)
                                                                                                                                                          txt.Name = "txtTelefono"
                                                                                                                                                          txt.Properties.MaxLength = 8
                                                                                                                                                      End Sub).GetHtml()
                                                                                                                        End Sub)
                                                                                              End Sub)
                                                                              group.Items.Add(Sub(item)
                                                                                                  item.Caption = "Correo"
                                                                                                  item.RequiredMarkDisplayMode = FieldRequiredMarkMode.Required
                                                                                                  item.SetNestedContent(Sub()
                                                                                                                            Html.DevExpress().TextBox(Sub(txt)
                                                                                                                                                          txt.Name = "txtCorreo"
                                                                                                                                                      End Sub).GetHtml()
                                                                                                                        End Sub)
                                                                                              End Sub)
                                                                              group.Items.Add(Sub(item)
                                                                                                  item.Caption = "Confirmación de correo"
                                                                                                  item.RequiredMarkDisplayMode = FieldRequiredMarkMode.Required
                                                                                                  item.SetNestedContent(Sub()
                                                                                                                            Html.DevExpress().TextBox(Sub(txt)
                                                                                                                                                          txt.Name = "txtCorreoConf"
                                                                                                                                                      End Sub).GetHtml()
                                                                                                                        End Sub)
                                                                                              End Sub)
                                                                          End Sub)


                                             frmLayout.Items.AddGroupItem(Sub(group)
                                                                              group.Caption = "Información del Cargo"
                                                                              group.ColCount = 1
                                                                              group.SettingsItemCaptions.VerticalAlign = FormLayoutVerticalAlign.Middle
                                                                              group.GroupBoxDecoration = GroupBoxDecoration.HeadingLine
                                                                              group.Items.Add(Sub(item)
                                                                                                  item.Caption = "Proyecto/Unidad"
                                                                                                  item.Name = "itemProyecto"
                                                                                                  item.SetNestedContent(Sub()
                                                                                                                            Html.RenderAction("PartialViewCbProyecto")
                                                                                                                        End Sub)
                                                                                              End Sub)
                                                                              group.Items.Add(Sub(item)
                                                                                                  item.Caption = "¿El usuario es jefe de esta unidad?"
                                                                                              End Sub)
                                                                              group.Items.Add(Sub(item)
                                                                                                  item.ShowCaption = DefaultBoolean.False
                                                                                                  item.SetNestedContent(Sub()
                                                                                                                            Html.DevExpress.RadioButton(Sub(rb)
                                                                                                                                                            rb.Name = "rbSi"
                                                                                                                                                            rb.Text = "Sí"
                                                                                                                                                            rb.Properties.ClientSideEvents.CheckedChanged = "rbSiChange"
                                                                                                                                                            rb.Checked = False
                                                                                                                                                        End Sub).GetHtml()
                                                                                                                        End Sub)
                                                                                              End Sub)
                                                                              group.Items.Add(Sub(item)
                                                                                                  item.ShowCaption = DefaultBoolean.False
                                                                                                  item.SetNestedContent(Sub()
                                                                                                                            Html.DevExpress.RadioButton(Sub(rb)
                                                                                                                                                            rb.Name = "rbNo"
                                                                                                                                                            rb.Text = "No"
                                                                                                                                                            rb.Properties.ClientSideEvents.CheckedChanged = "rbNoChange"
                                                                                                                                                            rb.Checked = True
                                                                                                                                                        End Sub).GetHtml()
                                                                                                                        End Sub)
                                                                                              End Sub)
                                                                              group.Items.Add(Sub(item)
                                                                                                  item.ShowCaption = DefaultBoolean.False
                                                                                                  item.SetNestedContent(Sub()
                                                                                                                            Html.RenderAction("PVGPermisosNuevoUsuario")
                                                                                                                        End Sub)
                                                                                              End Sub)
                                                                              group.Items.Add(Sub(item)
                                                                                                  item.Caption = "Nro Critico por defecto"
                                                                                                  item.SetNestedContent(Sub()
                                                                                                                            Html.DevExpress().CheckBox(Sub(chk)
                                                                                                                                                           chk.Name = "chkNroCritico"
                                                                                                                                                           chk.Checked = True
                                                                                                                                                           chk.Properties.ClientSideEvents.CheckedChanged = "chkNroCriticoChange"
                                                                                                                                                       End Sub).GetHtml()

                                                                                                                        End Sub)
                                                                                              End Sub)
                                                                              group.Items.Add(Sub(item)
                                                                                                  item.Caption = "Número Crítico"
                                                                                                  item.Name = "itemcritico"
                                                                                                  item.ClientVisible = False
                                                                                                  item.HelpText = "Módulo de Levantamiento e Incorporaciones"
                                                                                                  item.SetNestedContent(Sub()
                                                                                                                            Html.DevExpress().SpinEdit(Sub(spe)
                                                                                                                                                           spe.Name = "speNroCritico"
                                                                                                                                                           spe.Number = 0
                                                                                                                                                           spe.Properties.MaxValue = 100
                                                                                                                                                           spe.Properties.MinValue = 0
                                                                                                                                                           spe.Width = 100
                                                                                                                                                           spe.ClientVisible = True
                                                                                                                                                       End Sub).GetHtml()
                                                                                                                        End Sub)
                                                                                              End Sub)
                                                                          End Sub)
                                         End Sub).GetHtml()%>
    </div>
    <div align="center">
        <% Html.DevExpress().Button(Sub(btn)
                                         btn.Name = "btnRegistrar"
                                         btn.Text = "Registrar"
                                         btn.ClientEnabled = True
                                         btn.ClientSideEvents.Click = "btnRegistrarClick"
                                     End Sub).GetHtml()%>
        &nbsp&nbsp&nbsp
        <% Html.DevExpress().Button(Sub(btn)
                                         btn.Name = "btnLimpiar"
                                         btn.Text = "Limpiar"
                                         btn.ClientEnabled = True
                                         btn.ClientSideEvents.Click = "btnLimpiarClick"
                                     End Sub).GetHtml()%>
    </div>
    
     <div>
                  <%Html.DevExpress.PopupControl(Sub(popup)
                                                       popup.Name = "popupMensaje"

                                                       popup.Width = 380
                                                       popup.AllowDragging = True
                                                       popup.CloseAction = CloseAction.CloseButton
                                                       popup.CloseOnEscape = True
                                                       popup.PopupAnimationType = AnimationType.Fade
                                                       popup.HeaderText = "Mensaje de confirmación"
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
                                                                                                                                                      lbl.Text = "El usuario se ha registrado exitosamente, hemos enviado un correo eléctronico al usuario con su usuario y una contraseña generada aleatoriamente para ingresar al sistema"
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
                                                                                                                                                       btn.ClientSideEvents.Click = "function (s, e) { {  location.reload();} }"

                                                                                                                                                   End Sub).GetHtml()
                                                                                                                        End Sub)
                                                                                              End Sub)
                                                                            End Sub).GetHtml()
                                                                        End Sub)
                                                   End Sub).GetHtml() %>
              </div>
</asp:Content>
