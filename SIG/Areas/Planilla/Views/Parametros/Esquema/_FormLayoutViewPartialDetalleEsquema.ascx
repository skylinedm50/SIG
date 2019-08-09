<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    Dim objEsquema As New SIG.SIG.Areas.Planilla.Models.cl_esquema
    Dim arrDatos As Array
    arrDatos = objEsquema.fnc_obtener_detalle_esquema(CInt(ViewData("intCodEsquema")))
    ViewData("intCodConfMon") = arrDatos(5)
    Html.DevExpress.FormLayout(Sub(configuracionFormLayout)
                                   configuracionFormLayout.Name = "AspxFormLayoutCamposDetalleEsquema"
                                   configuracionFormLayout.ColCount = 1
                                   configuracionFormLayout.Items.AddGroupItem(Sub(configuracionGroupItem)
                                                                                  configuracionGroupItem.Caption = "Detalle esquema"
                                                                                  configuracionGroupItem.GroupBoxDecoration = GroupBoxDecoration.HeadingLine
                                                                                  configuracionGroupItem.ColCount = 1
                                                                                  configuracionGroupItem.Items.Add(Sub(configuracionItem)
                                                                                                                       configuracionItem.Caption = "Cantidad bono:"
                                                                                                                       configuracionItem.SetNestedContent(Sub()
                                                                                                                                                              Html.DevExpress.Label(Sub(configuracionLabel)
                                                                                                                                                                                        configuracionLabel.Text = arrDatos(0)
                                                                                                                                                                                    End Sub).GetHtml()
                                                                                                                                                          End Sub)
                                                                                                                   End Sub)
                                                                                  configuracionGroupItem.Items.Add(Sub(configuracionItem)
                                                                                                                       configuracionItem.Caption = "Periodo:"
                                                                                                                       configuracionItem.SetNestedContent(Sub()
                                                                                                                                                              Html.DevExpress.Label(Sub(configuracionLabel)
                                                                                                                                                                                        configuracionLabel.Text = arrDatos(1)
                                                                                                                                                                                    End Sub).GetHtml()
                                                                                                                                                          End Sub)
                                                                                                                   End Sub)
                                                                                  configuracionGroupItem.Items.Add(Sub(configuracionItem)
                                                                                                                       configuracionItem.Caption = "Observaciones:"
                                                                                                                       configuracionItem.SetNestedContent(Sub()
                                                                                                                                                              Html.DevExpress.Label(Sub(configuracionLabel)
                                                                                                                                                                                        configuracionLabel.Text = arrDatos(2)
                                                                                                                                                                                    End Sub).GetHtml()
                                                                                                                                                          End Sub)
                                                                                                                   End Sub)
                                                                                  configuracionGroupItem.Items.Add(Sub(configuracionItem)
                                                                                                                       configuracionItem.Caption = "Fecha eligibilidad:"
                                                                                                                       configuracionItem.SetNestedContent(Sub()
                                                                                                                                                              Html.DevExpress.Label(Sub(configuracionLabel)
                                                                                                                                                                                        configuracionLabel.Text = arrDatos(3)
                                                                                                                                                                                    End Sub).GetHtml()
                                                                                                                                                          End Sub)
                                                                                                                   End Sub)
                                                                                  configuracionGroupItem.Items.Add(Sub(configuracionItem)
                                                                                                                       configuracionItem.Caption = "Censo:"
                                                                                                                       configuracionItem.SetNestedContent(Sub()
                                                                                                                                                              Html.DevExpress.Label(Sub(configuracionLabel)
                                                                                                                                                                                        configuracionLabel.Text = arrDatos(4)
                                                                                                                                                                                    End Sub).GetHtml()
                                                                                                                                                          End Sub)
                                                                                                                   End Sub)
                                                                                  configuracionGroupItem.Items.Add(Sub(configuracionItem)
                                                                                                                       configuracionItem.ShowCaption = DefaultBoolean.False
                                                                                                                       configuracionItem.SetNestedContent(Sub()
                                                                                                                                                              Html.RenderPartial("Esquema/_GridViewPartialConfiguracionMontos", ViewData("intCodConfMon"))
                                                                                                                                                          End Sub)
                                                                                                                   End Sub)
                                                                              End Sub)
                               End Sub).GetHtml()

%>