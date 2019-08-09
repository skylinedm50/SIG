<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    Html.DevExpress.RadioButtonList(Sub(configuracionRadioButton)
                                        configuracionRadioButton.Name = "AspxRadioBUttonSigno"
                                        configuracionRadioButton.Properties.Items.Add("Incluir", "Incluir")
                                        configuracionRadioButton.Properties.Items.Add("Excluir", "Excluir")
                                        configuracionRadioButton.Properties.Items(0).Selected = True
                                        configuracionRadioButton.Properties.RepeatDirection = RepeatDirection.Horizontal
                                    End Sub).Render()
%>