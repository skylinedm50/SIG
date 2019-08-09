<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%

    Dim objReporCAI = New SIG.SIG.Areas.Corresponsabilidad.Models.cl_report_CAI
    Dim dtFechaActual As Date = Date.Now

    Html.DevExpress.FormLayout(Sub(configuracionFormLayout)
                                   configuracionFormLayout.Name = "AspxFormLayoutControlesReportes"
                                   configuracionFormLayout.ColCount = 3
                                   configuracionFormLayout.ControlStyle.CssClass = "FormLayoutControlesReportCAI"
                                   configuracionFormLayout.Items.AddGroupItem(Sub(configuracionGroupItem As MVCxFormLayoutGroup)
                                                                                  configuracionGroupItem.Caption = "Tipo de búsqueda"
                                                                                  configuracionGroupItem.GroupBoxDecoration = GroupBoxDecoration.HeadingLine
                                                                                  configuracionGroupItem.ColCount = 1
                                                                                  configuracionGroupItem.Items.Add(Sub(configuracionItem As MVCxFormLayoutItem)
                                                                                                                       configuracionItem.ShowCaption = DefaultBoolean.False
                                                                                                                       configuracionItem.HelpText = "Determina el tipo de busqueda que el sistema realizará en la base de datos, por alguna planilla ejecutada o por el estado actual de los registros en la base de datos, en otras palabras por planilla es una imagen de una determinada planilla y en el otro caso puede ser un dato hoy y mañana otro."
                                                                                                                       configuracionItem.SetNestedContent(Sub()
                                                                                                                                                              Html.DevExpress.RadioButtonList(Sub(configuracionRadioButton As RadioButtonListSettings)
                                                                                                                                                                                                  configuracionRadioButton.Name = "AspxRadioButtonListTipBusq"
                                                                                                                                                                                                  configuracionRadioButton.Properties.ClientInstanceName = "AspxRadioButtonListTipBusq"
                                                                                                                                                                                                  configuracionRadioButton.Properties.ValueField = "cod_tip_bus"
                                                                                                                                                                                                  configuracionRadioButton.Properties.TextField = "des_tip_bus"
                                                                                                                                                                                                  configuracionRadioButton.ControlStyle.Border.BorderWidth = 0
                                                                                                                                                                                                  configuracionRadioButton.SelectedIndex = 0
                                                                                                                                                                                                  configuracionRadioButton.Properties.ClientSideEvents.ValueChanged = "function(){ objReportCAI.SelectTipBusq(); }"
                                                                                                                                                                                              End Sub).BindList(objReporCAI.fnc_obtener_tipo_busqueda()).Render()
                                                                                                                                                          End Sub)
                                                                                                                       configuracionItem.Width = 200
                                                                                                                   End Sub)
                                                                                  configuracionGroupItem.Items.Add(Sub(configuracionItem As MVCxFormLayoutItem)
                                                                                                                       configuracionItem.Caption = "Departamento"
                                                                                                                       configuracionItem.SetNestedContent(Sub()
                                                                                                                                                              Html.RenderPartial("Localizacion_Sig/ViewPartialDepartamentoSIG")
                                                                                                                                                          End Sub)
                                                                                                                   End Sub)
                                                                                  configuracionGroupItem.Items.Add(Sub(configuracionItem As MVCxFormLayoutItem)
                                                                                                                       configuracionItem.Caption = "Municipio"
                                                                                                                       configuracionItem.SetNestedContent(Sub()
                                                                                                                                                              Html.RenderPartial("Localizacion_Sig/ViewPartialMunicipioSIG")
                                                                                                                                                          End Sub)
                                                                                                                   End Sub)
                                                                                  configuracionGroupItem.Items.Add(Sub(configuracionItem As MVCxFormLayoutItem)
                                                                                                                       configuracionItem.Caption = "Aldea"
                                                                                                                       configuracionItem.SetNestedContent(Sub()
                                                                                                                                                              Html.RenderPartial("Localizacion_Sig/ViewPartialAldeaSIG")
                                                                                                                                                          End Sub)
                                                                                                                   End Sub)
                                                                                  configuracionGroupItem.Items.Add(Sub(configuracionItem As MVCxFormLayoutItem)
                                                                                                                       configuracionItem.Caption = "Caserio"
                                                                                                                       configuracionItem.SetNestedContent(Sub()
                                                                                                                                                              Html.RenderPartial("Localizacion_Sig/ViewPartialCaserioSIG")
                                                                                                                                                          End Sub)
                                                                                                                       configuracionItem.HelpText = "En caso de no seleccionar ningún valor el sistema realizará una búsqueda de todo el país por lo que es posible que la respuesta sea lenta."
                                                                                                                   End Sub)
                                                                                  configuracionGroupItem.Items.Add(Sub(configuracionItem As MVCxFormLayoutItem)
                                                                                                                       configuracionItem.ShowCaption = DefaultBoolean.False
                                                                                                                       configuracionItem.SetNestedContent(Sub()
                                                                                                                                                              Html.DevExpress.Button(Sub(configuracionButton As ButtonSettings)
                                                                                                                                                                                         configuracionButton.Name = "AspxButtonBuscar"
                                                                                                                                                                                         configuracionButton.Text = "Buscar"
                                                                                                                                                                                         configuracionButton.UseSubmitBehavior = False
                                                                                                                                                                                         configuracionButton.ClientSideEvents.Click = "function(){objReportCAI.BuscarInfo();}"
                                                                                                                                                                                     End Sub).Render()
                                                                                                                                                          End Sub)

                                                                                                                   End Sub)
                                                                                  configuracionGroupItem.Items.Add(Sub(configuracionItem As MVCxFormLayoutItem)
                                                                                                                       configuracionItem.ShowCaption = DefaultBoolean.False
                                                                                                                       configuracionItem.SetNestedContent(Sub()
                                                                                                                                                              Html.DevExpress.Label(Sub(configuracionLabel As LabelSettings)
                                                                                                                                                                                        configuracionLabel.Name = "AspxLabelError"
                                                                                                                                                                                        configuracionLabel.Properties.ClientInstanceName = "AspxLabelError"
                                                                                                                                                                                        configuracionLabel.ControlStyle.ForeColor = System.Drawing.Color.Red
                                                                                                                                                                                        configuracionLabel.ControlStyle.Font.Bold = True
                                                                                                                                                                                    End Sub).GetHtml()
                                                                                                                                                          End Sub)

                                                                                                                   End Sub)
                                                                              End Sub)
                                   configuracionFormLayout.Items.AddGroupItem(Sub(configuracionGroupItem As MVCxFormLayoutGroup)
                                                                                  configuracionGroupItem.Caption = "Parámetros"
                                                                                  configuracionGroupItem.GroupBoxDecoration = GroupBoxDecoration.HeadingLine
                                                                                  configuracionGroupItem.ColCount = 1
                                                                                  configuracionGroupItem.Items.Add(Sub(configuracionItem As MVCxFormLayoutItem)
                                                                                                                       configuracionItem.Caption = "Año"
                                                                                                                       configuracionItem.SetNestedContent(Sub()
                                                                                                                                                              Html.DevExpress.SpinEdit(Sub(configuracionSpinEdit As SpinEditSettings)
                                                                                                                                                                                           configuracionSpinEdit.Name = "AspxSpinEditAño"
                                                                                                                                                                                           configuracionSpinEdit.Properties.ClientInstanceName = "AspxSpinEditAño"
                                                                                                                                                                                           configuracionSpinEdit.Properties.MaxValue = 2050
                                                                                                                                                                                           configuracionSpinEdit.Properties.MinValue = 2014
                                                                                                                                                                                           configuracionSpinEdit.Width = Unit.Percentage(100)
                                                                                                                                                                                           configuracionSpinEdit.Properties.ClientSideEvents.ValueChanged = "function(){ objReportCAI.SelectAño(); }"
                                                                                                                                                                                       End Sub).GetHtml()
                                                                                                                                                          End Sub)

                                                                                                                   End Sub)
                                                                                  configuracionGroupItem.Items.Add(Sub(configuracionItem As MVCxFormLayoutItem)
                                                                                                                       configuracionItem.ShowCaption = DefaultBoolean.False
                                                                                                                       configuracionItem.SetNestedContent(Sub()
                                                                                                                                                              Html.RenderAction("AspxGridViewPago", "Reportes", New With {Key .intAño = 0, Key .intCodTipBusq = 0})
                                                                                                                                                          End Sub)

                                                                                                                   End Sub)
                                                                                  configuracionGroupItem.Items.Add(Sub(configuracionItem As MVCxFormLayoutItem)
                                                                                                                       configuracionItem.ShowCaption = DefaultBoolean.False
                                                                                                                       configuracionItem.SetNestedContent(Sub()
                                                                                                                                                              Html.RenderAction("AspxGridViewCorres", "Reportes", New With {Key .intAño = 0, Key .intCodTipBusq = 0})
                                                                                                                                                          End Sub)

                                                                                                                   End Sub)
                                                                              End Sub)
                               End Sub).GetHtml()
%>