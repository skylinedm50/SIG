<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% 
    Dim objReporBusCorrBenef = New SIG.SIG.Areas.Corresponsabilidad.Models.cl_report_busq_corres_benef

    Html.DevExpress.GridView(Sub(configuracionGridView As GridViewSettings)
                                 configuracionGridView.Name = "AspxGridViewResultBusqCorrBenef"
                                 configuracionGridView.CallbackRouteValues = New With {Key .Controller = "Reportes",
                                                                                        Key .Action = "AspxGridViewResultBusqCorrBenef",
                                                                                        Key .intCodBenef = ViewData("intCodBenef"),
                                                                                        Key .intCodRUPBenef = ViewData("intCodRUPBenf"),
                                                                                        Key .strIdentiBenef = ViewData("strIdentiBenf"),
                                                                                        Key .strNom1Benef = ViewData("strNom1Benef"),
                                                                                        Key .strNom2Benef = ViewData("strNom2Benef"),
                                                                                        Key .strApe1Benef = ViewData("strApe1Benef"),
                                                                                        Key .strApe2Benef = ViewData("strApe2Benef"),
                                                                                        Key .strIdentiTit = ViewData("strIdentiTit"),
                                                                                        Key .intCodHogRUP = ViewData("intCodHogRUP"),
                                                                                        Key .intCodHogSIG = ViewData("intCodHogSIG")}

                                 configuracionGridView.Caption = "Resultados de búsqueda"
                                 configuracionGridView.KeyFieldName = "per_persona"
                                 configuracionGridView.SettingsPager.Visible = True
                                 configuracionGridView.Settings.ShowGroupPanel = False
                                 configuracionGridView.Settings.ShowFilterRow = True
                                 configuracionGridView.ControlStyle.CssClass = "GridViewCorres"

                                 configuracionGridView.ClientSideEvents.BeginCallback = "function(s, e){ " &
                                                                                       "     e.customArgs['intCodBenef'] = objBusqCorresBenef.Session.intCodBenef; " &
                                                                                        "     e.customArgs['intCodRUPBenef'] = objBusqCorresBenef.Session.intCodBenefRUP; " &
                                                                                        "     e.customArgs['strIdentiBenef'] = objBusqCorresBenef.Session.strIdentBenef; " &
                                                                                        "     e.customArgs['strNom1Benef'] = objBusqCorresBenef.Session.strNom1Benef; " &
                                                                                        "     e.customArgs['strNom2Benef'] = objBusqCorresBenef.Session.strNom2Benef; " &
                                                                                        "     e.customArgs['strApe1Benef'] = objBusqCorresBenef.Session.strApe1Benef; " &
                                                                                        "     e.customArgs['strApe2Benef'] = objBusqCorresBenef.Session.strApe2Benef; " &
                                                                                        "     e.customArgs['strIdentiTit'] = objBusqCorresBenef.Session.strIdentTitu; " &
                                                                                        "     e.customArgs['intCodHogRUP'] = objBusqCorresBenef.Session.intCodHogRUP; " &
                                                                                        "     e.customArgs['intCodHogSIG'] = objBusqCorresBenef.Session.intCodHogSIG; " &
                                                                                        "} "

                                 configuracionGridView.SettingsPager.PageSize = "10"
                                 configuracionGridView.SettingsPager.PageSizeItemSettings.Visible = True
                                 configuracionGridView.SettingsPager.PageSizeItemSettings.Items = {"20", "40", "70", "100"}
                                 configuracionGridView.Width = 1200
                                 configuracionGridView.SettingsBehavior.AllowSelectByRowClick = True
                                 configuracionGridView.ClientSideEvents.RowDblClick = "function(s, e){ objBusqCorresBenef.SelectBeneficiario(s.GetRowKey(e.visibleIndex)); }"
                                 configuracionGridView.ClientSideEvents.CallbackError = "function(){ AspxLabelError.SetText('*ERROR, imposible conectarse con el servidor favor comunicarse con el administrador del sistema.');}"

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
                                                                                                                configuracionColumn.Caption = "Caserío"
                                                                                                                configuracionColumn.Width = 250
                                                                                                            End Sub)
                                                                       End Sub)

                                 configuracionGridView.Columns.AddBand(Sub(configuracionAddBand As MVCxGridViewBandColumn)
                                                                           configuracionAddBand.Caption = "Hogar"
                                                                           configuracionAddBand.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                                                                           configuracionAddBand.Columns.AddBand(Sub(configuracionAddBandSub As MVCxGridViewBandColumn)
                                                                                                                    configuracionAddBandSub.Caption = "Titular"
                                                                                                                    configuracionAddBandSub.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                                                                                                                    configuracionAddBandSub.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                            configuracionColumn.FieldName = "tit_identidad"
                                                                                                                                                            configuracionColumn.Caption = "Identidad"
                                                                                                                                                            configuracionColumn.Width = 100
                                                                                                                                                        End Sub)
                                                                                                                    configuracionAddBandSub.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                            configuracionColumn.FieldName = "tit_nombre1"
                                                                                                                                                            configuracionColumn.Caption = "Primer Nombre"
                                                                                                                                                            configuracionColumn.Width = 110
                                                                                                                                                        End Sub)
                                                                                                                    configuracionAddBandSub.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                            configuracionColumn.FieldName = "tit_nombre2"
                                                                                                                                                            configuracionColumn.Caption = "Segundo Nombre"
                                                                                                                                                            configuracionColumn.Width = 110
                                                                                                                                                        End Sub)
                                                                                                                    configuracionAddBandSub.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                            configuracionColumn.FieldName = "tit_apellido1"
                                                                                                                                                            configuracionColumn.Caption = "Primer Apellido"
                                                                                                                                                            configuracionColumn.Width = 110
                                                                                                                                                        End Sub)
                                                                                                                    configuracionAddBandSub.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                            configuracionColumn.FieldName = "tit_apellido2"
                                                                                                                                                            configuracionColumn.Caption = "Segundo Apellido"
                                                                                                                                                            configuracionColumn.Width = 110
                                                                                                                                                        End Sub)
                                                                                                                End Sub)
                                                                           configuracionAddBand.Columns.AddBand(Sub(configuracionAddBandSub As MVCxGridViewBandColumn)
                                                                                                                    configuracionAddBandSub.Caption = "Beneficiario"
                                                                                                                    configuracionAddBandSub.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                                                                                                                    configuracionAddBandSub.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                            configuracionColumn.FieldName = "per_persona"
                                                                                                                                                            configuracionColumn.Caption = "Código SIG"
                                                                                                                                                            configuracionColumn.Width = 100
                                                                                                                                                        End Sub)
                                                                                                                    configuracionAddBandSub.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                            configuracionColumn.FieldName = "per_rup_persona"
                                                                                                                                                            configuracionColumn.Caption = "Código RUP"
                                                                                                                                                            configuracionColumn.Width = 100
                                                                                                                                                        End Sub)
                                                                                                                    configuracionAddBandSub.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                            configuracionColumn.FieldName = "per_identidad"
                                                                                                                                                            configuracionColumn.Caption = "Identidad"
                                                                                                                                                            configuracionColumn.Width = 100
                                                                                                                                                        End Sub)
                                                                                                                    configuracionAddBandSub.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                            configuracionColumn.FieldName = "per_nombre1"
                                                                                                                                                            configuracionColumn.Caption = "Primer Nombre"
                                                                                                                                                            configuracionColumn.Width = 110
                                                                                                                                                        End Sub)
                                                                                                                    configuracionAddBandSub.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                            configuracionColumn.FieldName = "per_nombre2"
                                                                                                                                                            configuracionColumn.Caption = "Segundo Nombre"
                                                                                                                                                            configuracionColumn.Width = 110
                                                                                                                                                        End Sub)
                                                                                                                    configuracionAddBandSub.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                            configuracionColumn.FieldName = "per_apellido1"
                                                                                                                                                            configuracionColumn.Caption = "Primer Apellido"
                                                                                                                                                            configuracionColumn.Width = 110
                                                                                                                                                        End Sub)
                                                                                                                    configuracionAddBandSub.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                            configuracionColumn.FieldName = "per_apellido2"
                                                                                                                                                            configuracionColumn.Caption = "Segundo Apellido"
                                                                                                                                                            configuracionColumn.Width = 110
                                                                                                                                                        End Sub)
                                                                                                                    configuracionAddBandSub.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                            configuracionColumn.FieldName = "hab_sexo"
                                                                                                                                                            configuracionColumn.Caption = "Sexo"
                                                                                                                                                            configuracionColumn.ColumnType = MVCxGridViewColumnType.ComboBox
                                                                                                                                                            Dim ComboBoxPropiedades As ComboBoxProperties = configuracionColumn.PropertiesEdit
                                                                                                                                                            ComboBoxPropiedades.DataSource = objReporBusCorrBenef.fnc_obtener_genero_sexual()
                                                                                                                                                            ComboBoxPropiedades.ValueField = "hab_sexo"
                                                                                                                                                            ComboBoxPropiedades.TextField = "hab_sexo_descripcion"
                                                                                                                                                            ComboBoxPropiedades.ValueType = GetType(Integer)
                                                                                                                                                            configuracionColumn.Width = 100
                                                                                                                                                        End Sub)
                                                                                                                    configuracionAddBandSub.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                            configuracionColumn.FieldName = "cod_ciclo_edad"
                                                                                                                                                            configuracionColumn.Caption = "Ciclo Elegibilidad"

                                                                                                                                                            configuracionColumn.ColumnType = MVCxGridViewColumnType.ComboBox
                                                                                                                                                            Dim ComboBoxPropiedades As ComboBoxProperties = configuracionColumn.PropertiesEdit
                                                                                                                                                            ComboBoxPropiedades.DataSource = objReporBusCorrBenef.fnc_obtener_ciclo_edades()
                                                                                                                                                            ComboBoxPropiedades.ValueField = "cod_ciclo_edad"
                                                                                                                                                            ComboBoxPropiedades.TextField = "ciclo_edad_descripcion"
                                                                                                                                                            ComboBoxPropiedades.ValueType = GetType(Integer)
                                                                                                                                                            configuracionColumn.Width = 200
                                                                                                                                                        End Sub)
                                                                                                                    configuracionAddBandSub.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                                                                                                            configuracionColumn.FieldName = "per_fch_nacimiento"
                                                                                                                                                            configuracionColumn.Caption = "Fecha Nacimiento"
                                                                                                                                                            configuracionColumn.Width = 185

                                                                                                                                                            configuracionColumn.ColumnType = MVCxGridViewColumnType.DateEdit
                                                                                                                                                            Dim DateEditPropiedades As DateEditProperties = configuracionColumn.PropertiesEdit
                                                                                                                                                            DateEditPropiedades.EditFormat = EditFormat.Custom
                                                                                                                                                            DateEditPropiedades.EditFormatString = "dd/MM/yyyy"
                                                                                                                                                            DateEditPropiedades.DisplayFormatString = "dd/MM/yyyy"
                                                                                                                                                            DateEditPropiedades.UseMaskBehavior = True
                                                                                                                                                            DateEditPropiedades.TimeSectionProperties.Visible = True
                                                                                                                                                            DateEditPropiedades.DisplayFormatInEditMode = True
                                                                                                                                                        End Sub)
                                                                                                                End Sub)

                                                                       End Sub)
                                 configuracionGridView.Settings.HorizontalScrollBarMode = ScrollBarMode.Auto
                             End Sub).Bind(Model).GetHtml()
    %>