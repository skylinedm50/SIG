<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% 
    Dim objReporCAI = New SIG.SIG.Areas.Corresponsabilidad.Models.cl_report_CAI
    Dim intTipBusq As Integer = CInt(ViewData("intCodTipBusq"))


    Html.DevExpress.GridView(Sub(configuracionGridView As GridViewSettings)
                                 configuracionGridView.Name = "AspxGridViewCAI"
                                 configuracionGridView.CallbackRouteValues = New With {Key .Controller = "Reportes",
                                                                                       Key .Action = "AspxGridViewCAI",
                                                                                       Key .intCodTipBusq = ViewData("intCodTipBusq"),
                                                                                       Key .intAño = ViewData("intAño"),
                                                                                       Key .strCodPago = ViewData("strCodPago"),
                                                                                       Key .strCodCorres = ViewData("strCodCorres"),
                                                                                       Key .strCodDep = ViewData("strCodDep"),
                                                                                       Key .strCodMun = ViewData("strCodMun"),
                                                                                       Key .strCodAld = ViewData("strCodAld"),
                                                                                       Key .strCodCas = ViewData("strCodCas")}

                                 configuracionGridView.Caption = "Detalle Cumplimiento, Incumplimiento y Apercibimiento (CAI)"
                                 If intTipBusq = 0 Then
                                     configuracionGridView.KeyFieldName = "tit_codigo"
                                 Else
                                     configuracionGridView.KeyFieldName = "hab_per_persona"
                                 End If

                                 configuracionGridView.SettingsPager.Visible = True
                                 configuracionGridView.Settings.ShowGroupPanel = True
                                 configuracionGridView.Settings.ShowFilterRow = True
                                 configuracionGridView.ControlStyle.CssClass = "GridViewCorres"

                                 configuracionGridView.ClientSideEvents.BeginCallback = "function(s, e){ " &
                                                                                        "	e.customArgs['intCodTipBusq'] = objReportCAI.SessionData.TipoBusq; " &
                                                                                        " 	e.customArgs['intAño'] = objReportCAI.SessionData.Año; " &
                                                                                        " 	e.customArgs['strCodPago'] = objReportCAI.SessionData.Pago; " &
                                                                                        " 	e.customArgs['strCodCorres'] = objReportCAI.SessionData.Corres; " &
                                                                                        " 	e.customArgs['strCodDep'] = objReportCAI.SessionData.Dep; " &
                                                                                        " 	e.customArgs['strCodMun'] = objReportCAI.SessionData.Muni; " &
                                                                                        " 	e.customArgs['trCodAld'] = objReportCAI.SessionData.Ald; " &
                                                                                        " 	e.customArgs['strCodCas'] = objReportCAI.SessionData.Cas; " &
                                                                                        "} "
                                 configuracionGridView.ClientSideEvents.EndCallback = "function(){ AspxButtonBuscar.SetEnabled(true); }"
                                 configuracionGridView.SettingsPager.PageSizeItemSettings.Visible = True
                                 configuracionGridView.SettingsPager.PageSize = 18
                                 configuracionGridView.SettingsPager.PageSizeItemSettings.Items = {"20", "40", "70", "100"}
                                 configuracionGridView.Width = Unit.Percentage(100)
                                 configuracionGridView.CommandColumn.Visible = True
                                 configuracionGridView.CommandColumn.ShowClearFilterButton = True

                                 configuracionGridView.Columns.AddBand(Sub(configuracionAddBand As MVCxGridViewBandColumn)
                                                                           configuracionAddBand.Caption = "Ubicación Geográfica"
                                                                           configuracionAddBand.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                                                                           configuracionAddBand.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                configuracionColumn.FieldName = "desc_departamento"
                                                                                                                configuracionColumn.Caption = "Departamento"
                                                                                                                configuracionColumn.Width = 200
                                                                                                            End Sub)
                                                                           configuracionAddBand.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                configuracionColumn.FieldName = "desc_municipio"
                                                                                                                configuracionColumn.Caption = "Municipio"
                                                                                                                configuracionColumn.Width = 200
                                                                                                            End Sub)
                                                                           configuracionAddBand.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                configuracionColumn.FieldName = "desc_aldea"
                                                                                                                configuracionColumn.Caption = "Aldea"
                                                                                                                configuracionColumn.Width = 250
                                                                                                            End Sub)
                                                                           configuracionAddBand.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                configuracionColumn.FieldName = "desc_caserio"
                                                                                                                configuracionColumn.Caption = "Caserio"
                                                                                                                configuracionColumn.Width = 250
                                                                                                            End Sub)
                                                                       End Sub)
                                 configuracionGridView.Columns.AddBand(Sub(configuracionAddBand As MVCxGridViewBandColumn)
                                                                           configuracionAddBand.Caption = "Información Hogar"
                                                                           configuracionAddBand.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                                                                           configuracionAddBand.Columns.AddBand(Sub(configuracionSubBand As MVCxGridViewBandColumn)
                                                                                                                    configuracionSubBand.Caption = "Titular"
                                                                                                                    configuracionSubBand.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                                                                                                                    configuracionSubBand.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                         configuracionColumn.FieldName = "tit_hogar"
                                                                                                                                                         configuracionColumn.Caption = "Hogar"

                                                                                                                                                         configuracionColumn.ColumnType = MVCxGridViewColumnType.SpinEdit
                                                                                                                                                         Dim SpinEditPropiedades As SpinEditProperties = configuracionColumn.PropertiesEdit
                                                                                                                                                         SpinEditPropiedades.MinValue = 0
                                                                                                                                                         SpinEditPropiedades.MaxValue = 9000000
                                                                                                                                                         configuracionColumn.Width = 100
                                                                                                                                                     End Sub)
                                                                                                                    configuracionSubBand.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                         configuracionColumn.FieldName = "hog_rup_hogar"
                                                                                                                                                         configuracionColumn.Caption = "Hogar RUP"

                                                                                                                                                         configuracionColumn.ColumnType = MVCxGridViewColumnType.SpinEdit
                                                                                                                                                         Dim SpinEditPropiedades As SpinEditProperties = configuracionColumn.PropertiesEdit
                                                                                                                                                         SpinEditPropiedades.MinValue = 0
                                                                                                                                                         SpinEditPropiedades.MaxValue = 9000000
                                                                                                                                                         configuracionColumn.Width = 100
                                                                                                                                                     End Sub)
                                                                                                                    configuracionSubBand.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                         configuracionColumn.FieldName = "tit_identidad"
                                                                                                                                                         configuracionColumn.Caption = "Identidad"
                                                                                                                                                         configuracionColumn.Width = 110
                                                                                                                                                     End Sub)
                                                                                                                    configuracionSubBand.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                         configuracionColumn.FieldName = "tit_nombre1"
                                                                                                                                                         configuracionColumn.Caption = "Primer Nombre"
                                                                                                                                                         configuracionColumn.Width = 110
                                                                                                                                                     End Sub)
                                                                                                                    configuracionSubBand.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                         configuracionColumn.FieldName = "tit_nombre2"
                                                                                                                                                         configuracionColumn.Caption = "Segundo Nombre"
                                                                                                                                                         configuracionColumn.Width = 110
                                                                                                                                                     End Sub)
                                                                                                                    configuracionSubBand.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                         configuracionColumn.FieldName = "tit_apellido1"
                                                                                                                                                         configuracionColumn.Caption = "Primer Apellido"
                                                                                                                                                         configuracionColumn.Width = 110
                                                                                                                                                     End Sub)
                                                                                                                    configuracionSubBand.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                         configuracionColumn.FieldName = "tit_apellido2"
                                                                                                                                                         configuracionColumn.Caption = "Segundo Apellido"
                                                                                                                                                         configuracionColumn.Width = 110
                                                                                                                                                     End Sub)
                                                                                                                End Sub)
                                                                           configuracionAddBand.Columns.AddBand(Sub(configuracionSubBand As MVCxGridViewBandColumn)
                                                                                                                    configuracionSubBand.Caption = "Beneficiario"
                                                                                                                    configuracionSubBand.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                                                                                                                    configuracionSubBand.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                         configuracionColumn.FieldName = "hab_identidad"
                                                                                                                                                         configuracionColumn.Caption = "Identidad"
                                                                                                                                                         configuracionColumn.Width = 110
                                                                                                                                                     End Sub)
                                                                                                                    configuracionSubBand.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                         configuracionColumn.FieldName = "hab_per_persona"
                                                                                                                                                         configuracionColumn.Caption = "Código"

                                                                                                                                                         configuracionColumn.ColumnType = MVCxGridViewColumnType.SpinEdit
                                                                                                                                                         Dim SpinEditPropiedades As SpinEditProperties = configuracionColumn.PropertiesEdit
                                                                                                                                                         SpinEditPropiedades.MinValue = 0
                                                                                                                                                         SpinEditPropiedades.MaxValue = 9000000
                                                                                                                                                         configuracionColumn.Width = 100
                                                                                                                                                     End Sub)
                                                                                                                    configuracionSubBand.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                         configuracionColumn.FieldName = "hab_nombre1"
                                                                                                                                                         configuracionColumn.Caption = "Primer Nombre"
                                                                                                                                                         configuracionColumn.Width = 110
                                                                                                                                                     End Sub)
                                                                                                                    configuracionSubBand.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                         configuracionColumn.FieldName = "hab_nombre2"
                                                                                                                                                         configuracionColumn.Caption = "Segundo Nombre"
                                                                                                                                                         configuracionColumn.Width = 110
                                                                                                                                                     End Sub)
                                                                                                                    configuracionSubBand.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                         configuracionColumn.FieldName = "hab_apellido1"
                                                                                                                                                         configuracionColumn.Caption = "Primer Apellido"
                                                                                                                                                         configuracionColumn.Width = 110
                                                                                                                                                     End Sub)
                                                                                                                    configuracionSubBand.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                         configuracionColumn.FieldName = "hab_apellido2"
                                                                                                                                                         configuracionColumn.Caption = "Segundo Apellido"
                                                                                                                                                         configuracionColumn.Width = 110
                                                                                                                                                     End Sub)
                                                                                                                    configuracionSubBand.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                         configuracionColumn.FieldName = "cod_ciclo_edad"
                                                                                                                                                         configuracionColumn.Caption = "Ciclo Elegibilidad"

                                                                                                                                                         configuracionColumn.ColumnType = MVCxGridViewColumnType.ComboBox
                                                                                                                                                         Dim ComboBoxPropiedades As ComboBoxProperties = configuracionColumn.PropertiesEdit
                                                                                                                                                         ComboBoxPropiedades.DataSource = objReporCAI.fnc_obtener_ciclo_edades()
                                                                                                                                                         ComboBoxPropiedades.ValueField = "cod_ciclo_edad"
                                                                                                                                                         ComboBoxPropiedades.TextField = "ciclo_edad_descripcion"
                                                                                                                                                         ComboBoxPropiedades.ValueType = GetType(Integer)
                                                                                                                                                         configuracionColumn.Width = 200
                                                                                                                                                     End Sub)
                                                                                                                    configuracionSubBand.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                         configuracionColumn.FieldName = "hab_sexo"
                                                                                                                                                         configuracionColumn.Caption = "Sexo"

                                                                                                                                                         configuracionColumn.ColumnType = MVCxGridViewColumnType.ComboBox
                                                                                                                                                         Dim ComboBoxPropiedades As ComboBoxProperties = configuracionColumn.PropertiesEdit
                                                                                                                                                         ComboBoxPropiedades.DataSource = objReporCAI.fnc_obtener_genero_sexual()
                                                                                                                                                         ComboBoxPropiedades.ValueField = "hab_sexo"
                                                                                                                                                         ComboBoxPropiedades.TextField = "hab_sexo_descripcion"
                                                                                                                                                         ComboBoxPropiedades.ValueType = GetType(Integer)
                                                                                                                                                         configuracionColumn.Width = 100
                                                                                                                                                     End Sub)
                                                                                                                End Sub)
                                                                           If intTipBusq = 0 Then
                                                                               configuracionAddBand.Columns.AddBand(Sub(configuracionSubBand As MVCxGridViewBandColumn)
                                                                                                                        configuracionSubBand.Caption = "Estado Planilla"
                                                                                                                        configuracionSubBand.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                                                                                                                        configuracionSubBand.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                             configuracionColumn.FieldName = "pag_nombre"
                                                                                                                                                             configuracionColumn.Caption = "Pago"
                                                                                                                                                             configuracionColumn.Width = 300
                                                                                                                                                         End Sub)
                                                                                                                        configuracionSubBand.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                             configuracionColumn.FieldName = "tit_planilla"
                                                                                                                                                             configuracionColumn.Caption = "Estado Programado"

                                                                                                                                                             configuracionColumn.ColumnType = MVCxGridViewColumnType.ComboBox
                                                                                                                                                             Dim ComboBoxPropiedades As ComboBoxProperties = configuracionColumn.PropertiesEdit
                                                                                                                                                             ComboBoxPropiedades.DataSource = objReporCAI.fnc_obtener_estado_programado()
                                                                                                                                                             ComboBoxPropiedades.ValueField = "tit_planilla"
                                                                                                                                                             ComboBoxPropiedades.TextField = "tit_planilla_descripcion"
                                                                                                                                                             ComboBoxPropiedades.ValueType = GetType(Integer)
                                                                                                                                                             configuracionColumn.Width = 130
                                                                                                                                                         End Sub)
                                                                                                                        configuracionSubBand.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                             configuracionColumn.FieldName = "tit_proy_corta"
                                                                                                                                                             configuracionColumn.Caption = "Razón de Exclusión"
                                                                                                                                                             configuracionColumn.Width = 300
                                                                                                                                                         End Sub)
                                                                                                                        configuracionSubBand.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                             configuracionColumn.FieldName = "tit_cobro"
                                                                                                                                                             configuracionColumn.Caption = "Estado Cobro"

                                                                                                                                                             configuracionColumn.ColumnType = MVCxGridViewColumnType.ComboBox
                                                                                                                                                             Dim ComboBoxPropiedades As ComboBoxProperties = configuracionColumn.PropertiesEdit
                                                                                                                                                             ComboBoxPropiedades.DataSource = objReporCAI.fnc_obtener_estado_cobro()
                                                                                                                                                             ComboBoxPropiedades.ValueField = "tit_cobro"
                                                                                                                                                             ComboBoxPropiedades.TextField = "tit_cobro_descripcion"
                                                                                                                                                             ComboBoxPropiedades.ValueType = GetType(Integer)
                                                                                                                                                             configuracionColumn.Width = 100
                                                                                                                                                         End Sub)
                                                                                                                        configuracionSubBand.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                             configuracionColumn.FieldName = "tit_monto_total_red"
                                                                                                                                                             configuracionColumn.Caption = "Monto"

                                                                                                                                                             configuracionColumn.ColumnType = MVCxGridViewColumnType.SpinEdit
                                                                                                                                                             Dim SpinEditPropiedades As SpinEditProperties = configuracionColumn.PropertiesEdit
                                                                                                                                                             SpinEditPropiedades.MinValue = 0
                                                                                                                                                             SpinEditPropiedades.MaxValue = 9000000
                                                                                                                                                             SpinEditPropiedades.DisplayFormatString = "c"
                                                                                                                                                             configuracionColumn.Width = 100
                                                                                                                                                         End Sub)
                                                                                                                    End Sub)
                                                                           End If
                                                                       End Sub)
                                 configuracionGridView.Columns.AddBand(Sub(configuracionAddBand As MVCxGridViewBandColumn)
                                                                           configuracionAddBand.Caption = "Cumplimiento"
                                                                           configuracionAddBand.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                                                                           configuracionAddBand.Columns.AddBand(Sub(configuracionSubBand As MVCxGridViewBandColumn)
                                                                                                                    configuracionSubBand.Caption = "Salud"
                                                                                                                    configuracionSubBand.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                                                                                                                    configuracionSubBand.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                         configuracionColumn.FieldName = "cumple_salud"
                                                                                                                                                         configuracionColumn.Caption = "Cumple"
                                                                                                                                                         configuracionColumn.Width = 60
                                                                                                                                                     End Sub)
                                                                                                                    configuracionSubBand.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                         configuracionColumn.FieldName = "cumple_salud_ciclo_1"
                                                                                                                                                         configuracionColumn.Caption = "Ciclo 1"
                                                                                                                                                         configuracionColumn.Width = 60
                                                                                                                                                     End Sub)
                                                                                                                    configuracionSubBand.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                         configuracionColumn.FieldName = "cumple_salud_ciclo_2"
                                                                                                                                                         configuracionColumn.Caption = "Ciclo 2"
                                                                                                                                                         configuracionColumn.Width = 60
                                                                                                                                                     End Sub)
                                                                                                                End Sub)
                                                                           If intTipBusq = 0 Then
                                                                               configuracionAddBand.Columns.AddBand(Sub(configuracionSubBand As MVCxGridViewBandColumn)
                                                                                                                        configuracionSubBand.Caption = "Educación"
                                                                                                                        configuracionSubBand.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                                                                                                                        configuracionSubBand.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                             configuracionColumn.FieldName = "cumple_educ"
                                                                                                                                                             configuracionColumn.Caption = "Cumple"
                                                                                                                                                             configuracionColumn.Width = 60
                                                                                                                                                         End Sub)
                                                                                                                        configuracionSubBand.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                             configuracionColumn.FieldName = "cumple_educ_ciclo_1_2"
                                                                                                                                                             configuracionColumn.Caption = "Ciclo 1 y 2"
                                                                                                                                                             configuracionColumn.Width = 70
                                                                                                                                                         End Sub)
                                                                                                                        configuracionSubBand.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                             configuracionColumn.FieldName = "cumple_educ_ciclo_3"
                                                                                                                                                             configuracionColumn.Caption = "Ciclo 3"
                                                                                                                                                             configuracionColumn.Width = 60
                                                                                                                                                         End Sub)
                                                                                                                    End Sub)
                                                                           Else
                                                                               configuracionAddBand.Columns.AddBand(Sub(configuracionSubBand As MVCxGridViewBandColumn)
                                                                                                                        configuracionSubBand.Caption = "Educación"
                                                                                                                        configuracionSubBand.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                                                                                                                        configuracionSubBand.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                             configuracionColumn.FieldName = "cumple_educ"
                                                                                                                                                             configuracionColumn.Caption = "Cumple"
                                                                                                                                                             configuracionColumn.Width = 60
                                                                                                                                                         End Sub)
                                                                                                                        configuracionSubBand.Columns.AddBand(Sub(configuracionSubBandSub As MVCxGridViewBandColumn)
                                                                                                                                                                 configuracionSubBandSub.Caption = "Matricula"
                                                                                                                                                                 configuracionSubBandSub.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                                                                                                                                                                 configuracionSubBandSub.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                                                                         configuracionColumn.FieldName = "cumple_educ_mat"
                                                                                                                                                                                                         configuracionColumn.Caption = "Cumple"
                                                                                                                                                                                                         configuracionColumn.Width = 60
                                                                                                                                                                                                     End Sub)
                                                                                                                                                                 configuracionSubBandSub.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                                                                         configuracionColumn.FieldName = "cumple_educ_mat_ciclo_1_2"
                                                                                                                                                                                                         configuracionColumn.Caption = "Ciclo 1 y 2"
                                                                                                                                                                                                         configuracionColumn.Width = 70
                                                                                                                                                                                                     End Sub)
                                                                                                                                                                 configuracionSubBandSub.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                                                                         configuracionColumn.FieldName = "cumple_educ_mat_ciclo_3"
                                                                                                                                                                                                         configuracionColumn.Caption = "Ciclo 3"
                                                                                                                                                                                                         configuracionColumn.Width = 60
                                                                                                                                                                                                     End Sub)
                                                                                                                                                             End Sub)
                                                                                                                        configuracionSubBand.Columns.AddBand(Sub(configuracionSubBandSub As MVCxGridViewBandColumn)
                                                                                                                                                                 configuracionSubBandSub.Caption = "Asistencia"
                                                                                                                                                                 configuracionSubBandSub.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                                                                                                                                                                 configuracionSubBandSub.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                                                                         configuracionColumn.FieldName = "cumple_educ_ina"
                                                                                                                                                                                                         configuracionColumn.Caption = "Cumple"
                                                                                                                                                                                                         configuracionColumn.Width = 60
                                                                                                                                                                                                     End Sub)
                                                                                                                                                                 configuracionSubBandSub.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                                                                         configuracionColumn.FieldName = "cumple_educ_ina_ciclo_1_2"
                                                                                                                                                                                                         configuracionColumn.Caption = "Ciclo 1 y 2"
                                                                                                                                                                                                         configuracionColumn.Width = 70
                                                                                                                                                                                                     End Sub)
                                                                                                                                                                 configuracionSubBandSub.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                                                                         configuracionColumn.FieldName = "cumple_educ_ina_ciclo_3"
                                                                                                                                                                                                         configuracionColumn.Caption = "Ciclo 3"
                                                                                                                                                                                                         configuracionColumn.Width = 60
                                                                                                                                                                                                     End Sub)
                                                                                                                                                             End Sub)
                                                                                                                    End Sub)
                                                                           End If
                                                                       End Sub)
                                 If intTipBusq = 0 Then
                                     configuracionGridView.Columns.AddBand(Sub(configuracionAddBand As MVCxGridViewBandColumn)
                                                                               configuracionAddBand.Caption = "Apercibimiento"
                                                                               configuracionAddBand.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                                                                               configuracionAddBand.Columns.AddBand(Sub(configuracionSubBand As MVCxGridViewBandColumn)
                                                                                                                        configuracionSubBand.Caption = "Salud"
                                                                                                                        configuracionSubBand.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                                                                                                                        configuracionSubBand.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                             configuracionColumn.FieldName = "aperci_salud_ciclo_1"
                                                                                                                                                             configuracionColumn.Caption = "Ciclo 1"
                                                                                                                                                             configuracionColumn.Width = 60
                                                                                                                                                         End Sub)
                                                                                                                        configuracionSubBand.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                             configuracionColumn.FieldName = "aperci_salud_ciclo_2"
                                                                                                                                                             configuracionColumn.Caption = "Ciclo 2"
                                                                                                                                                             configuracionColumn.Width = 60
                                                                                                                                                         End Sub)
                                                                                                                    End Sub)
                                                                               configuracionAddBand.Columns.AddBand(Sub(configuracionSubBand As MVCxGridViewBandColumn)
                                                                                                                        configuracionSubBand.Caption = "Educación"
                                                                                                                        configuracionSubBand.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                                                                                                                        configuracionSubBand.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                             configuracionColumn.FieldName = "aperci_educ_ciclo_1_2"
                                                                                                                                                             configuracionColumn.Caption = "Ciclo 1 y 2"
                                                                                                                                                             configuracionColumn.Width = 70
                                                                                                                                                         End Sub)
                                                                                                                        configuracionSubBand.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                             configuracionColumn.FieldName = "aperci_educ_ciclo_3"
                                                                                                                                                             configuracionColumn.Caption = "Ciclo 3"
                                                                                                                                                             configuracionColumn.Width = 60
                                                                                                                                                         End Sub)
                                                                                                                    End Sub)
                                                                           End Sub)
                                 End If
                                 configuracionGridView.Columns.AddBand(Sub(configuracionAddBand As MVCxGridViewBandColumn)
                                                                           configuracionAddBand.Caption = "Incumplimiento"
                                                                           configuracionAddBand.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                                                                           configuracionAddBand.Columns.AddBand(Sub(configuracionSubBand As MVCxGridViewBandColumn)
                                                                                                                    configuracionSubBand.Caption = "Salud"
                                                                                                                    configuracionSubBand.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                         configuracionColumn.FieldName = "no_cumple_salud_ciclo_1"
                                                                                                                                                         configuracionColumn.Caption = "Ciclo 1"
                                                                                                                                                         configuracionColumn.Width = 60
                                                                                                                                                     End Sub)
                                                                                                                    configuracionSubBand.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                         configuracionColumn.FieldName = "no_cumple_salud_ciclo_2"
                                                                                                                                                         configuracionColumn.Caption = "Ciclo 2"
                                                                                                                                                         configuracionColumn.Width = 60
                                                                                                                                                     End Sub)
                                                                                                                End Sub)
                                                                           configuracionAddBand.Columns.AddBand(Sub(configuracionSubBand As MVCxGridViewBandColumn)
                                                                                                                    configuracionSubBand.Caption = "Educación"
                                                                                                                    configuracionSubBand.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                                                                                                                    configuracionSubBand.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                         configuracionColumn.FieldName = "no_cumple_educ_ciclo_1_2"
                                                                                                                                                         configuracionColumn.Caption = "Ciclo 1 y 2"
                                                                                                                                                         configuracionColumn.Width = 70
                                                                                                                                                     End Sub)
                                                                                                                    configuracionSubBand.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                         configuracionColumn.FieldName = "no_cumple_educ_ciclo_3"
                                                                                                                                                         configuracionColumn.Caption = "Ciclo 3"
                                                                                                                                                         configuracionColumn.Width = 60
                                                                                                                                                     End Sub)
                                                                                                                End Sub)
                                                                       End Sub)
                                 If intTipBusq = 0 Then
                                     configuracionGridView.Columns.AddBand(Sub(configuracionAddBand As MVCxGridViewBandColumn)
                                                                               configuracionAddBand.Caption = "Corresponsabilidad"
                                                                               configuracionAddBand.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                                                                               configuracionAddBand.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                    configuracionColumn.FieldName = "corr_codigo"
                                                                                                                    configuracionColumn.Caption = "Nombre"

                                                                                                                    configuracionColumn.ColumnType = MVCxGridViewColumnType.ComboBox
                                                                                                                    Dim ComboBoxPropiedades As ComboBoxProperties = configuracionColumn.PropertiesEdit
                                                                                                                    ComboBoxPropiedades.DataSource = objReporCAI.fnc_obtener_corresponsabilidades()
                                                                                                                    ComboBoxPropiedades.ValueField = "corr_codigo"
                                                                                                                    ComboBoxPropiedades.TextField = "corr_nombre"
                                                                                                                    ComboBoxPropiedades.ValueType = GetType(Integer)
                                                                                                                    configuracionColumn.Width = 250
                                                                                                                End Sub)
                                                                               configuracionAddBand.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                    configuracionColumn.FieldName = "cod_centro"
                                                                                                                    configuracionColumn.Caption = "Código Centro"
                                                                                                                    configuracionColumn.Width = 100
                                                                                                                End Sub)
                                                                               configuracionAddBand.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                    configuracionColumn.FieldName = "nombre_centro"
                                                                                                                    configuracionColumn.Caption = "Nombre Centro"
                                                                                                                    configuracionColumn.Width = 300
                                                                                                                End Sub)
                                                                               configuracionAddBand.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                    configuracionColumn.FieldName = "habc_num_visitas_cs"
                                                                                                                    configuracionColumn.Caption = "Cantidad Visitas"

                                                                                                                    configuracionColumn.ColumnType = MVCxGridViewColumnType.SpinEdit
                                                                                                                    Dim SpinEditPropiedades As SpinEditProperties = configuracionColumn.PropertiesEdit
                                                                                                                    SpinEditPropiedades.MinValue = 0
                                                                                                                    SpinEditPropiedades.MaxValue = 9000000
                                                                                                                    configuracionColumn.Width = 100
                                                                                                                End Sub)
                                                                           End Sub)
                                 Else
                                     configuracionGridView.Columns.AddBand(Sub(configuracionAddBand As MVCxGridViewBandColumn)
                                                                               configuracionAddBand.Caption = "Corresponsabilidad"
                                                                               configuracionAddBand.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                                                                               configuracionAddBand.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                    configuracionColumn.FieldName = "corresp"
                                                                                                                    configuracionColumn.Caption = "Corresponsabilidad Validada"
                                                                                                                    configuracionColumn.Width = 300
                                                                                                                End Sub)
                                                                               configuracionAddBand.Columns.AddBand(Sub(configuracionSubBand As MVCxGridViewBandColumn)
                                                                                                                        configuracionSubBand.Caption = "Salud"
                                                                                                                        configuracionSubBand.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                                                                                                                        configuracionSubBand.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                             configuracionColumn.FieldName = "cod_cen_sal"
                                                                                                                                                             configuracionColumn.Caption = "Código"
                                                                                                                                                             configuracionColumn.Width = 100
                                                                                                                                                         End Sub)
                                                                                                                        configuracionSubBand.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                             configuracionColumn.FieldName = "cod_rup_cen_sal"
                                                                                                                                                             configuracionColumn.Caption = "Código RENPI"
                                                                                                                                                             configuracionColumn.Width = 100
                                                                                                                                                         End Sub)
                                                                                                                        configuracionSubBand.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                             configuracionColumn.FieldName = "nom_cen_sal"
                                                                                                                                                             configuracionColumn.Caption = "Nombre Centro"
                                                                                                                                                             configuracionColumn.Width = 300
                                                                                                                                                         End Sub)
                                                                                                                        configuracionSubBand.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                             configuracionColumn.FieldName = "habc_num_visitas_cs"
                                                                                                                                                             configuracionColumn.Caption = "Cantidad Visitas"

                                                                                                                                                             configuracionColumn.ColumnType = MVCxGridViewColumnType.SpinEdit
                                                                                                                                                             Dim SpinEditPropiedades As SpinEditProperties = configuracionColumn.PropertiesEdit
                                                                                                                                                             SpinEditPropiedades.MinValue = 0
                                                                                                                                                             SpinEditPropiedades.MaxValue = 9000000
                                                                                                                                                             configuracionColumn.Width = 100
                                                                                                                                                         End Sub)
                                                                                                                    End Sub)
                                                                               configuracionAddBand.Columns.AddBand(Sub(configuracionSubBand As MVCxGridViewBandColumn)
                                                                                                                        configuracionSubBand.Caption = "Educación"
                                                                                                                        configuracionSubBand.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                                                                                                                        configuracionSubBand.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                             configuracionColumn.FieldName = "cod_cen_edu"
                                                                                                                                                             configuracionColumn.Caption = "Código"
                                                                                                                                                             configuracionColumn.Width = 100
                                                                                                                                                         End Sub)
                                                                                                                        configuracionSubBand.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                             configuracionColumn.FieldName = "nom_cen_edu"
                                                                                                                                                             configuracionColumn.Caption = "Nombre Centro"
                                                                                                                                                             configuracionColumn.Width = 300
                                                                                                                                                         End Sub)
                                                                                                                        configuracionSubBand.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                             configuracionColumn.FieldName = "cod_gra"
                                                                                                                                                             configuracionColumn.Caption = "Grado"

                                                                                                                                                             configuracionColumn.ColumnType = MVCxGridViewColumnType.ComboBox
                                                                                                                                                             Dim ComboBoxPropiedades As ComboBoxProperties = configuracionColumn.PropertiesEdit
                                                                                                                                                             ComboBoxPropiedades.DataSource = objReporCAI.fnc_obtener_grados()
                                                                                                                                                             ComboBoxPropiedades.ValueField = "cod_gra"
                                                                                                                                                             ComboBoxPropiedades.TextField = "nom_gra"
                                                                                                                                                             ComboBoxPropiedades.ValueType = GetType(Integer)
                                                                                                                                                             configuracionColumn.Width = 250
                                                                                                                                                         End Sub)
                                                                                                                        configuracionSubBand.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                             configuracionColumn.FieldName = "dia_ina_det_ina"
                                                                                                                                                             configuracionColumn.Caption = "Días Inasistidos"

                                                                                                                                                             configuracionColumn.ColumnType = MVCxGridViewColumnType.SpinEdit
                                                                                                                                                             Dim SpinEditPropiedades As SpinEditProperties = configuracionColumn.PropertiesEdit
                                                                                                                                                             SpinEditPropiedades.MinValue = 0
                                                                                                                                                             SpinEditPropiedades.MaxValue = 9000000
                                                                                                                                                             configuracionColumn.Width = 100
                                                                                                                                                         End Sub)
                                                                                                                    End Sub)
                                                                           End Sub)
                                 End If
                                 configuracionGridView.Settings.HorizontalScrollBarMode = ScrollBarMode.Auto
                                 If intTipBusq = 0 Then
                                     configuracionGridView.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "tit_monto_total_red").DisplayFormat = "c"
                                     configuracionGridView.TotalSummary.Add(DevExpress.Data.SummaryItemType.Count, "aperci_salud_ciclo_1").DisplayFormat = "n0"
                                     configuracionGridView.TotalSummary.Add(DevExpress.Data.SummaryItemType.Count, "aperci_salud_ciclo_2").DisplayFormat = "n0"
                                     configuracionGridView.TotalSummary.Add(DevExpress.Data.SummaryItemType.Count, "aperci_educ_ciclo_1_2").DisplayFormat = "n0"
                                     configuracionGridView.TotalSummary.Add(DevExpress.Data.SummaryItemType.Count, "aperci_educ_ciclo_3").DisplayFormat = "n0"
                                 Else
                                     configuracionGridView.TotalSummary.Add(DevExpress.Data.SummaryItemType.Count, "cumple_educ_mat").DisplayFormat = "n0"
                                     configuracionGridView.TotalSummary.Add(DevExpress.Data.SummaryItemType.Count, "cumple_educ_ina").DisplayFormat = "n0"
                                     configuracionGridView.TotalSummary.Add(DevExpress.Data.SummaryItemType.Count, "cumple_educ_mat_ciclo_1_2").DisplayFormat = "n0"
                                     configuracionGridView.TotalSummary.Add(DevExpress.Data.SummaryItemType.Count, "cumple_educ_mat_ciclo_3").DisplayFormat = "n0"
                                     configuracionGridView.TotalSummary.Add(DevExpress.Data.SummaryItemType.Count, "cumple_educ_ina_ciclo_1_2").DisplayFormat = "n0"
                                     configuracionGridView.TotalSummary.Add(DevExpress.Data.SummaryItemType.Count, "cumple_educ_ina_ciclo_3").DisplayFormat = "n0"
                                 End If

                                 configuracionGridView.TotalSummary.Add(DevExpress.Data.SummaryItemType.Count, "cumple_salud").DisplayFormat = "n0"
                                 configuracionGridView.TotalSummary.Add(DevExpress.Data.SummaryItemType.Count, "cumple_salud_ciclo_1").DisplayFormat = "n0"
                                 configuracionGridView.TotalSummary.Add(DevExpress.Data.SummaryItemType.Count, "cumple_salud_ciclo_2").DisplayFormat = "n0"
                                 configuracionGridView.TotalSummary.Add(DevExpress.Data.SummaryItemType.Count, "cumple_educ").DisplayFormat = "n0"
                                 configuracionGridView.TotalSummary.Add(DevExpress.Data.SummaryItemType.Count, "cumple_educ_ciclo_1_2").DisplayFormat = "n0"
                                 configuracionGridView.TotalSummary.Add(DevExpress.Data.SummaryItemType.Count, "cumple_educ_ciclo_3").DisplayFormat = "n0"
                                 configuracionGridView.TotalSummary.Add(DevExpress.Data.SummaryItemType.Count, "no_cumple_salud_ciclo_1").DisplayFormat = "n0"
                                 configuracionGridView.TotalSummary.Add(DevExpress.Data.SummaryItemType.Count, "no_cumple_salud_ciclo_2").DisplayFormat = "n0"
                                 configuracionGridView.TotalSummary.Add(DevExpress.Data.SummaryItemType.Count, "no_cumple_educ_ciclo_1_2").DisplayFormat = "n0"
                                 configuracionGridView.TotalSummary.Add(DevExpress.Data.SummaryItemType.Count, "no_cumple_educ_ciclo_3").DisplayFormat = "n0"
                                 configuracionGridView.GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, "hab_per_persona").DisplayFormat = "n0"
                                 configuracionGridView.Settings.ShowFooter = True
                                 configuracionGridView.SettingsBehavior.AllowSelectByRowClick = True
                             End Sub).Bind(Model).GetHtml()
    %>