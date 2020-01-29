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
        <h2>DEPARTAMENTOS</h2>
        <%Html.DevExpress.FormLayout(Sub(frm)
                                           frm.Name = "frm"
                                           frm.Width = Unit.Percentage(100)
                                           frm.ColCount = 1
                                           frm.Items.AddGroupItem(Sub(group)
                                                                      group.Caption = "Departamentos"
                                                                      group.Width = Unit.Percentage(100)
                                                                      group.SettingsItemCaptions.VerticalAlign = FormLayoutVerticalAlign.Middle
                                                                      group.SettingsItemCaptions.HorizontalAlign = FormLayoutHorizontalAlign.Center
                                                                      group.GroupBoxDecoration = GroupBoxDecoration.HeadingLine
                                                                      group.Items.Add(Sub(item)
                                                                                          item.ShowCaption = DefaultBoolean.False
                                                                                          item.SetNestedContent(Sub()
                                                                                                                    Html.DevExpress.Button(Sub(btn)
                                                                                                                                               btn.Name = "btnNuevoDptm"
                                                                                                                                               btn.Text = "+  Nuevo Departamento"
                                                                                                                                               btn.Width = 100
                                                                                                                                               btn.ClientSideEvents.Click = "function (s, e) { { MdlNuevoDepartamento.Show();} }"
                                                                                                                                           End Sub).GetHtml()
                                                                                                                End Sub)
                                                                                      End Sub)
                                                                      group.Items.Add(Sub(item)
                                                                                          item.ShowCaption = DefaultBoolean.False
                                                                                          item.SetNestedContent(Sub()
                                                                                                                    ViewContext.Writer.Write("<div id='divGdvDepartamentos'>")
                                                                                                                    Html.RenderAction("PVGDepartamentos")
                                                                                                                    ViewContext.Writer.Write("</div>")
                                                                                                                End Sub)
                                                                                      End Sub)
                                                                  End Sub)

                                       End Sub).GetHtml() %>
    </div>
    <div>
        <%Html.DevExpress.PopupControl(Sub(popup)
                                             popup.Name = "MdlNuevoDepartamento"

                                             popup.Width = 380
                                             popup.AllowDragging = True
                                             popup.CloseAction = CloseAction.CloseButton
                                             popup.CloseOnEscape = True
                                             popup.PopupAnimationType = AnimationType.Fade
                                             popup.CloseAnimationType = AnimationType.Fade
                                             popup.HeaderText = "Nuevo Departamento"
                                             popup.Modal = True
                                             popup.AutoUpdatePosition = True
                                             popup.PopupHorizontalAlign = PopupHorizontalAlign.WindowCenter
                                             popup.PopupVerticalAlign = PopupVerticalAlign.WindowCenter
                                             popup.SetContent(Sub()
                                                                  Html.DevExpress.FormLayout(Sub(frm)
                                                                                                 frm.Name = "frmNuevoDep"

                                                                                                 frm.ColCount = 1
                                                                                                 frm.SettingsItemCaptions.VerticalAlign = FormLayoutVerticalAlign.Middle
                                                                                                 frm.Items.Add(Sub(item)
                                                                                                                   item.Name = "txtDepartamento"
                                                                                                                   item.Caption = "Departamento"
                                                                                                                   item.NestedExtensionType = FormLayoutNestedExtensionItemType.TextBox
                                                                                                                   item.NestedExtensionSettings.Width = 380
                                                                                                               End Sub)
                                                                                                 frm.Items.Add(Sub(item)
                                                                                                                   item.Caption = "Habilitado"
                                                                                                                   item.NestedExtensionSettings.Width = 380
                                                                                                                   item.SetNestedContent(Sub()
                                                                                                                                             Html.DevExpress.CheckBox(Sub(chk)
                                                                                                                                                                          chk.Name = "cbHabilitado"

                                                                                                                                                                          chk.Checked = True
                                                                                                                                                                      End Sub).GetHtml()
                                                                                                                                         End Sub)
                                                                                                               End Sub)
                                                                                                 frm.Items.Add(Sub(item)
                                                                                                                   item.HorizontalAlign = FormLayoutHorizontalAlign.Center
                                                                                                                   item.ShowCaption = DefaultBoolean.False
                                                                                                                   item.NestedExtensionSettings.Width = 380
                                                                                                                   item.SetNestedContent(Sub()
                                                                                                                                             Html.DevExpress().Button(Sub(btn)
                                                                                                                                                                          btn.Name = "btnAgregardepto"

                                                                                                                                                                          btn.Text = "Agregar"
                                                                                                                                                                          btn.ClientSideEvents.Click = "btnAgregardeptoClick"
                                                                                                                                                                          btn.Width = 100
                                                                                                                                                                      End Sub).GetHtml()
                                                                                                                                             ViewContext.Writer.Write("&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp")
                                                                                                                                             Html.DevExpress().Button(Sub(btn)
                                                                                                                                                                          btn.Name = "btnSalir"

                                                                                                                                                                          btn.Text = "Salir"
                                                                                                                                                                          btn.Width = 100
                                                                                                                                                                          btn.ClientSideEvents.Click = "function (s, e) { { MdlNuevoDepartamento.Hide();} }"
                                                                                                                                                                      End Sub).GetHtml()
                                                                                                                                         End Sub)
                                                                                                               End Sub)

                                                                                             End Sub).GetHtml()
                                                              End Sub)
                                         End Sub).GetHtml()%>
    </div>
     
     <div>
        <%Html.DevExpress.PopupControl(Sub(popup)
                                             popup.Name = "MdlEditarDepartamento"

                                             popup.Width = 380
                                             popup.AllowDragging = True
                                             popup.CloseAction = CloseAction.CloseButton
                                             popup.CloseOnEscape = True
                                             popup.PopupAnimationType = AnimationType.Fade
                                             popup.CloseAnimationType = AnimationType.Fade
                                             popup.HeaderText = "Modificar Departamento"
                                             popup.Modal = True
                                             popup.AutoUpdatePosition = True
                                             popup.PopupHorizontalAlign = PopupHorizontalAlign.WindowCenter
                                             popup.PopupVerticalAlign = PopupVerticalAlign.WindowCenter
                                             popup.SetContent(Sub()
                                                                  Html.DevExpress.FormLayout(Sub(frm)
                                                                                                 frm.Name = "frmEditarDep"

                                                                                                 frm.ColCount = 1
                                                                                                 frm.SettingsItemCaptions.VerticalAlign = FormLayoutVerticalAlign.Middle
                                                                                                 frm.Items.Add(Sub(item)
                                                                                                                   item.Name = "txteditDepartamento"
                                                                                                                   item.Caption = "Departamento"
                                                                                                                   item.NestedExtensionType = FormLayoutNestedExtensionItemType.TextBox
                                                                                                                   item.NestedExtensionSettings.Width = 380
                                                                                                               End Sub)
                                                                                                 frm.Items.Add(Sub(item)
                                                                                                                   item.Caption = "Habilitado"
                                                                                                                   item.NestedExtensionSettings.Width = 380
                                                                                                                   item.SetNestedContent(Sub()
                                                                                                                                             Html.DevExpress.CheckBox(Sub(chk)
                                                                                                                                                                          chk.Name = "chkeditHabilitado"

                                                                                                                                                                          chk.Checked = True
                                                                                                                                                                      End Sub).GetHtml()
                                                                                                                                         End Sub)
                                                                                                               End Sub)
                                                                                                 frm.Items.Add(Sub(item)
                                                                                                                   item.HorizontalAlign = FormLayoutHorizontalAlign.Center
                                                                                                                   item.ShowCaption = DefaultBoolean.False
                                                                                                                   item.NestedExtensionSettings.Width = 380
                                                                                                                   item.SetNestedContent(Sub()
                                                                                                                                             Html.DevExpress().Button(Sub(btn)
                                                                                                                                                                          btn.Name = "btnActualizardepto"

                                                                                                                                                                          btn.Text = "Aplicar Cambios"
                                                                                                                                                                          btn.ClientSideEvents.Click = "btnActualizardeptoClick"
                                                                                                                                                                          btn.Width = 100
                                                                                                                                                                      End Sub).GetHtml()
                                                                                                                                             ViewContext.Writer.Write("&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp")
                                                                                                                                             Html.DevExpress().Button(Sub(btn)
                                                                                                                                                                          btn.Name = "btnSalirEditar"

                                                                                                                                                                          btn.Text = "Salir"
                                                                                                                                                                          btn.Width = 100
                                                                                                                                                                          btn.ClientSideEvents.Click = "function (s, e) { { MdlEditarDepartamento.Hide();} }"
                                                                                                                                                                      End Sub).GetHtml()
                                                                                                                                         End Sub)
                                                                                                               End Sub)

                                                                                             End Sub).GetHtml()
                                                              End Sub)
                                         End Sub).GetHtml()%>
    </div>

</asp:Content>
