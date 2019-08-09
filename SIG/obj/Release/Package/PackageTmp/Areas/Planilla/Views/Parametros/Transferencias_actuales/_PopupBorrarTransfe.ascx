<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    Html.DevExpress.PopupControl(Sub(configuracionPopup)
                                     configuracionPopup.Name = "AspxPopupDesEnlazarEsquema"
                                     configuracionPopup.Modal = True
                                     configuracionPopup.HeaderText = "¿Desea desenlazar el esquema?"
                                     configuracionPopup.PopupHorizontalAlign = PopupHorizontalAlign.WindowCenter
                                     configuracionPopup.PopupVerticalAlign = PopupVerticalAlign.WindowCenter
                                     configuracionPopup.CloseAction = CloseAction.CloseButton
                                     configuracionPopup.AllowDragging = True
                                     configuracionPopup.SetContent(Sub()
                                                                       Html.DevExpress.FormLayout(Sub(configuracionFormLayout)
                                                                                                      configuracionFormLayout.Name = "AspxFormLayoutDesenlazarEsquema"
                                                                                                      configuracionFormLayout.ColCount = 1
                                                                                                      
                                                                                                      configuracionFormLayout.Items.AddGroupItem(Sub(configuracionGroupItem)
                                                                                                                                                     configuracionGroupItem.ShowCaption = DefaultBoolean.False
                                                                                                                                                     configuracionGroupItem.GroupBoxDecoration = GroupBoxDecoration.None
                                                                                                                                                     configuracionGroupItem.ColCount = 2
                                                                                                                                                     configuracionGroupItem.Items.Add(Sub(configuracionItem)
                                                                                                                                                                                          configuracionItem.ShowCaption = DefaultBoolean.False
                                                                                                                                                                                          configuracionItem.SetNestedContent(Sub()
                                                                                                                                                                                                                                 Html.DevExpress.Button(Sub(configuracionButton)
                                                                                                                                                                                                                                                            configuracionButton.Name = "AspxSiDesEnlzadar"
                                                                                                                                                                                                                                                            configuracionButton.Text = "Si"
                                                                                                                                                                                                                                                            configuracionButton.ClientSideEvents.Click = "function(){ objManejoEsquema.Esquemas.Desenlazar(); }"
                                                                                                                                                                                                                                                            configuracionButton.UseSubmitBehavior = False
                                                                                                                                                                                                                                                        End Sub).Render()
                                                                                                                                                                                                                             End Sub)
                                                                                                                                                                                      End Sub)
                                                                                                                                                     configuracionGroupItem.Items.Add(Sub(configuracionItem)
                                                                                                                                                                                          configuracionItem.ShowCaption = DefaultBoolean.False
                                                                                                                                                                                          configuracionItem.SetNestedContent(Sub()
                                                                                                                                                                                                                                 Html.DevExpress.Button(Sub(configuracionButton)
                                                                                                                                                                                                                                                            configuracionButton.Name = "AspxNoDesEnlzadar"
                                                                                                                                                                                                                                                            configuracionButton.Text = "No"
                                                                                                                                                                                                                                                            configuracionButton.ClientSideEvents.Click = "function(){AspxPopupDesEnlazarEsquema.Hide(); objManejoEsquema.Esquemas.LastCheckBoxSelect.checked = true;}"
                                                                                                                                                                                                                                                            configuracionButton.UseSubmitBehavior = False
                                                                                                                                                                                                                                                        End Sub).Render()
                                                                                                                                                                                                                             End Sub)
                                                                                                                                                                                      End Sub)
                                                                                                                                                 End Sub)
                                                                                                  End Sub).Render()
                                                                   End Sub)
                                 End Sub).GetHtml()
%>