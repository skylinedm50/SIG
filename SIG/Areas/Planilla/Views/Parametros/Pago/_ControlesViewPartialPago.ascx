    <%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    Html.DevExpress.FormLayout(Sub(configuracionFormLayout)
                                   configuracionFormLayout.Name = "AspxFormLayoutControlesPago"
                                   configuracionFormLayout.ColCount = 2
                                   configuracionFormLayout.ControlStyle.CssClass = "AspxFormLayoutControlesPlanilla"
                                   configuracionFormLayout.Items.AddGroupItem(Sub(configuracionGroupItem)
                                                                                  configuracionGroupItem.Caption = "Ingresar nuevo pago"
                                                                                  configuracionGroupItem.ColCount = 3
                                                                                  configuracionGroupItem.Items.Add(Sub(configuracionItem)
                                                                                                                       configuracionItem.Caption = "Año:"
                                                                                                                       configuracionItem.HelpText = "Seleccione el año del pago."
                                                                                                                       configuracionItem.SetNestedContent(Sub()
                                                                                                                                                              Html.DevExpress.SpinEdit(Sub(ConfiguracionSpinEdit)
                                                                                                                                                                                           Dim dtFechaActual As Date = Date.Now
                                                                                                                                                                                           
                                                                                                                                                                                           ConfiguracionSpinEdit.Name = "AspxSpinEditAñoPago"
                                                                                                                                                                                           ConfiguracionSpinEdit.Properties.ClientInstanceName = "AspxSpinEditAñoPago"
                                                                                                                                                                                           ConfiguracionSpinEdit.Properties.MinValue = dtFechaActual.Year
                                                                                                                                                                                           ConfiguracionSpinEdit.Properties.MaxValue = 2050
                                                                                                                                                                                       End Sub).GetHtml()
                                                                                                                                                          End Sub)
                                                                                                                   End Sub)
                                                                                  configuracionGroupItem.Items.Add(Sub(configuracionItem)
                                                                                                                       configuracionItem.Caption = "Nombre:"
                                                                                                                       configuracionItem.HelpText = "Por favor ingresar un nombre para el pago."
                                                                                                                       configuracionItem.SetNestedContent(Sub()
                                                                                                                                                              Html.DevExpress.TextBox(Sub(configuracionTextBox)
                                                                                                                                                                                          configuracionTextBox.Name = "AspxTextBoxNombrePago"
                                                                                                                                                                                          configuracionTextBox.Properties.ClientInstanceName = "AspxTextBoxNombrePago"
                                                                                                                                                                                      End Sub).GetHtml()
                                                                                                                                                          End Sub)
                                                                                                                   End Sub)
                                                                                  configuracionGroupItem.Items.Add(Sub(configuracionItem)
                                                                                                                       configuracionItem.Caption = "Descripción:"
                                                                                                                       configuracionItem.HelpText = "Por favor ingresar una descripción para el pago."
                                                                                                                       configuracionItem.SetNestedContent(Sub()
                                                                                                                                                              Html.DevExpress.Memo(Sub(configuracionMemo)
                                                                                                                                                                                       configuracionMemo.Name = "AspxMemoDescripPago"
                                                                                                                                                                                       configuracionMemo.Properties.ClientInstanceName = "AspxMemoDescripPago"
                                                                                                                                                                                       configuracionMemo.Height = 71
                                                                                                                                                                                   End Sub).GetHtml()
                                                                                                                                                          End Sub)
                                                                                                                   End Sub)
                                                                                  
                                                                                  configuracionGroupItem.Items.Add(Sub(configuracionItem)
                                                                                                                       configuracionItem.ShowCaption = DefaultBoolean.False
                                                                                                                       configuracionItem.HelpText = "Presione el botón para ingresar el pago."
                                                                                                                       configuracionItem.SetNestedContent(Sub()
                                                                                                                                                              Html.DevExpress.Button(
                                                                                                                                                                  Sub(configuracionButton)
                                                                                                                                                                      configuracionButton.Name = "AspxButtonGuardarPago"
                                                                                                                                                                      configuracionButton.Text = "Guardar"
                                                                                                                                                                      configuracionButton.UseSubmitBehavior = False 'Deshabilito el Autopostback del botón.
                                                                                                                                                                      configuracionButton.ClientSideEvents.Click = "function(s,e){fnc_ingresar_pago();}"
                                                                                                                                                                  End Sub).GetHtml()
                                                                                                                                                          End Sub)
                                                                                                                   End Sub)
                                                                              End Sub)
                               End Sub).GetHtml()
    
%>