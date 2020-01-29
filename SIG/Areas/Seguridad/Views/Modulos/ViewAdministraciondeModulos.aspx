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

<script type="text/javascript" src='/Areas/Seguridad/Scripts/modulos.js'></script>

<style type="text/css">
    .fixStyles {
        font: normal normal normal 25px FontAwesome;
        color: white ;
       
    }
        .fixStyles span {
            display: none;
        }
</style>

<div align="center">
<h2>ADMINISTRACIÓN DE MÓDULOS</h2>
</div>
    <div>
         <%Html.DevExpress().FormLayout(Sub(frmLayout)
                                              frmLayout.Name = "frmLaout"
                                              frmLayout.Width = Unit.Percentage(100)
                                              frmLayout.ColCount = 3
                                              ' frmLayout.ControlStyle.BackColor = System.Drawing.Color.WhiteSmoke
                                              frmLayout.Items.AddGroupItem(Sub(group)
                                                                               group.Caption = "Modulos"
                                                                               group.Width = Unit.Percentage(25) 'Ajuste tamaño del grid
                                                                               group.SettingsItemCaptions.VerticalAlign = FormLayoutVerticalAlign.Middle
                                                                               group.SettingsItemCaptions.HorizontalAlign = FormLayoutHorizontalAlign.Center
                                                                               group.GroupBoxDecoration = GroupBoxDecoration.HeadingLine
                                                                               group.Items.Add(Sub(item)
                                                                                                   item.ShowCaption = DefaultBoolean.False
                                                                                                   item.SetNestedContent(Sub()
                                                                                                                             Html.DevExpress.Button(Sub(btn)
                                                                                                                                                        btn.Name = "btnNuevoModulo"
                                                                                                                                                        btn.Text = "+  Nuevo Módulo"
                                                                                                                                                        btn.Width = Unit.Percentage(50)
                                                                                                                                                        btn.ClientSideEvents.Click = "btnNuevoModuloClick"
                                                                                                                                                    End Sub).GetHtml()
                                                                                                                         End Sub)
                                                                                               End Sub)
                                                                               group.Items.Add(Sub(item)
                                                                                                   item.ShowCaption = DefaultBoolean.False
                                                                                                   item.SetNestedContent(Sub()
                                                                                                                             ViewContext.Writer.Write("<div id='divGdvModulos'>")
                                                                                                                             Html.RenderAction("PartialViewGridModulos")
                                                                                                                             ViewContext.Writer.Write("</div>")
                                                                                                                         End Sub)
                                                                                               End Sub)
                                                                           End Sub)
                                              frmLayout.Items.AddGroupItem(Sub(group)
                                                                               group.Caption = "Opciones"
                                                                               group.Width = Unit.Percentage(25)
                                                                               group.SettingsItemCaptions.VerticalAlign = FormLayoutVerticalAlign.Middle
                                                                               group.SettingsItemCaptions.HorizontalAlign = FormLayoutHorizontalAlign.Center
                                                                               group.GroupBoxDecoration = GroupBoxDecoration.HeadingLine
                                                                               group.Items.Add(Sub(item)
                                                                                                   item.ShowCaption = DefaultBoolean.False
                                                                                                   item.SetNestedContent(Sub()
                                                                                                                             Html.DevExpress.Button(Sub(btn)
                                                                                                                                                        btn.Name = "btnNuevaOpcion"
                                                                                                                                                        btn.Text = "+   Nueva Opción"
                                                                                                                                                        btn.ClientEnabled = False
                                                                                                                                                        btn.Width = Unit.Percentage(50)
                                                                                                                                                        btn.ClientSideEvents.Click = "btnNuevaOpcionClick"
                                                                                                                                                    End Sub).GetHtml()
                                                                                                                         End Sub)
                                                                                               End Sub)
                                                                               group.Items.Add(Sub(item)
                                                                                                   item.ShowCaption = DefaultBoolean.False
                                                                                                   item.SetNestedContent(Sub()
                                                                                                                             ViewContext.Writer.Write("<div id='divGdvOpciones'>")
                                                                                                                             Html.RenderPartial("PartialViewGridOpciones")
                                                                                                                             ViewContext.Writer.Write("</div>")
                                                                                                                         End Sub)
                                                                                               End Sub)
                                                                           End Sub)
                                              frmLayout.Items.AddGroupItem(Sub(group)
                                                                               group.Caption = "Pantallas"
                                                                               group.Width = Unit.Percentage(25)
                                                                               group.SettingsItemCaptions.VerticalAlign = FormLayoutVerticalAlign.Middle
                                                                               group.SettingsItemCaptions.HorizontalAlign = FormLayoutHorizontalAlign.Center
                                                                               group.GroupBoxDecoration = GroupBoxDecoration.HeadingLine
                                                                               group.Items.Add(Sub(item)
                                                                                                   item.ShowCaption = DefaultBoolean.False
                                                                                                   item.SetNestedContent(Sub()
                                                                                                                             Html.DevExpress.Button(Sub(btn)
                                                                                                                                                        btn.Name = "btnNuevaPantalla"
                                                                                                                                                        btn.Text = "+  Nueva Pantalla"
                                                                                                                                                        btn.ClientEnabled = False
                                                                                                                                                        btn.Width = Unit.Percentage(50)
                                                                                                                                                        btn.ClientSideEvents.Click = "btnNuevaPantallaClick"
                                                                                                                                                    End Sub).GetHtml()
                                                                                                                         End Sub)
                                                                                               End Sub)
                                                                               group.Items.Add(Sub(item)
                                                                                                   item.ShowCaption = DefaultBoolean.False
                                                                                                   item.SetNestedContent(Sub()
                                                                                                                             ViewContext.Writer.Write("<div id='divGdvPantallas'>")
                                                                                                                             Html.RenderPartial("PartialViewGridPantallas")
                                                                                                                             ViewContext.Writer.Write("</div>")
                                                                                                                         End Sub)
                                                                                               End Sub)
                                                                           End Sub)
                                          End Sub).GetHtml()%>

    </div>


   <div>
            <%Html.DevExpress.PopupControl(Sub(popup)
                                                 popup.Name = "MdlNuevoModulo"
                                                 popup.Width = 380
                                                 popup.AllowDragging = True
                                                 popup.CloseAction = CloseAction.CloseButton
                                                 popup.CloseOnEscape = True
                                                 popup.PopupAnimationType = AnimationType.Fade
                                                 popup.CloseAnimationType = AnimationType.Fade
                                                 popup.HeaderText = "Nuevo Módulo"
                                                 popup.Modal = True
                                                 popup.AutoUpdatePosition = True
                                                 popup.PopupHorizontalAlign = PopupHorizontalAlign.WindowCenter
                                                 popup.PopupVerticalAlign = PopupVerticalAlign.WindowCenter
                                                 popup.SetContent(Sub()
                                                                      Html.DevExpress.FormLayout(Sub(frm)
                                                                                                     frm.Name = "frmNuevoModulo"
                                                                                                     frm.ColCount = 1
                                                                                                     frm.SettingsItemCaptions.VerticalAlign = FormLayoutVerticalAlign.Middle
                                                                                                     frm.Items.Add(Sub(item)
                                                                                                                       item.Name = "txtModulo"
                                                                                                                       item.Caption = "Módulo"
                                                                                                                       item.RequiredMarkDisplayMode = FieldRequiredMarkMode.Required
                                                                                                                       item.NestedExtensionType = FormLayoutNestedExtensionItemType.TextBox
                                                                                                                       item.NestedExtensionSettings.Width = 380
                                                                                                                   End Sub)
                                                                                                     frm.Items.Add(Sub(item)
                                                                                                                       item.Name = "txtIcono"
                                                                                                                       item.Caption = "Icono del módulo"
                                                                                                                       item.NestedExtensionType = FormLayoutNestedExtensionItemType.TextBox
                                                                                                                       item.NestedExtensionSettings.Width = 380
                                                                                                                   End Sub)
                                                                                                     frm.Items.Add(Sub(item)
                                                                                                                       item.Caption = "Habilitado"
                                                                                                                       item.SetNestedContent(Sub()
                                                                                                                                                 Html.DevExpress.CheckBox(Sub(chk)
                                                                                                                                                                              chk.Name = "chkhabilitarModulo"
                                                                                                                                                                              chk.Checked = True
                                                                                                                                                                          End Sub).GetHtml()
                                                                                                                                             End Sub)
                                                                                                                   End Sub)
                                                                                                     frm.Items.Add(Sub(item)
                                                                                                                       item.ShowCaption = DefaultBoolean.False
                                                                                                                       item.HorizontalAlign = FormLayoutHorizontalAlign.Center
                                                                                                                       item.NestedExtensionSettings.Width = 380
                                                                                                                       item.SetNestedContent(Sub()
                                                                                                                                                 Html.DevExpress.Button(Sub(btn)
                                                                                                                                                                            btn.Name = "btnAgregarModulo"
                                                                                                                                                                            btn.Text = "Guardar"
                                                                                                                                                                            btn.ClientSideEvents.Click = "btnAgregarModuloClick"
                                                                                                                                                                            btn.Width = 100
                                                                                                                                                                        End Sub).GetHtml()
                                                                                                                                                 ViewContext.Writer.Write("&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp")
                                                                                                                                                 Html.DevExpress.Button(Sub(btn)
                                                                                                                                                                            btn.Name = "btnSalirModulo"
                                                                                                                                                                            btn.Text = "Salir"
                                                                                                                                                                            btn.ClientSideEvents.Click = "btnSalirModuloClick"
                                                                                                                                                                            btn.Width = 100
                                                                                                                                                                        End Sub).GetHtml()
                                                                                                                                             End Sub)
                                                                                                                   End Sub)
                                                                                                 End Sub).GetHtml()
                                                                  End Sub)
                                             End Sub).GetHtml()%>
    </div>

     <div>
            <%Html.DevExpress.PopupControl(Sub(popup)
                                                 popup.Name = "MdlNuevaOpcion"
                                                 popup.Width = 380
                                                 popup.AllowDragging = True
                                                 popup.CloseAction = CloseAction.CloseButton
                                                 popup.CloseOnEscape = True
                                                 popup.PopupAnimationType = AnimationType.Fade
                                                 popup.CloseAnimationType = AnimationType.Fade
                                                 popup.HeaderText = "Nueva Opción"
                                                 popup.Modal = True
                                                 popup.AutoUpdatePosition = True
                                                 popup.PopupHorizontalAlign = PopupHorizontalAlign.WindowCenter
                                                 popup.PopupVerticalAlign = PopupVerticalAlign.WindowCenter
                                                 popup.SetContent(Sub()
                                                                      Html.DevExpress.FormLayout(Sub(frm)
                                                                                                     frm.Name = "frmNuevaOpcion"
                                                                                                     frm.ColCount = 1
                                                                                                     frm.SettingsItemCaptions.VerticalAlign = FormLayoutVerticalAlign.Middle
                                                                                                     frm.Width = 380
                                                                                                     frm.Items.Add(Sub(item)
                                                                                                                       item.Name = "txtOpcion"
                                                                                                                       item.Caption = "Opción"
                                                                                                                       item.RequiredMarkDisplayMode = FieldRequiredMarkMode.Required
                                                                                                                       item.NestedExtensionType = FormLayoutNestedExtensionItemType.TextBox
                                                                                                                       item.NestedExtensionSettings.Width = 380
                                                                                                                   End Sub)
                                                                                                     frm.Items.Add(Sub(item)
                                                                                                                       item.Caption = "Habilitado"
                                                                                                                       item.SetNestedContent(Sub()
                                                                                                                                                 Html.DevExpress.CheckBox(Sub(chk)
                                                                                                                                                                              chk.Name = "chkhabilitarOpcion"
                                                                                                                                                                              chk.Checked = True
                                                                                                                                                                          End Sub).GetHtml()
                                                                                                                                             End Sub)
                                                                                                                   End Sub)
                                                                                                     frm.Items.Add(Sub(item)
                                                                                                                       item.ShowCaption = DefaultBoolean.False
                                                                                                                       item.HorizontalAlign = FormLayoutHorizontalAlign.Center
                                                                                                                       item.NestedExtensionSettings.Width = 380
                                                                                                                       item.SetNestedContent(Sub()
                                                                                                                                                 Html.DevExpress.Button(Sub(btn)
                                                                                                                                                                            btn.Name = "btnAgregarOpcion"
                                                                                                                                                                            btn.Text = "Guardar"
                                                                                                                                                                            btn.ClientSideEvents.Click = "btnAgregarOpcionClick"
                                                                                                                                                                            btn.Width = 100
                                                                                                                                                                        End Sub).GetHtml()
                                                                                                                                                 ViewContext.Writer.Write("&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp")
                                                                                                                                                 Html.DevExpress.Button(Sub(btn)
                                                                                                                                                                            btn.Name = "btnSalirOpcion"
                                                                                                                                                                            btn.Text = "Salir"
                                                                                                                                                                            btn.ClientSideEvents.Click = "btnSalirOpcionClick"
                                                                                                                                                                            btn.Width = 100
                                                                                                                                                                        End Sub).GetHtml()
                                                                                                                                             End Sub)
                                                                                                                   End Sub)
                                                                                                 End Sub).GetHtml()
                                                                  End Sub)
                                             End Sub).GetHtml()%>
    </div>
    <div>
            <%Html.DevExpress.PopupControl(Sub(popup)
                                                 popup.Name = "MdlNuevaPantalla"
                                                 popup.Width = 380
                                                 popup.AllowDragging = True
                                                 popup.CloseAction = CloseAction.CloseButton
                                                 popup.CloseOnEscape = True
                                                 popup.PopupAnimationType = AnimationType.Fade
                                                 popup.CloseAnimationType = AnimationType.Fade
                                                 popup.HeaderText = "Nueva Pantalla"
                                                 popup.Modal = True
                                                 popup.AutoUpdatePosition = True
                                                 popup.PopupHorizontalAlign = PopupHorizontalAlign.WindowCenter
                                                 popup.PopupVerticalAlign = PopupVerticalAlign.WindowCenter
                                                 popup.SetContent(Sub()
                                                                      Html.DevExpress.FormLayout(Sub(frm)
                                                                                                     frm.Name = "frmNuevaPantalla"
                                                                                                     frm.ColCount = 1
                                                                                                     frm.SettingsItemCaptions.VerticalAlign = FormLayoutVerticalAlign.Middle
                                                                                                     '    frm.SettingsItemHelpTexts.VerticalAlign = HelpTextVerticalAlign.Top
                                                                                                     frm.Items.Add(Sub(item)
                                                                                                                       item.Name = "txtPantalla"
                                                                                                                       item.Caption = "Pantalla"
                                                                                                                       item.NestedExtensionType = FormLayoutNestedExtensionItemType.TextBox
                                                                                                                       item.RequiredMarkDisplayMode = FieldRequiredMarkMode.Required
                                                                                                                       item.NestedExtensionSettings.Width = 380
                                                                                                                   End Sub)
                                                                                                     frm.Items.Add(Sub(item)
                                                                                                                       item.Name = "txtUrlPantalla"
                                                                                                                       item.Caption = "Dirección Url de pantalla"
                                                                                                                       item.HelpText = "Ejemplo: /NombreModulo/Controlador/NombreVista(View)"
                                                                                                                       item.RequiredMarkDisplayMode = FieldRequiredMarkMode.Required
                                                                                                                       item.NestedExtensionType = FormLayoutNestedExtensionItemType.TextBox
                                                                                                                       item.CaptionSettings.VerticalAlign = FormLayoutVerticalAlign.Top
                                                                                                                       item.NestedExtensionSettings.Width = 380
                                                                                                                   End Sub)
                                                                                                     frm.Items.Add(Sub(item)
                                                                                                                       item.Caption = "Habilitado"
                                                                                                                       item.NestedExtensionSettings.Width = 380
                                                                                                                       item.SetNestedContent(
                                                                                                                       Sub()
                                                                                                                           Html.DevExpress.CheckBox(
                                                                                                                           Sub(chk)
                                                                                                                               chk.Name = "chkhabilitarPantalla"
                                                                                                                               chk.Checked = True
                                                                                                                           End Sub).GetHtml()
                                                                                                                       End Sub)
                                                                                                                   End Sub)
                                                                                                     frm.Items.Add(Sub(item)
                                                                                                                       item.ShowCaption = DefaultBoolean.False
                                                                                                                       item.HorizontalAlign = FormLayoutHorizontalAlign.Center
                                                                                                                       item.NestedExtensionSettings.Width = 380
                                                                                                                       item.SetNestedContent(Sub()
                                                                                                                                                 Html.DevExpress.Button(Sub(btn)
                                                                                                                                                                            btn.Name = "btnAgregarPantalla"
                                                                                                                                                                            btn.Text = "Guardar"
                                                                                                                                                                            btn.ClientSideEvents.Click = "btnAgregarPantallaClick"
                                                                                                                                                                            btn.Width = 100
                                                                                                                                                                        End Sub).GetHtml()
                                                                                                                                                 ViewContext.Writer.Write("&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp")
                                                                                                                                                 Html.DevExpress.Button(Sub(btn)
                                                                                                                                                                            btn.Name = "btnSalirPantalla"
                                                                                                                                                                            btn.Text = "Salir"
                                                                                                                                                                            btn.ClientSideEvents.Click = "btnSalirPantallaClick"
                                                                                                                                                                            btn.Width = 100
                                                                                                                                                                        End Sub).GetHtml()
                                                                                                                                             End Sub)
                                                                                                                   End Sub)
                                                                                                 End Sub).GetHtml()
                                                                  End Sub)
                                             End Sub).GetHtml()%>
    </div>
    
    <div>
        <%Html.DevExpress.PopupControl(Sub(popup)
                                             popup.Name = "MdlEditarModulo"
                                             popup.Width = 380
                                             popup.AllowDragging = True
                                             popup.CloseAction = CloseAction.CloseButton
                                             popup.CloseOnEscape = True
                                             popup.PopupAnimationType = AnimationType.Fade
                                             popup.CloseAnimationType = AnimationType.Fade
                                             popup.HeaderText = "Configuración del Módulo"
                                             popup.Modal = True
                                             popup.AutoUpdatePosition = True
                                             popup.PopupHorizontalAlign = PopupHorizontalAlign.WindowCenter
                                             popup.PopupVerticalAlign = PopupVerticalAlign.WindowCenter
                                             popup.SetContent(Sub()
                                                                  Html.DevExpress.FormLayout(Sub(frm)
                                                                                                 frm.Name = "frmEditModulo"
                                                                                                 frm.ColCount = 1
                                                                                                 frm.SettingsItemCaptions.VerticalAlign = FormLayoutVerticalAlign.Middle
                                                                                                 frm.Items.Add(Sub(item)
                                                                                                                   item.Name = "txtEditModulo"
                                                                                                                   item.Caption = "Módulo"
                                                                                                                   item.NestedExtensionType = FormLayoutNestedExtensionItemType.TextBox
                                                                                                                   item.RequiredMarkDisplayMode = FieldRequiredMarkMode.Required
                                                                                                                   item.NestedExtensionSettings.Width = 380
                                                                                                               End Sub)
                                                                                                 frm.Items.Add(Sub(item)
                                                                                                                   item.Name = "txtEditIcono"
                                                                                                                   item.Caption = "Icono"
                                                                                                                   item.NestedExtensionType = FormLayoutNestedExtensionItemType.TextBox
                                                                                                                   item.RequiredMarkDisplayMode = FieldRequiredMarkMode.Required
                                                                                                                   item.NestedExtensionSettings.Width = 380
                                                                                                               End Sub)
                                                                                                 frm.Items.Add(Sub(item)
                                                                                                                   item.Caption = "URL"
                                                                                                                   item.NestedExtensionSettings.Width = 380
                                                                                                                   item.SetNestedContent(Sub()
                                                                                                                                             Html.DevExpress.TextBox(Sub(txt)
                                                                                                                                                                         txt.Name = "txtEditModuloURL"
                                                                                                                                                                         txt.ReadOnly = True
                                                                                                                                                                         txt.Width = 380
                                                                                                                                                                     End Sub).GetHtml()
                                                                                                                                         End Sub)
                                                                                                               End Sub)
                                                                                                 frm.Items.Add(Sub(item)
                                                                                                                   item.Caption = "Habilitado"
                                                                                                                   item.NestedExtensionSettings.Width = 380

                                                                                                                   item.SetNestedContent(Sub()
                                                                                                                                             Html.DevExpress.CheckBox(Sub(chk)
                                                                                                                                                                          chk.Name = "chkEdithabilitarModulo"
                                                                                                                                                                          chk.Checked = True
                                                                                                                                                                      End Sub).GetHtml()
                                                                                                                                         End Sub)
                                                                                                               End Sub)
                                                                                                 frm.Items.Add(Sub(item)
                                                                                                                   item.ShowCaption = DefaultBoolean.False
                                                                                                                   item.HorizontalAlign = FormLayoutHorizontalAlign.Center
                                                                                                                   item.NestedExtensionSettings.Width = 380
                                                                                                                   item.SetNestedContent(Sub()
                                                                                                                                             Html.DevExpress.Button(Sub(btn)
                                                                                                                                                                        btn.Name = "btnActualizarModulo"
                                                                                                                                                                        btn.Text = "Aplicar Cambios"
                                                                                                                                                                        btn.ClientSideEvents.Click = "btnActualizarModuloClick"
                                                                                                                                                                        btn.Width = 100
                                                                                                                                                                        ' btn.Styles.Style.Paddings.PaddingRight = 50
                                                                                                                                                                    End Sub).GetHtml()
                                                                                                                                             ViewContext.Writer.Write("&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp")
                                                                                                                                             Html.DevExpress.Button(Sub(btn)
                                                                                                                                                                        btn.Name = "btnSalirEditarModulo"
                                                                                                                                                                        btn.Text = "Salir"
                                                                                                                                                                        btn.ClientSideEvents.Click = "btnSalirEditarModuloClick"
                                                                                                                                                                        btn.Width = 100
                                                                                                                                                                    End Sub).GetHtml()
                                                                                                                                         End Sub)
                                                                                                               End Sub)
                                                                                             End Sub).GetHtml()
                                                              End Sub)
                                         End Sub).GetHtml() %>
    </div>

    <div>
        <%Html.DevExpress.PopupControl(Sub(popup)
                                             popup.Name = "MdlEditarOpcion"
                                             popup.Width = 380
                                             popup.AllowDragging = True
                                             popup.CloseAction = CloseAction.CloseButton
                                             popup.CloseOnEscape = True
                                             popup.PopupAnimationType = AnimationType.Fade
                                             popup.CloseAnimationType = AnimationType.Fade
                                             popup.HeaderText = "Configuración de Opciónes"
                                             popup.Modal = True
                                             popup.AutoUpdatePosition = True
                                             popup.PopupHorizontalAlign = PopupHorizontalAlign.WindowCenter
                                             popup.PopupVerticalAlign = PopupVerticalAlign.WindowCenter
                                             popup.SetContent(Sub()
                                                                  Html.DevExpress.FormLayout(Sub(frm)
                                                                                                 frm.Name = "frmEditOpcion"
                                                                                                 frm.ColCount = 1
                                                                                                 frm.SettingsItemCaptions.VerticalAlign = FormLayoutVerticalAlign.Middle
                                                                                                 frm.Items.Add(Sub(item)
                                                                                                                   item.Name = "txtEditOpcion"
                                                                                                                   item.Caption = "Módulo"
                                                                                                                   item.NestedExtensionType = FormLayoutNestedExtensionItemType.TextBox
                                                                                                                   item.RequiredMarkDisplayMode = FieldRequiredMarkMode.Required
                                                                                                                   item.NestedExtensionSettings.Width = 380
                                                                                                               End Sub)
                                                                                                 frm.Items.Add(Sub(item)
                                                                                                                   item.Caption = "Habilitado"
                                                                                                                   item.NestedExtensionSettings.Width = 380
                                                                                                                   item.SetNestedContent(Sub()
                                                                                                                                             Html.DevExpress.CheckBox(Sub(chk)
                                                                                                                                                                          chk.Name = "chkEdithabilitarOpcion"
                                                                                                                                                                          chk.Checked = True
                                                                                                                                                                      End Sub).GetHtml()
                                                                                                                                         End Sub)
                                                                                                               End Sub)
                                                                                                 frm.Items.Add(Sub(item)
                                                                                                                   item.ShowCaption = DefaultBoolean.False
                                                                                                                   item.HorizontalAlign = FormLayoutHorizontalAlign.Center
                                                                                                                   item.NestedExtensionSettings.Width = 380
                                                                                                                   item.SetNestedContent(Sub()
                                                                                                                                             Html.DevExpress.Button(Sub(btn)
                                                                                                                                                                        btn.Name = "btnActualizarOpcion"
                                                                                                                                                                        btn.Text = "Aplicar Cambios"

                                                                                                                                                                        btn.ClientSideEvents.Click = "btnActualizarOpcionClick"
                                                                                                                                                                        btn.Width = 100
                                                                                                                                                                        ' btn.Styles.Style.Paddings.PaddingRight = 50
                                                                                                                                                                    End Sub).GetHtml()
                                                                                                                                             ViewContext.Writer.Write("&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp")
                                                                                                                                             Html.DevExpress.Button(Sub(btn)
                                                                                                                                                                        btn.Name = "btnSalirEditarOpcion"
                                                                                                                                                                        btn.Text = "Salir"
                                                                                                                                                                        btn.ClientSideEvents.Click = "btnSalirEditarOpcionClick"
                                                                                                                                                                        btn.Width = 100
                                                                                                                                                                    End Sub).GetHtml()
                                                                                                                                         End Sub)
                                                                                                               End Sub)
                                                                                             End Sub).GetHtml()
                                                              End Sub)
                                         End Sub).GetHtml() %>
    </div>

     <div>
        <%Html.DevExpress.PopupControl(Sub(popup)
                                             popup.Name = "MdlEditarPantalla"
                                             popup.Width = 380
                                             popup.AllowDragging = True
                                             popup.CloseAction = CloseAction.CloseButton
                                             popup.CloseOnEscape = True
                                             popup.PopupAnimationType = AnimationType.Fade
                                             popup.CloseAnimationType = AnimationType.Fade
                                             popup.HeaderText = "Configuración de Pantalla"
                                             popup.Modal = True
                                             popup.AutoUpdatePosition = True
                                             popup.PopupHorizontalAlign = PopupHorizontalAlign.WindowCenter
                                             popup.PopupVerticalAlign = PopupVerticalAlign.WindowCenter
                                             popup.SetContent(Sub()
                                                                  Html.DevExpress.FormLayout(Sub(frm)
                                                                                                 frm.Name = "frmEditPantalla"
                                                                                                 frm.ColCount = 1
                                                                                                 frm.SettingsItemCaptions.VerticalAlign = FormLayoutVerticalAlign.Middle
                                                                                                 frm.Items.Add(Sub(item)
                                                                                                                   item.Name = "txtEditPantalla"
                                                                                                                   item.Caption = "Módulo"
                                                                                                                   item.NestedExtensionType = FormLayoutNestedExtensionItemType.TextBox
                                                                                                                   item.RequiredMarkDisplayMode = FieldRequiredMarkMode.Required
                                                                                                                   item.NestedExtensionSettings.Width = 380
                                                                                                               End Sub)
                                                                                                 frm.Items.Add(Sub(item)
                                                                                                                   item.Caption = "URL"
                                                                                                                   item.SetNestedContent(Sub()
                                                                                                                                             Html.DevExpress.TextBox(Sub(txt)
                                                                                                                                                                         txt.Name = "txtEditPantallaURL"
                                                                                                                                                                         txt.Width = 380
                                                                                                                                                                     End Sub).GetHtml()
                                                                                                                                         End Sub)
                                                                                                               End Sub)
                                                                                                 frm.Items.Add(Sub(item)
                                                                                                                   item.Caption = "Habilitado"
                                                                                                                   item.NestedExtensionSettings.Width = 380
                                                                                                                   item.SetNestedContent(Sub()
                                                                                                                                             Html.DevExpress.CheckBox(Sub(chk)
                                                                                                                                                                          chk.Name = "chkEdithabilitarPantalla"

                                                                                                                                                                          chk.Checked = True
                                                                                                                                                                      End Sub).GetHtml()
                                                                                                                                         End Sub)
                                                                                                               End Sub)
                                                                                                 frm.Items.Add(Sub(item)
                                                                                                                   item.ShowCaption = DefaultBoolean.False
                                                                                                                   item.HorizontalAlign = FormLayoutHorizontalAlign.Center
                                                                                                                   item.NestedExtensionSettings.Width = 380
                                                                                                                   item.SetNestedContent(Sub()
                                                                                                                                             Html.DevExpress.Button(Sub(btn)
                                                                                                                                                                        btn.Name = "btnActualizarPantalla"
                                                                                                                                                                        btn.Text = "Aplicar Cambios"
                                                                                                                                                                        btn.ClientSideEvents.Click = "btnActualizarPantallaClick"
                                                                                                                                                                        btn.Width = 100
                                                                                                                                                                    End Sub).GetHtml()
                                                                                                                                             ViewContext.Writer.Write("&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp")
                                                                                                                                             Html.DevExpress.Button(Sub(btn)
                                                                                                                                                                        btn.Name = "btnSalirEditarPantalla"
                                                                                                                                                                        btn.Text = "Salir"
                                                                                                                                                                        btn.ClientSideEvents.Click = "btnSalirEditarPantallaClick"
                                                                                                                                                                        btn.Width = 100
                                                                                                                                                                    End Sub).GetHtml()
                                                                                                                                         End Sub)
                                                                                                               End Sub)
                                                                                             End Sub).GetHtml()
                                                              End Sub)
                                         End Sub).GetHtml()%>
    </div>

    <div>

    </div>
</asp:Content>
