<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%    Html.DevExpress.FormLayout(Sub(configuracionFormLayout)
                                   configuracionFormLayout.Name = "AspxFormLayoutGestionObservaciones"
                                   configuracionFormLayout.ColCount = 1
                                   configuracionFormLayout.ControlStyle.CssClass = "FormLayoutPantallas"
                                   configuracionFormLayout.Items.AddGroupItem(Sub(configuracionGroupItem As MVCxFormLayoutGroup)
                                                                                  configuracionGroupItem.Caption = "Parámetros de Ingreso"
                                                                                  configuracionGroupItem.ColCount = 2
                                                                                  configuracionGroupItem.GroupBoxDecoration = GroupBoxDecoration.HeadingLine
                                                                                  configuracionGroupItem.Items.Add(Sub(configuracionItem As MVCxFormLayoutItem)
                                                                                                                       configuracionItem.Caption = "Código de solicitud"
                                                                                                                       configuracionItem.SetNestedContent(Sub()
                                                                                                                                                              Html.DevExpress.SpinEdit(Sub(configuracionSpinEdit As SpinEditSettings)
                                                                                                                                                                                           configuracionSpinEdit.Name = "AspxSpinEditCodSolicitud"
                                                                                                                                                                                           configuracionSpinEdit.Properties.ClientInstanceName = "AspxSpinEditCodSolicitud"
                                                                                                                                                                                           configuracionSpinEdit.Properties.MaxValue = 10000000
                                                                                                                                                                                           configuracionSpinEdit.Properties.MinValue = 1
                                                                                                                                                                                           configuracionSpinEdit.Width = Unit.Percentage(100)
                                                                                                                                                                                       End Sub).Render()
                                                                                                                                                          End Sub)
                                                                                                                   End Sub)
                                                                                  configuracionGroupItem.Items.Add(Sub(configuracionItem As MVCxFormLayoutItem)
                                                                                                                       configuracionItem.Caption = "Observación"
                                                                                                                       configuracionItem.SetNestedContent(Sub()
                                                                                                                                                              Html.DevExpress.Memo(Sub(configuracionMemo As MemoSettings)
                                                                                                                                                                                       configuracionMemo.Name = "AspxMemoObservacion"
                                                                                                                                                                                       configuracionMemo.Properties.ClientInstanceName = "AspxMemoObservacion"
                                                                                                                                                                                       configuracionMemo.Width = 200
                                                                                                                                                                                       configuracionMemo.Height = 71
                                                                                                                                                                                   End Sub).Render()
                                                                                                                                                          End Sub)
                                                                                                                   End Sub)
                                                                                  configuracionGroupItem.Items.Add(Sub(configuracionItem As MVCxFormLayoutItem)
                                                                                                                       configuracionItem.ShowCaption = DefaultBoolean.False
                                                                                                                       configuracionItem.SetNestedContent(Sub()
                                                                                                                                                              Html.DevExpress.Button(Sub(configuracionButton As ButtonSettings)
                                                                                                                                                                                         configuracionButton.Name = "AspxButtonIngresar"
                                                                                                                                                                                         configuracionButton.Text = "Ingresar"
                                                                                                                                                                                         configuracionButton.UseSubmitBehavior = False
                                                                                                                                                                                         configuracionButton.ClientSideEvents.Click = "function(){objGestionSegumiento.ingresar();}"
                                                                                                                                                                                     End Sub).Render()
                                                                                                                                                          End Sub)
                                                                                                                   End Sub)
                                                                                  configuracionGroupItem.Items.Add(Sub(configuracionItem As MVCxFormLayoutItem)
                                                                                                                       configuracionItem.ShowCaption = DefaultBoolean.False
                                                                                                                       configuracionItem.SetNestedContent(Sub()
                                                                                                                                                              Html.DevExpress.Label(Sub(configuracionLabel As LabelSettings)
                                                                                                                                                                                        configuracionLabel.Name = "AspxLabelError"
                                                                                                                                                                                        configuracionLabel.Properties.ClientInstanceName = "AspxLabelError"
                                                                                                                                                                                        configuracionLabel.ControlStyle.ForeColor = System.Drawing.Color.Red
                                                                                                                                                                                    End Sub).Render()
                                                                                                                                                          End Sub)
                                                                                                                   End Sub)
                                                                              End Sub)
                               End Sub).GetHtml()

%>