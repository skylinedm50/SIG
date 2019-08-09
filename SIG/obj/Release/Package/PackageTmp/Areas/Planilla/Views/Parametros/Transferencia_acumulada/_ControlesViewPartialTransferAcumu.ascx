<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    Html.DevExpress.FormLayout(Sub(configuracionFormLayout)
                                   configuracionFormLayout.Name = "AspxFormLayoutControlTransferAcumulada"
                                   configuracionFormLayout.ControlStyle.CssClass = "AspxFormLayoutControlesPlanilla"
                                   configuracionFormLayout.ColCount = 1
                                   configuracionFormLayout.Items.AddGroupItem(Sub(configuracionGroupItem)
                                                                                  configuracionGroupItem.ShowCaption = DefaultBoolean.False
                                                                                  configuracionGroupItem.GroupBoxDecoration = GroupBoxDecoration.None
                                                                                  configuracionGroupItem.ColCount = 3
                                                                                  configuracionGroupItem.Items.Add(Sub(configuracionItem)
                                                                                                                       configuracionItem.Caption = "Pago:"
                                                                                                                       configuracionItem.SetNestedContent(Sub()
                                                                                                                                                              Html.RenderAction("pv_cbxPagos", "Shared")
                                                                                                                                                          End Sub)
                                                                                                                   End Sub)
                                                                                  configuracionGroupItem.Items.Add(Sub(configuracionItem)
                                                                                                                       configuracionItem.Caption = "Esquema:"
                                                                                                                       configuracionItem.SetNestedContent(Sub()
                                                                                                                                                              Html.RenderAction("pv_cbxEsquemas", "Shared")
                                                                                                                                                          End Sub)
                                                                                                                   End Sub)
                                                                                  configuracionGroupItem.Items.Add(Sub(configuracionItem)
                                                                                                                       configuracionItem.ShowCaption = DefaultBoolean.False
                                                                                                                       configuracionItem.SetNestedContent(Sub()
                                                                                                                                                              Html.DevExpress.Button(Sub(configuracionButton)
                                                                                                                                                                                         configuracionButton.Name = "AspxButtonVer"
                                                                                                                                                                                         configuracionButton.Text = "Ver"
                                                                                                                                                                                         configuracionButton.UseSubmitBehavior = False
                                                                                                                                                                                         configuracionButton.ClientSideEvents.Click = "function(){ objTrasnferAcumu.Ver(); }"
                                                                                                                                                                                     End Sub).Render()
                                                                                                                                                          End Sub)
                                                                                                                   End Sub)
                                                                                  
                                                                              End Sub)
                                   configuracionFormLayout.Items.AddGroupItem(Sub(configuracionGroupItem)
                                                                                  configuracionGroupItem.ShowCaption = DefaultBoolean.False
                                                                                  configuracionGroupItem.GroupBoxDecoration = GroupBoxDecoration.None
                                                                                  configuracionGroupItem.ColCount = 1
                                                                                  configuracionGroupItem.Items.Add(Sub(configuracionItem)
                                                                                                                       configuracionItem.ShowCaption = DefaultBoolean.False
                                                                                                                       configuracionItem.SetNestedContent(Sub()
                                                                                                                                                              Html.RenderAction("GridViewPagoAcumulado", "Parametros", New With {Key .intCodPago = 0})
                                                                                                                                                          End Sub)
                                                                                                                   End Sub)
                                                                                  configuracionGroupItem.Items.Add(Sub(configuracionItem)
                                                                                                                       configuracionItem.ShowCaption = DefaultBoolean.False
                                                                                                                       configuracionItem.SetNestedContent(Sub()
                                                                                                                                                              Html.RenderAction("GridViewEsquemaAcumulado", "Parametros", New With {Key .intCodPago = 0})
                                                                                                                                                          End Sub)
                                                                                                                   End Sub)
                                                                              End Sub)
                                   configuracionFormLayout.Items.AddGroupItem(Sub(configuracionGroupItem)
                                                                                  configuracionGroupItem.ShowCaption = DefaultBoolean.False
                                                                                  configuracionGroupItem.GroupBoxDecoration = GroupBoxDecoration.None
                                                                                  configuracionGroupItem.ColCount = 2
                                                                                  configuracionGroupItem.Items.Add(Sub(configuracionItem)
                                                                                                                       configuracionItem.ShowCaption = DefaultBoolean.False
                                                                                                                       configuracionItem.SetNestedContent(Sub()
                                                                                                                                                              Html.DevExpress.Button(Sub(configuracionButton)
                                                                                                                                                                                         configuracionButton.Name = "AspxButtonAgregar"
                                                                                                                                                                                         configuracionButton.Text = "Agregar"
                                                                                                                                                                                         configuracionButton.UseSubmitBehavior = False
                                                                                                                                                                                         configuracionButton.ClientSideEvents.Click = "function(){ objTrasnferAcumu.Agregar(); }"
                                                                                                                                                                                     End Sub).Render()
                                                                                                                                                          End Sub)
                                                                                                                   End Sub)
                                                                                  configuracionGroupItem.Items.Add(Sub(configuracionItem)
                                                                                                                       configuracionItem.ShowCaption = DefaultBoolean.False
                                                                                                                       configuracionItem.SetNestedContent(Sub()
                                                                                                                                                              Html.DevExpress.Button(Sub(configuracionButton)
                                                                                                                                                                                         configuracionButton.Name = "AspxButtonGuardar"
                                                                                                                                                                                         configuracionButton.Text = "Guardar"
                                                                                                                                                                                         configuracionButton.UseSubmitBehavior = False
                                                                                                                                                                                         configuracionButton.ClientSideEvents.Click = "function(){ objTrasnferAcumu.Guardar(); }"
                                                                                                                                                                                     End Sub).Render()
                                                                                                                                                          End Sub)
                                                                                                                   End Sub)
                                                                              End Sub)
                                   configuracionFormLayout.Items.AddGroupItem(Sub(configuracionGroupItem)
                                                                                  configuracionGroupItem.ShowCaption = DefaultBoolean.False
                                                                                  configuracionGroupItem.GroupBoxDecoration = GroupBoxDecoration.None
                                                                                  configuracionGroupItem.ColCount = 1
                                                                                  configuracionGroupItem.Items.Add(Sub(configuracionItem)
                                                                                                                       configuracionItem.ShowCaption = DefaultBoolean.False
                                                                                                                       configuracionItem.SetNestedContent(Sub()
                                                                                                                                                              Html.RenderAction("GridViewDetalleEsquemaAcumulado", "Parametros", New With {Key .strCodEsquemaAdd = "0", .intCodEsqBuscar = 0})
                                                                                                                                                          End Sub)
                                                                                                                   End Sub)
                                                                              End Sub)
                               End Sub).GetHtml()

%>