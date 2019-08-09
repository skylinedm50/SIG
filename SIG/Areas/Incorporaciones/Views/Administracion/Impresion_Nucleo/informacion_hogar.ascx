<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%    
    Html.DevExpress().FormLayout(Sub(setting)
        setting.Name = "settingFormulario" + Model.Rows(0).item(0).ToString()
        setting.ColCount = 1
        setting.Items.Add(Sub(item)
                              item.Caption = "Número del hogar"
                              item.SetNestedContent(Sub()
                                                        Html.DevExpress().Label(Sub(lblSetting)
                                                                                    lblSetting.Name = "Hogar" + Model.Rows(0).item(0).ToString()
                                                                                    lblSetting.Text = Model.Rows(0).item(0)
                                                                                End Sub).GetHtml()
                                                    End Sub)
                          End Sub)
        setting.Items.Add(Sub(item)
                              item.Caption = "Estado del hogar"
                              item.SetNestedContent(Sub()
                                                        Html.DevExpress().Label(Sub(lblSetting)
                                                                                    lblSetting.Name = "Estado" + Model.Rows(0).item(0).ToString()
                                                                                    lblSetting.Text = Model.Rows(0).item(17)
                                                                                End Sub).GetHtml()
                                                    End Sub)
                          End Sub)
        setting.Items.Add(Sub(item)
                              item.Caption = "Departamento"
                              item.SetNestedContent(Sub()
                                                        Html.DevExpress().Label(Sub(lblSetting)
                                                                                    lblSetting.Name = "departamento" + Model.Rows(0).item(0).ToString()
                                                                                    lblSetting.Text = Model.Rows(0).item(2)
                                                                                End Sub).GetHtml()
                                                    End Sub)
                          End Sub)
        setting.Items.Add(Sub(item)
                              item.Caption = "Municipio"
                              item.SetNestedContent(Sub()
                                                        Html.DevExpress().Label(Sub(lblSetting)
                                                                                    lblSetting.Name = "municipio" + Model.Rows(0).item(0).ToString()
                                                                                    lblSetting.Text = Model.Rows(0).item(3)
                                                                                End Sub).GetHtml()
                                                    End Sub)
                          End Sub)
        setting.Items.Add(Sub(item)
                              item.Caption = "Aldea"
                              item.SetNestedContent(Sub()
                                                        Html.DevExpress().Label(Sub(lblSetting)
                                                                                    lblSetting.Name = "aldea" + Model.Rows(0).item(0).ToString()
                                                                                    lblSetting.Text = Model.Rows(0).item(4)
                                                                                End Sub).GetHtml()
                                                    End Sub)
                          End Sub)
        setting.Items.Add(Sub(item)
                              item.Caption = "Caserio"
                              item.SetNestedContent(Sub()
                                                        Html.DevExpress().Label(Sub(lblSetting)
                                                                                    lblSetting.Name = "caserio" + Model.Rows(0).item(0).ToString()
                                                                                    lblSetting.Text = Model.Rows(0).item(5)
                                                                                End Sub).GetHtml()
                                                    End Sub)
                          End Sub)
        setting.Items.Add(Sub(item)
                              item.Caption = "Dirección"
                              item.SetNestedContent(Sub()
                                                        Html.DevExpress().Label(Sub(lblSetting)
                                                                                    lblSetting.Name = "direccion" + Model.Rows(0).item(0).ToString()
                                                                                    lblSetting.Text = Model.Rows(0).item(1)
                                                                                End Sub).GetHtml()
                                                    End Sub)
                          End Sub)
        setting.Items.Add(Sub(item)
                              item.Caption = "Número de telefono"
                              item.SetNestedContent(Sub()
                                                        Html.DevExpress().Label(Sub(lblSetting)
                                                                                    lblSetting.Name = "telefono" + Model.Rows(0).item(0).ToString()
                                                                                    lblSetting.Text = Model.Rows(0).item(6)
                                                                                End Sub).GetHtml()
                                                    End Sub)
                          End Sub)
        setting.Items.Add(Sub(item)
                              item.Caption = "Umbral de pobreza"
                              item.SetNestedContent(Sub()
                                                        Html.DevExpress().Label(Sub(lblSetting)
                                                                                    lblSetting.Name = "Umbral" + Model.Rows(0).item(0).ToString()
                                                                                    lblSetting.Text = Model.Rows(0).item(7)
                                                                                End Sub).GetHtml()
                                                    End Sub)
                          End Sub)
%>
<br /><br />
<%
    setting.Items.Add(Sub(item)
        item.ShowCaption = DefaultBoolean.False
        item.SetNestedContent(Sub()
                                                                                             %><br /><br /><%              
                                                                    Html.RenderAction("informacionPersonasHogar", "Administracion", New With {Key .Hogar = Model})
                                End Sub)
                            End Sub)
%><br /><br /><%
            setting.Items.Add(Sub(item)
                item.ShowCaption = DefaultBoolean.False
                item.SetNestedContent(Sub()
                                                                                             %><br /><br /><%
                                                                                     Html.DevExpress().Button(Sub(btnSetting)
                                                                                                                  btnSetting.Name = "btnSetting" + Model.Rows(0).item(0).ToString()
                                                                                                                  btnSetting.Text = "Exportar a PDF"
                                                                                                                  btnSetting.EnableTheming = False
                                                                                                                  btnSetting.ControlStyle.Font.Size = 9
                                                                                                                  btnSetting.ControlStyle.CssClass = "Boton"
                                                                                                                  btnSetting.ClientSideEvents.Click = "function(e,s){ fnc_exportarNucle_PDF(" + Model.Rows(0).item(0).ToString() + ")}"
                                                                                                              End Sub).GetHtml()
                                            End Sub)
                                        End Sub)
                                    End Sub).GetHtml()
%>