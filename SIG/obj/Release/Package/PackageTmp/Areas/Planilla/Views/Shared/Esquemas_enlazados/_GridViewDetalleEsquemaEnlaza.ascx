<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    Dim intTipoConfig = ViewData("intTipoConfig")
    
    Dim arrParteIniCaption() As String = {"Detalle de cobertura", "Establecimiento de fondos", "Mecanismo de pago"}
    Dim strParteLastCaption = " de esquemas seleccionados"
    Dim arrKeyFieldName() As String = {"esqc_codigo", "fesq_codigo", "pesq_codigo"}
    Dim arrNameSigno() As String = {"esqc_signo", "fesq_signo", "pesq_signo"}
    Dim arrNameTipLocali() As String = {"esqc_tipo_localizacion", "fesq_tipo_localizacion", "pesq_tipo_localizacion"}
    Dim objShared As New SIG.SIG.Areas.Planilla.Models.cl_shared
    

    Html.DevExpress.GridView(Sub(configuracionGridView)
                                 configuracionGridView.Name = "AspxGridViewDetalleEsquemaEnlazadoLocalizacion"
                                 configuracionGridView.CallbackRouteValues = New With {Key .Controller = "Shared", Key .Action = "GridViewDetalleEsquemaEnlazadoLocalizacion", Key .intCodEsquema = ViewData("intCodEsquema"), .intTipoConfig = ViewData("intTipoConfig")}
                                 configuracionGridView.CommandColumn.Visible = True
                                 
                                 configuracionGridView.KeyFieldName = arrKeyFieldName(intTipoConfig)
		
                                 configuracionGridView.SettingsPager.Visible = True
                                 configuracionGridView.Settings.ShowFilterRow = True
                                 configuracionGridView.Caption = arrParteIniCaption(intTipoConfig) & strParteLastCaption
                                 configuracionGridView.Width = 1400
                                 
                                 configuracionGridView.ClientSideEvents.BeginCallback = "function(s, e) { e.customArgs['intCodEsquema'] = objManejoEsquema.Esquemas.Select[0]; e.customArgs['intTipoConfig'] = objManejoEsquema.Esquemas.TipoEnlace; }"
                                 
                                 configuracionGridView.CommandColumn.CustomButtons.Add(New GridViewCommandColumnCustomButton() With {.ID = "btnDelete", .Text = "Borrar"})
                                 configuracionGridView.ClientSideEvents.CustomButtonClick = "function(s,e){objManejoEsquema.Esquemas.Borrar(s, e);}"
                                 
                                 configuracionGridView.SettingsPager.PageSize = "150"
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                       configuracionColumn.FieldName = "departamento"
                                                                       configuracionColumn.Caption = "Departamento"
                                                                   End Sub)
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                       configuracionColumn.FieldName = "municipio"
                                                                       configuracionColumn.Caption = "Municipio"
                                                                   End Sub)
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                       configuracionColumn.FieldName = "aldea"
                                                                       configuracionColumn.Caption = "Aldea"
                                                                   End Sub)
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                       configuracionColumn.FieldName = "caserio"
                                                                       configuracionColumn.Caption = "Caserio"
                                                                   End Sub)
                                 Select Case intTipoConfig
                                     Case 0
                                         configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                               configuracionColumn.FieldName = "esqc_planillas_si"
                                                                               configuracionColumn.Caption = "Planilla Si"
                                                                           End Sub)
                                         configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                               configuracionColumn.FieldName = "esqc_planillas_no"
                                                                               configuracionColumn.Caption = "Planilla No"
                                                                           End Sub)
                                         configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                               configuracionColumn.FieldName = "esqc_cargas_si"
                                                                               configuracionColumn.Caption = "Cargas"
                                                                           End Sub)
                                         Exit Select
                                     Case 1
                                         Dim objEstableFondo As New SIG.SIG.Areas.Planilla.Models.cl_establecimiento_fondo
                                         configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                               configuracionColumn.FieldName = "fond_codigo"
                                                                               configuracionColumn.Caption = "Fondo"
                                                                       
                                                                               configuracionColumn.ColumnType = MVCxGridViewColumnType.ComboBox
                                                                               Dim ComboBoxPropiedades As ComboBoxProperties = configuracionColumn.PropertiesEdit
                                                                               ComboBoxPropiedades.DataSource = objEstableFondo.fnc_obtener_fondos
                                                                               ComboBoxPropiedades.TextField = "fond_nombre"
                                                                               ComboBoxPropiedades.ValueField = "fond_codigo"
                                                                               ComboBoxPropiedades.ValueType = GetType(Integer)
                                                                           End Sub)
                                         Exit Select
                                     Case 2
                                         Dim objMecaniPago As New SIG.SIG.Areas.Planilla.Models.cl_mecanismo_pago
                                         configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                               configuracionColumn.FieldName = "pag_codigo"
                                                                               configuracionColumn.Caption = "Pagador"
                                       
                                                                               configuracionColumn.ColumnType = MVCxGridViewColumnType.ComboBox
                                                                               Dim ComboBoxPropiedades As ComboBoxProperties = configuracionColumn.PropertiesEdit
                                                                               ComboBoxPropiedades.DataSource = objMecaniPago.fnc_obtener_pagadores
                                                                               ComboBoxPropiedades.TextField = "Nombre_Pagador"
                                                                               ComboBoxPropiedades.ValueField = "Codigo_Pagador"
                                                                               ComboBoxPropiedades.ValueType = GetType(Integer)
                                                                           End Sub)
                                         Exit Select
                                 End Select
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                       configuracionColumn.FieldName = arrNameTipLocali(intTipoConfig)
                                                                       configuracionColumn.Caption = "Tipo localización"
                                       
                                                                       configuracionColumn.ColumnType = MVCxGridViewColumnType.ComboBox
                                                                       Dim ComboBoxPropiedades As ComboBoxProperties = configuracionColumn.PropertiesEdit
                                                                       ComboBoxPropiedades.DataSource = objShared.fnc_obtener_tipo_localizacion
                                                                       ComboBoxPropiedades.TextField = "int_descripcion"
                                                                       ComboBoxPropiedades.ValueField = "int_codigo"
                                                                       ComboBoxPropiedades.ValueType = GetType(Integer)
                                                                   End Sub)
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                       configuracionColumn.FieldName = arrNameSigno(intTipoConfig)
                                                                       configuracionColumn.Caption = "Signo"
                                                                   End Sub)
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn)
                                                                       configuracionColumn.FieldName = "desc_cambio"
                                                                       configuracionColumn.Caption = "Descripción"
                                                                   End Sub)
                             End Sub).Bind(Model).GetHtml()
%>