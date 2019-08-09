<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<fieldset>
    <legend>Parámetros de Filtración</legend>
    <% Html.DevExpress().FormLayout(
            Sub(settings)
                settings.Name = "frmLayoutAreaGeografica"
                settings.ColCount = 1
                settings.Items.AddGroupItem(
                    Sub(group)
                        group.Caption = "Área Geográfica"
                        group.Items.Add(
                            Sub(item)
                                item.Caption = "Departamentos"
                                item.SetNestedContent(
                                    Sub()
                                        Html.RenderAction("PartialDptoView")
                                    End Sub)
                            End Sub)
                        group.Items.Add(
                            Sub(item)
                                item.Caption = "Municipios"
                                item.SetNestedContent(
                                    Sub()
                                        Html.RenderPartial("PartialMuniView")
                                    End Sub)
                            End Sub)
                        group.Items.Add(
                            Sub(item)
                                item.Caption = "Aldeas"
                                item.SetNestedContent(
                                    Sub()
                                        Html.RenderPartial("PartialAldeaView")
                                    End Sub)
                            End Sub)
                    End Sub)
            End Sub).GetHtml()%>
    <% Html.DevExpress().FormLayout(
            Sub(settings)
                settings.Name = "frmLayoutOtrosParametros"
                settings.Width = Unit.Percentage(100)
                settings.ColCount = 1
                settings.Items.AddGroupItem(
                    Sub(group)
                        group.Caption = "Información Bancaria"
                        group.Items.Add(
                            Sub(item)
                                item.Caption = "Fondos"
                                item.SetNestedContent(
                                    Sub()
                                        Html.RenderAction("PartialFondoView")
                                    End Sub)
                            End Sub)
                        group.Items.Add(
                            Sub(item)
                                item.Caption = "Banco"
                                item.SetNestedContent(
                                    Sub()
                                        Html.RenderAction("PartialBancoView")
                                    End Sub)
                            End Sub)
                        group.Items.Add(
                            Sub(item)
                                item.Caption = "Sucursal"
                                item.SetNestedContent(
                                    Sub()
                                        Html.RenderPartial("PartialSucursalView")
                                    End Sub)
                            End Sub)
                    End Sub)
            End Sub).GetHtml()%>
</fieldset>