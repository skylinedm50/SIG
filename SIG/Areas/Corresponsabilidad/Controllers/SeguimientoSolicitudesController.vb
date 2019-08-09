Imports DevExpress.Web
Imports DevExpress.Web.Mvc
Imports DevExpress.Export

Namespace SIG.Areas.Corresponsabilidad.Controllers
    Public Class SeguimientoSolicitudesController
        Inherits System.Web.Mvc.Controller

        '
        ' GET: /Corresponsabilidad/SeguimientoSolicitudes

        Private login As New Global.SIG.Cl_Login


        Function GridViewSeguimientoObservaciones()
            Dim objSeguimientoObservacion As New SIG.Areas.Corresponsabilidad.Models.cl_seguimiento_observaciones()

            Return View("SeguimientoObservacion/_GridViewSeguimientoObservaciones", objSeguimientoObservacion.obtener_gestion_observaciones())
        End Function

        Function exportGridViewSeguimientoObservaciones()

            Dim objSeguimientoObservacion As New SIG.Areas.Corresponsabilidad.Models.cl_seguimiento_observaciones()
            Dim export As String = EditorExtension.GetValue(Of String)("cbxExpotar")
            Dim dt As DataTable = objSeguimientoObservacion.obtener_gestion_observaciones()
            Dim settings As GridViewSettings = exportGdvSeguimientoObservaciones.ExportGridViewSettings

            If export = "Excel" Then
                Return GridViewExtension.ExportToXlsx(settings, dt, "Seguimiento y Observaciones", ExportType.WYSIWYG)
            ElseIf export = "Pdf" Then
                Return GridViewExtension.ExportToPdf(settings, dt, "Seguimiento y Observaciones", ExportType.WYSIWYG)
            ElseIf export = "Rtf" Then
                Return GridViewExtension.ExportToRtf(settings, dt, "Seguimiento y Observaciones", ExportType.WYSIWYG)
            ElseIf export = "Csv" Then
                Return GridViewExtension.ExportToCsv(settings, dt, "Seguimiento y Observaciones", ExportType.WYSIWYG)
            End If

            Return Nothing


        End Function



#Region "Gestión de Observaciones"
        Function viewGetionObservaciones() As ActionResult
            Return View()
        End Function

        <HttpPost()>
        Function fnc_ingresar_observacion(ByVal intCodSolicitud As Integer, ByVal strObservacion As String) As Integer
            If login.Fnc_loggeado() IsNot Nothing Then
                Dim objSeguimientoObservacion As New SIG.Areas.Corresponsabilidad.Models.cl_gestion_observaciones()

                Return objSeguimientoObservacion.nueva_gestion(intCodSolicitud, strObservacion, HttpContext.Session("usuario"))
            Else
                Return 2
            End If

        End Function

#End Region

#Region "Seguimiento de Observaciones"
        Function viewSeguimientoObservaciones() As ActionResult
            Return View()
        End Function
#End Region

    End Class

    Public NotInheritable Class exportGdvSeguimientoObservaciones

        Private Shared exportSettings As GridViewSettings

        Private Sub New()
        End Sub

        Public Shared ReadOnly Property ExportGridViewSettings() As GridViewSettings

            Get
                exportSettings = CreateExportGdvSeguimientoObservaciones()
                Return exportSettings
            End Get

        End Property

        Private Shared Function CreateExportGdvSeguimientoObservaciones() As GridViewSettings

            Dim configuracionGridView As New GridViewSettings()

            configuracionGridView.Name = "AspxGridViewSeguimientoObservaciones"
            configuracionGridView.CallbackRouteValues = New With {Key .Controller = "SeguimientoSolicitudes", Key .Action = "GridViewSeguimientoObservaciones"}

            configuracionGridView.Caption = "Observaciones por Solicitud"
            configuracionGridView.KeyFieldName = "codigo_observacion"
            configuracionGridView.ControlStyle.CssClass = "GridViewCorres"

            configuracionGridView.SettingsPager.Visible = True
            configuracionGridView.Settings.ShowGroupPanel = True
            configuracionGridView.Settings.ShowFilterRow = True
            configuracionGridView.SettingsBehavior.AllowSelectByRowClick = True

            configuracionGridView.SettingsExport.FileName = "Observaciones por Solicitud"
            configuracionGridView.SettingsExport.ReportHeader = "Observaciones por Solicitud"
            configuracionGridView.SettingsExport.Landscape = True
            configuracionGridView.SettingsExport.PaperKind = Drawing.Printing.PaperKind.Letter


            configuracionGridView.SettingsPager.PageSizeItemSettings.Visible = True
            configuracionGridView.SettingsPager.PageSizeItemSettings.Items = {"50", "100", "150"}
            configuracionGridView.SettingsPager.PageSize = 30
            configuracionGridView.Width = 1200

            configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                  configuracionColumn.FieldName = "codigo_observacion"
                                                  configuracionColumn.Caption = "Código"
                                                  'configuracionColumn.Width = 150
                                                  configuracionColumn.ColumnType = MVCxGridViewColumnType.SpinEdit

                                                  Dim SpinEditPropiedades As SpinEditProperties = configuracionColumn.PropertiesEdit

                                                  SpinEditPropiedades.MinValue = 1
                                                  SpinEditPropiedades.MaxValue = 10000000
                                              End Sub)
            configuracionGridView.Columns.Add(Sub(configuracionColumn As MVCxGridViewColumn)
                                                  configuracionColumn.FieldName = "codigo_solicitud"
                                                  configuracionColumn.Caption = "Código Solicitud"
                                                  'configuracionColumn.Width = 150
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
                                                  'configuracionColumn.Width = 190

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

            Return configuracionGridView

        End Function

    End Class



End Namespace