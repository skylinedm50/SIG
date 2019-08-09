<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% 
    Dim objReporCarEduSal = New SIG.SIG.Areas.Corresponsabilidad.Models.cl_report_carg_edu_sal

    Html.DevExpress.GridView(Sub(configuracionGridView As GridViewSettings)
                                 configuracionGridView.Name = "AspxGridViewCargasEduSal"
                                 configuracionGridView.CallbackRouteValues = New With {Key .Controller = "Reportes",
                                                                                        Key .Action = "AspxGridViewCargasEduSal",
                                                                                        Key .intTipCom = ViewData("intTipCom"),
                                                                                        Key .intNumCar = ViewData("intNumActu")}
                                 configuracionGridView.Caption = "Resumen de Actualizaciones"
                                 configuracionGridView.KeyFieldName = "cod_log_car"
                                 configuracionGridView.SettingsPager.Visible = True
                                 configuracionGridView.Settings.ShowGroupPanel = False
                                 configuracionGridView.Settings.ShowFilterRow = True
                                 configuracionGridView.ControlStyle.CssClass = "GridViewCorres"
                                 configuracionGridView.ClientSideEvents.BeginCallback = "function(s, e){ " &
                                                                                        "	var intNum = AspxComboBoxActulizacion.GetValue(); " &
                                                                                        "	var intTip = AspxComboBoxTipComponente.GetValue(); " &
                                                                                        "	e.customArgs['intTipCom'] = ((intTip == null) ? 0 : intTip); " &
                                                                                        " 	e.customArgs['intNumActu'] = ((intNum == null) ? 0 : intNum); " &
                                                                                        "} "

                                 configuracionGridView.SettingsPager.PageSize = "12"
                                 configuracionGridView.SettingsPager.PageSizeItemSettings.Visible = True
                                 configuracionGridView.SettingsPager.PageSizeItemSettings.Items = {"20", "40", "70", "100"}
                                 configuracionGridView.Width = 1220

                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                       configuracionColumn.FieldName = "cod_log_car"
                                                                       configuracionColumn.Caption = "Código"
                                                                       configuracionColumn.Width = 100

                                                                       configuracionColumn.ColumnType = MVCxGridViewColumnType.SpinEdit
                                                                       Dim SpinEditPropiedades As SpinEditProperties = configuracionColumn.PropertiesEdit
                                                                       SpinEditPropiedades.MinValue = 1
                                                                       SpinEditPropiedades.MaxValue = 1000000
                                                                   End Sub)
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                       configuracionColumn.FieldName = "fec_ini_log_car"
                                                                       configuracionColumn.Caption = "Fecha ingreso"
                                                                       configuracionColumn.Width = 185

                                                                       configuracionColumn.ColumnType = MVCxGridViewColumnType.DateEdit
                                                                       Dim DateEditPropiedades As DateEditProperties = configuracionColumn.PropertiesEdit
                                                                       DateEditPropiedades.EditFormat = EditFormat.Custom
                                                                       DateEditPropiedades.EditFormatString = "dd/MM/yyyy hh:mm tt"
                                                                       DateEditPropiedades.DisplayFormatString = "dd/MM/yyyy hh:mm tt"
                                                                       DateEditPropiedades.UseMaskBehavior = True
                                                                       DateEditPropiedades.TimeSectionProperties.Visible = True
                                                                       DateEditPropiedades.TimeSectionProperties.TimeEditProperties.EditFormat = EditFormat.Custom
                                                                       DateEditPropiedades.TimeSectionProperties.TimeEditProperties.EditFormatString = "hh:mm tt"
                                                                       DateEditPropiedades.DisplayFormatInEditMode = True
                                                                   End Sub)
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                       configuracionColumn.FieldName = "fec_fin_log_car"
                                                                       configuracionColumn.Caption = "Fecha procesamiento"
                                                                       configuracionColumn.Width = 185

                                                                       configuracionColumn.ColumnType = MVCxGridViewColumnType.DateEdit
                                                                       Dim DateEditPropiedades As DateEditProperties = configuracionColumn.PropertiesEdit
                                                                       DateEditPropiedades.EditFormat = EditFormat.Custom
                                                                       DateEditPropiedades.EditFormatString = "dd/MM/yyyy hh:mm tt"
                                                                       DateEditPropiedades.DisplayFormatString = "dd/MM/yyyy hh:mm tt"
                                                                       DateEditPropiedades.UseMaskBehavior = True
                                                                       DateEditPropiedades.TimeSectionProperties.Visible = True
                                                                       DateEditPropiedades.TimeSectionProperties.TimeEditProperties.EditFormat = EditFormat.Custom
                                                                       DateEditPropiedades.TimeSectionProperties.TimeEditProperties.EditFormatString = "hh:mm tt"
                                                                       DateEditPropiedades.DisplayFormatInEditMode = True
                                                                   End Sub)
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                       configuracionColumn.FieldName = "can_rec_log_car"
                                                                       configuracionColumn.Caption = "Datos recibidos"
                                                                       configuracionColumn.Width = 100

                                                                       configuracionColumn.ColumnType = MVCxGridViewColumnType.SpinEdit
                                                                       Dim SpinEditPropiedades As SpinEditProperties = configuracionColumn.PropertiesEdit
                                                                       SpinEditPropiedades.MinValue = 0
                                                                       SpinEditPropiedades.MaxValue = 9000000
                                                                       SpinEditPropiedades.DisplayFormatString = "n0"
                                                                   End Sub)
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                       configuracionColumn.FieldName = "can_dat_pro_log_car"
                                                                       configuracionColumn.Caption = "Datos procesados"
                                                                       configuracionColumn.Width = 100

                                                                       configuracionColumn.ColumnType = MVCxGridViewColumnType.SpinEdit
                                                                       Dim SpinEditPropiedades As SpinEditProperties = configuracionColumn.PropertiesEdit
                                                                       SpinEditPropiedades.MinValue = 0
                                                                       SpinEditPropiedades.MaxValue = 9000000
                                                                       SpinEditPropiedades.DisplayFormatString = "n0"
                                                                   End Sub)
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                       configuracionColumn.FieldName = "cod_est_log_car"
                                                                       configuracionColumn.Caption = "Estado"
                                                                       configuracionColumn.Width = 180

                                                                       configuracionColumn.ColumnType = MVCxGridViewColumnType.ComboBox
                                                                       Dim ComboBoxPropiedades As ComboBoxProperties = configuracionColumn.PropertiesEdit
                                                                       ComboBoxPropiedades.DataSource = objReporCarEduSal.fnc_estados_log_carga()
                                                                       ComboBoxPropiedades.ValueField = "cod_est_log_car"
                                                                       ComboBoxPropiedades.TextField = "nom_est_log_car"
                                                                       ComboBoxPropiedades.ValueType = GetType(Integer)

                                                                   End Sub)
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                       configuracionColumn.FieldName = "cod_des_ser"
                                                                       configuracionColumn.Caption = "Tipo de registro"
                                                                       configuracionColumn.Width = 500

                                                                       configuracionColumn.ColumnType = MVCxGridViewColumnType.ComboBox
                                                                       Dim ComboBoxPropiedades As ComboBoxProperties = configuracionColumn.PropertiesEdit
                                                                       ComboBoxPropiedades.DataSource = objReporCarEduSal.fnc_tipos_registros(ViewData("intTipCom"))
                                                                       ComboBoxPropiedades.ValueField = "cod_des_ser"
                                                                       ComboBoxPropiedades.TextField = "nom_des_ser"
                                                                       ComboBoxPropiedades.ValueType = GetType(Integer)
                                                                   End Sub)
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                       configuracionColumn.FieldName = "num_car_log_car"
                                                                       configuracionColumn.Caption = "Actualización"
                                                                       configuracionColumn.Width = 100

                                                                       configuracionColumn.ColumnType = MVCxGridViewColumnType.SpinEdit
                                                                       Dim SpinEditPropiedades As SpinEditProperties = configuracionColumn.PropertiesEdit
                                                                       SpinEditPropiedades.MinValue = 0
                                                                       SpinEditPropiedades.MaxValue = 9000000
                                                                   End Sub)
                                 configuracionGridView.SettingsDetail.ShowDetailRow = True
                                 configuracionGridView.SettingsDetail.AllowOnlyOneMasterRowExpanded = True
                                 configuracionGridView.SetDetailRowTemplateContent(Sub(c)
                                                                                       Html.RenderAction("AspxGridViewDetallePorLog", New With {Key .intCodLog = DataBinder.Eval(c.DataItem, "cod_log_car"),
                                                                                                         Key .intCodTipRegis = DataBinder.Eval(c.DataItem, "cod_des_ser"), Key .intComp = ViewData("intTipCom")})
                                                                                   End Sub)
                             End Sub).Bind(Model).GetHtml()
    %>