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

<style type="text/css">
    .fixStyles {
        font: normal normal normal 25px FontAwesome;
        color: white ;
       
    }
        .fixStyles span {
            display: none;
        }
</style>
    
<script type="text/javascript" src='/Areas/Seguridad/Scripts/rol.js'></script>

    <div align="center">
        <h2>ADMINISTRACIÓN DE ROLES</h2>
         <%Html.DevExpress().FormLayout(Sub(frmLayout)
                                              frmLayout.Name = "frmLaout"
                                              frmLayout.Width = Unit.Percentage(100)
                                              frmLayout.ColCount = 1
                                              frmLayout.Items.AddGroupItem(Sub(group)
                                                                               group.Caption = "Roles"
                                                                               group.Width = Unit.Percentage(100)
                                                                               group.SettingsItemCaptions.VerticalAlign = FormLayoutVerticalAlign.Middle
                                                                               group.SettingsItemCaptions.HorizontalAlign = FormLayoutHorizontalAlign.Center
                                                                               group.GroupBoxDecoration = GroupBoxDecoration.HeadingLine
                                                                               group.Items.Add(Sub(item)
                                                                                                   item.ShowCaption = DefaultBoolean.False
                                                                                                   item.SetNestedContent(Sub()
                                                                                                                             Html.DevExpress.Button(Sub(btn)
                                                                                                                                                        btn.Name = "btnNuevoRol"
                                                                                                                                                        btn.Text = "+  Nuevo Rol"
                                                                                                                                                        btn.Width = Unit.Percentage(20)
                                                                                                                                                        btn.ClientSideEvents.Click = "btnNuevoRolClick"
                                                                                                                                                    End Sub).GetHtml()
                                                                                                                         End Sub)
                                                                                               End Sub)
                                                                               group.Items.Add(Sub(item)
                                                                                                   item.ShowCaption = DefaultBoolean.False
                                                                                                   item.SetNestedContent(Sub()
                                                                                                                             ViewContext.Writer.Write("<div id='divGdvRoles'>")
                                                                                                                             Html.RenderAction("PartialViewGridRoles")
                                                                                                                             ViewContext.Writer.Write("</div>")
                                                                                                                         End Sub)
                                                                                               End Sub)
                                                                           End Sub)
                                          End Sub).GetHtml()%>
    </div>
    <div>
        <%Html.DevExpress.PopupControl(Sub(popup)
                                             popup.Name = "MdlNuevoRol"
                                             popup.Width = 380
                                             popup.AllowDragging = True
                                             popup.CloseAction = CloseAction.CloseButton
                                             popup.CloseOnEscape = True
                                             popup.PopupAnimationType = AnimationType.Fade
                                             popup.CloseAnimationType = AnimationType.Fade
                                             popup.HeaderText = "Nuevo Rol"
                                             popup.Modal = True
                                             popup.AutoUpdatePosition = True
                                             popup.PopupHorizontalAlign = PopupHorizontalAlign.WindowCenter
                                             popup.PopupVerticalAlign = PopupVerticalAlign.WindowCenter
                                             popup.SetContent(Sub()
                                                                  Html.DevExpress.FormLayout(Sub(frmLayout)
                                                                                                 frmLayout.Name = "frmNuevoRol"
                                                                                                 frmLayout.Width = Unit.Percentage(50)
                                                                                                 frmLayout.ColCount = 1
                                                                                                 frmLayout.SettingsItemHelpTexts.VerticalAlign = HelpTextVerticalAlign.Middle
                                                                                                 frmLayout.SettingsItemCaptions.VerticalAlign = FormLayoutVerticalAlign.Middle
                                                                                                 frmLayout.Items.Add(Sub(item)
                                                                                                                         item.Name = "txtRol"
                                                                                                                         item.Caption = "Rol"
                                                                                                                         item.NestedExtensionType = FormLayoutNestedExtensionItemType.TextBox
                                                                                                                         item.RequiredMarkDisplayMode = FieldRequiredMarkMode.Required
                                                                                                                         item.NestedExtensionSettings.Width = 380
                                                                                                                     End Sub)
                                                                                                 frmLayout.Items.Add(Sub(item)
                                                                                                                         item.Caption = "Módulo"
                                                                                                                         item.NestedExtensionSettings.Width = 380
                                                                                                                         item.SetNestedContent(Sub()
                                                                                                                                                   Html.RenderAction("PartialViewCbNuevoModulo")
                                                                                                                                               End Sub)
                                                                                                                     End Sub)
                                                                                                 frmLayout.Items.Add(Sub(item)
                                                                                                                         item.Caption = "Habilitar"
                                                                                                                         item.NestedExtensionSettings.Width = 380
                                                                                                                         item.SetNestedContent(Sub()
                                                                                                                                                   Html.DevExpress.CheckBox(Sub(chk)
                                                                                                                                                                                chk.Name = "chkhabilitar"
                                                                                                                                                                                chk.Checked = True
                                                                                                                                                                            End Sub).GetHtml()
                                                                                                                                               End Sub)
                                                                                                                     End Sub)
                                                                                                 frmLayout.Items.Add(Sub(item)
                                                                                                                         item.ShowCaption = DefaultBoolean.False
                                                                                                                         item.HorizontalAlign = FormLayoutHorizontalAlign.Center
                                                                                                                         item.NestedExtensionSettings.Width = 380
                                                                                                                         item.SetNestedContent(Sub()
                                                                                                                                                   Html.DevExpress.Button(Sub(btn)
                                                                                                                                                                              btn.Name = "btnAgregarRol"
                                                                                                                                                                              btn.Text = "Guardar"
                                                                                                                                                                              btn.ClientSideEvents.Click = "btnAgregarRolClick"
                                                                                                                                                                              btn.Width = 100
                                                                                                                                                                          End Sub).GetHtml()
                                                                                                                                                   ViewContext.Writer.Write("&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp")
                                                                                                                                                   Html.DevExpress.Button(Sub(btn)
                                                                                                                                                                              btn.Name = "btnSalirRol"
                                                                                                                                                                              btn.Text = "Salir"
                                                                                                                                                                              btn.Width = 100
                                                                                                                                                                              btn.ClientSideEvents.Click = "btnSalirClick"
                                                                                                                                                                          End Sub).GetHtml()
                                                                                                                                               End Sub)
                                                                                                                     End Sub)
                                                                                             End Sub).GetHtml()
                                                              End Sub)
                                         End Sub).GetHtml()%>
    </div>

    <div>
        <%Html.DevExpress.PopupControl(Sub(popup)
                                             popup.Name = "MdlEditarRol"
                                             popup.Width = 380
                                             popup.AllowDragging = True
                                             popup.CloseAction = CloseAction.CloseButton
                                             popup.CloseOnEscape = True
                                             popup.PopupAnimationType = AnimationType.Fade
                                             popup.CloseAnimationType = AnimationType.Fade
                                             popup.HeaderText = "Editar Rol"
                                             popup.Modal = True
                                             popup.AutoUpdatePosition = True
                                             popup.PopupHorizontalAlign = PopupHorizontalAlign.WindowCenter
                                             popup.PopupVerticalAlign = PopupVerticalAlign.WindowCenter
                                             popup.SetContent(Sub()
                                                                  Html.DevExpress.FormLayout(Sub(frmLayout)
                                                                                                 frmLayout.Name = "frmEditarRol"
                                                                                                 frmLayout.Width = Unit.Percentage(50)
                                                                                                 frmLayout.ColCount = 1
                                                                                                 frmLayout.SettingsItemHelpTexts.VerticalAlign = HelpTextVerticalAlign.Middle
                                                                                                 frmLayout.SettingsItemCaptions.VerticalAlign = FormLayoutVerticalAlign.Middle
                                                                                                 frmLayout.Items.Add(Sub(item)
                                                                                                                         item.Name = "txtEditarRol"
                                                                                                                         item.Caption = "Rol"
                                                                                                                         item.NestedExtensionType = FormLayoutNestedExtensionItemType.TextBox
                                                                                                                         item.RequiredMarkDisplayMode = FieldRequiredMarkMode.Required
                                                                                                                         item.NestedExtensionSettings.Width = 380
                                                                                                                     End Sub)
                                                                                                 frmLayout.Items.Add(Sub(item)
                                                                                                                         item.Caption = "Módulo"
                                                                                                                         item.NestedExtensionSettings.Width = 380
                                                                                                                         item.SetNestedContent(Sub()
                                                                                                                                                   Html.RenderAction("PartialViewCbEditarModulo")
                                                                                                                                               End Sub)
                                                                                                                     End Sub)
                                                                                                 frmLayout.Items.Add(Sub(item)
                                                                                                                         item.Caption = "Habilitar"
                                                                                                                         item.NestedExtensionSettings.Width = 380
                                                                                                                         item.SetNestedContent(Sub()
                                                                                                                                                   Html.DevExpress.CheckBox(Sub(chk)
                                                                                                                                                                                chk.Name = "chkEditarhabilitar"
                                                                                                                                                                                chk.Checked = True
                                                                                                                                                                            End Sub).GetHtml()
                                                                                                                                               End Sub)
                                                                                                                     End Sub)
                                                                                                 frmLayout.Items.Add(Sub(item)
                                                                                                                         item.ShowCaption = DefaultBoolean.False
                                                                                                                         item.HorizontalAlign = FormLayoutHorizontalAlign.Center
                                                                                                                         item.NestedExtensionSettings.Width = 380
                                                                                                                         item.SetNestedContent(Sub()
                                                                                                                                                   Html.DevExpress.Button(Sub(btn)
                                                                                                                                                                              btn.Name = "btnEditarRol"
                                                                                                                                                                              btn.Text = "Aplicar Cambios"
                                                                                                                                                                              btn.ClientSideEvents.Click = "btnEditarRolClick"
                                                                                                                                                                              btn.Width = 100
                                                                                                                                                                          End Sub).GetHtml()
                                                                                                                                                   ViewContext.Writer.Write("&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp")
                                                                                                                                                   Html.DevExpress.Button(Sub(btn)
                                                                                                                                                                              btn.Name = "btnSalirEditarRol"
                                                                                                                                                                              btn.Text = "Salir"
                                                                                                                                                                              btn.ClientSideEvents.Click = "btnSalirEditarClick"
                                                                                                                                                                              btn.Width = 100
                                                                                                                                                                          End Sub).GetHtml()
                                                                                                                                               End Sub)
                                                                                                                     End Sub)
                                                                                             End Sub).GetHtml()
                                                              End Sub)
                                         End Sub).GetHtml()%>
    </div>
</asp:Content>
