<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%    Html.DevExpress.FormLayout(Sub(configuracionFormLayout)
                                   configuracionFormLayout.Name = "AspxFormLayoutCorresponsabilidad"
                                   configuracionFormLayout.ColCount = 1
                                   configuracionFormLayout.Items.AddGroupItem(Sub(configuracionGroupItem As MVCxFormLayoutGroup)
                                                                                  configuracionGroupItem.ShowCaption = DefaultBoolean.False
                                                                                  configuracionGroupItem.GroupBoxDecoration = GroupBoxDecoration.None
                                                                                  configuracionGroupItem.Items.Add(Sub(configuracionItem As MVCxFormLayoutItem)
                                                                                                                       configuracionItem.ShowCaption = DefaultBoolean.False
                                                                                                                       configuracionItem.SetNestedContent(Sub()
                                                                                                                                                              Html.RenderAction("GridViewPartialTipoCorresponsabilidad")
                                                                                                                                                          End Sub)
                                                                                                                   End Sub)
                                                                              End Sub)
                                   configuracionFormLayout.Items.AddGroupItem(Sub(configuracionGroupItem As MVCxFormLayoutGroup)
                                                                                  configuracionGroupItem.ShowCaption = DefaultBoolean.False
                                                                                  configuracionGroupItem.GroupBoxDecoration = GroupBoxDecoration.None
                                                                                  configuracionGroupItem.Items.Add(Sub(configuracionItem As MVCxFormLayoutItem)
                                                                                                                       configuracionItem.ShowCaption = DefaultBoolean.False
                                                                                                                       configuracionItem.SetNestedContent(Sub()
                                                                                                                                                              Html.RenderAction("GridViewPartialDetalleCorresponsabilidad")
                                                                                                                                                          End Sub)
                                                                                                                   End Sub)
                                                                              End Sub)
                               End Sub).GetHtml()

%>