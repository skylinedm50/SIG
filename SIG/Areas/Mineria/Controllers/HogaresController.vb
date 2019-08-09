Imports DevExpress.Web.Mvc
Imports DevExpress.Web
Imports DevExpress.Utils

'Imports System.Collections.Generic
'Imports System.Web.Mvc
'Imports System.Web.UI.WebControls
'Imports DevExpress.Web.ASPxPivotGrid
'Imports DevExpress.XtraExport
'Imports DevExpress.XtraPrinting
''Imports DevExpress.XtraPivotGrid

Namespace SIG.Areas.Mineria.Controllers
    Public Class HogaresController
        Inherits System.Web.Mvc.Controller

        '
        ' GET: /Mineria/Hogares

        Dim login As New Global.SIG.Cl_Login
        Dim hogares As SIG.Areas.Mineria.Models.Cl_Hogares = New SIG.Areas.Mineria.Models.Cl_Hogares

#Region "Funciones para el historial de pagos del participante"

        Function v_HistorialPagos() As ActionResult

            Response.Cookies("opciones").Expires = DateTime.Now.AddDays(-1)
            Response.Cookies("actividad").Expires = DateTime.Now.AddDays(-1)

            If login.Fnc_loggeado() IsNot Nothing Then
                If login.fnc_validarModulo(7) Then
                    login.Fnc_MenuModulo(7)
                    Return View()
                End If
            Else
                Return Redirect("/Home/Login")
            End If

        End Function

        Function pv_gdvHistorialPagos(ByVal hogar As String) As ActionResult
            ViewData("hogar") = hogar
            Return PartialView(hogares.Fnc_obtener_historial_pagos_hogar(hogar))
        End Function

        Function pv_PagoHogar(ByVal pago As String, ByVal hogar As String) As ActionResult

            Dim ds As DataSet = hogares.Fnc_obtener_pago_hogar(pago, hogar)

            ViewData("pago") = pago
            ViewData("hogar") = hogar

            ViewData("identidad") = ds.Tables("info_planilla").Rows(0).Item("identidad_titular")
            ViewData("nombre") = ds.Tables("info_planilla").Rows(0).Item("nombre_titular")
            ViewData("estado") = ds.Tables("info_planilla").Rows(0).Item("estado")
            ViewData("monto") = ds.Tables("info_planilla").Rows(0).Item("monto_total")
            ViewData("deduccion") = ds.Tables("info_planilla").Rows(0).Item("deducciones")
            ViewData("elegibilidad") = ds.Tables("info_planilla").Rows(0).Item("nombre_elegibilidad")
            ViewData("desc_elegibilidad") = ds.Tables("info_planilla").Rows(0).Item("desc_elegibilidad")
            ViewData("componente") = ds.Tables("info_planilla").Rows(0).Item("nombre_componente")
            ViewData("desc_componente") = ds.Tables("info_planilla").Rows(0).Item("desc_componente")
            ViewData("corresponsabilidad") = ds.Tables("info_planilla").Rows(0).Item("nombre_corresponsabilidad")
            ViewData("desc_corresponsabilidad") = ds.Tables("info_planilla").Rows(0).Item("desc_correponsabilidad")
            ViewData("proyeccion") = ds.Tables("info_planilla").Rows(0).Item("proyeccion")
            ViewData("proyeccion_corta") = ds.Tables("info_planilla").Rows(0).Item("proyeccion_corta")
            ViewData("cobro") = ds.Tables("info_planilla").Rows(0).Item("cobro")
            ViewData("fecha_cobro") = ds.Tables("info_planilla").Rows(0).Item("fecha_cobro")
            ViewData("estado_hogar") = ds.Tables("info_planilla").Rows(0).Item("estado_hogar")

            Return PartialView(ds.Tables("estado_cuenta"))

        End Function

        Function pv_gdvPersonas(ByVal identidad As String, ByVal nombre As String) As ActionResult

            If identidad = Nothing Then
                identidad = ""
            End If

            If nombre = Nothing Then
                nombre = ""
            End If

            ViewData("identidad") = identidad
            ViewData("nombre") = nombre

            Return PartialView(hogares.fnc_obtener_personas(identidad, nombre))

        End Function

        Function pv_gdvListadoNinos(ByVal pago As String, ByVal hogar As String) As ActionResult
            ViewData("pago") = pago
            ViewData("hogar") = hogar
            Return PartialView(hogares.Fnc_listado_ninos(pago, hogar))
        End Function

        Function pv_DetalleCorreponsabalidadNino(ByVal pago As String, ByVal cod_persona As String) As ActionResult

            Dim dt As DataTable = hogares.Fnc_detalle_correponsabilidad_nino(pago, cod_persona)

            'información del niño
            ViewData("identidad") = dt.Rows(0).Item("identidad").ToString()
            ViewData("fecha_nacimiento") = dt.Rows(0).Item("fecha_nacimiento").ToString()

            If dt.Rows(0).Item("sexo_persona").ToString() = "1" Then
                ViewData("genero") = "Masculino"
            ElseIf dt.Rows(0).Item("sexo_persona").ToString() = "2" Then
                ViewData("genero") = "Femenino"
            End If
            'ViewData("") = dt.Rows(0).Item("sexo_persona")
            ViewData("nombre") = dt.Rows(0).Item("nombre_persona")

            'información correponsabilidad
            ViewData("elegibilidad") = dt.Rows(0).Item("elegibilidad")
            ViewData("ciclo") = dt.Rows(0).Item("desc_nivel_elegibilidad")
            ViewData("correponsabilidad") = dt.Rows(0).Item("desc_corresponsabilidad")
            ViewData("cumplimiento") = dt.Rows(0).Item("estado_corresponsabilidad")

            'detalle correponsabilidad
            ViewData("centro") = dt.Rows(0).Item("nombre_centro_educativo_salud")
            ViewData("año") = dt.Rows(0).Item("año")
            ViewData("grado") = dt.Rows(0).Item("nombre_grado").ToString()
            ViewData("nro_visitas") = dt.Rows(0).Item("numero_visitas_centro_salud").ToString()
            ViewData("ultima_visita") = dt.Rows(0).Item("fecha_ultima_visita").ToString()

            Return PartialView()

        End Function

#End Region

#Region "Funciones para la cantidad de fichas por censo y año"

        Function v_FichasPorCensoAno() As ActionResult

            Response.Cookies("opciones").Expires = DateTime.Now.AddDays(-1)
            Response.Cookies("actividad").Expires = DateTime.Now.AddDays(-1)

            If login.Fnc_loggeado() IsNot Nothing Then
                'Menu.Fnc_MenuModulo()  
                If login.fnc_validarModulo(7) Then
                    login.Fnc_MenuModulo(7)
                    Return View()
                End If
            Else
                Return Redirect("/Home/Login")
            End If

        End Function

        Function pv_pvgFichasPorCensoAno() As ActionResult
            Return PartialView(hogares.Fnc_obtener_fichas_censo_ano())
        End Function

        Function exportarFichasPorCensoAno() As ActionResult

            Dim export As String = ComboBoxExtension.GetValue(Of String)("cbxExpotar")

            If export = "Excel" Then
                Return PivotGridExtension.ExportToXlsx(SIG.Areas.Mineria.Controllers.exportPvgFichasPorCensoAno.exportPvgSettings, hogares.Fnc_obtener_fichas_censo_ano, "Cantidad de Fichas Por Censo y Año")
            ElseIf export = "Pdf" Then
                Return PivotGridExtension.ExportToPdf(SIG.Areas.Mineria.Controllers.exportPvgFichasPorCensoAno.exportPvgSettings, hogares.Fnc_obtener_fichas_censo_ano, "Cantidad de Fichas Por Censo y Año")
            ElseIf export = "Rtf" Then
                Return PivotGridExtension.ExportToRtf(SIG.Areas.Mineria.Controllers.exportPvgFichasPorCensoAno.exportPvgSettings, hogares.Fnc_obtener_fichas_censo_ano, "Cantidad de Fichas Por Censo y Año")
            ElseIf export = "Html" Then
                Return PivotGridExtension.ExportToHtml(SIG.Areas.Mineria.Controllers.exportPvgFichasPorCensoAno.exportPvgSettings, hogares.Fnc_obtener_fichas_censo_ano, "Cantidad de Fichas Por Censo y Año")
            ElseIf export = "Csv" Then
                Return PivotGridExtension.ExportToCsv(SIG.Areas.Mineria.Controllers.exportPvgFichasPorCensoAno.exportPvgSettings, hogares.Fnc_obtener_fichas_censo_ano, "Cantidad de Fichas Por Censo y Año")
            End If

            Return Nothing

        End Function

#End Region

#Region "Funciones para el Resultado de Fichas"

        Function v_ResultadoFichas() As ActionResult

            Response.Cookies("opciones").Expires = DateTime.Now.AddDays(-1)
            Response.Cookies("actividad").Expires = DateTime.Now.AddDays(-1)

            If login.Fnc_loggeado() IsNot Nothing Then
                'Menu.Fnc_MenuModulo()  
                If login.fnc_validarModulo(7) Then
                    login.Fnc_MenuModulo(7)
                    Return View()
                End If
            Else
                Return Redirect("/Home/Login")
            End If

        End Function

        Function pv_pvgResultadoFichas() As ActionResult
            Return PartialView(hogares.Fnc_obtener_resultado_fichas())
        End Function

        Function exportarResultadoFichas() As ActionResult

            Dim export As String = ComboBoxExtension.GetValue(Of String)("cbxExpotar")

            If export = "Excel" Then
                Return PivotGridExtension.ExportToXlsx(SIG.Areas.Mineria.Controllers.exportPvgResultadoFichas.exportPvgSettings, hogares.Fnc_obtener_resultado_fichas, "Resultado Fichas")
            ElseIf export = "Pdf" Then
                Return PivotGridExtension.ExportToPdf(SIG.Areas.Mineria.Controllers.exportPvgResultadoFichas.exportPvgSettings, hogares.Fnc_obtener_resultado_fichas, "Resultado Fichas")
            ElseIf export = "Rtf" Then
                Return PivotGridExtension.ExportToRtf(SIG.Areas.Mineria.Controllers.exportPvgResultadoFichas.exportPvgSettings, hogares.Fnc_obtener_resultado_fichas, "Resultado Fichas")
            ElseIf export = "Html" Then
                Return PivotGridExtension.ExportToHtml(SIG.Areas.Mineria.Controllers.exportPvgResultadoFichas.exportPvgSettings, hogares.Fnc_obtener_resultado_fichas, "Resultado Fichas")
            ElseIf export = "Csv" Then
                Return PivotGridExtension.ExportToCsv(SIG.Areas.Mineria.Controllers.exportPvgResultadoFichas.exportPvgSettings, hogares.Fnc_obtener_resultado_fichas, "Resultado Fichas")
            End If

            Return Nothing

        End Function

#End Region

#Region "Fichas no remitidas por CENNISS"

        Function v_FichasNoRemitidas() As ActionResult

            Response.Cookies("opciones").Expires = DateTime.Now.AddDays(-1)
            Response.Cookies("actividad").Expires = DateTime.Now.AddDays(-1)

            If login.Fnc_loggeado() IsNot Nothing Then
                'Menu.Fnc_MenuModulo()  
                If login.fnc_validarModulo(7) Then
                    login.Fnc_MenuModulo(7)
                    ViewData("fechas") = hogares.Fnc_obtener_fechas_envio()
                    Return View()
                End If
            Else
                Return Redirect("/Home/Login")
            End If

        End Function

        Function pv_crtFichasNoRemitidas(ByVal departamento As String, ByVal municipio As String, ByVal aldea As String, ByVal fechas As String)

            If departamento = "" Then
                departamento = Nothing
            End If

            If municipio = "" Then
                municipio = Nothing
            End If

            If aldea = "" Then
                aldea = Nothing
            End If

            ViewData("departamento") = departamento
            ViewData("municpio") = municipio
            ViewData("aldea") = aldea
            ViewData("fecha") = fechas


            Return PartialView(hogares.Fnc_obtener_fichas_no_remitidas(departamento, municipio, aldea, fechas))
            'Return PartialView(hogares.Fnc_obtener_prueba())
        End Function

#End Region

    End Class

#Region "Clase para gridview"

    Public NotInheritable Class exportGdvHistorialPagos

        Private Shared exportSettings As GridViewSettings

        Private Sub New()
        End Sub

        Public Shared ReadOnly Property ExportGridViewSettings() As GridViewSettings

            Get
                exportSettings = CreateExportGdvHistorialPagos()
                Return exportSettings
            End Get

        End Property

        Private Shared Function CreateExportGdvHistorialPagos() As GridViewSettings


            Dim settings As New GridViewSettings()

            settings.Name = "gvMaestroHistorialPagos"

            settings.SettingsPager.Position = PagerPosition.Bottom
            settings.SettingsPager.FirstPageButton.Visible = True
            settings.SettingsPager.LastPageButton.Visible = True
            settings.SettingsPager.PageSizeItemSettings.Visible = True
            settings.SettingsPager.PageSizeItemSettings.Items = New String() {"10", "20", "50"}
            settings.SettingsBehavior.AllowGroup = True
            settings.SettingsBehavior.AllowSort = True
            settings.Settings.ShowGroupPanel = True

            settings.EnableCallbackAnimation = True
            settings.EnablePagingCallbackAnimation = True
            settings.EnableCallbackCompression = True

            'settings.Settings.ShowHeaderFilterButton = True
            'settings.SettingsPopup.HeaderFilter.Height = 200

            settings.SettingsExport.FileName = "HISTORIAL DE PAGOS DE HOGAR"
            settings.Caption = "HISTORIAL DE PAGOS DE HOGAR"

            'necesarios para el detalle
            settings.KeyFieldName = "cod_pago"
            settings.SettingsDetail.AllowOnlyOneMasterRowExpanded = True
            settings.SettingsDetail.ShowDetailRow = True
            settings.SettingsDetail.ExportMode = GridViewDetailExportMode.Expanded

            settings.Columns.AddBand(
                Sub(band)
                    band.Caption = "PAGO"
                    band.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    band.Columns.Add("año_pago", "Año")
                    band.Columns.Add("numero_pago", "Número")
                    band.Columns.Add("descripcion_pago", "Descripción")
                End Sub)
            settings.Columns.Add("estado", "Estado")

            Return settings

        End Function

    End Class

    Public NotInheritable Class exportPvgFichasPorCensoAno

        Private Shared exportSettings As PivotGridSettings

        Private Sub New()
        End Sub

        Public Shared ReadOnly Property exportPvgSettings() As PivotGridSettings

            Get
                exportSettings = CreateExportPvgFichasPorCensoAno()
                Return exportSettings
            End Get

        End Property

        Private Shared Function CreateExportPvgFichasPorCensoAno() As PivotGridSettings

            Dim settings As New PivotGridSettings()

            settings.Name = "pvgFichasPorCensoAno"
            settings.Width = Unit.Percentage(100)
            settings.OptionsView.ShowHorizontalScrollBar = True

            settings.SettingsExport.OptionsPrint.PrintRowHeaders = DefaultBoolean.True
            settings.SettingsExport.OptionsPrint.PrintColumnHeaders = DefaultBoolean.True
            settings.SettingsExport.OptionsPrint.PrintHeadersOnEveryPage = True
            settings.SettingsExport.OptionsPrint.PrintFilterHeaders = DefaultBoolean.False
            settings.SettingsExport.OptionsPrint.PrintDataHeaders = DefaultBoolean.False

            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
                    field.Index = 0
                    field.Caption = "Año"
                    field.FieldName = "año_ficha"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
                    field.Index = 1
                    field.Caption = "Censo"
                    field.FieldName = "censo"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
                    field.Index = 0
                    field.Caption = "Fichas"
                    field.FieldName = "fichas"
                End Sub)

            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
                    field.Index = 0
                    field.Caption = "Departamento"
                    field.FieldName = "desc_departamento"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.FilterArea
                    'field.Index = 3
                    field.Caption = "Municipio"
                    field.FieldName = "desc_municipio"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.FilterArea
                    'field.Index = 4
                    field.Caption = "Aldea"
                    field.FieldName = "desc_aldea"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.FilterArea
                    'field.Index = 5
                    field.Caption = "Caserio"
                    field.FieldName = "desc_caserio"
                End Sub)
            
            Return settings

        End Function

    End Class

    Public NotInheritable Class exportPvgResultadoFichas

        Private Shared exportSettings As PivotGridSettings

        Private Sub New()
        End Sub

        Public Shared ReadOnly Property exportPvgSettings() As PivotGridSettings

            Get
                exportSettings = CreateExportPvgResultadoFichas()
                Return exportSettings
            End Get

        End Property

        Private Shared Function CreateExportPvgResultadoFichas() As PivotGridSettings

            Dim settings As New PivotGridSettings()

            settings.Name = "pvgResultadoFichas"
            settings.Width = Unit.Percentage(100)
            settings.OptionsView.ShowHorizontalScrollBar = True

            settings.SettingsExport.OptionsPrint.PrintRowHeaders = DefaultBoolean.True
            settings.SettingsExport.OptionsPrint.PrintColumnHeaders = DefaultBoolean.True
            settings.SettingsExport.OptionsPrint.PrintHeadersOnEveryPage = True
            settings.SettingsExport.OptionsPrint.PrintFilterHeaders = DefaultBoolean.False
            settings.SettingsExport.OptionsPrint.PrintDataHeaders = DefaultBoolean.False

            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
                    field.Index = 1
                    field.Caption = "Resultado"
                    field.FieldName = "estado"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
                    field.Index = 0
                    field.Caption = "Fichas"
                    field.FieldName = "fichas"
                End Sub)

            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
                    field.Index = 0
                    field.Caption = "Departamento"
                    field.FieldName = "desc_departamento"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.FilterArea
                    'field.Index = 3
                    field.Caption = "Municipio"
                    field.FieldName = "desc_municipio"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.FilterArea
                    'field.Index = 4
                    field.Caption = "Aldea"
                    field.FieldName = "desc_aldea"
                End Sub)
            settings.Fields.Add(
                Sub(field)
                    field.Area = DevExpress.XtraPivotGrid.PivotArea.FilterArea
                    'field.Index = 5
                    field.Caption = "Caserio"
                    field.FieldName = "desc_caserio"
                End Sub)

            Return settings

        End Function

    End Class

#End Region

End Namespace