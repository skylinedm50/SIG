<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>
<% 
    Html.DevExpress.FormLayout(Sub(configuracionFormLayout)
                                   configuracionFormLayout.Name = "AspxFormLayoutDetalleEsquemaEnlazado"
                                   configuracionFormLayout.ColCount = 1
                                   configuracionFormLayout.ControlStyle.CssClass = "AspxFormLayoutControlesPlanilla"
                                   configuracionFormLayout.AlignItemCaptionsInAllGroups = True
                                   configuracionFormLayout.Items.AddGroupItem(Sub(configuracionGroupItem)
                                                                                  configuracionGroupItem.Caption = "Detalle de los esquemas seleccionados"
                                                                                  configuracionGroupItem.GroupBoxDecoration = GroupBoxDecoration.HeadingLine
                                                                                  configuracionGroupItem.ColCount = 1
                                                                                  configuracionGroupItem.Items.Add(Sub(configuracionItem)
                                                                                                                       configuracionItem.Caption = "Esquemas seleccionados:"
                                                                                                                       configuracionItem.CaptionSettings.Location = LayoutItemCaptionLocation.Top
                                                                                                                       configuracionItem.HorizontalAlign = FormLayoutHorizontalAlign.Center
                                                                                                                       configuracionItem.SetNestedContent(Sub()
                                                                                                                                                              Html.DevExpress.ListBox(Sub(configuracionListBox)
                                                                                                                                                                                          configuracionListBox.Name = "AspxListBoxEsquemasSelect"
                                                                                                                                                                                          configuracionListBox.Properties.ClientInstanceName = "AspxListBoxEsquemasSelect"
                                                                                                                                                                                          configuracionListBox.Properties.Columns.Add("nom_esq", "Esquema")
                                                                                                                                                                                          configuracionListBox.Width = 700
                                                                                                                                                                                      End Sub).Render()
                                                                                                                                                          End Sub)
                                                                                                                   End Sub)
                                                                                  configuracionGroupItem.Items.Add(Sub(configuracionItem)
                                                                                                                       configuracionItem.ShowCaption = DefaultBoolean.False
                                                                                                                       configuracionItem.SetNestedContent(Sub()
                                                                                                                                                              Html.RenderAction("GridViewDetalleEsquemaEnlazadoLocalizacion", "Shared", New With {Key .intCodEsquema = ViewData("intCodEsquema"), .intTipoConfig = ViewData("intTipoConfig")})
                                                                                                                                                          End Sub)
                                                                                                                   End Sub)
                                                                              End Sub)
                               End Sub).GetHtml()
%>