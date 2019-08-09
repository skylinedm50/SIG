<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%

    Html.DevExpress.FormLayout(Sub(configuracionFormLayout)
                                   configuracionFormLayout.Name = "AspxFormLayoutControlesReportes"
                                   configuracionFormLayout.ColCount = 2
                                   configuracionFormLayout.ControlStyle.CssClass = "FormLayoutControlesReportes"
                                   configuracionFormLayout.Items.AddGroupItem(Sub(configuracionGroupItem As MVCxFormLayoutGroup)
                                                                                  configuracionGroupItem.Caption = "Parámetros de búsqueda del beneficiario"
                                                                                  configuracionGroupItem.GroupBoxDecoration = GroupBoxDecoration.HeadingLine
                                                                                  configuracionGroupItem.ColCount = 2
                                                                                  configuracionGroupItem.Items.Add(Sub(configuracionItem As MVCxFormLayoutItem)
                                                                                                                       configuracionItem.Caption = "Código SIG"
                                                                                                                       configuracionItem.SetNestedContent(Sub()
                                                                                                                                                              Html.DevExpress.SpinEdit(Sub(configuracionSpinEdit As SpinEditSettings)
                                                                                                                                                                                           configuracionSpinEdit.Name = "AspxSpintEditCodBenef"
                                                                                                                                                                                           configuracionSpinEdit.Properties.ClearButton.DisplayMode = ClearButtonDisplayMode.Auto
                                                                                                                                                                                           configuracionSpinEdit.Properties.MaxValue = 10000000
                                                                                                                                                                                           configuracionSpinEdit.Properties.MinValue = 1
                                                                                                                                                                                           configuracionSpinEdit.Properties.ClearButton.DisplayMode = ClearButtonDisplayMode.OnHover
                                                                                                                                                                                       End Sub).Render()
                                                                                                                                                          End Sub)
                                                                                                                   End Sub)
                                                                                  configuracionGroupItem.Items.Add(Sub(configuracionItem As MVCxFormLayoutItem)
                                                                                                                       configuracionItem.Caption = "Código RUP"
                                                                                                                       configuracionItem.SetNestedContent(Sub()
                                                                                                                                                              Html.DevExpress.SpinEdit(Sub(configuracionSpinEdit As SpinEditSettings)
                                                                                                                                                                                           configuracionSpinEdit.Name = "AspxSpintEditCodRUPBenef"
                                                                                                                                                                                           configuracionSpinEdit.Properties.ClearButton.DisplayMode = ClearButtonDisplayMode.Auto
                                                                                                                                                                                           configuracionSpinEdit.Properties.MaxValue = 10000000
                                                                                                                                                                                           configuracionSpinEdit.Properties.MinValue = 1
                                                                                                                                                                                           configuracionSpinEdit.Properties.ClearButton.DisplayMode = ClearButtonDisplayMode.OnHover
                                                                                                                                                                                       End Sub).Render()
                                                                                                                                                          End Sub)
                                                                                                                   End Sub)
                                                                                  configuracionGroupItem.Items.Add(Sub(configuracionItem As MVCxFormLayoutItem)
                                                                                                                       configuracionItem.Caption = "Identidad"
                                                                                                                       configuracionItem.SetNestedContent(Sub()
                                                                                                                                                              Html.DevExpress.TextBox(Sub(configuracionTextBox As TextBoxSettings)
                                                                                                                                                                                          configuracionTextBox.Name = "AspxTextBoxIdentBenef"
                                                                                                                                                                                          configuracionTextBox.Properties.MaxLength = 13
                                                                                                                                                                                      End Sub).Render()
                                                                                                                                                          End Sub)
                                                                                                                   End Sub)
                                                                                  configuracionGroupItem.Items.Add(Sub(configuracionItem As MVCxFormLayoutItem)
                                                                                                                       configuracionItem.Caption = "Primer Nombre"
                                                                                                                       configuracionItem.SetNestedContent(Sub()
                                                                                                                                                              Html.DevExpress.TextBox(Sub(configuracionTextBox As TextBoxSettings)
                                                                                                                                                                                          configuracionTextBox.Name = "AspxTextBoxNom1Benef"
                                                                                                                                                                                      End Sub).Render()
                                                                                                                                                          End Sub)
                                                                                                                   End Sub)
                                                                                  configuracionGroupItem.Items.Add(Sub(configuracionItem As MVCxFormLayoutItem)
                                                                                                                       configuracionItem.Caption = "Segundo Nombre"
                                                                                                                       configuracionItem.SetNestedContent(Sub()
                                                                                                                                                              Html.DevExpress.TextBox(Sub(configuracionTextBox As TextBoxSettings)
                                                                                                                                                                                          configuracionTextBox.Name = "AspxTextBoxNom2Benef"
                                                                                                                                                                                      End Sub).Render()
                                                                                                                                                          End Sub)
                                                                                                                   End Sub)
                                                                                  configuracionGroupItem.Items.Add(Sub(configuracionItem As MVCxFormLayoutItem)
                                                                                                                       configuracionItem.Caption = "Primer Apellido"
                                                                                                                       configuracionItem.SetNestedContent(Sub()
                                                                                                                                                              Html.DevExpress.TextBox(Sub(configuracionTextBox As TextBoxSettings)
                                                                                                                                                                                          configuracionTextBox.Name = "AspxTextBoxApelli1Benef"
                                                                                                                                                                                      End Sub).Render()
                                                                                                                                                          End Sub)
                                                                                                                   End Sub)
                                                                                  configuracionGroupItem.Items.Add(Sub(configuracionItem As MVCxFormLayoutItem)
                                                                                                                       configuracionItem.Caption = "Segundo Apellido"
                                                                                                                       configuracionItem.SetNestedContent(Sub()
                                                                                                                                                              Html.DevExpress.TextBox(Sub(configuracionTextBox As TextBoxSettings)
                                                                                                                                                                                          configuracionTextBox.Name = "AspxTextBoxApelli2Benef"
                                                                                                                                                                                      End Sub).Render()
                                                                                                                                                          End Sub)
                                                                                                                   End Sub)
                                                                                  configuracionGroupItem.Items.Add(Sub(configuracionItem As MVCxFormLayoutItem)
                                                                                                                       configuracionItem.ShowCaption = DefaultBoolean.False
                                                                                                                       configuracionItem.SetNestedContent(Sub()
                                                                                                                                                              Html.DevExpress.Button(Sub(configuracionButton As ButtonSettings)
                                                                                                                                                                                         configuracionButton.Name = "AspxButtonBuscar"
                                                                                                                                                                                         configuracionButton.UseSubmitBehavior = False
                                                                                                                                                                                         configuracionButton.Text = "Buscar"
                                                                                                                                                                                         configuracionButton.ClientSideEvents.Click = "function(){ objBusqCorresBenef.BuscarCorresBenef(); }"
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
                                                                                                                                                                                        configuracionLabel.ControlStyle.Font.Bold = True
                                                                                                                                                                                    End Sub).GetHtml()
                                                                                                                                                          End Sub)

                                                                                                                   End Sub)
                                                                              End Sub)
                                   configuracionFormLayout.Items.AddGroupItem(Sub(configuracionGroupItem As MVCxFormLayoutGroup)
                                                                                  configuracionGroupItem.Caption = "Parámetros de búsqueda del titular"
                                                                                  configuracionGroupItem.GroupBoxDecoration = GroupBoxDecoration.HeadingLine
                                                                                  configuracionGroupItem.ColCount = 1
                                                                                  configuracionGroupItem.Items.Add(Sub(configuracionItem As MVCxFormLayoutItem)
                                                                                                                       configuracionItem.Caption = "Identidad"
                                                                                                                       configuracionItem.SetNestedContent(Sub()
                                                                                                                                                              Html.DevExpress.TextBox(Sub(configuracionTextBox As TextBoxSettings)
                                                                                                                                                                                          configuracionTextBox.Name = "AspxTextBoxIdentTitular"
                                                                                                                                                                                          configuracionTextBox.Properties.MaxLength = 13
                                                                                                                                                                                      End Sub).Render()
                                                                                                                                                          End Sub)

                                                                                                                   End Sub)
                                                                                  configuracionGroupItem.Items.Add(Sub(configuracionItem As MVCxFormLayoutItem)
                                                                                                                       configuracionItem.Caption = "Código SIG Hogar"
                                                                                                                       configuracionItem.SetNestedContent(Sub()
                                                                                                                                                              Html.DevExpress.SpinEdit(Sub(configuracionSpinEdit As SpinEditSettings)
                                                                                                                                                                                           configuracionSpinEdit.Name = "AspxSpintEditCodSIGHog"
                                                                                                                                                                                           configuracionSpinEdit.Properties.ClearButton.DisplayMode = ClearButtonDisplayMode.Auto
                                                                                                                                                                                           configuracionSpinEdit.Properties.MaxValue = 10000000
                                                                                                                                                                                           configuracionSpinEdit.Properties.MinValue = 1
                                                                                                                                                                                           configuracionSpinEdit.Properties.ClearButton.DisplayMode = ClearButtonDisplayMode.OnHover
                                                                                                                                                                                       End Sub).Render()
                                                                                                                                                          End Sub)
                                                                                                                   End Sub)
                                                                                  configuracionGroupItem.Items.Add(Sub(configuracionItem As MVCxFormLayoutItem)
                                                                                                                       configuracionItem.Caption = "Código RUP Hogar"
                                                                                                                       configuracionItem.SetNestedContent(Sub()
                                                                                                                                                              Html.DevExpress.SpinEdit(Sub(configuracionSpinEdit As SpinEditSettings)
                                                                                                                                                                                           configuracionSpinEdit.Name = "AspxSpintEditCodRUPHog"
                                                                                                                                                                                           configuracionSpinEdit.Properties.ClearButton.DisplayMode = ClearButtonDisplayMode.Auto
                                                                                                                                                                                           configuracionSpinEdit.Properties.MaxValue = 10000000
                                                                                                                                                                                           configuracionSpinEdit.Properties.MinValue = 1
                                                                                                                                                                                           configuracionSpinEdit.Properties.ClearButton.DisplayMode = ClearButtonDisplayMode.OnHover
                                                                                                                                                                                       End Sub).Render()
                                                                                                                                                          End Sub)
                                                                                                                   End Sub)
                                                                              End Sub)
                               End Sub).GetHtml()
%>