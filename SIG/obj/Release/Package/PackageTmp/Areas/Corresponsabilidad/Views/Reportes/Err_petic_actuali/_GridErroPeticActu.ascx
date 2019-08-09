<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% 
    Dim objReporErrPetActu As New SIG.SIG.Areas.Corresponsabilidad.Models.cl_report_error_petici_actuali

    Html.DevExpress.GridView(Sub(configuracionGridView As GridViewSettings)
                                 configuracionGridView.Name = "AspxGridViewErrorPetiActua"
                                 configuracionGridView.CallbackRouteValues = New With {Key .Controller = "Reportes",
                                                                                        Key .Action = "AspxGridViewErrorPetiActua",
                                                                                        Key .intCodComp = ViewData("intCodComp"),
                                                                                        Key .intNumActua = ViewData("intNumActua")}
                                 configuracionGridView.Caption = "Errores en la Petición de Actualizaciones"
                                 configuracionGridView.KeyFieldName = "cod_err_con_car"
                                 configuracionGridView.SettingsPager.Visible = True
                                 configuracionGridView.Settings.ShowGroupPanel = False
                                 configuracionGridView.Settings.ShowFilterRow = True
                                 configuracionGridView.CommandColumn.Visible = True
                                 configuracionGridView.CommandColumn.ShowClearFilterButton = True
                                 configuracionGridView.ControlStyle.CssClass = "GridViewCorres"
                                 configuracionGridView.ClientSideEvents.BeginCallback = "function(s, e){ " &
                                                                                        "	var intNum = AspxComboBoxActulizacion.GetValue(); " &
                                                                                        "	var intTip = AspxComboBoxTipComponente.GetValue(); " &
                                                                                        "	e.customArgs['intCodComp'] = ((intTip == null) ? 0 : intTip); " &
                                                                                        " 	e.customArgs['intNumActua'] = ((intNum == null) ? 0 : intNum); " &
                                                                                        "} "

                                 configuracionGridView.SettingsPager.PageSize = "12"
                                 configuracionGridView.SettingsPager.PageSizeItemSettings.Visible = True
                                 configuracionGridView.SettingsPager.PageSizeItemSettings.Items = {"20", "40", "70", "100"}
                                 configuracionGridView.Width = 1220

                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                       configuracionColumn.FieldName = "cod_err_con_car"
                                                                       configuracionColumn.Caption = "Código"
                                                                       configuracionColumn.Width = 100

                                                                       configuracionColumn.ColumnType = MVCxGridViewColumnType.SpinEdit
                                                                       Dim SpinEditPropiedades As SpinEditProperties = configuracionColumn.PropertiesEdit
                                                                       SpinEditPropiedades.MinValue = 1
                                                                       SpinEditPropiedades.MaxValue = 1000000
                                                                   End Sub)
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                       configuracionColumn.FieldName = "fec_err_con_car"
                                                                       configuracionColumn.Caption = "Fecha Error"
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
                                                                       configuracionColumn.FieldName = "int_err_con_car"
                                                                       configuracionColumn.Caption = "Intentos"
                                                                       configuracionColumn.Width = 60

                                                                       configuracionColumn.ColumnType = MVCxGridViewColumnType.SpinEdit
                                                                       Dim SpinEditPropiedades As SpinEditProperties = configuracionColumn.PropertiesEdit
                                                                       SpinEditPropiedades.MinValue = 1
                                                                       SpinEditPropiedades.MaxValue = 1000000
                                                                   End Sub)
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                       configuracionColumn.FieldName = "num_car_log_car"
                                                                       configuracionColumn.Caption = "Actualización"
                                                                       configuracionColumn.Width = 75

                                                                       configuracionColumn.ColumnType = MVCxGridViewColumnType.SpinEdit
                                                                       Dim SpinEditPropiedades As SpinEditProperties = configuracionColumn.PropertiesEdit
                                                                       SpinEditPropiedades.MinValue = 1
                                                                       SpinEditPropiedades.MaxValue = 1000000
                                                                   End Sub)
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                       configuracionColumn.FieldName = "cod_des_ser"
                                                                       configuracionColumn.Caption = "Servicio"
                                                                       configuracionColumn.Width = 180

                                                                       configuracionColumn.ColumnType = MVCxGridViewColumnType.ComboBox
                                                                       Dim ComboBoxPropiedades As ComboBoxProperties = configuracionColumn.PropertiesEdit
                                                                       ComboBoxPropiedades.DataSource = objReporErrPetActu.fnc_obtener_servicios(CInt(ViewData("intCodComp")))
                                                                       ComboBoxPropiedades.ValueField = "cod_des_ser"
                                                                       ComboBoxPropiedades.TextField = "nom_des_ser"
                                                                       ComboBoxPropiedades.ValueType = GetType(Integer)

                                                                   End Sub)
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                       configuracionColumn.FieldName = "err_err_con_car"
                                                                       configuracionColumn.Caption = "Error"
                                                                       configuracionColumn.Width = 300

                                                                       configuracionColumn.ColumnType = MVCxGridViewColumnType.Memo
                                                                       Dim MemoPropiedades As MemoProperties = configuracionColumn.PropertiesEdit
                                                                   End Sub)
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                       configuracionColumn.FieldName = "ser_err_con_car"
                                                                       configuracionColumn.Caption = "URL Acceso"
                                                                       configuracionColumn.Width = 300
                                                                   End Sub)
                             End Sub).Bind(Model).GetHtml()
    %>