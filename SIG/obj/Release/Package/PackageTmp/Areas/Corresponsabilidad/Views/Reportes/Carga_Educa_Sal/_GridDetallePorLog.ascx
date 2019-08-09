<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% 
    Dim objReporCarEduSal = New SIG.SIG.Areas.Corresponsabilidad.Models.cl_report_carg_edu_sal
    Dim arrStrCaptionEduca() As String = {"Detalle Estados de Matricula por Actualización", "Detalle Razones de Cancelación por Actualización", "Detalle de Tipo de Administración de Centros Educativos por Actualización",
        "Detalle de Tipo Periodo Escolar por Actualización", "Detalle de Niveles de Centro Educativo por Actualización", "Detalle de Sub-nivel de Centro Educativo por Actualización",
        "Detalle de Centro Educativo por Actualización", "Detalle de Matricula por Actualización", "Detalle de Parcial por Actualización", "Detalle de Asistencia por Actualización",
        "Detalle de Promoción por Actualización", "Detalle de Sub-nivel por Centro Educativo por Actualización"}
    Dim arrStrCaptionSal() As String = {"Detalle Centro de Salud por Actualización", "Detalle Visitas Médicas por Actualización"}
    Dim intCodTipRegis As Integer = CInt(ViewData("intCodTipRegis"))
    Dim intCodLog As Integer = CInt(ViewData("intCodLog"))
    Dim intComp As Integer = CInt(ViewData("intComp"))
    Dim strCaption As String = ""
    Dim strKeyFileName As String = ""

    If intComp = 1 Then
        strCaption = arrStrCaptionEduca(intCodTipRegis - 1)
        Select Case intCodTipRegis
            Case 1
                strKeyFileName = "cod_est_mat_car_est_mat"
                Exit Select
            Case 4
                strKeyFileName = "cod_car_per_esc"
                Exit Select
            Case 5
                strKeyFileName = "cod_car_niv_edu"
                Exit Select
            Case 6
                strKeyFileName = "cod_car_sub_niv"
                Exit Select
            Case 7
                strKeyFileName = "cod_car_cen_edu"
                Exit Select
            Case 9
                strKeyFileName = "cod_car_par"
                Exit Select
            Case 11
                strKeyFileName = "cod_car_pro_mat"
                Exit Select
            Case 12
                strKeyFileName = "cod_car_sub_niv_cen_edu"
                Exit Select
        End Select
    End If

    If (intComp = 1 And (intCodTipRegis = 8 Or intCodTipRegis = 10)) Or intComp = 2 Then
        Html.DevExpress.PivotGrid(Sub(configuracionPivotGrid As PivotGridSettings)
                                      configuracionPivotGrid.Name = "AspxPivotGridDetallePorLog"
                                      configuracionPivotGrid.CallbackRouteValues = New With {Key .Controller = "Reportes",
                                                                                            Key .Action = "AspxGridViewDetallePorLog",
                                                                                            Key .intCodLog = ViewData("intCodLog"),
                                                                                            Key .intCodTipRegis = ViewData("intCodTipRegis"),
                                                                                            Key .intComp = ViewData("intComp")}

                                      configuracionPivotGrid.OptionsView.ShowFilterHeaders = True
                                      configuracionPivotGrid.OptionsView.DataHeadersDisplayMode = PivotDataHeadersDisplayMode.Popup
                                      configuracionPivotGrid.OptionsView.DataHeadersPopupMinCount = 4
                                      configuracionPivotGrid.OptionsView.DataHeadersPopupMaxColumnCount = 2
                                      configuracionPivotGrid.OptionsView.RowTreeOffset = 2
                                      configuracionPivotGrid.OptionsPager.LastPageButton.Visible = True
                                      configuracionPivotGrid.OptionsPager.FirstPageButton.Visible = True
                                      configuracionPivotGrid.OptionsPager.RowsPerPage = 18
                                      configuracionPivotGrid.OptionsPager.PageSizeItemSettings.Items = {"30", "50", "80", "100"}
                                      configuracionPivotGrid.OptionsView.GroupFieldsInCustomizationWindow = True


                                      configuracionPivotGrid.PreRender = Sub(sender, e)
                                                                             sender.CollapseAll()
                                                                         End Sub

                                      configuracionPivotGrid.Fields.Add(Sub(configuracionField As MVCxPivotGridField)
                                                                            configuracionField.Area = PivotArea.DataArea
                                                                            configuracionField.FieldName = "cod_departamento"
                                                                            configuracionField.Caption = "Código Departamento"
                                                                            configuracionField.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
                                                                            configuracionField.SummaryType = PivotSummaryType.Min
                                                                            configuracionField.Visible = False
                                                                        End Sub)
                                      configuracionPivotGrid.Fields.Add(Sub(configuracionField As MVCxPivotGridField)
                                                                            configuracionField.Area = PivotArea.RowArea
                                                                            configuracionField.FieldName = "desc_departamento"
                                                                            configuracionField.Caption = "Departamento"
                                                                            configuracionField.SummaryType = PivotSummaryType.Min
                                                                        End Sub)
                                      configuracionPivotGrid.Fields.Add(Sub(configuracionField As MVCxPivotGridField)
                                                                            configuracionField.Area = PivotArea.RowArea
                                                                            configuracionField.FieldName = "desc_municipio"
                                                                            configuracionField.Caption = "Municipio"
                                                                            configuracionField.SummaryType = PivotSummaryType.Min
                                                                        End Sub)
                                      configuracionPivotGrid.Fields.Add(Sub(configuracionField As MVCxPivotGridField)
                                                                            configuracionField.Area = PivotArea.RowArea
                                                                            configuracionField.FieldName = "desc_aldea"
                                                                            configuracionField.Caption = "Aldea"
                                                                            configuracionField.SummaryType = PivotSummaryType.Min
                                                                        End Sub)
                                      configuracionPivotGrid.Fields.Add(Sub(configuracionField As MVCxPivotGridField)
                                                                            configuracionField.Area = PivotArea.RowArea
                                                                            configuracionField.FieldName = "desc_caserio"
                                                                            configuracionField.Caption = "Caserio"
                                                                            configuracionField.SummaryType = PivotSummaryType.Min
                                                                        End Sub)
                                      Select Case intComp
                                          Case 1
                                              If intCodTipRegis = 8 Then 'Matricula educativa
                                                  configuracionPivotGrid.Fields.Add(Sub(configuracionField As MVCxPivotGridField)
                                                                                        configuracionField.Area = PivotArea.DataArea
                                                                                        configuracionField.FieldName = "con_matricula"
                                                                                        configuracionField.Caption = "Niños con Matriculas"
                                                                                        configuracionField.CellFormat.FormatString = "n0"
                                                                                        configuracionField.CellFormat.FormatType = FormatType.Numeric
                                                                                    End Sub)
                                                  configuracionPivotGrid.Fields.Add(Sub(configuracionField As MVCxPivotGridField)
                                                                                        configuracionField.Area = PivotArea.DataArea
                                                                                        configuracionField.FieldName = "sin_matricula"
                                                                                        configuracionField.Caption = "Niños sin Matricula"
                                                                                        configuracionField.CellFormat.FormatString = "n0"
                                                                                        configuracionField.CellFormat.FormatType = FormatType.Numeric
                                                                                    End Sub)
                                                  configuracionPivotGrid.Fields.Add(Sub(configuracionField As MVCxPivotGridField)
                                                                                        configuracionField.Area = PivotArea.DataArea
                                                                                        configuracionField.FieldName = "Total"
                                                                                        configuracionField.UnboundType = DevExpress.Data.UnboundColumnType.Integer
                                                                                        configuracionField.UnboundExpression = String.Format("{0} + {1}", configuracionPivotGrid.Fields("con_matricula").ID, configuracionPivotGrid.Fields("sin_matricula").ID)
                                                                                        configuracionField.CellFormat.FormatString = "n0"
                                                                                        configuracionField.CellFormat.FormatType = FormatType.Numeric
                                                                                    End Sub)
                                                  configuracionPivotGrid.Fields.Add(Sub(configuracionField As MVCxPivotGridField)
                                                                                        configuracionField.Area = PivotArea.DataArea
                                                                                        configuracionField.FieldName = "est_activa"
                                                                                        configuracionField.Caption = "Activas"
                                                                                        configuracionField.CellFormat.FormatString = "n0"
                                                                                        configuracionField.CellFormat.FormatType = FormatType.Numeric
                                                                                    End Sub)
                                                  configuracionPivotGrid.Fields.Add(Sub(configuracionField As MVCxPivotGridField)
                                                                                        configuracionField.Area = PivotArea.DataArea
                                                                                        configuracionField.FieldName = "est_cancelada"
                                                                                        configuracionField.Caption = "Canceladas"
                                                                                        configuracionField.CellFormat.FormatString = "n0"
                                                                                        configuracionField.CellFormat.FormatType = FormatType.Numeric
                                                                                    End Sub)
                                                  configuracionPivotGrid.Fields.Add(Sub(configuracionField As MVCxPivotGridField)
                                                                                        configuracionField.Area = PivotArea.DataArea
                                                                                        configuracionField.FieldName = "est_traslado"
                                                                                        configuracionField.Caption = "En Traslado"
                                                                                        configuracionField.CellFormat.FormatString = "n0"
                                                                                        configuracionField.CellFormat.FormatType = FormatType.Numeric
                                                                                    End Sub)
                                                  configuracionPivotGrid.Fields.Add(Sub(configuracionField As MVCxPivotGridField)
                                                                                        configuracionField.Area = PivotArea.DataArea
                                                                                        configuracionField.FieldName = "est_trasladada"
                                                                                        configuracionField.Caption = "Trasladadas"
                                                                                        configuracionField.CellFormat.FormatString = "n0"
                                                                                        configuracionField.CellFormat.FormatType = FormatType.Numeric
                                                                                    End Sub)
                                                  configuracionPivotGrid.Fields.Add(Sub(configuracionField As MVCxPivotGridField)
                                                                                        configuracionField.Area = PivotArea.DataArea
                                                                                        configuracionField.FieldName = "est_ingreso_tralado"
                                                                                        configuracionField.Caption = "Ingreso por Traslado"
                                                                                        configuracionField.CellFormat.FormatString = "n0"
                                                                                        configuracionField.CellFormat.FormatType = FormatType.Numeric
                                                                                    End Sub)
                                              Else 'Asistencia
                                                  configuracionPivotGrid.Fields.Add(Sub(configuracionField As MVCxPivotGridField)
                                                                                        configuracionField.Area = PivotArea.DataArea
                                                                                        configuracionField.FieldName = "con_asistencia"
                                                                                        configuracionField.Caption = "Con Asistencia"
                                                                                        configuracionField.CellFormat.FormatString = "n0"
                                                                                        configuracionField.CellFormat.FormatType = FormatType.Numeric
                                                                                    End Sub)
                                                  configuracionPivotGrid.Fields.Add(Sub(configuracionField As MVCxPivotGridField)
                                                                                        configuracionField.Area = PivotArea.DataArea
                                                                                        configuracionField.FieldName = "sin_asistencia"
                                                                                        configuracionField.Caption = "Sin Asistencia"
                                                                                        configuracionField.CellFormat.FormatString = "n0"
                                                                                        configuracionField.CellFormat.FormatType = FormatType.Numeric
                                                                                    End Sub)
                                                  configuracionPivotGrid.Fields.Add(Sub(configuracionField As MVCxPivotGridField)
                                                                                        configuracionField.Area = PivotArea.DataArea
                                                                                        configuracionField.FieldName = "Total"
                                                                                        configuracionField.UnboundType = DevExpress.Data.UnboundColumnType.Integer
                                                                                        configuracionField.UnboundExpression = String.Format("{0} + {1}", configuracionPivotGrid.Fields("con_asistencia").ID, configuracionPivotGrid.Fields("sin_asistencia").ID)
                                                                                        configuracionField.CellFormat.FormatString = "n0"
                                                                                        configuracionField.CellFormat.FormatType = FormatType.Numeric
                                                                                    End Sub)
                                              End If
                                          Case 2 'Salud
                                              If intCodTipRegis = 2 Then 'Visitas médicas
                                                  configuracionPivotGrid.Fields.Add(Sub(configuracionField As MVCxPivotGridField)
                                                                                        configuracionField.Area = PivotArea.DataArea
                                                                                        configuracionField.FieldName = "can_vis"
                                                                                        configuracionField.Caption = "Niños con Visitas"
                                                                                        configuracionField.CellFormat.FormatString = "n0"
                                                                                        configuracionField.CellFormat.FormatType = FormatType.Numeric
                                                                                    End Sub)
                                                  configuracionPivotGrid.Fields.Add(Sub(configuracionField As MVCxPivotGridField)
                                                                                        configuracionField.Area = PivotArea.DataArea
                                                                                        configuracionField.FieldName = "can_vis_anu"
                                                                                        configuracionField.Caption = "Niños con Visitas Anuladas"
                                                                                        configuracionField.CellFormat.FormatString = "n0"
                                                                                        configuracionField.CellFormat.FormatType = FormatType.Numeric
                                                                                    End Sub)
                                                  configuracionPivotGrid.Fields.Add(Sub(configuracionField As MVCxPivotGridField)
                                                                                        configuracionField.Area = PivotArea.DataArea
                                                                                        configuracionField.FieldName = "con_vis"
                                                                                        configuracionField.Caption = "Con Vistas"
                                                                                        configuracionField.CellFormat.FormatString = "n0"
                                                                                        configuracionField.CellFormat.FormatType = FormatType.Numeric
                                                                                    End Sub)
                                                  configuracionPivotGrid.Fields.Add(Sub(configuracionField As MVCxPivotGridField)
                                                                                        configuracionField.Area = PivotArea.DataArea
                                                                                        configuracionField.FieldName = "sin_vis"
                                                                                        configuracionField.Caption = "Niños Sin Vistas"
                                                                                        configuracionField.CellFormat.FormatString = "n0"
                                                                                        configuracionField.CellFormat.FormatType = FormatType.Numeric
                                                                                    End Sub)
                                                  configuracionPivotGrid.Fields.Add(Sub(configuracionField As MVCxPivotGridField)
                                                                                        configuracionField.Area = PivotArea.DataArea
                                                                                        configuracionField.FieldName = "Total"
                                                                                        configuracionField.UnboundType = DevExpress.Data.UnboundColumnType.Integer
                                                                                        configuracionField.UnboundExpression = String.Format("{0} + {1}", configuracionPivotGrid.Fields("con_vis").ID, configuracionPivotGrid.Fields("sin_vis").ID)
                                                                                        configuracionField.CellFormat.FormatString = "n0"
                                                                                        configuracionField.CellFormat.FormatType = FormatType.Numeric
                                                                                    End Sub)
                                              Else 'Centro de salud
                                                  configuracionPivotGrid.Fields.Add(Sub(configuracionField As MVCxPivotGridField)
                                                                                        configuracionField.Area = PivotArea.DataArea
                                                                                        configuracionField.FieldName = "cod_renpi"
                                                                                        configuracionField.Caption = "Código RENPI"
                                                                                        configuracionField.SummaryType = PivotSummaryType.Min
                                                                                    End Sub)
                                                  configuracionPivotGrid.Fields.Add(Sub(configuracionField As MVCxPivotGridField)
                                                                                        configuracionField.Area = PivotArea.DataArea
                                                                                        configuracionField.FieldName = "nom_centro"
                                                                                        configuracionField.Caption = "Centro"
                                                                                        configuracionField.SummaryType = PivotSummaryType.Min
                                                                                    End Sub)
                                                  configuracionPivotGrid.Fields.Add(Sub(configuracionField As MVCxPivotGridField)
                                                                                        configuracionField.Area = PivotArea.DataArea
                                                                                        configuracionField.FieldName = "proceso"
                                                                                        configuracionField.Caption = "Proceso"
                                                                                        configuracionField.SummaryType = PivotSummaryType.Min
                                                                                    End Sub)
                                              End If
                                      End Select
                                  End Sub).Bind(Model).GetHtml()
    Else
        Html.DevExpress.GridView(Sub(configuracionGridView As GridViewSettings)
                                     configuracionGridView.Name = "AspxGridViewDetallePorLog"
                                     configuracionGridView.CallbackRouteValues = New With {Key .Controller = "Reportes",
                                                                                            Key .Action = "AspxGridViewDetallePorLog",
                                                                                            Key .intCodLog = ViewData("intCodLog"),
                                                                                            Key .intCodTipRegis = ViewData("intCodTipRegis"),
                                                                                            Key .intComp = ViewData("intComp")}
                                     configuracionGridView.Caption = strCaption
                                     configuracionGridView.KeyFieldName = strKeyFileName
                                     configuracionGridView.SettingsPager.Visible = True
                                     configuracionGridView.Settings.ShowGroupPanel = False
                                     configuracionGridView.Settings.ShowFilterRow = True
                                     configuracionGridView.ControlStyle.CssClass = "GridViewCorres"

                                     configuracionGridView.SettingsPager.PageSize = "20"
                                     configuracionGridView.SettingsPager.PageSizeItemSettings.Visible = True
                                     configuracionGridView.SettingsPager.PageSizeItemSettings.Items = {"40", "60", "80", "100"}
                                     configuracionGridView.CommandColumn.ShowClearFilterButton = True
                                     configuracionGridView.CommandColumn.Visible = True


                                     If intCodTipRegis = 1 Or intCodTipRegis = 2 Or intCodTipRegis = 3 Then 'Esatdos de matricula, Razones de cancelación, Tipo de administración de un centro
                                         configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                               configuracionColumn.FieldName = "codigo"
                                                                               configuracionColumn.Caption = "Código"
                                                                               configuracionColumn.Width = 200
                                                                           End Sub)
                                         configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                               configuracionColumn.FieldName = "nombre"
                                                                               configuracionColumn.Caption = "Nombre"
                                                                               configuracionColumn.Width = 200
                                                                           End Sub)
                                     Else
                                         Select Case intCodTipRegis
                                             Case 4 'Periodo escolar
                                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                       configuracionColumn.FieldName = "cod_per_esc_car_per_esc"
                                                                                       configuracionColumn.Caption = "Código"
                                                                                       configuracionColumn.Width = 60
                                                                                   End Sub)
                                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                       configuracionColumn.FieldName = "des_per_esc_car_per_esc"
                                                                                       configuracionColumn.Caption = "Nombre"
                                                                                       configuracionColumn.Width = 200
                                                                                   End Sub)
                                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                       configuracionColumn.FieldName = "mes_ini_per_esc_car_per_esc"
                                                                                       configuracionColumn.Caption = "Mes Inicio"
                                                                                       configuracionColumn.Width = 100
                                                                                   End Sub)
                                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                       configuracionColumn.FieldName = "mes_fin_per_esc_car_per_esc"
                                                                                       configuracionColumn.Caption = "Mes Fin"
                                                                                       configuracionColumn.Width = 100
                                                                                   End Sub)
                                             Case 5 'Nivel educativo
                                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                       configuracionColumn.FieldName = "cod_sac_niv_edu_car_niv_edu"
                                                                                       configuracionColumn.Caption = "Código SACE"
                                                                                       configuracionColumn.Width = 100
                                                                                   End Sub)
                                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                       configuracionColumn.FieldName = "cod_niv_edu_car_niv_edu"
                                                                                       configuracionColumn.Caption = "Código Oficial"
                                                                                       configuracionColumn.Width = 100
                                                                                   End Sub)
                                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                       configuracionColumn.FieldName = "des_niv_edu_car_niv_edu"
                                                                                       configuracionColumn.Caption = "Nombre"
                                                                                       configuracionColumn.Width = 100
                                                                                   End Sub)
                                             Case 6 'Sub-Nivel educativo
                                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                       configuracionColumn.FieldName = "cod_sac_sub_niv_car_sub_niv"
                                                                                       configuracionColumn.Caption = "Código SACE"
                                                                                       configuracionColumn.Width = 100
                                                                                   End Sub)
                                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                       configuracionColumn.FieldName = "cod_sac_niv_edu_car_sub_niv"
                                                                                       configuracionColumn.Caption = "Código Nivel"
                                                                                       configuracionColumn.Width = 100
                                                                                   End Sub)
                                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                       configuracionColumn.FieldName = "cod_sub_niv_car_sub_niv"
                                                                                       configuracionColumn.Caption = "Código Oficial Sub-Nivel"
                                                                                       configuracionColumn.Width = 100
                                                                                   End Sub)
                                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                       configuracionColumn.FieldName = "des_sub_niv_car_sub_niv"
                                                                                       configuracionColumn.Caption = "Nombre"
                                                                                       configuracionColumn.Width = 200
                                                                                   End Sub)
                                             Case 7 'Centro Educativo
                                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                       configuracionColumn.FieldName = "nom_dep_sac"
                                                                                       configuracionColumn.Caption = "Departamento"
                                                                                       configuracionColumn.Width = 100
                                                                                   End Sub)
                                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                       configuracionColumn.FieldName = "nom_mun_sac"
                                                                                       configuracionColumn.Caption = "Municipio"
                                                                                       configuracionColumn.Width = 150
                                                                                   End Sub)
                                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                       configuracionColumn.FieldName = "nom_ald_sac"
                                                                                       configuracionColumn.Caption = "Aldea"
                                                                                       configuracionColumn.Width = 200
                                                                                   End Sub)
                                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                       configuracionColumn.FieldName = "nom_cas_sac"
                                                                                       configuracionColumn.Caption = "Caserio"
                                                                                       configuracionColumn.Width = 200
                                                                                   End Sub)
                                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                       configuracionColumn.FieldName = "nom_bar_sac"
                                                                                       configuracionColumn.Caption = "Barrio"
                                                                                       configuracionColumn.Width = 250
                                                                                   End Sub)
                                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                       configuracionColumn.FieldName = "cod_sac_cen_edu_car_cen_edu"
                                                                                       configuracionColumn.Caption = "Código SACE"
                                                                                       configuracionColumn.Width = 90
                                                                                   End Sub)
                                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                       configuracionColumn.FieldName = "cod_cen_edu_car_cen_edu"
                                                                                       configuracionColumn.Caption = "Código"
                                                                                       configuracionColumn.Width = 100
                                                                                   End Sub)
                                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                       configuracionColumn.FieldName = "nom_cen_edu_car_cen_edu"
                                                                                       configuracionColumn.Caption = "Nombre"
                                                                                       configuracionColumn.Width = 350
                                                                                   End Sub)
                                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                       configuracionColumn.FieldName = "tel_cen_edu_car_cen_edu"
                                                                                       configuracionColumn.Caption = "Teléfono"
                                                                                       configuracionColumn.Width = 70
                                                                                   End Sub)
                                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                       configuracionColumn.FieldName = "cel_cen_edu_car_cen_edu"
                                                                                       configuracionColumn.Caption = "Celular"
                                                                                       configuracionColumn.Width = 70
                                                                                   End Sub)
                                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                       configuracionColumn.FieldName = "cor_cen_edu_car_cen_edu"
                                                                                       configuracionColumn.Caption = "Correo"
                                                                                       configuracionColumn.Width = 280
                                                                                   End Sub)
                                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                       configuracionColumn.FieldName = "dir_cen_edu_car_cen_edu"
                                                                                       configuracionColumn.Caption = "Dirección"
                                                                                       configuracionColumn.Width = 500
                                                                                   End Sub)
                                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                       configuracionColumn.FieldName = "cod_fun_cen_edu"
                                                                                       configuracionColumn.Caption = "Estado"
                                                                                       configuracionColumn.Width = 150

                                                                                       configuracionColumn.ColumnType = MVCxGridViewColumnType.ComboBox
                                                                                       Dim ComboBoxPropiedades As ComboBoxProperties = configuracionColumn.PropertiesEdit
                                                                                       ComboBoxPropiedades.DataSource = objReporCarEduSal.fnc_obtener_funcionamiento()
                                                                                       ComboBoxPropiedades.ValueField = "cod_fun_cen_edu"
                                                                                       ComboBoxPropiedades.TextField = "desc_fun_cen_edu"
                                                                                       ComboBoxPropiedades.ValueType = GetType(Integer)
                                                                                   End Sub)
                                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                       configuracionColumn.FieldName = "cod_adm_cen"
                                                                                       configuracionColumn.Caption = "Administración"
                                                                                       configuracionColumn.Width = 180

                                                                                       configuracionColumn.ColumnType = MVCxGridViewColumnType.ComboBox
                                                                                       Dim ComboBoxPropiedades As ComboBoxProperties = configuracionColumn.PropertiesEdit
                                                                                       ComboBoxPropiedades.DataSource = objReporCarEduSal.fnc_obtener_tipo_administracion()
                                                                                       ComboBoxPropiedades.ValueField = "cod_adm_cen"
                                                                                       ComboBoxPropiedades.TextField = "nom_adm_cen"
                                                                                       ComboBoxPropiedades.ValueType = GetType(Integer)
                                                                                   End Sub)
                                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                       configuracionColumn.FieldName = "cod_tip_zon"
                                                                                       configuracionColumn.Caption = "Zona"
                                                                                       configuracionColumn.Width = 100

                                                                                       configuracionColumn.ColumnType = MVCxGridViewColumnType.ComboBox
                                                                                       Dim ComboBoxPropiedades As ComboBoxProperties = configuracionColumn.PropertiesEdit
                                                                                       ComboBoxPropiedades.DataSource = objReporCarEduSal.fnc_obtener_tipo_zona()
                                                                                       ComboBoxPropiedades.ValueField = "cod_tip_zon"
                                                                                       ComboBoxPropiedades.TextField = "nom_tip_zon"
                                                                                       ComboBoxPropiedades.ValueType = GetType(Integer)
                                                                                   End Sub)
                                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                       configuracionColumn.FieldName = "cod_per_esc"
                                                                                       configuracionColumn.Caption = "Periodo Escolar"
                                                                                       configuracionColumn.Width = 300

                                                                                       configuracionColumn.ColumnType = MVCxGridViewColumnType.ComboBox
                                                                                       Dim ComboBoxPropiedades As ComboBoxProperties = configuracionColumn.PropertiesEdit
                                                                                       ComboBoxPropiedades.DataSource = objReporCarEduSal.fnc_obtener_periodo_escolar()
                                                                                       ComboBoxPropiedades.ValueField = "cod_per_esc"
                                                                                       ComboBoxPropiedades.TextField = "des_per_esc"
                                                                                       ComboBoxPropiedades.ValueType = GetType(Integer)
                                                                                   End Sub)
                                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                       configuracionColumn.FieldName = "cod_sac_pai_fro"
                                                                                       configuracionColumn.Caption = "País Fronterizo"
                                                                                       configuracionColumn.Width = 140

                                                                                       configuracionColumn.ColumnType = MVCxGridViewColumnType.ComboBox
                                                                                       Dim ComboBoxPropiedades As ComboBoxProperties = configuracionColumn.PropertiesEdit
                                                                                       ComboBoxPropiedades.DataSource = objReporCarEduSal.fnc_obtener_pais_fronterizo()
                                                                                       ComboBoxPropiedades.ValueField = "cod_sac_pai_fro"
                                                                                       ComboBoxPropiedades.TextField = "des_pai_fro"
                                                                                       ComboBoxPropiedades.ValueType = GetType(Integer)
                                                                                   End Sub)
                                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                       configuracionColumn.FieldName = "cod_etn"
                                                                                       configuracionColumn.Caption = "Etnia"
                                                                                       configuracionColumn.Width = 125

                                                                                       configuracionColumn.ColumnType = MVCxGridViewColumnType.ComboBox
                                                                                       Dim ComboBoxPropiedades As ComboBoxProperties = configuracionColumn.PropertiesEdit
                                                                                       ComboBoxPropiedades.DataSource = objReporCarEduSal.fnc_obtener_etnia()
                                                                                       ComboBoxPropiedades.ValueField = "cod_etn"
                                                                                       ComboBoxPropiedades.TextField = "des_etn"
                                                                                       ComboBoxPropiedades.ValueType = GetType(Integer)
                                                                                   End Sub)
                                                 configuracionGridView.Width = 1900
                                                 configuracionGridView.Settings.HorizontalScrollBarMode = ScrollBarMode.Auto
                                                 configuracionGridView.SettingsPager.PageSize = "10"
                                             Case 9 'Parcial
                                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                       configuracionColumn.FieldName = "cod_par_sac_car_par"
                                                                                       configuracionColumn.Caption = "Código SACE"
                                                                                       configuracionColumn.Width = 60
                                                                                   End Sub)
                                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                       configuracionColumn.FieldName = "año_parcial"
                                                                                       configuracionColumn.Caption = "Año"
                                                                                       configuracionColumn.Width = 40
                                                                                   End Sub)
                                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                       configuracionColumn.FieldName = "nom_par_car_par"
                                                                                       configuracionColumn.Caption = "Parcial"
                                                                                       configuracionColumn.Width = 70
                                                                                   End Sub)
                                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                       configuracionColumn.FieldName = "fec_ini_par_car_par"
                                                                                       configuracionColumn.Caption = "Fecha Inicio"
                                                                                       configuracionColumn.Width = 70
                                                                                   End Sub)
                                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                       configuracionColumn.FieldName = "fec_fin_par_car_par"
                                                                                       configuracionColumn.Caption = "Fecha Fin"
                                                                                       configuracionColumn.Width = 70
                                                                                   End Sub)
                                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                       configuracionColumn.FieldName = "cod_cen_edu"
                                                                                       configuracionColumn.Caption = "Código Centro"
                                                                                       configuracionColumn.Width = 100
                                                                                   End Sub)
                                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                       configuracionColumn.FieldName = "nom_cen_edu"
                                                                                       configuracionColumn.Caption = "Centro"
                                                                                       configuracionColumn.Width = 350
                                                                                   End Sub)
                                             Case 11
                                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                       configuracionColumn.FieldName = "cod_mat_sac_pro_mat_car_pro_mat"
                                                                                       configuracionColumn.Caption = "Código Matricula"
                                                                                       configuracionColumn.Width = 100
                                                                                   End Sub)
                                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                       configuracionColumn.FieldName = "cod_mat_sac_pro_mat_car_pro_mat"
                                                                                       configuracionColumn.Caption = "Código Promoción"
                                                                                       configuracionColumn.Width = 100
                                                                                   End Sub)
                                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                       configuracionColumn.FieldName = "ano_mat_det_mat"
                                                                                       configuracionColumn.Caption = "Año"
                                                                                       configuracionColumn.Width = 100
                                                                                   End Sub)
                                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                       configuracionColumn.FieldName = "cod_est_pro"
                                                                                       configuracionColumn.Caption = "Estado"
                                                                                       configuracionColumn.Width = 140

                                                                                       configuracionColumn.ColumnType = MVCxGridViewColumnType.ComboBox
                                                                                       Dim ComboBoxPropiedades As ComboBoxProperties = configuracionColumn.PropertiesEdit
                                                                                       ComboBoxPropiedades.DataSource = objReporCarEduSal.fnc_obtener_estado_promocion()
                                                                                       ComboBoxPropiedades.ValueField = "cod_est_pro"
                                                                                       ComboBoxPropiedades.TextField = "desc_est_pro"
                                                                                       ComboBoxPropiedades.ValueType = GetType(Integer)
                                                                                   End Sub)
                                             Case 12
                                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                       configuracionColumn.FieldName = "cod_cen_edu"
                                                                                       configuracionColumn.Caption = "Código Centro"
                                                                                       configuracionColumn.Width = 100
                                                                                   End Sub)
                                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                       configuracionColumn.FieldName = "nom_cen_edu"
                                                                                       configuracionColumn.Caption = "Centro"
                                                                                       configuracionColumn.Width = 300
                                                                                   End Sub)
                                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                       configuracionColumn.FieldName = "cod_sac_sub_niv_cen_edu_car_sub_niv_cen_edu"
                                                                                       configuracionColumn.Caption = "Código Sub-Nivel Centro"
                                                                                       configuracionColumn.Width = 300
                                                                                   End Sub)
                                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                       configuracionColumn.FieldName = "cod_sac_niv_edu"
                                                                                       configuracionColumn.Caption = "Nivel Educativo"
                                                                                       configuracionColumn.Width = 200

                                                                                       configuracionColumn.ColumnType = MVCxGridViewColumnType.ComboBox
                                                                                       Dim ComboBoxPropiedades As ComboBoxProperties = configuracionColumn.PropertiesEdit
                                                                                       ComboBoxPropiedades.DataSource = objReporCarEduSal.fnc_obtener_nivel_educativo()
                                                                                       ComboBoxPropiedades.ValueField = "cod_sac_niv_edu"
                                                                                       ComboBoxPropiedades.TextField = "des_niv_edu"
                                                                                       ComboBoxPropiedades.ValueType = GetType(Integer)
                                                                                   End Sub)
                                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                       configuracionColumn.FieldName = "des_sub_niv"
                                                                                       configuracionColumn.Caption = "Sub-Nivel Educativo"
                                                                                       configuracionColumn.Width = 300
                                                                                   End Sub)
                                         End Select
                                     End If
                                     configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                           configuracionColumn.FieldName = "cod_pro_regis"
                                                                           configuracionColumn.Caption = "Proceso"
                                                                           configuracionColumn.Width = 120

                                                                           configuracionColumn.ColumnType = MVCxGridViewColumnType.ComboBox
                                                                           Dim ComboBoxPropiedades As ComboBoxProperties = configuracionColumn.PropertiesEdit
                                                                           ComboBoxPropiedades.DataSource = objReporCarEduSal.fnc_obtener_proceso_registro()
                                                                           ComboBoxPropiedades.ValueField = "cod_pro_regis"
                                                                           ComboBoxPropiedades.TextField = "nom_pro_regis"
                                                                           ComboBoxPropiedades.ValueType = GetType(Integer)
                                                                       End Sub)
                                 End Sub).Bind(Model).GetHtml()
    End If
    %>