<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Html.DevExpress().GridView(
                         Sub(settings)
                             settings.Name = "gdvProgBancarizacion"
                             settings.Caption = "Tiempo Para Apertura de Cuentas Básica"
                             settings.CallbackRouteValues = New With {Key .Controller = "ProgramacionPago", Key .Action = "pv_gdvPeriodoApertura", Key .pago = ViewData("pago")}
                             settings.SettingsEditing.UpdateRowRouteValues = New With {Key .Controller = "ProgramacionPago", Key .Action = "actualizarPeriodoApertura", Key .pago = ViewData("pago")}
                             settings.SettingsEditing.Mode = GridViewEditingMode.Inline
                             settings.Width = Unit.Pixel(700)

                             settings.KeyFieldName = "Codigo_Pagador"

                             settings.CommandColumn.Visible = True
                             settings.CommandColumn.ShowEditButton = True
                             settings.CommandColumn.ShowNewButton = False
                             settings.CommandColumn.ShowDeleteButton = False
                             settings.CommandColumn.ShowNewButtonInHeader = False

                             settings.EnableCallbackAnimation = True
                             settings.EnablePagingCallbackAnimation = True
                             settings.EnableCallbackCompression = True

                             settings.Columns.Add(
                                 Sub(col)
                                     col.FieldName = "Codigo_Pagador"
                                     col.Caption = "Código"
                                     col.Visible = False
                                 End Sub)
                             settings.Columns.Add(
                                 Sub(col)
                                     col.FieldName = "Nombre_Pagador"
                                     col.Caption = "Pagador"
                                     col.ReadOnly = True
                                 End Sub)
                             settings.Columns.Add(
                               Sub(col)
                                   col.FieldName = "fecha_inicio"
                                   col.Caption = "Inicio"
                                   col.ColumnType = MVCxGridViewColumnType.DateEdit
                                   Dim dte As DateEditProperties = col.PropertiesEdit
                                   dte.ClientInstanceName = "txtFechaInicio"
                                   dte.ClientSideEvents.DateChanged = "function(s,e){fnc_validarFechas('inicio');}"
                               End Sub)
                             settings.Columns.Add(
                               Sub(col)
                                   col.FieldName = "fecha_fin"
                                   col.Caption = "Fin"
                                   col.ColumnType = MVCxGridViewColumnType.DateEdit
                                   Dim dte As DateEditProperties = col.PropertiesEdit
                                   dte.ClientInstanceName = "txtFechaFin"
                                   dte.ClientSideEvents.DateChanged = "function(s,e){fnc_validarFechas('fin');}"
                               End Sub)
                         End Sub).Bind(Model).GetHtml() %>