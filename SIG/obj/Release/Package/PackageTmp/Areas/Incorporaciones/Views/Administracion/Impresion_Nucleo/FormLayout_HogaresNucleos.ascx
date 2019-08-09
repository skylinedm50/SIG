<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    Html.DevExpress().FormLayout(Sub(formSetting)
                                     formSetting.Name = "setting"
                                     formSetting.ColCount = 2
                                     formSetting.Items.Add(Sub(columna)
                                                               columna.ShowCaption = DefaultBoolean.False
                                                               columna.SetNestedContent(Sub()
                                                                                            Html.RenderAction("informacion_hogar", "Administracion", New With {Key .Hogar = ViewData("Hogar1")})
                                                                                        End Sub)
                                                           End Sub)

                                     'formSetting.Items.Add(Sub(columna)
                                     '                          columna.ShowCaption = DefaultBoolean.False
                                     '                          columna.SetNestedContent(Sub()
                                     '                                                       If (ViewData("Hogar2") IsNot Nothing) Then
                                     '                                                           Html.RenderAction("informacion_hogar", "Administracion", New With {Key .Hogar = ViewData("Hogar2")})
                                     '                                                       End If

                                     '                                                   End Sub)
                                     '                      End Sub)
                                 End Sub).GetHtml()
    %>

