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

<script type="text/javascript" src='/Areas/Seguridad/Scripts/accesos.js'></script>

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
            
    <h2>CONTROL DE ACCESOS</h2>

            <%Html.DevExpress().FormLayout(Sub(frmLayout)
                                                 frmLayout.Name = "frmLaout"
                                                 frmLayout.Width = Unit.Percentage(100)
                                                 frmLayout.ColCount = 2
                                                 frmLayout.Items.AddGroupItem(
                                                         Sub(group)
                                                             group.Caption = "Rol"
                                                             group.Width = Unit.Percentage(50)
                                                             group.SettingsItemCaptions.VerticalAlign = FormLayoutVerticalAlign.Middle
                                                             group.SettingsItemCaptions.HorizontalAlign = FormLayoutHorizontalAlign.Center
                                                             group.GroupBoxDecoration = GroupBoxDecoration.HeadingLine
                                                             group.Items.Add(
                                                             Sub(item)
                                                                 item.ShowCaption = DefaultBoolean.False
                                                                 item.SetNestedContent(
                                                                  Sub()
                                                                      Html.RenderAction("PartialViewGridRolesAccesos")
                                                                  End Sub)
                                                             End Sub
                                                             )
                                                         End Sub)
                                                 frmLayout.Items.AddGroupItem(
                                                          Sub(group)
                                                              group.Caption = "Pantallas a las que tiene Acceso"
                                                              group.Width = Unit.Percentage(50)
                                                              group.SettingsItemCaptions.VerticalAlign = FormLayoutVerticalAlign.Middle
                                                              group.SettingsItemCaptions.HorizontalAlign = FormLayoutHorizontalAlign.Center
                                                              group.GroupBoxDecoration = GroupBoxDecoration.HeadingLine
                                                              group.Items.Add(
                                                              Sub(item)
                                                                  item.ShowCaption = DefaultBoolean.False

                                                                  item.SetNestedContent(
                                                                    Sub()
                                                                        Html.DevExpress.Button(
                                                                            Sub(btn)
                                                                                btn.Name = "btnNuevoAcceso"
                                                                                btn.Text = "+ Nuevo Acceso"
                                                                                btn.ClientEnabled = False
                                                                                btn.Width = Unit.Percentage(50)
                                                                                btn.ClientSideEvents.Click = "btnNuevoAccesoClick"
                                                                            End Sub).GetHtml()
                                                                    End Sub)

                                                              End Sub)
                                                              group.Items.Add(
                                                              Sub(item)
                                                                  item.ShowCaption = DefaultBoolean.False
                                                                  item.SetNestedContent(
                                                                   Sub()
                                                                       ViewContext.Writer.Write("<div id='divGdvPantallasAccesos'>")
                                                                       Html.RenderPartial("PartialViewGridPantallasAccesos")
                                                                       ViewContext.Writer.Write("</div>")
                                                                   End Sub)
                                                              End Sub)


                                                          End Sub)
                                             End Sub).GetHtml()%>
              <div>
                  <%Html.DevExpress.PopupControl(Sub(popup)
                                                       popup.Name = "popupNuevoAcceso"

                                                       popup.Width = 320
                                                       popup.AllowDragging = True
                                                       popup.CloseAction = CloseAction.CloseButton
                                                       popup.CloseOnEscape = True
                                                       popup.PopupAnimationType = AnimationType.Fade
                                                       popup.CloseAnimationType = AnimationType.Fade
                                                       popup.HeaderText = "Nuevo Acceso"
                                                       popup.Modal = True
                                                       popup.AutoUpdatePosition = True
                                                       popup.PopupHorizontalAlign = PopupHorizontalAlign.WindowCenter
                                                       popup.PopupVerticalAlign = PopupVerticalAlign.WindowCenter
                                                       popup.SetContent(Sub()
                                                                            Html.DevExpress.FormLayout(
                                                                            Sub(frm)
                                                                                frm.Name = "frmNuevoAcceso"

                                                                                frm.ColCount = 1
                                                                                frm.SettingsItemCaptions.VerticalAlign = FormLayoutVerticalAlign.Middle
                                                                                frm.Items.Add(Sub(item)
                                                                                                  item.Name = "txt"
                                                                                                  item.ShowCaption = DefaultBoolean.False
                                                                                                  item.SetNestedContent(Sub()
                                                                                                                            Html.DevExpress.Label(Sub(lbl)
                                                                                                                                                      lbl.Name = "lblmensaje"
                                                                                                                                                      lbl.Text = "Seleccione la pantalla que a la que tendrá acceso el rol según el módulo y opción correspondientes"
                                                                                                                                                  End Sub).GetHtml()
                                                                                                                        End Sub)
                                                                                              End Sub)
                                                                                frm.Items.Add(Sub(item)
                                                                                                  item.Caption = "Módulo"
                                                                                                  item.SetNestedContent(Sub()
                                                                                                                            Html.RenderAction("PartialViewCbModulos")
                                                                                                                        End Sub)
                                                                                              End Sub)
                                                                                frm.Items.Add(Sub(item)
                                                                                                  item.Caption = "Opción"
                                                                                                  item.SetNestedContent(Sub()
                                                                                                                            ViewContext.Writer.Write("<div id='divCbOpciones'>")
                                                                                                                            Html.RenderAction("PartialViewCbOpciones")
                                                                                                                            ViewContext.Writer.Write("</div>")
                                                                                                                        End Sub)

                                                                                              End Sub)
                                                                                frm.Items.Add(Sub(item)
                                                                                                  item.Caption = "Pantalla"
                                                                                                  item.SetNestedContent(Sub()
                                                                                                                            ViewContext.Writer.Write("<div id='divCbPantallas'>")
                                                                                                                            Html.RenderPartial("PartialViewCbPantallas")
                                                                                                                            ViewContext.Writer.Write("</div>")
                                                                                                                        End Sub)
                                                                                              End Sub)
                                                                                frm.Items.Add(Sub(item)
                                                                                                  item.ShowCaption = DefaultBoolean.False
                                                                                                  item.HorizontalAlign = FormLayoutHorizontalAlign.Center
                                                                                                  item.SetNestedContent(Sub()
                                                                                                                            Html.DevExpress.Button(
                                                                                                                                    Sub(btn)
                                                                                                                                        btn.Name = "btnAgregarAcceso"
                                                                                                                                        btn.Text = "Agregar"
                                                                                                                                        btn.Width = 100
                                                                                                                                        btn.ClientSideEvents.Click = "btnAgregarAccesoClick"
                                                                                                                                    End Sub).GetHtml()
                                                                                                                            ViewContext.Writer.Write("&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp")
                                                                                                                            Html.DevExpress.Button(
                                                                                                                                    Sub(btn)
                                                                                                                                        btn.Name = "btnSalirAcceso"
                                                                                                                                        btn.Text = "Salir"
                                                                                                                                        btn.Width = 100
                                                                                                                                        btn.ClientSideEvents.Click = "btnSalirClick"
                                                                                                                                    End Sub).GetHtml()
                                                                                                                        End Sub)
                                                                                              End Sub)
                                                                            End Sub).GetHtml()
                                                                        End Sub)
                                                   End Sub).GetHtml() %>
              </div>

               <div>
                  <%Html.DevExpress.PopupControl(Sub(popup)
                                                       popup.Name = "popupEliminarAcceso"

                                                       popup.Width = 380
                                                       popup.AllowDragging = True
                                                       popup.CloseAction = CloseAction.CloseButton
                                                       popup.CloseOnEscape = True
                                                       popup.PopupAnimationType = AnimationType.Fade
                                                       popup.HeaderText = "Quitar Acceso"
                                                       popup.CloseAnimationType = AnimationType.Fade
                                                       popup.Modal = True
                                                       popup.AutoUpdatePosition = True
                                                       popup.PopupHorizontalAlign = PopupHorizontalAlign.WindowCenter
                                                       popup.PopupVerticalAlign = PopupVerticalAlign.WindowCenter
                                                       popup.SetContent(Sub()
                                                                            Html.DevExpress.FormLayout(
                                                                            Sub(frm)
                                                                                frm.Name = "frmEliminarAcceso"

                                                                                frm.ColCount = 1
                                                                                frm.Items.Add(Sub(item)
                                                                                                  item.Name = "txt"
                                                                                                  item.ShowCaption = DefaultBoolean.False
                                                                                                  item.SetNestedContent(Sub()
                                                                                                                            Html.DevExpress.Label(Sub(lbl)
                                                                                                                                                      lbl.Name = "lblmensajeEliminar"
                                                                                                                                                      lbl.Properties.EnableClientSideAPI = True
                                                                                                                                                      lbl.Text = "Esta seguro que desear quitar el acceso de esta pantalla a este rol?"
                                                                                                                                                      lbl.Width = 380
                                                                                                                                                  End Sub).GetHtml()
                                                                                                                        End Sub)
                                                                                              End Sub)
                                                                                frm.Items.Add(Sub(item)
                                                                                                  item.ShowCaption = DefaultBoolean.False
                                                                                                  item.HorizontalAlign = FormLayoutHorizontalAlign.Center
                                                                                                  item.SetNestedContent(Sub()
                                                                                                                            Html.DevExpress.Button(Sub(btn)
                                                                                                                                                       btn.Name = "btnEliminarAcceso"

                                                                                                                                                       btn.Text = "SI"
                                                                                                                                                       btn.Width = 100
                                                                                                                                                       btn.ClientSideEvents.Click = "btnEliminarAccesoClick"

                                                                                                                                                   End Sub).GetHtml()
                                                                                                                            ViewContext.Writer.Write("&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp")
                                                                                                                            Html.DevExpress.Button(Sub(btn)
                                                                                                                                                       btn.Name = "btnSalirEliminarAcceso"

                                                                                                                                                       btn.Text = "NO"
                                                                                                                                                       btn.Width = 100
                                                                                                                                                       btn.ClientSideEvents.Click = "btnSalirEliminarAccesoClick"
                                                                                                                                                       btn.Styles.Style.BackColor = System.Drawing.Color.White
                                                                                                                                                       btn.Styles.Style.ForeColor = System.Drawing.Color.FromArgb(0, 150, 136)
                                                                                                                                                   End Sub).GetHtml()
                                                                                                                        End Sub)
                                                                                              End Sub)
                                                                            End Sub).GetHtml()
                                                                        End Sub)
                                                   End Sub).GetHtml() %>
              </div>
        </div>
    
</asp:Content>
