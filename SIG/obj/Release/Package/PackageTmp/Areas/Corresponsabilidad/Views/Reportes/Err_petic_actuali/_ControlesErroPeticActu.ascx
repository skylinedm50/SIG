<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    Html.DevExpress.FormLayout(Sub(configuracionFormLayout)
                                   configuracionFormLayout.Name = "AspxFormLayoutControlesReportes"
                                   configuracionFormLayout.ColCount = 2
                                   configuracionFormLayout.ControlStyle.CssClass = "FormLayoutControlesReportes"
                                   configuracionFormLayout.Items.AddGroupItem(Sub(configuracionGroupItem As MVCxFormLayoutGroup)
                                                                                  configuracionGroupItem.Caption = "Parámetros"
                                                                                  configuracionGroupItem.GroupBoxDecoration = GroupBoxDecoration.HeadingLine
                                                                                  configuracionGroupItem.ColCount = 2
                                                                                  configuracionGroupItem.Items.Add(Sub(configuracionItem As MVCxFormLayoutItem)
                                                                                                                       configuracionItem.Caption = "Tipo de componente"
                                                                                                                       configuracionItem.HelpText = "Por favor seleccione un tipo de componente."
                                                                                                                       configuracionItem.SetNestedContent(Sub()
                                                                                                                                                              Html.RenderAction("AspxComboBoxTipComponente")
                                                                                                                                                          End Sub)
                                                                                                                   End Sub)
                                                                                  configuracionGroupItem.Items.Add(Sub(configuracionItem As MVCxFormLayoutItem)
                                                                                                                       configuracionItem.Caption = "Actualización"
                                                                                                                       configuracionItem.HelpText = "Por favor seleccione una actualización."
                                                                                                                       configuracionItem.SetNestedContent(Sub()
                                                                                                                                                              Html.RenderAction("AspxComboBoxActulizacion", New With {Key .intCodTipCom = 0, Key .intTipoForm = 1})
                                                                                                                                                          End Sub)
                                                                                                                   End Sub)
                                                                                  configuracionGroupItem.Items.Add(Sub(configuracionItem As MVCxFormLayoutItem)
                                                                                                                       configuracionItem.ShowCaption = DefaultBoolean.False
                                                                                                                       configuracionItem.SetNestedContent(Sub()
                                                                                                                                                              Html.DevExpress.Button(Sub(configuracionButton As ButtonSettings)
                                                                                                                                                                                         configuracionButton.Name = "AspxButtonBuscar"
                                                                                                                                                                                         configuracionButton.UseSubmitBehavior = False
                                                                                                                                                                                         configuracionButton.Text = "Buscar"
                                                                                                                                                                                         configuracionButton.ClientSideEvents.Click = "function(){ AspxGridViewErrorPetiActua.PerformCallback(); }"
                                                                                                                                                                                     End Sub).Render()
                                                                                                                                                          End Sub)
                                                                                                                   End Sub)
                                                                              End Sub)
                               End Sub).GetHtml()
%>