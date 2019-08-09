<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%

    Html.DevExpress.PopupControl(Sub(configuracionAspxPopupControl As PopupControlSettings)
                                     configuracionAspxPopupControl.Name = "AspxPopupControlBorrarCorrespo"
                                     configuracionAspxPopupControl.PopupHorizontalAlign = PopupHorizontalAlign.WindowCenter
                                     configuracionAspxPopupControl.PopupVerticalAlign = PopupVerticalAlign.WindowCenter
                                     configuracionAspxPopupControl.HeaderText = "¿Desea borrar la corresponsabilidad?"
                                     configuracionAspxPopupControl.Modal = True
                                     configuracionAspxPopupControl.CloseAction = CloseAction.None
                                     configuracionAspxPopupControl.AllowDragging = True
                                     configuracionAspxPopupControl.ShowCloseButton = False
                                     configuracionAspxPopupControl.SetContent(Sub()
                                                                                  Html.DevExpress.FormLayout(Sub(configuracionFormLayout)
                                                                                                                 configuracionFormLayout.Name = "AspxFormLayoutBorrarCorresponsabilidad"
                                                                                                                 configuracionFormLayout.ColCount = 1
                                                                                                                 configuracionFormLayout.Items.AddGroupItem(Sub(configuracionGroupItem As MVCxFormLayoutGroup)
                                                                                                                                                                configuracionGroupItem.ShowCaption = DefaultBoolean.False
                                                                                                                                                                configuracionGroupItem.GroupBoxDecoration = GroupBoxDecoration.None
                                                                                                                                                                configuracionGroupItem.ColCount = 2
                                                                                                                                                                configuracionGroupItem.HorizontalAlign = FormLayoutHorizontalAlign.Center
                                                                                                                                                                configuracionGroupItem.Items.Add(Sub(configuracionItem As MVCxFormLayoutItem)
                                                                                                                                                                                                     configuracionItem.ShowCaption = DefaultBoolean.False
                                                                                                                                                                                                     configuracionItem.SetNestedContent(Sub()
                                                                                                                                                                                                                                            Html.DevExpress.Button(Sub(configuracionButton As ButtonSettings)
                                                                                                                                                                                                                                                                       configuracionButton.Name = "AspxButtonBorrSiCorrespo"
                                                                                                                                                                                                                                                                       configuracionButton.Text = "Si"
                                                                                                                                                                                                                                                                       configuracionButton.UseSubmitBehavior = False
                                                                                                                                                                                                                                                                       configuracionButton.ClientSideEvents.Click = "function(){ objCorrespo.Operaciones.Borrar(); }"
                                                                                                                                                                                                                                                                   End Sub).Render()
                                                                                                                                                                                                                                        End Sub)
                                                                                                                                                                                                 End Sub)
                                                                                                                                                                configuracionGroupItem.Items.Add(Sub(configuracionItem As MVCxFormLayoutItem)
                                                                                                                                                                                                     configuracionItem.ShowCaption = DefaultBoolean.False
                                                                                                                                                                                                     configuracionItem.SetNestedContent(Sub()
                                                                                                                                                                                                                                            Html.DevExpress.Button(Sub(configuracionButton As ButtonSettings)
                                                                                                                                                                                                                                                                       configuracionButton.Name = "AspxButtonBorrNoCorrespo"
                                                                                                                                                                                                                                                                       configuracionButton.Text = "No"
                                                                                                                                                                                                                                                                       configuracionButton.UseSubmitBehavior = False
                                                                                                                                                                                                                                                                       configuracionButton.ClientSideEvents.Click = "function(){ AspxPopupControlBorrarCorrespo.Hide(); }"
                                                                                                                                                                                                                                                                   End Sub).Render()
                                                                                                                                                                                                                                        End Sub)
                                                                                                                                                                                                 End Sub)
                                                                                                                                                            End Sub)
                                                                                                                 configuracionFormLayout.Items.AddGroupItem(Sub(configuracionGroupItem As MVCxFormLayoutGroup)
                                                                                                                                                                configuracionGroupItem.ShowCaption = DefaultBoolean.False
                                                                                                                                                                configuracionGroupItem.GroupBoxDecoration = GroupBoxDecoration.None
                                                                                                                                                                configuracionGroupItem.ColCount = 1
                                                                                                                                                                configuracionGroupItem.Items.Add(Sub(configuracionItem As MVCxFormLayoutItem)
                                                                                                                                                                                                     configuracionItem.ShowCaption = DefaultBoolean.False
                                                                                                                                                                                                     configuracionItem.SetNestedContent(Sub()
                                                                                                                                                                                                                                            Html.DevExpress.Label(Sub(configuracionLabel As LabelSettings)
                                                                                                                                                                                                                                                                      configuracionLabel.Name = "AspxLabelMensaBorr"
                                                                                                                                                                                                                                                                      configuracionLabel.Properties.ClientInstanceName = "AspxLabelMensaBorr"
                                                                                                                                                                                                                                                                      configuracionLabel.ControlStyle.ForeColor = System.Drawing.Color.Red
                                                                                                                                                                                                                                                                  End Sub).Render()
                                                                                                                                                                                                                                        End Sub)
                                                                                                                                                                                                 End Sub)

                                                                                                                                                            End Sub)
                                                                                                             End Sub).Render()

                                                                              End Sub)
                                 End Sub).GetHtml()
%>