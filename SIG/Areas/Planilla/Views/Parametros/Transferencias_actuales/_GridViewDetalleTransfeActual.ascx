<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    Dim objTrasnfeActual As New SIG.SIG.Areas.Planilla.Models.cl_transferencia_actual
    
    Html.DevExpress.GridView(Sub(configuracionGridView)
                                 configuracionGridView.Name = "AspxGridViewDetalleTransfeActual"
                                 configuracionGridView.CallbackRouteValues = New With {Key .Controller = "Parametros", Key .Action = "GridViewDetalleTransfeActual", Key .strCodEsquemas = ViewData("strCodEsquemas")}
                                 configuracionGridView.CommandColumn.Visible = True
                                 
                                 configuracionGridView.KeyFieldName = "bon_codigo"
		
                                 configuracionGridView.SettingsPager.Visible = True
                                 configuracionGridView.Settings.ShowFilterRow = True
                                 configuracionGridView.Caption = "Detalle de las transferencias actuales de los esquemas seleccionados"
                                 configuracionGridView.Width = 1400
                                 
                                 configuracionGridView.ClientSideEvents.BeginCallback = "function(s, e) { e.customArgs['strCodEsquemas'] = objManejoEsquema.Esquemas.Select.join(','); }"
                                 
                                 configuracionGridView.CommandColumn.CustomButtons.Add(New GridViewCommandColumnCustomButton() With {.ID = "btnDelete", .Text = "Borrar"})
                                 configuracionGridView.ClientSideEvents.CustomButtonClick = "function(s,e){ objManejoEsquema.Esquemas.BorrarBono(s.GetRowKey(e.visibleIndex)); }"
                                 
                                 configuracionGridView.SettingsPager.PageSize = "20"
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                       configuracionColumn.FieldName = "esq_codigo"
                                                                       configuracionColumn.Caption = "Código Esquema"
                                                                       configuracionColumn.ColumnType = MVCxGridViewColumnType.SpinEdit
                                                                   End Sub)
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                       configuracionColumn.FieldName = "nombre_esquema"
                                                                       configuracionColumn.Caption = "Esquema"
                                                                   End Sub)
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                       configuracionColumn.FieldName = "bon_codigo"
                                                                       configuracionColumn.Caption = "Código Transferencia"
                                                                       configuracionColumn.ColumnType = MVCxGridViewColumnType.SpinEdit
                                                                   End Sub)
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                       configuracionColumn.FieldName = "bon_numero"
                                                                       configuracionColumn.Caption = "No. Transferencia"
                                                                       configuracionColumn.ColumnType = MVCxGridViewColumnType.SpinEdit
                                                                   End Sub)
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                       configuracionColumn.FieldName = "bon_tipo_intervalo"
                                                                       configuracionColumn.Caption = "Intervalo"
                                                                       configuracionColumn.ColumnType = MVCxGridViewColumnType.ComboBox
                                                                       Dim ComboBoxPropiedades As ComboBoxProperties = configuracionColumn.PropertiesEdit
                                                                       ComboBoxPropiedades.DataSource = objTrasnfeActual.fnc_obtener_intervalo_tiempo
                                                                       ComboBoxPropiedades.TextField = "int_nombre"
                                                                       ComboBoxPropiedades.ValueField = "int_codigo"
                                                                       ComboBoxPropiedades.ValueType = GetType(Integer)
                                                                   End Sub)
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                       configuracionColumn.FieldName = "bon_intervalo"
                                                                       configuracionColumn.Caption = "No. Intervalo"
                                                                       configuracionColumn.ColumnType = MVCxGridViewColumnType.SpinEdit
                                                                   End Sub)
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                       configuracionColumn.FieldName = "bon_anyo"
                                                                       configuracionColumn.Caption = "Año"
                                                                       configuracionColumn.ColumnType = MVCxGridViewColumnType.SpinEdit
                                                                   End Sub)
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                       configuracionColumn.FieldName = "bon_fecha_ini"
                                                                       configuracionColumn.Caption = "Fecha Inicio"
                                                                       configuracionColumn.ColumnType = MVCxGridViewColumnType.DateEdit
                                                                   End Sub)
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                       configuracionColumn.FieldName = "bon_fecha_fin"
                                                                       configuracionColumn.Caption = "Fecha Fin"
                                                                       configuracionColumn.ColumnType = MVCxGridViewColumnType.DateEdit
                                                                   End Sub)
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                       configuracionColumn.FieldName = "bon_detalle_meses"
                                                                       configuracionColumn.Caption = "Meses"
                                                                   End Sub)
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                       configuracionColumn.FieldName = "bon_meses_cubrir"
                                                                       configuracionColumn.Caption = "Cantidad Meses"
                                                                       configuracionColumn.ColumnType = MVCxGridViewColumnType.SpinEdit
                                                                   End Sub)
                             End Sub).Bind(Model).GetHtml()
%>