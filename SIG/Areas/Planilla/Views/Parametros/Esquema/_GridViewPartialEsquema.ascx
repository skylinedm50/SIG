<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    Dim objEsquema As New SIG.SIG.Areas.Planilla.Models.cl_esquema
    Html.DevExpress.GridView(Sub(configuracionGridView)
                                 configuracionGridView.Name = "AspxGridViewEsquema"
                                 configuracionGridView.CallbackRouteValues = New With {Key .Controller = "Parametros", Key .Action = "GridViewPartialEsquema"}
                                 configuracionGridView.Caption = "Detalle de Esquemas"
                                 configuracionGridView.CommandColumn.Visible = True
                                 
                                 configuracionGridView.KeyFieldName = "esq_codigo"
		
                                 configuracionGridView.SettingsPager.Visible = False
                                 configuracionGridView.Settings.ShowGroupPanel = False
                                 configuracionGridView.Settings.ShowFilterRow = True
                                 configuracionGridView.SettingsBehavior.AllowSelectByRowClick = True
                                 configuracionGridView.SettingsDetail.AllowOnlyOneMasterRowExpanded = True
                                 configuracionGridView.SettingsDetail.ShowDetailRow = True
                                 configuracionGridView.SettingsPager.PageSize = 100000
                                 configuracionGridView.SettingsPager.Visible = False
                                 
                                 configuracionGridView.CommandColumn.ShowClearFilterButton = True
                           
                                 
                                 configuracionGridView.CommandColumn.CustomButtons.Add(New GridViewCommandColumnCustomButton() With {.ID = "btnNew", .Text = "Nuevo"})
                                 configuracionGridView.CommandColumn.CustomButtons.Add(New GridViewCommandColumnCustomButton() With {.ID = "btnUpdate", .Text = "Editar"})
                                 configuracionGridView.CommandColumn.CustomButtons.Add(New GridViewCommandColumnCustomButton() With {.ID = "btnDelete", .Text = "Borrar"})
                                 configuracionGridView.CommandColumn.Width = 125
                                 
                                 configuracionGridView.ClientSideEvents.CustomButtonClick = "function(s,e){fnc_tipo_operacion(s, e);}"
                                 
                                 configuracionGridView.Settings.VerticalScrollableHeight = 600
                                 configuracionGridView.Settings.VerticalScrollBarMode = ScrollBarMode.Auto
                                 
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                       configuracionColumn.FieldName = "esq_codigo"
                                                                       configuracionColumn.Caption = "Código"
                                                                       configuracionColumn.Width = 50
                                                                   End Sub)
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                       configuracionColumn.FieldName = "pag_codigo"
                                                                       configuracionColumn.Caption = "Nombre del pago"
                                                                       configuracionColumn.Width = 150
                                                                       configuracionColumn.ColumnType = MVCxGridViewColumnType.ComboBox
                                                                       Dim ComboBoxPropiedades As ComboBoxProperties = configuracionColumn.PropertiesEdit
                                                                       ComboBoxPropiedades.DataSource = objEsquema.fnc_obtener_pago
                                                                       ComboBoxPropiedades.TextField = "pag_nombre"
                                                                       ComboBoxPropiedades.ValueField = "pag_codigo"
                                                                       ComboBoxPropiedades.ValueType = GetType(Integer)
                                                                   End Sub)
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                       configuracionColumn.FieldName = "nombre_esquema"
                                                                       configuracionColumn.Caption = "Nombre del esquema"
                                                                       configuracionColumn.ColumnType = MVCxGridViewColumnType.Memo
                                                                       configuracionColumn.Width = 500
                                                                   End Sub)
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                       configuracionColumn.FieldName = "esq_numero"
                                                                       configuracionColumn.Caption = "No."
                                                                                                                      
                                                                       configuracionColumn.ColumnType = MVCxGridViewColumnType.SpinEdit
                                                                       Dim SpinEditPropiedades As SpinEditProperties = configuracionColumn.PropertiesEdit
                                                                       SpinEditPropiedades.MinValue = 1
                                                                       SpinEditPropiedades.MaxValue = 20
                                                                       configuracionColumn.Width = 50
                                                                                                                      
                                                                   End Sub)
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                       configuracionColumn.FieldName = "esq_anyo"
                                                                       configuracionColumn.Caption = "Año"
                                                                       configuracionColumn.Width = 56
                                                                       configuracionColumn.ColumnType = MVCxGridViewColumnType.SpinEdit
                                                                       Dim SpinEditPropiedades As SpinEditProperties = configuracionColumn.PropertiesEdit
                                                                       SpinEditPropiedades.MinValue = 2015
                                                                       SpinEditPropiedades.MaxValue = 2050
                                                                   End Sub)
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                       configuracionColumn.FieldName = "esq_fecha_ini"
                                                                       configuracionColumn.Caption = "Fecha inicio"
                                                                       configuracionColumn.ColumnType = MVCxGridViewColumnType.DateEdit
                                                                       Dim DateEditPropiedades As DateEditProperties = configuracionColumn.PropertiesEdit
                                                                       DateEditPropiedades.EditFormat = EditFormat.Custom
                                                                       DateEditPropiedades.EditFormatString = "dd/MM/yyyy"
                                                                       DateEditPropiedades.DisplayFormatString = "dd/MM/yyyy"
                                                                       DateEditPropiedades.DisplayFormatInEditMode = True
                                                                       DateEditPropiedades.UseMaskBehavior = True
                                                                       configuracionColumn.Width = 90
                                                                   End Sub)
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                       configuracionColumn.FieldName = "esq_fecha_fin"
                                                                       configuracionColumn.Caption = "Fecha fin"
                                                                       
                                                                       configuracionColumn.ColumnType = MVCxGridViewColumnType.DateEdit
                                                                       Dim DateEditPropiedades As DateEditProperties = configuracionColumn.PropertiesEdit
                                                                       DateEditPropiedades.EditFormat = EditFormat.Custom
                                                                       DateEditPropiedades.EditFormatString = "dd/MM/yyyy"
                                                                       DateEditPropiedades.DisplayFormatString = "dd/MM/yyyy"
                                                                       DateEditPropiedades.DisplayFormatInEditMode = True
                                                                       DateEditPropiedades.UseMaskBehavior = True
                                                                       configuracionColumn.Width = 90
                                                                   End Sub)
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                       configuracionColumn.FieldName = "esq_meses"
                                                                       configuracionColumn.Caption = "Meses"
                                                                       
                                                                       configuracionColumn.ColumnType = MVCxGridViewColumnType.SpinEdit
                                                                       Dim SpinEditPropiedades As SpinEditProperties = configuracionColumn.PropertiesEdit
                                                                       SpinEditPropiedades.MinValue = 1
                                                                       SpinEditPropiedades.MaxValue = 12
                                                                       configuracionColumn.Width = 45
                                                                   End Sub)
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                       configuracionColumn.FieldName = "esq_detalle_meses"
                                                                       configuracionColumn.Caption = "Detalle de los meses"
                                                                       configuracionColumn.ColumnType = MVCxGridViewColumnType.Memo
                                                                       configuracionColumn.Width = 590
                                                                   End Sub)
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                       configuracionColumn.FieldName = "esq_tipo_intervalo"
                                                                       configuracionColumn.Caption = "Intervalo tiempo"
                                                                       configuracionColumn.ColumnType = MVCxGridViewColumnType.ComboBox
                                                                       configuracionColumn.Width = 110
                                                                       
                                                                       Dim ComboBoxPropiedades As ComboBoxProperties = configuracionColumn.PropertiesEdit
                                                                       ComboBoxPropiedades.DataSource = objEsquema.fnc_obtener_intervalo_tiempo
                                                                       ComboBoxPropiedades.TextField = "int_nombre"
                                                                       ComboBoxPropiedades.ValueField = "int_codigo"
                                                                       ComboBoxPropiedades.ValueType = GetType(Integer)
                                                                   End Sub)
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                       configuracionColumn.FieldName = "esq_tipo_pago"
                                                                       configuracionColumn.Caption = "Tipo pago"
                                                                       configuracionColumn.Width = 100
                                                                       configuracionColumn.ColumnType = MVCxGridViewColumnType.ComboBox
                                                                       
                                                                       Dim ComboBoxPropiedades As ComboBoxProperties = configuracionColumn.PropertiesEdit
                                                                       ComboBoxPropiedades.DataSource = objEsquema.fnc_obtener_tipo_pago
                                                                       ComboBoxPropiedades.TextField = "nombre_tipo_pago"
                                                                       ComboBoxPropiedades.ValueField = "cod_tipo_pago"
                                                                       ComboBoxPropiedades.ValueType = GetType(String)
                                                                   End Sub)
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                       configuracionColumn.FieldName = "esq_tipo_esquema"
                                                                       configuracionColumn.Caption = "Tipo esquema"
                                                                       configuracionColumn.Width = 90
                                                                       configuracionColumn.ColumnType = MVCxGridViewColumnType.ComboBox
                                                                       
                                                                       Dim ComboBoxPropiedades As ComboBoxProperties = configuracionColumn.PropertiesEdit
                                                                       ComboBoxPropiedades.DataSource = objEsquema.fnc_obtener_tipo_esquema
                                                                       ComboBoxPropiedades.TextField = "nombre_tipo_esquema"
                                                                       ComboBoxPropiedades.ValueField = "cod_tipo_esquema"
                                                                       ComboBoxPropiedades.ValueType = GetType(Integer)
                                                                   End Sub)
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                       configuracionColumn.FieldName = "esq_aprobado"
                                                                       configuracionColumn.Caption = "Estado"
                                                                       configuracionColumn.Width = 100
                                                                       configuracionColumn.ColumnType = MVCxGridViewColumnType.ComboBox
                                                                       
                                                                       Dim ComboBoxPropiedades As ComboBoxProperties = configuracionColumn.PropertiesEdit
                                                                       ComboBoxPropiedades.DataSource = objEsquema.fnc_obtener_estado_aprobado
                                                                       ComboBoxPropiedades.TextField = "nombre_esq_aprobado"
                                                                       ComboBoxPropiedades.ValueField = "esq_aprobado"
                                                                       ComboBoxPropiedades.ValueType = GetType(Integer)
                                                                   End Sub)
                                 configuracionGridView.SetDetailRowTemplateContent(Sub(configuracionDetailRow)
                                                                                       ViewData("intCodEsquema") = DataBinder.Eval(configuracionDetailRow.DataItem, "esq_codigo")
                                                                                       Html.RenderPartial("Esquema/_FormLayoutViewPartialDetalleEsquema", ViewData("intCodEsquema"))
                                                                                   End Sub)
                             End Sub).Bind(Model).GetHtml()
%>