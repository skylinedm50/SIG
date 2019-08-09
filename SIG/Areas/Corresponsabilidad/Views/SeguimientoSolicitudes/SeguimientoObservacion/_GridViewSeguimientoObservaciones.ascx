<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>


<%--<%
    Html.DevExpress.GridView(Sub(configuracionGridView As GridViewSettings)
                                 configuracionGridView.Name = "AspxGridViewSeguimientoObservaciones"
                                 configuracionGridView.CallbackRouteValues = New With {Key .Controller = "SeguimientoSolicitudes", Key .Action = "GridViewSeguimientoObservaciones"}

                                 configuracionGridView.Caption = "Observaciones por Solicitud"
                                 configuracionGridView.KeyFieldName = "codigo_observacion"
                                 configuracionGridView.ControlStyle.CssClass = "GridViewCorres"

                                 configuracionGridView.SettingsPager.Visible = True
                                 configuracionGridView.Settings.ShowGroupPanel = True
                                 configuracionGridView.Settings.ShowFilterRow = True
                                 configuracionGridView.SettingsBehavior.AllowSelectByRowClick = True

                                 configuracionGridView.SettingsPager.PageSizeItemSettings.Visible = True
                                 configuracionGridView.SettingsPager.PageSizeItemSettings.Items = {"50", "100", "150"}
                                 configuracionGridView.SettingsPager.PageSize = 30
                                 configuracionGridView.Width = 1200

                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                       configuracionColumn.FieldName = "codigo_observacion"
                                                                       configuracionColumn.Caption = "Código"
                                                                       configuracionColumn.Width = 150
                                                                       configuracionColumn.ColumnType = MVCxGridViewColumnType.SpinEdit

                                                                       Dim SpinEditPropiedades As SpinEditProperties = configuracionColumn.PropertiesEdit

                                                                       SpinEditPropiedades.MinValue = 1
                                                                       SpinEditPropiedades.MaxValue = 10000000
                                                                   End Sub)
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                       configuracionColumn.FieldName = "codigo_solicitud"
                                                                       configuracionColumn.Caption = "Código Solicitud"
                                                                       configuracionColumn.Width = 150
                                                                       configuracionColumn.ColumnType = MVCxGridViewColumnType.SpinEdit

                                                                       Dim SpinEditPropiedades As SpinEditProperties = configuracionColumn.PropertiesEdit

                                                                       SpinEditPropiedades.MinValue = 1
                                                                       SpinEditPropiedades.MaxValue = 10000000
                                                                   End Sub)
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                       configuracionColumn.FieldName = "observacion"
                                                                       configuracionColumn.Caption = "Observación"

                                                                       configuracionColumn.ColumnType = MVCxGridViewColumnType.Memo
                                                                   End Sub)
                                 configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                                       configuracionColumn.FieldName = "fecha"
                                                                       configuracionColumn.Caption = "Fecha Ingreso"
                                                                       configuracionColumn.Width = 190

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
                                                                       configuracionColumn.FieldName = "nombre_usuario"
                                                                       configuracionColumn.Caption = "Usuario"

                                                                       configuracionColumn.ColumnType = MVCxGridViewColumnType.Memo
                                                                   End Sub)
                             End Sub).Bind(Model).Render()
%>--%>

<% 
    Dim settings As GridViewSettings = SIG.SIG.Areas.Corresponsabilidad.Controllers.exportGdvSeguimientoObservaciones.ExportGridViewSettings
    'settings.CallbackRouteValues = New With {Key .Controller = "SeguimientoSolicitudes", Key .Action = "pv_pvgConsolidadoPago"}
    Html.DevExpress().GridView(settings).Bind(Model).GetHtml()


%>