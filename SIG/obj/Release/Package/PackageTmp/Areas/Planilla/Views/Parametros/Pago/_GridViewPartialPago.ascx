<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    Dim gridEditarPagos = Html.DevExpress().GridView(Sub(configuracionGridView)
                                                         configuracionGridView.Name = "AspxGridViewPagos"
                                                         configuracionGridView.Caption = "Detalle de los Pagos"
                                                         configuracionGridView.CallbackRouteValues = New With {Key .Controller = "Parametros", Key .Action = "GridViewPartialPagos"}
                                                         configuracionGridView.SettingsEditing.UpdateRowRouteValues = New With {Key .Controller = "Parametros", Key .Action = "GridViewPartialUpdatePago"}
                                                         configuracionGridView.SettingsEditing.Mode = GridViewEditingMode.EditFormAndDisplayRow
                                                         
                                                         configuracionGridView.CommandColumn.CustomButtons.Add(New GridViewCommandColumnCustomButton() With {.ID = "btnDelete", .Text = "Borrar"})
                                                         configuracionGridView.ClientSideEvents.CustomButtonClick = "function(s,e){ fnc_borrar(s, e); }"
                                                         
                                                         configuracionGridView.SettingsBehavior.ConfirmDelete = True
                                                         configuracionGridView.CommandColumn.Visible = True
                                                         configuracionGridView.CommandColumn.ShowEditButton = True
                                                         
                                                         configuracionGridView.KeyFieldName = "pag_codigo"
		
                                                         configuracionGridView.SettingsPager.Visible = True
                                                         configuracionGridView.Settings.ShowGroupPanel = False
                                                         configuracionGridView.Settings.ShowFilterRow = True
                                                         configuracionGridView.SettingsBehavior.AllowSelectByRowClick = True
                                                         
                                                         configuracionGridView.SettingsPager.PageSizeItemSettings.Visible = True
                                                         configuracionGridView.SettingsPager.PageSizeItemSettings.Items = {"25", "50", "100", "200", "400"}
                                                         configuracionGridView.SettingsPager.PageSize = "100"
                                                         
                                                         configuracionGridView.Columns.Add(Sub(configuracionColumns)
                                                                                               configuracionColumns.FieldName = "pag_codigo"
                                                                                               configuracionColumns.Caption = "Código"
                                                                                               configuracionColumns.EditFormSettings.Visible = DefaultBoolean.False
                                                                                               configuracionColumns.ColumnType = MVCxGridViewColumnType.SpinEdit
                                                                                           End Sub)
                                                         configuracionGridView.Columns.Add(Sub(configuracionColumns)
                                                                                               configuracionColumns.FieldName = "pag_anyo"
                                                                                               configuracionColumns.Caption = "Año"
                                                                                               configuracionColumns.EditFormSettings.Visible = DefaultBoolean.False
                                                                                               configuracionColumns.ColumnType = MVCxGridViewColumnType.SpinEdit
                                                                                           End Sub)
                                                         configuracionGridView.Columns.Add(Sub(configuracionColumns)
                                                                                               configuracionColumns.FieldName = "pag_numero"
                                                                                               configuracionColumns.Caption = "Número"
                                                                                               configuracionColumns.EditFormSettings.Visible = DefaultBoolean.False
                                                                                               configuracionColumns.ColumnType = MVCxGridViewColumnType.SpinEdit
                                                                                           End Sub)
                                                         configuracionGridView.Columns.Add(Sub(configuracionColumns)
                                                                                               configuracionColumns.FieldName = "pag_nombre"
                                                                                               configuracionColumns.Caption = "Nombre"
                                                                                               configuracionColumns.ColumnType = MVCxGridViewColumnType.TextBox
                                                                                           End Sub)
                                                         configuracionGridView.Columns.Add(Sub(configuracionColumns)
                                                                                               configuracionColumns.FieldName = "pag_descripcion"
                                                                                               configuracionColumns.Caption = "Descripción"
                                                                                               configuracionColumns.ColumnType = MVCxGridViewColumnType.Memo
                                                                                           End Sub)
                                                     End Sub)
    If ViewData("EditError") IsNot Nothing Then
        gridEditarPagos.SetEditErrorText(CStr(ViewData("EditError")))
    End If
    
    gridEditarPagos.Bind(Model).Render()
%>