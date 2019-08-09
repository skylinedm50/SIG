Imports System.Web.Script.Serialization
Imports DevExpress.XtraCharts.Web
Imports DevExpress.XtraCharts
Imports System.Web.SessionState
Imports System.IO
Imports DevExpress.Web.Mvc
Imports System.Threading

Namespace SIG.Areas.Incorporaciones.Controllers
    Public Class ReportesController
        Inherits System.Web.Mvc.Controller

        '
        ' GET: /Reportes
#Region "Variables"
        Dim lista As New List(Of Dictionary(Of String, Object))
        Dim Obj_info_data As New DataTable()
        Dim serializador As New JavaScriptSerializer()
        Dim Dictionary_Datos As Dictionary(Of String, Object)
        Dim obj_Hogar_Beneficiario As New SIG.Areas.Incorporaciones.Models.Cl_Hogar_Beneficiario()
        Dim Obj_Usuarios As New SIG.Areas.Incorporaciones.Models.Cl_Usuarios()
        Dim Usuario As New Global.SIG.Cl_Login
        Private Shared pv_grid_setting As PivotGridSettings
#End Region


#Region "propiedades"
        Public Shared ReadOnly Property _Pv_gridSetting() As PivotGridSettings
            Get
                pv_grid_setting = GridExport.pv_actualizacion_usuarios()
                Return pv_grid_setting
            End Get
        End Property

#End Region

#Region "Reporte Actualizaciones"

        Public Function Fnc_ActualizacionUsuario()
            If Usuario.Fnc_loggeado() IsNot Nothing Then
                If Usuario.fnc_validarModulo(1) Then
                    If Usuario.Fnc_ValidarPermisoUrl(Request.Url.AbsolutePath) Then
                        Return View("ActualizacionesPorUsuario/ActualizacionUsuario")
                    Else
                        Return Redirect("/Incorporaciones/Inicio")
                    End If
                Else
                    Return Redirect("/Home/MenuPrincipal")
                End If
            Else
                Return Redirect("/Home/Login")
            End If
        End Function

        Public Function fnc_PivotGridReporteActualizacionesUsuario()
            Return PartialView("ActualizacionesPorUsuario/PivotGridReporteUsuario", obj_Hogar_Beneficiario.Fnc_ActualizacionesRealizadas())
        End Function

        Public Function Exportar_pv_reporteActualizacionUsuario()
            Dim Setting As PivotGridSettings = SIG.Areas.Incorporaciones.Controllers.GridExport.pv_actualizacion_usuarios()
            Return PivotGridExtension.ExportToXlsx(Setting, obj_Hogar_Beneficiario.Fnc_ActualizacionesRealizadas(), "Hogares actualizados por usuario")
        End Function

        Public Function Fnc_HogaresActualizados()
            If Usuario.Fnc_loggeado() IsNot Nothing Then
                If Usuario.fnc_validarModulo(1) Then
                    If Usuario.Fnc_ValidarPermisoUrl(Request.Url.AbsolutePath) Then
                        Return View("Hogares Actualizados/Hogares_actualizados")
                    Else
                        Return Redirect("/Incorporaciones/Inicio")
                    End If
                Else
                    Return Redirect("/Home/MenuPrincipal")
                End If
            Else
                Return Redirect("/Home/Login")
            End If
        End Function

        Public Function pv_Hogares_actualizados()
            Return PartialView("Hogares Actualizados/pv_Hogares_actualizados", obj_Hogar_Beneficiario.Fnc_Actualizaciones_Hogares())
        End Function

        Public Function Exportar_pv_reporteHogares_actualizados()
            Dim Setting As PivotGridSettings = SIG.Areas.Incorporaciones.Controllers.GridExport.pv_hogares_actualizados()
            Return PivotGridExtension.ExportToXlsx(Setting, obj_Hogar_Beneficiario.Fnc_Actualizaciones_Hogares(), "Hogares Actualizados")

        End Function


        Public Function Fnc_HogaresNuevasIncorporaciones()
            If Usuario.Fnc_loggeado() IsNot Nothing Then
                If Usuario.fnc_validarModulo(1) Then
                    If Usuario.Fnc_ValidarPermisoUrl(Request.Url.AbsolutePath) Then
                        Return View("Hogares Nueva Incorporacion/HogarNuevaIncorporacion")
                    Else
                        Return Redirect("/Incorporaciones/Inicio")
                    End If
                Else
                    Return Redirect("/Home/MenuPrincipal")
                End If
            Else
                Return Redirect("/Home/Login")
            End If
        End Function

        Public Function pv_HogaresNuevaIncorporacion()
            Return PartialView("Hogares Nueva Incorporacion/pv_HogaresNuevaIncorporacion", obj_Hogar_Beneficiario.Fnc_ReporteHogaresNuevasIncorporaciones())
        End Function


        Public Function Exportar_pv_reporteHogares_nuevos_miembros()
            Dim Setting As PivotGridSettings = SIG.Areas.Incorporaciones.Controllers.GridExport.pv_hogares_nuevos_miembros()
            Return PivotGridExtension.ExportToXlsx(Setting, obj_Hogar_Beneficiario.Fnc_ReporteHogaresNuevasIncorporaciones(), "Hogares Con Nuevas Incorporaciones")

        End Function


        Public Function Fnc_VerificadosUsuarios()
            If Usuario.Fnc_loggeado() IsNot Nothing Then
                If Usuario.fnc_validarModulo(1) Then
                    If Usuario.Fnc_ValidarPermisoUrl(Request.Url.AbsolutePath) Then
                        Return View("Hogares Verificados por Usuarios/HogaresVerificadosUsuario")
                    Else
                        Return Redirect("/Incorporaciones/Inicio")
                    End If
                Else
                    Return Redirect("/Home/MenuPrincipal")
                End If
            Else
                Return Redirect("/Home/Login")
            End If
        End Function

        Public Function fnc_PivotGridReporteVerificacionesUsuario()
            Return PartialView("Hogares Verificados por Usuarios/PivotGridReporteVerificado", obj_Hogar_Beneficiario.Fnc_VerificacionesRealizadas())
        End Function

        Public Function Exportar_pv_reporteVerificacionesUsuario()
            Dim Setting As PivotGridSettings = SIG.Areas.Incorporaciones.Controllers.GridExport.pv_verificacion_usuarios()
            Return PivotGridExtension.ExportToXlsx(Setting, obj_Hogar_Beneficiario.Fnc_VerificacionesRealizadas(), "Hogares verificados por usuario")
        End Function
#End Region


    End Class

    Partial Public Class GridExport

        Public Shared Function CrearGridExport()
            Dim setting As New GridViewSettings()
            setting.Name = "gridLevantamientos"
            setting.Caption = "REPORTES DE FICHAS SOCIOECONOMICAS UNICA DEL REGISTRO DE PARTICIPANRES (FSU-RUP)"
            setting.Theme = "DevEx"
            setting.Styles.Header.Font.Bold = True
            setting.Styles.Header.Font.Italic = True
            setting.Styles.Header.Font.Name = "Arial"
            setting.Styles.Header.Font.Size = 10
            setting.StylesPager.Pager.Paddings.PaddingBottom = 20
            setting.StylesPager.Pager.Paddings.PaddingLeft = 10
            setting.StylesPager.Pager.Paddings.PaddingRight = 5
            setting.SettingsPager.PageSize = 18
            setting.Styles.Header.Border.BorderColor = System.Drawing.Color.Black
            setting.Styles.Header.Border.BorderStyle = WebControls.BorderStyle.Outset
            setting.Width = 500
            setting.Columns.Add("cod_levantamiento", "N°.")
            setting.Columns.Add("desc_departamento", "Departamento")
            setting.Columns.Add("desc_municipio", "Municipio")
            setting.Columns.Add("desc_aldea", "Aldea")
            setting.Columns.Add("desc_caserio", "Caserio")
            setting.Columns.Add("dir_lev", "Dirección")
            setting.Columns.Add("nom_cen_edu", "Centro Educativo")
            setting.Columns.Add("total_ficha", "Total Fichas Levantadas")
            setting.Columns.Add("Observaciones", "Observaciones")
            setting.Columns.Add("fch_inicio_lev", "Fecha del Levantamiento").PropertiesEdit.DisplayFormatString = "d"
            setting.Columns.Add("fecha_envio", "Fecha de entrega al CENISS").PropertiesEdit.DisplayFormatString = "d"
            setting.Columns.Add("memorando", "Memorando")
            setting.SettingsExport.FileName = "Archivo"
            setting.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Rotated
            Return setting
        End Function

        Public Shared Function ExportGrodDetalleLev()
            Dim setting As New GridViewSettings()
            setting.Name = "gridLevantamientos"
            setting.Caption = "REPORTES DE LEVANTAMIENTOS REALIZADOS"
            setting.Theme = "DevEx"
            setting.Styles.Header.Font.Bold = True
            setting.Styles.Header.Font.Italic = True
            setting.Styles.Header.Font.Name = "Arial"
            setting.Styles.Header.Font.Size = 10
            setting.StylesPager.Pager.Paddings.PaddingBottom = 20
            setting.StylesPager.Pager.Paddings.PaddingLeft = 10
            setting.StylesPager.Pager.Paddings.PaddingRight = 5
            setting.Styles.Header.Border.BorderColor = System.Drawing.Color.Black
            setting.Styles.Header.Border.BorderStyle = WebControls.BorderStyle.Outset
            setting.Width = 1000
            setting.Columns.Add("cod_levantamiento", "N°.")
            setting.Columns.Add("fch_inicio_lev", "Fecha del Levantamiento").PropertiesEdit.DisplayFormatString = "d"
            setting.Columns.Add("desc_tipo_lev", "Tipo de levantamiento")
            setting.Columns.Add("desc_departamento", "Departamento")
            setting.Columns.Add("desc_municipio", "Municipio")
            setting.Columns.Add("desc_aldea", "Aldea")
            setting.Columns.Add("desc_caserio", "Caserio")
            setting.Columns.Add("nom_cen_edu", "Centro Educativo")
            setting.Columns.Add("nuevos", "Nuevos Hogares")
            setting.Columns.Add("actualizados", "Hogares Actualizados")
            setting.Columns.Add("No recibidas", "Fichas no recibidas")
            setting.SettingsExport.FileName = "Reporte de levantamientos realizados"
            setting.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A3Rotated
            Return setting
        End Function

        Public Shared Function pv_actualizacion_usuarios() As PivotGridSettings

            Dim pv_grid_setting As New PivotGridSettings()
            pv_grid_setting.Name = "pv_gridsetting"
            pv_grid_setting.Width = 1000
            pv_grid_setting.OptionsPager.RowsPerPage = 30
            pv_grid_setting.CallbackRouteValues = New With {Key .Controller = "Reportes", Key .Action = "fnc_PivotGridReporteActualizacionesUsuario"}
            pv_grid_setting.Fields.Add(Sub(field)
                                           field.AreaIndex = 0
                                           field.FieldName = "NOMBRE_USUARIO"
                                           field.Caption = "Nombre del Usuario"
                                       End Sub)

            pv_grid_setting.Fields.Add(Sub(field)
                                           field.AreaIndex = 0
                                           field.FieldName = "FECHA"
                                           field.SortOrder = DevExpress.XtraPivotGrid.PivotSortOrder.Descending
                                           field.Caption = "Fecha de Actualización"
                                           field.ValueFormat.FormatString = "dd/MM/yyyy"
                                           field.ValueFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                                       End Sub)

            pv_grid_setting.Fields.Add(Sub(field)
                                           field.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
                                           'field.AreaIndex = 0
                                           field.FieldName = "CANTIDAD_HOGARES"
                                           field.Caption = "Cantidad de hogares"
                                       End Sub)

            Return pv_grid_setting
        End Function

        Public Shared Function pv_hogares_actualizados() As PivotGridSettings

            Dim pv_grid_setting As New PivotGridSettings()
            pv_grid_setting.Name = "pv_setting"
            pv_grid_setting.Width = 1000
            pv_grid_setting.OptionsPager.RowsPerPage = 30
            pv_grid_setting.CallbackRouteValues = New With {Key .Controller = "Reportes", Key .Action = "pv_Hogares_actualizados"}

            pv_grid_setting.Fields.Add(Sub(field)
                                           field.AreaIndex = 0

                                           field.FieldName = "desc_municipio"
                                           field.Caption = "Municipio"
                                       End Sub)

            pv_grid_setting.Fields.Add(Sub(field)
                                           field.AreaIndex = 0

                                           field.FieldName = "desc_departamento"
                                           field.Caption = "Departamento"
                                       End Sub)

            pv_grid_setting.Fields.Add(Sub(field)
                                           field.AreaIndex = 0
                                           field.FieldName = "Fecha"
                                           field.Area = DevExpress.XtraPivotGrid.PivotArea.FilterArea
                                           field.SortOrder = DevExpress.XtraPivotGrid.PivotSortOrder.Descending
                                           field.Caption = "Fecha de Actualización"
                                           field.ValueFormat.FormatString = "dd/MM/yyyy"
                                           field.ValueFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                                       End Sub)

            pv_grid_setting.Fields.Add(Sub(field)
                                           field.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
                                           field.AreaIndex = 0
                                           field.FieldName = "Cantidad_hogares"
                                           field.Caption = "hogares"
                                       End Sub)

            Return pv_grid_setting

        End Function

        Public Shared Function pv_hogares_nuevos_miembros() As PivotGridSettings

            Dim pv_grid_setting As New PivotGridSettings()
            pv_grid_setting.Name = "pv_gridsetting"
            pv_grid_setting.Width = 1000
            pv_grid_setting.OptionsPager.RowsPerPage = 30
            pv_grid_setting.CallbackRouteValues = New With {Key .Controller = "Reportes", Key .Action = "pv_HogaresNuevaIncorporacion"}
            pv_grid_setting.Fields.Add(Sub(field)
                                           field.AreaIndex = 0
                                           field.FieldName = "Usuario"
                                           field.Caption = "Nombre del Usuario"
                                       End Sub)

            pv_grid_setting.Fields.Add(Sub(field)
                                           field.AreaIndex = 0
                                           field.FieldName = "Fecha_Actualizacion"
                                           field.SortOrder = DevExpress.XtraPivotGrid.PivotSortOrder.Descending
                                           field.Caption = "Fecha de Actualización"
                                           field.ValueFormat.FormatString = "dd/MM/yyyy"
                                           field.ValueFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                                       End Sub)

            pv_grid_setting.Fields.Add(Sub(field)
                                           field.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
                                           'field.AreaIndex = 0
                                           field.FieldName = "Cantidad_Hogares"
                                           field.Caption = "Cantidad de hogares"
                                       End Sub)

            Return pv_grid_setting

        End Function

        Public Shared Function pv_verificacion_usuarios() As PivotGridSettings

            Dim pv_grid_setting As New PivotGridSettings()
            pv_grid_setting.Name = "pv_gridsetting"
            pv_grid_setting.Width = 1000
            pv_grid_setting.OptionsPager.RowsPerPage = 30
            pv_grid_setting.CallbackRouteValues = New With {Key .Controller = "Reportes", Key .Action = "fnc_PivotGridReporteVerificacionesUsuario"}
            pv_grid_setting.Fields.Add(Sub(field)
                                           field.AreaIndex = 0
                                           field.FieldName = "Nombre_Usuario"
                                           field.Caption = "Nombre del Usuario"
                                       End Sub)

            pv_grid_setting.Fields.Add(Sub(field)
                                           field.AreaIndex = 0
                                           field.FieldName = "Fch_Verificacion"
                                           field.SortOrder = DevExpress.XtraPivotGrid.PivotSortOrder.Descending
                                           field.Caption = "Fecha de Actualización"
                                           field.ValueFormat.FormatString = "dd/MM/yyyy"
                                           field.ValueFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                                       End Sub)

            pv_grid_setting.Fields.Add(Sub(field)
                                           field.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
                                           field.FieldName = "Cantidad_Hogares"
                                           field.Caption = "Cantidad de hogares"
                                       End Sub)

            Return pv_grid_setting
        End Function


    End Class


End Namespace
