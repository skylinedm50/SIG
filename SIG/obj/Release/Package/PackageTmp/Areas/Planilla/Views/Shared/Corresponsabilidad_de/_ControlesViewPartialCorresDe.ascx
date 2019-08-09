<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    Html.DevExpress.FormLayout(Sub(configuracionFormLayout)
                                   configuracionFormLayout.Name = "AspxFormLayoutControlCorresDe"
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
                                                                                                                                                                                         configuracionButton.ClientSideEvents.Click = "function(){objCorrespDe.BuscarCorresp();}"
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
                                                                                                                                                              Html.RenderAction("GridViewTransferencias", "Shared", New With {Key .intCodEsquema = 0})
                                                                                                                                                          End Sub)
                                                                                                                   End Sub)
                                                                              End Sub)
                                   configuracionFormLayout.Items.AddGroupItem(Sub(configuracionGroupItem)
                                                                                  configuracionGroupItem.ShowCaption = DefaultBoolean.False
                                                                                  configuracionGroupItem.GroupBoxDecoration = GroupBoxDecoration.None
                                                                                  configuracionGroupItem.ColCount = 2
                                                                                  configuracionGroupItem.Items.Add(Sub(configuracionItem)
                                                                                                                       configuracionItem.Caption = "Componente:"
                                                                                                                       configuracionItem.SetNestedContent(Sub()
                                                                                                                                                              Html.RenderPartial("Corresponsabilidad_de\_ComBoxViewPartialComponente")
                                                                                                                                                          End Sub)
                                                                                                                   End Sub)
                                                                                  configuracionGroupItem.Items.Add(Sub(configuracionItem)
                                                                                                                       configuracionItem.Caption = "Corresponsabilidad:"
                                                                                                                       configuracionItem.SetNestedContent(Sub()
                                                                                                                                                              Html.RenderAction("AspxComboBoxCorresponsabilidad", "Shared", New With {Key .intCodComponente = 0})
                                                                                                                                                          End Sub)
                                                                                                                   End Sub)
                                                                              End Sub)
                                   configuracionFormLayout.Items.AddGroupItem(Sub(configuracionGroupItem)
                                                                                  configuracionGroupItem.ShowCaption = DefaultBoolean.False
                                                                                  configuracionGroupItem.GroupBoxDecoration = GroupBoxDecoration.None
                                                                                  configuracionGroupItem.ColCount = 4
                                                                                  configuracionGroupItem.Items.Add(Sub(configuracionItem)
                                                                                                                       configuracionItem.ShowCaption = DefaultBoolean.False
                                                                                                                       configuracionItem.SetNestedContent(Sub()
                                                                                                                                                              Html.DevExpress.Button(Sub(configuracionButton)
                                                                                                                                                                                         configuracionButton.Name = "AspxButtonAgregar"
                                                                                                                                                                                         configuracionButton.Text = "Agregar"
                                                                                                                                                                                         configuracionButton.UseSubmitBehavior = False
                                                                                                                                                                                         configuracionButton.ClientSideEvents.Click = "function(){objCorrespDe.Agregar();}"
                                                                                                                                                                                     End Sub).Render()
                                                                                                                                                          End Sub)
                                                                                                                   End Sub)
                                                                                  configuracionGroupItem.Items.Add(Sub(configuracionItem)
                                                                                                                       configuracionItem.ShowCaption = DefaultBoolean.False
                                                                                                                       configuracionItem.SetNestedContent(Sub()
                                                                                                                                                              Html.DevExpress.Button(Sub(configuracionButton)
                                                                                                                                                                                         configuracionButton.Name = "AspxButtonReordenar"
                                                                                                                                                                                         configuracionButton.Text = "Guardar reordenamiento"
                                                                                                                                                                                         configuracionButton.UseSubmitBehavior = False
                                                                                                                                                                                         configuracionButton.ClientSideEvents.Click = "function(){objCorrespDe.ReOrdenar();}"
                                                                                                                                                                                     End Sub).Render()
                                                                                                                                                          End Sub)
                                                                                                                   End Sub)
                                                                                  configuracionGroupItem.Items.Add(Sub(configuracionItem)
                                                                                                                       configuracionItem.ShowCaption = DefaultBoolean.False
                                                                                                                       configuracionItem.SetNestedContent(Sub()
                                                                                                                                                              Html.DevExpress.Button(Sub(configuracionButton)
                                                                                                                                                                                         configuracionButton.Name = "AspxButtonArriba"
                                                                                                                                                                                         configuracionButton.Text = "Arriba"
                                                                                                                                                                                         configuracionButton.UseSubmitBehavior = False
                                                                                                                                                                                         configuracionButton.ClientSideEvents.Click = "function(){ objCorrespDe.Mover(1); }"
                                                                                                                                                                                     End Sub).Render()
                                                                                                                                                          End Sub)
                                                                                                                   End Sub)
                                                                                  configuracionGroupItem.Items.Add(Sub(configuracionItem)
                                                                                                                       configuracionItem.ShowCaption = DefaultBoolean.False
                                                                                                                       configuracionItem.SetNestedContent(Sub()
                                                                                                                                                              Html.DevExpress.Button(Sub(configuracionButton)
                                                                                                                                                                                         configuracionButton.Name = "AspxButtonAbajo"
                                                                                                                                                                                         configuracionButton.Text = "Abajo"
                                                                                                                                                                                         configuracionButton.UseSubmitBehavior = False
                                                                                                                                                                                         configuracionButton.ClientSideEvents.Click = "function(){objCorrespDe.Mover(2);}"
                                                                                                                                                                                     End Sub).Render()
                                                                                                                                                          End Sub)
                                                                                                                   End Sub)
                                                                              End Sub)
                               End Sub).GetHtml()

%>